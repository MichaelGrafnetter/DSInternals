<#
.SYNOPSIS
    This script contains Pester tests for the Get-DpapiNgData, Protect-DpapiNgData,
    and Unprotect-DpapiNgData cmdlets from the DSInternals PowerShell module.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'DPAPI-NG Data Round-Trip' {
    BeforeAll {
        [string] $descriptor = 'LOCAL=user'
        [string] $secret     = 'Pa$$w0rd'
    }

    Context 'Protect-DpapiNgData | Unprotect-DpapiNgData' {
        It 'round-trips a UTF-16 string with a LOCAL=user descriptor' {
            [string] $blob = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret
            $blob | Should -Not -BeNullOrEmpty

            [string] $recovered = Unprotect-DpapiNgData -Blob $blob -Encoding ([System.Text.Encoding]::Unicode)
            $recovered | Should -BeExactly $secret
        }

        It 'round-trips a UTF-8 string when -Encoding is supplied' {
            [System.Text.Encoding] $encoding = [System.Text.Encoding]::UTF8
            [string] $blob = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret -Encoding $encoding
            [string] $recovered = Unprotect-DpapiNgData -Blob $blob -Encoding $encoding
            $recovered | Should -BeExactly $secret
        }

        It 'round-trips a UTF-8 string when -Encoding is supplied as a string' {
            [string] $blob = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret -Encoding UTF8
            [string] $recovered = Unprotect-DpapiNgData -Blob $blob -Encoding utf8
            $recovered | Should -BeExactly $secret
        }

        It 'produces different ciphertext each time for the same plaintext' {
            [string] $blob1 = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret
            [string] $blob2 = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret
            $blob1 | Should -Not -Be $blob2
        }
    }

    Context 'Get-DpapiNgData' {
        BeforeAll {
            [string] $blob = Protect-DpapiNgData -Descriptor $descriptor -Cleartext $secret
            $parsed = Get-DpapiNgData -Blob $blob
        }

        It 'returns a CngProtectedDataBlob instance' {
            $parsed | Should -Not -BeNullOrEmpty
            $parsed.GetType().FullName | Should -Be 'DSInternals.Common.Cryptography.CngProtectedDataBlob'
        }

        It 'recovers the LOCAL=user protection descriptor rule' {
            $parsed.Descriptor | Should -BeExactly $descriptor
        }

        It 'parses the AES-256-GCM content encryption algorithm OID' {
            $parsed.ContentEncryptionAlgorithm.Value | Should -Be '2.16.840.1.101.3.4.1.46'
        }

        It 'exposes a non-empty encrypted payload and AES-GCM nonce' {
            $parsed.EncryptedData.Length | Should -BeGreaterThan 0
            $parsed.Nonce.Length         | Should -Be 12
        }
    }
}
