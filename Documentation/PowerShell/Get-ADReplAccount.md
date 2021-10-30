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
Get-ADReplAccount [-All] [-NamingContext <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByName
```
Get-ADReplAccount [-SamAccountName] <String> [[-Domain] <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByUPN
```
Get-ADReplAccount -UserPrincipalName <String> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### BySID
```
Get-ADReplAccount -ObjectSid <SecurityIdentifier> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByDN
```
Get-ADReplAccount [-DistinguishedName] <String> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByGuid
```
Get-ADReplAccount -ObjectGuid <Guid> -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

## DESCRIPTION
Reads one or more accounts from a target Active Directory domain controller through the MS-DRSR protocol, including secret attributes.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADReplAccount -SamAccountName joe -Server 'lon-dc1.contoso.com'
<# Sample Output:
DistinguishedName: CN=Joe Smith,OU=Employees,DC=contoso,DC=com
Sid: S-1-5-21-1236425271-2880748467-2592687428-1110
Guid: 6fb7aca4-fe85-4dc5-9acd-b5b2529fe2bc
SamAccountName: joe
SamAccountType: User
UserPrincipalName: joe@contoso.com
PrimaryGroupId: 513
SidHistory:
Enabled: True
UserAccountControl: NormalAccount, PasswordNeverExpires
SupportedEncryptionTypes: Default
AdminCount: False
Deleted: False
LastLogonDate: 2/23/2015 10:27:18 AM
DisplayName: Joe Smith
GivenName: Joe
Surname: Smith
Description:
ServicePrincipalName:
SecurityDescriptor: DiscretionaryAclPresent, SystemAclPresent, DiscretionaryAclAutoInherited, SystemAclAutoInherited, SelfRelative
Owner: S-1-5-21-1236425271-2880748467-2592687428-512
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
Key Credentials:
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
#>
```

Replicates a single Active Directory account from the target domain controller.

### Example 2
```powershell
PS C:\> $accounts = Get-ADReplAccount -All -Server 'lon-dc1.contoso.com'
```

Replicates all Active Directory accounts from the target domain controller.

### Example 3
```powershell
PS C:\> $results = Get-ADReplAccount -All -Server 'lon-dc1.contoso.com' |
                   Test-PasswordQuality -WeakPasswordHashesSortedFile pwned-passwords-ntlm-ordered-by-hash-v5.txt
```

Performs an online credential hygiene audit of AD against HIBP.

### Example 4

```powershell
PS C:\> Get-ADReplAccount -All -Server LON-DC1 |
            Format-Custom -View PwDump |
            Out-File -FilePath users.pwdump -Encoding ascii
```

Replicates all Active Directory accounts from the target domain controller and exports their NT and LM password hashes to a pwdump file.

### Example 5
```powershell
PS C:\> Get-ADReplBackupKey -Server 'lon-dc1.adatum.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADReplAccount -All -Server 'lon-dc1.adatum.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
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
Accept pipeline input: True (ByPropertyName)
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
Accept pipeline input: True (ByPropertyName)
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

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADSIAccount](Get-ADSIAccount.md)
[Test-PasswordQuality](Test-PasswordQuality.md)
[Save-DPAPIBlob](Save-DPAPIBlob.md)
