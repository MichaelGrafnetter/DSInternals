Describe "ConvertTo-Hex" {
	Context "When the salt is provided" {
		$password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
		$salt = "317ee9d1dec6508fa510"
        $expected = "v1;PPH1_MD4,317ee9d1dec6508fa510,100,f4a257ffec53809081a605ce8ddedfbc9df9777b80256763bc0a6dd895ef404f;"
        $actual = ConvertTo-AADHash -Password $password -Salt $salt

        It "should always return the same string" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the salt is not provided" {
		$password = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        $expected = "v1;PPH1_MD4,317ee9d1dec6508fa510,100,f4a257ffec53809081a605ce8ddedfbc9df9777b80256763bc0a6dd895ef404f;"
        $actual = ConvertTo-AADHash -Password $password

        It "should return a string with the correct format" { $actual | Should MatchExactly '^v1;PPH1_MD4,[a-f0-9]{20},100,[a-f0-9]{64};$' }
    }
}