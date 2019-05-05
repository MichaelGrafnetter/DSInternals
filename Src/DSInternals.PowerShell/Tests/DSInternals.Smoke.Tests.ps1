param(
    [Parameter(Mandatory = $true)]
    [string]
    $ModulePath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]
    $MarkdownDocumentationPath
)

Describe 'DSInternals PowerShell Module' {

    Context 'Manifest' {

        $moduleManifestPath = Join-Path $ModulePath DSInternals.psd1

        It 'exists' {
            $moduleManifestPath | Should -Exist
        }

        It 'is valid' {
            Test-ModuleManifest -Path $moduleManifestPath -ErrorAction Stop
        }

        It 'has the same version as the binary module' {
            # Load the assembly
            $assemblyPath = Join-Path $ModulePath 'DSInternals.PowerShell.dll'
            $assembly = [System.Reflection.AssemblyName]::GetAssemblyName($assemblyPath)

            # Load the module manifest
            $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath
            [version] $moduleVersion = [version]::Parse($manifest.ModuleVersion)
            # Parser uses -1 instead of 0 for unused numbers, so we need to fix that
            if($moduleVersion.Build -eq -1)
            {
                $moduleVersion = [version]::new($moduleVersion.Major, $moduleVersion.Minor, 0, 0)
            }
            else
            {
                $moduleVersion = [version]::new($moduleVersion.Major, $moduleVersion.Minor, $moduleVersion.Build, 0)
            }
            # Compare their versions
            $moduleVersion | Should -BeExactly $assembly.Version
        }
    }

    Context 'File Structure' {
        
        It 'contains MAML help' {
            Join-Path $ModulePath en-US\DSInternals.PowerShell.dll-Help.xml | Should -Exist
        }

        $aboutPagePath = Join-Path $ModulePath 'en-US\about_DSInternals.help.txt'

        It 'contains an About topic' {
            $aboutPagePath | Should -Exist
        }
        
        It 'has a properly named About topic' {
            Select-String -Path $aboutPagePath -Pattern 'about_DSInternals' -CaseSensitive -SimpleMatch -Quiet | Should Be $true
        }

        It 'contains Visual C++ Runtime (<Platform>)' -TestCases @{ Platform = 'x86' },@{ Platform = 'amd64' } -Test {
            param([string] $Platform)

            # Regardless of the runtime version, we expect 2 additional DLLs to be present in the x86/amd64 directory
            $platformSpecificPath = Join-Path $ModulePath $Platform
            Get-ChildItem -Path $platformSpecificPath -Recurse -Include msvc*,vcruntime* |
                Should -HaveCount 2
        }

        It 'does not contain debug symbols' {
            # Only the DSInternals.DataStore.pdb should be present to simplify troubleshooting.

            Get-ChildItem -Path $ModulePath -Recurse -Filter *.pdb |
                Should -HaveCount 1
        }

        It 'does not contain unit tests' {
            Get-ChildItem -Path $ModulePath -Recurse -Filter *.Test.* | Should -BeNull
        }

        It 'does not contain .NET XML documentation' {
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

        It 'have up-to-date copyright information' {
            $expectedInfo = '*© 2015-{0}*' -f (Get-Date).Year
             Get-ChildItem $ModulePath -Recurse -Filter DSInternals.*.dll |
                where { $PSItem.VersionInfo.LegalCopyright -notlike $expectedInfo } |
                Should -BeNull
        }
    }

    if(-not [string]::IsNullOrEmpty($MarkdownDocumentationPath)) {
        Context 'Markdown Documentation' {
            It 'CHANGELOG should be up-to-date' {
                $changeLogPath = Join-Path $MarkdownDocumentationPath 'CHANGELOG.md'
                
                # Get the module version
                $moduleManifestPath = Join-Path $ModulePath DSInternals.psd1
                $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath
                $moduleVersion = $manifest.ModuleVersion
                
                # Check that the verisons match
                Select-String -Path $changeLogPath -Pattern "## [$moduleVersion]" -SimpleMatch -Quiet | Should Be $true
            }

            $modulePagePath = Join-Path $MarkdownDocumentationPath 'PowerShell\Readme.md'

            # Parse Views
            $exportViewFormatsPath = Join-Path $ModulePath 'Views\DSInternals.DSAccount.ExportViews.format.ps1xml'
            $views = Select-Xml -Path $exportViewFormatsPath -XPath 'Configuration/ViewDefinitions/View/Name/text()' |
                        foreach { @{ View = $PSItem.Node.Value }  }

            It 'contains the <View> view' -TestCases $views -Test {
                param([string] $View)

                Select-String -Path $modulePagePath -Pattern "- **$View**" -SimpleMatch -CaseSensitive -Quiet | Should Be $true
            }
                        
            # Parse MAML
            $mamlPath = Join-Path $ModulePath 'en-US\DSInternals.PowerShell.dll-Help.xml'
            $maml = [xml] (Get-Content -Path $mamlPath -ErrorAction Stop)
            $cmdlets = $maml.helpItems.command.details | foreach {
                            @{ Cmdlet = $PSItem.name;
                                Description = $PSItem.description.para
                            } }

            It 'contains the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
                param([string] $Cmdlet, [string] $Description)
                
                Select-String -Path $modulePagePath -Pattern "### [$Cmdlet]($Cmdlet.md)" -SimpleMatch -CaseSensitive -Quiet | Should Be $true
            }

            It 'contains proper description of the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
                param([string] $Cmdlet, [string] $Description)

                # Remove markdown links before searching
                Get-Content -Path $modulePagePath |
                    foreach { $PSItem -replace '\[([a-zA-Z\-]+)\]\(([([a-zA-Z\-]+)\.md\)','$1' } |
                    where { $PSItem -ceq $Description } | Should -HaveCount 1
            }
        }
    }
}

Describe 'Powershell Cmdlets' {

    # Import the DSInternals PowerShell Module
    Import-Module $ModulePath

    Context 'Hash Calculation' {

        It 'ConvertTo-NTHash should be working' {
            $password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force 
            ConvertTo-NTHash -Password $password |
                Should Be '92937945B518814341DE3F726500D4FF'
        }

        It 'ConvertTo-KerberosKey should be working' {
            # Check that 3 types of kerberos keys are generated from a given password.
            $password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
            ConvertTo-KerberosKey -Password $password -Salt 'CONTOSOAdministrator' |
                Should -HaveCount 3
        }
    }

    Context 'Database Manipulation' {

        It 'Get-BootKey is working in online mode' {
            (Get-BootKey -Online).Length | Should -Be 32
        }

        It 'Get-ADDBDomainController can open the initial ntds.dit' {
            # This test only works on Windows Server
            $initialDBPath = "$env:SystemRoot\System32\ntds.dit"

            if (Test-Path $initialDBPath)
            {
                $workingNTDSCopy = Copy-Item $initialDBPath TestDrive:\ -PassThru
                Get-ADDBDomainController -DBPath $workingNTDSCopy | Should -Not -BeNull

            }
            else
            {
                Set-ItResult -Inconclusive -Because 'The initial DB is not present in client SKUs.'
            }
        }

        It 'Get-ADDBDomainController works repeatedly' {
            if (Test-Path TestDrive:\ntds.dit)
            {
                # This actually checks proper Automapper initialization in the powershell.exe process.
                Get-ADDBDomainController -DBPath TestDrive:\ntds.dit | Should -Not -BeNull
            }
            else
            {
                Set-ItResult -Inconclusive -Because 'The initial DB is not present in client SKUs.'
            }
        }
    }

    Context 'Replication' {
        It 'Get-ADReplAccount is trying to establish an RPC connection' {
            # We do not have a test server, so we try to connect to a dummy address.
            # This will just check that the Interop stuff is compiled correctly, which is a good start.
            { Get-ADReplAccount -Server NonExistingServer -All -NamingContext 'DC=doesnotexist,DC=local' } |
                Should -Throw 'The RPC server is unavailable'
        }
    }

    Context 'SAM/LSA' {
        It 'Get-SamPasswordPolicy is returning some info' {
            Get-SamPasswordPolicy -Domain Builtin | Should -Not -BeNull
        }

        It 'Get-LsaPolicyInformation is returning computer info' {
            (Get-LsaPolicyInformation).LocalDomain.Name | Should -Be $env:COMPUTERNAME
        }
    }
}
