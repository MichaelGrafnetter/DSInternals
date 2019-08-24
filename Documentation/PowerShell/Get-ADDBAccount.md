---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBAccount.md
schema: 2.0.0
---

# Get-ADDBAccount

## SYNOPSIS
Reads one or more accounts from a ntds.dit file, including secret attributes.

## SYNTAX

### All
```
Get-ADDBAccount [-All] [-BootKey <Byte[]>] -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByName
```
Get-ADDBAccount [-BootKey <Byte[]>] [-SamAccountName] <String> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### BySID
```
Get-ADDBAccount [-BootKey <Byte[]>] -ObjectSid <SecurityIdentifier> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByDN
```
Get-ADDBAccount [-BootKey <Byte[]>] -DistinguishedName <String> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByGuid
```
Get-ADDBAccount [-BootKey <Byte[]>] -ObjectGuid <Guid> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -All
Indicates that all accounts will be read from the selected database.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: AllAccounts, ReturnAllAccounts

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BootKey
Specifies the boot key (AKA system key) that will be used to decrypt values of secret attributes.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: key, SysKey, SystemKey

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
Specifies the identifier of an account that will be retrieved from the database.

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
Specifies the identifier of an account that will be retrieved from the database.

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
Specifies the identifier of an account that will be retrieved from the database.

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
Specifies the identifier of an account that will be retrieved from the database.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### DSInternals.Common.Data.DSAccount

## NOTES

## RELATED LINKS

[Get-ADReplAccount](Get-ADDBAccount.md)
[Get-ADSIAccount](Get-ADSIAccount.md)
[Test-PasswordQuality](Test-PasswordQuality.md)
[Save-DPAPIBlob](Save-DPAPIBlob.md)
