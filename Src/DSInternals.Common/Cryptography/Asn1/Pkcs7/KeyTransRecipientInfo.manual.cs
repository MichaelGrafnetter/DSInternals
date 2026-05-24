namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct KeyTransRecipientInfo
{
    /// <summary>
    /// Gets the certificate descriptor represented by a key transport recipient.
    /// </summary>
    /// <value>The formatted certificate protection descriptor.</value>
    /// <example>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal string? Descriptor => this.Rid.Descriptor;
}
