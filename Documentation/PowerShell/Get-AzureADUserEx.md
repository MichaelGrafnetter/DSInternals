---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-AzureADUserEx.md
schema: 2.0.0
---

# Get-AzureADUserEx

## SYNOPSIS
Gets a user from Azure AD, including the associated FIDO and NGC keys.

## SYNTAX

### Multiple (Default)
```
Get-AzureADUserEx [-All] -AccessToken <String> [-TenantId <Guid>] [<CommonParameters>]
```

### ById
```
Get-AzureADUserEx -AccessToken <String> -ObjectId <Guid> [-TenantId <Guid>] [<CommonParameters>]
```

### ByUPN
```
Get-AzureADUserEx -AccessToken <String> -UserPrincipalName <String> [-TenantId <Guid>] [<CommonParameters>]
```

## DESCRIPTION
The Get-AzureADUserEx cmdlet uses an undocumented Azure AD Graph API endpoint to retrieve the normally hidden searchableDeviceKeys attribute of user accounts.
This attribute holds different types of key credentials, including the FIDO2 and NGC keys that are used by Windows Hello for Business.

This cmdlet is not intended to replace the Get-AzureADUser cmdlet from Microsoft's AzureAD module. Only a handful of attributes are retrieved from Azure Active Directory and authentication fully relies on the Connect-AzureAD cmdlet.

No administrative role is required to perform this operation. The cmdlet was tested on a tenant with 150,000 user accounts and ran under 5 minutes.

## EXAMPLES

### Example 1
```powershell
PS C:\> Install-Module -Name AzureAD,DSInternals -Force
PS C:\> Connect-AzureAD
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Get-AzureADUserEx -All -Token $token | Where-Object KeyCredentials -ne $null
<# Sample Output:

ObjectId: af4cf208-16e0-429d-b574-2a09c5f30dea
UserPrincipalName: john@contoso.com
Enabled: True
DisplayName: John Doe
Key Credentials:
  Usage=FIDO, Source=AzureAD, Device=00000000-0000-0000-0000-000000000000, Created=12/12/2019 9:42:21 AM
  Usage=NGC, Source=AzureAD, Device=cbad3c94-b480-4fa6-9187-ff1ed42c4479, Created=11/17/2015 8:17:13 AM

ObjectId: 5dd9c7f0-9441-4c5a-b2df-ca7b889d8c4c
UserPrincipalName: peter@contoso.com
Enabled: True
DisplayName: Peter Smith
Key Credentials:
  Usage=NGC, Source=AzureAD, Device=21c915a8-0326-47c4-8985-2aceda00eaee, Created=12/26/2019 1:22:17 PM
  Usage=NGC, Source=AzureAD, Device=ec45d71b-b5dd-45dc-beaf-e248cbcb2bd3, Created=12/24/2019 9:44:56 AM

#>
```

Displays info about Azure AD users with key credentials. Authentication is handled by the AzureAD module.

### Example 2
```powershell
PS C:\> Install-Module -Name AzureAD,DSInternals -Force
PS C:\> Connect-AzureAD
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Get-AzureADUserEx -All -Token $token |
            Where-Object Enabled -eq $true |
            Select-Object -ExpandProperty KeyCredentials |
            Where-Object Usage -eq FIDO |
            Format-Table -View FIDO
<# Sample Output:

DisplayName               AAGUID                               Alg   Counter Created    Owner
-----------               ------                               ---   ------- -------    -----
SoloKeys Tap              8876631b-d4a0-427f-5773-0ec71c9e0279 ES256     274 2019-08-29 james@contoso.com
SoloKeys Solo             8876631b-d4a0-427f-5773-0ec71c9e0279 ES256     281 2019-08-29 thomas@contoso.com
eWBM Goldengate G320      87dbc5a1-4c94-4dc8-8a47-97d800fd1f3c ES256      83 2019-08-29 jane@contoso.com
eWBM Goldengate G310      95442b2e-f15e-4def-b270-efb106facb4e ES256       4 2019-08-29 mary@contoso.com
Feitian BioPass FIDO2     77010bd7-212a-4fc9-b236-d2ca5e9d4084 ES256     261 2019-08-26 george@contoso.com
Yubico Security Key FIDO2 f8a011f3-8c0a-4d15-8006-17111f9edc7d ES256     257 2019-08-26 matt@contoso.com
Feitian AllinPass FIDO2   12ded745-4bed-47d4-abaa-e713f51d6393 ES256     231 2019-08-26 jenny@contoso.com
YubiKey 5                 fa2b99dc-9e39-4257-8f92-4a30d23c4118 ES256     229 2019-08-26 jill@contoso.com
YubiKey 5                 cb69481e-8ff7-4039-93ec-0a2729a154a8 ES256      25 2019-12-12 john@contoso.com
Feitian All-In-Pass       12ded745-4bed-47d4-abaa-e713f51d6393 ES256    1398 2020-03-31 peter@contoso.com
eWBM Goldengate G320      87dbc5a1-4c94-4dc8-8a47-97d800fd1f3c ES256      37 2019-08-29 joe@contoso.com
eWBM Goldengate G310      95442b2e-f15e-4def-b270-efb106facb4e ES256      48 2019-08-29 joe@contoso.com

#>
```

Lists all FIDO2 tokens registered in an Azure AD tenant, but only on accounts that are enabled.

### Example 3
```powershell
PS C:\> Get-AzureADUserEx -All -Token $token |
            Where-Object Enabled -eq $true |
            Select-Object -ExpandProperty KeyCredentials |
            Where-Object Usage -eq NGC |
            Format-Table -View ROCA
<# Sample Output:

Usage IsWeak Source  DeviceId                             Created    Owner
----- ------ ------  --------                             -------    -----
NGC   True   AzureAD fd591087-245c-4ff5-a5ea-c14de5e2b32d 2017-07-19 joe@contoso.com
NGC   False  AzureAD 1966d4da-14da-4581-a7a7-5e8e07e93ad9 2019-08-01 peter@contoso.com

#>
```

Lists weak public keys registered in Azure Active Directory that were generated on ROCA-vulnerable TPMs.

### Example 4
```powershell
PS C:\> Get-AzureADTenantDetail | Out-Null
PS C:\> $token = [Microsoft.Open.Azure.AD.CommonLibrary.AzureSession]::AccessTokens['AccessToken'].AccessToken
PS C:\> Get-AzureADUserEx -UserPrincipalName 'john@contoso.com' -Token $token

<# Sample Output:

ObjectId: af4cf208-16e0-429d-b574-2a09c5f30dea
UserPrincipalName: john@contoso.com
Enabled: True
DisplayName: John Doe
Key Credentials:
  Usage=FIDO, Source=AzureAD, Device=00000000-0000-0000-0000-000000000000, Created=12/12/2019 9:42:21 AM
  Usage=NGC, Source=AzureAD, Device=cbad3c94-b480-4fa6-9187-ff1ed42c4479, Created=11/17/2015 8:17:13 AM

#>
```

Gets information about a single Azure Active Directory user. If necessary, the access token is automatically refreshed by the standard Get-AzureADTenantDetail cmdlet.

### Example 5
```powershell
PS C:\> Get-AzureADUserEx -UserPrincipalName 'john@contoso.com' -AccessToken $token |
            ForEach-Object { $PSItem.KeyCredentials.FidoKeyMaterial }

<# Sample Output:

Version: 1
DisplayName: SoloKeys Tap
AttestationCertificates
  E=hello@solokeys.com, CN=solokeys.com, OU=Authenticator Attestation, O=Solo Keys, S=Maryland, C=US
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 274
  AttestedCredentialData
    AAGUID: 8876631b-d4a0-427f-5773-0ec71c9e0279
    CredentialID: 9d0c595c03cd6c9dd22b0b8f852585302c2d5a13f77669251406c390de6ba42a63f3ea6632a39cd8bc505184352541367725a5283689d825f4355fe2016af2f0008112010000
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: SoloKeys Solo
AttestationCertificates
  E=hello@solokeys.com, CN=solokeys.com, OU=Authenticator Attestation, O=Solo Keys, S=Maryland, C=US
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 281
  AttestedCredentialData
    AAGUID: 8876631b-d4a0-427f-5773-0ec71c9e0279
    CredentialID: ac5373c1eaa6722c351db6554715fd534906f5c98f4548b8390fe11b97325a943f0905d0a9de19765385f9bce512673128a95e3fea15f53ee46dbe307d0f94c84d8119010000
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: eWBM Goldengate G320
AttestationCertificates
  E=info@ewbm.com, CN=eWBM FIDO2 Certificate, OU=Authenticator Attestation, O="eWBM Co., Ltd.", L=Gangnam-Gu, S=Seoul-Si, C=KR
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 83
  AttestedCredentialData
    AAGUID: 87dbc5a1-4c94-4dc8-8a47-97d800fd1f3c
    CredentialID: fb64eb483921507239317b6f5f1d7a0b9499afd4dd0698eaa55ad8871fe1c25a
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: eWBM Goldengate G310
AttestationCertificates
  E=info@ewbm.com, CN=eWBM FIDO2 Certificate, OU=Authenticator Attestation, O="eWBM Co., Ltd.", L=Gangnam-Gu, S=Seoul-Si, C=KR
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 4
  AttestedCredentialData
    AAGUID: 95442b2e-f15e-4def-b270-efb106facb4e
    CredentialID: 4dd34d8760bc0e92fcb53b64cc1c354ac7112931bd6f53c0a6aabba7c813c36d
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: Feitian BioPass FIDO2
AttestationCertificates
  CN=FT BioPass FIDO2 USB, OU=Authenticator Attestation, O=Feitian Technologies, C=US
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 261
  AttestedCredentialData
    AAGUID: 77010bd7-212a-4fc9-b236-d2ca5e9d4084
    CredentialID: db2baabf8450f6af3b931b35acc7d5f77ebe4ed98cf0b55c0513cff31e18520d
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: Yubico Security Key FIDO2
AttestationCertificates
  CN=Yubico U2F EE Serial 8513128192, OU=Authenticator Attestation, O=Yubico AB, C=SE
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 257
  AttestedCredentialData
    AAGUID: f8a011f3-8c0a-4d15-8006-17111f9edc7d
    CredentialID: 09956acca523d532e04c647a4a158664
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: Feitian AllinPass FIDO2
AttestationCertificates
  CN=FT BioPass FIDO2 0470, OU=Authenticator Attestation, O=Feitian Technologies, C=US
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 231
  AttestedCredentialData
    AAGUID: 12ded745-4bed-47d4-abaa-e713f51d6393
    CredentialID: 59dc5439faef677d7e81688a27604a205dab922978372062c5c10206639c3c00
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

Version: 1
DisplayName: YubiKey 5
AttestationCertificates
  CN=Yubico U2F EE Serial 14818162, OU=Authenticator Attestation, O=Yubico AB, C=SE
AuthenticatorData
  RelyingPartyIdHash: 356c9ed4a09321b9695f1eaf918203f1b55f689da61fbc96184c157dda680c81
  Flags: UserPresent, UserVerified, AttestationData, ExtensionData
  SignatureCount: 229
  AttestedCredentialData
    AAGUID: fa2b99dc-9e39-4257-8f92-4a30d23c4118
    CredentialID: bc879d1e8da27d5f29a66d9a457ac1d8
    PublicKeyAlgorithm: ES256
  Extensions: {"hmac-secret": true}

#>
```

Displays details about FIDO2 keys registered in Azure Active Directory by a specific user.

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
Parameter Sets: Multiple
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

### DSInternals.Common.AzureAD.AzureADUser

## NOTES

## RELATED LINKS

[Set-AzureADUserEx](Set-AzureADUserEx.md)
[Get-ADKeyCredential](Get-ADKeyCredential.md)
