<#
.SYNOPSIS
Generates NuGet Packages. 
#>
#Requires -Version 3

$repoRoot = Join-Path $PSScriptRoot '..\'
$nuget = Join-Path $repoRoot 'Scripts\Tools\nuget.exe'
$solutionDir = Join-Path $repoRoot 'Src\'
$outputDir = Join-Path  $repoRoot 'Build\packages\'

# Create output dir if it does not exist
mkdir $outputDir -Force

# Pack all *.csproj files that have a corresponding *.nuspec file
Get-ChildItem -Path $repoRoot -Filter *.nuspec -Recurse -File |
    ForEach-Object { $PSItem.FullName.Replace('.nuspec', '.csproj') } |
    ForEach-Object {
        $solutionFile = $PSItem
        & $nuget pack $solutionFile -OutputDirectory $outputDir -IncludeReferencedProjects -Properties "Configuration=Release;Platform=x64;SolutionDir=$solutionDir" -Verbosity detailed        
    }