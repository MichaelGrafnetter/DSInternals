<#
.SYNOPSIS
Generates a Chocolatey package. Requires Chocolatey to be installed.
#>
#Requires -Version 5

$repoRoot = Join-Path $PSScriptRoot '..\'
$nuspecPath = Join-Path $repoRoot 'Src\DSInternals.PowerShell.Chocolatey\DSInternals.nuspec'
$outputDir = Join-Path  $repoRoot 'Build\packages\Chocolatey'
$moduleDir = Join-Path $repoRoot 'Build\bin\Release\DSInternals'

# Generate file catalog
$catalogPath = Join-Path $moduleDir '..\DSInternals.cat'
$catalogIsValid = (Test-Path -Path $catalogPath) -and
                  (Test-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir) -eq [System.Management.Automation.CatalogValidationStatus]::Valid
if(-not $catalogIsValid)
{
    New-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir -CatalogVersion 1.0 -ErrorAction Stop | Out-Null
} 

# Create target folder
mkdir $outputDir -Force | Out-Null

# Pack using Chocolatey
choco pack $nuspecPath --outputdirectory $outputDir