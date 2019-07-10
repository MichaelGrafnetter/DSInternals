---
Module Name: DSInternals
Module Guid: 766b3ad8-eb78-48e6-84bd-61b31d96b53e
Download Help Link: 
Help Version: 1.0
Locale: en-US
---

# DSInternals PowerShell Module

## Description
The DSInternals PowerShell Module exposes several internal features of Active Directory. These include offline ntds.dit file manipulation, password auditing, DC recovery from IFM backups and password hash calculation.

## Cmdlets for Offline Active Directory Operations

### [Get-ADDBAccount](Get-ADDBAccount.md#get-addbaccount)
Reads one or more accounts from a ntds.dit file, including secret attributes.

### [Enable-ADDBAccount](Enable-ADDBAccount.md#enable-addbaccount)
Enables an Active Directory account in an offline ntds.dit file.

### [Disable-ADDBAccount](Disable-ADDBAccount.md#disable-addbaccount)
Disables an Active Directory account in an offline ntds.dit file.

### [Add-ADDBSidHistory](Add-ADDBSidHistory.md#add-addbsidhistory)
Adds one or more values to the sIDHistory attribute of an object in a ntds.dit file.

### [Set-ADDBAccountPassword](Set-ADDBAccountPassword.md#set-addbaccountpassword)
Sets the password for a user, computer, or service account stored in a ntds.dit file.

### [Set-ADDBAccountPasswordHash](Set-ADDBAccountPasswordHash.md#set-addbaccountpasswordhash)
Sets the password hash for a user, computer, or service account stored in a ntds.dit file.

### [Set-ADDBPrimaryGroup](Set-ADDBPrimaryGroup.md#set-addbprimarygroup)
Modifies the primaryGroupId attribute of an object in a ntds.dit file.

### [Get-ADDBBackupKey](Get-ADDBBackupKey.md#get-addbbackupkey)
Reads the DPAPI backup keys from a ntds.dit file.

### [Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md#get-addbkdsrootkey)
Reads KDS Root Keys from a ntds.dit. file. Can be used to aid DPAPI-NG decryption, e.g. SID-protected PFX files.

### [Get-ADDBDomainController](Get-ADDBDomainController.md#get-addbdomaincontroller)
Reads information about the originating DC from a ntds.dit file, including domain name, domain SID, DC name and DC site.

### [Set-ADDBDomainController](Set-ADDBDomainController.md#set-addbdomaincontroller)
Writes information about the DC to a ntds.dit file, including the highest committed USN and database epoch.

### [Get-ADDBSchemaAttribute](Get-ADDBSchemaAttribute.md#get-addbschemaattribute)
Reads AD schema from a ntds.dit file, including datatable column names.

### [Get-BootKey](Get-BootKey.md#get-bootkey)
Reads the Boot Key (AKA SysKey or System Key) from an online or offline SYSTEM registry hive.

### [Set-ADDBBootKey](Set-ADDBBootKey.md#set-addbbootkey)
Re-encrypts a ntds.dit file with a new BootKey/SysKey. Highly experimental!

### [Remove-ADDBObject](Remove-ADDBObject.md#remove-addbobject)
Physically removes specified object from a ntds.dit file, making it semantically inconsistent. Highly experimental!

## Cmdlets for Online Active Directory Operations

### [Get-ADReplAccount](Get-ADReplAccount.md#get-adreplaccount)
Reads one or more accounts through the MS-DRSR protocol, including secret attributes.

### [Get-ADReplBackupKey](Get-ADReplBackupKey.md#get-adreplbackupkey)
Reads the DPAPI backup keys through the MS-DRSR protocol.

### [Get-SamPasswordPolicy](Get-SamPasswordPolicy.md#get-sampasswordpolicy)
Queries Active Directory for the default password policy.

### [Set-SamAccountPasswordHash](Set-SamAccountPasswordHash.md#set-samaccountpasswordhash)
Sets NT and LM hashes of an Active Directory or local account through the MS-SAMR protocol.

### [Get-ADSIAccount](Get-ADSIAccount.md#get-adsiaccount)
Gets all Active Directory user accounts from a given domain controller using ADSI. Typically used for Credential Roaming data retrieval through LDAP.

### [Get-LsaBackupKey](Get-LsaBackupKey.md#get-lsabackupkey)
Reads the DPAPI backup keys from a domain controller through the LSARPC protocol.

### [Get-LsaPolicyInformation](Get-LsaPolicyInformation.md#get-lsapolicyinformation)
Retrieves AD-related information from the Local Security Authority Policy of the local computer or a remote one.

### [Set-LsaPolicyInformation](Set-LsaPolicyInformation.md#set-lsapolicyinformation)
Configures AD-related Local Security Authority Policies of the local computer or a remote one.

## Password Hash Export Formats

The output of the [Get-ADDBAccount](Get-ADDBAccount.md#get-addbaccount) and [Get-ADReplAccount](Get-ADReplAccount.md#get-adreplaccount) cmdlets can be formatted using the following custom [Views](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/format-custom) to support different password cracking tools. ASCII file encoding is strongly recommended.

### Hashcat

- **HashcatNT** - NT hashes in Hashcat's format.
- **HashcatLM** - LM hashes in Hashcat's format.
- **HashcatNTHistory** - NT hashes, including historical ones, in Hashcat's format.
- **HashcatLMHistory** - LM hashes, including historical ones, in Hashcat's format.

### John the Ripper

- **JohnNT** - NT hashes in the format supported by John the Ripper.
- **JohnLM** - LM hashes in the format supported by John the Ripper.
- **JohnNTHistory** - NT hashes, including historical ones, in the format supported by John the Ripper.
- **JohnLMHistory** - LM hashes, including historical ones, in the format supported by John the Ripper.

### Ophcrack

- **Ophcrack** - NT and LM hashes in Ophcrack's format.

### Other Formats

- **PWDump** - NT and LM hashes in the pwdump format that is supported various password cracking tools, e.g. *ElcomSoft Distributed Password Recovery*, *rcracki-mt* or *John the Ripper*.
- **PWDumpHistory** - NT and LM hashes, including historical ones, in the pwdump format.
- **NTHash** - NT hashes only, without account names.
- **LMHash** - LM hashes only, without account names.
- **NTHashHistory** - NT hashes, including historical ones, without account names.
- **LMHashHistory** - LM hashes, including historical ones, without account names.

### Example 1

```powershell
Get-ADDBAccount -All -DatabasePath ntds.dit -BootKey $key | Format-Custom -View PwDump | Out-File -FilePath users.pwdump -Encoding ascii
```

### Example 2

```powershell
Get-ADReplAccount -All -NamingContext 'DC=adatum,DC=com' -Server LON-DC1 | Format-Custom -View JohnNT | Out-File -FilePath users.txt -Encoding ascii
```

## Cmdlets for Password Hash Calculation

### [ConvertTo-KerberosKey](ConvertTo-KerberosKey.md#convertto-kerberoskey)
Computes Kerberos keys from a given password using Kerberos version 5 Key Derivation Functions.

### [ConvertTo-NTHash](ConvertTo-NTHash.md#convertto-nthash)
Calculates NT hash of a given password.

### [ConvertTo-LMHash](ConvertTo-LMHash.md#convertto-lmhash)
Calculates LM hash of a given password.

### [ConvertTo-OrgIdHash](ConvertTo-OrgIdHash.md#convertto-orgidhash)
Calculates OrgId hash of a given password. Used by Azure Active Directory Connect.

## Cmdlets for Credential Decryption

### [Save-DPAPIBlob](Save-DPAPIBlob.md#save-dpapiblob)
Saves DPAPI and Credential Roaming data returned by the [Get-ADReplBackupKey](Get-ADReplBackupKey.md#get-adreplbackupkey), [Get-ADDBBackupKey](Get-ADDBBackupKey.md#get-addbbackupkey), [Get-ADReplAccount](Get-ADReplAccount.md#get-adreplaccount), [Get-ADDBAccount](Get-ADDBAccount.md#get-addbaccount) and [Get-ADSIAccount](Get-ADSIAccount.md#get-adsiaccount) cmdlets to files for further processing.

### [ConvertFrom-ADManagedPasswordBlob](ConvertFrom-ADManagedPasswordBlob.md#convertfrom-admanagedpasswordblob)
Decodes the value of the msDS-ManagedPassword attribute of a Group Managed Service Account.

### [Get-ADKeyCredential](Get-ADKeyCredential.md#get-adkeycredential)
Creates an object representing Windows Hello for Business or FIDO credentials from its binary representation or an X.509 certificate.

### [ConvertFrom-GPPrefPassword](ConvertFrom-GPPrefPassword.md#convertfrom-gpprefpassword)
Decodes a password from the format used by Group Policy Preferences.

### [ConvertTo-GPPrefPassword](ConvertTo-GPPrefPassword.md#convertto-gpprefpassword)
Converts a password to the format used by Group Policy Preferences.

### [ConvertFrom-UnicodePassword](ConvertFrom-UnicodePassword.md#convertfrom-unicodepassword)
Decodes a password from the format used in unattend.xml files.

### [ConvertTo-UnicodePassword](ConvertTo-UnicodePassword.md#convertto-unicodepassword)
Converts a password to the format used in unattend.xml or *.ldif files.

## Miscellaneous Cmdlets

### [New-ADDBRestoreFromMediaScript](New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript)
Generates a PowerShell script that can be used to restore a domain controller from an IFM-equivalent backup (i.e. ntds.dit + SYSVOL).

### [Test-PasswordQuality](Test-PasswordQuality.md#test-passwordquality)
Performs AD audit, including checks for weak, duplicate, default and empty passwords. Accepts input from the [Get-ADReplAccount](Get-ADReplAccount.md#get-adreplaccount) and [Get-ADDBAccount](Get-ADDBAccount.md#get-addbaccount) cmdlets.

### [ConvertTo-Hex](ConvertTo-Hex.md#convertto-hex)
Helper cmdlet that converts binary input to a hexadecimal string.
