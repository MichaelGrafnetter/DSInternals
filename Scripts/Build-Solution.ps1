#Requires -Version 3
<#
.SYNOPSIS
Compiles the binaries from source codes.
#>

$solutionDir = Join-Path $PSScriptRoot '..\Src'
$solutionFile = Join-Path $solutionDir 'DSInternals.sln'

# We need the Invoke-MSBuild module to always invoke the latest msbuild.exe.
$modulePath = Join-Path $PSScriptRoot 'Modules\Invoke-MSBuild'
Import-Module $modulePath -ErrorAction Stop

$targets = 'Clean','Build'
$configurations = 'Release' #,'Debug'
$platforms = 'x86','x64'

# Run all targets with all configurations and platforms
foreach($target in $targets)
{
    foreach($configuration in $configurations)
    {
        foreach($platform in $platforms)
        {
            Write-Host "$($target)ing $configuration|$platform..."
            Invoke-MsBuild -MsBuildParameters "/target:$target /property:Configuration=$configuration;Platform=$platform" `
                           -Path $solutionFile -ShowBuildWindow
        }
    }
}