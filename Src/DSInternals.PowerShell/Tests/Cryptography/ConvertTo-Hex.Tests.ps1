<#
.SYNOPSIS
    This script contains Pester tests for the ConvertTo-Hex cmdlet in the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'ConvertTo-Hex' {
	Context 'When the input is a byte array' {
		[byte[]] $testInput = (43,128,149,198,255)
        $expected = '2b8095c6ff'
        $actual = ConvertTo-Hex $testInput
			 
        It 'should return the correct hex number' {
            $actual | Should -BeExactly $expected
        }
    }
	Context 'When the switch UpperCase is present' {
		[byte[]] $testInput = (43,128,149,198,255)
        $expected = '2B8095C6FF'
        $actual = ConvertTo-Hex $testInput -UpperCase
			 
        It 'should return the correct hex number with capital letters' {
            $actual | Should -BeExactly $expected
        }
    }
	Context 'When the input is null' {
		[byte[]] $testInput = $null
        $actual = ConvertTo-Hex $testInput
		$expected = $null
			 
        It 'should return null' {
            $actual | Should -BeExactly $expected
        }
    }
	Context 'When the input is an ampty array' {
		[byte[]] $testInput = @()
        $actual = ConvertTo-Hex $testInput
		$expected = $null
			 
        It 'should return null' {
            $actual | Should -BeExactly $expected
        }
    }
}