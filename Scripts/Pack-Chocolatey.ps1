<#
.SYNOPSIS
Generates a Chocolatey package. Requires Chocolatey to be installed.
#>
#Requires -Version 5

$repoRoot = Join-Path $PSScriptRoot '..\' -Resolve
$nuspecPath = Join-Path $repoRoot 'Src\DSInternals.PowerShell\Chocolatey\*.nuspec' -Resolve
$outputDir = Join-Path  $repoRoot 'Build\packages\Chocolatey'
$moduleDir = Join-Path $repoRoot 'Build\bin\Release\DSInternals'

# Generate file catalog
$catalogPath = Join-Path $moduleDir '..\DSInternals.cat'
$catalogIsValid = (Test-Path -Path $catalogPath) -and
                  (Test-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir) -eq [System.Management.Automation.CatalogValidationStatus]::Valid
if(-not $catalogIsValid)
{
    New-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir -CatalogVersion 1.0 -ErrorAction Stop
} 

# Create target folder
New-Item -Path $outputDir -ItemType Directory -Force

# Pack using Chocolatey
choco pack $nuspecPath --outputdirectory $outputDir --timeout 60 --confirm --verbose