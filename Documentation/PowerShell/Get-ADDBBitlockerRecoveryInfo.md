---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBBitlockerRecoveryInfo.md
schema: 2.0.0
---

# Get-ADDBBitlockerRecoveryInfo

## SYNOPSIS
Reads msFVE-RecoveryInformation objects from a ntds.dit. file. Can be used to decrypt bitlocker containers.

## SYNTAX

```
Get-ADDBBitlockerRecoveryInfo -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
Bitlocker Recovery Information can be used to unlock encrypted bitlocker containers (Operating System, USB Drive, ...). 

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBBitlockerRecoveryInfo -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
<# Sample Output:
[TODO]
#>
```

Retrieves the Bitlocker Recovery Keys from an AD database.

## PARAMETERS

### -DatabasePath
Specifies the path to a domain database, for instance, C:\Windows\NTDS\ntds.dit.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Database, DBPath, DatabaseFilePath, DBFilePath

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RecoveryGuid
Specifies the RecoveryGuid to search, for instance, 11c43ee8-b9d3-4e51-b73f-bd9dda66e29c

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExportKeysPath
Specifies the path to a file where Bitlocker KeyPackages and RecoveryPasswords are written, for instance, c:\ADDumpBitlocker

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.Common.Data.BitlockerRecoveryInfo

## NOTES

## RELATED LINKS

[Get-ADReplBitlockerRecoveryInfo](Get-ADReplBitlockerRecoveryInfo.md)
