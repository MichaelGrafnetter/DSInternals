<#
.SYNOPSIS
    This script contains Pester tests for the ConvertTo-NTHash cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertTo-NTHash' {
    It 'should return the correct hash when the input is a unicode string' {
        [securestring] $testInput = ConvertTo-SecureString 'žluťoučký kůň' -AsPlainText -Force
        ConvertTo-NTHash -Password $testInput | Should -Be '0D90FB43740BE81B67E6A79A113817C4'
    }

    It 'should return the correct hash when the input is an ASCII string' {
        [securestring] $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        ConvertTo-NTHash $testInput | Should -Be '92937945B518814341DE3F726500D4FF'
    }

    It 'should return multiple hashes when the input comes from the pipeline' {
        [securestring] $testInput1 = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        [securestring] $testInput2 = ConvertTo-SecureString 'test' -AsPlainText -Force
        $testInput1,$testInput2 | ConvertTo-NTHash | Should -Be '92937945B518814341DE3F726500D4FF','0CB6948805F797BF2A82807973B89537'
    }

    It 'should return the correct hash when the input is an empty string' {
        ConvertTo-NTHash -Password (New-Object SecureString) | Should -Be '31D6CFE0D16AE931B73C59D7E0C089C0'
    }

    It 'should throw an exception when the input is a long string' {
        [securestring] $testInput = ConvertTo-SecureString '012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789' -AsPlainText -Force
        { ConvertTo-NTHash $testInput } | Should -Throw
    }
}