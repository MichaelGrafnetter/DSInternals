---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Save-DPAPIBlob

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### FromObject
```
Save-DPAPIBlob -DPAPIObject <DPAPIObject> [-DirectoryPath] <String> [<CommonParameters>]
```

### FromAccount
```
Save-DPAPIBlob -Account <DSAccount> [-DirectoryPath] <String> [<CommonParameters>]
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

### -Account
{{Fill Account Description}}

```yaml
Type: DSAccount
Parameter Sets: FromAccount
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DPAPIObject
{{Fill DPAPIObject Description}}

```yaml
Type: DPAPIObject
Parameter Sets: FromObject
Aliases: DPAPIBlob, Object, Blob, BackupKey

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DirectoryPath
{{Fill DirectoryPath Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path, OutputPath

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### DSInternals.Common.Data.DPAPIObject

### DSInternals.Common.Data.DSAccount

## OUTPUTS

### None

## NOTES

## RELATED LINKS
