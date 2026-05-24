using System.Formats.Asn1;
using DSInternals.Common.Cryptography.Asn1.DpapiNg;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct OtherRecipientInfo
{
    /// <summary>
    /// Gets the protection descriptor represented by an other recipient.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301 AND SID=S-1-5-21-3101812</code>
    /// <code language="text">DRACERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal string? Descriptor
    {
        get
        {
            if (this.OriType == DpapiNgConstants.AndCombinerProtected)
            {
                var combiner = CombinerRecipientInfo.Decode(this.OriValue, AsnEncodingRules.DER);
                return combiner.Descriptor;
            }

            if (this.OriType == DpapiNgConstants.DraCertificateProtected)
            {
                var recipientInfo = RecipientInfo.Decode(this.OriValue, AsnEncodingRules.DER);
                return ProtectionDescriptorFormatter.RebindProvider(
                    recipientInfo.Descriptor,
                    DpapiNgConstants.CertificateName,
                    DpapiNgConstants.DraCertificateName);
            }

            return null;
        }
    }
}
