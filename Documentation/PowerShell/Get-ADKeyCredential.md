---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADKeyCredential.md
schema: 2.0.0
---

# Get-ADKeyCredential

## SYNOPSIS
Creates an object representing Windows Hello for Business credentials from its binary representation or an X.509 certificate.

## SYNTAX

### FromCertificate (Default)
```
Get-ADKeyCredential [-Certificate] <X509Certificate2> [-DeviceId] <Guid> [<CommonParameters>]
```

### FromDNBinary
```
Get-ADKeyCredential -DNWithBinaryData <String> [<CommonParameters>]
```

### FromBinary
```
Get-ADKeyCredential -BinaryData <Byte[]> [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -BinaryData
{{Fill BinaryData Description}}

```yaml
Type: Byte[]
Parameter Sets: FromBinary
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Certificate
{{Fill Certificate Description}}

```yaml
Type: X509Certificate2
Parameter Sets: FromCertificate
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceId
{{Fill DeviceId Description}}

```yaml
Type: Guid
Parameter Sets: FromCertificate
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DNWithBinaryData
{{Fill DNWithBinaryData Description}}

```yaml
Type: String
Parameter Sets: FromDNBinary
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### DSInternals.Common.Data.KeyCredential
## NOTES

## RELATED LINKS
