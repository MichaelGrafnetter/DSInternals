<#
.SYNOPSIS
    This script contains Pester tests for the ConvertFrom-UnicodePassword and ConvertTo-UnicodePassword cmdlets in the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertFrom-UnicodePassword' {
    Context 'ConvertFrom-UnicodePassword' {
        It 'should return the correct password when correct input is provided' {
            ConvertFrom-UnicodePassword -UnicodePassword 'UABhACQAJAB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA==' |
                Should -BeExactly 'Pa$$w0rd'
        }

        It 'should throw an exception when the input is malformed' {
            { ConvertFrom-UnicodePassword -UnicodePassword 'abc' } | Should -Throw
        }
    }

    Context 'ConvertTo-UnicodePassword' {
        It 'should return the correct base64 string when correct input is provided' {
            [securestring] $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
            ConvertTo-UnicodePassword -Password $testInput | Should -BeExactly 'UABhACQAJAB3ADAAcgBkAA=='
        }

        It 'should return the correct encrypted base64 string when the IsUnattendPassword switch is present' {
            [securestring] $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
            ConvertTo-UnicodePassword -Password $testInput -IsUnattendPassword |
                Should -BeExactly 'UABhACQAJAB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA=='
        }
    }
}
