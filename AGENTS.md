# GitHub Copilot Instructions for DSInternals

## Project Overview

DSInternals is a .NET-based project consisting of:

- **DSInternals Framework**: A .NET library exposing internal Active Directory features
- **DSInternals PowerShell Module**: PowerShell cmdlets built on top of the Framework

The project primarily focuses on Active Directory security auditing, offline database manipulation, and password management.

Read the [README.md](.github/README.md), [CHANGELOG.md](Documentation/CHANGELOG.md), and [CONTRIBUTING.md](.github/CONTRIBUTING.md) files
and the [PowerShell module documentation](Documentation/PowerShell/Readme.md) for further context.

## Technology Stack

- **Languages**: C#, C++/CLI, PowerShell
- **Frameworks**: .NET Framework 4.8, .NET 10.0 (Windows-only)
- **Build System**: MSBuild with Central Package Management
- **Testing**: MSTest with Microsoft.Testing.Platform runner
- **IDE**: Visual Studio 2026+ with the [DSInternals.slnx](Src/DSInternals.slnx) solution

## Build Instructions

> **Important:** The `Src` directory must be the working directory for all build and test commands due to the `global.json` SDK configuration.

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
