---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Set-SamAccountPasswordHash.md
schema: 2.0.0
---

# Set-SamAccountPasswordHash

## SYNOPSIS
Sets NT and LM hashes of an Active Directory or local account through the MS-SAMR protocol.

## SYNTAX

### ByLogonName
```
Set-SamAccountPasswordHash -SamAccountName <String> -Domain <String> -NTHash <Byte[]> [-LMHash <Byte[]>]
 [-Credential <PSCredential>] [-Server <String>] [<CommonParameters>]
```

### BySid
```
Set-SamAccountPasswordHash -Sid <SecurityIdentifier> -NTHash <Byte[]> [-LMHash <Byte[]>]
 [-Credential <PSCredential>] [-Server <String>] [<CommonParameters>]
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
Specify the user account credentials to use to perform this task.
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
Specify the user's domain.

```yaml
Type: String
Parameter Sets: ByLogonName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -LMHash
Specify a new LM password hash value in hexadecimal format.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -NTHash
Specify a new NT password hash value in hexadecimal format.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SamAccountName
Specify user's login.

```yaml
Type: String
Parameter Sets: ByLogonName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Server
Specifies the name of a SAM server.

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

### -Sid
Specify user SID.

```yaml
Type: SecurityIdentifier
Parameter Sets: BySid
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Byte[]

## OUTPUTS

### None

## NOTES

## RELATED LINKS
