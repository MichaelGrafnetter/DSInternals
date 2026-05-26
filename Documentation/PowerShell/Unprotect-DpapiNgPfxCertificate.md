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
PS C:\> Unprotect-DpapiNgPfxCertificate -Path .\Certificate.pfx |
            Select-Object FilePath, Password
```

Decrypts the SID-protected certificate password by using the current security context.

### Example 2
```powershell
PS C:\> $rootKeys = Get-ADDBKdsRootKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
PS C:\> Unprotect-DpapiNgPfxCertificate -Path .\Certificate.pfx -KdsRootKey $rootKeys |
            Select-Object FilePath, Password
```

Uses KDS root keys from an offline Active Directory database to decrypt the protected certificate password.

### Example 3
```powershell
PS C:\> Get-DpapiNgPfxCertificate -Path .\Certificate.pfx |
            Unprotect-DpapiNgPfxCertificate |
            Select-Object FilePath, Password
```

Pipes a previously parsed `PfxProtectedPassword` into the decryption cmdlet.

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
