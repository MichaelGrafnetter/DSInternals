namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

internal partial struct DescriptorItem
{
    /// <summary>
    /// Gets a single provider assignment from a protection descriptor.
    /// </summary>
    /// <value>The descriptor item formatted as <c>providerName=providerAttribute</c>.</value>
    /// <example>
    /// <code language="text">SID=S-1-5-21-4392301</code>
    /// <code language="text">LOCAL=machine</code>
    /// </example>
    internal string Descriptor => ProtectionDescriptorFormatter.FormatProvider(this.Name, this.Value);
}
