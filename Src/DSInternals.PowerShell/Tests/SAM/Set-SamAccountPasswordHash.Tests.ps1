<#
.SYNOPSIS
    This script contains Pester tests for the Set-SamAccountPasswordHash cmdlet in the DSInternals PowerShell module.
.DESCRIPTION
	These tests require a domain controller to be available.
	The tests are skipped by default, but can be run manually.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Set-SamAccountPasswordHash' -Skip {
	$testServer = 'LON-DC1'
	$adminLogin = 'ADATUM\Administrator'
	$adminPwd = ConvertTo-SecureString 'Pa$$w0rd' -AsPlainText -Force
	
	$adminCred = New-Object PSCredential $adminLogin,$adminPwd
	$testUser = 'April'
	$testDomain = 'ADATUM'
	$newPwd = ConvertTo-SecureString 'pass' -AsPlainText -Force
	$newNtHash = ConvertTo-NTHash $newPwd
	
	Context 'Set NT hash' {
        It 'should not throw any error' {
            Set-SamAccountPasswordHash -SamAccountName $testUser -Domain $testDomain -NTHash $newNtHash -Server $testServer -Credential $adminCred
			# TODO: verify using repl command
        }
    }
	Context 'Set NT hash to non-existing account' {
        It 'should throw an exception' {
            { Set-SamAccountPasswordHash -SamAccountName 'abcdefghij' -Domain $testDomain -NTHash $newNtHash -Server $testServer -Credential $adminCred -ErrorAction Stop } |
				Should Throw
		}
    }
	Context 'Set NT hash to non-existing domain' {
        It 'should throw an exception' {
            { Set-SamAccountPasswordHash -SamAccountName 'abcdefghij' -Domain 'abcdefghij' -NTHash $newNtHash -Server $testServer -Credential $adminCred -ErrorAction Stop } |
				Should Throw
		}
    }
	Context 'Set NT hash to non-existing server' {
        It 'should throw an exception' {
             { Set-SamAccountPasswordHash -SamAccountName 'abcdefghij' -Domain 'abcdefghij' -NTHash $newNtHash -Server 'abcdefghij' -Credential $adminCred -ErrorAction Stop } |
				Should Throw
		}
    }
	Context 'Login using invalid credentials' {
        It 'should throw an exception' {
			$invalidCred = New-Object PSCredential 'abcdefghij',$adminPwd
            { Set-SamAccountPasswordHash -SamAccountName 'abcdefghij' -Domain 'abcdefghij' -NTHash $newNtHash -Server 'abcdefghij' -Credential $invalidCred -ErrorAction Stop } |
				Should Throw
		}
    }

}