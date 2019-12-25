param(
    [Parameter(Mandatory = $true)]
    [string]
    $ModulePath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]
    $MarkdownDocumentationPath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]
    $ChocolateySpecPath
)

Describe 'DSInternals PowerShell Module' {

    $moduleManifestPath = Join-Path $ModulePath DSInternals.psd1

    Context 'Manifest' {

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

        $bundledFiles = Get-ChildItem -Path $ModulePath -Recurse -File -Exclude *.pdb,*.psd1,msvcp*.dll,msvcr*.dll,vcruntime*.dll | foreach { @{ FileName = $PSItem.Name } }

        It 'references the <FileName> file.' -TestCases $bundledFiles -Test {
            param($FileName)
            
            $moduleManifestPath | Should -FileContentMatch $FileName
        }

        $bootstrapPath = Join-Path $ModulePath DSInternals.Bootstrap.psm1
        $moduleAliases = Select-String -Path $bootstrapPath -Pattern 'New-Alias -Name ([a-zA-Z\-]+) ' |
                            foreach { @{ AliasName = $PSItem.Matches.Groups[1].Value } }

        It 'exports alias <AliasName>.' -TestCases $moduleAliases -Test {
            param($AliasName)

            $moduleManifestPath | Should -FileContentMatch "'$AliasName'"
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

        It 'contains the License.txt file' {
            Join-Path $ModulePath 'License.txt' | Should -Exist
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

    Context 'Views' {
        
        # Get all .NET types referenced by Views
        $typeNames = Get-ChildItem -Filter *.format.ps1xml -Path $ModulePath -Recurse -File |
                        Select-Xml -XPath '//TypeName/text()' |
                        ForEach-Object { $PSItem.Node.Value } |
                        Sort-Object -Unique |
                        ForEach-Object { @{ TypeName = $PSItem } }

        # Import the DSInternals PowerShell Module
        Import-Module $ModulePath

        It 'referenced type <TypeName> exists' -TestCases $typeNames -Test {
            param($TypeName)
            ($TypeName -as [Type]) | Should -Not -BeNull
        }
    }

    Context 'Assemblies' {

        $assemblies = Get-ChildItem $ModulePath -Recurse -Filter *.dll | 
            where Name -NotLike msvcp* |
            where Name -NotLike msvcr* |
            where Name -NotLike vcruntime* |
            foreach { @{ Assembly = $PSItem } }

        $ownedAssemblies = $assemblies | where { $PSItem.Assembly.Name -like 'DSInternals.*.dll' }

        It '<Assembly> has a strong name' -TestCases $assemblies -Test {
            param($Assembly)
            $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($Assembly.FullName)
            $assemblyName.Flags.HasFlag([System.Reflection.AssemblyNameFlags]::PublicKey) | Should -Be $true
        }

        It '<Assembly> has file details' -TestCases $ownedAssemblies -Test {
            param($Assembly)
            $Assembly.VersionInfo.ProductName | Should -Not -BeNullOrEmpty
        }

        $expectedCopyrightInfo = '*© 2015-{0}*' -f (Get-Date).Year

        It '<Assembly> has up-to-date copyright information' -TestCases $ownedAssemblies -Test {
            param($Assembly)
            $Assembly.VersionInfo.LegalCopyright | Should -BeLike $expectedCopyrightInfo
        }
    }

    if(-not [string]::IsNullOrEmpty($MarkdownDocumentationPath)) {
        Context 'Markdown Documentation' {
            # Get the module manifest
            $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath

            It 'CHANGELOG should be up-to-date' {
                $changeLogPath = Join-Path $MarkdownDocumentationPath 'CHANGELOG.md'

                # Check that the verisons match
                $moduleVersion = $manifest.ModuleVersion
                Select-String -Path $changeLogPath -Pattern "## [$moduleVersion]" -SimpleMatch -Quiet | Should Be $true
            }

            $modulePagePath = Join-Path $MarkdownDocumentationPath 'PowerShell\Readme.md'
            $modulePage = Get-Content -Path $modulePagePath -ErrorAction Stop

            It 'contains proper module description' {
                $moduleDescription = ($manifest.Description -split 'DISCLAIMER:')[0].Trim()

                $modulePage | where { $PSItem -ceq $moduleDescription } | Should -HaveCount 1
            }

            # Parse Views
            $exportViewFormatsPath = Join-Path $ModulePath 'Views\DSInternals.DSAccount.ExportViews.format.ps1xml'
            $views = Select-Xml -Path $exportViewFormatsPath -XPath 'Configuration/ViewDefinitions/View/Name/text()' |
                        foreach { @{ View = $PSItem.Node.Value }  }

            It 'contains the <View> view' -TestCases $views -Test {
                param([string] $View)

                $modulePage | where { $PSItem.StartsWith("- **$View**") } | Should -HaveCount 1
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
                
                $cmdletLinkFormat = '### [{0}]({0}.md#{1})' -f $Cmdlet,$Cmdlet.ToLower()
                $modulePage | where { $PSItem -ceq $cmdletLinkFormat } | Should -HaveCount 1
            }

            It 'contains proper description of the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
                param([string] $Cmdlet, [string] $Description)

                # Remove markdown links before searching
                $modulePage | foreach { $PSItem -replace '\[([a-zA-Z\-]+)\]\(([([a-zA-Z\-]+)\.md#[a-z\-]+\)','$1' } |
                    where { $PSItem -ceq $Description } | Should -HaveCount 1
            }
        }
    }

    if(-not [string]::IsNullOrEmpty($ChocolateySpecPath)) {
        Context 'Chocolatey Package' {

            # Get the module manifest
            $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath

            # Get the Chocolatey package specification
            $chocolateySpec = [xml] (Get-Content -Path $ChocolateySpecPath)

            It 'does not exceed the maximum Description length' {
                # Package upload would fail otherwise
                $chocolateySpec.package.metadata.description.Length | Should -BeLessOrEqual 4000
            }

            # Now compare the shared values

            It 'has the same version as the module' {
                $chocolateySpec.package.metadata.version | Should Be $manifest.ModuleVersion
            }

            It 'has the same release notes as the module' {
                $chocolateySpec.package.metadata.releaseNotes.Replace("`r`n","`n").Trim() | Should Be $manifest.PrivateData.PSData.ReleaseNotes.Replace("`r`n","`n").Replace('- ','* ').Trim()
            }

            It 'has the same copyright info as the module' {
                $chocolateySpec.package.metadata.copyright | Should Be $manifest.Copyright
            }

            It 'has the same icon as the module' {
                $chocolateySpec.package.metadata.iconUrl | Should Be $manifest.PrivateData.PSData.IconUri
            }

            It 'has the same license as the module' {
                $chocolateySpec.package.metadata.licenseUrl | Should Be $manifest.PrivateData.PSData.LicenseUri
            }

            It 'has the same project URI the module' {
                $chocolateySpec.package.metadata.projectUrl | Should Be $manifest.PrivateData.PSData.ProjectUri
            }

            It 'has the same author as the module' {
                $chocolateySpec.package.metadata.authors | Should Be $manifest.Author
            }
        }
    }
}

Describe 'Powershell Cmdlets' {

    # Import the DSInternals PowerShell Module
    Import-Module $ModulePath

    Context 'Cryptography' {

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
        
        It 'Get-ADKeyCredential parses FIDO2 keys' {
            # The FIDO stuff depends on 3rd party libraries.
            # This test is here to check that they are loading correctly.
            $sampleData = 'B:2696:000200001000015847BA3C54FEDCC4FEA49D957D1FF88D20000261D774442D358413F23535501330F1FCB80FAA161C090932FA706966C812B724C404037B2276657273696F6E223A312C226175746844617461223A224E577965314B435449626C70587836766B59494438625666614A326D48377957474577566664706F44494846414141414D766F726D6479654F554A586A354A4B4D4E49385152674145466848756A78552F747A452F7153646C5830662B49326C415149444A69414249566767684858674A303148324B3568387A473075642F762B4E6757724F504C726F6B3976364E436D3168666F766B695743435376764C507A456F667878324D67442F4F54337A676C5850587A6357464B36554C575863505A54305862364672614731685979317A5A574E795A585431222C22783563223A5B224D4949437644434341615367417749424167494541363377456A414E42676B71686B69473977304241517346414441754D5377774B6759445651514445794E5A64574A705932386756544A4749464A7662335167513045675532567961574673494451314E7A49774D44597A4D544167467730784E4441344D4445774D4441774D444261474138794D4455774D446B774E4441774D4441774D466F776254454C4D416B474131554542684D4355305578456A415142674E5642416F4D43566C31596D6C6A62794242516A45694D434147413155454377775A515856306147567564476C6A59585276636942426448526C6333526864476C76626A456D4D43514741315545417777645758566961574E764946557952694246525342545A584A70595777674E6A45334D7A41344D7A51775754415442676371686B6A4F5051494242676771686B6A4F50514D4242774E434141515A6E6F65634669323333446E75536B4B675268616C73776E2B79676B766472344A53506C746270584B354D786C7A56536757632B3978386D7A477973646242684565634C41596651597170564C57576F7348506F586F327777616A416942676B724267454541594C4543674945465445754D7934324C6A45754E4334784C6A51784E4467794C6A45754E7A4154426773724267454541594C6C4841494241515145417749454D444168426773724267454541594C6C4841454242415153424244364B356E636E6A6C4356342B53536A4453504545594D41774741315564457745422F7751434D4141774451594A4B6F5A496876634E4151454C425141446767454241436A727332662B30646A77346F6E7279702F32324164587867366135587978636F796248446A4B7537324532534E3971444773495A536644793338444446722F6246317332356A6F69753757413674796C4B4130486D45446C6F654A584A69576A76376832417A322F736971576E4A4F4C6963345845316C4143684A53325841716B536B39564647656C6733534C4F696966724265742B6562645177414C2B325146726352374A7258525147396B557937364F3256635367626450524F7348664F59657977617268616C7956535A2B364F4F594B2F512F444C49614F43306A58726E6B7A6D32796D4D5146516C4241497973725965454D3177786946627744742B6C416362634F4574484566355A6C576937356E557A6C576E386253782F35464F3454625A35684945635569475270694942454D525A6C4F496D345A49625A79636E2F764A4F465254567073305630533479677444633D225D2C22646973706C61794E616D65223A22597562696B65792035227D0100040701000501100006000000000000000000000000000000000F00070101000000000000000000000000000800080040230E430000400800099310993462F6D648:CN=Account,DC=contoso,DC=com'
            $key = Get-ADKeyCredential -DNWithBinaryData $sampleData
            $key.FidoKeyMaterial.DisplayName | Should -Be 'YubiKey 5'
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
                Get-ADDBDomainController -DatabasePath $workingNTDSCopy | Should -Not -BeNull

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
                Get-ADDBDomainController -DatabasePath TestDrive:\ntds.dit | Should -Not -BeNull
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

    # Unload the module. The assemblies might stay loaded.
    Remove-Module -Name DSInternals -Force
}
