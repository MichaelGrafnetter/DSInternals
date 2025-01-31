---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-ADDBPrimaryGroup.md
schema: 2.0.0
---

# Set-ADDBPrimaryGroup

## SYNOPSIS
Modifies the primaryGroupId attribute of an object in a ntds.dit file.

## SYNTAX

### ByName
```
Set-ADDBPrimaryGroup -PrimaryGroupId <Int32> [-SkipMetaUpdate] [-Force] [-SamAccountName] <String>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### BySID
```
Set-ADDBPrimaryGroup -PrimaryGroupId <Int32> [-SkipMetaUpdate] [-Force] -ObjectSid <SecurityIdentifier>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Set-ADDBPrimaryGroup -PrimaryGroupId <Int32> [-SkipMetaUpdate] [-Force] -DistinguishedName <String>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByGuid
```
Set-ADDBPrimaryGroup -PrimaryGroupId <Int32> [-SkipMetaUpdate] [-Force] -ObjectGuid <Guid>
 -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
Modifies the primaryGroupId attribute of an account in a ntds.dit file. The most relevant group relative identifiers (RIDs) include 512 for *Domain Admins*, 513 for *Domain Users*, and 519 for *Schema Admins*.

## EXAMPLES

### Example 1
```powershell
PS C:\> Set-ADDBPrimaryGroup -SamAccountName John `
                             -PrimaryGroupId 512 `
                             -DatabasePath 'D:\Windows\NTDS\ntds.dit'
```

Moves the account *John* from the default *Domain Users* group to *Domain Admins*.

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

### -PrimaryGroupId
Specifies the new primary group relative identifier (RID) that will be written to the database.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: gid, Group, PrimaryGroup, GroupId

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

### System.Int32

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Add-ADDBSidHistory](Add-ADDBSidHistory.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
