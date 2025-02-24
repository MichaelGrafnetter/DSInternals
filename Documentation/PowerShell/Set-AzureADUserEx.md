---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-AzureADUserEx.md
schema: 2.0.0
---

# Set-AzureADUserEx

## SYNOPSIS
Registers new or revokes existing FIDO and NGC keys in Azure Active Directory.

## SYNTAX

### ByUPN (Default)
```
Set-AzureADUserEx -KeyCredential <KeyCredential[]> -AccessToken <String> -UserPrincipalName <String>
 [-TenantId <Guid>] [<CommonParameters>]
```

### ById
```
Set-AzureADUserEx -KeyCredential <KeyCredential[]> -AccessToken <String> -ObjectId <Guid> [-TenantId <Guid>]
 [<CommonParameters>]
```

## DESCRIPTION
The Set-AzureADUserEx cmdlet uses an undocumented Azure AD Graph API endpoint to modify the normally hidden searchableDeviceKeys attribute of user accounts.
This attribute holds different types of key credentials, including the FIDO2 and NGC keys that are used by Windows Hello for Business.

This cmdlet also enables Global Admins to selectively revoke security keys registered by other users. This is a unique feature, as Microsoft only supports self-service FIDO2 security key registration and revocation (at least at the time of publishing this cmdlet).

This cmdlet is not intended to replace the Set-AzureADUser cmdlet from Microsoft's AzureAD module. Authentication fully relies on the official Connect-AzureAD cmdlet.

## EXAMPLES

### Example 1
```powershell
PS C:\> Install-Module -Name AzureAD,DSInternals -Force
PS C:\> Connect-AzureAD
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Set-AzureADUserEx -UserPrincipalName 'john@contoso.com' -KeyCredential @() -Token $token
```

Revokes all FIDO2 security keys and NGC keys (Windows Hello for Business) that were previously registered by the specified user. Typical use case includes stolen devices and other security incidents.

### Example 2
```powershell
PS C:\> $user = Get-AzureADUserEx -UserPrincipalName 'john@contoso.com' -AccessToken $token 
PS C:\> $newCreds = $user.KeyCredentials | where { $PSItem.FidoKeyMaterial.DisplayName -notlike '*YubiKey*' }
PS C:\> Set-AzureADUserEx -UserPrincipalName 'john@contoso.com' -KeyCredential $newCreds -Token $token
```

Selectively revokes a specific FIDO2 security key based on its display name. Typical use case is a stolen/lost security key.

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

### -KeyCredential
Specifies a list of key credentials (typically FIDO2 and NGC keys) that can be used by the target user for authentication.

```yaml
Type: KeyCredential[]
Parameter Sets: (All)
Aliases: SearchableDeviceKey, KeyCredentialLink

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectId
Specifies the identity of a user in Azure AD.

```yaml
Type: Guid
Parameter Sets: ById
Aliases: Identity, Id, UserId, ObjectGuid

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantId
Specifies the Azure AD tenant to perform the search in. If not specified, the tenant of the authenticated user will be used.

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
Parameter Sets: ByUPN
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

### None

## NOTES

## RELATED LINKS

[Get-AzureADUserEx](Get-AzureADUserEx.md)
[Get-ADKeyCredential](Get-ADKeyCredential.md)
