using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.Execution;
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
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.Codecov.CodecovTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
[AppVeyor(
    AppVeyorImage.VisualStudio2022,
    InvokedTargets = new[]
    {
        nameof(UploadCodecov),
        nameof(PushNuGet),
        nameof(PushMyGet),
    })]
[AppVeyorSecret("MYGET_API_KEY", "78qy8e6pKfJlQV7RAG5tJOWegzXpjASkUs3aFdVBoPYA5gi6+mWdjbuAmNa5OQPe")]
[AppVeyorSecret("NUGET_API_KEY", "IvV8EXsJ4sMQb+AxZ983lPt5fwCDlhux8IM+1hUKOO9uRh5Y757KpXcCNqwjqunL")]
[AppVeyorSecret("CODECOV_TOKEN", "EHy2Ls7M8sCfmxReWmbRxdkNkA/AqTTgg+6SCc1ww1JgHXCFmS/bpHJOzGt1VYAm")]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter] [Secret] string MyGetApiKey { get; set; }
    [Parameter] [Secret] string NuGetApiKey { get; set; }
    [Parameter] [Secret] string CodecovToken { get; set; }

    [Solution] readonly Solution Solution;
    [GitVersion] readonly GitVersion GitVersion;
    [CI] readonly AppVeyor AppVeyor;

    static AbsolutePath SourceDirectory => RootDirectory / "src";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    static AbsolutePath CoverageDirectory => RootDirectory / "coverage";
    static AbsolutePath CoverageResults => CoverageDirectory / "results";

    static IReadOnlyCollection<AbsolutePath> Artifacts => ArtifactsDirectory.GlobFiles("*.nupkg");

    Target Clean => _ => _
        .Before(Compile)
        .Executes(() =>
        {
            SourceDirectory
                .GlobDirectories("**/bin", "**/obj")
                .ForEach(DeleteDirectory);

            EnsureCleanDirectory(ArtifactsDirectory);
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
                .EnableTreatWarningsAsErrors());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            EnsureCleanDirectory(CoverageDirectory);

            var projects = Solution.GetProjects("*.Tests");

            DotNetTest(s => s
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .EnableCollectCoverage()
                .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                .CombineWith(projects, (ss, project) => ss
                    .SetProjectFile(project)
                    .SetCoverletOutput(CoverageResults / $"{project.Name}.cobertura.xml")),
                degreeOfParallelism: Environment.ProcessorCount);

            if (IsLocalBuild)
            {
                ReportGenerator(s => s
                    .SetFramework("net6.0")
                    .SetReports(CoverageResults / "*.cobertura.*.xml")
                    .SetTargetDirectory(CoverageDirectory / "report"));
            }
        });

    Target Package => _ => _
        .DependsOn(Test)
        .Produces(ArtifactsDirectory / "*.nupkg")
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);

            DotNetPack(s => s
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .EnableIncludeSymbols()
                .EnableIncludeSource()
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion(GitVersion.SemVer)
                .SetSymbolPackageFormat(DotNetSymbolPackageFormat.snupkg));
        });

    Target PushMyGet => _ => _
        .DependsOn(Package)
        .OnlyWhenStatic(() => IsServerBuild && AppVeyor.BranchIsMaster())
        .Executes(() =>
        {
            NuGetPush(s => s
                .SetSource("https://www.myget.org/F/baunegaard/api/v2/package")
                .SetSymbolSource("https://www.myget.org/F/baunegaard/api/v3/index.json")
                .SetApiKey(MyGetApiKey)
                .CombineWith(Artifacts, (ss, artifact) => ss
                    .SetTargetPath(artifact)),
                degreeOfParallelism: Environment.ProcessorCount);
        });

    Target PushNuGet => _ => _
        .DependsOn(Package)
        .OnlyWhenStatic(() => IsServerBuild && AppVeyor.RepositoryTag)
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
            var files = CoverageResults
                .GlobFiles("*.cobertura.*.xml")
                .Select(file => file.ToString());

            Codecov(s => s
                .SetFramework("net5.0")
                .SetFiles(files)
                .SetToken(CodecovToken));
        });
}