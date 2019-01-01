param(
    [Parameter(Mandatory = $true)]
    [string]
    $ModulePath
)

Describe 'DSInternals PowerShell Module' {
    
    Context 'Module Manifest' {

        $moduleManifestPath = Join-Path $ModulePath DSInternals.psd1

        It 'should exist' {
            $moduleManifestPath | Should -Exist
        }

        It 'should be valid' {
            Test-ModuleManifest -Path $moduleManifestPath -ErrorAction Stop
        }

        It 'should have the same version as the binary module' {
            # Load the assembly
            $assemblyPath = Join-Path $ModulePath 'DSInternals.PowerShell.dll'
            $assembly = [System.Reflection.AssemblyName]::GetAssemblyName($assemblyPath)

            # Load the module manifest
            $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath
            $adjustedModuleVersion = [version]::Parse($manifest.ModuleVersion + '.0.0')

            # Compare their versions
            $adjustedModuleVersion | Should -BeExactly $assembly.Version
        }
    }

    Context 'Module File Structure' {
        
        It 'should contain MAML help' {
            Join-Path $ModulePath en-US\DSInternals.PowerShell.dll-Help.xml | Should -Exist
        }

        It 'should contain About topic' {
            Join-Path $ModulePath en-US\about_DSInternals.help.txt | Should -Exist
        }
        
        It 'should contain Visual C++ Runtime (<Platform>)' -TestCases @{ Platform = 'x86' },@{ Platform = 'amd64' } -Test {
            param([string] $Platform)

            # Regardless of the runtime version, we expect 2 additional DLLs to be present in the x86/amd64 directory
            $platformSpecificPath = Join-Path $ModulePath $Platform
            Get-ChildItem -Path $platformSpecificPath -Recurse -Include msvc*,vcruntime* |
                Measure-Object |
                Select-Object -ExpandProperty Count |
                Should -BeExactly 2
        }

        It 'should not contain debug symbols' {
            # Only the DSInternals.DataStore.pdb should be present to simplify troubleshooting.

            Get-ChildItem -Path $ModulePath -Recurse -Filter *.pdb |
                Measure-Object |
                Select-Object -ExpandProperty Count |
                Should -BeExactly 1
        }

        It 'should not contain unit tests' {
            Get-ChildItem -Path $ModulePath -Recurse -Filter *.Test.* | Should -BeNull
        }

        It 'should not contain .NET XML documentation' {
            Get-ChildItem -Path $ModulePath -Recurse -Filter *.xml -Exclude *.dll-Help.xml | Should -BeNull
        }
    }

    Context 'Assemblies' {

        It 'should have strong names' {
            Get-ChildItem $ModulePath -Recurse -Filter *.dll | foreach {
                try
                {
                    $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($PSItem.FullName)
                    $isSigned = $assemblyName.Flags.HasFlag([System.Reflection.AssemblyNameFlags]::PublicKey)
                    if(-not $isSigned)
                    {
                        throw "The assembly $PSItem does not have a strong name."
                    }
                }
                catch [System.BadImageFormatException]
                {
                    # The DLL file is not a .NET assembly. We can ignore this error, because it is probably the Visual C++ Runtime. 
                }
            }
        }

        It 'should have file details' {
             Get-ChildItem $ModulePath -Recurse -Filter DSInternals.*.dll |
                where { $PSItem.VersionInfo.ProductName -eq $null } |
                Should -BeNull       
        }

        It 'should have up-to-date copyright information' {
            $expectedInfo = '*© 2015-{0}*' -f (Get-Date).Year
             Get-ChildItem $ModulePath -Recurse -Filter DSInternals.*.dll |
                where { $PSItem.VersionInfo.LegalCopyright -notlike $expectedInfo } |
                Should -BeNull 
        }
    }

    Context 'Hash Calculation Cmdlets' {
        It 'should be working' {
        }
    }

    Context 'Database Manipulation Cmdlets' {
        It 'should be working' {
        }
    }

    Context 'Replication Cmdlets' {
        It 'should be working' {
        }
    }

    Context 'SAM Cmdlets' {
        It 'should be working' {
        }
    }

    Context 'LSA Cmdlets' {
        It 'should be working' {
        }
    }

    Context 'Misc Cmdlets' {
        It 'should be working' {
        }
    }
}