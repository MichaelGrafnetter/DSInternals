<#
.SYNOPSIS
    This script contains Pester tests for the Get-ADDBDomainController cmdlet from the DSInternals PowerShell module.
.DESCRIPTION
    This is only a smoke test, as it uses the built-in initial AD database file, which is present on all Windows Server installations.
    More sophisticated tests are required.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-ADDBDomainController' {
    It 'can open the initial ntds.dit' {
        # This test only works on Windows Server
        [string] $initialDBPath = "$env:SystemRoot\System32\ntds.dit"

        if (Test-Path -Path $initialDBPath -PathType Leaf) {
            # Create a copy of the initial ntds.dit file, to avoid possible modification by the tests.
            [string] $workingNTDSCopy = Copy-Item -Path $initialDBPath -Destination TestDrive:\ -PassThru -Force
            
            try {
                # Try to open the DB
                [DSInternals.PowerShell.DomainController] $dc = Get-ADDBDomainController -DatabasePath $workingNTDSCopy

                # Validate initial DB properties
                $dc | Should -Not -BeNull
                $dc.State | Should -Be 'Boot'
                $dc.Name | Should -Be 'BootMachine'
                $dc.DomainMode | Should -Be 'Win2000'
                $dc.ForestMode | Should -Be 'Win2000'
                $dc.ConfigurationNamingContext | Should -Be 'O=Boot'
                $dc.SchemaNamingContext | Should -Be 'CN=Schema,O=Boot'
            } finally {
                # Delete the temporary file
                Remove-Item -Path $workingNTDSCopy
            }
        } else {
            Set-ItResult -Inconclusive -Because 'The initial DB is not present on client SKUs.'
        }
    }
}
