---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Get-AzureADUserEx

## SYNOPSIS
Gets a user from Azure AD, including the associated FIDO and NGC keys.

## SYNTAX

### GetMultiple (Default)
```
Get-AzureADUserEx -AccessToken <String> [-All] [-TenantId <Guid>] [<CommonParameters>]
```

### GetById
```
Get-AzureADUserEx -AccessToken <String> -ObjectId <Guid> [-TenantId <Guid>] [<CommonParameters>]
```

### GetByUPN
```
Get-AzureADUserEx -AccessToken <String> -UserPrincipalName <String> [-TenantId <Guid>] [<CommonParameters>]
```

## DESCRIPTION
The Get-AzureADUserEx cmdlet uses an undocumented Azure AD Graph API endpoint to retrieve the normally hidden searchableDeviceKeys attribute of user accounts.
This attribute holds different types of key credentials, including the FIDO2 and NGC keys that are used by Windows Hello for Business.

This cmdlet is not intended to replace the standard Get-AzureADUser cmdlet. Only a handful of attributes are retrieved from Azure Active Directory and 
No administrative role is required to perform this operation.

## EXAMPLES

### Example 1
```powershell
PS C:\> Connect-AzureAD
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Get-AzureADUserEx -All -Token $token |
            Where-Object Enabled -eq $true |
            Select-Object -ExpandProperty KeyCredentials |
            Where-Object Usage -eq FIDO |
            Format-Table -View FIDO
<# Sample Output:

DisplayName           Flags       FidoFlags                                                 Created    HolderDN
-----------           -----       ---------                                                 -------    --------
YubiKey FIDO2         Attestation UserPresent, UserVerified, AttestationData, ExtensionData 2019-07-11
Yubikey 5             Attestation UserPresent, UserVerified, AttestationData, ExtensionData 2019-06-21
Feitian BioPass FIDO2 Attestation UserPresent, UserVerified, AttestationData, ExtensionData 2019-08-26
eWMB Goldengate G320  Attestation UserPresent, UserVerified, AttestationData, ExtensionData 2019-08-29
eWBM Goldengate G310  Attestation UserPresent, UserVerified, AttestationData, ExtensionData 2019-08-29

#>
```

Lists FIDO2 token registered in an Azure AD tenant.

### Example 2
```powershell
PS C:\> Get-AzureADUserEx -All -Token $token |
            Where-Object Enabled -eq $true |
            Select-Object -ExpandProperty KeyCredentials |
            Where-Object Usage -eq NGC |
            Format-Table -View ROCA
<# Sample Output:

Usage IsWeak Source  DeviceId                             Created    HolderDN
----- ------ ------  --------                             -------    --------
NGC   True   AzureAD fd591087-245c-4ff5-a5ea-c14de5e2b32d 2017-07-19 
NGC   False  AzureAD 1966d4da-14da-4581-a7a7-5e8e07e93ad9 2019-08-01 

#>
```

Lists weak public keys registered in Azure Active Directory that were generated on ROCA-vulnerable TPMs.

### Example 3
```powershell
PS C:\> Get-AzureADTenantDetail | Out-Null
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Get-AzureADUserEx -UserPrincipalName 'michael@dsinternals.com' -Token $token
<# Sample Output:

#>
```

Gets information about a single Azure Active Directory user. If necessary, the access token is automatically refreshed by the standard Get-AzureADTenantDetail cmdlet.

## PARAMETERS

### -AccessToken
Specifies the access token retrieved by the Connect-AzureAD cmdlet.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Token

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -All
If true, return all users. If false, return the first 999 objects.

```yaml
Type: SwitchParameter
Parameter Sets: GetMultiple
Aliases: AllUsers

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectId
Specifies the identity of a user in Azure AD.

```yaml
Type: Guid
Parameter Sets: GetById
Aliases: Identity, Id, UserId, ObjectGuid

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantId
Specifies the Azure AD tenant to perform the search in. If empty, the tenant of the authenticated user will be used.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: Tenant

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
Specifies the UPN of a user in Azure AD.

```yaml
Type: String
Parameter Sets: GetByUPN
Aliases: UPN, UserName

Required: True
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

### DSInternals.Common.AzureAD.AzureADUser

## NOTES

## RELATED LINKS

[Get-ADKeyCredential](Get-ADKeyCredential.md)
