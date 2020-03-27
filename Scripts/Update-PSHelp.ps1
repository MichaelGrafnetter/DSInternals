<#
.SYNOPSIS
Refreshes MD documentation files and builds MAML files.

.AUTHOR
Przemysław Kłys (@PrzemyslawKlys)
#>
#Requires -Version 5 -Module platyPS

# Set directory paths
$rootDir = Split-Path $PSScriptRoot -Parent
$locale = 'en-US'
$dsInternalsModulePath = Join-Path $rootDir 'Build\bin\Release\DSInternals'
$mdHelpPath = Join-Path $rootDir 'Documentation\PowerShell'
$modulePagePath = Join-Path $mdHelpPath 'Readme.md'
$xmlHelpSrcPath = Join-Path $rootDir "Src\DSInternals.PowerShell\$locale"
$xmlHelpBuildPath = Join-Path $dsInternalsModulePath $locale
$aboutPagePath = Join-Path $xmlHelpSrcPath 'about_DSInternals.help.txt'

# Import dependencies
Import-Module -Name platyPS

# Remove any pre-existing XML help
Remove-Item $xmlHelpBuildPath -Recurse -ErrorAction SilentlyContinue

# Load the freshly compiled module to generate the help for
Import-Module -Name $dsInternalsModulePath

<#
Note: This code has been used to create the initial version of the help files:
New-MarkdownHelp -Module DSInternals -AlphabeticParamsOrder -Locale $locale -WithModulePage -HelpVersion 1.0 -OutputFolder $mdHelpPath
New-MarkdownAboutHelp -AboutName DSInternals -OutputFolder $mdHelpPath
#>

# Update MD files
Update-MarkdownHelpModule -Path $mdHelpPath -ModulePagePath $modulePagePath -RefreshModulePage:$false -AlphabeticParamsOrder -UpdateInputOutput

# Generate the MAML file
New-ExternalHelp -Path $mdHelpPath -OutputPath $xmlHelpSrcPath -Force -ShowProgress

# Capitalize the help topic name
$aboutPage = Get-Content -Path $aboutPagePath
($aboutPage -creplace 'about_dsinternals','about_DSInternals') | Out-File -FilePath $aboutPagePath -Force -Encoding utf8