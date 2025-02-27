---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIAccount.md
schema: 2.0.0
---

# Get-ADSIAccount

## SYNOPSIS
Gets all Active Directory user accounts from a given domain controller using ADSI. Typically used for Credential Roaming data retrieval through LDAP.

## SYNTAX

```
Get-ADSIAccount [-Properties <AccountPropertySets>] [-Server <String>] [-Credential <PSCredential>]
 [<CommonParameters>]
```

## DESCRIPTION

Gets all Active Directory user accounts from a given domain controller using ADSI/LDAP. Typically used for Credential Roaming data retrieval and NGC key auditing.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-LsaBackupKey -ComputerName 'lon-dc1.contoso.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
PS C:\> Get-ADSIAccount -Server 'lon-dc1.contoso.com' | Save-DPAPIBlob -DirectoryPath '.\Output'
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

### -Properties
Specifies the set of properties that will be retrieved for each account.

```yaml
Type: AccountPropertySets
Parameter Sets: (All)
Aliases: Property, PropertySets, PropertySet
Accepted values: None, DistinguishedName, GenericAccountInfo, GenericUserInfo, GenericComputerInfo, GenericInfo, SecurityDescriptor, NTHash, LMHash, PasswordHashes, NTHashHistory, LMHashHistory, PasswordHashHistory, SupplementalCredentials, Secrets, KeyCredentials, RoamedCredentials, WindowsLAPS, LegacyLAPS, LAPS, ManagedBy, Manager, All

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

### DSInternals.Common.Data.DSAccount

### DSInternals.Common.Data.DSUser

### DSInternals.Common.Data.DSComputer

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADReplAccount](Get-ADReplAccount.md)
[Save-DPAPIBlob](Save-DPAPIBlob.md)
[Get-ADKeyCredential](Get-ADKeyCredential.md)
