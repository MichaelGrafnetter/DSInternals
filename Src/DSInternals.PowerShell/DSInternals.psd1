#
# Module manifest for module 'DSInternals'
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'DSInternals.psm1'

# Version number of this module.
ModuleVersion = '2.14'

# ID used to uniquely identify this module
GUID = '766b3ad8-eb78-48e6-84bd-61b31d96b53e'

# Author of this module
Author = 'Michael Grafnetter'

# Company or vendor of this module
CompanyName = 'DSInternals'

# Copyright statement for this module
Copyright = '(c) 2015-2016 Michael Grafnetter. All rights reserved.'

# Description of the functionality provided by this module
Description = 'The DSInternals PowerShell Module exposes several internal features of Active Directory.'

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
FormatsToProcess = 'DSInternals.format.ps1xml'

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('DSInternals.PowerShell.dll')

# Functions to export from this module
# FunctionsToExport = @()

# Cmdlets to export from this module
CmdletsToExport = 'ConvertTo-NTHash', 'ConvertTo-LMHash', 'Set-SamAccountPasswordHash',
			   'ConvertFrom-UnicodePassword', 'ConvertTo-UnicodePassword',
               'ConvertTo-OrgIdHash', 'ConvertFrom-GPPrefPassword',
               'ConvertTo-GPPrefPassword', 'Add-ADDBSidHistory',
               'Set-ADDBPrimaryGroup', 'Get-ADDBDomainController',
               'Set-ADDBDomainController', 'Get-ADDBSchemaAttribute',
               'Remove-ADDBObject', 'Get-ADDBAccount', 'Get-BootKey',
               'Get-ADReplAccount', 'ConvertTo-Hex',
			   'ConvertFrom-ADManagedPasswordBlob',
			   'Get-ADDBBackupKey', 'Get-ADReplBackupKey', 'Save-DPAPIBlob',
			   'Set-ADDBBootKey'

# Variables to export from this module
# VariablesToExport = @()

# Aliases to export from this module
AliasesToExport = 'Set-WinUserPasswordHash', 'Set-ADAccountPasswordHash',
				  'ConvertFrom-UnattendXmlPassword', 'ConvertTo-AADHash',
				  'ConvertTo-MsoPasswordHash', 'Get-ADReplicationAccount',
				  'ConvertFrom-ManagedPasswordBlob', 'Get-SysKey', 'Set-ADDBSysKey'

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'ActiveDirectory', 'Security', 'PSModule'

        # A URL to the license for this module.
        LicenseUri = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/LICENSE.md'

        # A URL to the main website for this project.
        ProjectUri = 'https://www.dsinternals.com/en'

        # A URL to an icon representing this module.
        IconUri = 'https://www.dsinternals.com/wp-content/uploads/ad.png'

        # ReleaseNotes of this module
        ReleaseNotes = @"
- Added support for Windows Server 2016 ntds.dit encryption.
- Added support for replication with renamed domains.
- Added support for reading security descriptors (ACLs) from both ntds.dit files and DRS-R.
- Added support for the AdminCount attribute.
"@
    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}