 <#
.SYNOPSIS
    This script contains Pester tests for the Get-SamPasswordPolicy cmdlet from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-SamPasswordPolicy' {
    It 'should return the password policy for the local SAM database' {
        [DSInternals.SAM.SamDomainPasswordInformation] $policy = Get-SamPasswordPolicy -Domain Builtin
        $policy | Should -Not -BeNull
        # This will probably be fals for every test computer
        $policy.ReversibleEncryptionEnabled | Should -Be $false
    }
}
