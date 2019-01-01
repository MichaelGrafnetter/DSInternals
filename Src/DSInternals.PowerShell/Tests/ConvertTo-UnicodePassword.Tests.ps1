Describe "ConvertTo-UnicodePassword" {
	Context "When correct input is provided" {
        $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        $actual = ConvertTo-UnicodePassword $testInput
		$expected = "UABhACQAJAB3ADAAcgBkAA=="

        It "should return the correct base64 string" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the IsUnattendPassword switch is present" {
        $testInput = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
        $actual = ConvertTo-UnicodePassword $testInput -IsUnattendPassword
		$expected = "UABhACQAJAB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA=="

        It "should return the correct encrypted base64 string" {
            $actual | Should BeExactly $expected
        }
    }
}