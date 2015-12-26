<#
.SYNOPSIS
This script is configured to be executed by the debugger.
Cmdlets you would like to debug can be executed from here.
#>

# Clean the prompt
function prompt()
{
    'PS> '
}

# Import the module from the current directory, while removing any pre-existing modules with the same name first.
Remove-Module -Name DSInternals -Force -ErrorAction SilentlyContinue -Verbose
Import-Module -Name .\DSInternals -Verbose -ErrorAction Stop

# Set directory paths 
$rootDir = Join-Path $PSScriptRoot '..\..\..\..\'
$testDataDir = Join-Path $rootDir 'TestData'
$solutionDir = Join-Path $rootDir 'Src'

# Run test commands
Get-BootKey -Online

$password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
ConvertTo-NTHash $password
