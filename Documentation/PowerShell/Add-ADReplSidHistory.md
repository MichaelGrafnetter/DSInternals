---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Add-ADReplSidHistory.md
schema: 2.0.0
---

# Add-ADReplSidHistory

## SYNOPSIS
Adds SID history from a source principal to a destination principal through the MS-DRSR protocol.

## SYNTAX

### CrossForest
```
Add-ADReplSidHistory -SourceDomain <String> -SourcePrincipal <String> [-SourceDomainController <String>]
 [-SourceCredential <PSCredential>] -DestinationDomain <String> -DestinationPrincipal <String> -Server <String>
 [-Credential <PSCredential>] [<CommonParameters>]
```

### IntraDomain
```
Add-ADReplSidHistory -SourcePrincipal <String> -DestinationPrincipal <String> [-DeleteSourceObject]
 -Server <String> [-Credential <PSCredential>] [<CommonParameters>]
```

### CheckSecureChannel
```
Add-ADReplSidHistory [-CheckSecureChannel] -Server <String> [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet wraps the IDL_DRSAddSidHistory RPC call. It passes source and destination principal information to the domain controller and can optionally validate the secure channel or delete the source object.

## EXAMPLES

### Example 1
```powershell
PS C:\> Add-ADReplSidHistory -SourceDomain 'CONTOSO' -SourcePrincipal 'LegacyUser' -SourceDomainController 'CONTOSO-PDC'
 -SourceCredential (Get-Credential 'CONTOSO\Administrator') -DestinationDomain 'FABRIKAM'
 -DestinationPrincipal 'MigratedUser' -Server 'FABRIKAM-DC1'
```

Adds the SID history of *LegacyUser* from the CONTOSO domain to *MigratedUser* in the FABRIKAM domain.

### Example 2
```powershell
PS C:\> Add-ADReplSidHistory -SourcePrincipal 'CN=LegacyUser,CN=Users,DC=contoso,DC=com'
 -DestinationPrincipal 'CN=MigratedUser,CN=Users,DC=contoso,DC=com' -DeleteSourceObject -Server 'CONTOSO-DC1'
```

Adds the SID history of the source object to the destination object in the same domain and deletes the source object.

### Example 3
```powershell
PS C:\> Add-ADReplSidHistory -CheckSecureChannel -Server 'FABRIKAM-DC1'
```

Verifies that the RPC connection to the destination DC is secure.

## PARAMETERS

### -CheckSecureChannel
Verifies whether the channel is secure and returns the result of the verification.

```yaml
Type: SwitchParameter
Parameter Sets: CheckSecureChannel
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credential
Specifies a user account that has permission to connect to the destination domain controller. The default is the current user.

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

### -DeleteSourceObject
Appends the source object's SID history to the destination and deletes the source object from the source domain.

```yaml
Type: SwitchParameter
Parameter Sets: IntraDomain
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationDomain
Specifies the destination domain in which the destination principal resides. The domain name can be an FQDN or a NetBIOS name.

```yaml
Type: String
Parameter Sets: CrossForest
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationPrincipal
Specifies the destination security principal that receives the source SID history.

```yaml
Type: String
Parameter Sets: CrossForest, IntraDomain
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Server
Specifies the target domain controller for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address.

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

### -SourceCredential
Specifies the credentials to be used in the source domain.

```yaml
Type: PSCredential
Parameter Sets: CrossForest
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceDomain
Specifies the source domain to query for the SID of the source principal. The domain name can be an FQDN or a NetBIOS name.

```yaml
Type: String
Parameter Sets: CrossForest
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceDomainController
Specifies the primary domain controller (PDC) or PDC role owner in the source domain.

```yaml
Type: String
Parameter Sets: CrossForest
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourcePrincipal
Specifies the source security principal whose SID history is to be added. If -DeleteSourceObject is specified, this value should be a DN; otherwise, it should be a domain-relative SAM name.

```yaml
Type: String
Parameter Sets: CrossForest, IntraDomain
Aliases:

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

## OUTPUTS

### None

## NOTES

## RELATED LINKS
