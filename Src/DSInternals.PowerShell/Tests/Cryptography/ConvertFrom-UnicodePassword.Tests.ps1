Describe "ConvertFrom-UnicodePassword" {
	Context "When correct input is provided" {
        $testInput = 'UABhACQAJAB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA=='
		$expected = 'Pa$$w0rd'
        $actual = ConvertFrom-UnicodePassword $testInput

        It "should return the correct password" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the input is malformed" {
        $testInput = 'abc'

        It "should throw an exception" {
            { ConvertFrom-UnicodePassword $testInput } | Should Throw
        }
    }
}