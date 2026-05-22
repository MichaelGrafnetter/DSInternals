---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIServiceAccount.md
schema: 2.0.0
---

# Get-ADSIServiceAccount

## SYNOPSIS
Reads all Group Managed Service Accounts (gMSAs) and Delegated Managed Service Accounts (dMSAs) from a domain controller through LDAP, while deriving their current passwords from KDS root keys.

## SYNTAX

```
Get-ADSIServiceAccount [-EffectiveTime <DateTime>] [-KdsRootKeys <KdsRootKey[]>] [-Server <String>]
 [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION

Retrieves all Group Managed Service Accounts (gMSAs) and Delegated Managed Service Accounts (dMSAs) from the target domain controller using ADSI/LDAP. Managed passwords are derived from the available KDS root keys and the gMSA / dMSA password identifier metadata.

By default, the interval ID stored in `msDS-ManagedPasswordId` is used as-is to derive the current managed password — no password-cycle math is performed. Supplying `-EffectiveTime` instead computes the password cycle for a given point in time (past or future).

The KDS root keys are normally read from the configuration naming context of the target domain controller, which typically requires Domain Admin privileges. As an alternative, the keys can be supplied explicitly using the `-KdsRootKeys` parameter, for instance when they have been retrieved through the MS-DRSR protocol or an offline ntds.dit file.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADSIServiceAccount -Server 'lon-dc1.contoso.com'
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
KDS Derived Secrets (Calculated)
  EffectivePasswordId: RootKey=7dc95c96-fa85-183a-dff5-f70696bf0b11, Cycle=9/9/2023 10:00:00 AM (L0=361, L1=26, L2=24)
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

Reads all Group Managed Service Accounts (gMSAs) and Delegated Managed Service Accounts (dMSAs) from the target domain controller, while deriving their current passwords from KDS root keys.

### Example 2
```powershell
PS C:\> Get-ADSIServiceAccount -Server 'lon-dc1.contoso.com' -EffectiveTime (Get-Date).AddMonths(1)
```

Reads all Group Managed Service Accounts (gMSAs) and Delegated Managed Service Accounts (dMSAs) from the target domain controller, while deriving their future passwords from KDS root keys.

### Example 3
```powershell
PS C:\> $rootKeys = Get-ADDBKdsRootKey -DatabasePath 'C:\ADBackup\ntds.dit'
PS C:\> Get-ADSIServiceAccount -Server 'lon-dc1.contoso.com' -KdsRootKeys $rootKeys
```

Reads KDS root keys from an offline ntds.dit file and then uses them to derive the managed passwords for all gMSAs and dMSAs on the target domain controller, bypassing the default LDAP-based lookup against the configuration naming context.

## PARAMETERS

### -Credential
Specifies a user account to use when connecting to the target domain controller. The default is the current user.

```yaml
Type: PSCredential
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EffectiveTime
Specifies the date and time at which the generated credentials should be valid. When omitted, passwords are derived directly from the `msDS-ManagedPasswordId` and `msDS-ManagedPasswordPreviousId` attributes stored on each account, with no password-cycle math.

```yaml
Type: DateTime
Parameter Sets: (All)
Aliases: EffectiveDate, PasswordLastSet, PwdLastSet, Date, Time, d, t

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KdsRootKeys
Provides an explicit set of KDS root keys to use when deriving managed passwords. When this parameter is specified, the supplied keys override the default LDAP-based lookup against the configuration naming context.

```yaml
Type: KdsRootKey[]
Parameter Sets: (All)
Aliases: KdsRootKey, RootKey, RootKeys

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server
Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Host, DomainController, DC, ComputerName

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

[Get-ADDBServiceAccount](Get-ADDBServiceAccount.md)
[Get-ADSIAccount](Get-ADSIAccount.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
