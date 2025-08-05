---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBSchemaAttribute.md
schema: 2.0.0
---

# Get-ADDBSchemaAttribute

## SYNOPSIS
Reads AD schema from a ntds.dit file, including datatable column names.

## SYNTAX

```
Get-ADDBSchemaAttribute [[-Name] <String[]>] -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
Reads Active Directory schema from a ntds.dit file and returns the list of attributes, including their datatable column names and indices.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBSchemaAttribute -DatabasePath 'C:\ADBackup\ntds.dit' |
	Select-Object -Property ColumnName,Name,AttributeOid,IndexName

<# Sample Output:

ColumnName Name           AttributeOid           IndexName
---------- ----           ------------           ---------
ATTm3      cn             2.5.4.3                INDEX_00000003
ATTm590045 sAMAccountName 1.2.840.113556.1.4.221 INDEX_000900DD
ATTj590126 sAMAccountType 1.2.840.113556.1.4.302 INDEX_0009012E
...

#>
```

Analyzes the internal database schema of the specified ntds.dit file. The results are redacted.

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

### -Name
Specifies the name of a specific attribute to retrieve.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: LdapDisplayName,AttributeName,AttrName,Attr

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### DSInternals.Common.Schema.AttributeSchema

## NOTES

## RELATED LINKS

[Get-ADDBDomainController](Get-ADDBDomainController.md)
