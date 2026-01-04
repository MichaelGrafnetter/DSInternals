# DSInternals SAM Library

## Introduction

The **DSInternals.SAM** package implements clients for the **Security Account Manager Remote Protocol (MS-SAMR)**
and **Local Security Authority Remote Protocol (MS-LSAD/LSARPC)**.
It enables remote manipulation of user accounts and LSA policies on Windows systems.

### Key Features

- **Password Hash Injection**: Set NT/LM password hashes directly on accounts
- **Account Management**: Query and modify user account properties
- **LSA Policy Access**: Read and modify Local Security Authority policies
- **DPAPI Backup Key Retrieval**: Extract domain DPAPI backup keys via LSARPC

## Usage Examples

### Connecting to a SAM Server

```csharp
using DSInternals.SAM;

string serverName = "dc01.contoso.com";

// Connect to the SAM server
using var samServer = new SamServer(serverName);

// List available domains
string[] domains = samServer.EnumerateDomains();

foreach (string domain in domains)
{
    Console.WriteLine($"Domain: {domain}");
}
```

### Setting a Password Hash

```csharp
using DSInternals.SAM;

string serverName = "dc01.contoso.com";
string domainName = "CONTOSO";
string userName = "testuser";

// NT hash to set (32 hex characters = 16 bytes)
byte[] ntHash = Convert.FromHexString("A4F49C406510BDCAB6824EE7C30FD852");

using var samServer = new SamServer(serverName);
using var domain = samServer.OpenDomain(domainName);
using var user = domain.OpenUser(userName);

// Set the NT hash directly
user.SetPasswordHash(ntHash);

Console.WriteLine($"Password hash set successfully for {userName}");
```

### Connecting with Alternate Credentials

```csharp
using DSInternals.SAM;
using System.Net;

string serverName = "dc01.contoso.com";

var credential = new NetworkCredential(
    userName: "admin",
    password: "P@ssw0rd",
    domain: "CONTOSO"
);

using var samServer = new SamServer(serverName, SamServerAccessMask.MaximumAllowed, credential);

// Now perform SAM operations...
```

### Retrieving LSA Backup Key

```csharp
using DSInternals.SAM;

string serverName = "dc01.contoso.com";

// Get the DPAPI backup key via LSARPC
var backupKey = LsaPolicy.GetDPAPIBackupKey(serverName);
```
