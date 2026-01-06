---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-UnicodePassword.md
schema: 2.0.0
---

# ConvertTo-UnicodePassword

## SYNOPSIS
Converts a password to the format used in unattend.xml or *.ldif files.

## SYNTAX

```
ConvertTo-UnicodePassword [-Password] <SecureString> [-IsUnattendPassword] [<CommonParameters>]
```

## DESCRIPTION
Converts a password to the Base64-encoded Unicode format that is used in Windows unattend.xml files or LDIF (LDAP Data Interchange Format) files. This format is required when specifying passwords in automated Windows deployment scenarios or when importing user accounts with passwords through LDIF.

## EXAMPLES

### Example 1
```powershell
PS C:\> $password = Read-Host -AsSecureString -Prompt 'Enter password'
PS C:\> ConvertTo-UnicodePassword -Password $password
<# Sample Output:
UABhAHMAcwB3ADAAcgBkAA==
#>
```

Converts a password to the Base64-encoded Unicode format suitable for use in LDIF files.

### Example 2
```powershell
PS C:\> $password = Read-Host -AsSecureString -Prompt 'Enter password'
PS C:\> ConvertTo-UnicodePassword -Password $password -IsUnattendPassword
<#
Sample Output:
UABhAHMAcwB3ADAAcgBkAEEAZABtAGkAbgBpAHMAdAByAGEAdABvAHIAUABhAHMAcwB3AG8AcgBkAA==
#>
```

Converts a password to the format used in Windows unattend.xml files, which includes the "AdministratorPassword" suffix.

## PARAMETERS

### -IsUnattendPassword
Indicates that the result should be in the format for unattend.xml instead of *.ldif.

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

### -Password
Specifies a password that will be converted to the specified format.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
