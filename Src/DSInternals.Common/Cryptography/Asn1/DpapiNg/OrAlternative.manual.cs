using System.Linq;

namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

internal partial struct OrAlternative
{
    /// <summary>
    /// Gets an AND-combined protection descriptor alternative.
    /// </summary>
    /// <value>The descriptor items joined by the capitalized <c>AND</c> operator.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// </example>
    internal string? Descriptor =>
        ProtectionDescriptorFormatter.FormatAnd(
            this.Values?.Select(static item => item.Descriptor));
}
