<#
.SYNOPSIS
Generates a Chocolatey package. Requires Chocolatey to be installed.
#>
#Requires -Version 3

$repoRoot = Join-Path $PSScriptRoot '..\'
$nuspecPath = Join-Path $repoRoot 'Src\DSInternals.PowerShell.Chocolatey\dsinternals.nuspec'
$outputDir = Join-Path  $repoRoot 'Build\packages\Chocolatey'

mkdir $outputDir -Force | Out-Null
choco pack $nuspecPath --outputdirectory $outputDir