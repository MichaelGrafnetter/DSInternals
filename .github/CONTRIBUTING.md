![DSInternals Logo](/.github/DSInternals-Dark.png#gh-light-mode-only)
![DSInternals Logo](/.github/DSInternals-Light.png#gh-dark-mode-only)

# Contributing to the Project

## How can I contribute?

Any contributions to this project are warmly welcome. These are the most important areas:

- [Bug reports](https://github.com/MichaelGrafnetter/DSInternals/issues)
- [Feature requests](https://github.com/MichaelGrafnetter/DSInternals/issues)
- Testing the PowerShell module against different Active Directory configurations
- Documentation
  - PowerShell help
  - XML documentation comments in the source code
- Code
  - Bug fixes
  - New features
  - Test cases
  - Code cleanup

## Building from Source Code

### Development Environment

If you want to build the module from source code yourself, you need to install these programs first:

- [Microsoft Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) 2026 with these features installed:
  - .NET Framework 4.8 targeting pack
  - C++ 2022 Redistributable Update
  - C++/CLI support for v143 build tools (Latest)
  - MSVC v143 - VS 2022 C++ x64/x86 build tools (Latest)
  - MSVC v143 - VS 2022 C++ ARM64/ARM64EC build tools (Latest)
  - Windows 11 SDK (you might have to retarget the `DSInternals.Replication.Interop` projects to the version you have)
  - PowerShell Tools for Visual Studio (optional)
  - Git for Windows (optional)
  - GitHub Extension for Visual Studio (optional)

To make IntelliSense work with `*.ps1xml` files, the following code needs to be added to the `C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Xml\Schemas\catalog.xml` file:

```xml
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Format.xsd" enableValidation="true"/>
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Types.xsd" enableValidation="true"/>
```

### Build and Debug Steps

1. Install the prerequisities.
2. Checkout or download the source codes.
3. Open the [Src\DSInternals.slnx](/Src/DSInternals.slnx) file in Visual Studio.
2. Put any cmdlets you wish to debug into the [Src\DSInternals.PowerShell\Debug.ps1](/Src/DSInternals.PowerShell/Debug.ps1) script.
3. Set the [DSInternals.PowerShell](/Src/DSInternals.PowerShell/DSInternals.PowerShell.csproj) project as StartUp Project.
4. Switch to the _Debug_ configuration.
5. Start debugging (F5).

### Continuous Integration

[![CI Build](https://github.com/MichaelGrafnetter/DSInternals/actions/workflows/autobuild.yml/badge.svg)](https://github.com/MichaelGrafnetter/DSInternals/actions/workflows/autobuild.yml)

GitHub Actions are used for CI builds.
