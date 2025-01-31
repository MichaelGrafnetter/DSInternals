---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADDBKdsRootKey.md
schema: 2.0.0
---

# Get-ADDBKdsRootKey

## SYNOPSIS
Reads KDS Root Keys from a ntds.dit. file. Can be used to aid DPAPI-NG decryption, e.g. SID-protected PFX files.

## SYNTAX

```
Get-ADDBKdsRootKey -DatabasePath <String> [-LogPath <String>] [<CommonParameters>]
```

## DESCRIPTION
KDS Root Keys are used to encrypt the following:

- SID-protected private keys in PFX certificate files
- BitLocker-enabled drives with SID protector
- Passwords of Group Managed Service Accounts (GMSA)
- DNSSEC signing keys

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBKdsRootKey -DatabasePath '.\ADBackup\Active Directory\ntds.dit'
<# Sample Output:
Id: 6a401799-8dd0-0b2c-3073-beb7ce2e734d
Version: 1
Creation Time: 7/27/2019 6:23:26 PM
Effective Time: 7/27/2019 8:23:26 AM
Domain Controller: CN=LON-DC1,OU=Domain Controllers,DC=contoso,DC=com
Key
  C16A0D16B80307D9CF102C7DB11F69FE015EB0DCD85C2FC0A5005C10E9DB963AC1E18BF161882ABEEAFF1B01CD50076F3C6F7807323253AB9598DBE027A77DD7
Key Derivation Function
  Algorithm: SP800_108_CTR_HMAC
  Parameters: {[0, SHA512]}
Secret Agreement
  Algorithm: DH
  Public Key Length: 2048
  Private Key Length: 512
  Parameters
    0c0200004448504d0001000087a8e61db4b6663cffbbd19c651959998ceef608660dd0f25d2ceed4435e3b00e00df8f1d61957d4faf7df4561b2aa3016c3d91134096fa 
    a3bf4296d830e9a7c209e0c6497517abd5a8a9d306bcf67ed91f9e6725b4758c022e0b1ef4275bf7b6c5bfc11d45f9088b941f54eb1e59bb8bc39a0bf12307f5c4fdb70
    c581b23f76b63acae1caa6b7902d52526735488a0ef13c6d9a51bfa4ab3ad8347796524d8ef6a167b5a41825d967e144e5140564251ccacb83e6b486f6b3ca3f7971506 
    026c0b857f689962856ded4010abd0be621c3a3960a54e710c375f26375d7014103a4b54330c198af126116d2276e11715f693877fad7ef09cadb094ae91e1a15973fb3 
    2c9b73134d0b2e77506660edbd484ca7b18f21ef205407f4793a1a0ba12510dbc15077be463fff4fed4aac0bb555be3a6c1b0c6b47b1bc3773bf7e8c6f62901228f8c28 
    cbb18a55ae31341000a650196f931c77a57f2ddf463e5e9ec144b777de62aaab8a8628ac376d282d6ed3864e67982428ebc831d14348f6f2f9193b5045af2767164e1df 
    c967c1fb3f2e55a4bd1bffe83b9c80d052b985d182ea0adb2a3b7313d3fe14c8484b1e052588b9b7d2bbd2df016199ecd06e1557cd0915b3353bbb64e0ec377fd028370 
    df92b52c7891428cdc67eb6184b523d1db246c32f63078490f00ef8d647d148d47954515e2327cfef98c582664b4c0f6cc41659
#>

PS C:\> .\CQDPAPINGPFXDecrypter.exe /pfx Certificate.p12 /master C16A0D16B80307D9CF102C7DB11F69FE015EB0DCD85C2FC0A5005C10E9DB963AC1E18BF161882ABEEAFF1B01CD50076F3C6F7807323253AB9598DBE027A77DD7
<# Sample Output:
Successfully decrypted password: VBGpKPryuiWBSyq/+CjC0WjNsnZ1xS3Hs6IqGZwa0BM=
#>
```

Retrieves a KDS Root Key from an AD database and then uses the "CQURE DPAPI NG PFX Decrypter" to decrypt the password of the PFX file.

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

### DSInternals.Common.Data.KdsRootKey

## NOTES

## RELATED LINKS

[Get-ADDBServiceAccount](Get-ADDBServiceAccount.md)
