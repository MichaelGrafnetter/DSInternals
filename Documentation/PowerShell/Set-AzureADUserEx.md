---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
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
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

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
{{ Fill KeyCredential Description }}

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

### System.Object
## NOTES

## RELATED LINKS
