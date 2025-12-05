#
# Module manifest for module 'DSInternals'
#

@{

# Script module file associated with this manifest.
RootModule = 'DSInternals.Bootstrap.psm1'

# Version number of this module.
ModuleVersion = '6.2'

# Supported PSEditions
CompatiblePSEditions = @('Desktop', 'Core')

# ID used to uniquely identify this module
GUID = '766b3ad8-eb78-48e6-84bd-61b31d96b53e'

# Author of this module
Author = 'Michael Grafnetter'

# Company or vendor of this module
CompanyName = 'DSInternals'

# Copyright statement for this module
Copyright = '(c) 2015-2025 Michael Grafnetter. All rights reserved.'

# Description of the functionality provided by this module
Description = @"
The DSInternals PowerShell Module exposes several internal features of Active Directory and Azure Active Directory. These include FIDO2 and NGC key auditing, offline ntds.dit file manipulation, password auditing, DC recovery from IFM backups and password hash calculation.

DISCLAIMER: Features exposed through this module are not supported by Microsoft and it is therefore not intended to be used in production environments. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.
"@

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '5.1'

# Processor architecture (None, X86, Amd64, ARM64) required by this module
ProcessorArchitecture = 'None'

# Type files (.ps1xml) to be loaded when importing this module
TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = @(
    'Views\DSInternals.AzureADUser.format.ps1xml',
    'Views\DSInternals.Hash.format.ps1xml',
    'Views\DSInternals.RoamedCredential.format.ps1xml',
    'Views\DSInternals.Kerberos.format.ps1xml',
    'Views\DSInternals.KeyCredential.format.ps1xml',
    'Views\DSInternals.FidoKeyMaterial.format.ps1xml',
    'Views\DSInternals.BitLockerRecoveryInformation.format.ps1xml',
    'Views\DSInternals.LapsPasswordInformation.format.ps1xml',
    'Views\DSInternals.DSAccount.ExportViews.format.ps1xml',
    'Views\DSInternals.DSAccount.format.ps1xml',
    'Views\DSInternals.DSUser.format.ps1xml',
    'Views\DSInternals.DSComputer.format.ps1xml',
    'Views\DSInternals.GroupManagedServiceAccount.format.ps1xml',
    'Views\DSInternals.PasswordQualityTestResult.format.ps1xml',
    'Views\DSInternals.KdsRootKey.format.ps1xml',
    'Views\DSInternals.SamDomainPasswordInformation.format.ps1xml',
    'Views\DSInternals.LsaPolicyInformation.format.ps1xml',
    'Views\DSInternals.DnsResourceRecord.format.ps1xml',
    'Views\DSInternals.TrustedDomain.format.ps1xml'
)

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @()

# Functions to export from this module
FunctionsToExport = @()

# Cmdlets to export from this module
CmdletsToExport = @(
    'ConvertTo-NTHash',
    'ConvertTo-LMHash',
    'Set-SamAccountPasswordHash',
    'ConvertFrom-UnicodePassword',
    'ConvertTo-UnicodePassword',
    'ConvertTo-OrgIdHash',
    'ConvertFrom-GPPrefPassword',
    'ConvertTo-GPPrefPassword',
    'Set-ADDBPrimaryGroup',
    'Get-ADDBDomainController',
    'Set-ADDBDomainController',
    'Get-ADDBSchemaAttribute',
    'Remove-ADDBObject',
    'Get-ADDBAccount',
    'Get-BootKey',
    'Get-ADReplAccount',
    'ConvertTo-Hex',
    'ConvertTo-KerberosKey',
    'ConvertFrom-ADManagedPasswordBlob',
    'Get-ADReplKdsRootKey',
    'Get-ADDBBackupKey',
    'Get-ADReplBackupKey',
    'Save-DPAPIBlob',
    'Set-ADDBBootKey',
    'Test-PasswordQuality',
    'Get-ADDBServiceAccount',
    'Get-ADDBBitLockerRecoveryInformation',
    'Get-ADDBKdsRootKey',
    'Get-SamPasswordPolicy',
    'Get-ADSIAccount',
    'Enable-ADDBAccount',
    'Disable-ADDBAccount',
    'Get-ADKeyCredential',
    'Set-ADDBAccountPassword',
    'Set-ADDBAccountPasswordHash',
    'Get-LsaPolicyInformation',
    'Set-LSAPolicyInformation',
    'New-ADDBRestoreFromMediaScript',
    'Get-LsaBackupKey',
    'Add-ADReplNgcKey',
    'Get-AzureADUserEx',
    'Set-AzureADUserEx',
    'Unlock-ADDBAccount',
    'Get-ADDBDnsResourceRecord',
    'Get-ADDBDnsZone',
    'Set-ADDBAccountControl',
    'Get-ADDBTrust'
    # Intentionally excluded: 'Add-ADDBSidHistory'
)

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module
AliasesToExport = @(
    'Set-WinUserPasswordHash',
    'Set-ADAccountPasswordHash',
    'ConvertFrom-UnattendXmlPassword',
    'ConvertTo-AADHash',
    'ConvertTo-MsoPasswordHash',
    'Get-ADReplicationAccount',
    'ConvertFrom-ManagedPasswordBlob',
    'Get-SysKey',
    'Set-ADDBSysKey',
    'New-NTHashSet',
    'Test-ADPasswordQuality',
    'Test-ADDBPasswordQuality',
    'Test-ADReplPasswordQuality',
    'Get-ADPasswordPolicy',
    'Get-ADDefaultPasswordPolicy',
    'Get-KeyCredential',
    'Get-KeyCredentialLink',
    'Get-ADKeyCredentialLink',
    'Get-LsaPolicy',
    'Set-LsaPolicy',
    'Get-SystemKey',
    'Write-ADReplNgcKey',
    'Write-ADNgcKey',
    'Add-ADNgcKey',
    'New-ADKeyCredential',
    'New-ADKeyCredentialLink',
    'New-ADNgcKey',
    'Get-ADDBGroupManagedServiceAccount',
    'Get-ADDBBitLockerRecoveryInfo',
    'Get-ADDBBitLockerKeyProtector',
    'Get-ADDBBitLockerRecoveryKey',
    'Get-ADDBBitLockerKey',
    'Get-ADDBBitLockerRecoveryPassword',
    'Get-ADDBFVERecoveryKey',
    'Get-ADDBFVERecoveryPassword',
    'Get-ADDBDelegatedManagedServiceAccount',
    'Get-ADDBFVERecoveryInformation',
    'Get-ADDBFVERecoveryInfo',
    'Get-ADDBDnsRecord'
)

# List of assemblies that must be loaded prior to importing this module
RequiredAssemblies = @()

# List of all files packaged with this module
FileList = @(
    'DSInternals.types.ps1xml',
    'License.txt',
    'Integrity.Tests.ps1',
    'en-US\about_DSInternals.help.txt',
    'en-US\DSInternals.PowerShell.dll-Help.xml',
    'net48\DSInternals.Common.dll',
    'net48\DSInternals.ADSI.dll',
    'net48\DSInternals.DataStore.dll',
    'net48\DSInternals.Replication.dll',
    'net48\DSInternals.Replication.Model.dll',
    'net48\DSInternals.SAM.dll',
    'net48\amd64\DSInternals.Replication.Interop.dll',
    'net48\arm64\DSInternals.Replication.Interop.dll',
    'net48\x86\DSInternals.Replication.Interop.dll',
    'net48\Esent.Interop.dll',
    'net48\Esent.Isam.dll',
    'net48\System.Formats.Cbor.dll',
    'net48\System.Formats.Asn1.dll',
    'net48\System.Buffers.dll',
    'net48\System.Memory.dll',
    'net48\System.Numerics.Vectors.dll',
    'net48\System.Runtime.CompilerServices.Unsafe.dll',
    'net48\System.Text.Json.dll',
    'net48\System.Text.Encodings.Web.dll',
    'net48\System.IO.Pipelines.dll',
    'net48\System.Threading.Tasks.Extensions.dll',
    'net48\Microsoft.Bcl.AsyncInterfaces.dll',
    'net48\Microsoft.Bcl.HashCode.dll',
    'net8.0-windows\DSInternals.DataStore.dll',
    'net8.0-windows\DSInternals.Replication.dll',
    'net8.0-windows\DSInternals.Replication.Model.dll',
    'net8.0-windows\DSInternals.Common.dll',
    'net8.0-windows\DSInternals.ADSI.dll',
    'net8.0-windows\DSInternals.SAM.dll',
    'net8.0-windows\amd64\DSInternals.Replication.Interop.dll',
    'net8.0-windows\arm64\DSInternals.Replication.Interop.dll',
    'net8.0-windows\x86\DSInternals.Replication.Interop.dll',
    'net8.0-windows\Esent.Interop.dll',
    'net8.0-windows\Esent.Isam.dll',
    'net8.0-windows\System.Formats.Cbor.dll'
)

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('ActiveDirectory', 'AzureAD', 'Security', 'SAM', 'LSA', 'DNS', 'BitLocker', 'LAPS', 'FIDO', 'NTDS', 'PSModule', 'Windows', 'PSEdition_Desktop', 'PSEdition_Core')

        # A URL to the license for this module.
        LicenseUri = 'https://github.com/MichaelGrafnetter/DSInternals/blob/master/Src/DSInternals.PowerShell/License.txt'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/MichaelGrafnetter/DSInternals'

        # A URL to an icon representing this module.
        IconUri = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/Src/Icons/module_black.png'

        # ReleaseNotes of this module
        ReleaseNotes = @"
- The New-ADDBRestoreFromMediaScript cmdlet should no longer be throwing the NullReferenceException.
- Disabled DES_CBC_MD5 Kerberos key derivation support due to recent Windows API changes.
- Removed the broken -Protocol parameter from replication cmdlets.
- Due to unexpected delays in code signing certificate renewal, this release is not digitally signed.
"@
    } # End of PSData hashtable

} # End of PrivateData hashtable

}
