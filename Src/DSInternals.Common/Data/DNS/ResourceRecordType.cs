using Windows.Win32.NetworkManagement.Dns;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies DNS record types that can be enumerated by the DNS server. 
    /// </summary>
    public enum ResourceRecordType : ushort
    {
        ZERO = DNS_TYPE.DNS_TYPE_ZERO,
        HINFO = DNS_TYPE.DNS_TYPE_HINFO,
        AFSDB = DNS_TYPE.DNS_TYPE_AFSDB,
        ATMA = DNS_TYPE.DNS_TYPE_ATMA,
        ISDN = DNS_TYPE.DNS_TYPE_ISDN,
        KEY = DNS_TYPE.DNS_TYPE_KEY,
        MB = DNS_TYPE.DNS_TYPE_MB,
        MD = DNS_TYPE.DNS_TYPE_MD,
        MF = DNS_TYPE.DNS_TYPE_MF,
        MG = DNS_TYPE.DNS_TYPE_MG,
        MINFO = DNS_TYPE.DNS_TYPE_MINFO,
        MR = DNS_TYPE.DNS_TYPE_MR,
        MX = DNS_TYPE.DNS_TYPE_MX,
        NXT = DNS_TYPE.DNS_TYPE_NXT,
        RP = DNS_TYPE.DNS_TYPE_RP,
        RT = DNS_TYPE.DNS_TYPE_RT,
        WKS = DNS_TYPE.DNS_TYPE_WKS,
        X25 = DNS_TYPE.DNS_TYPE_X25,
        A = DNS_TYPE.DNS_TYPE_A,
        AAAA = DNS_TYPE.DNS_TYPE_AAAA,
        CNAME = DNS_TYPE.DNS_TYPE_CNAME,
        PTR = DNS_TYPE.DNS_TYPE_PTR,
        SRV = DNS_TYPE.DNS_TYPE_SRV,
        TXT = DNS_TYPE.DNS_TYPE_TEXT,
        WINS = DNS_TYPE.DNS_TYPE_WINS,
        WINSR = DNS_TYPE.DNS_TYPE_WINSR,
        NS = DNS_TYPE.DNS_TYPE_NS,
        SOA = DNS_TYPE.DNS_TYPE_SOA,
        DNAME = DNS_TYPE.DNS_TYPE_DNAME,
        GPOS = DNS_TYPE.DNS_TYPE_GPOS,
        LOC = DNS_TYPE.DNS_TYPE_LOC,
        DHCID = DNS_TYPE.DNS_TYPE_DHCID,
        NAPTR = DNS_TYPE.DNS_TYPE_NAPTR,
        RRSIG = DNS_TYPE.DNS_TYPE_RRSIG,
        DNSKEY = DNS_TYPE.DNS_TYPE_DNSKEY,
        DS = DNS_TYPE.DNS_TYPE_DS,
        NSEC = DNS_TYPE.DNS_TYPE_NSEC,
        NSEC3 = DNS_TYPE.DNS_TYPE_NSEC3,
        NSEC3PARAM = DNS_TYPE.DNS_TYPE_NSEC3PARAM,
        TLSA = DNS_TYPE.DNS_TYPE_TLSA,
        CERT = DNS_TYPE.DNS_TYPE_CERT,
        TKEY = DNS_TYPE.DNS_TYPE_TKEY,
        TSIG = DNS_TYPE.DNS_TYPE_TSIG,
        CAA = 257,
        SSHFP = 44
    }
}
