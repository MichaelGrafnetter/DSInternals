 <#
.SYNOPSIS
    This script contains Pester tests for the Chocolatey NuGet package manifest of the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Chocolatey Package' {
    BeforeAll {
        # Get the manifest paths
        [string] $moduleManifestPath = Join-Path -Path $PSScriptRoot -ChildPath '..\DSInternals.psd1'
        [string] $chocolateySpecPath = Join-Path -Path $PSScriptRoot -ChildPath '..\Chocolatey\dsinternals-psmodule.nuspec'

        # Load the manifests
        [hashtable] $moduleManifest =  Import-PowerShellDataFile -Path $moduleManifestPath -ErrorAction Stop
        [xml] $chocolateySpec = Get-Content -Path $chocolateySpecPath -ErrorAction Stop
    }

    It 'does not exceed the maximum Description length' {
        $chocolateySpec.package.metadata.description.Length | Should -BeLessOrEqual 4000 -Because 'package upload would fail otherwise.'
    }

    It 'has the same release notes as the module' {
        # Remove line breaks and backticks (markdown code sections) from the release notes.
        [string] $normalizedChocolateyReleaseNotes = $chocolateySpec.package.metadata.releaseNotes.Replace("`r`n","`n").Replace('`','').Trim()

        # Remove line breaks and change dashes to asterisks (markdown bullet points) in the module release notes.
        [string] $normalizedModuleReleaseNotes = $moduleManifest.PrivateData.PSData.ReleaseNotes.Replace("`r`n","`n").Replace('- ','* ').Trim()

        # Now compare the release notes
        $normalizedChocolateyReleaseNotes | Should -Be $normalizedModuleReleaseNotes
    }

    It 'has the same copyright info as the module' {
        $chocolateySpec.package.metadata.copyright | Should -Be $moduleManifest.Copyright
    }

    It 'has the same icon as the module' {
        $chocolateySpec.package.metadata.iconUrl | Should -Be $moduleManifest.PrivateData.PSData.IconUri
    }

    It 'has the same license as the module' {
        $chocolateySpec.package.metadata.licenseUrl | Should -Be $moduleManifest.PrivateData.PSData.LicenseUri
    }

    It 'has the same project URI the module' {
        $chocolateySpec.package.metadata.projectUrl | Should -Be $moduleManifest.PrivateData.PSData.ProjectUri
    }

    It 'has the same author as the module' {
        $chocolateySpec.package.metadata.authors | Should -Be $moduleManifest.Author
    }
}
