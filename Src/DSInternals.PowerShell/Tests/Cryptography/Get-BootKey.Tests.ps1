<#
.SYNOPSIS
    This script contains Pester tests for the Get-BootKey cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-BootKey' {
    It 'should return a valid boot key in online mode' {
        [string] $bootKey = Get-BootKey -Online
        $bootKey.Length | Should -Be 32
    }

    It 'should return a valid boot key in offline mode' {
        Set-ItResult -Inconclusive -Because 'offline mode is not yet supported in tests.'
    }
}
    