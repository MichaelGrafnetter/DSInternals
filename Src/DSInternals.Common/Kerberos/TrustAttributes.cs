using Windows.Win32;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Information about a trust relartnship between two domains or forests.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/e9a2d23c-c31e-4a6f-88a0-6646fdb51a3c</remarks>
[Flags]
public enum TrustAttributes : uint
{
    None = 0,

    /// <summary>
    /// The trust cannot be used transitively.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_NON_TRANSITIVE</remarks>
    DisallowTransivity = 0x00000001,

    /// <summary>
    /// Only Windows 2000 operating system and newer clients can use the trust link.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_UPLEVEL_ONLY</remarks>
    UplevelOnly = 0x00000002,

    /// <summary>
    /// The trusted domain is quarantined and is subject to the rules of SID Filtering.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_QUARANTINED_DOMAIN</remarks>
    SIDFilteringQuarantined = 0x00000004,

    /// <summary>
    /// Trust link is a cross-forest trust between the root domains of two forests.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_FOREST_TRANSITIVE</remarks>
    ForestTransitive = 0x00000008,

    /// <summary>
    /// The trust is to a domain or forest that is not part of the organization.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_CROSS_ORGANIZATION</remarks>
    CrossOrganization = 0x00000010,

    /// <summary>
    /// The trusted domain is within the same forest.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_WITHIN_FOREST</remarks>
    IntraForest = 0x00000020,

    /// <summary>
    /// A cross-forest trust to a domain is to be treated as an external trust for the purposes of SID Filtering.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_TREAT_AS_EXTERNAL</remarks>
    External = 0x00000040,

    /// <summary>
    /// The MIT realm trust is capable of using RC4 keys. Historically, MIT Kerberos distributions supported only DES and 3DES keys.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_USES_RC4_ENCRYPTION</remarks>
    UsesRC4Encryption = PInvoke.TRUST_ATTRIBUTE_TRUST_USES_RC4_ENCRYPTION,

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_TRUST_USES_AES_KEYS</remarks>
    UsesAESKeys = PInvoke.TRUST_ATTRIBUTE_TRUST_USES_AES_KEYS,

    /// <summary>
    /// Tickets granted under this trust MUST NOT be trusted for delegation.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_CROSS_ORGANIZATION_NO_TGT_DELEGATION</remarks>
    DisallowTGTDelegation = PInvoke.TRUST_ATTRIBUTE_CROSS_ORGANIZATION_NO_TGT_DELEGATION,

    /// <summary>
    /// Tickets granted under this trust MUST be trusted for delegation.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_CROSS_ORGANIZATION_ENABLE_TGT_DELEGATION</remarks>
    EnableTGTDelegation = PInvoke.TRUST_ATTRIBUTE_CROSS_ORGANIZATION_ENABLE_TGT_DELEGATION,

    /// <summary>
    /// A cross-forest trust to a domain is to be treated as Privileged Identity Management trust for the purposes of SID Filtering.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_PIM_TRUST</remarks>
    PrivilegedIdentityManagementTrust = PInvoke.TRUST_ATTRIBUTE_PIM_TRUST,

    /// <summary>
    /// The domain name validation during NTLM pass-through authentication is disabled.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_DISABLE_AUTH_TARGET_VALIDATION</remarks>
    DisableAuthTargetValidation = PInvoke.TRUST_ATTRIBUTE_DISABLE_AUTH_TARGET_VALIDATION,

    /// <summary>
    /// Trust set to another tree root in the forest.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_TREE_ROOT</remarks>
    IsTreeRoot = PInvoke.TRUST_ATTRIBUTE_TREE_ROOT,

    /// <summary>
    /// Trust is set to the organization tree parent.
    /// </summary>
    /// <remarks>TRUST_ATTRIBUTE_TREE_PARENT</remarks>
    IsTreeParent = PInvoke.TRUST_ATTRIBUTE_TREE_PARENT
}
