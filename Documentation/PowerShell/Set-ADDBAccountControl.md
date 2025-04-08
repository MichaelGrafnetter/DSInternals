---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-ADDBAccountControl.md
schema: 2.0.0
---

# Set-ADDBAccountControl

## SYNOPSIS
Modifies user account control (UAC) values for an Active Directory account in an offline ntds.dit file.

## SYNTAX

### ByName
```
Set-ADDBAccountControl [-Enabled <Boolean>] [-CannotChangePassword <Boolean>] [-PasswordNeverExpires <Boolean>]
 [-SmartcardLogonRequired <Boolean>] [-UseDESKeyOnly <Boolean>] [-HomedirRequired <Boolean>] [-SkipMetaUpdate]
 [-Force] [-SamAccountName] <String> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### BySID
```
Set-ADDBAccountControl [-Enabled <Boolean>] [-CannotChangePassword <Boolean>] [-PasswordNeverExpires <Boolean>]
 [-SmartcardLogonRequired <Boolean>] [-UseDESKeyOnly <Boolean>] [-HomedirRequired <Boolean>] [-SkipMetaUpdate]
 [-Force] -ObjectSid <SecurityIdentifier> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Set-ADDBAccountControl [-Enabled <Boolean>] [-CannotChangePassword <Boolean>] [-PasswordNeverExpires <Boolean>]
 [-SmartcardLogonRequired <Boolean>] [-UseDESKeyOnly <Boolean>] [-HomedirRequired <Boolean>] [-SkipMetaUpdate]
 [-Force] -DistinguishedName <String> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByGuid
```
Set-ADDBAccountControl [-Enabled <Boolean>] [-CannotChangePassword <Boolean>] [-PasswordNeverExpires <Boolean>]
 [-SmartcardLogonRequired <Boolean>] [-UseDESKeyOnly <Boolean>] [-HomedirRequired <Boolean>] [-SkipMetaUpdate]
 [-Force] -ObjectGuid <Guid> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
Finds an account in Active Directory database file and modifies the appropriate bit(s) in its *userAccountControl* attribute.

## EXAMPLES

### Example 1
```powershell
PS C:\> Set-ADDBAccountControl -SamAccountName john -SmartcardLogonRequired $false -PasswordNeverExpires $true -DatabasePath .\ntds.dit
```

Finds an account with name *john*, disables the smart card logon requirement, and unexpires its password.

## PARAMETERS

### -CannotChangePassword
Indicates whether the account can change its password.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Enabled
Indicates whether the account is enabled.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### -HomedirRequired
Indicates whether a home directory is required for the account.

```yaml
Type: Boolean
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

### -PasswordNeverExpires
Indicates whether the password of the account can expire.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### -SmartcardLogonRequired
Indicates whether a smart card is required to logon.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseDESKeyOnly
Indicates whether the account is restricted to use only Data Encryption Standard (DES) encryption types for keys.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

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

[Set-ADDBAccountPassword](Set-ADDBAccountPassword.md)
[Set-ADDBAccountPasswordHash](Set-ADDBAccountPasswordHash.md)
[Enable-ADDBAccount](Enable-ADDBAccount.md)
[Disable-ADDBAccount](Disable-ADDBAccount.md)
[Unlock-ADDBAccount](Unlock-ADDBAccount.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
