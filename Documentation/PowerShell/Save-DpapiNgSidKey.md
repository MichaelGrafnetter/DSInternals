---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Save-DpapiNgSidKey.md
schema: 2.0.0
---

# Save-DpapiNgSidKey

## SYNOPSIS
Derives a SID-protected DPAPI-NG group key from a KDS root key and writes it to the local SID key cache.

## SYNTAX

### ParsedIdentifier (Default)
```
Save-DpapiNgSidKey [-Identifier] <ProtectionKeyIdentifier> [-KdsRootKey] <KdsRootKey[]>
 [-SecurityIdentifier] <SecurityIdentifier> [<CommonParameters>]
```

### IdentifierBlob
```
Save-DpapiNgSidKey [-IdentifierBlob] <Byte[]> [-KdsRootKey] <KdsRootKey[]>
 [-SecurityIdentifier] <SecurityIdentifier> [<CommonParameters>]
```

## DESCRIPTION

This cmdlet derives a SID-bound group key from the KDS root key whose identifier matches the supplied `ProtectionKeyIdentifier` and writes the resulting `KDSK` Group Key Envelope to the local SID key cache. The native DPAPI-NG implementation consults this cache during decryption, so seeding it offline allows subsequent DPAPI-NG operations (e.g. `Unprotect-DpapiNgData`, `Unprotect-DpapiNgPfxCertificate`, or `manage-bde -unlock -sid`) to succeed without contacting a domain controller.

The `-KdsRootKey` parameter accepts an array of candidate root keys; the cmdlet picks the one whose `KeyId` matches the root key identifier embedded in the `KDSK` blob. The identifier can be supplied either as a parsed `ProtectionKeyIdentifier` (via `-Identifier`) or as the raw `KDSK` byte array (via `-IdentifierBlob`, which also accepts a hex string).

The cmdlet does not return any output. Use the `-Verbose` switch to see the full path to the cache file that holds the envelope.

## EXAMPLES

### Example 1
```powershell
PS C:\> $rootKeys = Get-ADDBKdsRootKey -DatabasePath '.\ntds.dit'
PS C:\> $keyId = Get-DpapiNgSidKeyIdentifier -Blob '010000004B44534B020000006C010000040000001C000000716B551C22ED5FC4723CDDBE199F682420000000180000001800000068838DA438D3C1CA36707F12B46B37776E0A3DB46ADB3DB2199D29E01976FCAE63006F006E0074006F0073006F002E0063006F006D00000063006F006E0074006F0073006F002E0063006F006D000000'
PS C:\> Save-DpapiNgSidKey -Identifier $keyId -KdsRootKey $rootKeys -Sid 'S-1-5-21-3288850392-3299536932-2614793081-516'
```

Reconstructs a SID-protected group key for the domain controllers group and seeds it into the local SID key cache so subsequent DPAPI-NG decryption can proceed offline. The cmdlet selects the root key whose `KeyId` matches the identifier from the array of candidates.

### Example 2
```powershell
PS C:\> Save-DpapiNgSidKey -IdentifierBlob '010000004B44534B020000006C010000040000001C000000716B551C22ED5FC4723CDDBE199F682420000000180000001800000068838DA438D3C1CA36707F12B46B37776E0A3DB46ADB3DB2199D29E01976FCAE63006F006E0074006F0073006F002E0063006F006D00000063006F006E0074006F0073006F002E0063006F006D000000' -KdsRootKey (Get-ADDBKdsRootKey -DatabasePath '.\ntds.dit') -Sid 'S-1-5-21-3288850392-3299536932-2614793081-516'
```

Passes the `KDSK` Protection Key Identifier as a hex string. The cmdlet converts it to bytes via `[AcceptHexString]` and parses the result into a `ProtectionKeyIdentifier`.

### Example 4
```powershell
PS C:\> Save-DpapiNgSidKey -Identifier $keyId -KdsRootKey (Import-Clixml -Path .\KdsRootKeys.xml) -SecurityIdentifier S-1-5-21-3288850392-3299536932-2614793081-512 -Verbose

<# Sample Output:
VERBOSE: Saved DPAPI-NG SID key to 'C:\Users\John\AppData\Local\Microsoft\Crypto\KdsKey\b45c216c6b4390a526c9265a7926be9c9156980c7b2cb06c4f1a39acf0429765\PrivateKey\364-1c556b71-ed22-c45f-723c-ddbe199f6824'.
#>

PS C:\> manage-bde.exe -unlock e: -sid S-1-5-21-3288850392-3299536932-2614793081-512

<# Sample Output:
BitLocker Drive Encryption: Configuration Tool version 10.0.26100
Copyright (C) 2013 Microsoft Corporation. All rights reserved.

A SID-based Identity protector successfully unlocked the volume E:.
#>
```

Reconstructs the symmetric group key for a BitLocker SID-based Identity protector and seeds it into the local SID key cache. The KDS root key is loaded from a `Clixml` export (for example, captured earlier with `Get-ADSIKdsRootKey | Export-Clixml`). Once the key is cached, `manage-bde.exe -unlock -sid` succeeds offline; without the cached key the same command fails with *"A SID-based Identity protector failed to unlock volume E:."*

## PARAMETERS

### -Identifier
Specifies the parsed DPAPI-NG protection key identifier (`KDSK` blob) whose L0/L1/L2 cycle and root key identifier describe the group key to be derived. Typically obtained from `Get-DpapiNgSidKeyIdentifier`.

```yaml
Type: ProtectionKeyIdentifier
Parameter Sets: ParsedIdentifier
Aliases: ProtectionKeyIdentifier, KeyIdentifier, KeyId

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IdentifierBlob
Specifies the raw `KDSK` Protection Key Identifier blob as a byte array. Also accepts a hex-encoded string, which is converted to bytes via `[AcceptHexString]`.

```yaml
Type: Byte[]
Parameter Sets: IdentifierBlob
Aliases: ProtectionKeyIdentifierBlob, KeyIdentifierBlob, KeyIdBlob, Blob

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KdsRootKey
Specifies one or more candidate KDS root keys. The cmdlet selects the key whose `KeyId` matches the supplied `Identifier`.

```yaml
Type: KdsRootKey[]
Parameter Sets: (All)
Aliases: KdsRootKeys, RootKey, RootKeys

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SecurityIdentifier
Specifies the Security Identifier of the principal that is authorized to derive the group key.

```yaml
Type: SecurityIdentifier
Parameter Sets: (All)
Aliases: Sid, TargetSid, ObjectSid

Required: True
Position: 2
Default value: None
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

Alias: `Save-CngDpapiSidKey`

## RELATED LINKS

[Clear-DpapiNgSidKeyCache](Clear-DpapiNgSidKeyCache.md)
[Get-DpapiNgSidKeyIdentifier](Get-DpapiNgSidKeyIdentifier.md)
[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
[Get-ADReplKdsRootKey](Get-ADReplKdsRootKey.md)
[Get-ADSIKdsRootKey](Get-ADSIKdsRootKey.md)
[Unprotect-DpapiNgData](Unprotect-DpapiNgData.md)
[Unprotect-DpapiNgPfxCertificate](Unprotect-DpapiNgPfxCertificate.md)
