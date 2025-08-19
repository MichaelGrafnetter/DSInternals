<#
.SYNOPSIS
    This script contains Pester tests for the ConvertTo-OrgIdHash cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertTo-OrgIdHash' {
    It 'should always return the same string when the salt is provided' {
        [securestring] $password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        ConvertTo-OrgIdHash -Password $password -Salt '317ee9d1dec6508fa510' | Should -BeExactly 'v1;PPH1_MD4,317ee9d1dec6508fa510,1000,7eaea8e1628dffee62cf319f4e1fc05254da30a1d42ff755ff352f5b13497531;'
    }

    It 'should return a string with the correct format when the salt is not provided' {
        [securestring] $password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        ConvertTo-OrgIdHash -Password $password | Should -MatchExactly '^v1;PPH1_MD4,[a-f0-9]{20},1000,[a-f0-9]{64};$'
    }
}