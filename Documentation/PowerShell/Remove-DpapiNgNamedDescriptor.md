---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Remove-DpapiNgNamedDescriptor.md
schema: 2.0.0
---

# Remove-DpapiNgNamedDescriptor

## SYNOPSIS
Removes a named DPAPI-NG protection descriptor.

## SYNTAX

```
Remove-DpapiNgNamedDescriptor [-Name] <String> [-Machine] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet removes a named DPAPI-NG protection descriptor from the current user's descriptor registry hive.
When the `Machine` switch is supplied, the descriptor is removed from the local machine descriptor registry hive.

The named descriptors are stored as `REG_SZ` registry values, where the value name is the descriptor display name and the value data is the protection descriptor rule string, under the following key:

- Current user: `HKCU:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`
- Local machine (`-Machine`): `HKLM:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`

## EXAMPLES

### Example 1
```powershell
PS C:\> Remove-DpapiNgNamedDescriptor -Name 'LocalUser'
```

Removes a named descriptor from the current user's descriptor hive.

### Example 2
```powershell
PS C:\> Remove-DpapiNgNamedDescriptor -Name 'LocalMachine' -Machine
```

Removes a named descriptor from the local machine descriptor hive.

## PARAMETERS

### -Machine
Indicates that the descriptor should be removed from the local machine registry hive.

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

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### None

## NOTES

Alias: `Remove-CngDpapiNamedDescriptor`

## RELATED LINKS

[New-DpapiNgNamedDescriptor](New-DpapiNgNamedDescriptor.md)
[Get-DpapiNgNamedDescriptor](Get-DpapiNgNamedDescriptor.md)
