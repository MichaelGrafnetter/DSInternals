---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-DpapiNgPfxCertificate.md
schema: 2.0.0
---

# Get-DpapiNgPfxCertificate

## SYNOPSIS
Extracts the SID-based DPAPI-NG certificate password protector from a PFX file.

## SYNTAX

```
Get-DpapiNgPfxCertificate [-Path] <String> [<CommonParameters>]
```

## DESCRIPTION

This cmdlet parses a PKCS #12 PFX file and returns the SID-protected certificate password metadata.
The returned object contains the PFX file path and the DPAPI-NG encrypted certificate password protector.

To decrypt the protected password, pipe the returned object to [Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md#unprotect-dpapingpfxcertificate).

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-Item -Path C:\Certificates\*.pfx | Get-DpapiNgPfxCertificate

<# Sample Output:
FilePath: C:\Certificates\ContosoWildcard.pfx
Password:
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
Password:
EncryptedPassword
  Descriptor: SID=S-1-5-21-3288850392-3299536932-2614793081-512
  ContentEncryptionAlgorithm: aes256gcm (2.16.840.1.101.3.4.1.46)
  SID/SDDL Protectors
    SID=S-1-5-21-3288850392-3299536932-2614793081-512
      ProtectionKeyIdentifier: RootKey=1c556b71-ed22-c45f-723c-ddbe199f6824, Cycle=5/22/2026 6:00:00 AM (L0=364, L1=4, L2=22)
      KeyEncryptionAlgorithm: aes256wrap (2.16.840.1.101.3.4.1.45)
#>
```

Enumerates all PFX files in the `C:\Certificates` directory and displays their SID-based protection metadata. The `Password` field is empty because the protected password has not been decrypted yet; pipe the result to [Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md) to recover it.

## PARAMETERS

### -Path
Specifies the path to the PFX file.

```yaml
Type: String
Parameter Sets: (All)
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

## OUTPUTS

### DSInternals.PowerShell.PfxProtectedPassword

## NOTES

Alias: `Get-CngDpapiPfxCertificate`

## RELATED LINKS

[Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
