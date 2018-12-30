<#
.SYNOPSIS
Publishes NuGet packages to nuget.org.

.DESCRIPTION
This script is intended to be used by project maintainers only.
Secret API key is required to publish the module.
#>
#Requires -Version 5

$repoRoot = Join-Path $PSScriptRoot '..\'
$packagesDir = Join-Path  $repoRoot 'Build\packages\'

# NuGet.org API
$nugetGallery = 'https://www.nuget.org/api/v2/package'
$nugetExeUrl = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'

# Download nuget.exe to Tools
$toolsDirectoryPath = Join-Path $repoRoot 'Scripts\Tools'
$nuget = Join-Path $toolsDirectoryPath 'nuget.exe'
if(-not (Test-Path $nuget))
{
    mkdir $toolsDirectoryPath
    Invoke-WebRequest -Uri $nugetExeUrl -OutFile $nuget
}

# Load the API key and exit on error
$apiKeyPath = Join-Path $repoRoot 'Keys\NuGet.key'
$apiKey = Get-Content $apiKeyPath -ErrorAction Stop

Get-ChildItem -Path $packagesDir -Filter *.nupkg -Recurse -File |
    ForEach-Object {
        $packagePath = $PSItem.FullName
        & $nuget push $packagePath -ApiKey $apiKey -Source $nugetGallery -NonInteractive -Verbosity detailed
    }