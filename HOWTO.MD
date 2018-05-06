# How to Build

- Open PowerShell; browse to root of the repository
- Note: you have to be online for this to work; since it does fresh package restore
- run ```.\build.ps1```; you should see output as follows:

```PowerShell
PS C:\Projects\Github\RulesRUs> .\build.ps1
Preparing to run build script...
Running build script...

========================================
Clean
========================================

========================================
Restore-NuGet-Packages
========================================
  Restore completed in 70.63 ms 
.
.

========================================
Build
========================================
Microsoft (R) Build Engine version 15.6.84.34536 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

.
.

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:20.17

========================================
Run-Unit-Tests
========================================
running unit tests
Test run for .....RuleEngine.Tests.dll(.NETCoreApp,Version=v2.0)
Microsoft (R) Test Execution Command Line Tool Version 15.6.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
[xUnit.net 00:00:00.7926568]   Discovering: RuleEngine.Tests
[xUnit.net 00:00:00.9801770]   Discovered:  RuleEngine.Tests
[xUnit.net 00:00:00.9922466]   Starting:    RuleEngine.Tests
[xUnit.net 00:00:01.5835264]   Finished:    RuleEngine.Tests

Total tests: 91. Passed: 91. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 3.0426 Seconds
Test run for ....RuleFactory.Tests.dll(.NETCoreApp,Version=v2.0)
Microsoft (R) Test Execution Command Line Tool Version 15.6.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
[xUnit.net 00:00:00.8236596]   Discovering: RuleFactory.Tests
[xUnit.net 00:00:01.0202048]   Discovered:  RuleFactory.Tests
[xUnit.net 00:00:01.0301900]   Starting:    RuleFactory.Tests
[xUnit.net 00:00:01.8023745]   Finished:    RuleFactory.Tests

Total tests: 115. Passed: 115. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 3.1493 Seconds

========================================
Default
========================================

Task                          Duration
--------------------------------------------------
Clean                         00:00:00.1291243
Restore-NuGet-Packages        00:00:07.0229522
Build                         00:00:21.3961425
Run-Unit-Tests                00:00:12.9261100
--------------------------------------------------
Total:                        00:00:41.4879563
```


# How to Build Nuget Package

- Open PowerShell; change directory to root of the repository
- Make sure version, release notes, etc. had been updated
- create a local directory for the output (like c:\nuget-local)
- run ```dotnet pack .\src\RuleEngine\RuleEngine.csproj -c Release -o C:\nuget-local\ --include-symbols```; you should see ouput like below:

```PowerShell
PS C:\Projects\Github\RulesRUs> dotnet pack .\src\RuleEngine\RuleEngine.csproj -c Release -o C:\nuget-local\ --include-symbols
Microsoft (R) Build Engine version 15.6.84.34536 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 194.73 ms for C:\Projects\Github\RulesRUs\src\RuleEngine\RuleEngine.csproj.
  RuleEngine -> C:\Projects\Github\RulesRUs\src\RuleEngine\bin\Release\netstandard2.0\RuleEngine.dll
  Successfully created package 'C:\nuget-local\RuleEngine.___.nupkg'.
  Successfully created package 'C:\nuget-local\RuleEngine.___.symbols.nupkg'.
```

## Push to Nuget
- Open command prompt; change directory to c:\nuget-local (because my nuget.exe is there and dotnet-pack outputs are there)
- Set the API Key:
```PowerShell
PS C:\nuget-local> .\nuget SetApiKey ****
The API Key '****' was saved for the NuGet gallery (https://www.nuget.org) and the symbol server (https://nuget.smbsrc.net/).
```
- Push Package
```PowerShell
PS C:\nuget-local> .\nuget push RuleEngine.___.nupkg -source https://api.nuget.org/v3/index.json
Pushing RuleEngine.___.nupkg to 'https://www.nuget.org/api/v2/package'...
  PUT https://www.nuget.org/api/v2/package/
.
.
```
- Push Symbols
```PowerShell
PS C:\nuget-local> .\nuget push RuleEngine.___.symbols.nupkg -source https://www.symbolsource.org/Public/Metadata/NuGet
WARNING: No API Key was provided and no API Key could be found for 'https://www.symbolsource.org/Public/Metadata/NuGet'. To save an API Key for a source use the 'setApiKey' command.
Pushing RuleEngine.___.symbols.nupkg to 'https://www.symbolsource.org/Public/Metadata/NuGet'...
  PUT https://www.symbolsource.org/Public/Metadata/NuGet/
  OK https://www.symbolsource.org/Public/Metadata/NuGet/ 36044ms
Your package was pushed.
.
.
```