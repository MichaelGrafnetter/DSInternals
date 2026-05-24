using System.Linq;
using DSInternals.Common.Cryptography.Asn1.DpapiNg;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct KeyAgreeRecipientInfo
{
    /// <summary>
    /// Gets the protection descriptor represented by key agreement recipients.
    /// </summary>
    /// <value>The recipient encrypted key descriptors joined by the capitalized <c>OR</c> operator.</value>
    /// <example>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD OR CERTIFICATE=HashID:00112233445566778899AABBCCDDEEFF00112233</code>
    /// </example>
    internal string? Descriptor =>
        ProtectionDescriptorFormatter.FormatOr(
            this.RecipientEncryptedKeys?.Select(static encryptedKey => encryptedKey.Descriptor));
}
