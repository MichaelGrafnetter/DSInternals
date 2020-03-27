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
The OrgId hash is defined as PBKDF2( UTF-16( ToUpper( ToHex( MD4( UTF-16(plaintext))))), RND(10), 1000, HMAC-SHA256, 32).

## EXAMPLES

### Example 1
```powershell
PS C:\> $pwd = ConvertTo-SecureString -String 'Pa$$w0rd' -AsPlainText -Force
PS C:\> ConvertTo-OrgIdHash -Password $pwd
<# Sample Output:
v1;PPH1_MD4,60eaffd2c886b419df7a,1000,ab9c532104713157395a70da85cc8a1b418508753c6997f02341d541328ef16b;
#>
```

Calculates the OrgId hash from a cleartext password using a random salt.

### Example 2
```powershell
PS C:\> ConvertTo-OrgIdHash -NTHash 92937945b518814341de3f726500d4ff
<# Sample Output:
v1;PPH1_MD4,46c0c5d9095185ce5cf8,1000,6bb7b360d9105ed5157460b343d5d143e465a59195bc9b568718268c334ea4a9;
#>
```

Calculates the OrgId hash from a NT hash while using a random salt.

### Example 3
```powershell
PS C:\> ConvertTo-OrgIdHash -NTHash 92937945b518814341de3f726500d4ff -Salt a42b92067e4b8123101a
<# Sample Output:
v1;PPH1_MD4,a42b92067e4b8123101a,1000,f0fc762ea9051ef754652becd83ee5e54c1c857c1c0965abac5d85de9c143911;
#>
```

Calculates the OrgId hash from a NT hash while using the given salt.

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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Security.SecureString

### System.Byte[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[ConvertTo-NTHash](ConvertTo-NTHash.md)
