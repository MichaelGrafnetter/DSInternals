---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADReplAccount.md
schema: 2.0.0
---

# Get-ADReplAccount

## SYNOPSIS
Reads one or more accounts through the MS-DRSR protocol, including secret attributes.

## SYNTAX

### All
```
Get-ADReplAccount [-All] [-NamingContext <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByName
```
Get-ADReplAccount [-SamAccountName] <String> [[-Domain] <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByUPN
```
Get-ADReplAccount -UserPrincipalName <String> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### BySID
```
Get-ADReplAccount -ObjectSid <SecurityIdentifier> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByDN
```
Get-ADReplAccount [-DistinguishedName] <String> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByGuid
```
Get-ADReplAccount -ObjectGuid <Guid> -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
 [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -All
Indidates that all accounts will be replicated from the target domain controller.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: AllAccounts, ReturnAllAccounts

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
Specifies the identifier of the account that will be replicated.

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
Specifies the NetBIOS domain name of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByName
Aliases: AccountDomain, UserDomain

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -NamingContext
Specifies the naming context root of the replica to replicate.

```yaml
Type: String
Parameter Sets: All
Aliases: NC, DomainNC, DomainNamingContext

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectGuid
Specifies the identifier of the account that will be replicated.

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
Specifies the identifier of the account that will be replicated.

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

### -Protocol
Specifies the protocol sequence that is used for RPC communication.

```yaml
Type: RpcProtocol
Parameter Sets: (All)
Aliases: Proto, RPCProtocol, NCACN
Accepted values: TCP, SMB, HTTP

Required: False
Position: Named
Default value: TCP
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamAccountName
Specifies the identifier of the account that will be replicated.

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, sam, AccountName, User

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Specifies the identifier of the account that will be replicated.

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

## NOTES

## RELATED LINKS

[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-ADSIAccount](Get-ADSIAccount.md)
[Test-PasswordQuality](Test-PasswordQuality.md)
[Save-DPAPIBlob](Save-DPAPIBlob.md)
