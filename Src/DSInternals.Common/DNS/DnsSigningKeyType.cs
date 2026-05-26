namespace DSInternals.Common.DNS;

public enum DnsSigningKeyType : uint
{
    /// <summary>
    /// Zone Signing Key (ZSK).
    /// </summary>
    ZoneSigningKey = 0,

    /// <summary>
    /// Key Signing Key (KSK).
    /// </summary>
    KeySigningKey = 1
}
