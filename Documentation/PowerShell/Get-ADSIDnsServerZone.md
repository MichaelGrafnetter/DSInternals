---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIDnsServerZone.md
schema: 2.0.0
---

# Get-ADSIDnsServerZone

## SYNOPSIS
Retrieves the list of DNS zones hosted in Active Directory through LDAP.

## SYNTAX

```
Get-ADSIDnsServerZone [-ZoneName <String>] [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet retrieves DNS zones hosted in Active Directory by querying the target domain controller through LDAP.
Both legacy DNS zones stored under the domain naming context and modern zones stored in application partitions (such as DomainDnsZones and ForestDnsZones) are returned.
Pseudo-zones used for root hints and DNSSEC trust anchors are excluded from the results.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADSIDnsServerZone -Server 'lon-dc1.contoso.com'

<# Sample Output:
DistinguishedName   : DC=_msdcs.contoso.com,CN=MicrosoftDNS,DC=ForestDnsZones,DC=contoso,DC=com
ZoneName            : _msdcs.contoso.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : False
SignWithNSEC3       : False
NSEC3CurrentSalt    :

DistinguishedName   : DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com
ZoneName            : contoso.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : True
SignWithNSEC3       : False
NSEC3CurrentSalt    : 879006FFA707C0F7

DistinguishedName   : DC=adatum.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com
ZoneName            : adatum.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : False
SignWithNSEC3       : False
NSEC3CurrentSalt    :

DistinguishedName   : DC=example.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com
ZoneName            : example.com
IsDsIntegrated      : True
IsReverseLookupZone : False
IsSigned            : False
SignWithNSEC3       : False
NSEC3CurrentSalt    :
#>
```

Retrieves the list of DNS zones from the specified domain controller.

### Example 2
```powershell
PS C:\> Get-ADSIDnsServerZone -Server 'lon-dc1.contoso.com' | Format-Table

<# Sample Output:
ZoneName           IsSigned IsReverseLookupZone
--------           -------- -------------------
_msdcs.contoso.com False    False
contoso.com        True     False
adatum.com         False    False
example.com        False    False
#>
```

Displays a condensed table of the DNS zones hosted on the specified domain controller.

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
Restricts the output to the DNS zone with the specified name. The value is matched case-insensitively against the zone's fully qualified domain name (FQDN).

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

### DSInternals.Common.DNS.DnsZone

## NOTES

## RELATED LINKS

[Get-ADSIDnsServerResourceRecord](Get-ADSIDnsServerResourceRecord.md)
[Get-ADDBDnsServerZone](Get-ADDBDnsServerZone.md)
