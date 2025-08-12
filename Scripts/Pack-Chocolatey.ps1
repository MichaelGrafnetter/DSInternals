<#
.SYNOPSIS
Generates a Chocolatey package. Requires Chocolatey to be installed.
#>
#Requires -Version 5

$repoRoot = Split-Path -Path $PSScriptRoot -Parent
$nuspecPath = Join-Path -Path $repoRoot -ChildPath 'Src\DSInternals.PowerShell\Chocolatey\*.nuspec' -Resolve
$outputDir = Join-Path -Path $repoRoot -ChildPath 'Build\packages\Chocolatey'
$moduleDir = Join-Path -Path $repoRoot -ChildPath 'Build\bin\DSInternals.PowerShell\Release\DSInternals'

# Generate file catalog
$catalogPath = Join-Path -Path $moduleDir -ChildPath '..\DSInternals.cat'
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