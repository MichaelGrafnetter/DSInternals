---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Protect-DpapiNgData.md
schema: 2.0.0
---

# Protect-DpapiNgData

## SYNOPSIS
Encrypts text with DPAPI-NG.

## SYNTAX

### Descriptor (Default)
```
Protect-DpapiNgData [-Descriptor] <String> [-Cleartext] <String> [-Encoding <Encoding>] [<CommonParameters>]
```

### NamedDescriptor
```
Protect-DpapiNgData [-NamedDescriptor] <String> [-Machine] [-Cleartext] <String> [-Encoding <Encoding>]
 [<CommonParameters>]
```

## DESCRIPTION

This cmdlet protects the supplied plaintext with DPAPI-NG and returns the protected binary blob as a base64 string.
Use `Descriptor` to supply a protection descriptor rule string directly, or `NamedDescriptor` to use a registered named descriptor.
When `NamedDescriptor` is used, the `Machine` switch resolves the descriptor in the local machine descriptor registry hive.

The cleartext is encoded as UTF-16 little-endian before encryption by default. Supply `Encoding` to use a different text encoding.

## EXAMPLES

### Example 1
```powershell
PS C:\> Protect-DpapiNgData -Descriptor 'LOCAL=user' -Cleartext 'Pa$$w0rd'
```

Protects text for the current user and returns a base64-encoded DPAPI-NG blob.

### Example 2
```powershell
PS C:\> Protect-DpapiNgData -NamedDescriptor 'LocalMachine' -Machine -Cleartext 'Pa$$w0rd'
```

Protects text by using a named descriptor registered in the local machine hive.

### Example 3
```powershell
PS C:\> Protect-DpapiNgData -Descriptor 'SID=S-1-5-21-3288850392-3299536932-2614793081-512' -Cleartext 'Pa$$w0rd' -Encoding UTF8

<# Sample Output:
MIIBfgYJKoZIhvcNAQcDoIIBbzCCAWsCAQIxggEdooIBGQIBBDCB3ASBhAEAAABLRFNLAgAAAGwBAAAFAAAAAgAAAHFrVRwi7V/EcjzdvhmfaCQgAAAAGAAAABgAAADbXiCq9P/fSJ7/N+Pp/iN2B2WtpCgvzrvj0JscdeyQBGMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAAGMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAADBTBgkrBgEEAYI3SgEwRgYKKwYBBAGCN0oBATA4MDYwNAwDU0lEDC1TLTEtNS0yMS0zMjg4ODUwMzkyLTMyOTk1MzY5MzItMjYxNDc5MzA4MS01MTIwCwYJYIZIAWUDBAEtBCi/OCkpzyD9YKVGwFhAA7VGmfakng2fpmvMiG/DW4248BSlBDcfIFn+MEUGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMSAygtw55Qk5YcNtiAgEQgBiOh95J+ZwmKUL129c4D7lis40RYNLIs9g=
#>
```

Encrypts the UTF-8 encoded text so that only a holder of the specified SID-bound group key (here, the Domain Admins group) can decrypt it, and returns the protected blob as a base64 string.

## PARAMETERS

### -Cleartext
Specifies the text to protect.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Descriptor
Specifies the DPAPI-NG protection descriptor rule string.

```yaml
Type: String
Parameter Sets: Descriptor
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Encoding
Specifies the text encoding used to convert `Cleartext` to bytes before encryption.
Accepts a `System.Text.Encoding` instance (such as `[System.Text.Encoding]::UTF8`) or one of the following well-known encoding names, which are also offered through tab completion: `ASCII`, `BigEndianUnicode`, `Unicode`, `UTF32`, `UTF7`, `UTF8`.

```yaml
Type: Encoding
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: [System.Text.Encoding]::Unicode
Accept pipeline input: False
Accept wildcard characters: False
```

### -Machine
Indicates that the named descriptor should be resolved from the local machine registry hive.

```yaml
Type: SwitchParameter
Parameter Sets: NamedDescriptor
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -NamedDescriptor
Specifies the name of a registered DPAPI-NG protection descriptor.

```yaml
Type: String
Parameter Sets: NamedDescriptor
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.String

## NOTES

Alias: `Protect-CngDpapiData`

## RELATED LINKS

[Get-DpapiNgData](Get-DpapiNgData.md)
[Unprotect-DpapiNgData](Unprotect-DpapiNgData.md)
[New-DpapiNgNamedDescriptor](New-DpapiNgNamedDescriptor.md)
