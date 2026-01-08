using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Codecov;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.Codecov.CodecovTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter(Name = "MYGET_API_KEY")] [Secret] string MyGetApiKey { get; set; }
    [Parameter(Name = "NUGET_API_KEY")] [Secret] string NuGetApiKey { get; set; }
    [Parameter(Name = "CODECOV_TOKEN")] [Secret] string CodecovToken { get; set; }

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;
    [CI] readonly GitHubActions GitHubActions;

    static AbsolutePath SourceDirectory => RootDirectory / "src";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    static AbsolutePath CoverageDirectory => RootDirectory / "coverage";

    static AbsolutePath CoverageResult => CoverageDirectory / "Cobertura.xml";

    static IEnumerable<AbsolutePath> Artifacts => ArtifactsDirectory.GlobFiles("*.nupkg");

    Target Clean => _ => _
        .Before(Compile)
        .Executes(() =>
        {
            SourceDirectory
                .GlobDirectories("**/bin", "**/obj")
                .ForEach(path => path.DeleteDirectory());

            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Compile => _ => _
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableLockedMode()
                .EnableTreatWarningsAsErrors());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            CoverageDirectory.CreateOrCleanDirectory();
            TestResultFolders.ForEach(directory => directory.DeleteDirectory());

            DotNetTest(s => s
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetDataCollector("XPlat Code Coverage")
                .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                .CombineWith(TestProjects, (ss, project) => ss
                    .SetProjectFile(project)),
                degreeOfParallelism: Environment.ProcessorCount);

            ReportGenerator(s => s
                .SetReports(CoverageResults)
                .SetTargetDirectory(CoverageDirectory)
                .SetReportTypes(ReportTypes.Cobertura));

            if (IsLocalBuild)
            {
                ReportGenerator(s => s
                    .SetReports(CoverageResult)
                    .SetTargetDirectory(CoverageDirectory));
            }
        });

    Target Package => _ => _
        .DependsOn(Test)
        .Produces(ArtifactsDirectory / "*.nupkg")
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();

            DotNetPack(s => s
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion(GitVersion.SemVer));
        });

    Target PushMyGet => _ => _
        .DependsOn(Package)
        .OnlyWhenStatic(() => IsServerBuild && GitRepository.IsOnMainBranch())
        .Executes(() =>
        {
            NuGetPush(s => s
                .SetSource("https://www.myget.org/F/baunegaard/api/v2/package")
                .SetApiKey(MyGetApiKey)
                .CombineWith(Artifacts, (ss, artifact) => ss
                    .SetTargetPath(artifact)),
                degreeOfParallelism: Environment.ProcessorCount);
        });

    Target PushNuGet => _ => _
        .DependsOn(Package)
        .OnlyWhenStatic(() => IsServerBuild && GitHubActions.RefType == "tag")
        .Executes(() =>
        {
            NuGetPush(s => s
                .SetSource("https://api.nuget.org/v3/index.json")
                .SetApiKey(NuGetApiKey)
                .CombineWith(Artifacts, (ss, artifact) => ss
                    .SetTargetPath(artifact)),
                degreeOfParallelism: Environment.ProcessorCount);
        });

    Target UploadCodecov => _ => _
        .DependsOn(Test)
        .OnlyWhenStatic(() => IsServerBuild)
        .Executes(() =>
        {
            Codecov(s => s
                .SetFiles(CoverageResult)
                .SetToken(CodecovToken));
        });

    IEnumerable<Project> TestProjects => Solution.GetAllProjects("*.Tests");

    IEnumerable<AbsolutePath> TestResultFolders => TestProjects
        .SelectMany(project => project.Directory.GlobDirectories("TestResults"));

    IEnumerable<string> CoverageResults => TestResultFolders
        .SelectMany(testResults => testResults.GlobDirectories("*"))
        .SelectMany(output => output.GlobFiles("coverage.cobertura.xml"))
        .Select(file => file.ToString());
}
