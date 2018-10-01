---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Test-PasswordQuality

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
Test-PasswordQuality [-Account] <DSAccount> [-SkipDuplicatePasswordTest] [-IncludeDisabledAccounts]
 [-WeakPasswords <String[]>] [-WeakPasswordsFile <String>] [-WeakPasswordHashesFile <String>]
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

### -Account
{{Fill Account Description}}

```yaml
Type: DSAccount
Parameter Sets: (All)
Aliases: ADAccount, DSAccount

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IncludeDisabledAccounts
{{Fill IncludeDisabledAccounts Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipDuplicatePasswordTest
{{Fill SkipDuplicatePasswordTest Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordHashesFile
{{Fill WeakPasswordHashesFile Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswords
{{Fill WeakPasswords Description}}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WeakPasswordsFile
{{Fill WeakPasswordsFile Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### DSInternals.Common.Data.DSAccount

## OUTPUTS

### DSInternals.PowerShell.PasswordQualityTestResult

## NOTES

## RELATED LINKS
