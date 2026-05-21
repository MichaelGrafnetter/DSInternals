---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Save-DnsResourceRecord.md
schema: 2.0.0
---

# Save-DnsResourceRecord

## SYNOPSIS
Saves DNS resource records to DNS zone files.

## SYNTAX

```
Save-DnsResourceRecord -InputObject <DnsResourceRecord> [-DirectoryPath] <String> [-Force] [<CommonParameters>]
```

## DESCRIPTION
Saves DNS resource records retrieved from Active Directory to DNS zone files.
The cmdlet creates one `.dns` file for each DNS zone in the target directory.
Existing zone files are not overwritten unless the Force parameter is specified.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBDnsResourceRecord -DatabasePath 'C:\Backup\ntds.dit' |
            Save-DnsResourceRecord -Path '.\Zones' -Verbose
```

Extracts all DNS resource records from the offline Active Directory database file and saves them to DNS zone files in the Zones directory.
The cmdlet writes verbose information for each zone file that it creates.

### Example 2
```powershell
PS C:\> Get-ADDBDnsResourceRecord -DatabasePath '.\ntds.dit' |
            Where-Object Zone -eq 'contoso.com' |
            Save-DnsResourceRecord -Path '.\Zones' -Force
```

Saves records from the contoso.com zone to .\Zones\contoso.com.dns and overwrites an existing file if needed.

## PARAMETERS

### -DirectoryPath
Specifies the path to a target directory where DNS zone files will be saved.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path, OutputPath, OutputDirectory, OutDir

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Indicates that existing DNS zone files should be overwritten.

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

### -InputObject
Specifies a DNS resource record to save to a DNS zone file.

```yaml
Type: DnsResourceRecord
Parameter Sets: (All)
Aliases: Record, DnsRecord

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### DSInternals.Common.DNS.DnsResourceRecord

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Get-ADDBDnsResourceRecord](Get-ADDBDnsResourceRecord.md)
[Get-ADDBDnsZone](Get-ADDBDnsZone.md)
