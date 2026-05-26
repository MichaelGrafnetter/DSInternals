namespace DSInternals.Common.DNS;

/// <summary>
/// Describes the outcome of an attempt to decrypt a DNSSEC signing key's private key material.
/// </summary>
public enum DnsDecryptionStatus
{
    /// <summary>
    /// Decryption has not been attempted (no KDS root key resolver was supplied).
    /// </summary>
    NotAttempted,

    /// <summary>
    /// The private key was decrypted successfully.
    /// </summary>
    Success,

    /// <summary>
    /// The KDS root key required to derive the group key was not available to the resolver.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// Decryption failed unexpectedly.
    /// </summary>
    Error
}
