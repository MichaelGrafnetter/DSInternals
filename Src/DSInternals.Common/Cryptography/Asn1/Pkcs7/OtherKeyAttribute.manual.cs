using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Principal;
using DSInternals.Common.Cryptography.Asn1.DpapiNg;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs7;

/// <summary>
/// Adds DPAPI NG support for OtherKeyAttribute.
/// </summary>
partial struct OtherKeyAttribute
{
    internal ProtectionAlgorithmIdentifier? ProtectionAlgorithm
    {
        get
        {
            if (this.AttributeId != DpapiNgConstants.ProtectionInfo || this.AttributeValue == null)
            {
                return null;
            }

            return ProtectionAlgorithmIdentifier.Decode(this.AttributeValue.Value, AsnEncodingRules.DER);
        }
    }

    /// <summary>
    /// Gets the protection descriptor carried by this DPAPI-NG key attribute.
    /// </summary>
    /// <value>The formatted protection descriptor rule string.</value>
    /// <example>
    /// <code language="text">WEBCREDENTIALS=MyPasswordName,myweb.com</code>
    /// </example>
    internal string? Descriptor
    {
        get
        {
            var protectionAlgorithm = this.ProtectionAlgorithm;
            return protectionAlgorithm.HasValue ? protectionAlgorithm.Value.Descriptor : null;
        }
    }

    /// <summary>
    /// Gets the SID carried by a SID-based protection descriptor.
    /// </summary>
    /// <value>The target SID, or <see langword="null" /> when the descriptor is not SID-based.</value>
    public SecurityIdentifier? SidProtector
    {
        get
        {
            var protectionAlgorithm = this.ProtectionAlgorithm;

            if (!protectionAlgorithm.HasValue ||
                protectionAlgorithm.Value.Algorithm != DpapiNgConstants.SidProtected)
            {
                return null;
            }

            if (!TryGetFirstValue(protectionAlgorithm.Value.Parameters, DpapiNgConstants.SidName, out string? sidString))
            {
                throw new CryptographicException($"Missing {DpapiNgConstants.SidName} descriptor item.");
            }

            return new SecurityIdentifier(sidString!);
        }
    }

    /// <summary>
    /// Gets the SDDL string carried by an SDDL-based protection descriptor.
    /// </summary>
    /// <value>The target SDDL string, or <see langword="null" /> when the descriptor is not SDDL-based.</value>
    public string? SddlProtector
    {
        get
        {
            var protectionAlgorithm = this.ProtectionAlgorithm;

            if (!protectionAlgorithm.HasValue ||
                protectionAlgorithm.Value.Algorithm != DpapiNgConstants.SddlProtected)
            {
                return null;
            }

            if (!TryGetFirstValue(protectionAlgorithm.Value.Parameters, DpapiNgConstants.SddlName, out string? sddlString))
            {
                throw new CryptographicException($"Missing {DpapiNgConstants.SddlName} descriptor item.");
            }

            return sddlString;
        }
    }

    private static bool TryGetFirstValue(ProtectionDescriptorInfo descriptorInfo, string name, out string? value)
    {
        if (descriptorInfo.Values != null)
        {
            foreach (var alternative in descriptorInfo.Values)
            {
                if (alternative.Values == null)
                {
                    continue;
                }

                foreach (var item in alternative.Values)
                {
                    if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        value = item.Value;
                        return true;
                    }
                }
            }
        }

        value = null;
        return false;
    }
}
