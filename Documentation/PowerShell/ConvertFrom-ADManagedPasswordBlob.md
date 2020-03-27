---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/ConvertFrom-ADManagedPasswordBlob.md
schema: 2.0.0
---

# ConvertFrom-ADManagedPasswordBlob

## SYNOPSIS
Decodes the value of the msDS-ManagedPassword attribute of a Group Managed Service Account.

## SYNTAX

```
ConvertFrom-ADManagedPasswordBlob [-Blob] <Byte[]> [<CommonParameters>]
```

## DESCRIPTION
Decodes the MSDS-MANAGEDPASSWORD_BLOB structure containing a group-managed service account's password information.

The password is actually a cryptographically generated array of 256 bytes that is represented as an 128 characters long UTF-16 string.

## EXAMPLES

### Example 1
```powershell
PS C:\> $gmsa = Get-ADServiceAccount -Identity 'SQL_HQ_Primary' -Properties 'msDS-ManagedPassword'
PS C:\> ConvertFrom-ADManagedPasswordBlob -Blob $gmsa.'msDS-ManagedPassword'
<# Sample Output:
Version                   : 1
CurrentPassword           : 湤ୟɰ橣낔饔ᦺ几᧾ʞꈠ⿕ՔὬ랭뷾햾咶郸�렇ͧ퀟᝘럓몚ꬶ佩䎖∘Ǐ㦗ן뱷鼹⽩Ⲃ⫝咽㠅Ｅ䠹鸞왶婰鞪
PreviousPassword          :
QueryPasswordInterval     : 29.17:15:36.3736817
UnchangedPasswordInterval : 29.17:10:36.3736817
#>
```

Decodes the managed password information from a group-managed service account (GMSA) called *SQL_HQ_Primary*. The user retrieving the managed password needs to be listed in the *PrincipalsAllowedToRetrieveManagedPassword* property of the GMSA.

## PARAMETERS

### -Blob
Specifies the binary value stored in the msDS-ManagedPassword attribute.

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: msDS-ManagedPassword, ManagedPassword, ManagedPasswordBlob

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

### DSInternals.Common.Data.ManagedPassword

## NOTES

## RELATED LINKS

[Get-ADServiceAccount](https://docs.microsoft.com/en-us/powershell/module/addsadministration/get-adserviceaccount)
[Set-ADServiceAccount](https://docs.microsoft.com/en-us/powershell/module/addsadministration/set-adserviceaccount)
[New-ADServiceAccount](https://docs.microsoft.com/en-us/powershell/module/addsadministration/new-adserviceaccount)
