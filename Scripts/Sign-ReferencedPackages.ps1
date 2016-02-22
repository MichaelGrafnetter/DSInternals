<#
.SYNOPSIS
Signs the referenced NuGet packages that do not have a strong name.

.DESCRIPTION
A few of the referenced NuGet packages do not have a strong name.
As we want our assemblies to be signed, we have to sign these NuGet packages first.
This has to be done every time a referenced package gets updated.
#>
#Requires -Version 3

$solutionDir = Join-Path $PSScriptRoot '..\Src'
$packagesDir = Join-Path $solutionDir '.\packages'
$signer = Join-Path $packagesDir '.\Brutal.Dev.StrongNameSigner*\tools\StrongNameSigner.Console.exe'

# All referenced packages are currently strong-name signed, so there is no need for this script.

#$assembly1 = Join-Path $packagesDir '.\CryptSharpOfficial*\lib\CryptSharp.dll' -Resolve
#& $signer -AssemblyFile $assembly1