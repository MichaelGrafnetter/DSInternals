#Requires -Version 5
<#
.SYNOPSIS
Publishes the module into PowerShell Gallery.

.DESCRIPTION
URL: https://www.powershellgallery.com/packages/DSInternals/

This script is intended to be used by project maintainers only.
Secret API key is required to publish the module.
#>

$rootDir = Split-Path -Path $PSScriptRoot -Parent

# Load the API key and exit on error
$apiKeyPath = Join-Path -Path $rootDir -ChildPath 'Keys\PSGallery.key'
$apiKey = Get-Content -Path $apiKeyPath -ErrorAction Stop

# Publish
$modulePath = Join-Path -Path $rootDir -ChildPath 'Build\bin\DSInternals.PowerShell\Release\DSInternals'

Publish-Module -Path $modulePath `
               -NuGetApiKey $apiKey `
               -Repository PSGallery -Force