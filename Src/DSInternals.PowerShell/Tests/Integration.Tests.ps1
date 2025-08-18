<#
.SYNOPSIS
    This script contains Pester tests for validating if the DSInternals PowerShell module is properly assembled.
#>
#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

param(
    [Parameter(Mandatory = $false)]
    [ValidateSet('Debug', 'Release')]
    [ValidateNotNullOrEmpty()]
    [string] $Configuration = 'Release'
)

Describe 'DSInternals PowerShell Module' {
    BeforeAll {
        # Locate the module manifest file DSInternals.psd1 in the module root directory.
        [string] $modulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\DSInternals.PowerShell\$Configuration\DSInternals"
        [string] $moduleManifestPath = Join-Path -Path $modulePath -ChildPath 'DSInternals.psd1'
    }

    BeforeDiscovery {
        [string] $modulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\DSInternals.PowerShell\$Configuration\DSInternals"
    }

    Context 'Manifest' {
        BeforeDiscovery {
            # List all aliases defined in the module script file.
            [string] $bootstrapPath = Join-Path -Path $modulePath -ChildPath 'DSInternals.Bootstrap.psm1'
            [hashtable[]] $moduleAliases =
                Select-String -Path $bootstrapPath -Pattern 'New-Alias -Name ([a-zA-Z\-]+) ' |
                ForEach-Object { @{ AliasName = $PSItem.Matches.Groups[1].Value } }
            
            # Get the list of files bundled with the module.
            [hashtable[]] $bundledFiles =
                Get-ChildItem -Path $modulePath -Recurse -File -Exclude *.pdb,*.psd1,msvcp*.dll,msvcr*.dll,vcruntime*.dll,Ijwhost.dll,ucrtbased.dll,DSInternals.cat |
                ForEach-Object { @{ FileName = $PSItem.Name } }
        }

        It 'exists' {
            $moduleManifestPath | Should -Exist
        }

        It 'is valid' {
            { Test-ModuleManifest -Path $moduleManifestPath -ErrorAction Stop } | Should -Not -Throw
        }

        It 'has the same version as the binary module' -TestCases @{ AssemblyRelativePath = 'net48\DSInternals.PowerShell.dll' },@{ AssemblyRelativePath = 'net8.0-windows\DSInternals.PowerShell.dll' } -Test {
            param([string] $AssemblyPath)

            # Load the assembly
            [string] $assemblyPath = Join-Path -Path $modulePath -ChildPath $AssemblyRelativePath
            [System.Reflection.AssemblyName] $assembly = [System.Reflection.AssemblyName]::GetAssemblyName($assemblyPath)

            # Load the module manifest
            [hashtable] $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath
            [version] $moduleVersion = [version]::Parse($manifest.ModuleVersion)

            # Parser uses -1 instead of 0 for unused numbers, so we need to fix that
            if($moduleVersion.Build -eq -1) {
                $moduleVersion = [version]::new($moduleVersion.Major, $moduleVersion.Minor, 0, 0)
            } else {
                $moduleVersion = [version]::new($moduleVersion.Major, $moduleVersion.Minor, $moduleVersion.Build, 0)
            }

            # Compare their versions
            $moduleVersion | Should -BeExactly $assembly.Version
        }

        It 'references the <FileName> file.' -TestCases $bundledFiles -Test {
            param([string] $FileName)
            
            $moduleManifestPath | Should -FileContentMatch $FileName
        }

        It 'exports alias <AliasName>.' -TestCases $moduleAliases -Test {
            param([string] $AliasName)

            $moduleManifestPath | Should -FileContentMatch "'$AliasName'"
        }

    }

    Context 'File Structure' {
        
        It 'contains MAML help' {
            Join-Path -Path $modulePath -ChildPath 'en-US\DSInternals.PowerShell.dll-Help.xml' | Should -Exist
        }

        It 'contains an About topic' {
            [string] $aboutPagePath = Join-Path -Path $modulePath -ChildPath 'en-US\about_DSInternals.help.txt'

            $aboutPagePath | Should -Exist
            $aboutPagePath | Should -FileContentMatch 'about_DSInternals'
        }

        It 'contains the License.txt file with up-to-date copyright' {
            [string] $licenseFile = Join-Path -Path $modulePath -ChildPath 'License.txt'
            $licenseFile | Should -Exist
            $licenseFile | Should -FileContentMatch ('Copyright \(c\) 2015-{0}' -f (Get-Date).Year)
        }

        It 'contains Visual C++ Runtime (<Runtime>)' -TestCases @{ Runtime = 'net48\x86' },@{ Runtime = 'net48\amd64' },@{ Runtime = 'net48\arm64' },@{ Runtime = 'net8.0-windows\x86' },@{ Runtime = 'net8.0-windows\amd64' },@{ Runtime = 'net8.0-windows\arm64' } -Test {
            param([string] $Runtime)

            # Regardless of the runtime version, we expect 2-5 additional DLLs to be present in the x86/amd64/arm64 directory
            [string] $runtimeSpecificPath = Join-Path -Path $modulePath -ChildPath $Runtime

            Get-ChildItem -Path $runtimeSpecificPath -Recurse -Include msvc*,vcruntime*,Ijwhost.dll,ucrtbased.dll -ErrorAction Stop |
                Measure-Object |
                Select-Object -ExpandProperty Count |
                Should -BeIn @(2, 3, 4, 5)
        }

        It 'does not contain unit tests' {
            Get-ChildItem -Path $modulePath -Recurse -Filter *.Test.* | Should -BeNull
        }

        It 'does not contain .NET XML documentation' {
            Get-ChildItem -Path $modulePath -Recurse -Filter *.xml -Exclude *.dll-Help.xml | Should -BeNull
        }
    }

    Context 'Views' {
        BeforeDiscovery {
            # Get all .NET types referenced by Views, with the exception of virtual types (containing #)
            [hashtable[]] $typeNames = Get-ChildItem -Filter *.format.ps1xml -Path $modulePath -Recurse -File |
                        Select-Xml -XPath '//TypeName/text()' |
                        ForEach-Object { $PSItem.Node.Value } |
                        Sort-Object -Unique |
                        Where-Object { $PSItem -notlike '*#*' } |
                        ForEach-Object { @{ TypeName = $PSItem } }
        }
        

        It 'referenced type <TypeName> exists' -TestCases $typeNames -Test {
            param([string] $TypeName)

            ($TypeName -as [Type]) | Should -Not -BeNull
        }
    }

    Context 'Types' {
        BeforeDiscovery {
            # Get all .NET types referenced by Types, while removing the Deserialized PowerShell prefix
            [hashtable[]] $typeNames = Get-ChildItem -Filter *.types.ps1xml -Path $modulePath -Recurse -File |
                            Select-Xml -XPath '//Type/Name/text()' |
                            ForEach-Object { $PSItem.Node.Value -replace '^Deserialized\.','' } |
                            Sort-Object -Unique |
                            ForEach-Object { @{ TypeName = $PSItem } }
        }

        It 'referenced type <TypeName> exists' -TestCases $typeNames -Test {
            param($TypeName)
            ($TypeName -as [Type]) | Should -Not -BeNull
        }
    }

    Context 'Assemblies' {
        BeforeDiscovery {
            [hashtable[]] $assemblies = Get-ChildItem $ModulePath -Recurse -Filter *.dll | 
                Where-Object Name -NotLike msvcp* |
                Where-Object Name -NotLike msvcr* |
                Where-Object Name -NotLike vcruntime* |
                ForEach-Object { @{ Assembly = $PSItem; Name = $PSItem.Name } }

            [hashtable[]] $ownedAssemblies = $assemblies | Where-Object { $PSItem.Assembly.Name -like 'DSInternals.*.dll' }
        }
            

        It '<Name> has up-to-date copyright information' -TestCases $ownedAssemblies -Test {
            param([System.IO.FileInfo] $Assembly)

            [string] $expectedCopyrightInfo = '* 2015-{0}*' -f (Get-Date).Year

            $Assembly.VersionInfo.LegalCopyright | Should -BeLike $expectedCopyrightInfo
        }
    }
}
