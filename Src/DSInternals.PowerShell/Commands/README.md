# PowerShell Command Class Inheritance Diagram

## Overview

This document shows the class inheritance hierarchy for all binary PowerShell cmdlets in the DSInternals module.

## Base Class Hierarchy

```mermaid
classDiagram
    direction TB
    class PSCmdlet {
        <<System.Management.Automation>>
    }
    class PSCmdletEx {
        <<abstract>>
    }
    class ADDBCommandBase {
        <<abstract>>
        +DatabasePath : string
        +LogPath : string
    }
    class ADDBObjectCommandBase {
        <<abstract>>
        +DistinguishedName : string
        +ObjectGuid : Guid
    }
    class ADDBPrincipalCommandBase {
        <<abstract>>
        +SamAccountName : string
        +ObjectSid : SecurityIdentifier
    }
    class ADDBModifyPrincipalCommandBase {
        <<abstract>>
        +SkipMetaUpdate : SwitchParameter
        +Force : SwitchParameter
    }
    class ADDBAccountStatusCommandBase {
        <<abstract>>
    }
    class ADReplCommandBase {
        <<abstract>>
        +Server : string
        +Credential : PSCredential
    }
    class ADReplObjectCommandBase {
        <<abstract>>
        +DistinguishedName : string
        +ObjectGuid : Guid
    }
    class ADReplPrincipalCommandBase {
        <<abstract>>
        +SamAccountName : string
        +Domain : string
        +UserPrincipalName : string
        +ObjectSid : SecurityIdentifier
    }
    class ADSICommandBase {
        <<abstract>>
        +Server : string
        +Credential : PSCredential
    }
    class AzureADCommandBase {
        <<abstract>>
        +AccessToken : string
        +ObjectId : Guid
        +UserPrincipalName : string
        +TenantId : Guid
    }
    class LsaPolicyCommandBase {
        <<abstract>>
        +ComputerName : string
    }
    class SamCommandBase {
        <<abstract>>
        +Credential : PSCredential
        +Server : string
    }

    PSCmdlet <|-- PSCmdletEx
    PSCmdletEx <|-- ADDBCommandBase
    ADDBCommandBase <|-- ADDBObjectCommandBase
    ADDBObjectCommandBase <|-- ADDBPrincipalCommandBase
    ADDBPrincipalCommandBase <|-- ADDBModifyPrincipalCommandBase
    ADDBModifyPrincipalCommandBase <|-- ADDBAccountStatusCommandBase
    PSCmdletEx <|-- ADReplCommandBase
    ADReplCommandBase <|-- ADReplObjectCommandBase
    ADReplObjectCommandBase <|-- ADReplPrincipalCommandBase
    PSCmdlet <|-- ADSICommandBase
    PSCmdlet <|-- AzureADCommandBase
    PSCmdlet <|-- LsaPolicyCommandBase
    PSCmdletEx <|-- SamCommandBase
```

## Database Commands (ADDB)

```mermaid
classDiagram
    direction TB
    class ADDBCommandBase {
        <<abstract>>
        +DatabasePath : string
        +LogPath : string
    }
    class ADDBObjectCommandBase {
        <<abstract>>
        +DistinguishedName : string
        +ObjectGuid : Guid
    }
    class ADDBPrincipalCommandBase {
        <<abstract>>
        +SamAccountName : string
        +ObjectSid : SecurityIdentifier
    }
    class ADDBModifyPrincipalCommandBase {
        <<abstract>>
        +SkipMetaUpdate : SwitchParameter
        +Force : SwitchParameter
    }
    class ADDBAccountStatusCommandBase {
        <<abstract>>
    }

    ADDBCommandBase <|-- ADDBObjectCommandBase
    ADDBObjectCommandBase <|-- ADDBPrincipalCommandBase
    ADDBPrincipalCommandBase <|-- ADDBModifyPrincipalCommandBase
    ADDBModifyPrincipalCommandBase <|-- ADDBAccountStatusCommandBase

    class GetADDBAccountCommand {
        +All : SwitchParameter
        +Properties : AccountPropertySets
        +BootKey : byte[]
        +ExportFormat : AccountExportFormat
    }
    class GetADDBBackupKeyCommand {
        +BootKey : byte[]
    }
    class GetADDBDomainControllerCommand
    class GetADDBSchemaAttributeCommand {
        +Name : string[]
    }
    class GetADDBServiceAccountCommand {
        +EffectiveTime : DateTime
    }
    class GetADDBTrustCommand {
        +BootKey : byte[]
    }
    class GetADDBKdsRootKeyCommand {
        +RootKeyId : Guid
        +All : SwitchParameter
    }
    class GetADDBDnsResourceRecordCommand {
        +IncludeTombstones : SwitchParameter
        +IncludeRootHints : SwitchParameter
    }
    class GetADDBDnsZoneCommand
    class GetADDBIndexCommand
    class SetADDBBootKeyCommand {
        +OldBootKey : byte[]
        +NewBootKey : byte[]
        +Force : SwitchParameter
    }
    class SetADDBDomainControllerCommand {
        +HighestCommittedUsn : long
        +Epoch : int
        +BackupExpiration : DateTime
        +Force : SwitchParameter
    }
    class NewADDBRestoreFromMediaScriptCommand {
        +BootKey : byte[]
        +SysvolPath : string
        +SafeModeAdministratorPassword : SecureString
        +SkipDNSServer : SwitchParameter
        +PostInstallScriptPath : string
        +StatusReportScriptPath : string
    }

    ADDBCommandBase <|-- GetADDBBackupKeyCommand
    ADDBCommandBase <|-- GetADDBDomainControllerCommand
    ADDBCommandBase <|-- GetADDBSchemaAttributeCommand
    ADDBCommandBase <|-- GetADDBServiceAccountCommand
    ADDBCommandBase <|-- GetADDBTrustCommand
    ADDBCommandBase <|-- GetADDBKdsRootKeyCommand
    ADDBCommandBase <|-- GetADDBDnsResourceRecordCommand
    ADDBCommandBase <|-- GetADDBDnsZoneCommand
    ADDBCommandBase <|-- GetADDBIndexCommand
    ADDBCommandBase <|-- SetADDBBootKeyCommand
    ADDBCommandBase <|-- SetADDBDomainControllerCommand
    ADDBCommandBase <|-- NewADDBRestoreFromMediaScriptCommand
    ADDBPrincipalCommandBase <|-- GetADDBAccountCommand
```

## Database Object/Principal Commands

```mermaid
classDiagram
    direction TB
    class ADDBObjectCommandBase {
        <<abstract>>
        +DistinguishedName : string
        +ObjectGuid : Guid
    }
    class ADDBPrincipalCommandBase {
        <<abstract>>
        +SamAccountName : string
        +ObjectSid : SecurityIdentifier
    }
    class ADDBModifyPrincipalCommandBase {
        <<abstract>>
        +SkipMetaUpdate : SwitchParameter
        +Force : SwitchParameter
    }
    class ADDBAccountStatusCommandBase {
        <<abstract>>
    }

    ADDBObjectCommandBase <|-- ADDBPrincipalCommandBase
    ADDBPrincipalCommandBase <|-- ADDBModifyPrincipalCommandBase
    ADDBModifyPrincipalCommandBase <|-- ADDBAccountStatusCommandBase

    class RemoveADDBObjectCommand {
        +Force : SwitchParameter
    }
    class RestoreADDBAttributeCommand {
        +Property : string[]
        +VerInc : int
        +SubTree : SwitchParameter
        +Force : SwitchParameter
    }
    class GetADDBBitLockerRecoveryInformationCommand {
        +RecoveryGuid : Guid
        +ComputerName : string
        +All : SwitchParameter
    }
    class DisableADDBAccountCommand
    class EnableADDBAccountCommand
    class AddADDBSidHistoryCommand {
        +SidHistory : SecurityIdentifier[]
    }
    class SetADDBAccountPasswordCommand {
        +NewPassword : SecureString
        +BootKey : byte[]
    }
    class SetADDBAccountPasswordHashCommand {
        +NTHash : byte[]
        +SupplementalCredentials : SupplementalCredentials
        +BootKey : byte[]
    }
    class SetADDBPrimaryGroupCommand {
        +PrimaryGroupId : int
    }
    class SetADDBAccountControlCommand {
        +Enabled : bool
        +CannotChangePassword : bool
        +PasswordNeverExpires : bool
        +SmartcardLogonRequired : bool
        +UseDESKeyOnly : bool
        +HomedirRequired : bool
    }
    class UnlockADDBAccountCommand

    ADDBObjectCommandBase <|-- RemoveADDBObjectCommand
    ADDBObjectCommandBase <|-- RestoreADDBAttributeCommand
    ADDBObjectCommandBase <|-- GetADDBBitLockerRecoveryInformationCommand
    ADDBAccountStatusCommandBase <|-- DisableADDBAccountCommand
    ADDBAccountStatusCommandBase <|-- EnableADDBAccountCommand
    ADDBModifyPrincipalCommandBase <|-- AddADDBSidHistoryCommand
    ADDBModifyPrincipalCommandBase <|-- SetADDBAccountPasswordCommand
    ADDBModifyPrincipalCommandBase <|-- SetADDBAccountPasswordHashCommand
    ADDBModifyPrincipalCommandBase <|-- SetADDBPrimaryGroupCommand
    ADDBModifyPrincipalCommandBase <|-- SetADDBAccountControlCommand
    ADDBModifyPrincipalCommandBase <|-- UnlockADDBAccountCommand
```

## Replication Commands (ADRepl)

```mermaid
classDiagram
    direction TB
    class ADReplCommandBase {
        <<abstract>>
        +Server : string
        +Credential : PSCredential
    }
    class ADReplObjectCommandBase {
        <<abstract>>
        +DistinguishedName : string
        +ObjectGuid : Guid
    }
    class ADReplPrincipalCommandBase {
        <<abstract>>
        +SamAccountName : string
        +Domain : string
        +UserPrincipalName : string
        +ObjectSid : SecurityIdentifier
    }

    ADReplCommandBase <|-- ADReplObjectCommandBase
    ADReplObjectCommandBase <|-- ADReplPrincipalCommandBase

    class GetADReplAccountCommand {
        +All : SwitchParameter
        +NamingContext : string
        +Properties : AccountPropertySets
        +ExportFormat : AccountExportFormat
    }
    class GetADReplBackupKeyCommand {
        +Domain : string
    }
    class AddADReplNgcKeyCommand {
        +PublicKey : byte[]
    }
    class GetADReplKdsRootKeyCommand {
        +RootKeyId : Guid
    }

    ADReplPrincipalCommandBase <|-- GetADReplAccountCommand
    ADReplPrincipalCommandBase <|-- AddADReplNgcKeyCommand
    ADReplCommandBase <|-- GetADReplBackupKeyCommand
    ADReplCommandBase <|-- GetADReplKdsRootKeyCommand
```

## ADSI, Azure AD, LSA, and SAM Commands

```mermaid
classDiagram
    direction TB
    class ADSICommandBase {
        <<abstract>>
        +Server : string
        +Credential : PSCredential
    }
    class GetADSIAccountCommand {
        +Properties : AccountPropertySets
    }
    ADSICommandBase <|-- GetADSIAccountCommand

    class AzureADCommandBase {
        <<abstract>>
        +AccessToken : string
        +ObjectId : Guid
        +UserPrincipalName : string
        +TenantId : Guid
    }
    class GetAzureADUserExCommand {
        +All : SwitchParameter
    }
    class SetAzureADUserExCommand {
        +KeyCredential : KeyCredential[]
    }
    AzureADCommandBase <|-- GetAzureADUserExCommand
    AzureADCommandBase <|-- SetAzureADUserExCommand

    class LsaPolicyCommandBase {
        <<abstract>>
        +ComputerName : string
    }
    class GetLsaBackupKeyCommand
    class GetLsaPolicyInformationCommand
    class SetLsaPolicyInformationCommand {
        +DomainName : string
        +DnsDomainName : string
        +DnsForestName : string
        +DomainGuid : Guid
        +DomainSid : SecurityIdentifier
    }
    LsaPolicyCommandBase <|-- GetLsaBackupKeyCommand
    LsaPolicyCommandBase <|-- GetLsaPolicyInformationCommand
    LsaPolicyCommandBase <|-- SetLsaPolicyInformationCommand

    class SamCommandBase {
        <<abstract>>
        +Credential : PSCredential
        +Server : string
    }
    class GetSamPasswordPolicyCommand {
        +Domain : string
    }
    class SetSamAccountPasswordHashCommand {
        +SamAccountName : string
        +Domain : string
        +Sid : SecurityIdentifier
        +NTHash : byte[]
        +LMHash : byte[]
    }
    SamCommandBase <|-- GetSamPasswordPolicyCommand
    SamCommandBase <|-- SetSamAccountPasswordHashCommand
```

## Standalone Commands

```mermaid
classDiagram
    direction TB
    class PSCmdlet
    class PSCmdletEx

    PSCmdlet <|-- PSCmdletEx

    class GetBootKeyCommand {
        +SystemHiveFilePath : string
        +Online : SwitchParameter
    }
    class SaveDPAPIBlobCmdlet {
        +DPAPIObject : DPAPIObject
        +Account : DSAccount
        +DirectoryPath : string
    }
    class TestPasswordQualityCommand {
        +Account : DSAccount
        +SkipDuplicatePasswordTest : SwitchParameter
        +IncludeDisabledAccounts : SwitchParameter
        +WeakPasswords : string[]
        +WeakPasswordsFile : string
        +WeakPasswordHashesFile : string
        +WeakPasswordHashesSortedFile : string
        +WeakPasswordHashesSortedFilePath : string
    }
    PSCmdletEx <|-- GetBootKeyCommand
    PSCmdletEx <|-- SaveDPAPIBlobCmdlet
    PSCmdletEx <|-- TestPasswordQualityCommand

    class ConvertToKerberosKeyCommand {
        +Password : SecureString
        +Salt : string
        +Iterations : int
    }
    class ConvertToLMHashCommand {
        +Password : SecureString
    }
    class ConvertToNTHashCommand {
        +Password : SecureString
    }
    class ConvertToOrgIdHashCommand {
        +Password : SecureString
        +NTHash : byte[]
        +Salt : byte[]
    }
    class ConvertFromGPPrefPasswordCommand {
        +EncryptedPassword : string
    }
    class ConvertToGPPrefPasswordCommand {
        +Password : SecureString
    }
    class ConvertFromUnicodePasswordCommand {
        +UnicodePassword : string
    }
    class ConvertToUnicodePasswordCommand {
        +Password : SecureString
        +IsUnattendPassword : SwitchParameter
    }
    class ConvertFromADManagedPasswordBlobCommand {
        +Blob : byte[]
    }
    class ConvertToHexCommand {
        +Input : byte[]
        +UpperCase : SwitchParameter
    }
    class GetADKeyCredentialCommand {
        +DNWithBinaryData : string[]
        +BinaryData : byte[]
        +Certificate : X509Certificate2
        +DeviceId : Guid
        +OwnerDN : string
        +CreationTime : DateTime
        +IsComputerKey : SwitchParameter
    }

    PSCmdlet <|-- ConvertToKerberosKeyCommand
    PSCmdlet <|-- ConvertToLMHashCommand
    PSCmdlet <|-- ConvertToNTHashCommand
    PSCmdlet <|-- ConvertToOrgIdHashCommand
    PSCmdlet <|-- ConvertFromGPPrefPasswordCommand
    PSCmdlet <|-- ConvertToGPPrefPasswordCommand
    PSCmdlet <|-- ConvertFromUnicodePasswordCommand
    PSCmdlet <|-- ConvertToUnicodePasswordCommand
    PSCmdlet <|-- ConvertFromADManagedPasswordBlobCommand
    PSCmdlet <|-- ConvertToHexCommand
    PSCmdlet <|-- GetADKeyCredentialCommand
```

## Legend

- **Abstract classes** are marked with `<<abstract>>` and serve as base classes
- **Properties** shown are PowerShell parameters and switches
- Inheritance flows from top to bottom (parent → child)

## Command Categories

| Category | Base Class | Description |
|----------|------------|-------------|
| Database | `ADDBCommandBase` | Offline ntds.dit database operations |
| Replication | `ADReplCommandBase` | DCSync/DRS replication protocol operations |
| ADSI | `ADSICommandBase` | LDAP-based operations via ADSI |
| Azure AD | `AzureADCommandBase` | Azure Active Directory operations |
| LSA | `LsaPolicyCommandBase` | Local Security Authority operations |
| SAM | `SamCommandBase` | Security Accounts Manager operations |
| Standalone | `PSCmdlet`/`PSCmdletEx` | Utility commands without specialized base |
