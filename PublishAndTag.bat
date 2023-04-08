@echo off
REM As a SourceTree custom action, for greatest convenience, enable the "Show Full Output" and "Run command silently" options

echo Publishing a Local build
msbuild "Services\Services.csproj" -target:publish -property:PublishProfile=\Services\Properties\PublishProfiles\Local.pubxml -verbosity:quiet
if NOT %errorlevel% == 0 exit /B %errorlevel%

echo Extracting version number from the Local build
cd FileVersionOf
dotnet restore
dotnet msbuild -p:Configuration=Release
if NOT %errorlevel% == 0 exit /B %errorlevel%
cd ..
REM Sadly batch files cannot directly assign the output of a command to a variable. This is an industry-standard work-around hack.
for /f %%i in ('FileVersionOf\bin\Release\net7.0\FileVersionOf.exe Services\bin\Release\net7.0\Services.dll') do set VERSION=%%i

echo Pushing a Git tag with the version number v%VERSION%
git tag v%VERSION%
if NOT %errorlevel% == 0 exit /B %errorlevel%
git push origin v%VERSION%
if NOT %errorlevel% == 0 exit /B %errorlevel%

echo Pushing a Nuget Package with version number v%VERSION%
dotnet nuget push "Services\bin\Release\Services.%VERSION%.nupkg" --api-key %NugetGitToken% --source "github"
if NOT %errorlevel% == 0 exit /B %errorlevel%

echo New version successfully published