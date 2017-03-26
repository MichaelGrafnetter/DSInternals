#Requires -Version 3
<#
.SYNOPSIS
Compiles the binaries from source codes.
#>

$rootDir = Split-Path $PSScriptRoot -Parent
$solutionFile = Join-Path $rootDir 'Src\DSInternals.sln'
$buildDir = Join-Path $rootDir 'Build'
$logRootDir = Join-Path $buildDir 'log'

# We need the Invoke-MSBuild module to always invoke the latest msbuild.exe.
$modulePath = Join-Path $PSScriptRoot 'Modules\Invoke-MSBuild'
Import-Module $modulePath -ErrorAction Stop

$targets = 'Build' # 'Clean'
$configurations = 'Release', 'Debug'
$platforms = 'x86','x64'

# Delete the entire Build directory
if(Test-Path $buildDir)
{
    del $buildDir -Recurse -Force
}

# Run all targets with all configurations and platforms
foreach($target in $targets)
{
    foreach($configuration in $configurations)
    {
        foreach($platform in $platforms)
        {
            Write-Host "$($target)ing $configuration|$platform..."
            $logDir = Join-Path $logRootDir "$configuration\$platform"
            mkdir $logDir -Force | Out-Null
            $result = Invoke-MsBuild -MsBuildParameters "/target:$target /property:Configuration=$configuration;Platform=$platform" `
                           -Path $solutionFile -BuildLogDirectoryPath $logDir -KeepBuildLogOnSuccessfulBuilds -ShowBuildOutputInNewWindow
            echo ('Success: {0}' -f $result.BuildSucceeded)
        }
    }
}