---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-ADDBDomainController.md
schema: 2.0.0
---

# Set-ADDBDomainController

## SYNOPSIS
Writes information about the DC to a ntds.dit file, including the highest committed USN and database epoch.

## SYNTAX

### USN
```
Set-ADDBDomainController [-Force] -DatabasePath <String> [-LogPath <String>] -HighestCommittedUsn <Int64>
 [<CommonParameters>]
```

### Epoch
```
Set-ADDBDomainController [-Force] -DatabasePath <String> [-LogPath <String>] -Epoch <Int32>
 [<CommonParameters>]
```

### Expiration
```
Set-ADDBDomainController [-Force] -DatabasePath <String> [-LogPath <String>] -BackupExpiration <DateTime>
 [<CommonParameters>]
```

## DESCRIPTION
The Set-ADDBDomainController cmdlet can be used to simulate USN rollbacks, USN depletion, and database file restore operations. This cmdlet should only be used in lab environments.

## EXAMPLES

### Example 1
```powershell
PS C:\> $currentEpoch = Get-ItemPropertyValue -Path 'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\NTDS\Parameters' -Name 'DSA Database Epoch'
PS C:\> Set-ADDBDomainController -DatabasePath .\ntds.dit -Epoch $currentEpoch -Force
```

Copies the database epoch from registry to the ntds.dit file.

### Example 2
```powershell
PS C:\> Set-ADDBDomainController -DatabasePath .\ntds.dit -HighestCommittedUsn 9223372036854775800 -Force
```

Modifies the highest committed USN of the AD database. This might be helpful when trying to simulate USN rollbacks and USN depletion.

## PARAMETERS

### -BackupExpiration
Specifies the database backup expiration time.

```yaml
Type: DateTime
Parameter Sets: Expiration
Aliases: Expiration, Expire

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Epoch
Specifies the database epoch which must be consistent with the information in the registry.

```yaml
Type: Int32
Parameter Sets: Epoch
Aliases: DSAEpoch

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Confirms that you understand the implications of using this cmdlet and still want to use it.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HighestCommittedUsn
Specifies the highest committed USN for the database.

```yaml
Type: Int64
Parameter Sets: USN
Aliases: USN

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogPath
Specifies the path to a directory where the transaction log files are located. For instance, C:\Windows\NTDS. The default log directory is the one that contains the database file itself.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Log, TransactionLogPath

Required: False
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

### None

## NOTES

## RELATED LINKS

[Get-ADDBDomainController](Get-ADDBDomainController.md)
[Set-ADDBBootKey](Set-ADDBBootKey.md)
