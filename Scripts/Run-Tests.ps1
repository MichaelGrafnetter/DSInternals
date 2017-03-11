#Requires -Version 3
<#
.SYNOPSIS
Executes all unit tests.
#>

$rootDir = Split-Path $PSScriptRoot -Parent
$time = Get-Date -Format 'yyyy-MM-dd hh-mm-ss'
$resultsDir = Join-Path $rootDir "TestResults\$time"
$buildDir = Join-Path $rootDir 'Build\bin'
$runConfig = Join-Path $rootDir 'Src\Configuration\Local.testsettings'

# We need the Invoke-MSTest module to always invoke the latest mstest.exe.
$modulePath = Join-Path $PSScriptRoot 'Modules\Invoke-MSTest'
Import-Module $modulePath -ErrorAction Stop
$msTest = Get-MsTest

# Create output dir if it does not exist
mkdir $resultsDir -Force | Out-Null

# Execute all Visual Studio Unit Tests
Get-ChildItem -Path $buildDir -Filter *.Test.dll -File -Recurse | foreach {
    $unitTestFile = $_.FullName
    $resultsFile = Join-Path $resultsDir ($_.BaseName + '.trx')
    & $msTest /testcontainer:$unitTestFile /resultsfile:$resultsFile /runconfig:$runConfig
}

# TODO: Execute Pester tests