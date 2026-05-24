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
PS C:\> Get-DpapiNgData -Blob $blob
```

Parses a base64-encoded DPAPI-NG protected blob.

### Example 2
```powershell
PS C:\> Get-DpapiNgData -Blob $blob | Select-Object -ExpandProperty SidKeyProtectors
```

Displays the SID and SDDL protectors collected from the blob.

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

### System.String

## OUTPUTS

### DSInternals.Common.Cryptography.CngProtectedDataBlob

## NOTES

Alias: `Get-CngDpapiData`

## RELATED LINKS

[Protect-DpapiNgData](Protect-DpapiNgData.md)
[Unprotect-DpapiNgData](Unprotect-DpapiNgData.md)
