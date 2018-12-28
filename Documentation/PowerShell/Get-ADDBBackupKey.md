---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBBackupKey.md
schema: 2.0.0
---

# Get-ADDBBackupKey

## SYNOPSIS
Reads the DPAPI backup keys from a ntds.dit file.

## SYNTAX

```
Get-ADDBBackupKey -BootKey <Byte[]> -DBPath <String> [-LogPath <String>] [<CommonParameters>]
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

### -BootKey
TODO

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: key, SysKey, SystemKey

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

### DSInternals.Common.Data.DPAPIBackupKey
## NOTES

## RELATED LINKS

[Save-DPAPIBlob](Save-DPAPIBlob.md)
[Get-ADReplBackupKey](Get-ADReplBackupKey.md)
