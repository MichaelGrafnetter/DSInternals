---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-BootKey.md
schema: 2.0.0
---

# Get-BootKey

## SYNOPSIS
Reads the Boot Key (AKA SysKey or System Key) from an online or offline SYSTEM registry hive.

## SYNTAX

### Offline
```
Get-BootKey [-SystemHiveFilePath] <String> [<CommonParameters>]
```

### Online
```
Get-BootKey [-Online] [<CommonParameters>]
```

## DESCRIPTION
The BootKey/SysKey is an encryption key that is stored in the Windows SYSTEM registry hive. This key is used by several Windows components to encrypt sensitive information like the AD database, machine account password or system certificates etc.

The Boot Key is returned in hexadecimal format.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-BootKey -Online
0be7a2afe1713642182e9b96f73a75da
```

Retrieves the BootKey from the currently running OS.

### Example 2
```powershell
PS C:\> reg.exe SAVE HKLM\SYSTEM C:\RegBackup\SYSTEM.hiv
PS C:\> $key = Get-BootKey -SystemHiveFilePath C:\RegBackup\SYSTEM.hiv
```

Creates a backup of the SYSTEM registry hive and then retrieves the BootKey from this backup. 

## PARAMETERS

### -Online
Specifies that the action is to be taken on the operating system that is currently running on the local computer.

```yaml
Type: SwitchParameter
Parameter Sets: Online
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SystemHiveFilePath
Path to an offline SYSTEM registry hive.

```yaml
Type: String
Parameter Sets: Offline
Aliases: Path, FilePath, SystemHivePath, HivePath

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADDBBackupKey](Get-ADDBBackupKey.md)
[Set-ADDBAccountPassword](Set-ADDBAccountPassword.md)
[Set-ADDBAccountPasswordHash](Set-ADDBAccountPasswordHash.md)
[Set-ADDBBootKey](Set-ADDBBootKey.md)
