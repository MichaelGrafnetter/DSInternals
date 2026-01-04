# DSInternals Common Library

## Introduction

The **DSInternals.Common** package is the foundational library shared between other DSInternals packages.
It provides core cryptographic functionality and utilities for working with Active Directory and Microsoft Entra ID (Azure AD) security data.

As this library uses Windows-specific cryptographic APIs (P/Invoke), it is only supported on Windows.

### Key Features

- **Password Hash Computation**: Calculate NT hash, LM hash, and OrgId hash values
- **Kerberos Key Derivation**: Generate Kerberos keys from passwords
- **Key Credential Parsing**: Parse and create NGC, FIDO2, and STK key credentials
- **Active Directory Credential Extraction**: Decrypt password hashes, LAPS passwords, DPAPI certificates, and DNSSEC keys stored in Active Directory

## Usage Examples

### Computing NT Hash

```csharp
using DSInternals.Common.Cryptography;

// Compute NT hash from a plain-text password
byte[] ntHash = NTHash.ComputeHash("MyPassword123");
```
