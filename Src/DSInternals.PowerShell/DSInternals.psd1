#
# Module manifest for module 'DSInternals'
#

@{

# Script module file associated with this manifest.
RootModule = 'DSInternals.Bootstrap.psm1'

# Version number of this module.
ModuleVersion = '4.7'

# Supported PSEditions
# CompatiblePSEditions = 'Desktop'

# ID used to uniquely identify this module
GUID = '766b3ad8-eb78-48e6-84bd-61b31d96b53e'

# Author of this module
Author = 'Michael Grafnetter'

# Company or vendor of this module
CompanyName = 'DSInternals'

# Copyright statement for this module
Copyright = '(c) 2015-2021 Michael Grafnetter. All rights reserved.'

# Description of the functionality provided by this module
Description = @"
The DSInternals PowerShell Module exposes several internal features of Active Directory and Azure Active Directory. These include FIDO2 and NGC key auditing, offline ntds.dit file manipulation, password auditing, DC recovery from IFM backups and password hash calculation.

DISCLAIMER: Features exposed through this module are not supported by Microsoft and it is therefore not intended to be used in production environments. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.
"@

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Minimum version of Microsoft .NET Framework required by this module
DotNetFrameworkVersion = '4.7.2' # This requirement is not enforced by older versions of PS.

# Minimum version of the common language runtime (CLR) required by this module
CLRVersion = '4.0.30319.42000' # Corresponds to .NET Framework 4.6 and later

# Processor architecture (None, X86, Amd64) required by this module
ProcessorArchitecture = 'None'

# Type files (.ps1xml) to be loaded when importing this module
TypesToProcess = @('DSInternals.types.ps1xml')

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = 'Views\DSInternals.AzureADUser.format.ps1xml',
                   'Views\DSInternals.Hash.format.ps1xml',
                   'Views\DSInternals.RoamedCredential.format.ps1xml',
                   'Views\DSInternals.Kerberos.format.ps1xml',
                   'Views\DSInternals.KeyCredential.format.ps1xml',
                   'Views\DSInternals.FidoKeyMaterial.format.ps1xml',
                   'Views\DSInternals.DSAccount.format.ps1xml',
                   'Views\DSInternals.DSAccount.ExportViews.format.ps1xml',
                   'Views\DSInternals.PasswordQualityTestResult.format.ps1xml',
                   'Views\DSInternals.KdsRootKey.format.ps1xml',
                   'Views\DSInternals.SamDomainPasswordInformation.format.ps1xml',
                   'Views\DSInternals.LsaPolicyInformation.format.ps1xml'

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('DSInternals.PowerShell.dll')

# Functions to export from this module
FunctionsToExport = @()

# Cmdlets to export from this module
CmdletsToExport = 'ConvertTo-NTHash', 'ConvertTo-LMHash', 'Set-SamAccountPasswordHash',
                  'ConvertFrom-UnicodePassword', 'ConvertTo-UnicodePassword',
                  'ConvertTo-OrgIdHash', 'ConvertFrom-GPPrefPassword',
                  'ConvertTo-GPPrefPassword', 'Add-ADDBSidHistory',
                  'Set-ADDBPrimaryGroup', 'Get-ADDBDomainController',
                  'Set-ADDBDomainController', 'Get-ADDBSchemaAttribute',
                  'Remove-ADDBObject', 'Get-ADDBAccount', 'Get-BootKey',
                  'Get-ADReplAccount', 'ConvertTo-Hex', 'ConvertTo-KerberosKey',
                  'ConvertFrom-ADManagedPasswordBlob',
                  'Get-ADDBBackupKey', 'Get-ADReplBackupKey', 'Save-DPAPIBlob',
                  'Set-ADDBBootKey', 'Test-PasswordQuality',
                  'Get-ADDBKdsRootKey', 'Get-SamPasswordPolicy', 'Get-ADSIAccount',
                  'Enable-ADDBAccount', 'Disable-ADDBAccount', 'Get-ADKeyCredential',
                  'Set-ADDBAccountPassword', 'Set-ADDBAccountPasswordHash', 'Get-LsaPolicyInformation',
                  'Set-LSAPolicyInformation', 'New-ADDBRestoreFromMediaScript','Get-LsaBackupKey',
                  'Add-ADReplNgcKey', 'Get-AzureADUserEx', 'Set-AzureADUserEx'

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module
AliasesToExport = 'Set-WinUserPasswordHash', 'Set-ADAccountPasswordHash',
                  'ConvertFrom-UnattendXmlPassword', 'ConvertTo-AADHash',
                  'ConvertTo-MsoPasswordHash', 'Get-ADReplicationAccount',
                  'ConvertFrom-ManagedPasswordBlob', 'Get-SysKey', 'Set-ADDBSysKey',
                  'New-NTHashSet', 'Test-ADPasswordQuality',
                  'Test-ADDBPasswordQuality', 'Test-ADReplPasswordQuality',
                  'Get-ADPasswordPolicy', 'Get-ADDefaultPasswordPolicy', 'Get-KeyCredential',
                  'Get-KeyCredentialLink', 'Get-ADKeyCredentialLink', 'Get-LsaPolicy',
                  'Set-LsaPolicy', 'Get-SystemKey', 'Write-ADReplNgcKey', 'Write-ADNgcKey',
                  'Add-ADNgcKey', 'New-ADKeyCredential', 'New-ADKeyCredentialLink',
                  'New-ADNgcKey'

# List of assemblies that must be loaded prior to importing this module
RequiredAssemblies = @('DSInternals.Common.dll')

# List of all files packaged with this module
FileList = 'AutoMapper.dll',
           'CBOR.dll',
           'DSInternals.DataStore.dll',
           'DSInternals.Replication.dll',
           'DSInternals.Replication.Model.dll',
           'DSInternals.SAM.dll',
           'Esent.Interop.dll',
           'Esent.Isam.dll',
           'License.txt',
           'NDceRpc.Microsoft.dll',
           'Newtonsoft.Json.dll',
           'Numbers.dll',
           'amd64\DSInternals.Replication.Interop.dll',
           'x86\DSInternals.Replication.Interop.dll',
           'en-US\about_DSInternals.help.txt',
           'en-US\DSInternals.PowerShell.dll-Help.xml'

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'ActiveDirectory', 'AzureAD', 'Security', 'SAM', 'LSA', 'PSModule', 'Windows', 'FIDO'

        # A URL to the license for this module.
        LicenseUri = 'https://github.com/MichaelGrafnetter/DSInternals/blob/master/Src/DSInternals.PowerShell/License.txt'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/MichaelGrafnetter/DSInternals'

        # A URL to an icon representing this module.
        IconUri = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/Src/Icons/module_black.png'

        # ReleaseNotes of this module
        ReleaseNotes = @"
- The Test-PasswordQuality cmdlet can now detect kerberoastable user accounts. Its performance has also been slightly improved.
- Objects returned by the Get-ADDBAccount, Get-ADReplAccount, and Get-ADSIAccount cmdlets now have the SupportedEncryptionTypes property.
"@
    } # End of PSData hashtable

} # End of PrivateData hashtable

}
