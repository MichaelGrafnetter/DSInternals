---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADReplBitlockerRecoveryInfo.md
schema: 2.0.0
---

# Get-ADReplBitlockerRecoveryInfo

## SYNOPSIS
Reads one or more msFVE-RecoveryInformation objects through the MS-DRSR protocol, including recovery keys and passwords.

## SYNTAX

### All
```
Get-ADReplBitlockerRecoveryInfo [-NamingContext <String>] [-Domain] <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### ByRecoveryGuid
```
Get-ADReplBitlockerRecoveryInfo [-RecoveryGuid <String>] [-Domain] <String>] -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

### -ExportKeysPath
Specifies the path to a file where Bitlocker KeyPackages and RecoveryPasswords are written, for instance, c:\ADDumpBitlocker

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## DESCRIPTION
Reads one or more Bitlocker msFVE-RecoveryInformation objects from a target Active Directory domain controller through the MS-DRSR protocol, including recovery keys and passwords.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADReplBitlockerRecoveryInfo -RecoveryGuid 11c43ee8-b9d3-4e51-b73f-bd9dda66e29c -Server 'lon-dc1.contoso.com'
<# Sample Output:
TODO
#>
```

Replicates a single Active Directory msFVE-RecoveryInformation object from the target domain controller.

### Example 2
```powershell
PS C:\> $accounts = Get-ADReplBitlockerRecoveryInfo -Server 'lon-dc1.contoso.com'
```

Replicates all Active Directory msFVE-RecoveryInformation objects from the target domain controller.

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

### -RecoveryGuid
Specifies the identifier of the msFVE-RecoveryInformation object that will be replicated.

```yaml
Type: String

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### DSInternals.Common.Data.BitlockerRecoveryInfo

## NOTES

## RELATED LINKS

[Get-ADDBBitlockerRecoveryInfo](Get-ADDBBitlockerRecoveryInfo.md)
