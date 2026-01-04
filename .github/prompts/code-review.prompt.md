---
agent: agent
tools: ['edit/editFiles', 'search/codebase', 'search/readFile', 'problems', 'changes', 'runTests']
description: 'Perform a systematic code review of all source files, focusing on security, performance, backwards compatibility, and design principles.'
---

# Code Review Instructions for DSInternals

These instructions guide code reviews for the DSInternals repository, which contains C#, C++/CLI, and PowerShell code for Active Directory security auditing, offline database manipulation, and password management. Focus on higher-level concerns that require expert judgment rather than stylistic or syntactic issues handled by automated tooling.

If there are no code changes to review, perform a review of the entire codebase based on these guidelines to identify potential improvements or issues. Do not rely solely on targeted searches for specific artefacts. Systematically read all source files to ensure comprehensive coverage. For each file read, apply **all** the review priorities documented below. Do not skip files even if they appear simple—security issues often hide in seemingly innocuous code.

## Review Priorities

### 1. Security

**Critical Security Concerns (Especially Important for AD Security Tooling):**
- **Credential Handling**: Ensure passwords, hashes (NT, LM, Kerberos keys), and secrets are securely handled, never logged, and properly cleared from memory when no longer needed
  - Use `SecureString` where appropriate for password input
  - Clear sensitive byte arrays with `Array.Clear()` or `CryptographicOperations.ZeroMemory()` in finally blocks
  - Pin sensitive data in memory during cryptographic operations to prevent GC relocation
- **Cryptographic Operations**: Verify proper use of cryptographic APIs for password hashing (MD4, MD5, SHA-1, PBKDF2), key derivation, and encryption/decryption operations
  - Use `RandomNumberGenerator` for cryptographically secure random bytes, never `System.Random`
  - Ensure proper IV/nonce generation (unique per encryption operation)
  - Verify constant-time comparison for secret data using `CryptographicOperations.FixedTimeEquals()` to prevent timing attacks
- **Buffer Overflows (C++/CLI)**: In native C++/CLI code, check for proper bounds checking, safe memory operations, and correct use of RPC marshaling
- **Input Validation & Sanitization**: Ensure all external inputs (Active Directory data, ntds.dit database content, network data from MS-DRSR/MS-SAMR protocols) are properly validated
  - Validate string lengths before buffer operations
  - Check array bounds before indexing
  - Validate enum values are within expected ranges
- **Path Traversal**: Check for potential path traversal vulnerabilities when handling file paths (especially for ntds.dit database files)
  - Use `Path.GetFullPath()` and validate paths are within expected directories
- **Injection Vulnerabilities**: Check for potential LDAP injection, command injection, or code injection risks
- **Authentication & Authorization**: Ensure proper handling of Kerberos, NTLM, and other AD authentication mechanisms
- **Information Disclosure**: Watch for accidental logging or exposure of sensitive data (password hashes, DPAPI keys, BitLocker recovery keys, certificates)
  - Ensure `ToString()` overrides don't expose sensitive fields
  - Check exception messages don't leak sensitive information
- **Deserialization**: Ensure safe deserialization practices, especially when parsing AD replication data or database records
- **Race Conditions**: Identify potential TOCTOU vulnerabilities, especially when accessing ntds.dit database files or AD replication streams
- **Exception Handling for Crypto**: Catch and handle `CryptographicException` appropriately; avoid leaking information about why decryption failed

### 2. Performance

**Performance Considerations:**
- **Algorithmic Complexity**: Identify inefficient algorithms, especially when processing large AD databases with millions of objects
- **Memory Allocations (C#)**: Watch for excessive allocations in hot paths, consider `Span<T>`, `stackalloc`, or object pooling where appropriate
- **Memory Management (C++/CLI)**: Ensure proper use of native/managed memory boundaries, avoid unnecessary marshaling overhead
- **Boxing**: Identify unnecessary boxing of value types (SIDs, GUIDs, timestamps)
- **String Operations**: Check for string concatenation in loops (use StringBuilder), especially when building distinguished names or LDAP filters
- **ESE/JET Database Access**: Ensure efficient cursor operations, proper index usage, and minimal database round-trips when querying ntds.dit
- **RPC Call Efficiency**: Optimize MS-DRSR/MS-SAMR RPC calls to minimize network round-trips
- **Collection Choices**: Verify appropriate collection types for access patterns (especially when handling SID histories, group memberships)
- **Lazy Initialization**: Check for opportunities to defer expensive operations (schema loading, database connections)
- **Native Interop**: Ensure P/Invoke and C++/CLI interop calls are efficient with minimal marshaling overhead
- **Async/Await Best Practices**:
  - Use `ConfigureAwait(false)` in library code (DSInternals Framework) to avoid deadlocks and improve performance
  - Avoid `async void` except for event handlers; use `async Task` instead
  - Support `CancellationToken` for long-running operations (database enumeration, RPC calls)
  - Prefer `ValueTask` over `Task` for hot paths that often complete synchronously
  - Avoid blocking on async code (no `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` in async contexts)
  - Don't use `Task.Run()` to wrap synchronous code in library methods; let the caller decide
  - Ensure proper exception handling in async methods (exceptions are captured in the returned Task)

### 3. Backwards Compatibility

**Compatibility Requirements:**
- **Public API Changes**: Any change to DSInternals Framework public APIs requires careful scrutiny
  - Breaking changes are generally not acceptable
  - New optional parameters, overloads, and interface implementations need careful consideration
  - Verify that API additions follow existing patterns and naming conventions
- **PowerShell Cmdlet Compatibility**: Ensure cmdlet parameter sets, output types, and behavior remain consistent
  - Parameter names and aliases must be preserved
  - Output object properties must maintain backwards compatibility
  - Pipeline behavior must be preserved
- **Multi-Framework Support**: Changes must work on both .NET Framework 4.8 and .NET 10.0
- **Serialization Compatibility**: Ensure changes don't break serialization of persisted data (e.g., exported credentials)
- **Behavioral Changes**: Even non-breaking changes can break consumers if behavior changes unexpectedly
  - Document behavioral changes clearly in CHANGELOG.md
- **Obsolete APIs**: Check that proper obsolescence process is followed (ObsoleteAttribute, documentation, migration path)

### 4. Cross-Component Interactions

**Integration Points:**
- **C#/C++/CLI Boundaries**: Verify proper marshaling of data structures between managed C# and C++/CLI code
  - Ensure correct memory ownership semantics
  - Check for proper exception handling across managed/native boundaries
- **PowerShell/Framework Boundaries**: Ensure clean separation between cmdlet implementations and underlying framework code
  - Cmdlets should be thin wrappers delegating to framework classes
  - Error handling should translate framework exceptions to appropriate PowerShell errors
- **RPC Protocol Interactions**: Verify correct implementation of MS-DRSR (replication) and MS-SAMR (SAM) protocols
  - Check for proper handling of protocol version differences
  - Ensure correct RPC stub memory allocation/deallocation
- **ESE/JET Database Access**: Ensure proper database session management, cursor handling, and transaction semantics
- **Threading Models**: Ensure thread-safety is maintained, especially for database connections and RPC handles
- **Lifecycle Management**: Verify proper initialization, disposal patterns (IDisposable), and cleanup across component boundaries
- **Error Handling**: Ensure exceptions and error codes are properly propagated across component boundaries

### 5. Correctness and Edge Cases

**Code Correctness:**
- **Null Handling**: While the compiler enforces nullable reference types, verify runtime null checks for AD data that may be absent
- **Boundary Conditions**: Test for empty collections, null attributes, maximum SID lengths, large group memberships
- **AD Data Variations**: Handle variations in AD data across different domain/forest functional levels and schema versions
- **Error Paths**: Ensure error handling is correct and complete; database cursors and RPC handles are properly cleaned up
- **Concurrency**: Identify race conditions, especially when accessing shared database connections
- **Exception Safety**: Verify operations maintain invariants even when exceptions occur (especially in C++/CLI code)
- **Resource Management**: Ensure IDisposable is implemented correctly; database sessions, RPC connections, and native handles are not leaked
- **Numeric Overflow**: Check for potential integer overflow when handling RID pools, USNs, or large object counts
- **Encoding Issues**: Verify proper handling of Unicode strings, UTF-8/UTF-16 conversions, and binary attribute data
- **Time Handling**: Check for proper handling of AD timestamps (FILETIME, GeneralizedTime, epoch differences)

### 6. Design and Architecture

**Design Quality:**
- **API Design**: Ensure new APIs are intuitive, follow .NET Framework Design Guidelines, and are hard to misuse
- **Abstraction Level**: Verify abstractions are at the appropriate level
  - Framework classes should not depend on PowerShell
  - Protocol implementations should be separate from data model classes
- **Separation of Concerns**: Check that responsibilities are properly separated
  - DSInternals.Common: Shared utilities and cryptographic functions
  - DSInternals.DataStore: Offline database access only
  - DSInternals.Replication: Protocol implementation only
  - DSInternals.PowerShell: Cmdlet implementations only
- **SOLID Principles**: Evaluate adherence to single responsibility, open/closed, and other design principles
- **Code Duplication**: Identify opportunities to reduce duplication between .NET Framework and .NET versions
- **Testability**: Ensure the code is designed to be testable (proper dependency injection, separation of concerns)

### 7. Testing

**Test Quality:**
- **Coverage**: Ensure new functionality has appropriate test coverage
  - C# unit tests using MSTest framework
  - PowerShell Pester tests for cmdlet behavior
- **Test Scenarios**: Verify tests cover happy paths, error paths, and edge cases
- **Test Data**: Ensure tests use appropriate test data (sample ntds.dit databases, mock AD objects)
- **Test Reliability**: Watch for flaky tests (timing dependencies, environmental assumptions)
- **Test Performance**: Ensure tests run efficiently and don't unnecessarily slow down CI
- **Regression Tests**: Check that bugs being fixed have corresponding regression tests

### 8. C++/CLI Specific Concerns

**Native Code Quality:**
- **Memory Safety**: Check for buffer overflows, use-after-free, double-free, and memory leaks
  - Prefer `std::unique_ptr` and `std::vector` over raw pointers and C-style arrays where possible
  - Use `SecureZeroMemory()` to clear sensitive data before freeing
- **RPC Memory Management**: Verify correct use of MIDL-generated allocation/deallocation functions
  - Match `MIDL_user_allocate()` with `MIDL_user_free()`
  - Check for proper cleanup in error paths
- **Exception Handling**: Ensure native exceptions are properly caught and converted to managed exceptions
  - Use SEH (`__try`/`__except`) for Win32 exceptions where appropriate
  - Never let native exceptions propagate to managed code unhandled
- **Resource Cleanup**: Verify proper cleanup of native handles, RPC bindings, and allocated buffers
- **Preprocessor Usage**: Check for proper use of conditional compilation for multi-platform/multi-framework builds
- **Header Dependencies**: Minimize header dependencies to improve build times

**C++/CLI Security:**
- **Buffer Security**: Ensure `/GS` (Buffer Security Check) compiler flag is enabled
- **Safe Integer Arithmetic**: Use `SafeInt<T>` or check for overflow before arithmetic operations on sizes/lengths
- **Secure CRT Functions**: Prefer `_s` suffixed functions (`strcpy_s`, `memcpy_s`, `sprintf_s`) over unsafe versions
- **Format String Safety**: Never pass user-controlled strings as format specifiers to `printf`-family functions
- **Stack Allocations**: Validate sizes before `_alloca()` or prefer heap allocation for variable-sized buffers
- **Uninitialized Memory**: Ensure all variables are initialized; uninitialized stack variables can leak sensitive data
- **Pointer Validation**: Check pointers for null before dereferencing, especially for RPC output parameters

### 9. PowerShell Specific Concerns

**Cmdlet Quality:**
- **Parameter Validation**: Ensure proper use of validation attributes (ValidateNotNull, ValidatePattern, etc.)
- **Pipeline Support**: Verify proper implementation of pipeline input (ValueFromPipeline, ValueFromPipelineByPropertyName)
- **Output Types**: Ensure cmdlets declare and return correct output types
- **Error Handling**: Use appropriate error types (terminating vs. non-terminating errors)
- **Help Documentation**: Ensure cmdlet help documentation is complete and accurate
- **Credential Handling**: Use SecureString for password parameters where appropriate
- **ShouldProcess**: Implement -WhatIf and -Confirm for cmdlets that modify data

### 10. Documentation and Code Clarity

**Documentation:**
- **XML Documentation**: New public APIs must have clear XML documentation explaining purpose, parameters, return values, and exceptions. Do not comment on existing APIs that lack documentation.
- **PowerShell Help**: Cmdlets should have comprehensive comment-based or external help (MAML)
- **Complex Logic**: Comments should explain the "why" behind non-obvious decisions, especially for AD protocol quirks or cryptographic operations
- **TODOs and FIXMEs**: Ensure they are tracked with issues and are appropriate for the change
- **Breaking Changes**: Must be clearly documented in CHANGELOG.md with migration guidance

## What NOT to Focus On

The following are handled by automated tooling and don't need review comments:

- Code formatting and style (handled by `.editorconfig` and analyzers)
- Naming convention violations (handled by analyzers)
- Missing using directives (handled by compiler)
- Most syntax errors (handled by compiler)
- Simple code style preferences without technical merit
- PowerShell script formatting (handled by PSScriptAnalyzer)

## Review Approach

1. **Understand the Context**: Read the PR description and linked issues to understand the goal. Consider as much relevant code from the containing project as possible. For public APIs, review any code in the repo that consumes the method.
2. **Assess the Scope**: Verify the change is focused and not mixing unrelated concerns
3. **Evaluate Risk**: Consider the risk level based on what components are affected
   - DSInternals.Common changes affect all other components
   - C++/CLI changes may have memory safety implications
   - PowerShell changes may affect user-facing behavior
4. **Think Like an Attacker**: For security-sensitive code (credential handling, crypto, protocol implementations), consider how it might be exploited
5. **Think Like a Consumer**: Consider how the API or cmdlet will be used by security researchers and AD administrators
6. **Consider Maintenance**: Think about long-term maintenance burden, especially for multi-framework targeting

## Severity Guidelines

- **Critical**: Security vulnerabilities (credential exposure, buffer overflows), data corruption, crashes, breaking changes
- **High**: Memory leaks in C++/CLI, performance regressions, incorrect AD data handling, resource leaks
- **Medium**: Edge case bugs, suboptimal design, missing documentation, PowerShell parameter issues
- **Low**: Code clarity issues, minor inefficiencies, nice-to-have improvements
