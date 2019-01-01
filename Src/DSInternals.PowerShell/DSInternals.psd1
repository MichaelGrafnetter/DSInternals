#
# Module manifest for module 'DSInternals'
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'DSInternals.Bootstrap.psm1'

# Version number of this module.
ModuleVersion = '3.1'

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
The DSInternals PowerShell Module exposes several internal features of Active Directory.

DISCLAIMER: Features exposed through this module are not supported by Microsoft and it is therefore not intended to be used in production environments. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.
"@

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module
DotNetFrameworkVersion = '4.5.1'

# Minimum version of the common language runtime (CLR) required by this module
CLRVersion = '4.0'

# Processor architecture (None, X86, Amd64) required by this module
ProcessorArchitecture = 'None'

# Modules that must be imported into the global environment prior to importing this module
# RequiredModules = @()

# Assemblies that must be loaded prior to importing this module
# RequiredAssemblies = @()

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

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

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

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
        Tags = 'ActiveDirectory', 'Security', 'SAM', 'LSA', 'PSModule'

        # A URL to the license for this module.
        LicenseUri = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/LICENSE.md'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/MichaelGrafnetter/DSInternals'

        # A URL to an icon representing this module.
        IconUri = 'https://www.dsinternals.com/wp-content/uploads/ad.png'

        # ReleaseNotes of this module
        ReleaseNotes = @"
- Added the New-ADDBRestoreFromMediaScript cmdlet to aid with file-level DC recovery process.
- Added the Get-LSAPolicyInformation and Set-LSAPolicyInformation cmdlets that can be used to retrieve and change domain-related LSA Policies.
- Extended the information returned by the The Get-ADDBDomainController cmdlet.
- Added MAML documentation for Get-Help.
- Path to the DSInternals.psd1 file now does not need to be specified when loading the module from a non-default location.
"@
    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}