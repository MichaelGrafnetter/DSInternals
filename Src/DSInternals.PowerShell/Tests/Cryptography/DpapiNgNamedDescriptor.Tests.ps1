<#
.SYNOPSIS
    This script contains Pester tests for the New-DpapiNgNamedDescriptor, Get-DpapiNgNamedDescriptor,
    and Remove-DpapiNgNamedDescriptor cmdlets from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'DPAPI-NG Named Descriptor' {
    BeforeAll {
        # Use a randomized name so concurrent or repeated runs cannot collide with each other
        # or with any descriptor that the user happens to have registered.
        [string] $descriptorName = 'DSInternalsPesterTest_' + [guid]::NewGuid().ToString('N')
        [string] $descriptorRule = 'LOCAL=user'
    }

    AfterAll {
        # Best-effort cleanup in case a failing test left the descriptor registered.
        Remove-DpapiNgNamedDescriptor -Name $descriptorName -ErrorAction SilentlyContinue
    }

    Context 'New-DpapiNgNamedDescriptor' {
        It 'registers a new descriptor without throwing' {
            { New-DpapiNgNamedDescriptor -Name $descriptorName -Descriptor $descriptorRule } |
                Should -Not -Throw
        }
    }

    Context 'Get-DpapiNgNamedDescriptor' {
        It 'returns the registered descriptor by name' {
            $result = Get-DpapiNgNamedDescriptor -Name $descriptorName
            $result          | Should -Not -BeNullOrEmpty
            $result.Key      | Should -BeExactly $descriptorName
            $result.Value    | Should -BeExactly $descriptorRule
        }

        It 'includes the registered descriptor when enumerating all descriptors' {
            $all = Get-DpapiNgNamedDescriptor
            ($all | Where-Object Key -EQ $descriptorName).Value |
                Should -BeExactly $descriptorRule
        }

        It 'writes an error when the descriptor does not exist' {
            $missing = 'DSInternalsPesterMissing_' + [guid]::NewGuid().ToString('N')
            Get-DpapiNgNamedDescriptor -Name $missing -ErrorAction SilentlyContinue -ErrorVariable err
            $err | Should -Not -BeNullOrEmpty
        }
    }

    Context 'Remove-DpapiNgNamedDescriptor' {
        It 'deletes the registered descriptor' {
            Remove-DpapiNgNamedDescriptor -Name $descriptorName
            Get-DpapiNgNamedDescriptor -Name $descriptorName -ErrorAction SilentlyContinue -ErrorVariable err
            $err | Should -Not -BeNullOrEmpty
        }

        It 'writes an error when the descriptor does not exist' {
            $missing = 'DSInternalsPesterMissing_' + [guid]::NewGuid().ToString('N')
            Remove-DpapiNgNamedDescriptor -Name $missing -ErrorAction SilentlyContinue -ErrorVariable err
            $err | Should -Not -BeNullOrEmpty
        }
    }
}
