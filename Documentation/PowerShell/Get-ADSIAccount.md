---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIAccount.md
schema: 2.0.0
---

# Get-ADSIAccount

## SYNOPSIS
Gets one or more Active Directory accounts from a given domain controller using ADSI. Typically used for Credential Roaming data retrieval through LDAP.

## SYNTAX

### All (Default)
```
Get-ADSIAccount [-All] [-Properties <AccountPropertySets>] [-KdsRootKey <KdsRootKey[]>] [-Server <String>]
 [-Credential <PSCredential>] [<CommonParameters>]
```

### ByName
```
Get-ADSIAccount [-SamAccountName] <String> [-Properties <AccountPropertySets>] [-KdsRootKey <KdsRootKey[]>]
 [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

### ByUPN
```
Get-ADSIAccount -UserPrincipalName <String> [-Properties <AccountPropertySets>] [-KdsRootKey <KdsRootKey[]>]
 [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

### BySID
```
Get-ADSIAccount -ObjectSid <SecurityIdentifier> [-Properties <AccountPropertySets>]
 [-KdsRootKey <KdsRootKey[]>] [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

### ByDN
```
Get-ADSIAccount [-DistinguishedName] <String> [-Properties <AccountPropertySets>] [-KdsRootKey <KdsRootKey[]>]
 [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

### ByGuid
```
Get-ADSIAccount -ObjectGuid <Guid> [-Properties <AccountPropertySets>] [-KdsRootKey <KdsRootKey[]>]
 [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION

Gets one or more Active Directory accounts from a given domain controller using ADSI/LDAP. Typically used for Credential Roaming data retrieval and NGC key auditing. A single account can be retrieved by specifying its `SamAccountName`, `UserPrincipalName`, `ObjectSid`, `DistinguishedName`, or `ObjectGuid`. When no identifier is supplied, all accounts in the target domain are returned.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-LsaBackupKey -ComputerName 'lon-dc1.contoso.com' | Save-DpapiBlob -DirectoryPath '.\Output'
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' | Save-DpapiBlob -DirectoryPath '.\Output'
```

Retrieves DPAPI backup keys from the target domain controller through the MS-LSAD protocol. Also retrieves roamed credentials (certificates, private keys, and DPAPI master keys) from this domain controller through LDAP and saves them to the Output directory. Also creates a file called kiwiscript.txt that contains mimikatz commands needed to decrypt the private keys.

### Example 2
```powershell
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' |
            Select-Object -ExpandProperty KeyCredentials |
            Where-Object Usage -eq NGC |
            Format-Table -View ROCA
<# Sample Output:

Usage IsWeak Source  DeviceId                             Created    Owner
----- ------ ------  --------                             -------    -----
NGC   True   AzureAD fd591087-245c-4ff5-a5ea-c14de5e2b32d 2017-07-19 CN=John Doe,CN=Users,DC=contoso,DC=com
NGC   False  AD      1966d4da-14da-4581-a7a7-5e8e07e93ad9 2019-08-01 CN=Jane Doe,CN=Users,DC=contoso,DC=com
#>
```

Lists weak public keys registered in Active Directory that were generated on ROCA-vulnerable TPMs.

### Example 3
```powershell
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' -SamAccountName 'joe'
```

Retrieves a single Active Directory user account by its `sAMAccountName` through LDAP.

### Example 4
```powershell
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' -UserPrincipalName 'joe@contoso.com'
```

Retrieves a single Active Directory user account by its user principal name through LDAP.

## PARAMETERS

### -All
Indicates that all accounts will be retrieved from the target domain controller. This is the default behavior when no identifier is specified.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: AllAccounts, ReturnAllAccounts

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -DistinguishedName
Specifies the distinguished name of the account that will be retrieved.

```yaml
Type: String
Parameter Sets: ByDN
Aliases: DN

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -KdsRootKey
Provides an explicit set of KDS root keys to use when decrypting Windows LAPS passwords. When this parameter is specified, the supplied keys override the default LDAP-based lookup against the configuration naming context.

```yaml
Type: KdsRootKey[]
Parameter Sets: (All)
Aliases: KdsRootKeys, RootKey, RootKeys

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectGuid
Specifies the object GUID of the account that will be retrieved.

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
Specifies the security identifier (SID) of the account that will be retrieved.

```yaml
Type: SecurityIdentifier
Parameter Sets: BySID
Aliases: SID

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Properties
Specifies the set of properties that will be retrieved for each account.

```yaml
Type: AccountPropertySets
Parameter Sets: (All)
Aliases: Property, PropertySets, PropertySet
Accepted values: None, DistinguishedName, GenericAccountInfo, GenericUserInfo, GenericComputerInfo, GenericInfo, SecurityDescriptor, NTHash, LMHash, PasswordHashes, NTHashHistory, LMHashHistory, PasswordHashHistory, SupplementalCredentials, Secrets, KeyCredentials, RoamedCredentials, WindowsLAPS, LegacyLAPS, LAPS, ManagedBy, Manager, All

Required: False
Position: Named
Default value: All
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamAccountName
Specifies the sAMAccountName of the account that will be retrieved.

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, SAM, AccountName, User

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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

### -UserPrincipalName
Specifies the user principal name (UPN) of the account that will be retrieved.

```yaml
Type: String
Parameter Sets: ByUPN
Aliases: UPN

Required: True
Position: Named
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

### DSInternals.Common.Data.DSUser

### DSInternals.Common.Data.DSComputer

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADReplAccount](Get-ADReplAccount.md)
[Save-DpapiBlob](Save-DpapiBlob.md)
[Get-ADKeyCredential](Get-ADKeyCredential.md)
