Describe "ConvertTo-NTHash" {
	Context "When the input is a unicode string" {
        $testInput = ConvertTo-SecureString 'ûluùouËk˝ k˘Ú' -AsPlainText -Force 
        $expected = "0D90FB43740BE81B67E6A79A113817C4"
        $actual = ConvertTo-NTHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an ASCII string" {
        $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force 
        $expected = "92937945B518814341DE3F726500D4FF"
        $actual = ConvertTo-NTHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input comes from the pipeline" {
        $testInput1 = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
		$testInput2 = ConvertTo-SecureString 'test' -AsPlainText -Force
        $expected = "92937945B518814341DE3F726500D4FF","0CB6948805F797BF2A82807973B89537" 
        $actual = $testInput1,$testInput2 | ConvertTo-NTHash
			 
        It "should return multiple hashes" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an empty string" {
        $testInput = New-Object SecureString
        $expected = "31D6CFE0D16AE931B73C59D7E0C089C0"
        $actual = ConvertTo-NTHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is a long string" {
        $testInput = ConvertTo-SecureString '012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789' -AsPlainText -Force

        It "should throw an exception" {
            { ConvertTo-NTHash $testInput } | Should Throw
        }
    }
}