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
Get-ADSIDnsServerResourceRecord [-IncludeTombstones] [-IncludeRootHints] [-IncludeTrustAnchors]
 [-ZoneName <String>] [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
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

### Example 3
```powershell
PS C:\> Get-ADSIDnsServerResourceRecord -ZoneName 'example.com'

<# Sample Output:
@                                                                     NS    contoso-dc.contoso.com.
@                                                                     NS    ns1.example.com.
@                                                                 IN  SOA   contoso-dc.contoso.com. hostmaster.contoso.com. (
                                                                            104          ; serial number
                                                                            900          ; refresh
                                                                            600          ; retry
                                                                            86400        ; expire
                                                                            3600       ) ; default TTL
@                                                                     MX    10 mail.example.com.
@                                                                     MX    20 backup.example.com.
@                                                                     TXT   ( "v=spf1 -all" )
@                                                           0         WINS  LOCAL L2 C900 ( 192.0.2.100 )
_25._tcp.mail                                                         TLSA  0 0 2 AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
_443._tcp.www                                                         TLSA  3 1 1 0000000000000000000000000000000000000000000000000000000000000000
_afs3-vlserver._udp                                                   SRV   0 100 7003 afs.example.com.
_ldap._tcp                                                            SRV   0 100 389  dc01.example.com.
afsdb-sample                                                          AFSDB 1 afs.example.com.
afsdb-sample                                                          AFSDB 2 dce.example.com.
alias                                                                 CNAME www.example.com.
atma-e164                                                             ATMA  +123456789
atma-e164                                                             ATMA  +1.2345.6789
dhcid-sample                                                          DHCID AAIBY2/AuCccgoJbsaxcQc9TUapptP69lOjxfNuVAA2kjEA=
full6                                                                 AAAA  2001:db8::42
hinfo-arm                                                             HINFO ( "ARM64" "Linux" )
hinfo-sample                                                          HINFO ( "Intel-x64" "Windows" )
isdn-sample                                                           ISDN  ( "150862028003217" "004" )
ptr-sample                                                            PTR   target.example.com.
quoted                                                                TXT   ( "value with \"embedded quotes\" and spaces" )
rp-sample                                                             RP    admin.example.com. admin-info.example.com.
rt-sample                                                             RT    10 router.example.com.
sub                                                                   DNAME other.example.com.
sub2                                                                  DNAME other.example.com.
wks-tcp                                                               WKS   192.0.2.20 tcp ( smtp domain http )
wks-udp                                                               WKS   192.0.2.21 udp ( domain ntp snmp )
www                                                                   A     192.0.2.10
www                                                                   A     192.0.2.11
www6                                                                  AAAA  2001:db8::1
x25-sample                                                            X25   ( "311061700956" )
atma-nsap                                                             ATMA  47.0027.01000000000000000000.000000000000.00
#>
```

Retrieves the resource records belonging to the example.com zone and renders them in the BIND and Windows Server compatible zone file text format. The sample illustrates how the supported record types (NS, SOA, MX, TXT, WINS, TLSA, SRV, AFSDB, CNAME, ATMA, DHCID, AAAA, HINFO, ISDN, PTR, RP, RT, DNAME, WKS, A, and X25) are presented.

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

### -IncludeTrustAnchors
Indicates that DNSSEC trust anchor records should be included in the output.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: TrustAnchors

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

### -ZoneName
Restricts the output to records belonging to the DNS zone with the specified name. The value is matched case-insensitively against the zone's fully qualified domain name (FQDN).

```yaml
Type: String
Parameter Sets: (All)
Aliases: Zone, DnsZone

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### DSInternals.Common.DNS.DnsResourceRecord

## NOTES

## RELATED LINKS

[Get-ADSIDnsServerZone](Get-ADSIDnsServerZone.md)
[Get-ADDBDnsServerResourceRecord](Get-ADDBDnsServerResourceRecord.md)
[Save-DnsServerResourceRecord](Save-DnsServerResourceRecord.md)
