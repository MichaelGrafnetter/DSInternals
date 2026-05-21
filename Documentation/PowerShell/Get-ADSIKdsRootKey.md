---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADSIKdsRootKey.md
schema: 2.0.0
---

# Get-ADSIKdsRootKey

## SYNOPSIS
Reads KDS Root Keys from a domain controller through the LDAP protocol.

## SYNTAX

### All (Default)
```
Get-ADSIKdsRootKey [-All] [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

### ByGuid
```
Get-ADSIKdsRootKey [-RootKeyId] <Guid> [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION
KDS Root Keys are used to encrypt the following:

- SID-protected private keys in PFX certificate files
- BitLocker-enabled drives with SID protector
- Passwords of Group Managed Service Accounts (gMSA) and Delegated Managed Service Accounts (dMSA)
- DNSSEC key signing keys
- Windows LAPS passwords

KDS Root Keys are stored in the configuration partition of Active Directory. Their secret material can be retrieved by any user that is a member of the Domain Admins or Enterprise Admins group.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADSIKdsRootKey -Server CONTOSO-DC
```

Retrieves all KDS Root Keys from the specified domain controller through LDAP.

### Example 2
```powershell
PS C:\> Get-ADSIKdsRootKey -Server CONTOSO-DC -RootKeyId 1c556b71-ed22-c45f-723c-ddbe199f6824
```

Retrieves a single KDS Root Key by its identifier through LDAP.

## PARAMETERS

### -All
Indicates that all KDS Root Keys should be fetched.

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases:

Required: False
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

### -RootKeyId
Specifies a single KDS Root Key to be fetched.

```yaml
Type: Guid
Parameter Sets: ByGuid
Aliases: Id, KeyId

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Server
Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the parameter is omitted, the cmdlet connects to the current user's domain.

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

### System.Guid

## OUTPUTS

### DSInternals.Common.Data.KdsRootKey

## NOTES

## RELATED LINKS

[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
