---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Unprotect-DpapiNgPfxCertificate.md
schema: 2.0.0
---

# Unprotect-DpapiNgPfxCertificate

## SYNOPSIS
Decrypts the SID-based DPAPI-NG certificate password protector from a PFX file.

## SYNTAX

### Path (Default)
```
Unprotect-DpapiNgPfxCertificate [-Path] <String> [-KdsRootKey <KdsRootKey[]>] [<CommonParameters>]
```

### InputObject
```
Unprotect-DpapiNgPfxCertificate [-InputObject] <PfxProtectedPassword> [-KdsRootKey <KdsRootKey[]>]
 [<CommonParameters>]
```

## DESCRIPTION

This cmdlet decrypts the SID-protected certificate password embedded in a PKCS #12 PFX file and returns the populated `PfxProtectedPassword` object with the cleartext password attached.

When the `KdsRootKey` parameter is not supplied, decryption is attempted online by using the current security context.
When `KdsRootKey` is supplied, the cmdlet uses the matching KDS root key to derive and cache the SID group key before decrypting the certificate password offline.

The input can be supplied either as a file `Path` or as a `PfxProtectedPassword` object (typically piped from [Get-DpapiNgPfxCertificate](Get-DpapiNgPfxCertificate.md#get-dpapingpfxcertificate)).

## EXAMPLES

### Example 1
```powershell
PS C:\> Unprotect-DpapiNgPfxCertificate -Path C:\Certificates\ContosoWildcard.pfx |
            Select-Object FilePath, Password

<# Sample Output:
FilePath                            Password
--------                            --------
C:\Certificates\ContosoWildcard.pfx 7+qwxfTcEBB2ySg5UipSl4fskvjwF6pqf4DEvlB0q7M=
#>
```

Decrypts the SID-protected certificate password by using the current security context.

### Example 2
```powershell
PS C:\> $rootKeys = Get-ADDBKdsRootKey -DatabasePath '.\ntds.dit'
PS C:\> Unprotect-DpapiNgPfxCertificate -Path C:\Certificates\ContosoWildcard.pfx -KdsRootKey $rootKeys |
            Select-Object FilePath, Password

<# Sample Output:
FilePath                            Password
--------                            --------
C:\Certificates\ContosoWildcard.pfx 7+qwxfTcEBB2ySg5UipSl4fskvjwF6pqf4DEvlB0q7M=
#>
```

Uses KDS root keys from an offline Active Directory database to decrypt the protected certificate password.

### Example 3
```powershell
PS C:\> Get-DpapiNgPfxCertificate -Path C:\Certificates\ContosoWildcard.pfx |
            Unprotect-DpapiNgPfxCertificate |
            Select-Object FilePath, Password

<# Sample Output:
FilePath                            Password
--------                            --------
C:\Certificates\ContosoWildcard.pfx 7+qwxfTcEBB2ySg5UipSl4fskvjwF6pqf4DEvlB0q7M=
#>
```

Pipes a previously parsed `PfxProtectedPassword` into the decryption cmdlet.

### Example 4
```powershell
PS C:\> Get-Item -Path C:\Certificates\*.pfx | Unprotect-DpapiNgPfxCertificate

<# Sample Output:
FilePath: C:\Certificates\ContosoWildcard.pfx
Password: 7+qwxfTcEBB2ySg5UipSl4fskvjwF6pqf4DEvlB0q7M=
EncryptedPassword
  Descriptor: SID=S-1-5-21-3288850392-3299536932-2614793081-519 OR SID=S-1-5-21-3288850392-3299536932-2614793081-512
  ContentEncryptionAlgorithm: aes256gcm (2.16.840.1.101.3.4.1.46)
  SID/SDDL Protectors
    SID=S-1-5-21-3288850392-3299536932-2614793081-519
      ProtectionKeyIdentifier: RootKey=1c556b71-ed22-c45f-723c-ddbe199f6824, Cycle=5/22/2026 6:00:00 AM (L0=364, L1=4, L2=22)
      KeyEncryptionAlgorithm: aes256wrap (2.16.840.1.101.3.4.1.45)
    SID=S-1-5-21-3288850392-3299536932-2614793081-512
      ProtectionKeyIdentifier: RootKey=1c556b71-ed22-c45f-723c-ddbe199f6824, Cycle=5/22/2026 6:00:00 AM (L0=364, L1=4, L2=22)
      KeyEncryptionAlgorithm: aes256wrap (2.16.840.1.101.3.4.1.45)

FilePath: C:\Certificates\ContosoWebServer.pfx
Password: SUUlfn+1+S/9cmak+Rnd7tBK6yRSQWTAOMOXx8wCMZM=
EncryptedPassword
  Descriptor: SID=S-1-5-21-3288850392-3299536932-2614793081-512
  ContentEncryptionAlgorithm: aes256gcm (2.16.840.1.101.3.4.1.46)
  SID/SDDL Protectors
    SID=S-1-5-21-3288850392-3299536932-2614793081-512
      ProtectionKeyIdentifier: RootKey=1c556b71-ed22-c45f-723c-ddbe199f6824, Cycle=5/22/2026 6:00:00 AM (L0=364, L1=4, L2=22)
      KeyEncryptionAlgorithm: aes256wrap (2.16.840.1.101.3.4.1.45)
#>
```

Enumerates all PFX files in the `C:\Certificates` directory and decrypts their SID-protected passwords by using the current security context. In contrast to [Get-DpapiNgPfxCertificate](Get-DpapiNgPfxCertificate.md), the `Password` field now holds the recovered cleartext.

## PARAMETERS

### -InputObject
Specifies an already-loaded `PfxProtectedPassword` to decrypt.

```yaml
Type: PfxProtectedPassword
Parameter Sets: InputObject
Aliases: ProtectedPassword

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -KdsRootKey
Specifies KDS root keys that can be used to decrypt the protected certificate password offline.

```yaml
Type: KdsRootKey[]
Parameter Sets: (All)
Aliases: KdsRootKeys, RootKey, RootKeys

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies the path to the PFX file to load and decrypt.

```yaml
Type: String
Parameter Sets: Path
Aliases: FilePath, FullName, PfxPath

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### DSInternals.PowerShell.PfxProtectedPassword

## OUTPUTS

### DSInternals.PowerShell.PfxProtectedPassword

## NOTES

Alias: `Unprotect-CngDpapiPfxCertificate`

## RELATED LINKS

[Get-DpapiNgPfxCertificate](Get-DpapiNgPfxCertificate.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
