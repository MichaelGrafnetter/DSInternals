---
agent: agent
tools: ['edit/editFiles', 'search/codebase', 'search/readFile', 'problems', 'changes']
description: 'Prepare the DSInternals project for a new release by updating version numbers, release notes, and changelog.'
---

# Release Preparation Guide

## Copyright Year Updates

If releasing in a new calendar year, first run [update-copyright-year.prompt.md](update-copyright-year.prompt.md).

## Files to Update

### 1. Assembly Version and NuGet Release Notes

C# project files - Update `<Version>` and `<PackageReleaseNotes>`:
- [DSInternals.Common.csproj](../../Src/DSInternals.Common/DSInternals.Common.csproj)
- [DSInternals.DataStore.csproj](../../Src/DSInternals.DataStore/DSInternals.DataStore.csproj)
- [DSInternals.Replication.csproj](../../Src/DSInternals.Replication/DSInternals.Replication.csproj)
- [DSInternals.SAM.csproj](../../Src/DSInternals.SAM/DSInternals.SAM.csproj)

C# project files - Update `<Version>` only (not packable):
- [DSInternals.ADSI.csproj](../../Src/DSInternals.ADSI/DSInternals.ADSI.csproj)
- [DSInternals.Replication.Model.csproj](../../Src/DSInternals.Replication.Model/DSInternals.Replication.Model.csproj)
- [DSInternals.PowerShell.csproj](../../Src/DSInternals.PowerShell/DSInternals.PowerShell.csproj)

C++/CLI interop library:
- [version.h](../../Src/DSInternals.Replication.Interop/version.h) - Update `VERSION_MAJOR`, `VERSION_MINOR`, `VERSION_REVISION`, `VERSION_BUILD`.

### 2. PowerShell Module Manifest

[DSInternals.psd1](../../Src/DSInternals.PowerShell/DSInternals.psd1) - Update `ModuleVersion` and `ReleaseNotes` in the PowerShell module manifest file.

**Note:** `ModuleVersion` must match the assembly version (e.g., `6.3` for assembly `6.3.0.0`).

### 3. Chocolatey Package Specification

[dsinternals-psmodule.nuspec](../../Src/DSInternals.PowerShell/Chocolatey/dsinternals-psmodule.nuspec) - Update `<releaseNotes>` in the Chocolatey package specification.

### 4. Changelog

[CHANGELOG.md](../../Documentation/CHANGELOG.md) - Add new version section following [Keep a Changelog](https://keepachangelog.com/) format. Update comparison links at the bottom.

## Release Notes Consistency

Release notes should be consistent across files:
- **CHANGELOG.md**: Most detailed, follows Keep a Changelog format with sections (Added, Changed, Fixed, etc.)
- **DSInternals.psd1** and **dsinternals-psmodule.nuspec**: Should match each other and contain a relevant subset of the changelog (user-visible changes only)

## Release Process

1. Update all files listed above with the new version and release notes.
2. Build the solution.
3. Verify version consistency across all files by running the following Pester tests:
  - [Integration.Tests.ps1](../../Src/DSInternals.PowerShell/Tests/Integration.Tests.ps1) verifies module/assembly version match
  - [Documentation.Tests.ps1](../../Src/DSInternals.PowerShell/Tests/Documentation.Tests.ps1) verifies changelog contains current version
4. Commit: `Release v{VERSION}`
5. Tag and push: `git tag v{VERSION} && git push origin master -tags`
