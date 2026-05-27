---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Export-ADSIDnsServerSigningKey.md
schema: 2.0.0
---

# Export-ADSIDnsServerSigningKey

## SYNOPSIS
Exports DNSSEC signing key private keys from Active Directory through LDAP to files.

## SYNTAX

```
Export-ADSIDnsServerSigningKey [-DirectoryPath] <String> [-Force] [-KdsRootKey <KdsRootKey[]>]
 [-ZoneName <String>] [-Server <String>] [-Credential <PSCredential>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet retrieves the DNSSEC signing keys from the target domain controller through LDAP, decrypts their private key material, and writes each one to a file in the target directory.
Each key is saved in the CNG private key blob format using the `{zone}_{guid}.pvk` naming convention.

## EXAMPLES

### Example 1
```powershell
PS C:\> Export-ADSIDnsServerSigningKey -DirectoryPath .\keys -Verbose -Force

<# Sample Output:
VERBOSE: Wrote DNS signing key 0b2ec560-1db7-41aa-98c6-8bdacc8727e5 to 'C:\keys\contoso.com_0b2ec560-1db7-41aa-98c6-8bdacc8727e5.pvk'.
VERBOSE: Wrote DNS signing key 54462d7d-3b27-46b4-bc72-67eedd994003 to 'C:\keys\contoso.com_54462d7d-3b27-46b4-bc72-67eedd994003.pvk'.
VERBOSE: Wrote DNS signing key ddf94e7e-0ed8-4bbb-9447-03fe66202874 to 'C:\keys\contoso.com_ddf94e7e-0ed8-4bbb-9447-03fe66202874.pvk'.
VERBOSE: Wrote DNS signing key f91d2384-895d-45a2-adf9-f037624a107f to 'C:\keys\contoso.com_f91d2384-895d-45a2-adf9-f037624a107f.pvk'.
VERBOSE: Wrote DNS signing key 1c3737ea-f9a4-4902-b6a1-debc6227e627 to 'C:\keys\contoso.com_1c3737ea-f9a4-4902-b6a1-debc6227e627.pvk'.
VERBOSE: Wrote DNS signing key 8f35bfb0-2e9e-41e5-b165-feba44d1f206 to 'C:\keys\contoso.com_8f35bfb0-2e9e-41e5-b165-feba44d1f206.pvk'.
VERBOSE: Wrote DNS signing key becff19e-2a5d-409a-ad37-c6e730ec2620 to 'C:\keys\contoso.com_becff19e-2a5d-409a-ad37-c6e730ec2620.pvk'.
VERBOSE: Wrote DNS signing key f7953139-0209-487e-931e-fd217763b314 to 'C:\keys\contoso.com_f7953139-0209-487e-931e-fd217763b314.pvk'.
VERBOSE: Wrote DNS signing key 7488d063-5beb-4962-911b-baaccf1bb91d to 'C:\keys\contoso.com_7488d063-5beb-4962-911b-baaccf1bb91d.pvk'.
VERBOSE: Wrote DNS signing key 1a661c6a-b7e0-4efe-9fe2-98855f2a3a81 to 'C:\keys\contoso.com_1a661c6a-b7e0-4efe-9fe2-98855f2a3a81.pvk'.
VERBOSE: Wrote DNS signing key dc947fb5-42b9-4674-a3c6-1ab33ffdfa75 to 'C:\keys\contoso.com_dc947fb5-42b9-4674-a3c6-1ab33ffdfa75.pvk'.
VERBOSE: Wrote DNS signing key a49c47bb-ee02-4daa-a28c-18ae96626f2b to 'C:\keys\contoso.com_a49c47bb-ee02-4daa-a28c-18ae96626f2b.pvk'.
VERBOSE: Wrote DNS signing key 9e2e19e8-4515-414f-8f54-e59501bad001 to 'C:\keys\contoso.com_9e2e19e8-4515-414f-8f54-e59501bad001.pvk'.
#>
```

Exports every DNSSEC signing key (including the rolled-over historical keys) to the current `.\keys` directory, overwriting any existing files and reporting each written file path through the verbose stream.

## PARAMETERS

### -Credential
Specifies a user account to use when connecting to the target domain controller. The default is the current user.

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

### -DirectoryPath
Specifies the path to the output directory where the exported signing key files are written. The directory must already exist.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Path, OutputPath, OutputDirectory, OutDir

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Indicates that existing signing key files in the output directory should be overwritten.

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

### -KdsRootKey
Provides an explicit set of KDS root keys to use when decrypting the DPAPI-NG-protected signing key private keys. When this parameter is specified, the supplied keys override the default LDAP-based lookup against the configuration naming context.

```yaml
Type: KdsRootKey[]
Parameter Sets: (All)
Aliases: KdsRootKeys, RootKey, RootKeys

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Server
Specifies the target domain controller. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Host, DomainController, DC, ComputerName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZoneName
Restricts the export to the DNS zone with the specified name. The value is matched case-insensitively against the zone's fully qualified domain name (FQDN).

```yaml
Type: String
Parameter Sets: (All)
Aliases: Zone, DnsZone

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### None

## NOTES

## RELATED LINKS

[Get-ADSIDnsServerSigningKey](Get-ADSIDnsServerSigningKey.md)
[Export-ADDBDnsServerSigningKey](Export-ADDBDnsServerSigningKey.md)
[Get-ADSIDnsServerZone](Get-ADSIDnsServerZone.md)
