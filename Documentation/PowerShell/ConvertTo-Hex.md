---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-Hex.md
schema: 2.0.0
---

# ConvertTo-Hex

## SYNOPSIS
Helper cmdlet that converts binary input to a hexadecimal string.

## SYNTAX

```
ConvertTo-Hex [-Input] <Byte[]> [-UpperCase] [<CommonParameters>]
```

## DESCRIPTION
Converts a byte array to its hexadecimal representation.

## EXAMPLES

### Example 1
```powershell
PS C:\> ConvertTo-Hex -Input 0,255,32
00ff20
```

Converts the byte array to its hexadecimal representation, using lowercase characters.

### Example 2
```powershell
PS C:\> ConvertTo-Hex -Input 0,255,32 -UpperCase
00FF20
```

Converts the byte array to its hexadecimal representation, using uppercase characters.

## PARAMETERS

### -Input
Specifies the binary input in the form of an array of bytes.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UpperCase
Indicates that the output should be encoded using uppercase characters instead of lowercase ones.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Byte[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
