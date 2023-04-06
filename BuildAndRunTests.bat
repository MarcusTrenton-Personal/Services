@echo off

msbuild ServicesTests\ServicesTests.csproj -verbosity:quiet
if NOT %errorlevel% == 0 exit /B %errorlevel%
vstest.console.exe ServicesTests\bin\Debug\netcoreapp3.1\ServicesTests.dll /Settings:TestUtils\FullTestCleanup.runsettings
if NOT %errorlevel% == 0 exit /B %errorlevel%
