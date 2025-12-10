namespace DSInternals.Common.Data;

public enum DnsSigningKeyScope : uint
{
    /// <summary>
    /// The key is used for its default purpose.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_DEFAULT.
    /// </remarks>
    Default = 0x00000000,

    /// <summary>
    /// The key is used to sign only DNSKEY records in the zone.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_DNSKEY_ONLY.
    /// </remarks>
    SignDnsKeyRecords = 0x00000001,

    /// <summary>
    /// The key is used to sign all records in the zone.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_ALL_RECORDS.
    /// </remarks>
    SignAllRecords = 0x00000002,

    /// <summary>
    /// The key is published as a DNSKEY in the zone, but it is not used to sign any records.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_ADD_ONLY.
    /// </remarks>
    AddOnly = 0x00000003,

    /// <summary>
    /// The key is not published to the zone and is not used to sign any records.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_DO_NOT_PUBLISH.
    /// </remarks>
    DoNotPublish = 0x00000004,

    /// <summary>
    /// The key is published as a DNSKEY in the zone with its "Revoked" bit ([RFC5011] section 2.1) set. It is used to sign DNSKEY records.
    /// </summary>
    /// <remarks>
    /// Corresponds to DNS_SIGN_SCOPE_REVOKED.
    /// </remarks>
    Revoked = 0x00000005
}
