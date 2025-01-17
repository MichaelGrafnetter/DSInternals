![DSInternals Logo](../.github/DSInternals-Dark.png#gh-light-mode-only)
![DSInternals Logo](../.github/DSInternals-Light.png#gh-dark-mode-only)

# Changelog

All notable changes to this project will be documented in this file. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [4.16] - 2025-01-05

### Added

- The `DomainController` class exposes more DC properties, including `ServerObjectDN`, `NTDSSettingsObjectDN`, and `ForestRootNamingContext`.

### Fixed

- The [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now properly sets the `Configuration NC`, `Root Domain`, and `Machine DN Name` registry values under the `HKLM\SYSTEM\CurrentControlSet\Services\NTDS\Parameters` key.

## [4.15.1] - 2025-01-02

This is a PowerShell-only bugfix release.

### Fixed

- Resolved the `AmbiguousParameterSet` error returned by the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet.

## [4.15] - 2024-12-23

This is a PowerShell-only release.

### Added

- Implemented support for individual *.txt files from HIBP in the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet.

### Fixed

- The [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now generates a more robust DC recovery script:
  - Regular scheduled tasks are used instead of PowerShell scheduled jobs and workflows.
  - The script can be executed under the SYSTEM account.
  - Domain controller names longer than 15 characters are now fully supported.
  - SYSVOL GPO ACLs are optionally restored as well.
  - The Directory Services Restore Mode (DSRM) phase is skipped and only 2 reboots are required instead of 3.
  - The entire process has been tested on Windows Server 2022 and Windows Server 2008 R2.

### Removed

- The [Add-ADDBSidHistory](PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) cmdlet has been removed to prevent it from being used in migration scenarios.

## [4.14] - 2024-04-13

### Fixed

- Increased tolerance for malformed DPAPI CNG private keys.
- Improved parsing of conflicting secret object names, e.g., `CN=BCKUPKEY_PREFERRED Secret\\0ACNF:26c8edbb-6b48-4f11-9e13-9ddbccedab5a,CN=System,DC=contoso,DC=com`.

## [4.13] - 2023-12-20

### Fixed

- The [Set-LsaPolicyInformation](PowerShell/Set-LsaPolicyInformation.md#set-lsapolicyinformation) cmdlet now generates the [UNICODE_STRING](https://learn.microsoft.com/en-us/windows/win32/api/ntdef/ns-ntdef-_unicode_string) structure with the trailing null character, to improve compatibility with NETLOGON. This issue mainly affects the functionality of the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet. Thanks Christoffer Andersson for reporting this issue and sorry Microsoft support escalation engineers for the trouble this bug has caused.

## [4.12] - 2023-10-06

### Added

- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet now works against Windows Server 2025 Insider Preview with the [32k database page size optional feature](https://learn.microsoft.com/en-us/windows-server/identity/ad-ds/whats-new-active-directory-domain-services-insider-preview#32k-database-page-size-optional-feature) enabled.
- The [Get-ADDBAccount](PowerShell/Get-ADDBAccount.md#get-addbaccount) cmdlet is now able to read databases originating from Windows Server 2025 Insider Preview with the [32k database page size optional feature](https://learn.microsoft.com/en-us/windows-server/identity/ad-ds/whats-new-active-directory-domain-services-insider-preview#32k-database-page-size-optional-feature) enabled.
- Added support for parsing AES SHA2 Kerbers keys.

### Fixed

- Improved KDS Root Key selection algorithm in the [Get-ADDBServiceAccount](PowerShell/Get-ADDBServiceAccount.md#get-addbserviceaccount) cmdlet.

## [4.11] - 2023-10-01

### Added

- Added the [Get-ADDBServiceAccount](PowerShell/Get-ADDBServiceAccount.md#get-addbserviceaccount) cmdlet for offline managed password derivation.
- Implemented the [Unlock-ADDBAccount](PowerShell/Unlock-ADDBAccount.md#unlock-addbaccount) cmdlet that can perform offline account unlock.

### Fixed

- Fixed Kerberos PBKDF2 salt derivation for service accounts in the [ConvertTo-KerberosKey](PowerShell/ConvertTo-KerberosKey.md#convertto-kerberoskey) cmdlet and the corresponding 
[KerberosKeyDerivation](../Src/DSInternals.Common/Cryptography/KerberosKeyDerivation.cs) class.

## [4.10] - 2023-09-16

### Added

- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now checks if a user's password is equal to their SamAccountName attribute, thanks to @BlueCurby.
- Replication cmdlets in the PowerShell module should now work on the ARM64 platform as well. Tests were performed using the [Windows Dev Kit 2023, AKA Project Volterra](https://learn.microsoft.com/en-us/windows/arm/dev-kit/).

### Fixed

- Fixed a rare security descriptor parsing issue.
- Parallel reading of multiple databases is now supported.

## [4.9] - 2023-02-25

### Changed

- Implemented [FIPS compliance requirement](https://learn.microsoft.com/en-us/windows/security/threat-protection/security-policy-settings/system-cryptography-use-fips-compliant-algorithms-for-encryption-hashing-and-signing) check (issues [#97](https://github.com/MichaelGrafnetter/DSInternals/issues/97), [#111](https://github.com/MichaelGrafnetter/DSInternals/issues/111), and [#152](https://github.com/MichaelGrafnetter/DSInternals/issues/152)).
- Added a check that the module is running on Windows.
- The [Set-ADDBBootKey](PowerShell/Set-ADDBBootKey.md#set-addbbootkey) cmdlet now also has the `-Force` parameter, as do all other cmdlets for offline DB modification.

### Fixed

- The [Get-BootKey](PowerShell/Get-BootKey.md#get-bootkey) cmdlet should now be able to read inconsistent/corrupted SYSTEM registry hives (issue [#47](https://github.com/MichaelGrafnetter/DSInternals/issues/47)).

## [4.8] - 2022-12-06

### Changed

- Upgraded to the latest [JSON.NET library](https://www.newtonsoft.com/json) to fix some security issues.
- Upgraded to the latest [CBOR library](https://github.com/peteroupc/CBOR) to fix some security issues.
- Added pipeline input support to the `-SamAccountName` parameter of the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet.
- All PowerShell cmdlets that modify the `ntds.dit` file now have the `-Force` parameter.

### Fixed

- Fixed a regression error in `ntds.dit` file modification on Windows Server 2022 that was introduced in release [4.7].

## [4.7] - 2021-10-30

### Added

- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet can now identify [kerberoastable](https://attack.mitre.org/techniques/T1558/003/) user accounts.
- The [DSAccount](../Src/DSInternals.Common/Data/Principals/DSAccount.cs) class now exposes the [msDs-supportedEncryptionTypes](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/4f1f447a-4f35-49b1-a129-d76ea280a4c1) attribute in its `SupportedEncryptionTypes` property.

### Changed

- DSInternals.Replication.Interop is targeting the latest Windows 10 SDK instead of a specific one.

### Fixed

- Computer accounts are now skipped when searching for duplicate passwords.
- Improved exception handling when opening read-only database files.

## [4.6] - 2021-10-19

### Added

- Windows Server 2022 ntds.dit file modification is now supported.

### Changed

- Updated ManagedEsent to 1.9.4.1 and extracted customizations to partial classes.

### Fixed

- ESE parameter set now better mimics the one used in AD.

## [4.5] - 2021-10-14

### Fixed
- Added support for ntds.dit files with conflicting defunct attributes.
- Fixed the detection of default computer passwords.
- Improved parsing of roaming CNG private keys.

### Changed

- Updated the target .NET Framework to 4.7.2.

## [4.4.1] - 2020-07-18

### Fixed

- The `vcruntime140_1.dll` file is now part of the binary distribution. Its absence sometimes prevented the `DSInternals.Replication.Interop.dll` file from being loaded.

## [4.4] - 2020-07-03

### Added

- The new [Set-AzureADUserEx](PowerShell/Set-AzureADUserEx.md#set-azureaduserex) cmdlet can be used to revoke FIDO2 and NGC keys in Azure Active Directory.

## [4.3] - 2020-04-02

### Added

- New logo and package icons!
- The new [Get-AzureADUserEx](PowerShell/Get-AzureADUserEx.md#get-azureaduserex) cmdlet can be used to retrieve FIDO and NGC keys from Azure Active Directory, as the first tool on the market.
- Both [lastLogon](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-ada1/93258066-276d-4357-8458-981c19caad95) and [lastLogonTimestamp](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-ada1/530d7194-20f6-4aaa-8d80-9ca6b6350ad6) user account attributes are now exposed. The LastLogonDate PowerShell property returns whichever of these 2 values is available.
- The `-Server` parameter of the [Get-ADSIAccount](PowerShell/Get-ADSIAccount.md#get-adsiaccount) cmdlet now has the standard `-ComputerName` alias.

### Changed

- Major [PowerShell module documentation](PowerShell/Readme.md#directory-services-internals-powershell-module) improvements.

## [4.2] - 2020-03-18

### Added

- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now supports **cross-domain and cross-forest duplicate password discovery**.
- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount), [Get-ADReplBackupKey](PowerShell/Get-ADReplBackupKey.md#get-adreplbackupkey) and [Add-ADReplNgcKey](PowerShell/Add-ADReplNgcKey.md#add-adreplngckey) cmdlets no longer require the `Domain` and `NamingContext` parameters to be specified, as their proper values are automatically retrieved from the target DC.

### Changed

- Updated license information in Nuget packages to resolve [Warning NU5125](https://docs.microsoft.com/en-us/nuget/reference/errors-and-warnings/nu5125).

### Fixed

- Resolved a bug in the [Get-ADDBBackupKey](PowerShell/Get-ADDBBackupKey.md#get-addbbackupkey) cmdlet that prevented it from working on global catalogs in multi-domain forests.
- Resolved a bug in DPAPI credential display.

## [4.1] - 2019-12-12

### Added

- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now contains a check for accounts that require smart card authentication and have a password at the same time. 

### Fixed

- The [Save-DPAPIBlob](PowerShell/Save-DPAPIBlob.md#save-dpapiblob) cmdlet now saves roamed CNG keys in proper format.
- Fixed an issue with the [Set-ADDBAccountPassword](PowerShell/Set-ADDBAccountPassword.md#set-addbaccountpassword) and [Set-ADDBAccountPasswordHash](PowerShell/Set-ADDBAccountPasswordHash.md#set-addbaccountpasswordhash) cmdlets, which, under rare circumstances, could incorrectly modify replication metadata. Unfortunately, the documentation does not say that [PROPERTY_META_DATA_EXT_VECTOR](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/22bccd51-1e7d-4502-aef8-b84da983f94f) must be sorted.

## [4.0] - 2019-12-04

### Added

- Added support for [auditing (Azure) Active Directory NGC keys](PowerShell/Get-ADKeyCredential.md#get-adkeycredential) against the [ROCA](https://crocs.fi.muni.cz/public/papers/rsa_ccs17) vulnerability.
- Added the [Add-ADReplNgcKey](PowerShell/Add-ADReplNgcKey.md#add-adreplngckey) cmdlet for NGC key injection through the MS-DRSR protocol.
- Added the `Moduli` custom PowerShell view to enable export of public keys stored in the `msDS-KeyCredentialLink` attribute.
- Added the `FIDO` custom PowerShell view to provide visibility into FIDO2 keys registered in the `msDS-KeyCredentialLink` attribute.
- Implemented FIDO2 token information parsing in the `KeyCredential` class. Tested with **YubiKey**, **Feitian**, **eWBM** and **SoloKeys**. Big thanks to @aseigler for major code contribution!
- Implemented public key retrieval capability in the `KeyCredential` class.

### Changed

- .NET Framework 4.7 is now required because of ECC support.
- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet can now search accounts by the `userPrincipalName` attribute.
- NGC keys generated by the [Get-ADKeyCredential](PowerShell/Get-ADKeyCredential.md#get-adkeycredential) cmdlet are now accepted in validated writes.

### Fixed
- Eliminated a memory leak in `DRS_MSG_GETCHGREQ_V8` deallocation.
- Fixed the output type of the [Set-ADDBBootKey](PowerShell/Set-ADDBBootKey.md#set-addbbootkey) cmdlet.

## [3.6.1] - 2019-08-10

### Fixed
- Resolved issue #91 (The boot key provided cannot be used to decrypt the database),
  which appeared during decryption of ntds.dit files originating from Windows Server 2016+ DCs that were promoted using IFM.

## [3.6] - 2019-06-28

### Changed
- Renamed the `-DBPath` parameter of [database cmdlets](PowerShell/Readme.md#cmdlets-for-offline-active-directory-operations) to `-DatabasePath`.
- Improved [Get-Help documentation](PowerShell/Readme.md#dsinternals-powershell-module).

### Fixed
- Resolved issue #88 (Test-PasswordQuality errors out with "Offset and length must refer to a position in the string").

## 3.5.1 - 2019-05-23

This is a [Chocolatey](https://chocolatey.org/packages/dsinternals-psmodule)-only release.

### Fixed
- Temporarily removed the package dependency on PowerShell 3, which caused some [issues](https://github.com/MichaelGrafnetter/DSInternals/issues/85). Will be resolved in a future release.

## [3.5] - 2019-05-10

### Added
- Official [Chocolatey Package](https://chocolatey.org/packages/dsinternals-psmodule)
- New password hash export formats: [JohnLMHistory](PowerShell/Readme.md#john-the-ripper), [HashcatLMHistory](PowerShell/Readme.md#hashcat), [PWDumpHistory](PowerShell/Readme.md#other-formats) and [LMHashHistory](PowerShell/Readme.md#other-formats).

### Changed
- The [JohnNTHistory](PowerShell/Readme.md#john-the-ripper) and [HashcatNTHistory](PowerShell/Readme.md#hashcat) export formats now differentiate between current and historical password hashes.

### Fixed
- Improved the [JohnNT](PowerShell/Readme.md#john-the-ripper) and [JohnLM](PowerShell/Readme.md#john-the-ripper) export formats.
- Scripts generated by the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now correctly restore SYSVOL on Windows Server 2008 R2+.
- Scripts generated by the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now supports SYSVOL FRS replication in addition to DFS-R.
- Scripts generated by the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now do not require the ActiveDirectory module to be pre-installed.

## [3.4] - 2019-04-23

### Added
- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now has a parameter called `-WeakPasswordHashesSortedFile`. This parameter should be used with ordered hash files downloaded from [HaveIBeenPwned](https://haveibeenpwned.com/Passwords) as it has huge performance benefits over the older `-WeakPasswordHashesFile` parameter due to the usage of binary search algorithm.
- The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now has a proper documentation, including usage examples.

### Fixed
  - The [PWDump](PowerShell/Readme.md#other-formats) export format is now more compatible with some 3rd party tools, e.g. ElcomSoft Distributed Password Recovery, although the ASCII encoding still must be enforced.
  - The speed of processing the `-WeakPasswordHashesFile` and `-WeakPasswordsFile` parameters of the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet has significantly been increased.
  - Parsing of roamed credentials is now slightly faster.
  - Documentation improvements!

## [3.3] - 2019-03-02
### Changed
- Implemented a slightly more secure handling of [GMSA passwords](../Src/DSInternals.Common/Data/Principals/ManagedPassword.cs).
- The .NET Framework 4.5.1 requirement is now enforced.

### Fixed
- Scripts generated by the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet will also fix SYSVOL references in the DFS-R subscription object if it is restored to a different path.
- A more explanatory exception is now thrown when opening databases that originate from different OS versions.
- A more explanatory exception is now thrown when the *Universal C Runtime* is missing from Windows.
- A more explanatory exception is now thrown when the assemblies are blocked.
- PEK list decryption exceptions now contain troubleshooting data.
- Minor improvement in C++ build speed.

## [3.2.1] - 2019-01-04
### Fixed
- The implementation of database re-encryption now behaves more closely to Windows Server 2016.

## [3.2] - 2019-01-03
### Added
- [Module] Added the [Get-LsaBackupKey](PowerShell/Get-LsaBackupKey.md#get-lsabackupkey) cmdlet for DPAPI domain backup key retrieval through LSARPC.
- [Framework] Added support for DPAPI domain backup key retrieval from LSA Policy.

### Changed
- [Module] The [Set-ADDBBootKey](PowerShell/Set-ADDBBootKey.md#set-addbbootkey) cmdlet now works with Windows Server 2000-2019 databases.
- [Module] The [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet now uses shutdown.exe instead of Restart-Computer.
- [Framework] Updated package references.

### Fixed
- [Framework] Fixed `DSInternals.Replication.Interop` assembly versioning.

## [3.1] - 2018-12-29
### Added
- [Module] Added the [New-ADDBRestoreFromMediaScript](PowerShell/New-ADDBRestoreFromMediaScript.md#new-addbrestorefrommediascript) cmdlet to aid with file-level DC recovery process.
- [Module] Added the [Get-LSAPolicyInformation](PowerShell/Get-LSAPolicyInformation.md#get-lsapolicyinformation) and [Set-LSAPolicyInformation](PowerShell/Set-LSAPolicyInformation.md#set-lsapolicyinformation) cmdlets that can be used to retrieve and change domain-related LSA Policies.
- [Module] Extended the information returned by the [Get-ADDBDomainController](PowerShell/Get-ADDBDomainController.md#get-addbdomaincontroller) cmdlet.
- [Module] Added MAML documentation for `Get-Help`.
- [Framework] Added support for LSA Policy retrieval and modification.

### Changed
- [Framework] Implemented distinguished name (DN) caching in the database access layer.

### Fixed
- [Module] Path to the DSInternals.psd1 file now does not need to be specified when loading the module from a non-default location.

## [3.0] - 2018-09-29
### Added
- [Module] Added the [Set-ADDBAccountPassword](PowerShell/Set-ADDBAccountPassword.md#set-addbaccountpassword) and [Set-ADDBAccountPasswordHash](PowerShell/Set-ADDBAccountPasswordHash.md#set-addbaccountpasswordhash) cmdlets for offline password modification.
- [Module] The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet now supports NTLM hash list from haveibeenpwned.com.
- [Module] Added the [Get-ADKeyCredential](PowerShell/Get-ADKeyCredential.md#get-adkeycredential) cmdlet for linked credential generation (AKA Windows Hello for Business).
- [Module] The [Get-ADDBAccount](PowerShell/Get-ADDBAccount.md#get-addbaccount), [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) and [Get-ADSIAccount](PowerShell/Get-ADSIAccount.md#get-adsiaccount) cmdlets now display linked credentials.
- [Module] Databases from Windows Server 2016 can now be read on non-DCs.
- [Module] Added the [ConvertTo-KerberosKey](PowerShell/ConvertTo-KerberosKey.md#convertto-kerberoskey) cmdlet for key generation.
- [Module] The [Save-DPAPIBlob](PowerShell/Save-DPAPIBlob.md#save-dpapiblob) now generates scripts for mimikatz.
- [Module] The [Save-DPAPIBlob](PowerShell/Save-DPAPIBlob.md#save-dpapiblob) cmdlet now accepts pipeline input from both Get-ADDBBackupKey and ADDBAccount cmdlets.
- [Module] Added Views [JohnNTHistory](PowerShell/Readme.md#john-the-ripper), [HashcatNTHistory](PowerShell/Readme.md#hashcat) and [NTHashHistory](PowerShell/Readme.md#other-formats).
- [Module] The [Get-ADDBDomainController](PowerShell/Get-ADDBDomainController.md#get-addbdomaincontroller) now displays domain and forest functional levels.
- [Module] The [Set-ADDBDomainController](PowerShell/Set-ADDBDomainController.md#set-addbdomaincontroller) can now be used to modify backup expiration.
- [Module] The [Get-ADDBAccount](PowerShell/Get-ADDBAccount.md#get-addbaccount) cmdlet now reports progress when retrieving multiple accounts.
- [Framework] Added support for offline password changes.
- [Framework] Added support for kerberos key derivation.
- [Framework] Added support for WDigest hash calculation.

### Fixed
- [Framework] Minor bug fixes.

### Removed
- [Module] Removed the `ConvertTo-NTHashDictionary` cmdlet as its functionality had been integrated into the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet.
- [Module] Removed the `-ShowPlainTextPasswords` parameter of the [Test-PasswordQualiy](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet. It might be re-added in the future.

## [2.23] - 2018-07-07
### Changed
- [Module] The [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) now supports accounts that require smart card authentication.

### Fixed
- [Module] Fixed a bug in in the processing of the `-SkipDuplicatePasswordTest` switch of the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet.

## [2.22] - 2017-04-29
### Added
- [Framework] Added the [Enable-ADDBAccount](PowerShell/Enable-ADDBAccount.md#enable-addbaccount) and [Disable-ADDBAccount](PowerShell/Disable-ADDBAccount.md#disable-addbaccount) cmdlets.
- [Module] Added the ability to enable or disable accounts in offline databases.

## [2.21.2] - 2017-04-19
### Fixed
- [Framework] Fixed a bug in roamed credentials processing.
- [Module] Fixed a bug in hexadecimal parameter parsing. 

## [2.21.1] - 2017-04-14
### Fixed
- Fixed a bug in linked value replication.

## [2.21] - 2017-03-25
- [Module] The replication cmdlets now use Kerberos authentication by default. 
- [Module] Added support for roamed credentials.
- [Module] Cmdlets now accept hashes in both byte array and hexadecimal string forms.
- [Framework] Added support for linked value retrieval.
- [Framework] Updated referenced packages.
- [Framework] Added the SamEnumerateDomainsInSamServer call.

## [2.20] - 2016-11-15
- Added the [Get-ADPasswordPolicy](PowerShell/Get-SamPasswordPolicy.md#get-sampasswordpolicy) cmdlet.

## [2.19] - 2016-10-21
- Added support for the ServicePrincipalName attribute.

## [2.18] - 2016-10-02
- [Module] Added the [Get-ADDBKdsRootKey](PowerShell/Get-ADDBKdsRootKey.md#get-addbkdsrootkey) cmdlet to aid DPAPI-NG decryption, e.g. SID-protected PFX files.
- [Module] The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet now correctly reports the access denied error.
- [Module] Fixed a bug in progress reporting of the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet.
- [Framework] Added support for KDS Root Key retrieval.
- [Framework] Replication errors are now reported using more suitable exception types.

## 2.17 - 2016-09-16
- [Module] The `Get-ADReplAccount -All` command now reports replication progress.
- [Framework] Added the ability to retrieve the replication cursor.
- [Framework] The `ReplicationCookie` class is now immutable and replication progress is reported using a delegate.
- [Framework] Win32 exceptions are now translated to more specific .NET exceptions by the `Validator` class.

## [2.16.1] - 2016-08-08
- [Module] Added the `-ShowPlainTextPasswords` parameter to the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) cmdlet.
  Cracked and cleartext passwords now do not get displayed by default.

## [2.16] - 2016-08-07
- [Module] Added the [Test-PasswordQuality](PowerShell/Test-PasswordQuality.md#test-passwordquality) and `ConvertTo-NTHashDictionary` cmdlets.
- [Module] Added support for the the UserAccountControl attribute of user accounts.
- [Framework] Added the ability to replicate user accounts by specifying their UPN.
- [Framework] Added the ability to calculate a NT hash from both String and SecureString.
- [Framework] Added the `HashEqualityComparer`, which allows the hashes to be stored 
  in the built-in generic collections.

## [2.15] - 2016-06-18
- Removed dependency on ADSI.
- Added support for the PAM optional feature.
- Added the [PWDump](PowerShell/Readme.md#other-formats) custom view.
- Added the [NTHash](PowerShell/Readme.md#other-formats) custom view.
- Added the [LMHash](PowerShell/Readme.md#other-formats) custom view.

## [2.14] - 2016-04-30
- Added support for Windows Server 2016 ntds.dit encryption.
- Added support for replication with renamed domains.
- Added support for reading security descriptors (ACLs) from both ntds.dit files and DRS-R.
- Added support for the AdminCount attribute.
- Updated the forked ManagedEsent source codes to version 1.9.3.3.

## [2.13.1] - 2016-02-25
- Fixed a bug regarding incorrect OS version detection.

## [2.13] - 2016-02-21
- Fixed a rare bug which caused the database cmdlets to hang while loading indices.
- Meaningful error messages are now displayed when a dirty or downlevel ntds.dit file is encountered.
- The `DSInternals.Replication` library now supports incremental replication (not exposed through PowerShell).

## [2.12] - 2016-02-07
- Commandlets for ntds.dit manipulation now work on Windows 7 / Windows Server 2008 R2.
- The module now requires .NET Framework 4.5.1 instead of 4.5.
- Both Visual Studio 2013 and 2015 are now supported platforms.

## 2.11.1 - 2016-02-03
- Added support for Windows Server 2003 R2.
- The replication now works on x86, again.
- Fixed a bug in temporary index loading.

## [2.10] - 2016-01-14
- Added support for the NTLM-Strong-NTOWF package in Supplemental Credentials (new in Windows Server 2016 TP4)
- Added support for initial databases
- Added partial support for ADAM/LDS databases
- The [Get-ADDBSchemaAttribute](PowerShell/Get-ADDBSchemaAttribute.md#get-addbschemaattribute) now shows attribute OIDs
- Fixed a bug in Exchange schema loading

## 2.9 - 2015-12-27
- The [Get-BootKey](PowerShell/Get-BootKey.md#get-bootkey) cmdlet now supports online boot key retrieval
- The PBKDF2.NET library has been replaced by CryptSharp
- The [Get-ADDBDomainController](PowerShell/Get-ADDBDomainController.md#get-addbdomaincontroller) cmdlet now extracts some more data from the DB
- The project has been open-sourced

## 2.8 - 2015-10-20
- Added the [ConvertFrom-ADManagedPasswordBlob](PowerShell/ConvertFrom-ADManagedPasswordBlob.md#convertfrom-admanagedpasswordblob) cmdlet
- Added the [Get-ADDBBackupKey](PowerShell/Get-ADDBBackupKey.md#get-addbbackupkey) cmdlet
- Added the [Get-ADReplBackupKey](PowerShell/Get-ADReplBackupKey.md#get-adreplbackupkey) cmdlet
- Added the [Save-DPAPIBlob](PowerShell/Save-DPAPIBlob.md#save-dpapiblob) cmdlet
- Added the [HashcatLM](PowerShell/Readme.md#hashcat) view

## 2.7 - 2015-09-30
- Added the `about_DSInternals` help page (work in progress)
- Fixed a bug in the [Set-ADDBPrimaryGroup](PowerShell/Set-ADDBPrimaryGroup.md#set-addbprimarygroup) cmdlet

## 2.6 - 2015-09-21
- Implemented CRC checks in the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet
- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet now displays meaningful error messages on 64-bit systems
- The `-Server` parameter of the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) is now compulsory instead of localhost being default
- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) and [Set-SamAccountPasswordHash](PowerShell/Set-SamAccountPasswordHash.md#set-samaccountpasswordhash) cmdlets now display a warning in case they are supplied with a DNS domain name instead of a NetBIOS one.
- Fixed a bug in SupplementalCredentials parsing

## 2.5 - 2015-09-14
- Both x86 and x64 platforms are now supported.
- A few parameters have been changed and new aliases added.
- Fixed a bug in the [Add-ADDBSidHistory](PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) cmdlet.

## 2.4 - 2015-09-05
- Fixed a bug regarding distinguished name parsing in the [Get-ADDBAccount](PowerShell/Get-ADDBAccount.md#get-addbaccount) cmdlet
- Removed a big memory leak in the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet
- Added the `Get-ADReplicationAccount` alias for [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount)
- Updated AutoMapper to the latest version
- Switched to the official build of Microsoft's Managed Esent libraries
- The module has been published in PowerShell Gallery.

## 2.3
- Parameter `-SystemHiveFilePath` of the [Get-BootKey](PowerShell/Get-BootKey.md#get-bootkey) cmdlet is now positional
- Added the Readme.txt file with system requirements
- Fixed a bug in distinguished name parsing that caused the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet to fail under some circumstances

## 2.2
### Changed
- Added a few parameter validations
### Fixed
- Fixed a bug in SupplementalCredentials parsing

## 2.1
- The [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet can now retrieve all accounts from AD or just a sigle one
- Added Microsoft Visual C++ 2013 Runtime libraries to the distribution
- The module is now 64-bit only
- Minor bug fixes

## 2.0 - 2015-07-14
- Added the [Get-ADDBAccount](PowerShell/Get-ADDBAccount.md#get-addbaccount) cmdlet
- Added the [Get-BootKey](PowerShell/Get-BootKey.md#get-bootkey) cmdlet
- Added the [Get-ADReplAccount](PowerShell/Get-ADReplAccount.md#get-adreplaccount) cmdlet
- Added the [Remove-ADDBObject](PowerShell/Remove-ADDBObject.md#remove-addbobject) cmdlet
- Added the [ConvertTo-Hex](PowerShell/ConvertTo-Hex.md#convertto-hex) cmdlet
- Merged the `DSInternals.Cryptography` assembly into `DSInternals.Common`
- Minor bug fixes

## 1.6
### Added
- Added the [Set-ADDBDomainController](PowerShell/Set-ADDBDomainController.md#set-addbdomaincontroller) cmdlet
- Added the [Get-ADDBSchemaAttribute](PowerShell/Get-ADDBSchemaAttribute.md#get-addbschemaattribute) cmdlet

## 1.5
### Added
- Added the [Get-ADDBDomainController](PowerShell/Get-ADDBDomainController.md#get-addbdomaincontroller) cmdlet

## 1.4 - 2015-05-31
### Added
- Added the [Set-ADDBPrimaryGroup](PowerShell/Set-ADDBPrimaryGroup.md#set-addbprimarygroup) cmdlet

### Fixed
- The [Add-ADDBSidHistory](PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) cmdlet now supports relative file paths

## 1.3.1
### Fixed
- Fixed a bug in the Microsoft.Isam.Esent.Interop library,
  that prevented the [Add-ADDBSidHistory](PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) cmdlet to run on Windows Server 2008 R2

## 1.3 - 2015-05-24
### Added
- Added the [Add-ADDBSidHistory](PowerShell/Add-ADDBSidHistory.md#add-addbsidhistory) cmdlet

## 1.2
### Added
- Added the [ConvertTo-GPPrefPassword](PowerShell/ConvertTo-GPPrefPassword.md#convertto-gpprefpassword) cmdlet

## 1.1
### Added
- Added the [ConvertTo-OrgIdHash](PowerShell/ConvertTo-OrgIdHash.md#convertto-orgidhash) cmdlet
- Added the [ConvertFrom-GPPrefPassword](PowerShell/ConvertFrom-GPPrefPassword.md#convertfrom-gpprefpassword) cmdlet

## 1.0 - 2015-01-20
Initial release!

[Unreleased]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.16...HEAD
[4.16]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.15.1...v4.16
[4.15.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.15...v4.15.1
[4.15]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.14...v4.15
[4.14]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.13...v4.14
[4.13]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.12...v4.13
[4.12]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.11...v4.12
[4.11]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.10...v4.11
[4.10]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.9...v4.10
[4.9]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.8...v4.9
[4.8]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.7...v4.8
[4.7]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.6...v4.7
[4.6]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.5...v4.6
[4.5]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.4.1...v4.5
[4.4.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.4...v4.4.1
[4.4]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.3...v4.4
[4.3]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.2...v4.3
[4.2]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.1...v4.2
[4.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v4.0...v4.1
[4.0]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.6.1...v4.0
[3.6.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.6...v3.6.1
[3.6]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.5...v3.6
[3.5]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.4...v3.5
[3.4]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.3...v3.4
[3.3]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.2.1...v3.3
[3.2.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.2...v3.2.1
[3.2]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.1...v3.2
[3.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v3.0...v3.1
[3.0]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.23...v3.0
[2.23]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.22...v2.23
[2.22]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.21.2...v2.22
[2.21.2]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.21.1...v2.21.2
[2.21.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.21...v2.21.1
[2.21]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.20...v2.21
[2.20]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.19...v2.20
[2.19]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.18...v2.19
[2.18]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.16.1...v2.18
[2.16.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.16...v2.16.1
[2.16]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.15...v2.16
[2.15]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.14...v2.15
[2.14]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.13.1...v2.14
[2.13.1]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.13...v2.13.1
[2.13]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.12...v2.13
[2.12]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.11...v2.12
[2.11]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.10...v2.11
[2.10]: https://github.com/MichaelGrafnetter/DSInternals/compare/v2.9...v2.10
