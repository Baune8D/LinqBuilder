#tool "GitVersion.CommandLine&version=4.0.0-beta0012"
#tool "OpenCover&version=4.6.519"
#tool "ReportGenerator&version=3.1.1"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = File("./LinqBuilder.sln");
var coverageResult = File("./coverage.xml");

var artifactsFolder = Directory("./artifacts");
var coverageFolder = Directory("./coverage");

Func<IDirectory, bool> excludeFolders = fileSystemInfo => 
	!fileSystemInfo.Path.FullPath.Contains("/bin") &&
	!fileSystemInfo.Path.FullPath.Contains("/obj");

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
			UpdateAssemblyInfo = true
		});
	}

    var result = GitVersion(new GitVersionSettings 
	{
		OutputType = GitVersionOutput.Json
    });

	semVersion = result.NuGetVersionV2;

	Information($"SemVersion is: {semVersion}");
});

Task("Restore")
	.IsDependentOn("Version")
    .Does(() =>
{
	Information("Restoring NuGet packages");

	DotNetCoreRestore(solutionFile, new DotNetCoreRestoreSettings
	{
		MSBuildSettings = new DotNetCoreMSBuildSettings()
			.SetVersion(semVersion)
	});
});

Task("Build")
	.IsDependentOn("Restore")
    .Does(() =>
{
	Information("Building solution");

	DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings
    {
        Configuration = configuration
	});
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
	Information("Removing old coverage.xml");

	DeleteFileIfExists(coverageResult);

	Information("Running tests and generating coverage");

	string[] coverageFilters = 
	{
		"+[LinqBuilder.*]*",
		"-[LinqBuilder.*.Tests]*"
	};

	var settings = new OpenCoverSettings
	{
		OldStyle = true,
		MergeOutput = true,
		SkipAutoProps = true,
		ReturnTargetCodeOffset = 0
	}
	.ExcludeByAttribute("*.ExcludeFromCodeCoverage*");

	foreach (var filter in coverageFilters)
	{
		settings.WithFilter(filter);
	}

	var parameters = $"--fx-version 2.0.5 -nobuild -configuration {configuration}";

	foreach (var file in GetFiles("./test/*/*.csproj", excludeFolders))
	{
		settings.WorkingDirectory = file.GetDirectory();

		OpenCover(tool => 
		{
			tool.DotNetCoreTool(file, "xunit", parameters);
		},
		coverageResult, settings);
	}
});

Task("Package")
	.IsDependentOn("Test")
    .Does(() =>
{
	Information("Cleaning artifacts directory");

	DeleteDirectoryIfExists(artifactsFolder);

	Information("Packaging libraries to artifacts directory");

	foreach (var file in GetFiles("./src/*/*.csproj", excludeFolders))
	{
		DotNetCorePack(file.FullPath, new DotNetCorePackSettings
		{
			Configuration = configuration,
			IncludeSymbols = true,
			IncludeSource = true,
			MSBuildSettings = new DotNetCoreMSBuildSettings()
				.SetVersion(semVersion)
		});
	}

	CreateDirectoryIfNotExists(artifactsFolder);
	MoveFiles("./src/*/bin/*/*.nupkg", artifactsFolder);
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
});

Task("NuGet-Push")
    .IsDependentOn("Package")
    .Does(() =>
{
	if (AppVeyor.IsRunningOnAppVeyor && EnvironmentVariable("APPVEYOR_REPO_TAG") == "true")
	{
		Information("Pushing artifacts to MyGet repository");

		foreach (var file in GetFiles(artifactsFolder.Path + "/*.nupkg"))
		{
			if (file.ToString().Contains(".symbols.nupkg"))
			{
				NuGetPush(file, new NuGetPushSettings 
				{
					Source = "https://www.myget.org/F/baunegaard/symbols/api/v2/package",
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
});

Task("Coverage-Report")
	.IsDependentOn("Test")
    .Does(() =>
{
	Information("Generating coverage report");

	DeleteDirectoryIfExists(coverageFolder);
	ReportGenerator(coverageResult, coverageFolder);
});

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

void CreateDirectoryIfNotExists(ConvertableDirectoryPath path)
{
	if (!DirectoryExists(path))
	{
		CreateDirectory(path);
	}
}

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

void DeleteFileIfExists(ConvertableFilePath path)
{
	if (FileExists(path))
	{
		DeleteFile(path);
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