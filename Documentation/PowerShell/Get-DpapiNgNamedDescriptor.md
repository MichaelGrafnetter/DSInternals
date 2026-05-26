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

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DpapiNgNamedDescriptor
```

Returns all named DPAPI-NG protection descriptors registered for the current user.

### Example 2
```powershell
PS C:\> Get-DpapiNgNamedDescriptor -Name 'LocalUser'
```

Returns the descriptor rule string registered under the `LocalUser` name.

### Example 3
```powershell
PS C:\> Get-DpapiNgNamedDescriptor -Machine
```

Returns all named DPAPI-NG protection descriptors registered in the local machine hive.

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
