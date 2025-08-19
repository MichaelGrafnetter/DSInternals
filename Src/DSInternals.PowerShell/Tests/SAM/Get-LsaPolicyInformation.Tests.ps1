<#
.SYNOPSIS
    This script contains Pester tests for the Get-LsaPolicyInformation cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-LsaPolicyInformation' {
    It 'should returning computer name' {
        [DSInternals.PowerShell.LsaPolicyInformation] $policy = Get-LsaPolicyInformation
        $policy.LocalDomain.Name | Should -Be $env:COMPUTERNAME
    }
}
