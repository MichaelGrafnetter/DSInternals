<#
.SYNOPSIS
    This script contains Pester tests for the Get-ADReplAccount cmdlet from the DSInternals PowerShell module.
.DESCRIPTION
    This is only a smoke test, as it only tries to connect to a non-existing server.
    More sophisticated tests are required.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-ADReplAccount' {
    It 'is trying to establish an RPC connection' {
        # We do not have a test server, so we try to connect to a dummy address.
        # This will just check that the Interop stuff is compiled correctly, which is a good start.
        { Get-ADReplAccount -Server 'NonExistingServer.github.com' -All } |
            Should -Throw -Because 'the RPC server is unavailable.'
    }
}
