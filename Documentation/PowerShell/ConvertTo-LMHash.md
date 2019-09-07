---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-LMHash.md
schema: 2.0.0
---

# ConvertTo-LMHash

## SYNOPSIS
Calculates LM hash of a given password.

## SYNTAX

```
ConvertTo-LMHash [-Password] <SecureString> [<CommonParameters>]
```

## DESCRIPTION
Applies the Lan Manager one-way function (LM OWF) to a given cleartext password and returns the resulting hash.

## EXAMPLES

### Example 1
```powershell
PS C:\> ConvertTo-LMHash

cmdlet ConvertTo-LMHash at command pipeline position 1
Supply values for the following parameters:
(Type !? for Help.)
Password: ********
727e3576618fa1754a3b108f3fa6cb6d
```

Reads a password from the command line and calculates its LM hash.

### Example 2
```powershell
PS C:\> $pwd = ConvertTo-SecureString -String 'Pa$$w0rd' -AsPlainText -Force
PS C:\> ConvertTo-LMHash -Password $pwd
727e3576618fa1754a3b108f3fa6cb6d
```

Calculates the LM hash of password *Pa$$w0rd*.

## PARAMETERS

### -Password
Specifies a password in the form of a SecureString.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases: p

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Security.SecureString

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[ConvertTo-NTHash](ConvertTo-NTHash.md)
[ConvertTo-KerberosKey](ConvertTo-KerberosKey.md)