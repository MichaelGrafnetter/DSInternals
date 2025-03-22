---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBDnsResourceRecord.md
schema: 2.0.0
---

# Get-ADDBDnsResourceRecord

## SYNOPSIS
Retrieves DNS resource records from an Active Directory database.

## SYNTAX

```
Get-ADDBDnsResourceRecord [-IncludeTombstones] [-IncludeRootHints] -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
Retrieves DNS resource records from an offline Active Directory database.
The output can include tombstoned records and root hints if specified.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBDnsResourceRecord -DatabasePath 'C:\IFM Backup\Active Directory\ntds.dit' |
            Where-Object Zone -eq 'contoso.com' |
            Where-Object Type -in SOA,NS,A,CNAME,MX,CNAME |
            Sort-Object -Property Name
<# Sample Output:
@                               3600  IN  SOA   dc01.contoso.com. hostmaster.contoso.com. (
                                                186          ; serial number
                                                900          ; refresh
                                                600          ; retry
                                                86400        ; expire
                                                3600       ) ; default TTL
@                               3600  IN  NS    dc01.contoso.com.
@                               3600  IN  NS    dc02.contoso.com.
@                               600   IN  A     10.213.0.3
@                               600   IN  A     10.213.0.9
_msdcs                          3600  IN  NS    dc01.contoso.com.
certauth.login                  3600  IN  A     10.213.0.4
dc01                            3600  IN  A     10.213.0.3
dc02                            3600  IN  A     10.213.0.9
DomainDnsZones                  600   IN  A     10.213.0.9
DomainDnsZones                  600   IN  A     10.213.0.3
ForestDnsZones                  600   IN  A     10.213.0.9
ForestDnsZones                  600   IN  A     10.213.0.3
ftp                             3600  IN  CNAME www
login                           3600  IN  A     10.213.0.4
#>
```

Extracts all DNS recource records from the offline Active Directory database file and displays the SOA, NS, A, CNAME, MX, and CNAME records from the contoso.com zone in a text format compatible with Windows Server and BIND zone files.

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

### -IncludeRootHints
Indicates that root hints should be included in the output.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: RootHints, RootServers

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeTombstones
Indicates that tombstoned DNS records should be included in the output.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: Tombstones, IncludeTombstoned

Required: False
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

### DSInternals.Common.Data.DnsResourceRecord

## NOTES

## RELATED LINKS

[Get-ADDBDnsZone](Get-ADDBDnsZone.md)
[Get-ADDBDomainController](Get-ADDBDomainController.md)
