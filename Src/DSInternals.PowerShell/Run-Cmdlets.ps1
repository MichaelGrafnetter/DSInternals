<#
.SYNOPSIS
This script is configured to be executed by the debugger.
Cmdlets you would like to debug can be executed from here.
#>

# Requires -Version 5

# Clean the prompt
function prompt()
{
    'PS> '
}

# Remove any pre-existing modules with the same name first.
Remove-Module -Name DSInternals -Force -ErrorAction SilentlyContinue -Verbose

# Import the module from the parent directory of the working directory
[string] $workingDirectory = (Get-Location).ProviderPath
[string] $moduleDirectory = Split-Path -Path $workingDirectory -Parent

Import-Module -Name $moduleDirectory -Verbose

# Set directory paths 
[string] $solutionDirectory = Split-Path -Path $PSScriptRoot -Parent
[string] $repoRootDirectory = Split-Path -Path $solutionDirectory -Parent
[string] $testDataDirectory = Join-Path -Path $repoRootDirectory -ChildPath 'TestData'

# Run test commands
Get-BootKey -Online

$password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
ConvertTo-NTHash $password
