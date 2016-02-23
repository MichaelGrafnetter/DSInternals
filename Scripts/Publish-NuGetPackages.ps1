<#
.SYNOPSIS
Publishes NuGet packages to nuget.org.

.DESCRIPTION
This script is intended to be used by project maintainers only.
Secret API key is required to publish the module.
#>
#Requires -Version 3

$repoRoot = Join-Path $PSScriptRoot '..\'
$nuget = Join-Path $repoRoot 'Scripts\Tools\nuget.exe'
$packagesDir = Join-Path  $repoRoot 'Build\packages\'

# Load the API key and exit on error
$apiKeyPath = Join-Path $repoRoot 'Keys\NuGet.key'
$apiKey = Get-Content $apiKeyPath -ErrorAction Stop

Get-ChildItem -Path $packagesDir -Filter *.nupkg -Recurse -File |
    ForEach-Object {
        $packagePath = $PSItem.FullName
        & $nuget push $packagePath -ApiKey $apiKey -NonInteractive -Verbosity detailed
    }