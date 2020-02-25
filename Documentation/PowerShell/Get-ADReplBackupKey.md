---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADReplBackupKey.md
schema: 2.0.0
---

# Get-ADReplBackupKey

## SYNOPSIS
Reads the DPAPI backup keys through the MS-DRSR protocol.

## SYNTAX

```
Get-ADReplBackupKey [-Domain <String>] -Server <String> [-Credential <PSCredential>] [-Protocol <RpcProtocol>]
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

### -Domain
TODO

```yaml
Type: String
Parameter Sets: (All)
Aliases: FQDN, DomainName, DNSDomainName

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
Default value: None
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

### None

## OUTPUTS

### DSInternals.Common.Data.DPAPIBackupKey

## NOTES

## RELATED LINKS

[Save-DPAPIBlob](Save-DPAPIBlob.md)
[Get-ADDBBackupKey](Get-ADDBBackupKey.md)
[Get-LsaBackupKey](Get-LsaBackupKey.md)
