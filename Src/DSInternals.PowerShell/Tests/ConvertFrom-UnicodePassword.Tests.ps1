Describe "ConvertFrom-UnicodePassword" {
	Context "When correct input is provided" {
        $input = 'UABhACQAJAB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA=='
		$expected = 'Pa$$w0rd'
        $actual = ConvertFrom-UnicodePassword $input

        It "should return the correct password" {
            $actual | Should BeExactly $expected
        }
    }
	Context "When the input is malformed" {
        $input = 'abc'

        It "should throw an exception" {
            { ConvertFrom-UnicodePassword $input } | Should Throw
        }
    }
}