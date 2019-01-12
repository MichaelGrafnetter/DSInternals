#Requires -Version 3
<#
.SYNOPSIS
Downloads the referenced NuGet packages from internet.
#>

$nuget = Join-Path $PSScriptRoot 'Tools\nuget.exe'
$solutionFile = Join-Path $PSScriptRoot '..\Src\DSInternals.sln'

& $nuget restore $solutionFile