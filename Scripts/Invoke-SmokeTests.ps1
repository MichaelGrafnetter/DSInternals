<#
.SYNOPSIS
The purpose of this script is to check the Release build for any obvious flaws without running all unit tests.
#>

#Requires -Version 5
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0.0' }

[string] $root = Split-Path -Path $PSScriptRoot -Parent
[string] $dsInternalsModulePath = Join-Path -Path $root -ChildPath 'Build\bin\DSInternals.PowerShell\Release\DSInternals'

[string] $testsPath = Join-Path -Path $root -ChildPath 'Src\DSInternals.PowerShell\Tests\'
[string] $resultsPath = Join-Path -Path $root -ChildPath 'TestResults'
[string] $docPath = Join-Path -Path $root -ChildPath 'Documentation'
[string] $nuspecPath = Join-Path -Path $root -ChildPath 'Src\DSInternals.PowerShell\Chocolatey\*.nuspec' -Resolve

# Create output dir if it does not exist
New-Item -Path $resultsPath -ItemType Directory -Force | Out-Null

Import-Module -Name Pester

# Prepare test options
[Pester.ContainerInfo] $testParams = New-PesterContainer -Path "$testsPath\*.Smoke.Tests.ps1" -Data @{
    ModulePath = $dsInternalsModulePath;
    MarkdownDocumentationPath = $docPath;
    ChocolateySpecPath = $nuspecPath
}

[PesterConfiguration] $testConfig = New-PesterConfiguration
$testConfig.Run.Container = $testParams
$testConfig.Output.Verbosity = 'Detailed'
$testConfig.TestResult.Enabled = $true
$testConfig.TestResult.OutputFormat = 'NUnitXml'
$testConfig.TestResult.OutputPath = "$resultsPath\SmokeTests.xml"
$testConfig.Run.Exit = $true
$testConfig.Run.PassThru = $false

# Run tests
Invoke-Pester -Configuration $testConfig
