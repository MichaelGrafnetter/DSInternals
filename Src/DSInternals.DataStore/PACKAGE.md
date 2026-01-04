# DSInternals DataStore Library

## Introduction

The **DSInternals.DataStore** package allows you to directly access and modify
the Active Directory database without running a domain controller.

### Key Features

- **Password Hash Extraction**: Extract NT hashes, LM hashes, and password history from ntds.dit
- **Account Manipulation**: Modify account attributes like sIDHistory and primaryGroupId
- **Backup Key Extraction**: Retrieve DPAPI backup keys from the database
- **Trust Password Extraction**: Extract inter-domain trust passwords and derive Kerberos trust keys
- **KDS Root Key Access**: Retrieve Key Distribution Service root keys for gMSA password computation
- **LAPS Password Decryption**: Decrypt Windows LAPS encrypted passwords
- **DNS Zone Export**: Extract DNS resource records from AD-integrated zones
- **BitLocker Recovery**: Extract BitLocker recovery information

## Usage Examples

### Opening an ntds.dit File

```csharp
using DSInternals.DataStore;

// Open the database in read-only mode
string ntdsPath = @"C:\Backup\ntds.dit";

using var context = new DirectoryContext(ntdsPath, readOnly: true);
using var agent = new DirectoryAgent(context);

Console.WriteLine($"Domain: {context.DomainController.DomainName}");
Console.WriteLine($"Forest Mode: {context.DomainController.ForestMode}");
```

### Extracting Password Hashes

```csharp
using DSInternals.DataStore;

string ntdsPath = @"C:\Backup\ntds.dit";
byte[] bootKey = /* Extract from SYSTEM registry hive */;

using var context = new DirectoryContext(ntdsPath, readOnly: true);
using var agent = new DirectoryAgent(context);

// Enumerate all accounts with password hashes
foreach (var account in agent.GetAccounts(bootKey))
{
    Console.WriteLine($"Account: {account.SamAccountName}");

    if (account.NTHash != null)
    {
        string ntHash = BitConverter.ToString(account.NTHash).Replace("-", "");
        Console.WriteLine($"NT Hash: {ntHash}");
    }
}
```
