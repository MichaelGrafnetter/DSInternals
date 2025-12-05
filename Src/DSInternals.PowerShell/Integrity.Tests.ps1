<#
.SYNOPSIS
    This script contains Pester tests for the integrity of the DSInternals PowerShell module.

.DESCRIPTION
    The script uses the Pester module to verify the integrity of the module's catalog file, manifest,
    scripts, and assemblies. It checks if all required files are present and digitally signed.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

if ($env:OS -ne 'Windows_NT') {
    # The Get-AuthenticodeSignature cmdlet used to verify digital signatures is only available on Windows.
    Write-Error -Message 'The module integrity tests are only supported on Windows.' `
                -Category ([System.Management.Automation.ErrorCategory]::NotImplemented) `
                -ErrorAction Stop
}

Describe 'DSInternals PowerShell Module Integrity' {
    Context 'Catalog file' {
        BeforeAll {
            # Locate the catalog file DSInternals.cat in the module root directory.
            [string] $catalogFilePath = Join-Path -Path $PSScriptRoot -ChildPath 'DSInternals.cat'
        }

        It 'should exist' {
            # Check if the catalog file DSInternals.cat is present in the module root directory.
            $catalogFilePath | Should -Exist
        }

        It 'should be valid' {
            # Check the directory structure against the catalog file.
            # Skip the PowerShell Gallery metadata file (PSGetModuleInfo.xml), as it is dynamically generated and cannot be verified.
            [string] $powerShellGetFile = Join-Path -Path $PSScriptRoot -ChildPath 'PSGetModuleInfo.xml'
            Test-FileCatalog -CatalogFilePath $catalogFilePath -Path $PSScriptRoot -FilesToSkip $powerShellGetFile -ErrorAction Stop | Should -Be 'Valid'
        }

        It 'should be digitally signed' {
            # Check that the catalog file DSInternals.cat is digitally signed.
            Get-AuthenticodeSignature -FilePath $catalogFilePath -ErrorAction Stop |
                Select-Object -ExpandProperty Status |
                Should -Be 'Valid'
        }
    }

    Context 'Module manifest' {
        BeforeAll {
            # LOcate the module manifest file DSInternals.psd1 in the module root directory.
            [string] $moduleManifestPath = Join-Path -Path $PSScriptRoot -ChildPath 'DSInternals.psd1' -ErrorAction Stop
        }

        It 'should exist' {
            # Check if the module manifest file DSInternals.psd1 is present in the module root directory.
            $moduleManifestPath | Should -Exist
        }

        It 'should be valid' {
            # Validate the module manifest file DSInternals.psd1. All files referenced in the manifest should exist.
            Test-ModuleManifest -Path $moduleManifestPath -ErrorAction Stop | Should -BeTrue
        }

        It 'should be digitally signed' {
            # Check that the module manifest file DSInternals.psd1 is digitally signed.
            Get-AuthenticodeSignature -FilePath $moduleManifestPath -ErrorAction Stop |
                Select-Object -ExpandProperty Status |
                Should -Be 'Valid'
        }
    }

    Context 'Scripts' {
        BeforeDiscovery {
            # Fetch all PowerShell script files in the module directory (*.ps1, *.psd1, *.psm1, *.ps1xml).
            [hashtable[]] $scriptFiles = Get-ChildItem -Path $PSScriptRoot -File -Recurse -Include '*.ps1','*.psd1','*.psm1','*.ps1xml' -ErrorAction Stop |
                ForEach-Object { @{ FileName = $PSItem.Name; FilePath = $PSItem.FullName } }
        }

        It 'File "<FileName>" should be digitally signed' -TestCases $scriptFiles {
            param([string] $FileName, [string] $FilePath)

            # Verify that each PowerShell script file is digitally signed.
            Get-AuthenticodeSignature -FilePath $FilePath -ErrorAction Stop |
                Select-Object -ExpandProperty Status |
                Should -Be 'Valid'
        }
    }

    Context 'Assemblies' {
        BeforeDiscovery {
             # Fetch all DLL files in the module directory and its subdirectories.
            [hashtable[]] $assemblies = Get-ChildItem -Path $PSScriptRoot -File -Recurse -Include '*.dll' -ErrorAction Stop |
                ForEach-Object { @{ FileName = $PSItem.Name; FilePath = $PSItem.FullName } }
        }

        It 'Assembly "<FileName>" should have a strong name' -TestCases $assemblies {
            param([string] $FileName, [string] $FilePath)
            
            # Check if each assembly has a strong name by verifying its public key.
            try {
                [System.Reflection.AssemblyName] $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($FilePath)
                $assemblyName.Flags | Should -Contain 'PublicKey'
            }
            catch [System.BadImageFormatException] {
                # This DLL file is not a .NET assembly, so the test is not applicable.
                # Such files include the Microsoft C++ runtime libraries.
                Set-ItResult -Skipped -Because 'this file is not a .NET assembly.'
            }
        }

        It 'Assembly "<FileName>" should have a valid digital signature' -TestCases $assemblies {
            param([string] $FileName, [string] $FilePath)

            # These 3rd-party files are not signed, so we will skip them.
            [string[]] $unsignedFiles = @()

            if ($FileName -in $unsignedFiles) {
                Set-ItResult -Skipped -Because 'this file is not signed by its vendor.'
                return
            }

            # Verify that each assembly is digitally signed using a certificate.
            Get-AuthenticodeSignature -FilePath $FilePath -ErrorAction Stop |
                Select-Object -ExpandProperty Status |
                Should -Be 'Valid'
        }
    }
}
