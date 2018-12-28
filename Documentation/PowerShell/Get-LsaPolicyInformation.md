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
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-LSAPolicyInformation
```

Retrieves LSA Policy from the local computer.

### Example 2
```powershell
PS C:\> Get-LSAPolicyInformation -ComputerName LON-DC1
```

Retrieves LSA Policy from a remote computer called LON-DC1.

## PARAMETERS

### -ComputerName
{{Fill ComputerName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: Computer, Machine, MachineName, System, SystemName

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### DSInternals.PowerShell.LsaPolicyInformation
## NOTES

## RELATED LINKS
