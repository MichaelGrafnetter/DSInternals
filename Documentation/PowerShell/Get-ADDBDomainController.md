---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBDomainController.md
schema: 2.0.0
---

# Get-ADDBDomainController

## SYNOPSIS
Reads information about the originating DC from a ntds.dit file, including domain name, domain SID, DC name and DC site.

## SYNTAX

```
Get-ADDBDomainController -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION

Reads domain controller (DC) infromation from a ntds.dit file that is either retrieved from an offline DC or from an (IFM) backup.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBDomainController -DatabasePath .\ntds.dit
<# Sample Output:
Name                       : LON-DC1
DNSHostName                : LON-DC1.contoso.com
ServerReference            : CN=LON-DC1,OU=Domain Controllers,DC=contoso,DC=com
DomainName                 : contoso.com
ForestName                 : contoso.com
NetBIOSDomainName          : contoso
DomainSid                  : S-1-5-21-1236425271-2880748467-2592687428
DomainGuid                 : 262d915a-3c58-4614-86c0-f9fb3f1aa1cd
Guid                       : 71ccee43-1c03-4ab1-910c-ed4168df5a33
Sid                        : S-1-5-21-1236425271-2880748467-2592687428-1111
DomainMode                 : WinThreshold
ForestMode                 : WinThreshold
SiteName                   : Default-First-Site-Name
DsaGuid                    : 0a8574e2-9361-4f3c-8528-ca73d7534f4b
InvocationId               : 14a1b16d-591c-45bc-a342-153090027bbc
IsADAM                     : False
IsGlobalCatalog            : True
Options                    : GlobalCatalog
OSName                     : Windows Server 2019 Datacenter
OSVersion                  : 10.0
OSVersionMajor             : 10
OSVersionMinor             : 0
DomainNamingContext        : DC=contoso,DC=com
ConfigurationNamingContext : CN=Configuration,DC=contoso,DC=com
SchemaNamingContext        : CN=Schema,CN=Configuration,DC=contoso,DC=com
WritablePartitions         : {DC=contoso,DC=com, CN=Configuration,DC=contoso,DC=com, CN=Schema,CN=Configuration,DC=contoso,DC=com, DC=DomainDnsZones,DC=contoso,DC=com...}
State                      : BackedUp
HighestCommittedUsn        : 69642
UsnAtIfm                   :
BackupUsn                  : 65812
BackupExpiration           : 2/4/2020 6:32:27 AM
Epoch                      : 961
#>
```

Reads DC information from a *ntds.dit* file located in the working directory.

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

### DSInternals.PowerShell.DomainController

## NOTES

## RELATED LINKS

[Set-ADDBDomainController](Set-ADDBDomainController.md)
[Set-ADDBPrimaryGroup](Set-ADDBPrimaryGroup.md)
[Add-ADDBSidHistory](Add-ADDBSidHistory.md)
