using System.Linq;

namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

internal partial struct CombinerRecipientInfo
{
    /// <summary>
    /// Gets the descriptor represented by an AND combiner recipient.
    /// </summary>
    /// <value>The child recipient descriptors joined by the capitalized <c>AND</c> operator.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// </example>
    internal string? Descriptor =>
        ProtectionDescriptorFormatter.FormatAnd(
            this.RecipientInfos?.Select(static recipientInfo => recipientInfo.Descriptor));
}
