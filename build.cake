#tool "GitVersion.CommandLine&version=4.0.0-beta0012"
#tool "OpenCover"
#tool "ReportGenerator"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = "./LinqBuilder.sln";
var coverageResult = "./coverage.xml";

string[] coverageFilters = 
{
	"+[LinqBuilder.*]*",
	"-[LinqBuilder.*.Tests]*"
};

string semVersion = null;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
	DeleteDirectoryIfExists("./artifacts");
	DeleteFiles("./src/*/bin/*/*.nupkg");
});

Task("Version")
    .Does(() =>
{
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

	Information("SemVersion is: " + semVersion);
});

Task("Restore")
	.IsDependentOn("Version")
    .Does(() =>
{
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
	DotNetCoreBuild(solutionFile, new DotNetCoreBuildSettings
    {
        Configuration = configuration
	});
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
	DeleteFileIfExists(coverageResult);

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

	var parameters = "--fx-version 2.0.3 -nobuild -configuration " + configuration;

	foreach (var file in GetFiles("./test/*/*.csproj"))
	{
		settings.WorkingDirectory = file.GetDirectory();

		OpenCover(tool => 
		{
			tool.DotNetCoreTool(file, "xunit", parameters);
		},
		coverageResult, settings);
	}
});

Task("Coverage-Report")
	.IsDependentOn("Test")
    .Does(() =>
{
	DeleteDirectoryIfExists(coverageResult);

	ReportGenerator(coverageResult, "./coverage");
});

Task("Package")
	.IsDependentOn("Clean")
	.IsDependentOn("Test")
    .Does(() =>
{
	foreach (var file in GetFiles("./src/*/*.csproj"))
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

	CreateDirectoryIfNotExists("./artifacts");
	MoveFiles("./src/*/bin/*/*.nupkg", "./artifacts");
});

Task("Upload-Artifacts")
    .IsDependentOn("Package")
    .Does(() =>
{
	foreach (var file in GetFiles("./artifacts/*.nupkg"))
	{
		if (AppVeyor.IsRunningOnAppVeyor)
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
		foreach (var file in GetFiles("./artifacts/*.nupkg"))
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

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

void CreateDirectoryIfNotExists(string path)
{
	var directory = Directory(path);
	if (!DirectoryExists(directory))
	{
		CreateDirectory(directory);
	}
}

void DeleteDirectoryIfExists(string path)
{
	var directory = Directory(path);
	if (DirectoryExists(directory))
	{
		DeleteDirectory(directory, new DeleteDirectorySettings
		{
			Recursive = true,
			Force = true
		});
	}
}

void DeleteFileIfExists(string path)
{
	var file = File(path);
	if (FileExists(file))
	{
		DeleteFile(file);
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