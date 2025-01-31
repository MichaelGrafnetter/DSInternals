---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Get-ADDBServiceAccount

## SYNOPSIS
Reads all Group Managed Service Accounts (gMSAs) from a ntds.dit file, while deriving their current passwords from KDS root keys.

## SYNTAX

```
Get-ADDBServiceAccount [-EffectiveTime <DateTime>] -DatabasePath <String> [-LogPath <String>]
 [<CommonParameters>]
```

## DESCRIPTION
As none of the required information is encrypted, the BootKey is not required.
Does not work on database files from RODCs.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADDBServiceAccount -DatabasePath 'C:\ADBackup\ntds.dit'
<# Sample Output:
DistinguishedName: CN=svc_adfs,CN=Managed Service Accounts,DC=contoso,DC=com
Sid: S-1-5-21-2468531440-3719951020-3687476655-1109
Guid: 53c845f7-d9cd-471b-a364-e733641dcc86
SamAccountName: svc_adfs$
Description: ADFS Service Account
Enabled: True
Deleted: False
UserAccountControl: WorkstationAccount
SupportedEncryptionTypes: RC4_HMAC, AES128_CTS_HMAC_SHA1_96, AES256_CTS_HMAC_SHA1_96
ServicePrincipalName: {http/login.contoso.com, host/login.contoso.com}
WhenCreated: 9/9/2023 5:02:05 PM
PasswordLastSet: 9/9/2023 5:02:06 PM
ManagedPasswordInterval: 30
ManagedPasswordId: RootKey=7dc95c96-fa85-183a-dff5-f70696bf0b11, Cycle=9/9/2023 10:00:00 AM (L0=361, L1=26, L2=24)
ManagedPasswordPreviousId:
KDS Derived Secrets
  NTHash: 0b5fbfb646dd7bce4f160ad69edb86ba
  Kerberos Keys
    AES256_CTS_HMAC_SHA1_96
      Key: 5dcc418cd0a30453b267e6e5b158be4b4d80d23fd72a6ae4d5bd07f023517117
      Iterations: 4096
    AES128_CTS_HMAC_SHA1_96
      Key: 8e1e66438a15d764ae2242eefd15e09a
      Iterations: 4096
#>
```

Reads all Group Managed Service Accounts (gMSAs) from the specified ntds.dit file, while deriving their current passwords from KDS root keys.

### Example 2
```powershell
PS C:\> Get-ADDBServiceAccount -EffectiveTime (Get-Date).AddMonths(1) -DatabasePath 'C:\ADBackup\ntds.dit'
```

Reads all Group Managed Service Accounts (gMSAs) from the specified ntds.dit file, while deriving their future passwords from KDS root keys.

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

### -EffectiveTime
Specifies the date and time at which the fenerated credentials should be valid. Defaults to the current time. 

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases: EffectiveDate, PasswordLastSet, PwdLastSet, Date, Time, d, t

Required: False
Position: Named
Default value: [datetime]::Now
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

### DSInternals.Common.Data.GroupManagedServiceAccount

## NOTES

## RELATED LINKS

[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
