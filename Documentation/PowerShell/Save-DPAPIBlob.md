---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Save-DPAPIBlob.md
schema: 2.0.0
---

# Save-DPAPIBlob

## SYNOPSIS
Saves DPAPI and Credential Roaming data returned by the Get-ADReplBackupKey, Get-ADDBBackupKey, Get-ADReplAccount, Get-ADDBAccount and Get-ADSIAccount cmdlets to files for further processing.

## SYNTAX

### FromObject
```
Save-DPAPIBlob -DPAPIObject <DPAPIObject> [-DirectoryPath] <String> [<CommonParameters>]
```

### FromAccount
```
Save-DPAPIBlob -Account <DSAccount> [-DirectoryPath] <String> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBBackupKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                          -BootKey 0be7a2afe1713642182e9b96f73a75da |
             Save-DPAPIBlob -DirectoryPath .\Output
PS C:\> Get-ADDBAccount -All `
                -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                -BootKey 0be7a2afe1713642182e9b96f73a75da |
             Save-DPAPIBlob -DirectoryPath .\Output
```

Extracts DPAPI backup keys and roamed credentials (certificates, private keys and DPAPI master keys) to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

## PARAMETERS

### -Account
Specifies an Active Directory account whose DPAPI-related attribute values will be exported to the target directory.

```yaml
Type: DSAccount
Parameter Sets: FromAccount
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -DirectoryPath
Specifies the path to a target directory where the output file(s) will be saved.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path, OutputPath

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DPAPIObject
Specifies a DPAPI object (e.g. Domain Backup Key or Master Key) that will be saved to the target directory.

```yaml
Type: DPAPIObject
Parameter Sets: FromObject
Aliases: DPAPIBlob, Object, Blob, BackupKey

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### DSInternals.Common.Data.DPAPIObject

### DSInternals.Common.Data.DSAccount

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Get-ADDBBackupKey](Get-ADDBBackupKey.md)
[Get-ADReplBackupKey](Get-ADReplBackupKey.md)
[Get-LsaBackupKey](Get-LsaBackupKey.md)
[Get-ADReplAccount](Get-ADReplAccount.md)
[Get-ADDBAccount](Get-ADDBAccount.md) 
