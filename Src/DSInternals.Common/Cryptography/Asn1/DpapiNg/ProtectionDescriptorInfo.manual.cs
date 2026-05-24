using System.Linq;

namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

internal partial struct ProtectionDescriptorInfo
{
    /// <summary>
    /// Gets a complete protection descriptor rule string.
    /// </summary>
    /// <value>The descriptor alternatives joined by the capitalized <c>OR</c> operator.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 OR LOCAL=machine</code>
    /// </example>
    internal string? Descriptor =>
        ProtectionDescriptorFormatter.FormatOr(
            this.Values?.Select(static alternative => alternative.Descriptor));
}
