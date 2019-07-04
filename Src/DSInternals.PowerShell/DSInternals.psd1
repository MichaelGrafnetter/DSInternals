#
# Module manifest for module 'DSInternals'
#

@{

# Script module file associated with this manifest.
RootModule = 'DSInternals.Bootstrap.psm1'

# Version number of this module.
ModuleVersion = '3.6'

# ID used to uniquely identify this module
GUID = '766b3ad8-eb78-48e6-84bd-61b31d96b53e'

# Author of this module
Author = 'Michael Grafnetter'

# Company or vendor of this module
CompanyName = 'DSInternals'

# Copyright statement for this module
Copyright = '(c) 2015-2019 Michael Grafnetter. All rights reserved.'

# Description of the functionality provided by this module
Description = @"
The DSInternals PowerShell Module exposes several internal features of Active Directory. These include offline ntds.dit file manipulation, password auditing, DC recovery from IFM backups and password hash calculation.

DISCLAIMER: Features exposed through this module are not supported by Microsoft and it is therefore not intended to be used in production environments. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.
"@

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Minimum version of Microsoft .NET Framework required by this module
DotNetFrameworkVersion = '4.5.1' # This requirement is not enforced by older versions of PS.

# Minimum version of the common language runtime (CLR) required by this module
CLRVersion = '4.0.30319.18408' # Corresponds to .NET Framework 4.5.1

# Processor architecture (None, X86, Amd64) required by this module
ProcessorArchitecture = 'None'

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = 'Views\DSInternals.Hash.format.ps1xml',
                   'Views\DSInternals.RoamedCredential.format.ps1xml',
                   'Views\DSInternals.Kerberos.format.ps1xml',
                   'Views\DSInternals.KeyCredential.format.ps1xml',
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
                  'Set-LSAPolicyInformation', 'New-ADDBRestoreFromMediaScript','Get-LsaBackupKey'

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
                  'Set-LsaPolicy', 'Get-SystemKey'

# List of all files packaged with this module
FileList = 'AutoMapper.dll',
           'DSInternals.Common.dll',
           'DSInternals.DataStore.dll',
           'DSInternals.Replication.dll',
           'DSInternals.Replication.Model.dll',
           'DSInternals.SAM.dll',
           'Esent.Interop.dll',
           'Esent.Isam.dll',
           'NDceRpc.Microsoft.dll',
           'amd64\DSInternals.Replication.Interop.dll',
           'x86\DSInternals.Replication.Interop.dll'

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'ActiveDirectory', 'Security', 'SAM', 'LSA', 'PSModule', 'Windows'

        # A URL to the license for this module.
        LicenseUri = 'https://github.com/MichaelGrafnetter/DSInternals/blob/master/LICENSE.md'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/MichaelGrafnetter/DSInternals'

        # A URL to an icon representing this module.
        IconUri = 'https://www.dsinternals.com/wp-content/uploads/ad.png'

        # ReleaseNotes of this module
        ReleaseNotes = @"
- Fixed a bug in the Test-PasswordQuality cmdlet.
- Renamed the -DBPath parameter of database cmdlets to -DatabasePath.
- Improved Get-Help documentation.
"@
    } # End of PSData hashtable

} # End of PrivateData hashtable

}
