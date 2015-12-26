#Requires -Version 3

<#
A few of the referenced NuGet packages do not have a strong name.
As we want our assemblies to be signed, we have to sign these NuGet packages first.
This has to be done every time a referenced package gets updated.
#>

$solutionDir = Join-Path $PSScriptRoot '..\Src'
$packagesDir = Join-Path $solutionDir '.\packages'
$signer = Join-Path $packagesDir '.\Brutal.Dev.StrongNameSigner*\tools\StrongNameSigner.Console.exe'

$assembly1 = Join-Path $packagesDir '.\CryptSharpOfficial*\lib\CryptSharp.dll' -Resolve

& $signer -AssemblyFile $assembly1