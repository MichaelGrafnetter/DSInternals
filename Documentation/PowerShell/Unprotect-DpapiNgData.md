---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Unprotect-DpapiNgData.md
schema: 2.0.0
---

# Unprotect-DpapiNgData

## SYNOPSIS
Decrypts DPAPI-NG protected data.

## SYNTAX

### Online (Default)
```
Unprotect-DpapiNgData [-Blob] <Byte[]> [-Encoding <Encoding>] [<CommonParameters>]
```

### Offline
```
Unprotect-DpapiNgData [-Blob] <Byte[]> -KdsRootKey <KdsRootKey[]> [-Encoding <Encoding>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet decrypts a DPAPI-NG protected blob. The `Blob` parameter accepts either a byte array or a base64-encoded `CngProtectedDataBlob`.

By default, the cmdlet returns the decrypted data as a hexadecimal string. When `Encoding` is supplied, the cmdlet decodes the decrypted bytes and returns the decoded string.
When `KdsRootKey` is supplied, the cmdlet derives and caches the matching SID group key before decrypting the blob, which enables offline decryption for SID-protected data.

## EXAMPLES

### Example 1
```powershell
PS C:\> $blob = Protect-DpapiNgData -Descriptor 'LOCAL=user' -Cleartext 'Secret'
PS C:\> Unprotect-DpapiNgData -Blob $blob -Encoding ([System.Text.Encoding]::Unicode)
```

Decrypts a protected blob and returns the cleartext as a string.

### Example 2
```powershell
PS C:\> $rootKeys = Get-ADDBKdsRootKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
PS C:\> Unprotect-DpapiNgData -Blob $blob -KdsRootKey $rootKeys -Encoding ([System.Text.Encoding]::Unicode)
```

Uses KDS root keys from an offline Active Directory database to decrypt SID-protected data.

## PARAMETERS

### -Blob
Specifies the DPAPI-NG protected blob as a byte array or base64 string.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: CngProtectedDataBlob, ProtectedBlob

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Encoding
Specifies the text encoding to use when returning decrypted data as a string.
Accepts a `System.Text.Encoding` instance (such as `[System.Text.Encoding]::Unicode`) or one of the following well-known encoding names, which are also offered through tab completion: `ASCII`, `BigEndianUnicode`, `Unicode`, `UTF32`, `UTF7`, `UTF8`.

```yaml
Type: System.Text.Encoding
Parameter Sets: (All)
Aliases: None

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KdsRootKey
Specifies the KDS root keys used to decrypt SID-protected data offline.

```yaml
Type: KdsRootKey[]
Parameter Sets: Offline
Aliases: KdsRootKeys, RootKey, RootKeys

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Byte[]

### System.String

## OUTPUTS

### System.String

## NOTES

Alias: `Unprotect-CngDpapiData`

## RELATED LINKS

[Get-DpapiNgData](Get-DpapiNgData.md)
[Protect-DpapiNgData](Protect-DpapiNgData.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
