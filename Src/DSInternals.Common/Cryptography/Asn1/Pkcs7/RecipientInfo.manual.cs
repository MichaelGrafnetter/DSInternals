namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

internal partial struct RecipientInfo
{
    /// <summary>
    /// Gets the protection descriptor represented by this CMS recipient.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301</code>
    /// <code language="text">CERTIFICATE=HashID:5BD6F6AF8477755554026221BC81D1F3DAF5C4CD</code>
    /// </example>
    internal string? Descriptor
    {
        get
        {
            if (this.KEKRecipientInfo.HasValue)
            {
                return this.KEKRecipientInfo.Value.Descriptor;
            }

            if (this.OtherRecipientInfo.HasValue)
            {
                return this.OtherRecipientInfo.Value.Descriptor;
            }

            if (this.KeyTransRecipientInfo.HasValue)
            {
                return this.KeyTransRecipientInfo.Value.Descriptor;
            }

            if (this.KeyAgreeRecipientInfo.HasValue)
            {
                return this.KeyAgreeRecipientInfo.Value.Descriptor;
            }

            return null;
        }
    }
}
