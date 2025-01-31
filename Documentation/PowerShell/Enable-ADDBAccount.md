---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Enable-ADDBAccount.md
schema: 2.0.0
---

# Enable-ADDBAccount

## SYNOPSIS
Enables an Active Directory account in an offline ntds.dit file.

## SYNTAX

### ByName
```
Enable-ADDBAccount [-SkipMetaUpdate] [-Force] [-SamAccountName] <String> -DatabasePath <String>
 [-LogPath <String>] [<CommonParameters>]
```

### BySID
```
Enable-ADDBAccount [-SkipMetaUpdate] [-Force] -ObjectSid <SecurityIdentifier> -DatabasePath <String>
 [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Enable-ADDBAccount [-SkipMetaUpdate] [-Force] -DistinguishedName <String> -DatabasePath <String>
 [-LogPath <String>] [<CommonParameters>]
```

### ByGuid
```
Enable-ADDBAccount [-SkipMetaUpdate] [-Force] -ObjectGuid <Guid> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
Finds an account in Active Directory database file and modifies the appropriate bit in its *userAccountControl* attribute.

## EXAMPLES

### Example 1
```powershell
PS C:\> Enable-ADDBAccount -SamAccountName john -DatabasePath .\ntds.dit
```

Finds an account with name *john* and enables it.

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
Specifies the identifier of an account on which to perform this operation.

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
Specifies the identifier of an account on which to perform this operation.

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
Specifies the identifier of an account on which to perform this operation.

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
Specifies the identifier of an account on which to perform this operation.

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

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Disable-ADDBAccount](Disable-ADDBAccount.md)
[Unlock-ADDBAccount](Unlock-ADDBAccount.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
