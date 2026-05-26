---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIDnsServerResourceRecord.md
schema: 2.0.0
---

# Get-ADSIDnsServerResourceRecord

## SYNOPSIS
Retrieves DNS resource records from Active Directory through LDAP.

## SYNTAX

```
Get-ADSIDnsServerResourceRecord [-IncludeTombstones] [-IncludeRootHints] [-Server <String>]
 [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet retrieves DNS resource records hosted in Active Directory by querying the target domain controller through LDAP.
Both legacy DNS records stored under the domain naming context and modern records stored in application partitions (such as DomainDnsZones and ForestDnsZones) are returned.
Tombstoned records and root hints are omitted by default and can be included with the corresponding switch parameters.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADSIDnsServerResourceRecord -Server 'lon-dc1.contoso.com' |
            Where-Object Zone -eq 'contoso.com' |
            Where-Object Type -in SOA,NS,A,CNAME,MX |
            Sort-Object -Property Name
```

Retrieves DNS resource records from the specified domain controller through LDAP and displays the SOA, NS, A, CNAME, and MX records from the contoso.com zone in a text format compatible with Windows Server and BIND zone files.

### Example 2
```powershell
PS C:\> Get-ADSIDnsServerResourceRecord -Server 'lon-dc1.contoso.com' |
            Save-DnsServerResourceRecord -Path '.\Zones' -Verbose
```

Retrieves all DNS resource records from the specified domain controller and saves them to DNS zone files in the Zones directory.

## PARAMETERS

### -Credential
Specifies a user account to use when connecting to the target domain controller. The default is the current user.

```yaml
Type: PSCredential
Parameter Sets: (All)
Aliases:

Required: False
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

### -Server
Specifies the target domain controller. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Host, DomainController, DC, ComputerName

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

### DSInternals.Common.DNS.DnsResourceRecord

## NOTES

## RELATED LINKS

[Get-ADSIDnsServerZone](Get-ADSIDnsServerZone.md)
[Get-ADDBDnsServerResourceRecord](Get-ADDBDnsServerResourceRecord.md)
[Save-DnsServerResourceRecord](Save-DnsServerResourceRecord.md)
