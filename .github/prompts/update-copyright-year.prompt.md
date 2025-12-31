---
agent: agent
tools: ['edit/editFiles', 'changes']
description: 'Update copyright year references across the project at the beginning of each calendar year.'
---

# Yearly Copyright and License Update

This prompt helps update all copyright year references in the DSInternals project at the beginning of each new calendar year.

## Files to Update

The following files contain copyright year references that need to be updated:

- [LICENSE.md](../../LICENSE.md) - Main project license (MIT License)
- [Src/Directory.Build.props](../../Src/Directory.Build.props) - MSBuild properties for all projects
- [Src/DSInternals.Replication.Interop/version.h](../../Src/DSInternals.Replication.Interop/version.h) - Version header for native interop library
- [Src/DSInternals.PowerShell/DSInternals.psd1](../../Src/DSInternals.PowerShell/DSInternals.psd1) - PowerShell module manifest
- [Src/DSInternals.PowerShell/License.txt](../../Src/DSInternals.PowerShell/License.txt) - License file included in binary distributions
- [Src/DSInternals.PowerShell/Chocolatey/dsinternals-psmodule.nuspec](../../Src/DSInternals.PowerShell/Chocolatey/dsinternals-psmodule.nuspec) - Chocolatey package specification

## Important Notes

- The start year (2015) should **never** change - it represents when the project was first created. Only the end year should be updated to the current year.
- The copyright holder name "Michael Grafnetter" should not be changed.
- Third-party copyright notices should **not** be modified.
- No other content in these files should be altered, only the year values. This applies to whitespaces as well.

