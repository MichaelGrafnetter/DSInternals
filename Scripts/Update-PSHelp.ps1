#Requires -Version 5 -Module platyPS
<#
.SYNOPSIS
Refreshes MD documentation files and builds MAML files.

.AUTHOR
Przemysław Kłys (@PrzemyslawKlys)
#>

# Set directory paths
$rootDir = Split-Path $PSScriptRoot -Parent
$locale = 'en-US'
$dsInternalsModulePath = Join-Path $rootDir 'Build\bin\Release\DSInternals'
$dsInternalsModuleManifestPath = Join-Path $dsInternalsModulePath 'DSInternals.psd1'
$mdHelpPath = Join-Path $rootDir 'Documentation\PowerShell'
$xmlHelpPath = Join-Path $rootDir "Src\DSInternals.PowerShell\$locale"

# Import dependencies
Import-Module -Name platyPS
Import-Module -Name $dsInternalsModuleManifestPath

<#
Note: This code has been used to create the initial version of the help files:
New-MarkdownHelp -Module DSInternals -AlphabeticParamsOrder -Locale $locale -WithModulePage -HelpVersion 1.0 -OutputFolder $mdHelpPath
New-MarkdownAboutHelp -AboutName DSInternals -OutputFolder $mdHelpPath
#>

# Update MD files
Update-MarkdownHelpModule -Path $mdHelpPath -RefreshModulePage -AlphabeticParamsOrder -UpdateInputOutput

# Copy the DSInternals.md to readme.md so that it is displayed at GitHub
$moduleModuleMDPath = Join-Path $mdHelpPath 'DSInternals.md'
$readmeMDPath = Join-Path $mdHelpPath 'Readme.md'
Copy-Item -Path $moduleModuleMDPath -Destination $readmeMDPath -Force

# Generate the MAML file
New-ExternalHelp -Path $mdHelpPath -OutputPath $xmlHelpPath -Force