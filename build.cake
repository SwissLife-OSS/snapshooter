#addin "nuget:?package=Cake.Sonar&version=1.1.22"
#addin "nuget:?package=Cake.FileHelpers&version=3.2.1"
#tool "nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.6.0"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");

var version = EnvironmentVariable<string>("Version", default(string));
var configuration = EnvironmentVariable<string>("Build_Configuration", "Release");
var nugetToken = EnvironmentVariable<string>("Nuget_Token", default(string));
var sonarLogin = EnvironmentVariable<string>("Sonar_Token", default(string));
var sonarPrKey = EnvironmentVariable<string>("Sonar_Pr_Key", default(string));
var sonarBranch = EnvironmentVariable<string>("Sonar_Branch", default(string));

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
var testOutputDir = Directory("./testoutput");
var publishOutputDir = Directory("./artifacts");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    DotNetCoreClean("./src");
});

Task("Restore")
    .Does(() =>
{
    DotNetCoreRestore("./src");
});

Task("Build")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    };

    DotNetCoreBuild("./src", settings);
});

Task("Publish")
    .Does(() =>
{
    using(var process = StartAndReturnProcess("msbuild",
        new ProcessSettings{ Arguments = "./src/Snapshooter.sln /t:restore /p:configuration=" + configuration }))
    {
        process.WaitForExit();
    }

    using(var process = StartAndReturnProcess("msbuild",
        new ProcessSettings{ Arguments = "./src/Snapshooter.sln /t:build /p:configuration=" + configuration }))
    {
        process.WaitForExit();
    }

    using(var process = StartAndReturnProcess("msbuild",
        new ProcessSettings{ Arguments = "./src/Snapshooter.sln /t:pack /p:configuration=" + configuration + " /p:IncludeSource=true /p:IncludeSymbols=true" }))
    {
        process.WaitForExit();
    }
});

Task("Push")
    .Does(() =>
{
    var packages = GetFiles("./**/Snapshooter.*.nupkg");

    NuGetPush(packages, new NuGetPushSettings {
        Source = "https://api.nuget.org/v3/index.json",
        ApiKey = nugetToken
    });
});

Task("Tests")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings
    {
        Configuration = "Debug"
    };

    int i = 0;
    var testSettings = new DotNetCoreTestSettings
    {
        Configuration = "Debug",
        ResultsDirectory = $"./{testOutputDir}",
        Logger = "trx",
        NoRestore = true,
        NoBuild = true,
        ArgumentCustomization = args => args
            .Append("/p:CollectCoverage=true")
            .Append("/p:Exclude=[xunit.*]*")
            .Append("/p:CoverletOutputFormat=opencover")
            .Append($"/p:CoverletOutput=\"../../{testOutputDir}/full_{i++}\" --blame")
    };

    DotNetCoreBuild("./src", buildSettings);

    foreach(var file in GetFiles("./src/**/*.Tests.csproj"))
    {
        DotNetCoreTest(file.FullPath, testSettings);
    }
});

Task("SonarBegin")
    .Does(() =>
{
    SonarBegin(new SonarBeginSettings
    {
        Url = "https://sonarcloud.io",
        Login = sonarLogin,
        Key = "SwissLife-OSS_Snapshooter",
        Organization = "swisslife",
        VsTestReportsPath = "**/*.trx",
        OpenCoverReportsPath = "**/*.opencover.xml",
        Exclusions = "**/*.js,**/*.html,**/*.css,**/examples/**/*.*,**/benchmarks/**/*.*,**/src/Templates/**/*.*",
        Verbose = false,
        Version = version,
        ArgumentCustomization = args => {
            var a = args;

            if(!string.IsNullOrEmpty(sonarPrKey))
            {
                a = a.Append($"/d:sonar.pullrequest.key=\"{sonarPrKey}\"");
                a = a.Append($"/d:sonar.pullrequest.branch=\"{sonarBranch}\"");
                a = a.Append($"/d:sonar.pullrequest.base=\"master\"");
                a = a.Append($"/d:sonar.pullrequest.provider=\"github\"");
                a = a.Append($"/d:sonar.pullrequest.github.repository=\"SwissLife-OSS/Snapshooter\"");
            }

            return a;
        }
    });
});

Task("SonarEnd")
    .Does(() =>
{
    SonarEnd(new SonarEndSettings
    {
        Login = sonarLogin,
    });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Tests");

Task("Sonar")
    .IsDependentOn("SonarBegin")
    .IsDependentOn("Tests")
    .IsDependentOn("SonarEnd");

Task("Release")
    .IsDependentOn("Sonar")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
