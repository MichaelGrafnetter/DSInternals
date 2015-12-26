Describe "Test1" {
	Context "Test1" {
        
		#dir Env:\ > c:\e.txt
		#Get-Variable > C:\v.txt
        It "test" {
            $true | Should BeExactly $true
        }
    }
}