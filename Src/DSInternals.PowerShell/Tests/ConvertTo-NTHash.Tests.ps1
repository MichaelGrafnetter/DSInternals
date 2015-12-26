Describe "ConvertTo-NTHash" {
	Context "When the input is a unicode string" {
        $input = ConvertTo-SecureString 'ûluùouËk˝ k˘Ú' -AsPlainText -Force 
        $expected = "0D90FB43740BE81B67E6A79A113817C4"
        $actual = ConvertTo-NTHash $input
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an ASCII string" {
        $input = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force 
        $expected = "92937945B518814341DE3F726500D4FF"
        $actual = ConvertTo-NTHash $input
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input comes from the pipeline" {
        $input1 = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
		$input2 = ConvertTo-SecureString 'test' -AsPlainText -Force
        $expected = "92937945B518814341DE3F726500D4FF","0CB6948805F797BF2A82807973B89537" 
        $actual = $input1,$input2 | ConvertTo-NTHash
			 
        It "should return multiple hashes" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an empty string" {
        $input = New-Object SecureString
        $expected = "31D6CFE0D16AE931B73C59D7E0C089C0"
        $actual = ConvertTo-NTHash $input
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is a long string" {
        $input = ConvertTo-SecureString '012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789' -AsPlainText -Force

        It "should throw an exception" {
            { ConvertTo-NTHash $input } | Should Throw
        }
    }
}