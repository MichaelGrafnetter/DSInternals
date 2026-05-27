namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct KEKRecipientInfo
{
    /// <summary>
    /// Gets the protection descriptor represented by a KEK recipient.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">LOCAL=machine</code>
    /// </example>
    internal string? Descriptor => this.KekId.Descriptor;
}
