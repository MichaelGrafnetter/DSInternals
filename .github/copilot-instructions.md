# GitHub Copilot Instructions for DSInternals

## Project Overview

DSInternals is a .NET-based project consisting of:
- **DSInternals Framework**: A .NET library exposing internal Active Directory features
- **DSInternals PowerShell Module**: PowerShell cmdlets built on top of the Framework

The project primarily focuses on Active Directory security auditing, offline database manipulation, and password management.

## Technology Stack

- **Languages**: C#, C++/CLI, PowerShell
- **Frameworks**: .NET Framework 4.8, .NET 10.0 (Windows-only)
- **Build System**: MSBuild with Central Package Management
- **Testing**: MSTest with Microsoft.Testing.Platform runner
- **IDE**: Visual Studio 2022+ with the `DSInternals.slnx` solution

## Project Structure

```
Src/
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
```

## Build Instructions

> **Important:** The `Src` directory must be the working directory for all build and test commands due to the `global.json` SDK configuration.

### Building the PowerShell Module (Full Build)

The PowerShell module depends on C++/CLI projects (`DSInternals.Replication.Interop`) which require Visual Studio to compile. Use the full solution for complete builds:

1. Open `Src/DSInternals.slnx` in Visual Studio 2022+
2. Select the desired configuration (Debug/Release) and platform
3. Build the solution (Ctrl+Shift+B)

### Building C# Projects Only

For C#-only development, you can use the `dotnet` CLI with the filtered solution:

```powershell
cd Src

# Build C# projects only
dotnet build DSInternals.DotNetSdk.slnf

# Build a specific project
dotnet build DSInternals.Common
```

> **Note:** The `dotnet build` command cannot compile C++/CLI projects. Use MSBuild or Visual Studio for full builds including the PowerShell module:

```powershell
cd Src

# Restore NuGet packages
msbuild.exe DSInternals.slnx -target:Restore -property:RestorePackagesConfig=true -property:Configuration=Release -property:Platform=x64

# Build architecture-specific C++/CLI projects (x86)
msbuild.exe DSInternals.ArchitectureSpecific.slnf -target:Build -property:Configuration=Release -property:Platform=Win32 -property:RestorePackages=false

# Build architecture-specific C++/CLI projects (ARM64)
msbuild.exe DSInternals.ArchitectureSpecific.slnf -target:Build -property:Configuration=Release -property:Platform=ARM64 -property:RestorePackages=false

# Build full solution (x64)
msbuild.exe DSInternals.slnx -target:Build -property:Configuration=Release -property:Platform=x64 -property:RestorePackages=false
```

## Test Instructions

### Running Unit Tests

```powershell
cd Src

# Run all unit tests
dotnet test --solution DSInternals.DotNetSdk.slnf

# Run tests for a specific project
dotnet test --project DSInternals.Common.Test
```

### Running PowerShell Smoke Tests

```powershell
cd Scripts

# Run smoke tests in Windows PowerShell
powershell -File ./Invoke-SmokeTests.ps1 -Configuration 'Debug'

# Run smoke tests in PowerShell Core
pwsh -File ./Invoke-SmokeTests.ps1 -Configuration 'Debug'
```


