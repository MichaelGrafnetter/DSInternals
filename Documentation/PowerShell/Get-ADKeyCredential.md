---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADKeyCredential.md
schema: 2.0.0
---

# Get-ADKeyCredential

## SYNOPSIS
Creates an object representing Windows Hello for Business or FIDO credentials from its binary representation or an X.509 certificate.

## SYNTAX

### FromUserCertificate (Default)
```
Get-ADKeyCredential [-Certificate] <X509Certificate2> [-DeviceId] <Guid> -OwnerDN <String>
 [-CreationTime <DateTime>] [<CommonParameters>]
```

### FromDNBinary
```
Get-ADKeyCredential [-DNWithBinaryData] <String[]> [<CommonParameters>]
```

### FromBinary
```
Get-ADKeyCredential -BinaryData <Byte[]> -OwnerDN <String> [<CommonParameters>]
```

### FromComputerCertificate
```
Get-ADKeyCredential [-Certificate] <X509Certificate2> -OwnerDN <String> [-CreationTime <DateTime>]
 [-IsComputerKey] [<CommonParameters>]
```

## DESCRIPTION

This cmdlet can be used to display existing key credentials from Active Directory (including NGC, STK and FIDO keys) and to generate new NGC credentials from self-signed certificates. See the examples for more info.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADObject -LDAPFilter '(msDS-KeyCredentialLink=*)' -Properties msDS-KeyCredentialLink |
            Select-Object -ExpandProperty msDS-KeyCredentialLink |
            Get-ADKeyCredential
<# Sample Output:

Usage Source  Flags       DeviceId                             Created    Owner
----- ------  -----       --------                             -------    -----
NGC   AD      None        cfe9a872-13ff-4751-a777-aec88c30a762 2019-08-01 CN=John Doe,CN=Users,DC=contoso,DC=com
STK   AD      None                                             2017-08-23 CN=PC01,CN=Computers,DC=contoso,DC=com
NGC   AD      MFANotUsed                                       2017-08-23 CN=PC02,CN=Computers,DC=contoso,DC=com
NGC   AzureAD None        fd591087-245c-4ff5-a5ea-c14de5e2b32d 2017-07-19 CN=John Doe,CN=Users,DC=contoso,DC=com
FIDO  AzureAD Attestation 00000000-0000-0000-0000-000000000000 2019-08-26 CN=John Doe,CN=Users,DC=contoso,DC=com

#>
```

Lists all key credentials that are registered in Active Directory.

### Example 2
```powershell
PS C:\> Get-ADObject -LDAPFilter '(msDS-KeyCredentialLink=*)' -Properties msDS-KeyCredentialLink |
            Select-Object -ExpandProperty msDS-KeyCredentialLink |
            Get-ADKeyCredential |
            Where-Object Usage -eq NGC |
            Format-Table -View ROCA
<# Sample Output:

Usage IsWeak Source  DeviceId                             Created    Owner
----- ------ ------  --------                             -------    -----
NGC   True   AzureAD fd591087-245c-4ff5-a5ea-c14de5e2b32d 2017-07-19 CN=John Doe,CN=Users,DC=contoso,DC=com
NGC   False  AD      1966d4da-14da-4581-a7a7-5e8e07e93ad9 2019-08-01 CN=Jane Doe,CN=Users,DC=contoso,DC=com

#>
```

Lists weak public keys registered in Active Directory that were generated on ROCA-vulnerable TPMs.

### Example 3
```powershell
PS C:\> Get-ADObject -LDAPFilter '(msDS-KeyCredentialLink=*)' -Properties msDS-KeyCredentialLink |
            Select-Object -ExpandProperty msDS-KeyCredentialLink |
            Get-ADKeyCredential |
            Where-Object Usage -eq NGC |
            Format-Custom -View Moduli |
            Out-File -FilePath .\moduli.txt -Encoding ascii -Force
```

Exports all RSA public key moduli from NGC keys to a file in BASE64 encoding. This format is supported by the original Python ROCA Detection Tool.

### Example 4
```powershell
PS C:\> Get-ADObject -LDAPFilter '(msDS-KeyCredentialLink=*)' -Properties msDS-KeyCredentialLink |
            Select-Object -ExpandProperty msDS-KeyCredentialLink |
            Get-ADKeyCredential |
            Where-Object Usage -eq FIDO |
            Format-Table -View FIDO
<# Sample Output:

DisplayName           AAGUID                               Alg   Counter Created    Owner
-----------           ------                               ---   ------- -------    -----
eWMB Goldengate G320  87dbc5a1-4c94-4dc8-8a47-97d800fd1f3c ES256      37 2019-08-29 CN=John Doe,CN=Users,DC=contoso,DC=com
eWBM Goldengate G310  95442b2e-f15e-4def-b270-efb106facb4e ES256      48 2019-08-29 CN=John Doe,CN=Users,DC=contoso,DC=com
Yubikey 5             cb69481e-8ff7-4039-93ec-0a2729a154a8 ES256      25 2019-06-21 CN=John Doe,CN=Users,DC=contoso,DC=com
Feitian All-In-Pass   12ded745-4bed-47d4-abaa-e713f51d6393 ES256    1398 2020-03-31 CN=John Doe,CN=Users,DC=contoso,DC=com

#>
```

Lists FIDO tokens registered in Active Directory.

### Example 5
```powershell
PS C:\> Get-ADUser -Identity john -Properties msDS-KeyCredentialLink |
    Select-Object -ExpandProperty msDS-KeyCredentialLink |
    Get-ADKeyCredential |
    Out-GridView -OutputMode Multiple -Title 'Select a credentials for removal...' |
    ForEach-Object { Set-ADObject -Identity $PSItem.Owner -Remove @{ 'msDS-KeyCredentialLink' = $PSItem.ToDNWithBinary() } }
```

Selectively deletes key credentials from Active Directory.

### Example 6
```powershell
PS C:\> $upn = 'john@contoso.com'
PS C:\> $userSid = 'S-1-5-21-1236425271-2880748467-2592687428-1109'
PS C:\> $certificateSubject = '{0}/{1}/login.windows.net/383a3889-5bc9-47a3-846c-2b70f0b7fe0e/{2}' -f $userSid, (New-Guid), $upn
PS C:\> $certificate = New-SelfSignedCertificate -Subject $certificateSubject `
                                                 -KeyLength 2048 `
                                                 -Provider 'Microsoft Strong Cryptographic Provider' `
                                                 -CertStoreLocation Cert:\CurrentUser\My `
                                                 -NotAfter (Get-Date).AddYears(30) `
                                                 -TextExtension '2.5.29.19={text}false', '2.5.29.37={text}1.3.6.1.4.1.311.20.2.2' `
                                                 -SuppressOid '2.5.29.14' `
                                                 -KeyUsage None `
                                                 -KeyExportPolicy Exportable
PS C:\> $ngcKey = Get-ADKeyCredential -Certificate $certificate -DeviceId (New-Guid) -OwnerDN 'CN=John Doe,CN=Users,DC=contoso,DC=com'
PS C:\> Set-ADObject -Identity $ngcKey.Owner -Add @{ 'msDS-KeyCredentialLink' = $ngcKey.ToDNWithBinary() }
```

Generates a new NGC key for a user account and registers it in Active Directory.
Note that the value of the certificate Subject has no effect on the functionality, but as it appears in DC logs, this example uses the same format as Windows does.

### Example 7
```powershell
PS C:\> $certificate = New-SelfSignedCertificate -Subject 'S-1-5-21-1236425271-2880748467-2592687428-1001' `
                                                 -KeyLength 2048 `
                                                 -Provider 'Microsoft Strong Cryptographic Provider' `
                                                 -CertStoreLocation Cert:\LocalMachine\My `
                                                 -NotAfter (Get-Date).AddYears(30) `
                                                 -TextExtension '2.5.29.19={text}false', '2.5.29.37={text}1.3.6.1.4.1.311.20.2.2' `
                                                 -SuppressOid '2.5.29.14' `
                                                 -KeyUsage None `
                                                 -KeyExportPolicy Exportable
PS C:\> $ngcKey = Get-ADKeyCredential -IsComputerKey -Certificate $certificate -OwnerDN 'CN=PC01,CN=Computers,DC=contoso,DC=com'
PS C:\> Set-ADComputer -Identity 'PC01$' -Clear msDS-KeyCredentialLink -Add @{ 'msDS-KeyCredentialLink' = $ngcKey.ToDNWithBinary() }
```

Performs a validated write of computer NGC key. Must be executed under the computer account's identity.

## PARAMETERS

### -BinaryData
Specifies the credentials in binary/hexadecimal format.

```yaml
Type: Byte[]
Parameter Sets: FromBinary
Aliases: Binary

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Certificate
Specifies a certificate that wraps an NGC key.

```yaml
Type: X509Certificate2
Parameter Sets: FromUserCertificate, FromComputerCertificate
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CreationTime
Specifies the time when the key was created. Default value is the current time.

```yaml
Type: DateTime
Parameter Sets: FromUserCertificate, FromComputerCertificate
Aliases: CreatedTime, TimeCreated, TimeGenerated

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeviceId
Specifies an identifier (typically objectGUID) of the associated computer.

```yaml
Type: Guid
Parameter Sets: FromUserCertificate
Aliases: ComputerId, ComputerGuid

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DNWithBinaryData
Specifies the credentials in the DN-Binary syntax.

```yaml
Type: String[]
Parameter Sets: FromDNBinary
Aliases: DNWithBinary, DistinguishedNameWithBinary

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IsComputerKey
Indicates that the resulting key credential must meet the DS-Validated-Write-Computer requirements.

```yaml
Type: SwitchParameter
Parameter Sets: FromComputerCertificate
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwnerDN
Specifies the distinguished name (DN) of the object that these credentials are associated with.

```yaml
Type: String
Parameter Sets: FromUserCertificate, FromBinary, FromComputerCertificate
Aliases: DistinguishedName, DN, ObjectDN, HolderDN, Holder, Owner, UserPrincipalName, UPN

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### DSInternals.Common.Data.KeyCredential

## NOTES

## RELATED LINKS

[Add-ADReplNgcKey](Add-ADReplNgcKey.md)
[Get-ADReplAccount](Get-ADReplAccount.md)
[Get-ADDBAccount](Get-ADDBAccount.md)
[Get-AzureADUserEx](Get-AzureADUserEx.md)
