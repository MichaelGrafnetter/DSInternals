using DSInternals.Common.Cryptography.Asn1.DpapiNg;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct RecipientIdentifier
{
    /// <summary>
    /// Gets the certificate descriptor represented by this recipient identifier.
    /// </summary>
    /// <value>The formatted certificate protection descriptor.</value>
    /// <example>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal string? Descriptor
    {
        get
        {
            if (this.SubjectKeyIdentifier.HasValue)
            {
                return ProtectionDescriptorFormatter.FormatCertificateHash(this.SubjectKeyIdentifier.Value.Span);
            }

            return null;
        }
    }
}
