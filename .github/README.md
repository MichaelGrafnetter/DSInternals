# DSInternals PowerShell Module and Framework

![MIT License](https://img.shields.io/badge/License-MIT-green.svg)
![PowerShell 3 | 4 | 5](https://img.shields.io/badge/PowerShell-3%20|%204%20|%205-0000FF.svg)
![Windows Server 2008 R2 | 2012 R2 | 2016 | 2019](https://img.shields.io/badge/Windows%20Server-2008%20R2%20|%202012%20R2%20|%202016%20|%202019-007bb8.svg)
![.NET Framework 4.5.1+](https://img.shields.io/badge/.NET%20Framework-4.5.1%2B-007FFF.svg)

## Introduction

The DSInternals project consists of these two parts:
- The [DSInternals Framework](https://www.nuget.org/profiles/MichaelGrafnetter) exposes several internal features of [Active Directory](https://docs.microsoft.com/en-us/windows-server/identity/ad-ds/get-started/virtual-dc/active-directory-domain-services-overview) and can be used from any .NET application. The codebase has already been integrated into several 3<sup>rd</sup> party commercial products that use it in scenarios like Active Directory disaster recovery, identity management, cross-forest migrations and password strength auditing.
- The [DSInternals PowerShell Module](https://www.powershellgallery.com/packages/DSInternals/) provides easy-to-use cmdlets that are built on top of the Framework. These are the main features:
  - Offline [ntds.dit file](https://technet.microsoft.com/en-us/library/cc961761.aspx) manipulation, including [hash dumping](../Documentation/PowerShell/Get-ADDBAccount.md), [password resets](../Documentation/PowerShell/Set-ADDBAccountPassword.md), [group membership changes](../Documentation/PowerShell/Set-ADDBPrimaryGroup.md), [SID History injection](../Documentation/PowerShell/Add-ADDBSidHistory.md) and [enabling](../Documentation/PowerShell/Enable-ADDBAccount.md)/[disabling](../Documentation/PowerShell/Disable-ADDBAccount.md) accounts.
  - [Online password hash dumping](../Documentation/PowerShell/Get-ADReplAccount.md) through the [Directory Replication Service (DRS) Remote Protocol (MS-DRSR)](https://msdn.microsoft.com/en-us/library/cc228086.aspx). This feature is commonly called DCSync.
  - [Active Directory password auditing](../Documentation/PowerShell/Test-PasswordQuality.md) that discovers accounts sharing the same passwords or having passwords in a public database like [HaveIBeenPwned](https://haveibeenpwned.com) or in a custom dictionary.
  - [Domain or local account password hash injection](../Documentation/PowerShell/Set-SamAccountPasswordHash.md) through the [Security Account Manager (SAM) Remote Protocol (MS-SAMR)](https://msdn.microsoft.com/en-us/library/cc245476.aspx) or [directly into the database](../Documentation/PowerShell/Set-ADDBAccountPasswordHash.md).
  - [LSA Policy modification](../Documentation/PowerShell/Set-LsaPolicyInformation.md) through the [Local Security Authority (Domain Policy) Remote Protocol (MS-LSAD / LSARPC)](https://msdn.microsoft.com/en-us/library/cc234225.aspx).
  - [Extracting credential roaming data](../Documentation/PowerShell/Save-DPAPIBlob.md) and DPAPI domain backup keys, either online through [directory replication](../Documentation/PowerShell/Get-ADReplBackupKey.md) and [LSARPC](../Documentation/PowerShell/Get-LsaBackupKey.md) or [offline from ntds.dit](../Documentation/PowerShell/Get-ADDBBackupKey.md).
  - [Bare-metal recovery of domain controllers](../Documentation/PowerShell/New-ADDBRestoreFromMediaScript.md) from just IFM backups (ntds.dit + SYSVOL). 
  - Password hash calculation, including [NT hash](../Documentation/PowerShell/ConvertTo-NTHash.md), [LM hash](../Documentation/PowerShell/ConvertTo-LMHash.md) and [kerberos keys](../Documentation/PowerShell/ConvertTo-KerberosKey.md).

> DISCLAIMER: Features exposed through these tools are not supported by Microsoft. Improper use might cause irreversible damage to domain controllers or negatively impact domain security.

## Author

### Michael Grafnetter

[![Twitter](https://img.shields.io/twitter/follow/MGrafnetter.svg?label=@MGrafnetter&style=social)](https://twitter.com/MGrafnetter)
[![Blog](https://img.shields.io/badge/Blog-www.dsinternals.com-2A6496.svg)](https://www.dsinternals.com/en)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-grafnetter-0077B5.svg)](https://www.linkedin.com/in/grafnetter)

I have created these tools in my spare time and I am using them while performing AD security audits and also in my lectures to demonstrate how Active Directory works internally. The code has many flaws and I could definitely do better if I had more free time.

I would like to thank all people who have contributed to the project by [sending their feedback](https://github.com/MichaelGrafnetter/DSInternals/issues) or by [submitting their code](https://github.com/MichaelGrafnetter/DSInternals/graphs/contributors). In case you would also like to help with this project, please see the [CONTRIBUTING](CONTRIBUTING.md) document.

## Downloads
[![PowerShell Gallery Downloads](https://img.shields.io/powershellgallery/dt/DSInternals.svg?label=PowerShell%20Gallery%20Downloads)](https://www.powershellgallery.com/packages/DSInternals/)
[![NuGet Gallery Downloads](https://img.shields.io/nuget/dt/DSInternals.Common.svg?label=NuGet%20Gallery%20Downloads)](https://www.nuget.org/profiles/MichaelGrafnetter)
[![GitHub Downloads](https://img.shields.io/github/downloads/MichaelGrafnetter/DSInternals/total.svg?label=GitHub%20Downloads)](https://github.com/MichaelGrafnetter/DSInternals/releases)

### PowerShell Gallery (PowerShell 5+)

Since PowerShell 5, you can install the DSInternals module directly from the official [PowerShell Gallery](https://www.powershellgallery.com/packages/DSInternals/) by running the following command:

```powershell
Install-Module DSInternals -Force
```

### Offline Module Distribution (PowerShell 3+)

1. Download the [current release](https://github.com/MichaelGrafnetter/DSInternals/releases) from GitHub.
2. *Unblock* the ZIP file, using either the *Properties dialog* or the `Unblock-File` cmdlet. If you fail to do so, all the extracted DLLs will inherit this attribute and PowerShell will refuse to load them.
3. Extract the *DSInternals* directory to your PowerShell modules directory, e.g. *C:\Windows\system32\WindowsPowerShell\v1.0\Modules\DSInternals* or *C:\Users\John\Documents\WindowsPowerShell\Modules\DSInternals*.
4. (Optional) If you copied the module to a different directory than advised in the previous step, you have to manually import it using the `Import-Module` cmdlet.

### NuGet Packages

The easiest way of integrating the DSInternals functionality into .NET applications is by using the [DSInternals Framework NuGet packages](https://www.nuget.org/profiles/MichaelGrafnetter).

### Building from Source Code

![Visual Studio 2015 | 2017](https://img.shields.io/badge/Visual%20Studio-2015%20|%202017-383278.svg)
[![Build Status](https://dev.azure.com/DSInternals/DSInternals%20CI/_apis/build/status/MichaelGrafnetter.DSInternals?branchName=master&jobName=Release)](https://dev.azure.com/DSInternals/DSInternals%20CI/_build/latest?definitionId=2?branchName=master)
![Test Results](https://img.shields.io/azure-devops/tests/DSInternals/DSInternals%20CI/2.svg?label=Test%20Results)

You can of course download the [source code](https://github.com/MichaelGrafnetter/DSInternals/archive/master.zip), perform a review and compile the Module/Framework yourself. See the [CONTRIBUTING](CONTRIBUTING.md#Building%20from%20Source%20Code) guide for more info.

## Documentation

### Get-Help
The online version of [PowerShell Get-Help documentation](../Documentation/PowerShell/Readme.md) contains the list of all cmdlets and some usage examples.

### Blog Posts

I have also published a series of articles about the DSInternals module on [my blog](https://www.dsinternals.com/en/list-of-cmdlets-in-the-dsinternals-module/). Here are a few of them:

- [Auditing Active Directory Password Quality](https://www.dsinternals.com/en/auditing-active-directory-password-quality/)
- [Dumping ntds.dit files](https://www.dsinternals.com/en/dumping-ntds-dit-files-using-powershell/)
- [Retrieving Active Directory Passwords Remotely](https://www.dsinternals.com/en/retrieving-active-directory-passwords-remotely/)
- [Retrieving DPAPI Backup Keys from Active Directory](https://www.dsinternals.com/en/retrieving-dpapi-backup-keys-from-active-directory/)
- [Retrieving Cleartext GMSA Passwords from Active Directory](https://www.dsinternals.com/en/retrieving-cleartext-gmsa-passwords-from-active-directory/)
- [Peeking into the Active Directory Database](https://www.dsinternals.com/en/peeking-into-the-active-directory-database/)
- [Dumping and modifying Active Directory database using a bootable flash drive](https://www.dsinternals.com/en/dumping-modifying-active-directory-database-bootable-flash-drive/)
- [Impersonating Office 365 Users With Mimikatz](https://www.dsinternals.com/en/impersonating-office-365-users-mimikatz/)

### Slide Decks

- [HipConf 2018: Offline Attacks on Active Directory](https://www.dsinternals.com/wp-content/uploads/HIP_AD_Offline_Attacks.pdf)

## Acknowledgements

This project utilizes the following 3<sup>rd</sup> party copyrighted material:

- [ManagedEsent](https://github.com/Microsoft/ManagedEsent) - Provides managed access to esent.dll, the embeddable database engine native to Windows.
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - A convention-based object-object mapper in .NET.
- [NDceRpc](https://github.com/OpenSharp/NDceRpc) - Integration of WCF and .NET with MS-RPC and binary serialization.
- [PBKDF2.NET](https://github.com/therealmagicmike/PBKDF2.NET) - Provides PBKDF2 for .NET Framework.

## Related Projects

- [Mimikatz](https://github.com/gentilkiwi/mimikatz) - The No.1 tool for pass-the-hash attacks. Can use the credentials extracted by the DSInternals module to do some nasty stuff.
- [NTDSXtract](https://github.com/csababarta/ntdsxtract) - A framework for ntds.dit parsing written in Python.
- [Impacket](https://github.com/SecureAuthCorp/impacket) - Various MSRPC-based protocols implemented in Python.
- [DIT Snapshot Viewer](https://github.com/yosqueoy/ditsnap) - A graphical inspection tool for Active Directory databases.
- [Esent Workbench](https://bitbucket.org/orthoprog/esentworkbench/wiki/Home) - Great tool for displaying the structure of ntds.dit files.
