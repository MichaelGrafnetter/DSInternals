<#
.SYNOPSIS
Generates NuGet Packages. 
#>
#Requires -Version 3

$repoRoot = Join-Path $PSScriptRoot '..\'
$solutionDir = Join-Path $repoRoot 'Src\'
$outputDir = Join-Path  $repoRoot 'Build\packages\NuGet\'
$toolsDir = Join-Path $repoRoot 'Scripts\Tools'
$nuget = Join-Path $toolsDir 'nuget.exe'

# Create output dir if it does not exist
mkdir $outputDir -Force

# Download nuget.exe to Tools
$nugetExeUrl = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'

if(-not (Test-Path $nuget))
{
    New-Item -Path $toolsDir -ItemType Directory -Force
    Invoke-WebRequest -Uri $nugetExeUrl -OutFile $nuget
}

# Pack all *.csproj files that have a corresponding *.nuspec file
Get-ChildItem -Path $repoRoot -Filter *.nuspec -Recurse -File |
    ForEach-Object { $PSItem.FullName.Replace('.nuspec', '.csproj') } |
    Where-Object { Test-Path -Path $PSItem -PathType Leaf } |
    ForEach-Object {
        $solutionFile = $PSItem
        & $nuget pack $solutionFile -OutputDirectory $outputDir -IncludeReferencedProjects -Verbosity detailed -NonInteractive
    }