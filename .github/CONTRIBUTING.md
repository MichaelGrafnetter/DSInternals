![DSInternals Logo](DSInternals-Dark.png#gh-light-mode-only)
![DSInternals Logo](DSInternals-Light.png#gh-dark-mode-only)

# Contributing to the Project

## How can I contribute?

Any contributions to this project are warmly welcome. These are the most important areas:
- Bug reports
- Feature requests
- Testing the module against different Active Directory configurations
- Documentation
    * PowerShell help
    * XML documentation comments in the source code
- Code
    * Bug fixes
    * New features
    * Test cases
    * Code cleanup (StyleCop, FXCop, String to Resource extraction,...)

## Submitting Bug Reports

When submitting a [new bug report](https://github.com/MichaelGrafnetter/DSInternals/issues), please do not forget to mention the version of the target domain controller. The following information would also be very useful:

```powershell
# The entire command that caused the error
Get-History

# Error message
$Error[0].Exception.Message

# The entire exception stack trace
$Error[0].Exception.StackTrace

# Version of the DSInternals module
(Get-Module -Name DSInternals -ListAvailable).Version.ToString()

# OS on which the command was running
(Get-WmiObject -Class Win32_OperatingSystem).Caption

# PowerShell version
$PSVersion

# Anything else that might help identifying and fixing the issue
```

## Building from Source Code

### Development Environment

If you want to build the module from source code yourself, you need to install these programs first:
- [Microsoft Visual Studio Community](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx) 2022 with these features installed:
   * .NET Framework 4.7.2 targeting pack
   * C++ 2022 Redistributable Update
   * C++/CLI support for v143 build tools (Latest)
   * MSVC v143 - VS 2022 C++ x64/x86 build tools (Latest)
   * MSVC v143 - VS 2022 C++ ARM64/ARM64EC build tools (Latest)
   * Windows Universal C Runtime
   * Windows 11 SDK (you might have to retarget the [DSInternals.Replication.Interop](../Src/DSInternals.Replication.Interop/DSInternals.Replication.Interop.vcxproj) project to the version you have)
   * PowerShell Tools for Visual Studio (optional)
   * Git for Windows (optional)
   * GitHub Extension for Visual Studio (optional)
- [Windows Management Framework 5](https://www.microsoft.com/en-us/download/details.aspx?id=50395).

To make IntelliSense work with *.psm1 files, the following code needs to be added to the `C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Xml\Schemas\catalog.xml` file:

```xml
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Format.xsd" enableValidation="true"/>
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Types.xsd" enableValidation="true"/>
```

### Build Steps
1. Install the prerequisities.
2. Checkout or download the source codes.
3. Run the [Scripts\Make.ps1](../Scripts/Make.ps1) script from PowerShell.
4. The resulting module will appear in the *Build\bin\Release* folder.

### Debugging

1. Open the [Src\DSInternals.sln](../Src/DSInternals.sln) file in Visual Studio.
2. Put any cmdlets you wish to debug into the [Src\DSInternals.PowerShell\Run-Cmdlets.ps1](../Src/DSInternals.PowerShell/Run-Cmdlets.ps1) script.
3. Set the [DSInternals.PowerShell](../Src/DSInternals.PowerShell/DSInternals.PowerShell.csproj) project as StartUp Project.
4. Switch to the _Debug_ configuration.
5. Start debugging (F5).

### Continuous Integration

[![Build Status](https://dev.azure.com/DSInternals/DSInternals%20CI/_apis/build/status/MichaelGrafnetter.DSInternals?branchName=master&jobName=Release)](https://dev.azure.com/DSInternals/DSInternals%20CI/_build/latest?definitionId=2?branchName=master)
![Test Results](https://img.shields.io/azure-devops/tests/DSInternals/DSInternals%20CI/2.svg?label=Test%20Results&logo=azuredevops)

Automatic build configuration is in the [azure-pipelines.yml](../azure-pipelines.yml) file.
