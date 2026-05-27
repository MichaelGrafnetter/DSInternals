---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/New-DpapiNgNamedDescriptor.md
schema: 2.0.0
---

# New-DpapiNgNamedDescriptor

## SYNOPSIS
Registers a named DPAPI-NG protection descriptor.

## SYNTAX

```
New-DpapiNgNamedDescriptor [-Name] <String> [-Descriptor] <String> [-Machine] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet registers or updates a named DPAPI-NG protection descriptor in the current user's descriptor registry hive.
When the `Machine` switch is supplied, the descriptor is registered in the local machine descriptor registry hive and can be queried with the DPAPI-NG machine key flag.

The named descriptors are stored as `REG_SZ` registry values, where the value name is the descriptor display name and the value data is the protection descriptor rule string, under the following key:

- Current user: `HKCU:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`
- Local machine (`-Machine`): `HKLM:\Software\Microsoft\Cryptography\NProtect\NamedDescriptors`

## EXAMPLES

### Example 1
```powershell
PS C:\> New-DpapiNgNamedDescriptor -Name 'LocalUser' -Descriptor 'LOCAL=user'
```

Registers a named descriptor for the current user.

### Example 2
```powershell
PS C:\> New-DpapiNgNamedDescriptor -Name 'LocalMachine' -Descriptor 'LOCAL=machine' -Machine
```

Registers a named descriptor in the local machine hive.

### Example 3
```powershell
PS C:\> New-DpapiNgNamedDescriptor -Name 'OracleConnectionStringProtector' -Descriptor 'SID=S-1-5-21-3288850392-3299536932-2614793081-1171 OR LOCAL=machine' -Machine
```

Registers a SID-based named descriptor in the local machine hive that allows decryption by a specific account SID or by the local machine. The cmdlet produces no output; use [Get-DpapiNgNamedDescriptor](Get-DpapiNgNamedDescriptor.md) with the `-Machine` switch to list the registered descriptors.

### Example 4
```powershell
PS C:\> New-DpapiNgNamedDescriptor -Name 'DomainAdmins' -Descriptor 'SID=S-1-5-21-3288850392-3299536932-2614793081-512' -Machine
```

Registers a SID-based named descriptor in the local machine hive that restricts decryption to members of the Domain Admins group.

## PARAMETERS

### -Descriptor
Specifies the DPAPI-NG protection descriptor rule string.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Machine
Indicates that the descriptor should be registered in the local machine registry hive.

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
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### None

## NOTES

Alias: `New-CngDpapiNamedDescriptor`

## RELATED LINKS

[Get-DpapiNgNamedDescriptor](Get-DpapiNgNamedDescriptor.md)
[Remove-DpapiNgNamedDescriptor](Remove-DpapiNgNamedDescriptor.md)
