#Requires -Version 3

$solutionDir = Join-Path $PSScriptRoot '..\Src'
$solutionFile = Join-Path $solutionDir 'DSInternals.sln'

# We need the Invoke-MSBuild module (distributed as NuGet package) to always invoke the latest msbuild.exe.
$modulePath = Join-Path $solutionDir  '.\packages\invokemsbuild*\Modules\Invoke-MSBuild'
Import-Module $modulePath -ErrorAction Stop

$configuration = 'Release'
# $configuration = 'Debug'

# Clean
Invoke-MsBuild -MsBuildParameters "/target:Clean /property:Configuration=$configuration;Platform=x64" `
               -Path $solutionFile
Invoke-MsBuild -MsBuildParameters "/target:Clean /property:Configuration=$configuration;Platform=x86" `
               -Path $solutionFile

# Build
Invoke-MsBuild -MsBuildParameters "/target:Build /property:Configuration=$configuration;Platform=x64" `
               -Path $solutionFile -ShowBuildWindow
Invoke-MsBuild -MsBuildParameters "/target:Build /property:Configuration=$configuration;Platform=x86" `
               -Path $solutionFile -ShowBuildWindow