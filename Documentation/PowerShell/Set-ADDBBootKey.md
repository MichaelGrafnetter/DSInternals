---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-ADDBBootKey.md
schema: 2.0.0
---

# Set-ADDBBootKey

## SYNOPSIS
Re-encrypts a ntds.dit file with a new BootKey/SysKey.

## SYNTAX

```
Set-ADDBBootKey -OldBootKey <Byte[]> [-NewBootKey <Byte[]>] [-Force] -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
Decrypts the password encryption key list from the pekList domain attribute using the current/old boot key and re-encrypts it using a new one. This might be useful during some DC restore operations. Note that this procedure is highly unsupported by Microsoft.

## EXAMPLES

### Example 1
```powershell
PS C:\> Set-ADDBBootKey -DatabasePath 'C:\Backup\Active Directory\ntds.dit' `
                        -LogPath 'C:\Backup\Active Directory' `
                        -OldBootKey 610bc29e6f62ca7004e9872cd51a0116 `
                        -NewBootKey 6ffec6b70dc863db1906a5507c0576ee
```

Re-encrypts the ntds.dit file with a new boot key.

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

### -NewBootKey
Specifies the new boot key (AKA system key) that will be used to re-encrypt the password encryption key (pekList) stored in the target Active Directory database.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: NewKey, New, NewSysKey

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OldBootKey
Specifies the current boot key (AKA system key) that will be used to decrypt the password encryption key (pekList) stored in the target Active Directory database.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: OldKey, Old, OldSysKey

Required: True
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

### None

## NOTES

## RELATED LINKS

[Get-BootKey](Get-BootKey.md)
[New-ADDBRestoreFromMediaScript](New-ADDBRestoreFromMediaScript.md)
