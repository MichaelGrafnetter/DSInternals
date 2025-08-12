Describe "ConvertTo-LMHash" {
	Context "When the input is a unicode string" {
        $testInput = ConvertTo-SecureString 'ûluùouËk˝ k˘Ú' -AsPlainText -Force 
        $expected = "AAD3B435B51404EEAAD3B435B51404EE"
        $actual = ConvertTo-LMHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an ASCII string" {
        $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force 
        $expected = "727E3576618FA1754A3B108F3FA6CB6D"
        $actual = ConvertTo-LMHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input comes from the pipeline" {
        $testInput1 = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
		$testInput2 = ConvertTo-SecureString 'test' -AsPlainText -Force
        $expected = "727E3576618FA1754A3B108F3FA6CB6D","01FC5A6BE7BC6929AAD3B435B51404EE" 
        $actual = $testInput1,$testInput2 | ConvertTo-LMHash
			 
        It "should return multiple hashes" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is an empty string" {
        $testInput = New-Object SecureString
        $expected = "AAD3B435B51404EEAAD3B435B51404EE"
        $actual = ConvertTo-LMHash $testInput
			 
        It "should return the correct hash" {
            $actual | Should Be $expected
        }
    }
	Context "When the input is a long string" {
        $testInput = ConvertTo-SecureString 'EHB3xUAY2NZIp9wI7khNWGWyOiuhyK' -AsPlainText -Force

        It "should throw an exception" {
            { ConvertTo-LMHash $testInput } | Should Throw
        }
    }
}