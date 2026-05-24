---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-DpapiNgPfxCertificate.md
schema: 2.0.0
---

# Get-DpapiNgPfxCertificate

## SYNOPSIS
Extracts the SID-based DPAPI-NG certificate password protector from a PFX file.

## SYNTAX

```
Get-DpapiNgPfxCertificate [-Path] <String> [<CommonParameters>]
```

## DESCRIPTION

This cmdlet parses a PKCS #12 PFX file and returns the SID-protected certificate password metadata.
The returned object contains the PFX file path and the DPAPI-NG encrypted certificate password protector.

To decrypt the protected password, pipe the returned object to [Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md#unprotect-dpapingpfxcertificate).

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DpapiNgPfxCertificate -Path .\Certificate.pfx |
            Select-Object FilePath, @{ Name = 'Descriptor'; Expression = { $_.EncryptedPassword.Descriptor } }
```

Reads a SID-protected PFX file and displays the source path and protection descriptor.

## PARAMETERS

### -Path
Specifies the path to the PFX file.

```yaml
Type: String
Parameter Sets: (All)
Aliases: FilePath, FullName, PfxPath

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### DSInternals.PowerShell.PfxProtectedPassword

## NOTES

Alias: `Get-CngDpapiPfxCertificate`

## RELATED LINKS

[Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
