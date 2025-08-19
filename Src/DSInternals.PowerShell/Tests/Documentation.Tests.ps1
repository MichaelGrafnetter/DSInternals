<#
.SYNOPSIS
    This script contains Pester tests of the DSInternals PowerShell module documentation.
#>
#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Markdown Module Documentation' {
    BeforeAll {
        # Load the module manifest
        [string] $moduleManifestPath = Join-Path -Path $PSScriptRoot -ChildPath '..\DSInternals.psd1'
        [hashtable] $moduleManifest =  Import-PowerShellDataFile -Path $moduleManifestPath -ErrorAction Stop

        # Locate the markdown documentation
        [string] $markdownDocumentationPath = Join-Path -Path $PSScriptRoot -ChildPath '..\..\..\Documentation'

        # Locate the module page in the markdown documentation.
        [string] $modulePagePath = Join-Path -Path $markdownDocumentationPath -ChildPath 'PowerShell\README.md'
        [string] $modulePageContent = Get-Content -Raw -Path $modulePagePath -ErrorAction Stop
    }

    Context 'CHANGELOG' {
        BeforeAll {
            # Locate the CHANGELOG file in the markdown documentation.
            [string] $changeLogPath = Join-Path -Path $markdownDocumentationPath -ChildPath 'CHANGELOG.md'
        }
        
        It 'should exist' {
            # Check if the CHANGELOG file exists.
            $changeLogPath | Should -Exist
        }
        
        It 'should be up-to-date' {
            # Check that the current module version is mentioned in the changelog.
            [string] $moduleVersion = $moduleManifest.ModuleVersion
            $changeLogPath | Should -FileContentMatch "## \[$moduleVersion\]"
        }
    }

    Context 'Module Page Header' {
        It 'should exist' {
            # Check if the module page exists.
            $modulePagePath | Should -Exist
        }

        It 'contains a proper title' {
            # Check that the module page starts with a proper title.
            $modulePageContent | Should -Match '# Directory Services Internals PowerShell Module'
        }

        It 'contains proper module description' {
            # Check that the module page contains the same description of the module as the manifest.
            [string] $moduleDescription = ($moduleManifest.Description -split 'DISCLAIMER:')[0].Trim()
            $moduleDescription | Should -Not -BeNullOrEmpty
            $modulePageContent | Should -Match $moduleDescription
        }
    }

    Context 'Module Page Export Formats' {
        BeforeDiscovery {
            # Parse the list of Views / Export formats from the module.
            [string] $exportViewFormatsPath = Join-Path -Path $PSScriptRoot -ChildPath '..\Views\DSInternals.DSAccount.ExportViews.format.ps1xml'
            [hashtable[]] $views = Select-Xml -Path $exportViewFormatsPath -XPath 'Configuration/ViewDefinitions/View/Name/text()' -ErrorAction Stop |
                    ForEach-Object { @{ ViewName = $PSItem.Node.Value }  }
        }

        It 'contains the <ViewName> view' -TestCases $views {
            param([string] $ViewName)

            # All export formats should be mentioned in the module page.
            $modulePageContent | Should -Match "- \*\*$ViewName\*\*"
        }
    }

    Context 'Cmdlet Pages' {
        BeforeDiscovery {
            # Parse the list of cmdlets from the module manifest.
            [string] $moduleManifestPath = Join-Path -Path $PSScriptRoot -ChildPath '..\DSInternals.psd1'
            [hashtable] $moduleManifest =  Import-PowerShellDataFile -Path $moduleManifestPath -ErrorAction Stop
            [hashtable[]] $manifestCmdlets = $moduleManifest.CmdletsToExport | ForEach-Object { @{
                Cmdlet = $PSItem
            }}
        }

        It 'markdown page for the <Cmdlet> cmdlet exists' -TestCases $manifestCmdlets {
            param([string] $Cmdlet)

            [string] $cmdletPagePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Documentation\PowerShell\$Cmdlet.md"
            $cmdletPagePath | Should -Exist
        }
    }

    Context 'MAML Documentation' {
        BeforeDiscovery {
            # Load the module manifest
            [string] $mamlDocumentationPath = Join-Path -Path $PSScriptRoot -ChildPath '..\en-US\DSInternals.PowerShell.dll-Help.xml'
            [xml] $mamlDocumentation = Get-Content -Path $mamlDocumentationPath -Raw -ErrorAction Stop

            [hashtable[]] $mamlCmdlets = $mamlDocumentation.helpItems.command.details | ForEach-Object { @{
                Cmdlet = $PSItem.name;
                Description = $PSItem.description.para
            }}
        }

        It 'module page contains the <Cmdlet> cmdlet' -TestCases $mamlCmdlets {
            param([string] $Cmdlet, [string] $Description)
            
            [string] $lowerCaseCmdlet = $Cmdlet.ToLower()
            [string] $cmdletLinkFormat = "### \[$Cmdlet\]\($Cmdlet.md#$lowerCaseCmdlet\)"
            $modulePageContent | Should -Match $cmdletLinkFormat
        }

        It 'module page contains proper description of the <Cmdlet> cmdlet' -TestCases $mamlCmdlets {
            param([string] $Cmdlet, [string] $Description)

            if ($Cmdlet -eq 'Add-ADDBSidHistory') {
                Set-ItResult -Skipped -Because 'this cmdlet is deprecated.'
                return
            }

            # Remove markdown links before searching
            [string] $normalizedContent = $modulePageContent -replace '\[([a-zA-Z\-]+)\]\(([([a-zA-Z\-]+)\.md#[a-z\-]+\)','$1'

            $normalizedContent | Should -BeLike "*$Description*"
        }
    }
}
