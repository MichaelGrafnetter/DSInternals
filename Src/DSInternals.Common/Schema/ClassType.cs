namespace DSInternals.Common.Schema
{
    /// <summary>
    /// Any OID-valued quantity is stored as a 32-bit unsigned integer. 
    /// </summary>
    using ATTRTYP = uint;

    /// <summary>
    /// Compressed representation of a schema class OIDs (subset of ATTRTYP).
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/98b55783-7029-4a04-8f8b-9df9344089c3</remarks>
    public enum ClassType : ATTRTYP
    {
        /// <summary>
        /// classSchema
        /// </summary>
        ClassSchema = 196621,

        /// <summary>
        /// attributeSchema
        /// </summary>
        AttributeSchema = 196622,

        /// <summary>
        /// secret
        /// </summary>
        Secret = 655388,

        /// <summary>
        /// user
        /// </summary>
        User = 655369,

        /// <summary>
        /// computer
        /// </summary>
        Computer = 196638,

        /// <summary>
        /// group
        /// </summary>
        Group = 655368,

        /// <summary>
        /// contact
        /// </summary>
        Contact = 655375,

        /// <summary>
        /// dMD
        /// </summary>
        Schema = 196617,

        /// <summary>
        /// msKds-ProvRootKey
        /// </summary>
        KdsRootKey = 655638,

        /// <summary>
        /// msDS-GroupManagedServiceAccount
        /// </summary>
        GroupManagedServiceAccount = 655642,

        /// <summary>
        /// msDS-DelegatedManagedServiceAccount
        /// </summary>
        DelegatedManagedServiceAccount = 655662,

        /// <summary>
        /// msFVE-RecoveryInformation
        /// </summary>
        FVERecoveryInformation = 655613,

        /// <summary>
        /// dnsZone
        /// </summary>
        DnsZone = 655445,

        /// <summary>
        /// dnsNode
        /// </summary>
        DnsNode = 655446,

        /// <summary>
        /// nTDSDSA
        /// </summary>
        NtdsSettings = 1507375,

        /// <summary>
        /// nTDSDSARO
        /// </summary>
        NtdsSettingsRO = 655614,

        /// <summary>
        /// ou
        /// </summary>
        OrganizationalUnit = 65541,

        /// <summary>
        /// container
        /// </summary>
        Container = 196631,

        /// <summary>
        /// domain
        /// </summary>
        Domain = 655426,

        /// <summary>
        /// trustedDomain
        /// </summary>
        TrustedDomain = 655394,

        /// <summary>
        /// configuration
        /// </summary>
        Configuration = 655372,

        /// <summary>
        /// top
        /// </summary>
        Top = 65536
    }
}
