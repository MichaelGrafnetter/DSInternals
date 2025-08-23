<#
.SYNOPSIS
    Debugging script for the DSInternals PowerShell module.
.PARAMETER ModulePath
    Path to the compiled PowerShell module
#>

#Requires -Version 5.1

param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    # No path has been provided, so use a the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\bin\DSInternals.PowerShell\Release\DSInternals' -Resolve -ErrorAction Stop
} else {
    [bool] $isFile = Test-Path -Path $ModulePath -PathType Leaf -ErrorAction SilentlyContinue
    if ($isFile) {
        # This is probably the module manifest path
        # Get the path to the module directory, without the trailing slash
        $ModulePath = Split-Path -Path $ModulePath -Parent -Resolve -ErrorAction Stop
    } else {
        # Translate possibly relative module directory path to an absolute one
        $ModulePath = Resolve-Path -Path $ModulePath -ErrorAction Stop
    }
}

# Clean the prompt
function prompt() { 'PS> ' }

# Load the compiled module
Import-Module -Name $ModulePath -Force -Verbose -ErrorAction Stop

# Set directory paths 
[string] $solutionDirectory = Split-Path -Path $PSScriptRoot -Parent
[string] $repoRootDirectory = Split-Path -Path $solutionDirectory -Parent
[string] $testDataDirectory = Join-Path -Path $repoRootDirectory -ChildPath 'TestData'

# Run test commands
Get-BootKey -Online

$password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
ConvertTo-NTHash $password
