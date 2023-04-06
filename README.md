# Setup instructions for developers

This repo does not assume a platform.

## Libraries and SDKS

### .NET 7.0

* SDK direct download link for platform of choice: https://dotnet.microsoft.com/en-us/download/dotnet/7.0

## Path

All the following must be in system path for the included scripts to function:
* vstest.console.exe (For running tests)
* msbuild.exe	(For building the tests before pushing)
* git.exe (For a tagging script)
Find those executables using the `where` command:

`where /R C:\ vstest.console` will yield a result like C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe

`where /R C:\ msbuild` will yield a result like C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe

`where /R C:\ git` will yield a result like C:\Users\Marcus\AppData\Local\Atlassian\SourceTree\git_local\bin\git.exe, if you use SourceTree for source control.

### Release Managers Only

Release Managers will also need to use Sign Tool for security.

`where /R C:\ signtool` will yield a result like C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\signtool.exe

## Environmental Variables

You must set environmental variables before starting Visual Studio or the command line prompt.

### Release Managers Only

Release Managers will need to setup environmental variables for "SigningKey" file path, "SigningKeyPassword", and "NugetGitToken". The NugetGitToken is only valid for a fixed duration. Afterwards it will need to be regenerated.

How do you actually get the signing key, password, and token? Email MarcusTrenton@gmail.com with a good reason why you want to sign software in his name.

Without these variables, the publishing targets will not succeed.

## Git Hooks

To run automated tests before each push, follow the instructions in pre-push.template.

# Automated Testing

Successful tests are automatically required before every publish. The publish action will compile and run the tests. Assuming the pre-push Git Hook was setup (see above), the tests are also built and run in the same way.

# Dev Procedure

## Versioning

Versioning has the format Major.Minor.Day.Minute. 
- Major: Increment when backwards compatiblity is broken.
- Minor: Increment for feature additions and bug fixes. Recent to 0 when the Major number increments
- Day: Part of the build timestamp of form XXYYY, where XX is years since 2020 and YYY is the day of the year. For example 2023/01/05 would be 3005. Automatically set per build.
- Minute: Part of the build timestamp. It measures minutes since midnight UTC time. Automatically set per build.

To publish, run PublishAndTag.bat. That script will automatically publish to Dropbox and tag the last commit.

## File Encoding

All user input files, such as localization, and output files are in UTF-8.
