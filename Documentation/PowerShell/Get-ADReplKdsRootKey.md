---
external help file: DSInternals.PowerShell.dll-Help.xml
Module Name: DSInternals
online version: https://github.com/MichaelGrafnetter/DSInternals/blob/master/Documentation/PowerShell/Get-ADReplKdsRootKey.md
schema: 2.0.0
---

# Get-ADReplKdsRootKey

## SYNOPSIS
Fetches the specified KDS Root Key through the MS-DRSR protocol.

## SYNTAX

```
Get-ADReplKdsRootKey [-RootKeyId] <Guid> -Server <String> [-Credential <PSCredential>]
 [-Protocol <RpcProtocol>] [<CommonParameters>]
```

## DESCRIPTION
KDS Root Keys are used to encrypt the following:

- SID-protected private keys in PFX certificate files
- BitLocker-enabled drives with SID protector
- Passwords of Group Managed Service Accounts (gMSA) and Delegated Managed Service Accounts (dMSA)
- DNSSEC key signing keys
- Windows LAPS passwords

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-ADReplKdsRootKey -Server CONTOSO-DC -RootKeyId 1c556b71-ed22-c45f-723c-ddbe199f6824

<# Sample Output:
Id: 1c556b71-ed22-c45f-723c-ddbe199f6824
Creation Time: 7/17/2025 10:13:54 AM
Effective Time: 7/17/2025 12:13:54 AM
Domain Controller: CN=CONTOSO-DC,OU=Domain Controllers,DC=contoso,DC=com
Key: ccc82c5ed3c98fdb0c2890b2881c01e3a0dbc0f5594fca5b5aab7286fff60154b0733fab4b0ec5b622c1c2ab9c45cf1567893534f073ab6fb072e8113bbce329
Key Derivation Function
  Algorithm: SP800_108_CTR_HMAC
  Parameters: {[0, SHA512]}
Secret Agreement
  Algorithm: DH
  Public Key Length: 2048
  Private Key Length: 512
  Parameters
    0c0200004448504d0001000087a8e61db4b6663cffbbd19c651959998ceef608660dd0f25d2ceed4435e3b00e00df8f1d61957d4faf7df4561b2aa3016c3d91134096faa3
    bf4296d830e9a7c209e0c6497517abd5a8a9d306bcf67ed91f9e6725b4758c022e0b1ef4275bf7b6c5bfc11d45f9088b941f54eb1e59bb8bc39a0bf12307f5c4fdb70c581
    b23f76b63acae1caa6b7902d52526735488a0ef13c6d9a51bfa4ab3ad8347796524d8ef6a167b5a41825d967e144e5140564251ccacb83e6b486f6b3ca3f7971506026c0b
    857f689962856ded4010abd0be621c3a3960a54e710c375f26375d7014103a4b54330c198af126116d2276e11715f693877fad7ef09cadb094ae91e1a15973fb32c9b7313
    4d0b2e77506660edbd484ca7b18f21ef205407f4793a1a0ba12510dbc15077be463fff4fed4aac0bb555be3a6c1b0c6b47b1bc3773bf7e8c6f62901228f8c28cbb18a55ae
    31341000a650196f931c77a57f2ddf463e5e9ec144b777de62aaab8a8628ac376d282d6ed3864e67982428ebc831d14348f6f2f9193b5045af2767164e1dfc967c1fb3f2e
    55a4bd1bffe83b9c80d052b985d182ea0adb2a3b7313d3fe14c8484b1e052588b9b7d2bbd2df016199ecd06e1557cd0915b3353bbb64e0ec377fd028370df92b52c789142
    8cdc67eb6184b523d1db246c32f63078490f00ef8d647d148d47954515e2327cfef98c582664b4c0f6cc41659
#>
```

Retrieves the KDS Root Key with ID 1c556b71-ed22-c45f-723c-ddbe199f6824 from a DC using the replication protocol.

## PARAMETERS

### -Credential
Specifies a user account that has permission to perform this action. The default is the current user.

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

### -RootKeyId
Specifies a single KDS Root Key to be fetched.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: Id, KeyId

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Server
Specifies the target computer for the operation. Enter a fully qualified domain name (FQDN), a NetBIOS name, or an IP address. When the remote computer is in a different domain than the local computer, the fully qualified domain name is required.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Host, DomainController, DC

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid

## OUTPUTS

### DSInternals.Common.Data.KdsRootKey

## NOTES

## RELATED LINKS

[Get-ADDBKdsRootKey](Get-ADDBKdsRootKey.md)
