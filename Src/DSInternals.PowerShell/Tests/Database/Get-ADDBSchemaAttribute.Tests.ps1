<#
.SYNOPSIS
    This script contains Pester tests for the Get-ADDBSchemaAttribute cmdlet from the DSInternals PowerShell module.
.DESCRIPTION
    This is only a smoke test, as it uses the built-in initial AD database file, which is present on all Windows Server installations.
    More sophisticated tests are required.
#>
#Requires -Version 5.1
#Requires -Modules DSInternals,@{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

Describe 'Get-ADDBSchemaAttribute' {
    It 'can load schema from the initial ntds.dit' {
        # This test only works on Windows Server
        [string] $initialDBPath = "$env:SystemRoot\System32\ntds.dit"

        if (Test-Path -Path $initialDBPath -PathType Leaf) {
            # Create a copy of the initial ntds.dit file, to avoid possible modification by the tests.
            [string] $workingNTDSCopy = Copy-Item -Path $initialDBPath -Destination TestDrive:\ -PassThru -Force
            
            try {
                # Try to open the DB
                [DSInternals.Common.Schema.AttributeSchema[]] $attributes = Get-ADDBSchemaAttribute -DatabasePath $workingNTDSCopy

                # Check that the schema has been loaded
                $attributes | Measure-Object | Select-Object -ExpandProperty Count | Should -BeGreaterThan 1000 -Because 'the initial schema contains 1500+ attributes on Windows Server 2025.'

                # Check the properties of the Common-Name attribute
                # See https://learn.microsoft.com/en-us/windows/win32/adschema/a-cn
                [DSInternals.Common.Schema.AttributeSchema] $cn = $attributes | Where-Object Name -eq 'cn'
                $cn | Should -Not -BeNullOrEmpty -Because 'the CN attribute must exist in the initial schema.'
                $cn.AttributeOid
                $cn.DerivedColumnName | Should -Be 'ATTm3'
                $cn.ColumnName | Should -Be 'ATTm3'
                $cn.DerivedIndexName | Should -Be 'INDEX_00000003'
                $cn.IndexName | Should -Be 'INDEX_00000003'
                $cn.CommonName | Should -Be 'Common-Name'
                $cn.AttributeOid | should -Be '2.5.4.3'
                $cn.SchemaGuid | should -Be 'bf96793f-0de6-11d0-a285-00aa003049e2'
                $cn.IsInGlobalCatalog | Should -BeTrue
                $cn.RangeLower  | Should -Be 1
                $cn.RangeUpper  | Should -Be 64
                $cn.IsSingleValued | Should -BeTrue
                $cn.LinkId | Should -BeNullOrEmpty
                $cn.Syntax | Should -Be 'UnicodeString'
                $cn.SyntaxOid | Should -Be '2.5.5.12'
                $cn.OmSyntax | Should -Be 64

                # Check the properties of the Member attribute
                # See https://learn.microsoft.com/en-us/windows/win32/adschema/a-member
                [DSInternals.Common.Schema.AttributeSchema] $member = $attributes | Where-Object Name -eq 'member'
                $member | Should -Not -BeNullOrEmpty -Because 'the member attribute must exist in the initial schema.'
                $member.AttributeOid
                $member.DerivedColumnName | Should -BeNullOrEmpty -Because 'the member attribute uses linked-value replication.'
                $member.ColumnName | Should -BeNullOrEmpty -Because 'the member attribute uses linked-value replication.'
                $member.DerivedIndexName | Should -BeNullOrEmpty -Because 'the member attribute uses linked-value replication.'
                $member.IndexName | Should -BeNullOrEmpty -Because 'the member attribute uses linked-value replication.'
                $member.CommonName | Should -Be 'Member'
                $member.AttributeOid | should -Be '2.5.4.31'
                $member.SchemaGuid | should -Be 'bf9679c0-0de6-11d0-a285-00aa003049e2'
                $member.IsInGlobalCatalog | Should -BeTrue
                $member.RangeLower  | Should -BeNullOrEmpty
                $member.RangeUpper  | Should -BeNullOrEmpty
                $member.IsSingleValued | Should -BeFalse
                $member.LinkId | Should -Be 2
                $member.Syntax | Should -Be 'DN'
                $member.SyntaxOid | Should -Be '2.5.5.1'
                $member.OmSyntax | Should -Be 127

            } finally {
                # Delete the temporary file
                Remove-Item -Path $workingNTDSCopy
            }
        } else {
            Set-ItResult -Inconclusive -Because 'the initial DB is not present on client SKUs.'
        }
    }
}
