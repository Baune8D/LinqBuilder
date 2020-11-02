// Install modules.
#module nuget:?package=Cake.DotNetTool.Module&version=0.4.0

// Install addins.
#addin nuget:?package=Cake.Coverlet&version=2.5.1

// Install tools.
#tool nuget:?package=ReportGenerator&version=4.7.1

// Install .NET Core Global tools.
#tool dotnet:?package=GitVersion.Tool&version=5.5.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = File("./LinqBuilder.sln");
var coverageFilename = "coverage";

var artifactsFolder = Directory("./artifacts");
var coverageFolder = Directory("./coverage");

var excludeFolders = new GlobberSettings
{
  Predicate = fileSystemInfo =>
	!fileSystemInfo.Path.FullPath.Contains("/bin") &&
	!fileSystemInfo.Path.FullPath.Contains("/obj")
};

string semVersion;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Version")
    .Does(() =>
{
	Information("Calculating software version");

    if (AppVeyor.IsRunningOnAppVeyor)
    {
        GitVersion(new GitVersionSettings
        {
            OutputType = GitVersionOutput.BuildServer,
            UpdateAssemblyInfo = true,
            NoFetch = true
        });
    }

    var result = GitVersion(new GitVersionSettings
	{
		OutputType = GitVersionOutput.Json,
		NoFetch = true
    });

	semVersion = result.NuGetVersionV2;

	Information($"SemVersion is: {semVersion}");
});

Task("Build")
    .Does(() =>
{
	Information("Building solution");

	DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        MSBuildSettings = new DotNetCoreMSBuildSettings
        {
            TreatAllWarningsAs = MSBuildTreatAllWarningsAs.Error
        }
	});
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Removing old coverage");
    DeleteDirectoryIfExists(coverageFolder);

    Information("Running tests and generating coverage");

    var testSettings = new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoRestore = true,
        NoBuild = true
    };

    var coverageSettings = new CoverletSettings {
        CollectCoverage = true,
        MergeWithFile = coverageFolder + File($"{coverageFilename}.json"),
        CoverletOutputDirectory = coverageFolder,
        CoverletOutputName = coverageFilename,
        CoverletOutputFormat = CoverletOutputFormat.json | CoverletOutputFormat.opencover,
        Exclude = {
            "[LinqBuilder.*Tests.Shared]*"
        }
    };

    foreach (var file in GetFiles("./src/**/*.Tests/*.csproj", excludeFolders))
    {
        DotNetCoreTest(file.FullPath, testSettings, coverageSettings);
    }

    if (!AppVeyor.IsRunningOnAppVeyor)
    {
        Information("Generating coverage report");
        ReportGenerator(coverageFolder + File($"{coverageFilename}.opencover.xml"), coverageFolder);
    }
});

Task("Package")
	.IsDependentOn("Version")
	.IsDependentOn("Test")
    .Does(() =>
{
	Information("Cleaning artifacts");
	DeleteDirectoryIfExists(artifactsFolder);

	Information("Packaging libraries to artifacts directory");

    DotNetCorePack(solutionFile, new DotNetCorePackSettings
    {
        Configuration = configuration,
        IncludeSymbols = true,
        IncludeSource = true,
        OutputDirectory = artifactsFolder,
        MSBuildSettings = new DotNetCoreMSBuildSettings()
            .SetVersion(semVersion)
            .WithProperty("SymbolPackageFormat", "snupkg")
    });
});

Task("Upload-Artifacts")
    .IsDependentOn("Package")
    .Does(() =>
{
	if (AppVeyor.IsRunningOnAppVeyor)
	{
		Information("Uploading artifacts to AppVeyor");

		foreach (var file in GetFiles(artifactsFolder.Path + "/*.nupkg"))
		{
			AppVeyor.UploadArtifact(file);
		}
	}
	else
	{
		Information("Nothing to do");
	}
});

Task("NuGet-Push")
    .IsDependentOn("Package")
    .Does(() =>
{
	if (AppVeyor.IsRunningOnAppVeyor)
	{
		if (EnvironmentVariable("APPVEYOR_REPO_TAG") == "true")
		{
			Information("Pushing artifacts to NuGet repository");

			foreach (var file in GetFiles(artifactsFolder.Path + "/*.nupkg"))
			{
                NuGetPush(file, new NuGetPushSettings
                {
                    Source = "https://api.nuget.org/v3/index.json",
                    ApiKey = EnvironmentVariable("NUGET_API_KEY")
                });
			}
		}
		else if (EnvironmentVariable("APPVEYOR_REPO_BRANCH") == "master")
		{
			Information("Pushing artifacts to MyGet repository");

			foreach (var file in GetFiles(artifactsFolder.Path + "/*.nupkg"))
			{
				if (file.ToString().EndsWith(".symbols.nupkg"))
				{
					NuGetPush(file, new NuGetPushSettings
					{
						Source = "https://www.myget.org/F/baunegaard/api/v3/index.json",
						ApiKey = EnvironmentVariable("MYGET_API_KEY")
					});
				}
				else
				{
					NuGetPush(file, new NuGetPushSettings
					{
						Source = "https://www.myget.org/F/baunegaard/api/v2/package",
						ApiKey = EnvironmentVariable("MYGET_API_KEY")
					});
				}
			}
		}
		else
		{
			Information("Nothing to do");
		}
	}
	else
	{
		Information("Nothing to do");
	}
});

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

void DeleteDirectoryIfExists(ConvertableDirectoryPath path)
{
	if (DirectoryExists(path))
	{
		DeleteDirectory(path, new DeleteDirectorySettings
		{
			Recursive = true,
			Force = true
		});
	}
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Upload-Artifacts")
	.IsDependentOn("NuGet-Push");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
