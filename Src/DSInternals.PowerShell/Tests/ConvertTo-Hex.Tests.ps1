Describe "ConvertTo-Hex" {
	Context "When the input is a byte array" {
		[byte[]] $input = (43,128,149,198,255)
        $expected = "2b8095c6ff"
        $actual = ConvertTo-Hex $input
			 
        It "should return the correct hex number" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the switch UpperCase is present" {
		[byte[]] $input = (43,128,149,198,255)
        $expected = "2B8095C6FF"
        $actual = ConvertTo-Hex $input -UpperCase
			 
        It "should return the correct hex number with capital letters" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the input is null" {
		[byte[]] $input = $null
        $actual = ConvertTo-Hex $input
		$expected = $null
			 
        It "should return null" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the input is an ampty array" {
		[byte[]] $input = @()
        $actual = ConvertTo-Hex $input
		$expected = $null
			 
        It "should return null" {
            $actual | Should BeExactly $expected
        }
    }
}