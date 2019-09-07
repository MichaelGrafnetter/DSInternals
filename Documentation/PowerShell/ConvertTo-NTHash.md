---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-NTHash.md
schema: 2.0.0
---

# ConvertTo-NTHash

## SYNOPSIS
Calculates NT hash of a given password.

## SYNTAX

```
ConvertTo-NTHash [-Password] <SecureString> [<CommonParameters>]
```

## DESCRIPTION
Applies the NT one-way function (NT OWF) to a given cleartext password and returns the resulting hash, which is just the MD4 hash function applied to the UTF-16 encoded input.

This hash is sometimes called NTLM hash, because it is mainly used in the NTLM(v2) network authentication protocol.

## EXAMPLES

### Example 1
```powershell
PS C:\> ConvertTo-NTHash

cmdlet ConvertTo-NTHash at command pipeline position 1
Supply values for the following parameters:
(Type !? for Help.)
Password: ********
92937945b518814341de3f726500d4ff
```

Reads a password from the command line and calculates its NT hash.

### Example 2
```powershell
PS C:\> $pwd = ConvertTo-SecureString -String 'Pa$$w0rd' -AsPlainText -Force
PS C:\> ConvertTo-NTHash -Password $pwd
92937945b518814341de3f726500d4ff
```

Calculates the NT hash of password *Pa$$w0rd*.

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

[ConvertTo-LMHash](ConvertTo-NTHash.md)
[ConvertTo-KerberosKey](ConvertTo-KerberosKey.md)