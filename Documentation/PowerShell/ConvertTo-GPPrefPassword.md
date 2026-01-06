---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertTo-GPPrefPassword.md
schema: 2.0.0
---

# ConvertTo-GPPrefPassword

## SYNOPSIS
Converts a password to the format used by Group Policy Preferences.

## SYNTAX

```
ConvertTo-GPPrefPassword [-Password] <SecureString> [<CommonParameters>]
```

## DESCRIPTION
Encrypts a password using the AES key that was used by Group Policy Preferences (GPP) to store passwords in SYSVOL. This encryption method is considered insecure because Microsoft published the AES key in MSDN documentation (MS14-025). This cmdlet is provided for educational and testing purposes.

## EXAMPLES

### Example 1
```powershell
PS C:\> $password = Read-Host -AsSecureString -Prompt 'Enter password'
PS C:\> ConvertTo-GPPrefPassword -Password $password
<#
Sample Output:
v9NWtCCOKEUHkZBxakMd6HLzo4+DzuizXP83EaImqF8
#>
```

Encrypts a password using the well-known Group Policy Preferences AES key and returns the Base64-encoded ciphertext.

## PARAMETERS

### -Password
Provide a password in the form of a SecureString.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases: p

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Security.SecureString

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
