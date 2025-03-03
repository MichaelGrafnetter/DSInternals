![DSInternals Logo](DSInternals-Dark.png#gh-light-mode-only)
![DSInternals Logo](DSInternals-Light.png#gh-dark-mode-only)

# Directory Services Internals<br/>PowerShell Module and Framework

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](../LICENSE.md)
[![PowerShell 3 | 4 | 5](https://img.shields.io/badge/PowerShell-3%20|%204%20|%205-0000FF.svg?logo=PowerShell)](#)
[![Windows Server 2008 R2 | 2012 R2 | 2016 | 2019 | 2022 | 2025](https://img.shields.io/badge/Windows%20Server-2008%20R2%20|%202012%20R2%20|%202016%20|%202019%20|%202022|%202025-007bb8.svg?logo=Windows%2011)](#)
[![.NET Framework 4.7.2+](https://img.shields.io/badge/Framework-4.7.2%2B-007FFF.svg?logo=.net)](#)
[![Architecture x64 | x86 | arm64](https://img.shields.io/badge/Architecture-x64%20|%20x86%20|%20ARM64-0071c5.svg?logo=Amazon%20EC2)](#)

## Introduction

The DSInternals project consists of these two parts:
- The [DSInternals Framework](https://www.nuget.org/profiles/DSInternals) exposes several internal features of *Active Directory* and can be used from any .NET application. The codebase has already been integrated into several 3<sup>rd</sup> party commercial products that use it in scenarios like Active Directory disaster recovery, identity management, cross-forest migrations and password strength auditing.
- The [DSInternals PowerShell Module](https://www.powershellgallery.com/packages/DSInternals/) provides easy-to-use cmdlets that are built on top of the Framework. These are the main features:
  - [Entra ID FIDO2 key auditing](../Documentation/PowerShell/Get-AzureADUserEx.md#get-azureaduserex) and retrieval of system information about all user-registered key credentials.
  - [Active Directory password auditing](../Documentation/PowerShell/Test-PasswordQuality.md#test-passwordquality) that discovers accounts sharing the same passwords or having passwords in a public database like [HaveIBeenPwned](https://haveibeenpwned.com) or in a custom dictionary.
  - [BitLocker recovery key](../Documentation/PowerShell/Get-ADDBBitLockerRecoveryInformation.md#get-addbbitlockerrecoveryinformation) and [LAPS password](../Documentation/PowerShell/Get-ADDBAccount.md#get-addbaccount) retrieval from Active Directory backups.
  - [Salvaging DNS resource records](../Documentation/PowerShell/Get-ADDBDnsResourceRecord.md#get-addbdnsresourcerecord) in the form of zone files from Active Directory backups.
  - [Key credential auditing and generation](../Documentation/PowerShell/Get-ADKeyCredential.md#get-adkeycredential), including support for NGC, FIDO2 and STK keys. Keys can also be tested against the [ROCA vulnerability](https://portal.msrc.microsoft.com/en-us/security-guidance/advisory/ADV190026). New NGC keys can also be [registered through the MS-DRSR protocol](../Documentation/PowerShell/Add-ADReplNgcKey.md#add-adreplngckey).
  - [Bare-metal recovery of domain controllers](../Documentation/PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) from just IFM backups (ntds.dit + SYSVOL).
  - Offline ntds.dit file manipulation, including [hash dumping](../Documentation/PowerShell/Get-ADDBAccount.md#get-addbaccount), [password resets](../Documentation/PowerShell/Set-ADDBAccountPassword.md#set-addbaccountpassword), [group membership changes](../Documentation/PowerShell/Set-ADDBPrimaryGroup.md#set-addbprimarygroup), [SID History injection](../Documentation/PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) and [enabling](../Documentation/PowerShell/Enable-ADDBAccount.md#enable-addbaccount)/[disabling](../Documentation/PowerShell/Disable-ADDBAccount.md#disable-addbaccount) accounts.
  - [Online password hash dumping](../Documentation/PowerShell/Get-ADReplAccount.md#get-adreplaccount) through the Directory Replication Service (DRS) Remote Protocol (MS-DRSR). This feature is commonly called DCSync.
  - [Domain or local account password hash injection](../Documentation/PowerShell/Set-SamAccountPasswordHash.md#set-samaccountpasswordhash) through the Security Account Manager (SAM) Remote Protocol (MS-SAMR) or [directly into the database](../Documentation/PowerShell/Set-ADDBAccountPasswordHash.md#set-addbaccountpasswordhash).
  - [LSA Policy modification](../Documentation/PowerShell/Set-LsaPolicyInformation.md#set-lsapolicyinformation) through the Local Security Authority (Domain Policy) Remote Protocol (MS-LSAD / LSARPC).
  - [Extracting credential roaming data](../Documentation/PowerShell/Save-DPAPIBlob.md#save-dpapiblob) and DPAPI domain backup keys, either online through [directory replication](../Documentation/PowerShell/Get-ADReplBackupKey.md#get-adreplbackupkey), [LSARPC](../Documentation/PowerShell/Get-LsaBackupKey.md#get-lsabackupkey) and [offline from ntds.dit](../Documentation/PowerShell/Get-ADDBBackupKey.md#get-addbbackupkey).
  - Password hash calculation, including [NT hash](../Documentation/PowerShell/ConvertTo-NTHash.md#convertto-nthash), [LM hash](../Documentation/PowerShell/ConvertTo-LMHash.md#convertto-lmhash) and [kerberos keys](../Documentation/PowerShell/ConvertTo-KerberosKey.md#convertto-kerberoskey).

> DISCLAIMER: Features exposed through these tools are not supported by Microsoft. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.

## Author

### Michael Grafnetter

[![Twitter](https://img.shields.io/twitter/follow/MGrafnetter.svg?label=Twitter%20@MGrafnetter&style=social)](https://twitter.com/MGrafnetter)
[![Blog](https://img.shields.io/badge/Blog-www.dsinternals.com-2A6496.svg)](https://www.dsinternals.com/en)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-grafnetter-0077B5.svg?logo=LinkedIn)](https://www.linkedin.com/in/grafnetter)

I have created these tools in my spare time and I am using them while performing AD security audits and also in my lectures to demonstrate how Active Directory works internally.

I would like to thank all people who have contributed to the project by [sending their feedback](https://github.com/MichaelGrafnetter/DSInternals/issues) or by [submitting their code](https://github.com/MichaelGrafnetter/DSInternals/graphs/contributors). In case you would also like to help with this project, please see the [CONTRIBUTING](CONTRIBUTING.md#contributing-to-dsinternals) document.

## Downloads

[![PowerShell Gallery Downloads](https://img.shields.io/powershellgallery/dt/DSInternals.svg?label=PowerShell%20Gallery%20Downloads&logo=PowerShell)](https://www.powershellgallery.com/packages/DSInternals/)
[![Chocolatey Downloads](https://img.shields.io/chocolatey/dt/dsinternals-psmodule.svg?label=Chocolatey%20Downloads&logo=NuGet)](https://chocolatey.org/packages/dsinternals-psmodule)
[![GitHub Downloads](https://img.shields.io/github/downloads/MichaelGrafnetter/DSInternals/total.svg?label=GitHub%20Downloads&logo=GitHub)](https://github.com/MichaelGrafnetter/DSInternals/releases)
[![NuGet Gallery Downloads](https://img.shields.io/nuget/dt/DSInternals.Common.svg?label=NuGet%20Gallery%20Downloads&logo=NuGet)](https://www.nuget.org/profiles/DSInternals)

### PowerShell Gallery (PowerShell 5+)

Since PowerShell 5, you can install the DSInternals module directly from the official [PowerShell Gallery](https://www.powershellgallery.com/packages/DSInternals/) by running the following command:

```powershell
Install-Module DSInternals -Force
```

Additional steps might be required on some freshly installed computers before DSInternals can be downloaded:

```powershell
# TLS 1.2 must be enabled on older versions of Windows.
[System.Net.ServicePointManager]::SecurityProtocol =
    [System.Net.ServicePointManager]::SecurityProtocol -bor [System.Net.SecurityProtocolType]::Tls12

# Download the NuGet package manager binary.
Install-PackageProvider -Name NuGet -Force

# Register the PowerShell Gallery as package repository if it is missing for any reason.
if($null -eq (Get-PSRepository -Name PSGallery -ErrorAction SilentlyContinue)) {
    Register-PSRepository -Default
}

# Download the DSInternals PowerShell module.
Install-Module -Name DSInternals -Force
```

### Chocolatey Package

The DSInternals PowerShell Module can also be installed using the official [Chocolatey package](https://chocolatey.org/packages/dsinternals-psmodule) by executing the following Chocolatey command:

```powershell
choco install dsinternals-psmodule --confirm
```

This package is self-contained and it will also install all dependencies. Note that package versions prior to 3.5 were not official.

### WAPT Package

The DSInternals PowerShell Module can also be installed using the [WAPT package](https://wapt.tranquil.it/store/tis-dsinternals/).

The package can be installed by the [WAPT console](https://www.wapt.fr/en/doc/wapt-console-usage.html) or by the [WAPT Command-line interface](https://www.wapt.fr/en/doc/wapt-command-line-interface.html) like so:

```powershell
wapt-get install dsinternals
```

This package is self-contained and it will also install all dependencies.

### Offline Module Distribution (PowerShell 3+)

1. Download the [current release](https://github.com/MichaelGrafnetter/DSInternals/releases) from GitHub.
2. *Unblock* the ZIP file, using either the *Properties dialog* or the `Unblock-File` cmdlet. If you fail to do so, all the extracted DLLs will inherit this attribute and PowerShell will refuse to load them.
3. Extract the *DSInternals* directory to your PowerShell modules directory, e.g. *C:\Windows\system32\WindowsPowerShell\v1.0\Modules\DSInternals* or *C:\Users\John\Documents\WindowsPowerShell\Modules\DSInternals*.
4. (Optional) If you copied the module to a different directory than advised in the previous step, you have to manually import it using the `Import-Module` cmdlet.

### Commando VM

The DSInternals PowerShell module is part of FireEye's [Commando VM](https://github.com/fireeye/commando-vm), the Windows-based alternative to Kali Linux.

###  NuGet Packages

The easiest way of integrating the DSInternals functionality into .NET applications is by using the [DSInternals Framework NuGet packages](https://www.nuget.org/profiles/DSInternals):

- [DSInternals.Common](https://www.nuget.org/packages/DSInternals.Common/)
- [DSInternals.DataStore](https://www.nuget.org/packages/DSInternals.DataStore/)
- [DSInternals.Replication](https://www.nuget.org/packages/DSInternals.Replication/)
- [DSInternals.SAM](https://www.nuget.org/packages/DSInternals.SAM/)

### Building from Source Code

[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-383278.svg?logo=Visual-Studio-Code)](CONTRIBUTING.md#building-from-source-code)
[![Build Status](https://dev.azure.com/DSInternals/DSInternals%20CI/_apis/build/status/DSInternals?branchName=master)](https://dev.azure.com/DSInternals/DSInternals%20CI/_build/latest?definitionId=2&branchName=master)
[![Test Results](https://img.shields.io/azure-devops/tests/DSInternals/DSInternals%20CI/2.svg?label=Test%20Results&logo=Azure-DevOps)](https://dev.azure.com/DSInternals/DSInternals%20CI/_build/latest?definitionId=2&branchName=master)

You can of course download the [source code](https://github.com/MichaelGrafnetter/DSInternals/archive/master.zip), perform a review and compile the Module/Framework yourself. See the [CONTRIBUTING](CONTRIBUTING.md#building-from-source-code) guide for more info.

## Documentation

### Get-Help

The online version of [PowerShell Get-Help documentation](../Documentation/PowerShell/Readme.md#dsinternals-powershell-module) contains the list of all cmdlets and some usage examples.

### Blog Posts

I have also published a series of articles about the DSInternals module on [my blog](https://www.dsinternals.com/en/). Here are a few of them:

- [New Offline Capabilities in DSInternals 4.11](https://www.dsinternals.com/en/dsinternals-v4.11/)
- [Cross-Forest Duplicate Password Discovery](https://www.dsinternals.com/en/cross-forest-duplicate-password-discovery/)
- [CQLabs – Extracting Roamed Private Keys from Active Directory](https://cqureacademy.com/blog/extracting-roamed-private-keys)
- [CQLabs – Offline Attacks on Active Directory](https://cqureacademy.com/cqure-labs/cqlabs-dsinternals-powershell-module)
- [Auditing Active Directory Password Quality](https://www.dsinternals.com/en/auditing-active-directory-password-quality/)
- [Dumping ntds.dit files](https://www.dsinternals.com/en/dumping-ntds-dit-files-using-powershell/)
- [Retrieving Active Directory Passwords Remotely](https://www.dsinternals.com/en/retrieving-active-directory-passwords-remotely/)
- [Retrieving DPAPI Backup Keys from Active Directory](https://www.dsinternals.com/en/retrieving-dpapi-backup-keys-from-active-directory/)
- [Retrieving Cleartext GMSA Passwords from Active Directory](https://www.dsinternals.com/en/retrieving-cleartext-gmsa-passwords-from-active-directory/)
- [Peeking into the Active Directory Database](https://www.dsinternals.com/en/peeking-into-the-active-directory-database/)
- [Dumping and modifying Active Directory database using a bootable flash drive](https://www.dsinternals.com/en/dumping-modifying-active-directory-database-bootable-flash-drive/)
- [Impersonating Office 365 Users With Mimikatz](https://www.dsinternals.com/en/impersonating-office-365-users-mimikatz/)

### Slide Decks
- [Black Hat Europe 2019: DSInternals PowerShell Module](https://www.dsinternals.com/wp-content/uploads/eu-19-Grafnetter-DSInternals-PowerShell-Module.pdf)
- [Black Hat Europe 2019: Exploiting Windows Hello for Business](https://www.dsinternals.com/wp-content/uploads/eu-19-Grafnetter-Exploiting-Windows-Hello-for-Business.pdf)
- [HipConf New York 2018: Offline Attacks on Active Directory](https://www.dsinternals.com/wp-content/uploads/HIP_AD_Offline_Attacks.pdf)

## Acknowledgements

This project utilizes the following 3<sup>rd</sup> party copyrighted material:

- [ManagedEsent](https://github.com/Microsoft/ManagedEsent) - Provides managed access to esent.dll, the embeddable database engine native to Windows.
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - A convention-based object-object mapper in .NET.
- [NDceRpc](https://github.com/OpenSharp/NDceRpc) - Integration of WCF and .NET with MS-RPC and binary serialization.
- [PBKDF2.NET](https://github.com/therealmagicmike/PBKDF2.NET) - Provides PBKDF2 for .NET Framework.
- [Bouncy Castle](https://www.bouncycastle.org/csharp/index.html) - A lightweight cryptography API for Java and C#. 
- [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) - Popular high-performance JSON framework for .NET.
- [Peter O. CBOR](https://github.com/peteroupc/CBOR) - A C# implementation of Concise Binary Object Representation (RFC 7049).

## Related Projects

- [Mimikatz](https://github.com/gentilkiwi/mimikatz) - The No.1 tool for pass-the-hash attacks. Can use the credentials extracted by the DSInternals module to do some nasty stuff.
- [NTDSXtract](https://github.com/csababarta/ntdsxtract) - A framework for ntds.dit parsing written in Python.
- [Impacket](https://github.com/SecureAuthCorp/impacket) - Various MSRPC-based protocols implemented in Python.
- [DIT Snapshot Viewer](https://github.com/yosqueoy/ditsnap) - A graphical inspection tool for Active Directory databases.
- [Esent Workbench](https://bitbucket.org/orthoprog/esentworkbench/wiki/Home) - Great tool for displaying the structure of ntds.dit files.
