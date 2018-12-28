---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-LsaPolicyInformation.md
schema: 2.0.0
---

# Set-LsaPolicyInformation

## SYNOPSIS
Configures AD-related Local Security Authority Policies of the local computer or a remote one.

## SYNTAX

```
Set-LsaPolicyInformation -DomainName <String> -DnsDomainName <String> -DnsForestName <String>
 -DomainGuid <Guid> -DomainSid <SecurityIdentifier> [-ComputerName <String>] [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> Set-LsaPolicyInformation -DomainName 'ADATUM' -DnsDomainName 'Adatum.com' -DnsForestName 'Adatum.com' -DomainGuid 279b615e-ae79-4c86-a61a-50f687b9f7b8 -DomainSid S-1-5-21-1817670852-3242289776-1304069626
```

Configures AD-related LSA Policy Information of the local computer.

## PARAMETERS

### -ComputerName
{{Fill ComputerName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: Computer, Machine, MachineName, System, SystemName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DnsDomainName
{{Fill DnsDomainName Description}}

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

### -DnsForestName
{{Fill DnsForestName Description}}

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

### -DomainGuid
{{Fill DomainGuid Description}}

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
{{Fill DomainName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: NetBIOSDomainName, Workgroup

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainSid
{{Fill DomainSid Description}}

```yaml
Type: SecurityIdentifier
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### None
## NOTES

## RELATED LINKS
