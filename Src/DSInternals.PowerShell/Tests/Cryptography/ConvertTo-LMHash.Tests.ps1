<#
.SYNOPSIS
    This script contains Pester tests for the ConvertTo-LMHash cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertTo-LMHash' {
    It 'should return the correct hash when the input is an ASCII string' {
        [securestring] $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        ConvertTo-LMHash $testInput | Should -Be '727E3576618FA1754A3B108F3FA6CB6D'
    }

    It 'should return multiple hashes when the input comes from the pipeline' {
        [securestring] $testInput1 = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        [securestring] $testInput2 = ConvertTo-SecureString 'test' -AsPlainText -Force
        $testInput1,$testInput2 | ConvertTo-LMHash | Should -Be '727E3576618FA1754A3B108F3FA6CB6D','01FC5A6BE7BC6929AAD3B435B51404EE'
    }

    It 'should return the correct hash when the input is an empty string' {
        ConvertTo-LMHash -Password (New-Object SecureString) | Should -Be 'AAD3B435B51404EEAAD3B435B51404EE'
    }
    
    It 'should throw an exception when the input is a long string' {
        [securestring] $testInput = ConvertTo-SecureString 'EHB3xUAY2NZIp9wI7khNWGWyOiuhyK' -AsPlainText -Force
        { ConvertTo-LMHash $testInput } | Should -Throw
    }
}
