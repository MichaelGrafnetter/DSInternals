---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Get-ADDBDnsServerZone

## SYNOPSIS
Retrieves the list of DNS zones stored in an Active Directory database.

## SYNTAX

```
Get-ADDBDnsServerZone -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet retrieves the list of DNS zones that are stored in the specified Active Directory database file.
This can be useful in some disaster recovery scenarios.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBDnsServerZone -DatabasePath .\ntds.dit

<# Sample Output:
DistinguishedName   : DC=_msdcs.contoso.com,CN=MicrosoftDNS,DC=ForestDnsZones,DC=contoso,DC=com
ZoneName            : _msdcs.contoso.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : False

DistinguishedName   : DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com
ZoneName            : contoso.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : False
#>
```

Retrieves the DNS zones from the specified Active Directory database file.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.Common.DNS.DnsZone

## NOTES

## RELATED LINKS

[Get-ADDBDnsServerResourceRecord](Get-ADDBDnsServerResourceRecord.md)
[Get-ADDBDomainController](Get-ADDBDomainController.md)
