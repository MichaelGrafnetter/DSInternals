---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-DpapiNgData.md
schema: 2.0.0
---

# Get-DpapiNgData

## SYNOPSIS
Parses DPAPI-NG protected data.

## SYNTAX

```
Get-DpapiNgData [-Blob] <Byte[]> [<CommonParameters>]
```

## DESCRIPTION

This cmdlet parses a DPAPI-NG protected blob and returns a `CngProtectedDataBlob` object.
The `Blob` parameter accepts either a byte array or a base64-encoded `CngProtectedDataBlob`.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DpapiNgData -Blob MIIBfgYJKoZIhvcNAQcDoIIBbzCCAWsCAQIxggEdooIBGQIBBDCB3ASBhAEAAABLRFNLAgAAAGwBAAAFAAAAAgAAAHFrVRwi7V/EcjzdvhmfaCQgAAAAGAAAABgAAADbXiCq9P/fSJ7/N+Pp/iN2B2WtpCgvzrvj0JscdeyQBGMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAAGMAbwBuAHQAbwBzAG8ALgBjAG8AbQAAADBTBgkrBgEEAYI3SgEwRgYKKwYBBAGCN0oBATA4MDYwNAwDU0lEDC1TLTEtNS0yMS0zMjg4ODUwMzkyLTMyOTk1MzY5MzItMjYxNDc5MzA4MS01MTIwCwYJYIZIAWUDBAEtBCi/OCkpzyD9YKVGwFhAA7VGmfakng2fpmvMiG/DW4248BSlBDcfIFn+MEUGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMSAygtw55Qk5YcNtiAgEQgBiOh95J+ZwmKUL129c4D7lis40RYNLIs9g=

<# Sample Output:
Descriptor: SID=S-1-5-21-3288850392-3299536932-2614793081-512
ContentEncryptionAlgorithm: aes256gcm (2.16.840.1.101.3.4.1.46)
SID/SDDL Protectors
  SID=S-1-5-21-3288850392-3299536932-2614793081-512
    ProtectionKeyIdentifier: RootKey=1c556b71-ed22-c45f-723c-ddbe199f6824, Cycle=5/27/2026 6:00:00 AM (L0=364, L1=5, L2=2)
    KeyEncryptionAlgorithm: aes256wrap (2.16.840.1.101.3.4.1.45)
#>
```

Parses a base64-encoded blob produced by [Protect-DpapiNgData](Protect-DpapiNgData.md) and displays its protection descriptor, content encryption algorithm, and the SID protector with its associated KDS protection key identifier. No decryption is performed.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Byte[]

## OUTPUTS

### DSInternals.Common.Cryptography.CngProtectedDataBlob

## NOTES

Alias: `Get-CngDpapiData`

## RELATED LINKS

[Protect-DpapiNgData](Protect-DpapiNgData.md)
[Unprotect-DpapiNgData](Unprotect-DpapiNgData.md)
