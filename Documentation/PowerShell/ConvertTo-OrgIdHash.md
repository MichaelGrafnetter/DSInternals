---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-OrgIdHash.md
schema: 2.0.0
---

# ConvertTo-OrgIdHash

## SYNOPSIS
Calculates OrgId hash of a given password. Used by Azure Active Directory Connect.

## SYNTAX

### FromHash (Default)
```
ConvertTo-OrgIdHash [-NTHash] <Byte[]> [[-Salt] <Byte[]>] [<CommonParameters>]
```

### FromPassword
```
ConvertTo-OrgIdHash [-Password] <SecureString> [[-Salt] <Byte[]>] [<CommonParameters>]
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

### -NTHash
Provide a 16-byte NT Hash of user's password in hexadecimal format.

```yaml
Type: Byte[]
Parameter Sets: FromHash
Aliases: h

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Password
Provide a password in the form of a SecureString.

```yaml
Type: SecureString
Parameter Sets: FromPassword
Aliases: p

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Salt
Provide a 10-byte salt in hexadecimal format.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: s

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Security.SecureString
### System.Byte[]
## OUTPUTS

### System.String
## NOTES

## RELATED LINKS
