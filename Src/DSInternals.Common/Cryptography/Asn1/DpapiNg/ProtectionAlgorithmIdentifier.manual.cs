namespace DSInternals.Common.Cryptography.Asn1.DpapiNg;

internal partial struct ProtectionAlgorithmIdentifier
{
    /// <summary>
    /// Gets the protection descriptor carried by the protection algorithm parameters.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">SDDL=O:S-1-5-5-0-290724G:SYD:(A;;CCDC;;;S-1-5-5-0-290724)(A;;DC;;;WD)</code>
    /// </example>
    internal string? Descriptor => this.Parameters.Descriptor;
}
