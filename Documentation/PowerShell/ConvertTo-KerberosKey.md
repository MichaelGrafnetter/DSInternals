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
Supports the derivation of AES256, AES128 and DES encryption keys. To calculate the RC4 key, the ConvertTo-NTHash cmdlet should be used instead.

## EXAMPLES

### Example 1
```powershell
PS C:\> $pwd = ConvertTo-SecureString -String 'Pa$$w0rd' -AsPlainText -Force
PS C:\> ConvertTo-KerberosKey -Password $pwd -Salt 'CONTOSO.COMAdministrator'
<# Sample Output:

AES256_CTS_HMAC_SHA1_96
  Key: 660e61042b190b5724c62bb473facca12058fb9ad3c03c0d2809f839c0352502
  Iterations: 4096

AES128_CTS_HMAC_SHA1_96
  Key: bd75e98362b16649ffbaed630d5341d0
  Iterations: 4096

DES_CBC_MD5
  Key: aed02c52204ca2ce
  Iterations: 4096
#>
```

Applies 3 different kerberos key derivation functions to the specified password and salt.

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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.Common.Data.KerberosKeyDataNew

## NOTES

## RELATED LINKS

[ConvertTo-NTHash](ConvertTo-NTHash.md)
[ConvertTo-LMHash](ConvertTo-LMHash.md)