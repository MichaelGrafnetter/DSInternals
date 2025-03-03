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
PS C:\> Get-ADDBBitLockerRecoveryInformation -All -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
<# Sample Output:
ComputerName RecoveryGuid                         RecoveryPassword
------------ ------------                         ----------------
PC01         704b1998-54ea-4899-8f46-81628b6a0731 366561-423260-035024-137224-631070-580492-357566-596908
PC02         caeaa622-6c6a-4d2b-8e33-29e46df659af 782066-216356-283624-291397-405614-078166-321530-943804
#>
```

Retrieves all BitLocker recovery keys from an AD database.

## PARAMETERS

### -All
Indicates that all BitLocker recovery keys should be read from the selected database.

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
Specifies the name of the computer whose BitLocker recovery keys should be fetched from the database.

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
Specifies the distinguished name of the recovery key to fetch from the database.

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
Specifies the recovery key object identifier to search for, for instance, 9a36024b-3e97-4305-8315-5ed0ff646367.

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
Specifies the recovery key identifier to search for, for instance, 11c43ee8-b9d3-4e51-b73f-bd9dda66e29c.

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

[Get-ADDBDomainController](Get-ADDBDomainController.md)
