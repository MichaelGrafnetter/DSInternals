---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertFrom-GPPrefPassword.md
schema: 2.0.0
---

# ConvertFrom-GPPrefPassword

## SYNOPSIS
Decodes a password from the format used by Group Policy Preferences.

## SYNTAX

```
ConvertFrom-GPPrefPassword [-EncryptedPassword] <String> [<CommonParameters>]
```

## DESCRIPTION
Decrypts a password that was encrypted using Group Policy Preferences (GPP). GPP stored passwords in SYSVOL using AES encryption with a key that Microsoft published in MSDN documentation. This vulnerability (MS14-025) allows anyone with access to SYSVOL to decrypt these passwords. This cmdlet can be used to audit GPP XML files for exposed credentials.

## EXAMPLES

### Example 1
```powershell
PS C:\> ConvertFrom-GPPrefPassword -EncryptedPassword 'v9NWtCCOKEUHkZBxakMd6HLzo4+DzuizXP83EaImqF8'
<#
Output:
Pa$$w0rd
#>
```

Decrypts a password from a Group Policy Preferences XML file (e.g., Groups.xml, ScheduledTasks.xml, or Drives.xml) and returns the cleartext password.

## PARAMETERS

### -EncryptedPassword
Provide an encrypted password from a Group Policy Preferences XML file.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
