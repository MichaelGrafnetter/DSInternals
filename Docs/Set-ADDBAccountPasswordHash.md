---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version:
schema: 2.0.0
---

# Set-ADDBAccountPasswordHash

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### ByName
```
Set-ADDBAccountPasswordHash -NTHash <Byte[]> [-SupplementalCredentials <SupplementalCredentials>]
 -BootKey <Byte[]> [-SkipMetaUpdate] [-SamAccountName] <String> -DBPath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### BySID
```
Set-ADDBAccountPasswordHash -NTHash <Byte[]> [-SupplementalCredentials <SupplementalCredentials>]
 -BootKey <Byte[]> [-SkipMetaUpdate] -ObjectSid <SecurityIdentifier> -DBPath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByDN
```
Set-ADDBAccountPasswordHash -NTHash <Byte[]> [-SupplementalCredentials <SupplementalCredentials>]
 -BootKey <Byte[]> [-SkipMetaUpdate] -DistinguishedName <String> -DBPath <String> [-LogPath <String>]
 [<CommonParameters>]
```

### ByGuid
```
Set-ADDBAccountPasswordHash -NTHash <Byte[]> [-SupplementalCredentials <SupplementalCredentials>]
 -BootKey <Byte[]> [-SkipMetaUpdate] -ObjectGuid <Guid> -DBPath <String> [-LogPath <String>]
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

### -BootKey
{{Fill BootKey Description}}

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: Key, SysKey

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DBPath
TODO

```yaml
Type: String
Parameter Sets: (All)
Aliases: Database, DatabasePath, DatabaseFilePath, DBFilePath

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DistinguishedName
TODO

```yaml
Type: String
Parameter Sets: ByDN
Aliases: dn

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -LogPath
TODO

```yaml
Type: String
Parameter Sets: (All)
Aliases: Log, TransactionLogPath

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NTHash
{{Fill NTHash Description}}

```yaml
Type: Byte[]
Parameter Sets: (All)
Aliases: Hash, PasswordHash, NTLMHash, MD4Hash, h

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ObjectGuid
TODO

```yaml
Type: Guid
Parameter Sets: ByGuid
Aliases: Guid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ObjectSid
TODO

```yaml
Type: SecurityIdentifier
Parameter Sets: BySID
Aliases: Sid

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SamAccountName
TODO

```yaml
Type: String
Parameter Sets: ByName
Aliases: Login, sam

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SkipMetaUpdate
TODO

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: SkipMeta, NoMetaUpdate, NoMeta, SkipObjMeta, NoObjMeta, SkipMetaDataUpdate, NoMetaDataUpdate

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SupplementalCredentials
{{Fill SupplementalCredentials Description}}

```yaml
Type: SupplementalCredentials
Parameter Sets: (All)
Aliases: KerberosKeys, sc, c

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Byte[]

### DSInternals.Common.Data.SupplementalCredentials

### System.String

### System.Security.Principal.SecurityIdentifier

### System.Guid

## OUTPUTS

### None

## NOTES

## RELATED LINKS
