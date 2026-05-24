<#
.SYNOPSIS
    This script contains Pester tests for CLI XML serialization of the KdsRootKey class.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'KdsRootKey CLI XML Serialization' {
    BeforeAll {
        [guid] $testKeyId = [guid]::Parse('7dc95c96-fa85-183a-dff5-f70696bf0b11')
        [byte[]] $testKeyValue = [DSInternals.Common.ByteArrayExtensions]::HexToBinary(
            '814ad2f3928ff96d3650487967392feab3924f3d0dff8629d46a723640101cff8ca2cbd6aba40805cf03b380803b27837d80663eb4d18fd4cec414ebb2271fe2'
        )
        [DSInternals.Common.Data.KdsRootKey] $originalKey =
            [DSInternals.Common.Data.KdsRootKey]::new($testKeyId, $testKeyValue)

        [string] $exportPath = Join-Path -Path TestDrive: -ChildPath 'KdsRootKey.xml'
        $originalKey | Export-Clixml -Path $exportPath
        $deserializedKey = Import-Clixml -Path $exportPath
    }

    It 'preserves the deserialized type name' {
        $deserializedKey.PSObject.TypeNames |
            Should -Contain 'Deserialized.DSInternals.Common.Data.KdsRootKey'
    }

    It 'round-trips the KeyId' {
        [guid] $deserializedKey.KeyId | Should -Be $testKeyId
    }

    It 'round-trips the KeyValue' {
        [DSInternals.Common.ByteArrayExtensions]::ToHex([byte[]] $deserializedKey.KeyValue) |
            Should -Be ([DSInternals.Common.ByteArrayExtensions]::ToHex($testKeyValue))
    }

    It 'round-trips the KDF algorithm and parameters' {
        $deserializedKey.KdfAlgorithm | Should -Be 'SP800_108_CTR_HMAC'
        [DSInternals.Common.ByteArrayExtensions]::ToHex([byte[]] $deserializedKey.RawKdfParameters) |
            Should -Be ([DSInternals.Common.ByteArrayExtensions]::ToHex($originalKey.RawKdfParameters))
        $deserializedKey.KdfParameters[0] | Should -Be 'SHA512'
    }

    It 'round-trips the secret agreement algorithm and parameters' {
        $deserializedKey.SecretAgreementAlgorithm | Should -Be 'DH'
        $deserializedKey.SecretAgreementPublicKeyLength | Should -Be 2048
        $deserializedKey.SecretAgreementPrivateKeyLength | Should -Be 512
        [DSInternals.Common.ByteArrayExtensions]::ToHex([byte[]] $deserializedKey.SecretAgreementParameters) |
            Should -Be ([DSInternals.Common.ByteArrayExtensions]::ToHex($originalKey.SecretAgreementParameters))
    }
}
