---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-SamPasswordPolicy.md
schema: 2.0.0
---

# Get-SamPasswordPolicy

## SYNOPSIS
Queries Active Directory for the default password policy.

## SYNTAX

```
Get-SamPasswordPolicy -Domain <String> [-Credential <PSCredential>] [-Server <String>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves the current password policy for a domain through the MS-SAMR protocol. 

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-SamPasswordPolicy -Domain CONTOSO -Server LON-DC1
<# Sample Output:
MinPasswordLength           : 8
ComplexityEnabled           : True
ReversibleEncryptionEnabled : False
MaxPasswordAge              : 90.00:00:00.0
MinPasswordAge              : 01:00:00
PasswordHistoryCount        : 10
#>
```

Queries the LON-DC1 domain controller for default domain password policy.

### Example 2
```powershell
PS C:\> Get-SamPasswordPolicy -Domain Builtin
<# Sample Output:
MinPasswordLength           : 0
ComplexityEnabled           : False
ReversibleEncryptionEnabled : False
MaxPasswordAge              : 42.22:47:31.7437440
MinPasswordAge              : 00:00:00
PasswordHistoryCount        : 0
#>
```

Queries the local computer for its current password policy.

## PARAMETERS

### -Credential
Specifies the user account credentials to use to perform this task.
The default credentials are the credentials of the currently logged on user.

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
Specifies the NetBIOS domain name. Local accounts are stored a domain called Builtin.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server

Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

The default is the local computer. To specify the local computer, such as in a list of computer names, use "localhost", the local computer name, or a dot (.).

```yaml
Type: String
Parameter Sets: (All)
Aliases: ComputerName, Computer

Required: False
Position: Named
Default value: localhost
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.SAM.SamDomainPasswordInformation

## NOTES

## RELATED LINKS

[Set-SamAccountPasswordHash](Set-SamAccountPasswordHash.md)
