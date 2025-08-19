<#
.SYNOPSIS
    This script contains Pester tests for the ConvertTo-Hex cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertTo-Hex' {
    It 'should return the correct hex number when the input is a byte array' {
        ConvertTo-Hex -Input @(43,128,149,198,255) | Should -BeExactly '2b8095c6ff'
    }

    It 'should return the correct hex number with capital letters when the UpperCase switch is present' {
        ConvertTo-Hex -Input @(43,128,149,198,255) -UpperCase | Should -BeExactly '2B8095C6FF'
    }
}