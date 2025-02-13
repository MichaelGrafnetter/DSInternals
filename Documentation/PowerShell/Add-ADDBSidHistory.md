---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Add-ADDBSidHistory.md
schema: 2.0.0
---

# Add-ADDBSidHistory

## SYNOPSIS
Adds one or more values to the sIDHistory attribute of an object in a ntds.dit file.

> [!WARNING]
> This cmdlet has been removed from the DSInternals PowerShell module.
> Information in this topic is provided for reference purposes only.

## SYNTAX

### ByName
```
Add-ADDBSidHistory -SidHistory <SecurityIdentifier[]> [-SkipMetaUpdate] [-Force] [-SamAccountName] <String>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### BySID
```
Add-ADDBSidHistory -SidHistory <SecurityIdentifier[]> [-SkipMetaUpdate] [-Force]
 -ObjectSid <SecurityIdentifier> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Add-ADDBSidHistory -SidHistory <SecurityIdentifier[]> [-SkipMetaUpdate] [-Force] -DistinguishedName <String>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByGuid
```
Add-ADDBSidHistory -SidHistory <SecurityIdentifier[]> [-SkipMetaUpdate] [-Force] -ObjectGuid <Guid>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet can be used to add any value to the sIDHistory attribute by directly modifying the Active Directory database.
Note that the Active Directory Migration Tool (ADMT) is the only supported way of modifying the sIDHistory attribute. Improper usage of this cmdlet may cause irreversible damage to the target Active Directory environment.

## EXAMPLES

### Example 1
```powershell
PS C:\> Stop-Service -Name ntds -Force
PS C:\> Add-ADDBSidHistory -SamAccountName John `
                           -SidHistory 'S-1-5-21-3623811102-3361044346-30300840-512',
                                       'S-1-5-21-3623811102-3361044346-30300840-519' `
                           -DatabasePath C:\Windows\NTDS\ntds.dit
PS C:\> Start-Service -Name ntds
```

Adds the SIDs of the *Domain Admins* and *Enterprise Admins* groups into user *John*'s sIDHistory.

### Example 2
```powershell
PS C:\> Import-Csv user.csv | Add-ADDBSidHistory -DatabasePath C:\Windows\NTDS\ntds.dit
```

Imports a CSV file containing *SamAccountName* and *SidHistory* columns into a nds.dit file.

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

### -DistinguishedName
Specifies the identifier of an object on which to perform this operation.

```yaml
Type: String
Parameter Sets: ByDN
Aliases: dn

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Force
Forces the cmdlet to perform the desired operation.

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

### -ObjectGuid
Specifies the identifier of an object on which to perform this operation.

```yaml
Type: Guid
Parameter Sets: ByGuid
Aliases: Guid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ObjectSid
Specifies the identifier of an object on which to perform this operation.

```yaml
Type: SecurityIdentifier
Parameter Sets: BySID
Aliases: Sid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SamAccountName
Specifies the identifier of an object on which to perform this operation.

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, sam

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SidHistory
Specifies an array of security IDs (SIDs) that will be added to the SID History of the target object.

```yaml
Type: SecurityIdentifier[]
Parameter Sets: (All)
Aliases: hist, History

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SkipMetaUpdate
Indicates that the replication metadata of the affected object should not be updated.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: SkipMeta, NoMetaUpdate, NoMeta, SkipObjMeta, NoObjMeta, SkipMetaDataUpdate, NoMetaDataUpdate

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Security.Principal.SecurityIdentifier[]

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Set-ADDBPrimaryGroup](Set-ADDBPrimaryGroup.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADDBDomainController](Get-ADDBDomainController.md)
