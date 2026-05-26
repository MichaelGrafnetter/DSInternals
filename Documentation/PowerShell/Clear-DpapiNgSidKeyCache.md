---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Clear-DpapiNgSidKeyCache.md
schema: 2.0.0
---

# Clear-DpapiNgSidKeyCache

## SYNOPSIS
Deletes all KDS root key derived DPAPI-NG group keys cached on the local machine by the current user.

## SYNTAX

```
Clear-DpapiNgSidKeyCache [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet purges the calling user's local DPAPI-NG SID key cache by invoking the `kdscli!DeleteAllCachedKeys` API. After the cache is cleared, subsequent DPAPI-NG decryption operations will need to re-derive the required group keys from the corresponding KDS root keys, either by contacting a domain controller or by re-seeding the cache with `Save-DpapiNgSidKey`.

This is typically useful when testing offline DPAPI-NG recovery scenarios, where stale cached keys would otherwise mask the effect of newly seeded keys.

## EXAMPLES

### Example 1
```powershell
PS C:\> Clear-DpapiNgSidKeyCache
```

Deletes all cached KDS root key derived DPAPI-NG group keys for the current user, prompting for confirmation.

### Example 2
```powershell
PS C:\> Clear-DpapiNgSidKeyCache -Confirm:$false
```

Deletes all cached KDS root key derived DPAPI-NG group keys without prompting.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### None

## NOTES

Aliases: `Remove-DpapiNgSidKey`, `Clear-CngDpapiSidKeyCache`, `Remove-CngDpapiSidKey`

## RELATED LINKS

[Save-DpapiNgSidKey](Save-DpapiNgSidKey.md)
[Get-DpapiNgSidKeyIdentifier](Get-DpapiNgSidKeyIdentifier.md)
[Unprotect-DpapiNgData](Unprotect-DpapiNgData.md)
[Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md)
