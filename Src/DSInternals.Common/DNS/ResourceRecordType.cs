using Windows.Win32.NetworkManagement.Dns;

namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies DNS record types that can be enumerated by the DNS server.
/// </summary>
public enum ResourceRecordType : ushort
{
    /// <summary>
    /// Represents an empty DNS record type.
    /// </summary>
    ZERO = DNS_TYPE.DNS_TYPE_ZERO,

    /// <summary>
    /// Represents a host information record.
    /// </summary>
    HINFO = DNS_TYPE.DNS_TYPE_HINFO,

    /// <summary>
    /// Represents an AFS database location record.
    /// </summary>
    AFSDB = DNS_TYPE.DNS_TYPE_AFSDB,

    /// <summary>
    /// Represents an Asynchronous Transfer Mode address record.
    /// </summary>
    ATMA = DNS_TYPE.DNS_TYPE_ATMA,

    /// <summary>
    /// Represents an ISDN address record.
    /// </summary>
    ISDN = DNS_TYPE.DNS_TYPE_ISDN,

    /// <summary>
    /// Represents a DNSSEC public key record.
    /// </summary>
    KEY = DNS_TYPE.DNS_TYPE_KEY,

    /// <summary>
    /// Represents a mailbox record.
    /// </summary>
    MB = DNS_TYPE.DNS_TYPE_MB,

    /// <summary>
    /// Represents a mail destination record.
    /// </summary>
    MD = DNS_TYPE.DNS_TYPE_MD,

    /// <summary>
    /// Represents a mail forwarder record.
    /// </summary>
    MF = DNS_TYPE.DNS_TYPE_MF,

    /// <summary>
    /// Represents a mail group member record.
    /// </summary>
    MG = DNS_TYPE.DNS_TYPE_MG,

    /// <summary>
    /// Represents a mailbox or mailing list information record.
    /// </summary>
    MINFO = DNS_TYPE.DNS_TYPE_MINFO,

    /// <summary>
    /// Represents a mail rename record.
    /// </summary>
    MR = DNS_TYPE.DNS_TYPE_MR,

    /// <summary>
    /// Represents a null resource record.
    /// </summary>
    NULL = DNS_TYPE.DNS_TYPE_NULL,

    /// <summary>
    /// Represents a mail exchanger record.
    /// </summary>
    MX = DNS_TYPE.DNS_TYPE_MX,

    /// <summary>
    /// Represents a next domain record.
    /// </summary>
    NXT = DNS_TYPE.DNS_TYPE_NXT,

    /// <summary>
    /// Represents a responsible person record.
    /// </summary>
    RP = DNS_TYPE.DNS_TYPE_RP,

    /// <summary>
    /// Represents a route through record.
    /// </summary>
    RT = DNS_TYPE.DNS_TYPE_RT,

    /// <summary>
    /// Represents a cryptographic signature record.
    /// </summary>
    SIG = DNS_TYPE.DNS_TYPE_SIG,

    /// <summary>
    /// Represents a well-known service record.
    /// </summary>
    WKS = DNS_TYPE.DNS_TYPE_WKS,

    /// <summary>
    /// Represents an X.25 PSDN address record.
    /// </summary>
    X25 = DNS_TYPE.DNS_TYPE_X25,

    /// <summary>
    /// Represents an IPv4 address record.
    /// </summary>
    A = DNS_TYPE.DNS_TYPE_A,

    /// <summary>
    /// Represents an IPv6 address record.
    /// </summary>
    AAAA = DNS_TYPE.DNS_TYPE_AAAA,

    /// <summary>
    /// Represents a canonical name record.
    /// </summary>
    CNAME = DNS_TYPE.DNS_TYPE_CNAME,

    /// <summary>
    /// Represents a domain name pointer record.
    /// </summary>
    PTR = DNS_TYPE.DNS_TYPE_PTR,

    /// <summary>
    /// Represents a server selection record.
    /// </summary>
    SRV = DNS_TYPE.DNS_TYPE_SRV,

    /// <summary>
    /// Represents a text string record.
    /// </summary>
    TXT = DNS_TYPE.DNS_TYPE_TEXT,

    /// <summary>
    /// Represents a Windows Internet Name Service forward lookup record.
    /// </summary>
    WINS = DNS_TYPE.DNS_TYPE_WINS,

    /// <summary>
    /// Represents a Windows Internet Name Service reverse lookup record.
    /// </summary>
    WINSR = DNS_TYPE.DNS_TYPE_WINSR,

    /// <summary>
    /// Represents an authoritative name server record.
    /// </summary>
    NS = DNS_TYPE.DNS_TYPE_NS,

    /// <summary>
    /// Represents a start of authority record.
    /// </summary>
    SOA = DNS_TYPE.DNS_TYPE_SOA,

    /// <summary>
    /// Represents a delegation name record.
    /// </summary>
    DNAME = DNS_TYPE.DNS_TYPE_DNAME,

    /// <summary>
    /// Represents a geographical position record.
    /// </summary>
    GPOS = DNS_TYPE.DNS_TYPE_GPOS,

    /// <summary>
    /// Represents a location information record.
    /// </summary>
    LOC = DNS_TYPE.DNS_TYPE_LOC,

    /// <summary>
    /// Represents a DHCP identifier record.
    /// </summary>
    DHCID = DNS_TYPE.DNS_TYPE_DHCID,

    /// <summary>
    /// Represents a naming authority pointer record.
    /// </summary>
    NAPTR = DNS_TYPE.DNS_TYPE_NAPTR,

    /// <summary>
    /// Represents a DNSSEC resource record signature.
    /// </summary>
    RRSIG = DNS_TYPE.DNS_TYPE_RRSIG,

    /// <summary>
    /// Represents a DNSSEC public key record.
    /// </summary>
    DNSKEY = DNS_TYPE.DNS_TYPE_DNSKEY,

    /// <summary>
    /// Represents a delegation signer record.
    /// </summary>
    DS = DNS_TYPE.DNS_TYPE_DS,

    /// <summary>
    /// Represents a next secure record.
    /// </summary>
    NSEC = DNS_TYPE.DNS_TYPE_NSEC,

    /// <summary>
    /// Represents a next secure version 3 record.
    /// </summary>
    NSEC3 = DNS_TYPE.DNS_TYPE_NSEC3,

    /// <summary>
    /// Represents a next secure version 3 parameters record.
    /// </summary>
    NSEC3PARAM = DNS_TYPE.DNS_TYPE_NSEC3PARAM,

    /// <summary>
    /// Represents a TLSA certificate association record.
    /// </summary>
    TLSA = DNS_TYPE.DNS_TYPE_TLSA,

    /// <summary>
    /// Represents a certificate record.
    /// </summary>
    CERT = DNS_TYPE.DNS_TYPE_CERT,

    /// <summary>
    /// Represents a transaction key record.
    /// </summary>
    TKEY = DNS_TYPE.DNS_TYPE_TKEY,

    /// <summary>
    /// Represents a transaction signature record.
    /// </summary>
    TSIG = DNS_TYPE.DNS_TYPE_TSIG,

    /// <summary>
    /// Represents a Certification Authority Authorization record.
    /// </summary>
    CAA = 257,

    /// <summary>
    /// Represents an SSH key fingerprint record.
    /// </summary>
    SSHFP = 44,

    /// <summary>
    /// Represents a general-purpose service binding record.
    /// </summary>
    SVCB = DNS_TYPE.DNS_TYPE_SVCB,

    /// <summary>
    /// Represents an HTTPS service binding record.
    /// </summary>
    HTTPS = DNS_TYPE.DNS_TYPE_HTTPS
}
