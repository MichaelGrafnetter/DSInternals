<#
.SYNOPSIS
Creates a ZIP file distribution of the DSInternals module.
It is presumed that the module has already been compiled.
#>

#Requires -Version 5

# Set paths
$rootDir = Split-Path $PSScriptRoot -Parent
$moduleDir = Join-Path $rootDir 'Build\bin\Release\DSInternals'
$targetDir = Join-Path $rootDir 'Build\packages\Module'

# Generate file catalog
$catalogPath = Join-Path $moduleDir '..\DSInternals.cat'
$catalogIsValid = (Test-Path -Path $catalogPath) -and
                  (Test-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir) -eq [System.Management.Automation.CatalogValidationStatus]::Valid
if(-not $catalogIsValid)
{
    New-FileCatalog -CatalogFilePath $catalogPath -Path $moduleDir -CatalogVersion 1.0 -ErrorAction Stop | Out-Null
}

# Retrieve module version from the manifest
$manifestPath = Join-Path $moduleDir 'DSInternals.psd1'
$moduleVersion = Test-ModuleManifest -Path $manifestPath -ErrorAction Stop | select -ExpandProperty Version
$archiveName = 'DSInternals_v{0}.zip' -f $moduleVersion

# Create the target ZIP archive
New-Item -Path $targetDir -ItemType Directory -Force | Out-Null
Compress-Archive -Path $moduleDir,$catalogPath -DestinationPath $targetDir\$archiveName -Force -ErrorAction Stop