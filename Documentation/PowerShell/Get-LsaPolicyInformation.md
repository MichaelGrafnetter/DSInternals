---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-LsaPolicyInformation.md
schema: 2.0.0
---

# Get-LsaPolicyInformation

## SYNOPSIS
Retrieves AD-related information from the Local Security Authority Policy of the local computer or a remote one.

## SYNTAX

```
Get-LsaPolicyInformation [[-ComputerName] <String>] [<CommonParameters>]
```

## DESCRIPTION

The Local Security Authority (LSA) is a protected subsystem of Windows that maintains information about all aspects of local security on a system, collectively known as the local security policy of the system.

The local security policy of a system is a set of information about the security of a local computer.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-LSAPolicyInformation
<# Sample Output:
Domain/Workgroup Name : WORKGROUP
Account Domain Name   : MYPC
Account Domain SID    : S-1-5-21-2814909047-1086830290-2660982408
Local Domain Name     : MYPC
Local Domain SID      : S-1-5-21-2814909047-1086830290-2660982408
#>
```

Retrieves LSA Policy from the local computer.

### Example 2
```powershell
PS C:\> Get-LSAPolicyInformation -ComputerName LON-DC1
<# Sample Output:
Domain/Workgroup Name : ADATUM
Forest DNS Name       : Adatum.com
Domain DNS Name       : Adatum.com
Domain GUID           : 281582b4-9d3e-449d-a238-676bd5844f56
Domain SID            : S-1-5-21-3180365339-800773672-3767752645
Account Domain Name   : ADATUM
Account Domain SID    : S-1-5-21-3180365339-800773672-3767752645
Local Domain Name     : LON-DC1
Local Domain SID      : S-1-5-21-2929860833-2984454239-2848460202
#>
```

Retrieves LSA Policy from a remote computer called LON-DC1.

## PARAMETERS

### -ComputerName

Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

The default is the local computer. To specify the local computer, such as in a list of computer names, use "localhost", the local computer name, or a dot (.).

```yaml
Type: String
Parameter Sets: (All)
Aliases: Server, ServerName, Computer, Machine, MachineName, System, SystemName

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### DSInternals.PowerShell.LsaPolicyInformation

## NOTES

## RELATED LINKS

[Set-LsaPolicyInformation](Set-LsaPolicyInformation.md)
[Get-LsaBackupKey](Get-LsaBackupKey.md)
[LSA Policy](https://docs.microsoft.com/en-us/windows/desktop/secmgmt/lsa-policy)
