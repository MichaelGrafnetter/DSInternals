---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-LsaBackupKey.md
schema: 2.0.0
---

# Get-LsaBackupKey

## SYNOPSIS
Reads the DPAPI backup keys from a domain controller through the LSARPC protocol.

## SYNTAX

```
Get-LsaBackupKey [[-ComputerName] <String>] [<CommonParameters>]
```

## DESCRIPTION

Reads the Data Protection API (DPAPI) backup keys from an Active Directory domain controller through the MS-LSAD (AKA LSARPC) protocol. The output can be saved to the file system using the Save-DPAPIBlob cmdlet.

DPAPI is used by several components of Windows to securely store passwords, encryption keys and other sensitive data. When DPAPI is used in an Active Directory domain environment, a copy of user's master key is encrypted with a so-called DPAPI Domain Backup Key that is known to all domain controllers. Windows Server 2000 DCs use a symmetric key and newer systems use a public/private key pair. If the user password is reset and the original master key is rendered inaccessible to the user, the user's access to the master key is automatically restored using the backup key.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-LsaBackupKey -ComputerName LON-DC1
<# Sample Output:

FilePath          : ntds_capi_b1c56a3e-ddf7-41dd-a5f3-44a2ed27a96d.pvk
KiwiCommand       : REM Add this parameter to at least the first dpapi::masterkey command:
                    /pvk:"ntds_capi_b1c56a3e-ddf7-41dd-a5f3-44a2ed27a96d.pvk"
Type              : RSAKey
DistinguishedName :
KeyId             : b1c56a3e-ddf7-41dd-a5f3-44a2ed27a96d
Data              : {2, 0, 0, 0...}

FilePath          : ntds_legacy_7882b20e-96ef-4ce5-a2b9-3efdccbbce28.key
KiwiCommand       :
Type              : LegacyKey
DistinguishedName :
KeyId             : 7882b20e-96ef-4ce5-a2b9-3efdccbbce28
Data              : {1, 0, 0, 0...}
#>
```

Displays the DPAPI domain backup keys.

### Example 2
```powershell
PS C:\> Get-LsaBackupKey -ComputerName LON-DC1 | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ChildItem -Path '.\Output' | Select-Object -ExpandProperty Name
<# Sample Output:
kiwiscript.txt
ntds_legacy_b116cbfa-b881-43e6-ba85-ef3efa64ba22.key
ntds_capi_4cee80c0-b6c6-406c-a68b-c0e5818bc436.cer
ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pfx
ntds_capi_290914ed-b1a8-482e-a89f-7caa217bf3c3.pvk
#>
```

Saves the DPAPI domain backup keys to the Output directory.

## PARAMETERS

### -ComputerName

Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

The default is the local computer. To specify the local computer, such as in a list of computer names, use "localhost", the local computer name, or a dot (.).

```yaml
Type: String
Parameter Sets: (All)
Aliases: Server, ServerName, Computer, Machine, MachineName, System, SystemName

Required: False
Position: 1
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

Administrative permissions on the target domain controller (DC) are required in order to retrieve DPAPI backup keys.

## RELATED LINKS

[Save-DPAPIBlob](Save-DPAPIBlob.md)
[Get-ADDBBackupKey](Get-ADDBBackupKey.md)
[Get-ADReplBackupKey](Get-ADReplBackupKey.md)