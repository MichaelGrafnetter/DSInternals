---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-KerberosKey.md
schema: 2.0.0
---

# ConvertTo-KerberosKey

## SYNOPSIS
Computes Kerberos keys from a given password using Kerberos version 5 Key Derivation Functions.

## SYNTAX

```
ConvertTo-KerberosKey [-Password] <SecureString> [-Salt] <String> [[-Iterations] <Int32>] [<CommonParameters>]
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

### -Iterations
Specifies the iteration count parameter of the string-to-key functions.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: i

Required: False
Position: 2
Default value: 4096
Accept pipeline input: False
Accept wildcard characters: False
```

### -Password
Specifies an input password from which kerberos keys will be derived.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases: p

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Salt
Specifies the salt parameter of the string-to-key functions.

```yaml
Type: String
Parameter Sets: (All)
Aliases: s

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### DSInternals.Common.Data.KerberosKeyDataNew
## NOTES

## RELATED LINKS

[ConvertTo-NTHash](ConvertTo-NTHash.md)
[ConvertTo-LMHash](ConvertTo-LMHash.md)