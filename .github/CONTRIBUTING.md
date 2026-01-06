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

If you want to build the module from source code yourself,
you will need a Windows machine with either the [Microsoft Visual Studio Community 2026](https://visualstudio.microsoft.com/vs/community/)
or the [Build Tools for Visual Studio 2026](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2026)
with these components installed:

| Component ID                                             | Name                                        |
|----------------------------------------------------------|---------------------------------------------|
| Microsoft.VisualStudio.Workload.ManagedDesktopBuildTools | .NET desktop build tools                    |
| Microsoft.VisualStudio.Workload.VCTools                  | Desktop development with C++                |
| Microsoft.VisualStudio.Component.VC.CLI.Support          | C++/CLI support (Latest MSVC)               |
| Microsoft.VisualStudio.Component.VC.Tools.x86.x64        | MSVC Build Tools for x64/x86 (Latest)       |
| Microsoft.VisualStudio.Component.VC.Tools.ARM64          | MSVC Build Tools for ARM64/ARM64EC (Latest) |
| Microsoft.VisualStudio.Component.Windows11SDK.26100      | Windows 11 SDK (10.0.26100.6901)            |
| Microsoft.Net.Component.4.8.1.SDK                        | .NET Framework 4.8.1 SDK                    |
| Microsoft.Net.Component.4.8.TargetingPack                | .NET Framework 4.8 targeting pack           |
| Microsoft.NetCore.Component.SDK                          | .NET SDK                                    |
| Microsoft.NetCore.Component.Runtime.10.0                 | .NET 10.0 Runtime (Long Term Support)       |
| Microsoft.NetCore.Component.Runtime.9.0                  | .NET 9.0 Runtime                            |
| Microsoft.NetCore.Component.Runtime.8.0                  | .NET 8.0 Runtime (Long Term Support)        |

[Git for Windows](https://git-scm.com/install/windows) is also required to clone the repository.

The following PowerShell script can be used to install the required components using [winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/):

```powershell
# Install Visual Studio Build Tools with the required components
[string] $components = @(
    'Microsoft.VisualStudio.Workload.ManagedDesktopBuildTools',
    'Microsoft.VisualStudio.Workload.VCTools',
    'Microsoft.VisualStudio.Component.VC.CLI.Support',
    'Microsoft.VisualStudio.Component.VC.Tools.x86.x64',
    'Microsoft.VisualStudio.Component.VC.Tools.ARM64',
    'Microsoft.VisualStudio.Component.Windows11SDK.26100',
    'Microsoft.Net.Component.4.8.1.SDK',
    'Microsoft.Net.Component.4.8.TargetingPack',
    'Microsoft.NetCore.Component.SDK',
    'Microsoft.NetCore.Component.Runtime.10.0',
    'Microsoft.NetCore.Component.Runtime.9.0',
    'Microsoft.NetCore.Component.Runtime.8.0'
) -join ' --add '

winget install --id Microsoft.VisualStudio.BuildTools --exact --override "--wait --passive --productId Microsoft.VisualStudio.Product.BuildTools --channelId VisualStudio.18.Stable --addProductLang En-us --add $components"

# Install Git for Windows
winget install --id Git.Git --exact
```

### PowerShell XML Authoring Support

To make IntelliSense work with `*.ps1xml` files,
the following code needs to be added to the `C:\Program Files\Microsoft Visual Studio\18\Community\Xml\Schemas\catalog.xml` file:

```xml
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Format.xsd" enableValidation="true"/>
<Association extension="ps1xml" schema="https://raw.githubusercontent.com/PowerShell/PowerShell/master/src/Schemas/Types.xsd" enableValidation="true"/>
```

### Build and Debug Steps

1. Install the [required software](#development-environment).
2. Checkout or download the source code.
3. Open the [Src/DSInternals.slnx](/Src/DSInternals.slnx) file in Visual Studio.
4. Put any cmdlets you wish to debug into the [Src/DSInternals.PowerShell/Debug.ps1](/Src/DSInternals.PowerShell/Debug.ps1) script.
5. Set the [DSInternals.PowerShell](/Src/DSInternals.PowerShell/DSInternals.PowerShell.csproj) project as StartUp Project.
6. Switch to the _Debug_ configuration.
7. Build the `DSInternals.Replication.Interop.*` projects for x86, x64, and ARM64 architectures.
8. Start debugging the PowerShell module (F5).

Alternatively, you can use the following PowerShell script to clone, build, and load the module for debugging:

```powershell
# Ensure that the NuGet.org package source is available
dotnet nuget add source "https://api.nuget.org/v3/index.json" --name "nuget.org"

# Create a directory for the source code
mkdir ~/source/repos
cd ~/source/repos/

# Clone the repository
git clone https://github.com/MichaelGrafnetter/DSInternals.git

# Build the solution
cd ~/source/repos/DSInternals/Src
$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\18\BuildTools\\MSBuild\Current\Bin\amd64\MSBuild.exe"
$configuration = 'Debug'
& $msbuild DSInternals.slnx /t:Restore /p:Configuration=$configuration /p:Platform=x64
& $msbuild DSInternals.ArchitectureSpecific.slnf /t:Build /p:Configuration=$configuration /p:Platform=Win32
& $msbuild DSInternals.ArchitectureSpecific.slnf /t:Build /p:Configuration=$configuration /p:Platform=ARM64
& $msbuild DSInternals.slnx /t:Build /p:Configuration=$configuration /p:Platform=x64

# Load the PowerShell module
~/source/repos/DSInternals/Src/DSInternals.PowerShell/Debug.ps1 -Configuration $configuration
```

### Testing

[![CI Build](https://github.com/MichaelGrafnetter/DSInternals/actions/workflows/autobuild.yml/badge.svg)](https://github.com/MichaelGrafnetter/DSInternals/actions/workflows/autobuild.yml)

To run the .NET unit tests, either use Visual Studio's Test Explorer
or execute the following commands in a PowerShell terminal:

```powershell
cd ~/source/repos/DSInternals/Src

# Run all unit tests
dotnet test --solution DSInternals.TestsOnly.slnf --configuration Debug --no-build

# Run tests for a specific project
dotnet test --project DSInternals.Common.Test --configuration Debug --no-build
```

To run the PowerShell Pester integration / smoke tests, either use Visual Studio's Test Explorer
or execute the following commands in a PowerShell terminal:

```powershell
cd ~/source/repos/DSInternals/Scripts

# Windows PowerShell 5.1
powershell -File ./Invoke-SmokeTests.ps1 -Configuration 'Debug'

# PowerShell Core 7+
pwsh -File ./Invoke-SmokeTests.ps1 -Configuration 'Debug'
```

GitHub Actions are used for Continuous Integration (CI) builds.

## Directory Structure

```plaintext
Src/                                  # Source code
├── DSInternals.Common/               # Shared utilities and cryptographic functions
│   └── DSInternals.Common.csproj
├── DSInternals.Common.Test/          # Unit tests for Common
│   └── DSInternals.Common.Test.csproj
├── DSInternals.DataStore/            # Offline ntds.dit database access (ESE/JET)
│   └── DSInternals.DataStore.csproj
├── DSInternals.DataStore.Test/       # Unit tests for DataStore
│   └── DSInternals.DataStore.Test.csproj
├── DSInternals.Replication/          # Replication (MS-DRSR) protocol implementation
│   └── DSInternals.Replication.csproj
├── DSInternals.Replication.Interop/  # Native C++/CLI interop for MS-DRSR RPC calls
│   ├── Directory.Build.props         # C++/CLI-specific MSBuild properties
│   ├── DSInternals.Replication.Interop.Shared.vcxitems  # C++/CLI items shared between .NET and .NET Framework projects
│   ├── NetCore/
│   │   └── DSInternals.Replication.Interop.NetCore.vcxproj
│   └── NetFramework/
│       └── DSInternals.Replication.Interop.NetFramework.vcxproj
├── DSInternals.Replication.Model/    # Replication data model
│   └── DSInternals.Replication.Model.csproj
├── DSInternals.Replication.Model.Test/ # Unit tests for Replication Model
│   └── DSInternals.Replication.Model.Test.csproj
├── DSInternals.Replication.Test/     # Unit tests for Replication
│   └── DSInternals.Replication.Test.csproj
├── DSInternals.SAM/                  # Security Accounts Manager (MS-SAMR) protocol implementation
│   └── DSInternals.SAM.csproj
├── DSInternals.SAM.Test/             # Unit tests for SAM
│   └── DSInternals.SAM.Test.csproj
├── DSInternals.ADSI/                 # Active Directory Service Interfaces (ADSI) / LDAP client wrapper
│   └── DSInternals.ADSI.csproj
├── DSInternals.PowerShell/           # PowerShell cmdlet implementations
│   ├── DSInternals.PowerShell.csproj
│   └── DSInternals.psd1              # PowerShell module manifest
├── Directory.Build.props             # Common MSBuild properties
├── Directory.Packages.props          # Central Package Management
├── global.json                       # .NET SDK configuration
├── DSInternals.slnx                  # Full solution (requires Visual Studio for C++/CLI projects)
├── DSInternals.DotNetSdk.slnf        # C# projects only (works with dotnet CLI)
├── DSInternals.TestsOnly.slnf        # Test projects only
└── DSInternals.SkipTests.slnf        # Non-test projects only

Documentation/                        # Project documentation
├── CHANGELOG.md                      # Version history and release notes
└── PowerShell/                       # PowerShell module documentation

Keys/                                 # Strong name signing keys
├── DSInternals.Public.snk            # Public key for delay signing
└── README.md                         # Instructions for key management

Scripts/                              # Build and test automation scripts
├── Get-TestData.ps1                  # Downloads test data files
├── Invoke-SmokeTests.ps1             # Runs PowerShell Pester smoke tests
├── Update-Licenses.ps1               # Updates third-party license information
└── Update-PSHelp.ps1                 # Regenerates PowerShell MAML help files
```
