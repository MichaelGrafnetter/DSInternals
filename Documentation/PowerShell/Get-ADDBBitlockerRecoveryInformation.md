---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBBitLockerRecoveryInformation.md
schema: 2.0.0
---

# Get-ADDBBitLockerRecoveryInformation

## SYNOPSIS
Reads BitLocker recovery passwords from a ntds.dit file.

## SYNTAX

### ByKeyIdentifier
```
Get-ADDBBitLockerRecoveryInformation -RecoveryGuid <Guid> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByComputerName
```
Get-ADDBBitLockerRecoveryInformation -ComputerName <String> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### All
```
Get-ADDBBitLockerRecoveryInformation [-All] -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

### ByDN
```
Get-ADDBBitLockerRecoveryInformation -DistinguishedName <String> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByGuid
```
Get-ADDBBitLockerRecoveryInformation -ObjectGuid <Guid> -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION

BitLocker recovery information (msFVE-RecoveryInformation objects) can be used to unlock volumes encrypted using BitLocker Drive Encryption. 

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBBitLockerRecoveryInformation -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
<# Sample Output:
[TODO]
#>
```

Retrieves the BitLocker Recovery Keys from an AD database.

## PARAMETERS

### -All
{{ Fill All Description }}

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ComputerName
{{ Fill ComputerName Description }}

```yaml
Type: String
Parameter Sets: ByComputerName
Aliases: Computer, SamAccountName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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
{{ Fill DistinguishedName Description }}

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
{{ Fill LogPath Description }}

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
{{ Fill ObjectGuid Description }}

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

### -RecoveryGuid
Specifies the RecoveryGuid to search, for instance, 11c43ee8-b9d3-4e51-b73f-bd9dda66e29c

```yaml
Type: Guid
Parameter Sets: ByKeyIdentifier
Aliases: KeyIdentifier, KeyId, RecoveryId, KeyProtectorId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid

### System.String

## OUTPUTS

### DSInternals.Common.Data.BitLockerRecoveryInformation

## NOTES

## RELATED LINKS
