namespace DSInternals.Common.Data;

/// <summary>
/// Describes how the key transported by a <see cref="GroupKeyEnvelope"/> can be used.
/// These values correspond to the bits of the <c>dwFlags</c> field of the MS-GKDI Group Key Envelope structure.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-gkdi/192c061c-e740-4aa0-ab1d-6954fb3e58f7</remarks>
[Flags]
public enum GroupKeyEnvelopeFlags : int
{
    /// <summary>
    /// No flags are set. The envelope transports the L1 and L2 seed keys, which may only be used for decryption.
    /// </summary>
    PrivateAsymmetricKey = 0,

    /// <summary>
    /// The envelope transports a group public key instead of the L1 and L2 seed keys.
    /// Corresponds to bit 31 (the least significant bit) of the <c>dwFlags</c> field.
    /// </summary>
    PublicAsymmetricKey = 0x1,

    /// <summary>
    /// The transported key may be used for both encryption and decryption; otherwise it may only be used for decryption.
    /// Corresponds to bit 30 of the <c>dwFlags</c> field.
    /// </summary>
    SymmetricKey = 0x2
}
