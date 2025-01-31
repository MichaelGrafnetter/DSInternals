---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-ADDBAccountPassword.md
schema: 2.0.0
---

# Set-ADDBAccountPassword

## SYNOPSIS
Sets the password for a user, computer, or service account stored in a ntds.dit file.

## SYNTAX

### ByName
```
Set-ADDBAccountPassword -NewPassword <SecureString> -BootKey <Byte[]> [-SkipMetaUpdate] [-Force]
 [-SamAccountName] <String> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### BySID
```
Set-ADDBAccountPassword -NewPassword <SecureString> -BootKey <Byte[]> [-SkipMetaUpdate] [-Force]
 -ObjectSid <SecurityIdentifier> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Set-ADDBAccountPassword -NewPassword <SecureString> -BootKey <Byte[]> [-SkipMetaUpdate] [-Force]
 -DistinguishedName <String> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByGuid
```
Set-ADDBAccountPassword -NewPassword <SecureString> -BootKey <Byte[]> [-SkipMetaUpdate] [-Force]
 -ObjectGuid <Guid> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION

Generates new password hashes of the given password, including NT hash, WDigest hashes and Kerberos DES, AES128 and AES256 keys and encrypts them into the database using boot key.

## EXAMPLES

### Example 1
```powershell
PS C:\> $pass = Read-Host -AsSecureString -Prompt 'Provide new password for user john'
PS C:\> Set-ADDBAccountPassword -SamAccountName john `
                                -NewPassword $pass `
                                -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                                -BootKey 0be7a2afe1713642182e9b96f73a75da
```

Performs an offline password reset for user *john*.

## PARAMETERS

### -BootKey
Specifies the boot key (AKA system key) that will be used to decrypt/encrypt values of secret attributes.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: Key, SysKey, SystemKey

Required: True
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

### -NewPassword
Specifies a new password value. This value is stored as an encrypted string.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases: Password, Pwd, Pass, AccountPassword, p

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

### System.Security.SecureString

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Set-ADDBAccountPasswordHash](Set-ADDBAccountPasswordHash.md)
[Set-SamAccountPasswordHash](Set-SamAccountPasswordHash.md)
[Unlock-ADDBAccount](Unlock-ADDBAccount.md)
[Get-BootKey](Get-BootKey.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
