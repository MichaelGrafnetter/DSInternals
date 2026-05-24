namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct KEKIdentifier
{
    /// <summary>
    /// Gets the protection descriptor carried in the other key attribute.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301</code>
    /// </example>
    internal string? Descriptor => this.Other.HasValue ? this.Other.Value.Descriptor : null;
}
