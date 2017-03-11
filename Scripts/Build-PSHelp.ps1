
if((Get-Module -Name platyPS -ListAvailable) -eq $null)
{
    # We need the platyPS module, so install it if it is missing.
    Install-PackageProvider -Name NuGet -Force -ForceBootstrap -Scope CurrentUser | Out-Null
    Install-Module -Name platyPS -Scope CurrentUser -Force -Confirm:$false

}

# Load the platyPS module
Import-Module platyPS -Force -ErrorAction Stop


$rootDir = Split-Path $PSScriptRoot -Parent
$locale = 'en-US'
$dsInternalsModulePath = Join-Path $rootDir 'Build\bin\Release\DSInternals'
$dsInternalsModuleManifestPath = Join-Path $dsInternalsModulePath "DSInternals.psd1"
$mdHelpPath = Join-Path $rootDir 'Doc\PowerShell'
$xmlHelpPath = Join-Path $dsInternalsModulePath $locale

Import-Module -Name $dsInternalsModuleManifestPath

<#
Note: This code has been used to create the initial version of the help files:
New-MarkdownHelp -Module DSInternals -AlphabeticParamsOrder -Locale $locale -WithModulePage -HelpVersion 1.0 -OutputFolder $mdHelpPath
New-MarkdownAboutHelp -AboutName DSInternals -OutputFolder $mdHelpPath
#>

Update-MarkdownHelpModule -Path $mdHelpPath -RefreshModulePage -AlphabeticParamsOrder

New-ExternalHelp -Path $mdHelpPath -OutputPath $xmlHelpPath -Force