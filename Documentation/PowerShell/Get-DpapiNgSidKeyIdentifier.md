---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-DpapiNgSidKeyIdentifier.md
schema: 2.0.0
---

# Get-DpapiNgSidKeyIdentifier

## SYNOPSIS
Parses a SID-protected DPAPI-NG KeyId blob.

## SYNTAX

```
Get-DpapiNgSidKeyIdentifier [-Blob] <Byte[]> [<CommonParameters>]
```

## DESCRIPTION

This cmdlet parses a DPAPI-NG Protection Key Identifier (`KDSK`) blob and returns a `ProtectionKeyIdentifier` object.
Such blobs are emitted by the Windows DPAPI-NG implementation when SID-based protectors are used
and can be observed, for example, in the `KeyId` field of Microsoft-Windows-Crypto-DPAPI events.
They identify the KDS root key and the L0/L1/L2 key cycle that was used to derive a group key.

The `Blob` parameter accepts either a byte array or a hexadecimal string, matching the encoding
used in the `KeyId` field of Microsoft-Windows-Crypto-DPAPI events.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DpapiNgSidKeyIdentifier -Blob $keyId
```

Parses a 132-byte (or longer) `KDSK` Protection Key Identifier and returns its fields.

### Example 2
```powershell
PS C:\> '010000004B44534B...' | Get-DpapiNgSidKeyIdentifier
```

Parses a hexadecimal `KDSK` blob piped from another command. The `KeyId` alias on the
parameter allows consumption of events whose property is named `KeyId`.

## PARAMETERS

### -Blob
Specifies the DPAPI-NG Protection Key Identifier as a byte array or a hexadecimal string.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: KeyId, ProtectionKeyIdentifier, KeyIdentifier

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

### DSInternals.Common.Data.ProtectionKeyIdentifier

## NOTES

Alias: `Get-CngDpapiSidKeyIdentifier`

## RELATED LINKS

[Get-DpapiNgData](Get-DpapiNgData.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
