---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-DpapiNgNamedDescriptor.md
schema: 2.0.0
---

# Get-DpapiNgNamedDescriptor

## SYNOPSIS
Gets one or more named DPAPI-NG protection descriptors.

## SYNTAX

```
Get-DpapiNgNamedDescriptor [[-Name] <String>] [-Machine] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet returns named DPAPI-NG protection descriptors from the current user's descriptor registry hive.
When the `Name` parameter is supplied, the descriptor is resolved through the NCrypt DPAPI-NG API.
Without `Name`, the cmdlet enumerates all descriptor registry values.
When the `Machine` switch is supplied, the cmdlet queries or enumerates the local machine descriptor registry hive.

The named descriptors are stored as `REG_SZ` registry values, where the value name is the descriptor display name and the value data is the protection descriptor rule string, under the following key:

- Current user: `HKCU:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`
- Local machine (`-Machine`): `HKLM:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DpapiNgNamedDescriptor -Machine

<# Sample Output:
Key                             Value
---                             -----
OracleConnectionStringProtector SID=S-1-5-21-3288850392-3299536932-2614793081-1171 OR LOCAL=machine
DomainAdmins                    SID=S-1-5-21-3288850392-3299536932-2614793081-512
#>
```

Returns all named DPAPI-NG protection descriptors registered in the local machine hive.

### Example 2
```powershell
PS C:\> Get-DpapiNgNamedDescriptor -Machine -Name OracleConnectionStringProtector

<# Sample Output:
Key                             Value
---                             -----
OracleConnectionStringProtector SID=S-1-5-21-3288850392-3299536932-2614793081-1171 OR LOCAL=machine
#>
```

Resolves a single named descriptor from the local machine hive by its name.

## PARAMETERS

### -Machine
Indicates that descriptors should be queried from the local machine registry hive.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Specifies the named descriptor display name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Key

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]

## NOTES

Alias: `Get-CngDpapiNamedDescriptor`

## RELATED LINKS

[New-DpapiNgNamedDescriptor](New-DpapiNgNamedDescriptor.md)
[Remove-DpapiNgNamedDescriptor](Remove-DpapiNgNamedDescriptor.md)
