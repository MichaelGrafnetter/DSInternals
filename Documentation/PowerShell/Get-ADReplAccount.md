---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADReplAccount.md
schema: 2.0.0
---

# Get-ADReplAccount

## SYNOPSIS
Reads one or more accounts through the MS-DRSR protocol, including secret attributes.

## SYNTAX

### All
```
Get-ADReplAccount [-All] [-NamingContext <String>] [-Properties <AccountPropertySets>]
 [-ExportFormat <AccountExportFormat>] -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

### ByName
```
Get-ADReplAccount [-Properties <AccountPropertySets>] [-ExportFormat <AccountExportFormat>]
 [-SamAccountName] <String> [[-Domain] <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByUPN
```
Get-ADReplAccount [-Properties <AccountPropertySets>] [-ExportFormat <AccountExportFormat>]
 -UserPrincipalName <String> -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

### BySID
```
Get-ADReplAccount [-Properties <AccountPropertySets>] [-ExportFormat <AccountExportFormat>]
 -ObjectSid <SecurityIdentifier> -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

### ByDN
```
Get-ADReplAccount [-Properties <AccountPropertySets>] [-ExportFormat <AccountExportFormat>]
 [-DistinguishedName] <String> -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

### ByGuid
```
Get-ADReplAccount [-Properties <AccountPropertySets>] [-ExportFormat <AccountExportFormat>] -ObjectGuid <Guid>
 -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>] [<CommonParameters>]
```

## DESCRIPTION
Reads one or more accounts from a target Active Directory domain controller through the MS-DRSR protocol, including secret attributes.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADReplAccount -SamAccountName joe -Server 'lon-dc1.contoso.com'
<# Sample Output:
DistinguishedName: CN=Joe Smith,OU=Employees,DC=contoso,DC=com
SamAccountName: joe
UserPrincipalName: joe@contoso.com
Enabled: True
Deleted: False
Sid: S-1-5-21-1236425271-2880748467-2592687428-1110
Guid: 6fb7aca4-fe85-4dc5-9acd-b5b2529fe2bc
SamAccountType: User
UserAccountControl: NormalAccount, PasswordNeverExpires
Description: Joe's standard account
Notes: Lorem Ipsum
AdminCount: False
PrimaryGroupId: 513
SidHistory:
SupportedEncryptionTypes: Default
ServicePrincipalName:
LastLogonDate:  2/23/2015 10:27:18 AM
PasswordLastSet: 7/11/2014 11:35:47 AM
SecurityDescriptor: DiscretionaryAclPresent, SystemAclPresent, DiscretionaryAclAutoInherited, SystemAclAutoInherited, SelfRelative
GivenName: Joe
Surname: Smith
Initials: RR
DisplayName: Joe Smith
Email: joe@contoso.com
StreetAddress: 1 Microsoft Way
City: Redmond
PostalCode: 98052
State: Washington
Country: US
Office: A309
TelephoneNumber: 0118 999 881 999 119 7253
Mobile: (312) 555-0690
HomePhone: (408) 555-1439
Company: Contoso
Department: IT
JobTitle: Admin
EmployeeID:
EmployeeNumber:
ProfilePath:
ScriptPath:
Key Credentials
  Usage=NGC, Source=ActiveDirectory, Device=1966d4da-14da-4581-a7a7-5e8e07e93ad9, Created=8/1/2019 10:53:12 PM, LastLogon=8/1/2019 10:53:12 PM
  Usage=NGC, Source=ActiveDirectory, Device=cfe9a872-13ff-4751-a777-aec88c30a762, Created=8/1/2019 11:09:15 PM, LastLogon=8/1/2019 11:09:15 PM
Credential Roaming
  Created: 3/12/2017 9:15:56 AM
  Modified: 3/13/2017 10:01:18 AM
  Credentials:
    DPAPIMasterKey: joe\Protect\S-1-5-21-1236425271-2880748467-2592687428-1110\47070660-c259-4d90-8bc9-187605323450
    DPAPIMasterKey: joe\Protect\S-1-5-21-1236425271-2880748467-2592687428-1110\7fc19508-7b85-4a7c-9e5d-15f9e00e7ce5
    CryptoApiCertificate: joe\SystemCertificates\My\Certificates\574E4687133998544C0095C7B348C52CD398182E
    CNGCertificate: joe\SystemCertificates\My\Certificates\3B83BFA7037F6A79B3F3D17D229E1BC097F35B51
    RSAPrivateKey: joe\Crypto\RSA\S-1-5-21-1236425271-2880748467-2592687428-1110\701577141985b6923998dcca035c007a_f8b7bbef-d227-4ac7-badd-3a238a7f741e
    CNGPrivateKey: joe\Crypto\Keys\E8F13C2BA0209401C4DFE839CD57375E26BBE38F
Secrets
  NTHash: 92937945b518814341de3f726500d4ff
  LMHash:
  NTHashHistory:
    Hash 01: 92937945b518814341de3f726500d4ff
  LMHashHistory:
    Hash 01: 30ce97eef1084cf1656cc4be70d68600
  SupplementalCredentials:
    ClearText:
    NTLMStrongHash: 2c6d57beebeafdae65b3f40f2a0d5430
    Kerberos:
      Credentials:
        DES_CBC_MD5
          Key: 7f16bc4ada0b8a52
      OldCredentials:
      Salt: CONTOSO.COMjoe
      Flags: 0
    KerberosNew:
      Credentials:
        AES256_CTS_HMAC_SHA1_96
          Key: cd541be0838c787b5c6a34d7b19274aee613545a0e6cc6f5ac5918d8a464d24f
          Iterations: 4096
        AES128_CTS_HMAC_SHA1_96
          Key: 5c88972747bd454704c117ae52c474e4
          Iterations: 4096
        DES_CBC_MD5
          Key: 7f16bc4ada0b8a52
          Iterations: 4096
      OldCredentials:
      OlderCredentials:
      ServiceCredentials:
      Salt: CONTOSO.COMjoe
      DefaultIterationCount: 4096
      Flags: 0
    WDigest:
      Hash 01: 61fed940f0e8d03a49d3727f55800497
      Hash 02: a1d54499dda6a6b5431f29a8d741a640
      Hash 03: b6cdf00bc0c4578992f718de81251721
      Hash 04: 61fed940f0e8d03a49d3727f55800497
      Hash 05: a1d54499dda6a6b5431f29a8d741a640
      Hash 06: 9a8991bd99763df2e37f1e1e67d71cc8
      Hash 07: 61fed940f0e8d03a49d3727f55800497
      Hash 08: 8a9fe94883c8ccf3bcfc6591ddd2288f
      Hash 09: 8a9fe94883c8ccf3bcfc6591ddd2288f
      Hash 10: 1b7b16b49ecd8d9d59c1d0db6fa2cc36
      Hash 11: d4c24695cfa4dc3810a469d5efb8ecaf
      Hash 12: 8a9fe94883c8ccf3bcfc6591ddd2288f
      Hash 13: a5b8aa5088280298c8c27fa99dcaa1e3
      Hash 14: d4c24695cfa4dc3810a469d5efb8ecaf
      Hash 15: 1aa8e567622fe53d6fb36f1f34f12aaa
      Hash 16: 1aa8e567622fe53d6fb36f1f34f12aaa
      Hash 17: 2af425244079f8f45927c34fa115e45b
      Hash 18: cf283a35102b820e25003b1ddf270221
      Hash 19: b98c902c57449253e6f06b5d585866bd
      Hash 20: 2a690b1eeda9cb8f3157a4a3ba0be9c3
      Hash 21: af2654776d5f9f27f3283ecb0aa25011
      Hash 22: af2654776d5f9f27f3283ecb0aa25011
      Hash 23: ba6fe0513ed2a60ec253a41bbde6a837
      Hash 24: 8bf5a67b598087be948e040f85c72b4d
      Hash 25: 8bf5a67b598087be948e040f85c72b4d
      Hash 26: aa5ff46d23a5c7ebd603e1793225350d
      Hash 27: 656b6a7f5b52d05b3ce9168a2b7ac8ac
      Hash 28: ae884c92ecd87e8d54f1844f09c5a519
      Hash 29: a500a9e26afc9f817df8a07e15771577
#>
```

Replicates a single Active Directory user account from the target domain controller.

### Example 2
```powershell
PS C:\> Get-ADReplAccount -SamAccountName 'PC01$' -Server 'lon-dc1.contoso.com'
<# Sample Output:
DistinguishedName: CN=PC01,CN=Computers,DC=contoso,DC=com
SamAccountName: PC01$
Enabled: True
Deleted: False
Sid: S-1-5-21-2072841070-1873892158-2095746001-1104
Guid: 34017f6d-a264-4681-8738-09780122884f
SamAccountType: Computer
UserAccountControl: WorkstationAccount
DNSHostName: PC01.contoso.com
OperatingSystem: Windows 11 Enterprise
OperatingSystemVersion: 10.0 (26100)
Description: John's computer
PrimaryGroupId: 515
Location: USA/WA/Seattle
SidHistory:
SupportedEncryptionTypes: RC4_HMAC, AES128_CTS_HMAC_SHA1_96, AES256_CTS_HMAC_SHA1_96
ServicePrincipalName: {HOST/PC01.contoso.com, RestrictedKrbHost/PC01.contoso.com, HOST/PC01, RestrictedKrbHost/PC01...}
LastLogonDate: 1/27/2025 9:22:36 AM
PasswordLastSet: 1/22/2025 9:23:45 PM
SecurityDescriptor: DiscretionaryAclPresent, SystemAclPresent, DiscretionaryAclAutoInherited, SystemAclAutoInherited, SelfRelative
LAPS
Key Credentials
  Usage: NGC, Source: AD, Device: , Created: 11/23/2024 10:58:30 PM
Secrets
  NTHash: 0ec8485560274b5352fab8085f83f5cf
  LMHash:
  NTHashHistory:
    Hash 01: 0ec8485560274b5352fab8085f83f5cf
    Hash 02: d3981b0fa179f60b3eac48ea0aa06b62
    Hash 03: f6ab2345d24e09993c972087d189a365
  LMHashHistory:
    Hash 01: ecb0097500ffd72b005071e31a237ed5
    Hash 02: 1d16a311401fba1f5d95090cb4fcacdb
    Hash 03: 1224652b76e22751d79a06a7ce796e56
  SupplementalCredentials:
    ClearText:
    NTLMStrongHash:
    Kerberos:
    KerberosNew:
      Credentials:
        AES256_CTS_HMAC_SHA384_192
          Key: f97e0809c70a0c88aa5e6bc2d891f44e56ded641425a9bb0e9468f83a89b23d1
          Iterations: 4096
        AES128_CTS_HMAC_SHA256_128
          Key: b5bf869da88f6d23c7a7e0585fc4d385
          Iterations: 4096
        AES256_CTS_HMAC_SHA1_96
          Key: 099378565ec8edb5be48624bf1af8569c63963a28cb5a2e165668b618bb39d0c
          Iterations: 4096
        AES128_CTS_HMAC_SHA1_96
          Key: 483599c78841e3d14c91283a33ceaa38
          Iterations: 4096
        RC4_HMAC_NT
          Key: 0ec8485560274b5352fab8085f83f5cf
          Iterations: 4096
      OldCredentials:
        AES256_CTS_HMAC_SHA384_192
          Key: ab6e6e6c22a3689cccfaee4a46744cd5fa25b466992246b9d5474090dc6a6d32
          Iterations: 4096
        AES128_CTS_HMAC_SHA256_128
          Key: cca81c6d197a8619f81d67db8b2f510f
          Iterations: 4096
        AES256_CTS_HMAC_SHA1_96
          Key: 7a59559a82e25d7c3b11ba9746a0af776905d404e17c07be9dc7f45689600017
          Iterations: 4096
        AES128_CTS_HMAC_SHA1_96
          Key: 6203b2af5353b22f7948f4dc393fc2e3
          Iterations: 4096
        RC4_HMAC_NT
          Key: d3981b0fa179f60b3eac48ea0aa06b62
          Iterations: 4096
      OlderCredentials:
        AES256_CTS_HMAC_SHA384_192
          Key: 662eb233817c7f92fe89d956b66522193848c94851176a962c76092932e195bd
          Iterations: 4096
        AES128_CTS_HMAC_SHA256_128
          Key: 36b9535feaf1127aa3f7b4940345b6b9
          Iterations: 4096
        AES256_CTS_HMAC_SHA1_96
          Key: 3bed800004f155ad9d7b7492ca1b4902b3dca64dbca503b94d8cdc622b3a69aa
          Iterations: 4096
        AES128_CTS_HMAC_SHA1_96
          Key: 29f2bbec3fb8a177c788afc4c24d368a
          Iterations: 4096
        RC4_HMAC_NT
          Key: f6ab2345d24e09993c972087d189a365
          Iterations: 4096
      ServiceCredentials:
        AES256_CTS_HMAC_SHA384_192
          Key: f97e0809c70a0c88aa5e6bc2d891f44e56ded641425a9bb0e9468f83a89b23d1
          Iterations: 4096
        AES128_CTS_HMAC_SHA256_128
          Key: b5bf869da88f6d23c7a7e0585fc4d385
          Iterations: 4096
        AES256_CTS_HMAC_SHA1_96
          Key: 099378565ec8edb5be48624bf1af8569c63963a28cb5a2e165668b618bb39d0c
          Iterations: 4096
        AES128_CTS_HMAC_SHA1_96
          Key: 483599c78841e3d14c91283a33ceaa38
          Iterations: 4096
      Salt: CONTOSO.COMhostpc01.contoso.com
      DefaultIterationCount: 4096
      Flags: 0
    WDigest:
      Hash 01: 51ec1c08dc1515d691f86c35efd6951a
      Hash 02: 0804fe417162838cee1b585e05403c7e
      Hash 03: 51ec1c08dc1515d691f86c35efd6951a
      Hash 04: 51ec1c08dc1515d691f86c35efd6951a
      Hash 05: f7dfe69378cd20462567a3f12d306d9c
      Hash 06: f7dfe69378cd20462567a3f12d306d9c
      Hash 07: 84d3754825bb778f150e2e0f9e0ad791
      Hash 08: c3f6531e37407660f4eaab3685b10c77
      Hash 09: 7c9c69f0f4af01f24d00cd7fd40f0ad8
      Hash 10: abbb73611689c4726a81d89adbb58a76
      Hash 11: abbb73611689c4726a81d89adbb58a76
      Hash 12: c3f6531e37407660f4eaab3685b10c77
      Hash 13: c3f6531e37407660f4eaab3685b10c77
      Hash 14: 2597e2d6baeb6a27746efa36afb108ce
      Hash 15: b7e76a5d9451f3e9dbfcbab122dde4a7
      Hash 16: 299ed0932d98d3c08f0037402cc4b025
      Hash 17: 13188b1800edb36ce4a5ebae8bdc163d
      Hash 18: cd9d520759c3919e442437eb84d3fecf
      Hash 19: 7eb065737a969f09daf7650ca1aa9441
      Hash 20: cd9d520759c3919e442437eb84d3fecf
      Hash 21: b491f3531400f2a25ac123c74f698109
      Hash 22: 55ab8a2e40e730d1d7426d5b71842021
      Hash 23: b491f3531400f2a25ac123c74f698109
      Hash 24: eec9350591609e3caef02e6ebc5b5da5
      Hash 25: 9046b70b3f463105ac60d72d14d083ee
      Hash 26: 45a252b159aec8f9e6a6c18dac468b47
      Hash 27: a5e2fb397f93c24f53c055bb8e38975a
      Hash 28: db978e9c18bca0774a2b53ddf9c20d45
      Hash 29: a5e2fb397f93c24f53c055bb8e38975a
#>
```

Replicates a single Active Directory computer account from the target domain controller.

### Example 3
```powershell
PS C:\> $accounts = Get-ADReplAccount -All -Server 'lon-dc1.contoso.com'
```

Replicates all Active Directory accounts from the target domain controller.

### Example 4
```powershell
PS C:\> $results = Get-ADReplAccount -All -Server 'lon-dc1.contoso.com' -Properties Secrets |
                   Test-PasswordQuality -WeakPasswordHashesSortedFile pwned-passwords-ntlm-ordered-by-hash-v5.txt
```

Performs an online credential hygiene audit of AD against HIBP.

### Example 5

```powershell
PS C:\> Get-ADReplAccount -All -Server LON-DC1 -ExportFormat PwDump |
            Where-Object SamAccountType -eq User |
            Where-Object Enabled -eq $true |
            Where-Object NTHash -ne $null |
            Out-File -FilePath users.pwdump -Encoding ascii
```

Replicates all Active Directory accounts from the target domain controller and exports their NT and LM password hashes to a pwdump file.

### Example 6
```powershell
PS C:\> Get-ADReplBackupKey -Server 'lon-dc1.adatum.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADReplAccount -All -Server 'lon-dc1.adatum.com' -Properties RoamedCredentials | Save-DPAPIBlob -DirectoryPath '.\Output'
```

Replicates all DPAPI backup keys and roamed credentials (certificates, private keys, and DPAPI master keys) from the target Active Directory domain controller and saves them to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

## PARAMETERS

### -All
Indidates that all accounts will be replicated from the target domain controller.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: AllAccounts, ReturnAllAccounts

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credential
Specifies a user account that has permission to perform this action. The default is the current user.

```yaml
Type: PSCredential
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DistinguishedName
Specifies the identifier of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByDN
Aliases: dn

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Domain
Specifies the NetBIOS domain name of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByName
Aliases: AccountDomain, UserDomain

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExportFormat
Specifies the format in which the account information will be displayed.

```yaml
Type: AccountExportFormat
Parameter Sets: (All)
Aliases: View, ExportView, Format
Accepted values: JohnNT, JohnNTHistory, JohnLM, JohnLMHistory, HashcatNT, HashcatNTHistory, HashcatLM, HashcatLMHistory, NTHash, NTHashHistory, LMHash, LMHashHistory, Ophcrack, PWDump, PWDumpHistory

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NamingContext
Specifies the naming context root of the replica to replicate.

```yaml
Type: String
Parameter Sets: All
Aliases: NC, DomainNC, DomainNamingContext

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectGuid
Specifies the identifier of the account that will be replicated.

```yaml
Type: Guid
Parameter Sets: ByGuid
Aliases: Guid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ObjectSid
Specifies the identifier of the account that will be replicated.

```yaml
Type: SecurityIdentifier
Parameter Sets: BySID
Aliases: Sid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Properties
Specifies the set of properties that will be retrieved for each account.

```yaml
Type: AccountPropertySets
Parameter Sets: (All)
Aliases: Property, PropertySets, PropertySet
Accepted values: None, DistinguishedName, GenericAccountInfo, GenericUserInfo, GenericComputerInfo, GenericInfo, SecurityDescriptor, NTHash, LMHash, PasswordHashes, NTHashHistory, LMHashHistory, PasswordHashHistory, SupplementalCredentials, Secrets, KeyCredentials, RoamedCredentials, WindowsLAPS, LegacyLAPS, LAPS, ManagedBy, Manager, All

Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -Protocol
Specifies the protocol sequence that is used for RPC communication.

```yaml
Type: RpcProtocol
Parameter Sets: (All)
Aliases: Proto, RPCProtocol, NCACN
Accepted values: TCP, SMB, HTTP

Required: False
Position: Named
Default value: TCP
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamAccountName
Specifies the identifier of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, sam, AccountName, User

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Server
Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Host, DomainController, DC

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
Specifies the identifier of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByUPN
Aliases: UPN

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### DSInternals.Common.Data.DSAccount

### DSInternals.Common.Data.DSUser

### DSInternals.Common.Data.DSComputer

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADSIAccount](Get-ADSIAccount.md)
[Test-PasswordQuality](Test-PasswordQuality.md)
[Save-DPAPIBlob](Save-DPAPIBlob.md)
