---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBBackupKey.md
schema: 2.0.0
---

# Get-ADDBBackupKey

## SYNOPSIS
Reads the DPAPI backup keys from a ntds.dit file.

## SYNTAX

```
Get-ADDBBackupKey -BootKey <Byte[]> -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION

Reads and decrypts Data Protection API (DPAPI) backup keys from an Active Directory database file. The output can be saved to the file system using the Save-DPAPIBlob cmdlet.

DPAPI is used by several components of Windows to securely store passwords, encryption keys and other sensitive data. When DPAPI is used in an Active Directory domain environment, a copy of user's master key is encrypted with a so-called DPAPI Domain Backup Key that is known to all domain controllers. Windows Server 2000 DCs use a symmetric key and newer systems use a public/private key pair. If the user password is reset and the original master key is rendered inaccessible to the user, the user's access to the master key is automatically restored using the backup key.

## EXAMPLES

### Example 1
```powershell
PS C:\> $key = Get-BootKey -SystemHiveFilePath '.\ADBackup\registry\SYSTEM'
PS C:\> Get-ADDBBackupKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                          -BootKey $key | Format-List
<# Sample Output:

FilePath          : ntds_legacy_b116cbfa-b881-43e6-ba85-ef3efa64ba22.key
KiwiCommand       : 
Type              : LegacyKey
DistinguishedName : CN=BCKUPKEY_b116cbfa-b881-43e6-ba85-ef3efa64ba22 
                    Secret,CN=System,DC=contoso,DC=com
KeyId             : b116cbfa-b881-43e6-ba85-ef3efa64ba22
Data              : {1, 0, 0, 0...}

FilePath          : 
KiwiCommand       : 
Type              : PreferredLegacyKeyPointer
DistinguishedName : CN=BCKUPKEY_P Secret,CN=System,DC=contoso,DC=com
KeyId             : b116cbfa-b881-43e6-ba85-ef3efa64ba22
Data              : {250, 203, 22, 177...}

FilePath          : ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pvk
KiwiCommand       : REM Add this parameter to at least the first dpapi::masterkey 
                    command: /pvk:"ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pvk"
Type              : RSAKey
DistinguishedName : CN=BCKUPKEY_290914ed-b1a8-482e-a89f-7caa217bf3c3 
                    Secret,CN=System,DC=contoso,DC=com
KeyId             : 290914ed-b1a8-482e-a89f-7caa217bf3c3
Data              : {2, 0, 0, 0...}

FilePath          : 
KiwiCommand       : 
Type              : PreferredRSAKeyPointer
DistinguishedName : CN=BCKUPKEY_PREFERRED Secret,CN=System,DC=contoso,DC=com
KeyId             : 290914ed-b1a8-482e-a89f-7caa217bf3c3
Data              : {237, 20, 9, 41...}
#>
```

Extracts the boot key (AKA SysKey or system key) from a backup of the SYSTEM registry hive and decrypts all DPAPI backup keys stored in the an Active Directory database file.

### Example 2
```powershell
PS C:\> Get-ADDBBackupKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                          -BootKey 0be7a2afe1713642182e9b96f73a75da |
             Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ChildItem -Path '.\Output' | Select-Object -ExpandProperty Name
<# Sample Output:
kiwiscript.txt
ntds_legacy_b116cbfa-b881-43e6-ba85-ef3efa64ba22.key
ntds_capi_4cee80c0-b6c6-406c-a68b-c0e5818bc436.cer
ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pfx
ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pvk
#>
```

Exports DPAPI backup keys to the Output directory.

## PARAMETERS

### -BootKey
Specifies the boot key (AKA system key) that will be used to decrypt values of secret attributes.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: key, SysKey, SystemKey

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.Common.Data.DPAPIBackupKey

## NOTES

## RELATED LINKS

[Save-DPAPIBlob](Save-DPAPIBlob.md)
[Get-ADReplBackupKey](Get-ADReplBackupKey.md)
[Get-LsaBackupKey](Get-LsaBackupKey.md)
