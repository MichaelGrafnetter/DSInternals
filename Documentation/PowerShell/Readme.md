---
Module Name: DSInternals
Module Guid: 766b3ad8-eb78-48e6-84bd-61b31d96b53e
Download Help Link: 
Help Version: 1.0
Locale: en-US
---

# DSInternals Module
## Description
The DSInternals PowerShell Module exposes several internal and undocumented features of Active Directory.

## DSInternals Cmdlets
### [Add-ADDBSidHistory](Add-ADDBSidHistory.md)
Adds one or more values to the sIDHistory attribute of an object in a ntds.dit file.

### [ConvertFrom-ADManagedPasswordBlob](ConvertFrom-ADManagedPasswordBlob.md)
Decodes the value of the msDS-ManagedPassword attribute of a Group Managed Service Account.

### [ConvertFrom-GPPrefPassword](ConvertFrom-GPPrefPassword.md)
Decodes a password from the format used by Group Policy Preferences.

### [ConvertFrom-UnicodePassword](ConvertFrom-UnicodePassword.md)
Decodes a password from the format used in unattend.xml files.

### [ConvertTo-GPPrefPassword](ConvertTo-GPPrefPassword.md)
Converts a password to the format used by Group Policy Preferences.

### [ConvertTo-Hex](ConvertTo-Hex.md)
Helper cmdlet that converts binary input to hexadecimal string.

### [ConvertTo-KerberosKey](ConvertTo-KerberosKey.md)
{{Fill in the Synopsis}}

### [ConvertTo-LMHash](ConvertTo-LMHash.md)
Calculates LM hash of a given password.

### [ConvertTo-NTHash](ConvertTo-NTHash.md)
Calculates NT hash of a given password.

### [ConvertTo-OrgIdHash](ConvertTo-OrgIdHash.md)
Calculates OrgId hash of a given password. Used by Azure Active Directory Sync.

### [ConvertTo-UnicodePassword](ConvertTo-UnicodePassword.md)
Converts a password to the format used in unattend.xml or *.ldif files.

### [Disable-ADDBAccount](Disable-ADDBAccount.md)
Disables an Active Directory account in an offline ntds.dit file.

### [Enable-ADDBAccount](Enable-ADDBAccount.md)
Enables an Active Directory account in an offline ntds.dit file.

### [Get-ADDBAccount](Get-ADDBAccount.md)
Reads one or more accounts from a ntds.dit file, including secret attributes.

### [Get-ADDBBackupKey](Get-ADDBBackupKey.md)
Reads the DPAPI backup keys from a ntds.dit file.

### [Get-ADDBDomainController](Get-ADDBDomainController.md)
Reads information about the originating DC from a ntds.dit file, including domain name, domain SID, DC name and DC site.

### [Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
Reads KDS Root Keys from a ntds.dit. file. Can be used to aid DPAPI-NG decryption, e.g. SID-protected PFX files.

### [Get-ADDBSchemaAttribute](Get-ADDBSchemaAttribute.md)
Reads AD schema from a ntds.dit file, including datatable column names.

### [Get-ADKeyCredential](Get-ADKeyCredential.md)
{{Fill in the Synopsis}}

### [Get-ADReplAccount](Get-ADReplAccount.md)
Reads one or more accounts through the DRSR protocol, including secret attributes.

### [Get-ADReplBackupKey](Get-ADReplBackupKey.md)
Reads the DPAPI backup keys through the DRSR protocol.

### [Get-ADSIAccount](Get-ADSIAccount.md)
{{Fill in the Synopsis}}

### [Get-BootKey](Get-BootKey.md)
Reads the BootKey/SysKey from an offline SYSTEM registry hive.

### [Get-LsaPolicyInformation](Get-LsaPolicyInformation.md)
{{Fill in the Synopsis}}

### [Get-SamPasswordPolicy](Get-SamPasswordPolicy.md)
Queries Active Directory for the default password policy.

### [Remove-ADDBObject](Remove-ADDBObject.md)
Physically removes specified object from a ntds.dit file, making it semantically inconsistent. Highly experimental!

### [Save-DPAPIBlob](Save-DPAPIBlob.md)
Saves the output of the Get-ADReplBackupKey and Get-ADDBBackupKey cmdlets to a file.

### [Set-ADDBAccountPassword](Set-ADDBAccountPassword.md)
{{Fill in the Synopsis}}

### [Set-ADDBAccountPasswordHash](Set-ADDBAccountPasswordHash.md)
{{Fill in the Synopsis}}

### [Set-ADDBBootKey](Set-ADDBBootKey.md)
Re-encrypts a ntds.dit with a new BootKey. Highly experimental!

### [Set-ADDBDomainController](Set-ADDBDomainController.md)
Writes information about the DC to a ntds.dit file, including the highest commited USN and database epoch.

### [Set-ADDBPrimaryGroup](Set-ADDBPrimaryGroup.md)
Modifies the primaryGroupId attribute of an object to a ntds.dit file.

### [Set-LsaPolicyInformation](Set-LsaPolicyInformation.md)
{{Fill in the Synopsis}}

### [Set-SamAccountPasswordHash](Set-SamAccountPasswordHash.md)
Sets NT and LM hashes of an account through the SAMR protocol.

### [Test-PasswordQuality](Test-PasswordQuality.md)
Performs AD audit, including checks for weak, duplicate, default and empty passwords.

