#Requires -Version 5
<#
.SYNOPSIS
Publishes the module into Chocolatey Gallery.

.DESCRIPTION

This script is intended to be used by project maintainers only.
Secret API key is required to publish the module.
#>

$rootDir = Split-Path $PSScriptRoot -Parent

# Load the API key and exit on error
$apiKeyPath = Join-Path $rootDir 'Keys\Chocolatey.key'
$apiKey = Get-Content $apiKeyPath -ErrorAction Stop

# Publish
$packagePath = Join-Path $rootDir 'Build\packages\Chocolatey\DSInternals-PSModule.*.nupkg' -Resolve -ErrorAction Stop
choco push $packagePath --apikey $apiKey --source 'https://push.chocolatey.org/'
