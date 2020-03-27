---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Save-DPAPIBlob.md
schema: 2.0.0
---

# Save-DPAPIBlob

## SYNOPSIS
Saves DPAPI and Credential Roaming data retrieved from Active Directory to the filesystem for further processing.

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

This cmdlet saves DPAPI-related data retrieved from Active Directory to a selected directory. It also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys and to decode the certificates.
Supports DPAPI backup keys returned by the Get-ADReplBackupKey, Get-ADDBBackupKey, and Get-LsaBackupKey cmdlets and roamed credentials (certificates, private keys, and DPAPI master keys) returned by the Get-ADReplAccount, Get-ADDBAccount, and Get-ADSIAccount cmdlets.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBBackupKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit' `
                          -BootKey 0be7a2afe1713642182e9b96f73a75da |
             Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADDBAccount -All -DatabasePath '.\ADBackup\Active Directory\ntds.dit' |
             Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ChildItem -Path '.\Output' -Recurse -File |
            Foreach-Object { $PSItem.FullName.Replace((Resolve-Path -Path '.\Output'), '') }
<# Sample Output:
\kiwiscript.txt
\ntds_capi_4cee80c0-b6c6-406c-a68b-c0e5818bc436.cer
\ntds_capi_4cee80c0-b6c6-406c-a68b-c0e5818bc436.pfx
\ntds_capi_4cee80c0-b6c6-406c-a68b-c0e5818bc436.pvk
\ntds_legacy_d78736ad-5206-4eda-bfd4-cd10cc49d163.key
\Abbi\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1304\99c6f954ca07d75267f9a369a0bf5cd3_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Abbi\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1304\ba7577742c7900c29f8e7f8193ca5f6d_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Abbi\Protect\S-1-5-21-4534338-1127018997-2609994386-1304\eadae2b5-3933-434a-9bcf-804175877104
\Abbi\SystemCertificates\My\Certificates\366004B5FA21294B80B22DA1385F414C70DF611B
\Abbi\SystemCertificates\My\Certificates\6441367E7BF2D4C7DAA1CF27C72D6552F4A48B48
\Administrator\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-500\0b0c01d1f2bb6db4cd9496cd5e1214d6_f8b7bbef-d227-4ac7-badd-3a238a7f741e
\Administrator\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-500\2907acacb201238bd89fe63b20c6d23b_f8b7bbef-d227-4ac7-badd-3a238a7f741e
\Administrator\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-500\701577141985b6923998dcca035c007a_f8b7bbef-d227-4ac7-badd-3a238a7f741e
\Administrator\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-500\d881dc8bbed7c3a08f03b01de4b9f45f_f8b7bbef-d227-4ac7-badd-3a238a7f741e
\Administrator\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-500\e1b4cc613d831f27c664af17b8f98021_f8b7bbef-d227-4ac7-badd-3a238a7f741e
\Administrator\Protect\S-1-5-21-4534338-1127018997-2609994386-500\47070660-c259-4d90-8bc9-187605323450
\Administrator\Protect\S-1-5-21-4534338-1127018997-2609994386-500\e13655bb-9519-45aa-abf8-a50a7b01317a
\Administrator\SystemCertificates\My\Certificates\01ADA5237C2D2D1F1571247A239CA66B31885389
\Administrator\SystemCertificates\My\Certificates\5479CDDE0747E2CB5DF64F28A9E4AD3266AB27AF
\Administrator\SystemCertificates\My\Certificates\574E4687133998544C0095C7B348C52CD398182E
\Administrator\SystemCertificates\My\Certificates\B422F98237039C9836D24E22E5A92FCEC507EF89
\Administrator\SystemCertificates\My\Certificates\DBE2B5417D56BC061B05B7265A47D3595EEC6A32
\Administrator\SystemCertificates\Request\Certificates\AE1EBACC333E48E80C5DED7D0C644D80417CB6EC
\Lara\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1359\1eceade740dd71b94c3a7333522b9859_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Lara\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1359\2995fb4c62c9211bc265c89fe1c85061_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Lara\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1359\3183cd1aef41afc9af73e231607b5266_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Lara\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1359\4f8bd0d10c208c8d57d2a1babd288a83_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Lara\Protect\S-1-5-21-4534338-1127018997-2609994386-1359\5f6d65d9-c363-4c78-af8d-034fb80efc5a
\Lara\SystemCertificates\My\Certificates\1307CE05C8247AA08508302431B6A99647FF600E
\Lara\SystemCertificates\My\Certificates\7B0928AF99A3244E73F7F17957ABD5A80818B210
\Lara\SystemCertificates\My\Certificates\90E1D7F90AD73F66F2C8F60120C256D038FD1F2C
\Lara\SystemCertificates\My\Certificates\DB690E9D99D094D3E9746DE484D3050951516E29
\Logan\Crypto\RSA\S-1-5-21-4534338-1127018997-2609994386-1272\fd56f510920bd55b31ff5207eafda8c8_9e75a609-18c7-4c98-8cd0-c34c3aeae423
\Logan\Protect\S-1-5-21-4534338-1127018997-2609994386-1272\9c6cc9e0-b5f8-48f4-a478-305ad77fceab
\Logan\SystemCertificates\My\Certificates\5D7A3A4FE8ADF5A61C5079EB7FDD1507B2753682
#>

PS C:\> Get-Content -Path '.\Output\kiwiscript.txt'
<# Sample Output:
REM Add this parameter to at least the first dpapi::masterkey command: /pvk:"ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pvk"
dpapi::masterkey /in:"Install\Protect\S-1-5-21-1236425271-2880748467-2592687428-1000\0f2ca69c-c144-4d80-905f-a6bcdfb0d659" /sid:S-1-5-21-1236425271-2880748467-2592687428-1000
dpapi::masterkey /in:"Install\Protect\S-1-5-21-1236425271-2880748467-2592687428-1000\acdad60e-bcc0-48fb-9ceb-7514ca5aa558" /sid:S-1-5-21-1236425271-2880748467-2592687428-1000
dpapi::cng /in:"Install\Crypto\Keys\002F8F86566CEFBC8694EE7F5BB24A5FF2BA2C18"
dpapi::cng /in:"Install\Crypto\Keys\476D927F1B009662D46D785BA58BD8E9DB42F687"
crypto::system /file:"Install\SystemCertificates\My\Certificates\EA4AD6192A82AB059BFA5E774515FDE0DA604160" /export
crypto::system /file:"Install\SystemCertificates\My\Certificates\D6F23BB7BD8C0099DF5F1324507EA0CA3DE7DEAB" /export
dpapi::masterkey /in:"john\Protect\S-1-5-21-1236425271-2880748467-2592687428-1109\bfefb3a6-5cdc-44f9-8521-a31feb3acdb1" /sid:S-1-5-21-1236425271-2880748467-2592687428-1109
dpapi::masterkey /in:"john\Protect\S-1-5-21-1236425271-2880748467-2592687428-1109\c14e7f69-3bf5-4c49-92d8-78d759d74ece" /sid:S-1-5-21-1236425271-2880748467-2592687428-1109
crypto::system /file:"john\SystemCertificates\My\Certificates\AF839B040D1257997A8D83EE71F96918F4C3EA01" /export
dpapi::cng /in:"john\Crypto\Keys\9F95F8E4F381BFFFD22B5EFAA013E53268451310"
dpapi::cng /in:"john\Crypto\Keys\C9ABDF8DC38EA2BA2E20AEC770D91210FF919F87"
crypto::system /file:"john\SystemCertificates\My\Certificates\DEFFADB62EE547CB88973DF664C4DC958E8E64D8" /export
crypto::system /file:"john\SystemCertificates\My\Certificates\49FD324E5CC4A6020AC9D12D4311C7B33393A1C4" /export
crypto::system /file:"john\SystemCertificates\My\Certificates\4E951C29567A261B2E90C94BCCEFAE1FA878A2CB" /export
dpapi::capi /in:"john\Crypto\RSA\S-1-5-21-1236425271-2880748467-2592687428-1109\0581f4e6088649266038726d9f8786a9_edc46440-65c9-41ce-aaeb-73754e0e38c8"
dpapi::capi /in:"john\Crypto\RSA\S-1-5-21-1236425271-2880748467-2592687428-1109\4771dfabcc8ad1ec2c84c489df041fad_edc46440-65c9-41ce-aaeb-73754e0e38c8"
#>
```

Extracts DPAPI backup keys and roamed credentials (certificates, private keys, and DPAPI master keys) from an Active Directory database file and saves them to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

### Example 2
```powershell
PS C:\> Get-ADReplBackupKey -Server 'lon-dc1.adatum.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADReplAccount -All -Server 'lon-dc1.adatum.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
```

Replicates all DPAPI backup keys and roamed credentials (certificates, private keys, and DPAPI master keys) from the target Active Directory domain controller and saves them to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

### Example 3
```powershell
PS C:\> Get-LsaBackupKey -ComputerName 'lon-dc1.contoso.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
```

Retrieves DPAPI backup keys from the target domain controller through the MS-LSAD protocol. Also retrieves roamed credentials (certificates, private keys, and DPAPI master keys) from this domain controller through LDAP and saves them to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

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
[Get-ADSIAccount](Get-ADSIAccount.md)
