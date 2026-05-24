namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct RecipientEncryptedKey
{
    /// <summary>
    /// Gets the protection descriptor represented by a recipient encrypted key.
    /// </summary>
    /// <value>The formatted certificate protection descriptor.</value>
    /// <example>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal string? Descriptor => this.Rid.Descriptor;
}
