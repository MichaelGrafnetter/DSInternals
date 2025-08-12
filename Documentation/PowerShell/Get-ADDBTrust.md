---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBTrust.md
schema: 2.0.0
---

# Get-ADDBTrust

## SYNOPSIS
Reads inter-domain trust objects from a ntds.dit and decrypts the trust passwords.

## SYNTAX

```
Get-ADDBTrust [-BootKey <Byte[]>] -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
Reads all AD forest and MIT realm trust objects from the specified ntds.dit file.
The cmdlet decrypts the trust passwords and derives the Kerberos trust keys.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBTrust -DatabasePath 'C:\ADBackup\ntds.dit' -BootKey c53a1d6ce3b391432863073cea763915

<# Sample Output:
DistinguishedName: CN=adatum.com,CN=System,DC=contoso,DC=com
TrustPartner: adatum.com
FlatName: adatum
Sid: S-1-5-21-2072939287-465948493-1385512467
Direction: Bidirectional
Source: contoso.com
SourceFlatName: contoso
Type: Uplevel
Attributes: ForestTransitive
SupportedEncryptionTypes: AES128_CTS_HMAC_SHA1_96, AES256_CTS_HMAC_SHA1_96
Deleted: False
TrustAuthIncoming
  CurrentPassword: 鑵׶肞뚙ᝑ꣤ς搏ﴲᛍ⨾녰钳맦
  CurrentNTHash: a00b29a3ab2fe08bf169096798193290
  PreviousPassword: Pa$$w0rd
  PreviousNTHash: 92937945b518814341de3f726500d4ff
IncomingTrustKeys (Calculated)
  Credentials:
    AES256_CTS_HMAC_SHA1_96
      Key: f253328c380a20b24c59866ab5a4f222a7fdec9de05502b261de6bbccd392da9
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: 039d99f0b5c78bd7d07e0fed28fe2cf8
      Iterations: 4096
    DES_CBC_MD5
      Key: 0ee92c61b66b5d0d
      Iterations: 4096
  OldCredentials:
    AES256_CTS_HMAC_SHA1_96
      Key: ab18197b48942fcbb8dab398f1b78fcbad8a223ff6779eb332f42f21655f5aa0
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: 676c6a1e69f0ec7d78010e75e9c24b6f
      Iterations: 4096
    DES_CBC_MD5
      Key: 2afbc7d94fa4ab29
      Iterations: 4096
  OlderCredentials:
  ServiceCredentials:
  Salt: CONTOSO.COMkrbtgtadatum
  DefaultIterationCount: 4096
  Flags: 0
TrustAuthOutgoing
  CurrentPassword: 쩘僞◀ꝵ黠鯹안꽾仈퍯䢥鉑꾲
  CurrentNTHash: ea1d78e82a3e496eb65ccd9a108575d0
  PreviousPassword: Pa$$w0rd
  PreviousNTHash: 92937945b518814341de3f726500d4ff
OutgoingTrustKeys (Calculated)
  Credentials:
    AES256_CTS_HMAC_SHA1_96
      Key: 25668ca9f03154e3cf0509a01f51bb3a5fcac8200e69eb542e6f2ad4609d39ce
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: 65a4c7a238f2cf8146f15db4dfda4bad
      Iterations: 4096
    DES_CBC_MD5
      Key: d90425dc58571a86
      Iterations: 4096
  OldCredentials:
    AES256_CTS_HMAC_SHA1_96
      Key: 214a5078f4fdb6405ca669a4ce9662cb631989d331585ce115c769c7218f6583
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: efc764b4de373d40c3e9b173c0ee3a47
      Iterations: 4096
    DES_CBC_MD5
      Key: 9ec1cbd9163da72a
      Iterations: 4096
  OlderCredentials:
  ServiceCredentials:
  Salt: ADATUM.COMkrbtgtcontoso
  DefaultIterationCount: 4096
  Flags: 0

DistinguishedName: CN=MIT.EDU,CN=System,DC=contoso,DC=com
TrustPartner: MIT.EDU
FlatName: MIT.EDU
Sid:
Direction: Outbound
Source: contoso.com
SourceFlatName: contoso
Type: MIT
Attributes: DisallowTransivity
SupportedEncryptionTypes:
Deleted: False
TrustAuthIncoming
  CurrentPassword:
  CurrentNTHash:
  PreviousPassword:
  PreviousNTHash:
IncomingTrustKeys (Calculated)
TrustAuthOutgoing
  CurrentPassword: Pa$$w0rd
  CurrentNTHash: 92937945b518814341de3f726500d4ff
  PreviousPassword: Pa$$w0rd
  PreviousNTHash: 92937945b518814341de3f726500d4ff
OutgoingTrustKeys (Calculated)
  Credentials:
    AES256_CTS_HMAC_SHA1_96
      Key: 86382b311288ae8e1bf0157ac93849ae4f4f84a9a2e71aea57c2a8936067f486
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: b28d2d6afd811c05de733ae143cf9d06
      Iterations: 4096
    DES_CBC_MD5
      Key: 3bd940c1f79b79ce
      Iterations: 4096
  OldCredentials:
    AES256_CTS_HMAC_SHA1_96
      Key: 86382b311288ae8e1bf0157ac93849ae4f4f84a9a2e71aea57c2a8936067f486
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: b28d2d6afd811c05de733ae143cf9d06
      Iterations: 4096
    DES_CBC_MD5
      Key: 3bd940c1f79b79ce
      Iterations: 4096
  OlderCredentials:
  ServiceCredentials:
  Salt: MIT.EDUkrbtgtcontoso
  DefaultIterationCount: 4096
  Flags: 0
#>
```

Reads all AD forest and MIT realm trust objects from the specified ntds.dit file.

## PARAMETERS

### -BootKey
Specifies the boot key (AKA system key) that will be used to decrypt values of secret attributes.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: key, SysKey, SystemKey

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabasePath
Specifies the path to a domain database, for instance, C:\Windows\NTDS\ntds.dit.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Database, DBPath, DatabaseFilePath, DBFilePath

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogPath
Specifies the path to a directory where the transaction log files are located. For instance, C:\Windows\NTDS. The default log directory is the one that contains the database file itself.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Log, TransactionLogPath

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.Common.Kerberos.TrustedDomain

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
