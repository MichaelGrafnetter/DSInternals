---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Set-ADDBDomainController

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### USN
```
Set-ADDBDomainController [-Force] -DBPath <String> [-LogPath <String>] -HighestCommittedUsn <Int64>
 [<CommonParameters>]
```

### Epoch
```
Set-ADDBDomainController [-Force] -DBPath <String> [-LogPath <String>] -Epoch <Int32> [<CommonParameters>]
```

### Expiration
```
Set-ADDBDomainController [-Force] -DBPath <String> [-LogPath <String>] -BackupExpiration <DateTime>
 [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -BackupExpiration
{{Fill BackupExpiration Description}}

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

### -DBPath
TODO

```yaml
Type: String
Parameter Sets: (All)
Aliases: Database, DatabasePath, DatabaseFilePath, DBFilePath

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Epoch
{{Fill Epoch Description}}

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
{{Fill Force Description}}

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
{{Fill HighestCommittedUsn Description}}

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
TODO

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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

## NOTES

## RELATED LINKS
