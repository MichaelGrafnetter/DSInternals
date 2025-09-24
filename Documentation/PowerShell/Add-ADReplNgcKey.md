---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Add-ADReplNgcKey.md
schema: 2.0.0
---

# Add-ADReplNgcKey

## SYNOPSIS
Composes and updates the msDS-KeyCredentialLink value on an object through the MS-DRSR protocol.

## SYNTAX

### ByName
```
Add-ADReplNgcKey -PublicKey <Byte[]> [-SamAccountName] <String> [[-Domain] <String>] -Server <String>
 [-Credential <PSCredential>] [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByUPN
```
Add-ADReplNgcKey -PublicKey <Byte[]> -UserPrincipalName <String> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### BySID
```
Add-ADReplNgcKey -PublicKey <Byte[]> -ObjectSid <SecurityIdentifier> -Server <String>
 [-Credential <PSCredential>] [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByDN
```
Add-ADReplNgcKey -PublicKey <Byte[]> [-DistinguishedName] <String> -Server <String>
 [-Credential <PSCredential>] [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByGuid
```
Add-ADReplNgcKey -PublicKey <Byte[]> -ObjectGuid <Guid> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet wraps the IDL_DRSWriteNgcKey RPC call.

## EXAMPLES

### Example 1
```powershell
PS C:\> Add-ADReplNgcKey -UserPrincipalName 'john@contoso.com' -Server LON-DC1 -PublicKey 525341310008000003000000000100000000000000000000010001C1A78914457758B0B13C70C710C7F8548F3F9ED56AD4640B6E6A112655C98ECAC1CBD68A298F5686C08439428A97FE6FDF58D78EA481905182BAD684C2D9C5CDE1CDE34AA19742E8BBF58B953EAC4C562FCF598CC176B02DBE9FFFEF5937A65815C236F92892F7E511A1FEDD5483CB33F1EA715D68106180DED2432A293367114A6E325E62F93F73D7ECE4B6A2BCDB829D95C8645C3073B94BA7CB7515CD29042F0967201C6E24A77821E92A6C756DF79841ACBAAE11D90CA03B9FCD24EF9E304B5D35248A7BD70557399960277058AE3E99C7C7E2284858B7BF8B08CDD286964186A50A7FCBCC6A24F00FEE5B9698BBD3B1AEAD0CE81FEA461C0ABD716843A5
```

Registers the specified NGC public key for user *john@contoso.com*.

## PARAMETERS

### -Credential
Specifies a user account that has permission to perform this action. The default is the current user.

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
Specifies the identifier of the target Active Directory object.

```yaml
Type: String
Parameter Sets: ByDN
Aliases: dn

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Domain
Specifies the NetBIOS domain name of the target Active Directory account.

```yaml
Type: String
Parameter Sets: ByName
Aliases: AccountDomain, UserDomain

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectGuid
Specifies the identifier of the target Active Directory object.

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
Specifies the identifier of the target Active Directory account.

```yaml
Type: SecurityIdentifier
Parameter Sets: BySID
Aliases: Sid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -PublicKey
Specifies the NGC key value.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamAccountName
Specifies the identifier of the target Active Directory account.

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, sam, AccountName, User

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
Aliases: Host, DomainController, DC

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserPrincipalName
Specifies the identifier of the target Active Directory account.

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

### None

## NOTES

## RELATED LINKS

[Get-ADReplAccount](Get-ADReplAccount.md)
[Get-ADKeyCredential](Get-ADKeyCredential.md)
