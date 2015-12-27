#Requires -Version 5
<#
.SYNOPSIS
Creates a ZIP file distribution of the DSInternals module.
It is presumed that the moduel has already been compiled.
#>

# Set paths
$rootDir = Split-Path $PSScriptRoot -Parent
$releaseDir = Join-Path $rootDir 'Build\bin\Release'

# Retrieve module version from manifest
$manifestPath = Join-Path $releaseDir 'DSInternals\DSInternals.psd1'
$moduleVersion = Test-ModuleManifest -Path $manifestPath -ErrorAction Stop | select -ExpandProperty Version
$archiveName = 'DSInternals_v{0}.zip' -f $moduleVersion

# Copy documentation files next to the built module
Copy-Item -Path $rootDir\*.txt -Destination $releaseDir -Force

# Create the target ZIP archive
Compress-Archive -Path $releaseDir\* $releaseDir\$archiveName -Force