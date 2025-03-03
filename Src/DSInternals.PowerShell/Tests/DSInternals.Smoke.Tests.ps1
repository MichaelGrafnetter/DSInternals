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

        It 'contains the License.txt file with up-to-date copyright' {
            $licenseFile = Join-Path $ModulePath 'License.txt'
            $licenseFile | Should -Exist
            $licenseFile |  Should -FileContentMatch ('Copyright \(c\) 2015-{0}' -f (Get-Date).Year)
        }

        It 'contains Visual C++ Runtime (<Platform>)' -TestCases @{ Platform = 'x86' },@{ Platform = 'amd64' },@{ Platform = 'arm64' } -Test {
            param([string] $Platform)

            # Regardless of the runtime version, we expect 2-3 additional DLLs to be present in the x86/amd64/arm64 directory
            $platformSpecificPath = Join-Path $ModulePath $Platform
            Get-ChildItem -Path $platformSpecificPath -Recurse -Include msvc*,vcruntime* |
                Measure-Object |
                Select-Object -ExpandProperty Count |
                Should -BeIn @(2,3,4)
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
        
        # Get all .NET types referenced by Views, with the exception of virtual types (containing #)
        $typeNames = Get-ChildItem -Filter *.format.ps1xml -Path $ModulePath -Recurse -File |
                        Select-Xml -XPath '//TypeName/text()' |
                        ForEach-Object { $PSItem.Node.Value } |
                        Sort-Object -Unique |
                        Where-Object { $PSItem -notlike '*#*' } |
                        ForEach-Object { @{ TypeName = $PSItem } }

        # Import the DSInternals PowerShell Module
        Import-Module $ModulePath

        It 'referenced type <TypeName> exists' -TestCases $typeNames -Test {
            param($TypeName)
            ($TypeName -as [Type]) | Should -Not -BeNull
        }
    }

    Context 'Types' {
        
        # Get all .NET types referenced by Types, while removing the Deserialized PowerShell prefix
        $typeNames = Get-ChildItem -Filter *.types.ps1xml -Path $ModulePath -Recurse -File |
                        Select-Xml -XPath '//Type/Name/text()' |
                        ForEach-Object { $PSItem.Node.Value -replace '^Deserialized\.','' } |
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
                $chocolateySpec.package.metadata.releaseNotes.Replace("`r`n","`n").Replace('`','').Trim() | Should Be $manifest.PrivateData.PSData.ReleaseNotes.Replace("`r`n","`n").Replace('- ','* ').Trim()
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
                Should -HaveCount 4
        }
        
        It 'Get-ADKeyCredential parses FIDO2 keys' {
            # The FIDO stuff depends on 3rd party libraries.
            # This test is here to check that they are loading correctly.
            $sampleData = 'B:2696:000200001000015847BA3C54FEDCC4FEA49D957D1FF88D20000261D774442D358413F23535501330F1FCB80FAA161C090932FA706966C812B724C404037B2276657273696F6E223A312C226175746844617461223A224E577965314B435449626C70587836766B59494438625666614A326D48377957474577566664706F44494846414141414D766F726D6479654F554A586A354A4B4D4E49385152674145466848756A78552F747A452F7153646C5830662B49326C415149444A69414249566767684858674A303148324B3568387A473075642F762B4E6757724F504C726F6B3976364E436D3168666F766B695743435376764C507A456F667878324D67442F4F54337A676C5850587A6357464B36554C575863505A54305862364672614731685979317A5A574E795A585431222C22783563223A5B224D4949437644434341615367417749424167494541363377456A414E42676B71686B69473977304241517346414441754D5377774B6759445651514445794E5A64574A705932386756544A4749464A7662335167513045675532567961574673494451314E7A49774D44597A4D544167467730784E4441344D4445774D4441774D444261474138794D4455774D446B774E4441774D4441774D466F776254454C4D416B474131554542684D4355305578456A415142674E5642416F4D43566C31596D6C6A62794242516A45694D434147413155454377775A515856306147567564476C6A59585276636942426448526C6333526864476C76626A456D4D43514741315545417777645758566961574E764946557952694246525342545A584A70595777674E6A45334D7A41344D7A51775754415442676371686B6A4F5051494242676771686B6A4F50514D4242774E434141515A6E6F65634669323333446E75536B4B675268616C73776E2B79676B766472344A53506C746270584B354D786C7A56536757632B3978386D7A477973646242684565634C41596651597170564C57576F7348506F586F327777616A416942676B724267454541594C4543674945465445754D7934324C6A45754E4334784C6A51784E4467794C6A45754E7A4154426773724267454541594C6C4841494241515145417749454D444168426773724267454541594C6C4841454242415153424244364B356E636E6A6C4356342B53536A4453504545594D41774741315564457745422F7751434D4141774451594A4B6F5A496876634E4151454C425141446767454241436A727332662B30646A77346F6E7279702F32324164587867366135587978636F796248446A4B7537324532534E3971444773495A536644793338444446722F6246317332356A6F69753757413674796C4B4130486D45446C6F654A584A69576A76376832417A322F736971576E4A4F4C6963345845316C4143684A53325841716B536B39564647656C6733534C4F696966724265742B6562645177414C2B325146726352374A7258525147396B557937364F3256635367626450524F7348664F59657977617268616C7956535A2B364F4F594B2F512F444C49614F43306A58726E6B7A6D32796D4D5146516C4241497973725965454D3177786946627744742B6C416362634F4574484566355A6C576937356E557A6C576E386253782F35464F3454625A35684945635569475270694942454D525A6C4F496D345A49625A79636E2F764A4F465254567073305630533479677444633D225D2C22646973706C61794E616D65223A22597562696B65792035227D0100040701000501100006000000000000000000000000000000000F00070101000000000000000000000000000800080040230E430000400800099310993462F6D648:CN=Account,DC=contoso,DC=com'
            $key = Get-ADKeyCredential -DNWithBinaryData $sampleData
            $key.FidoKeyMaterial.DisplayName | Should -Be 'YubiKey 5'
        }
    }

    Context 'CLIXML Serialization' {
        # Test that Export-Clixml and Impot-Clixml are working with complex DSInternals data types

        It 'Supplemental credentials are serialized properly' {
            $xmlSerializationFile = 'TestDrive:\SupplementalCredentials.xml'
            $hexCredentials = '000000008809000000000000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000200020002000500005003200200001005000720069006d006100720079003a004e0054004c004d002d005300740072006f006e0067002d004e0054004f005700460064626533343361626337656632643162313166366136643537666437393636383600a00201005000720069006d006100720079003a004b00650072006200650072006f0073002d004e0065007700650072002d004b006500790073003034303030303030303330303030303030333030303030303230303032303030633030303030303030303130303030303030303030303030303030303030303030303130303030303132303030303030323030303030303065303030303030303030303030303030303030303030303030303130303030303131303030303030313030303030303030303031303030303030303030303030303030303030303030303130303030303033303030303030303830303030303031303031303030303030303030303030303030303030303030303130303030303132303030303030323030303030303031383031303030303030303030303030303030303030303030303130303030303131303030303030313030303030303033383031303030303030303030303030303030303030303030303130303030303033303030303030303830303030303034383031303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303431303034343030343130303534303035353030346430303265303034333030346630303464303037353030373330303635303037323030333030303333303037646266326238323237313934353066633533303531343334626438336361616433613436323462326166316538396461633266353665336438316436663937633362653262636439616533336665643862383338383463643032386532353936326539363765363062643637356561376462663262383232373139343530666335333035313433346264383363616164336134363234623261663165383964616332663536653364383164366639376333626532626364396165333366656438623833383834636430323865323539363265393637653630626436373565612000f80001005000720069006d006100720079003a004b00650072006200650072006f00730030333030303030303031303030313030323030303230303034633030303030303030303030303030303030303030303030333030303030303038303030303030366330303030303030303030303030303030303030303030303330303030303030383030303030303734303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303431303034343030343130303534303035353030346430303265303034333030346630303464303037353030373330303635303037323030333030303333303036326539363765363062643637356561363265393637653630626436373565611000d80002005000610063006b0061006700650073003465303035343030346330303464303032643030353330303734303037323030366630303665303036373030326430303465303035343030346630303537303034363030303030303462303036353030373230303632303036353030373230303666303037333030326430303465303036353030373730303635303037323030326430303462303036353030373930303733303030303030346230303635303037323030363230303635303037323030366630303733303030303030353730303434303036393030363730303635303037333030373430301e00c00301005000720069006d006100720079003a00570044006900670065007300740033313030303131643030303030303030303030303030303030303030303030303033666330316465373131623033333135386130363038343037343538393034373431363466323265653166303435326136373739623432393031613835306163343437393533313466343738363639313362366464663230386236633966323033666330316465373131623033333135386130363038343037343538393034373431363466323265653166303435326136373739623432393031613835306136353165383630313533313961626163366262393430326531663734323166323033666330316465373131623033333135386130363038343037343538393034383863363563336635653232336461366662386638663465326163303463663239653363613966353463656566323431623733303737333566366363656430336335396365613336353464656464653466363463356236373662306665636165303035613437313931353136366563333065383862623837323833353733626639653363613966353463656566323431623733303737333566366363656430333732613561323461326234653337616562616334323639336233303237326331303035613437313931353136366563333065383862623837323833353733626631323137336233363932663337346233303432306262646632346234356539633631303263396561656334333932396536373031313365323532316565663939323438383132306339386637333638653539326561663164613233636661303663353465333139303763363631353431646264303864323362326339613530613461666431643934656636363932333163313462393339326537373937653762336266333936633530353338356566653139363261643930303963373231393538636137303462616534623538303863616639313862343930356165616331393863613730346261653462353830386361663931386234393035616561633139663230346164333262663665343466383837643066666666326437626539633236633432373937373061346565343762633465396231623938633134613832313835366539613133666634313337346432656336326365666234396237306664306634653636393032626236653564363530303963313061656338343762643435383432363833383731376238353831323436353136356262373631366132636463383031623063333762623034643964346364393463343863613634613861366334663632366135353334616337666330333161313736623563383366363800'
            $binaryCredentials = [DSInternals.Common.ByteArrayExtensions]::HexToBinary($hexCredentials)
            $credentials = [DSInternals.Common.Data.SupplementalCredentials]::new($binaryCredentials)
            Export-Clixml -InputObject $credentials -Path $xmlSerializationFile
            $importedCredentials = Import-Clixml $xmlSerializationFile
            ConvertTo-Hex -Input $importedCredentials.ToByteArray() | Should -Be $hexCredentials
        }

        It 'NGC keys are serialized properly' {
            Set-ItResult -Inconclusive -Because 'This test is not yet implemented.'
        }

        It 'FIDO keys are serialized properly' {
            Set-ItResult -Inconclusive -Because 'This test is not yet implemented.'
        }

        It 'DPAPI master keys are serialized properly' {
            Set-ItResult -Inconclusive -Because 'This test is not yet implemented.'
        }

        It 'DPAPI bakup keys are serialized properly' {
            Set-ItResult -Inconclusive -Because 'This test is not yet implemented.'
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
