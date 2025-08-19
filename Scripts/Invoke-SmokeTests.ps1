<#
.SYNOPSIS
The purpose of this script is to check the PowerShell module for any obvious flaws.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0.0' }

param(
    [Parameter(Mandatory = $false)]
    [ValidateSet('Release', 'Debug')]
    [string] $Configuration = 'Release'
)

[string] $root = Split-Path -Path $PSScriptRoot -Parent
[string] $dsInternalsModulePath = Join-Path -Path $root -ChildPath "Build\bin\DSInternals.PowerShell\$Configuration\DSInternals"

[string] $testsPath = Join-Path -Path $root -ChildPath 'Src\DSInternals.PowerShell\Tests\'
[string] $resultsPath = Join-Path -Path $root -ChildPath 'TestResults'

# Create output dir if it does not exist
New-Item -Path $resultsPath -ItemType Directory -Force | Out-Null

# Re-import the compiled DSInternals module
Remove-Module -Name DSInternals -ErrorAction SilentlyContinue
Import-Module -Name $dsInternalsModulePath -Force -ErrorAction Stop

# Import the Pester module to make the PesterConfiguration type available.
Import-Module -Name Pester -ErrorAction Stop

# Example: Pester-Smoke-Release-Desktop.xml
[string] $resultsFileName = 'Pester-Smoke-{0}-{1}.xml' -f $Configuration,$PSVersionTable.PSEdition
[string] $resultsFilePath = Join-Path -Path $resultsPath -ChildPath $resultsFileName

# Prepare test options
[PesterConfiguration] $testConfig = New-PesterConfiguration
$testConfig.Run.Path = $testsPath
$testConfig.Run.Container = New-PesterContainer -Path $testsPath -Data @{
    Configuration = $Configuration
}
$testConfig.Output.Verbosity = 'Detailed'
$testConfig.TestResult.Enabled = $true
$testConfig.TestResult.OutputFormat = 'NUnitXml'
$testConfig.TestResult.OutputPath = $resultsFilePath
$testConfig.Run.Exit = $true
$testConfig.Run.PassThru = $false

# Run tests
Invoke-Pester -Configuration $testConfig
