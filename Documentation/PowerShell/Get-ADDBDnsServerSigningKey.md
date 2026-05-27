---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBDnsServerSigningKey.md
schema: 2.0.0
---

# Get-ADDBDnsServerSigningKey

## SYNOPSIS
Retrieves DNSSEC signing key descriptors from an Active Directory database.

## SYNTAX

```
Get-ADDBDnsServerSigningKey [-ZoneName <String>] -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
This cmdlet retrieves the DNSSEC zone signing key (ZSK) and key signing key (KSK) descriptors that are stored in the specified Active Directory database file.
Only the key metadata is returned; use the Export-ADDBDnsServerSigningKey cmdlet to export the private key material.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBDnsServerSigningKey -DatabasePath '.\ntds.dit' -ZoneName 'contoso.com'

<# Sample Output:
ZoneName                      : contoso.com
KeyId                         : e6eb8329-330c-4dd3-9989-2827434cb3d5
KeyType                       : KeySigningKey
CryptoAlgorithm               : EcDsaP256Sha256
KeyLength                     : 256
CurrentState                  : Active
ZoneSignatureValidityPeriod   : 10.00:00:00
DnsKeySignatureValidityPeriod : 7.00:00:00
DSSignatureValidityPeriod     : 7.00:00:00
IsRolloverEnabled             : True
RolloverType                  : DoubleSignature
RolloverPeriod                : 755.00:00:00
InitialRolloverOffset         : 00:00:00
CurrentRolloverStatus         : NotRolling
NextRolloverAction            : Normal
LastRolloverTime              :
NextRolloverTime              : 6/10/2028 9:21:46 AM
NextKeyGenerationTime         :
KeyStorageProvider            : Microsoft Software Key Storage Provider
StoreKeysInAD                 : True
ActiveKey                     : f91d2384-895d-45a2-adf9-f037624a107f
ActiveKeyScope                : Default
NextKey                       :
NextKeyScope                  : Default
StandbyKey                    : ddf94e7e-0ed8-4bbb-9447-03fe66202874
StandbyKeyScope               : AddOnly

ZoneName                      : contoso.com
KeyId                         : 2e8c3148-04b3-462d-ae45-d87fe631fcee
KeyType                       : ZoneSigningKey
CryptoAlgorithm               : RsaSha256
KeyLength                     : 1024
CurrentState                  : Active
ZoneSignatureValidityPeriod   : 10.00:00:00
DnsKeySignatureValidityPeriod : 7.00:00:00
DSSignatureValidityPeriod     : 7.00:00:00
IsRolloverEnabled             : True
RolloverType                  : PrePublish
RolloverPeriod                : 90.00:00:00
InitialRolloverOffset         : 00:00:00
CurrentRolloverStatus         : NotRolling
NextRolloverAction            : Normal
LastRolloverTime              : 3/28/2026 12:35:27 AM
NextRolloverTime              : 8/15/2026 9:21:46 AM
NextKeyGenerationTime         : 5/17/2026 9:21:46 AM
KeyStorageProvider            : Microsoft Software Key Storage Provider
StoreKeysInAD                 : True
ActiveKey                     : 1c3737ea-f9a4-4902-b6a1-debc6227e627
ActiveKeyScope                : Default
NextKey                       : 0b2ec560-1db7-41aa-98c6-8bdacc8727e5
NextKeyScope                  : AddOnly
StandbyKey                    :
StandbyKeyScope               : Default
#>
```

Retrieves the DNSSEC signing key descriptors for the contoso.com zone from the specified Active Directory database file. The zone is protected by a key signing key (KSK) and a zone signing key (ZSK).

### Example 2
```powershell
PS C:\> Get-ADDBDnsServerSigningKey -DatabasePath '.\ntds.dit' | Format-Table

<# Sample Output:
ZoneName    KeyType        Algorithm       KeyLength State  RolloverType
--------    -------        ---------       --------- -----  ------------
contoso.com KeySigningKey  EcDsaP256Sha256       256 Active DoubleSignature
contoso.com ZoneSigningKey RsaSha256            1024 Active PrePublish
#>
```

Displays a condensed table of all DNSSEC signing keys stored in the specified Active Directory database file.

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

### DSInternals.Common.DNS.DnsSigningKeyDescriptor

## NOTES

## RELATED LINKS

[Export-ADDBDnsServerSigningKey](Export-ADDBDnsServerSigningKey.md)
[Get-ADSIDnsServerSigningKey](Get-ADSIDnsServerSigningKey.md)
[Get-ADDBDnsServerZone](Get-ADDBDnsServerZone.md)
