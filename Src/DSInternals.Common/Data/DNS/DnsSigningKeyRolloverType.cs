namespace DSInternals.Common.Data;

/// <summary>
/// Specifies possible key rollover types for a signing key descriptor.
/// </summary>
public enum DnsSigningKeyRolloverType : uint
{
    /// <summary>
    /// A method of key rollover in which the new key is published in the zone before it will be used.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_TYPE_PREPUBLISH.
    /// </remarks>
    Prepublish = 0x00000000,

    /// <summary>
    /// A method of key rollover in which data is signed by both old and new keys simultaneously for a period of time.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_ROLLOVER_TYPE_DOUBLE_SIGNATURE.
    /// </remarks>
    DoubleSignature = 0x00000001
}
