#tool "nuget:?package=GitVersion.CommandLine&prerelease"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionFile = "./LinqBuilder.sln";
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
});

Task("NuGet-Restore")
    .Does(() =>
{
	MSBuild(solutionFile, new MSBuildSettings()
		.SetConfiguration(configuration)
		.WithProperty("Version", semVersion)
		.WithTarget("restore")
	);
});

Task("Build")
    .IsDependentOn("NuGet-Restore")
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
	foreach (var file in GetFiles("./test/*/*.csproj"))
	{
		DotNetCoreTest(file.FullPath, new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		});
	}
});

Task("Package")
	.IsDependentOn("Clean")
	.IsDependentOn("Version")
	.IsDependentOn("Test")
    .Does(() =>
{
	foreach (var file in GetFiles("./src/*/*.csproj"))
	{
		MSBuild(file, new MSBuildSettings()
			.SetConfiguration(configuration)
			.WithProperty("IncludeSymbols", "true")
			.WithProperty("IncludeSource", "true")
			.WithProperty("Version", semVersion)
			.WithTarget("pack")
		);
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
		DeleteDirectory(directory, true);
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