using System;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies DNS record flags. 
    /// </summary>
    [Flags]
    public enum ResourceRecordFlags : ushort
    {
        /// <summary>
        /// No flags are set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The record is at the root of a zone (not necessarily a zone hosted by this server; the record could have come from the cache). (DNS_RPC_FLAG_ZONE_ROOT)
        /// </summary>
        ZoneRoot = 0x4000,

        /// <summary>
        /// The record is at the root of a zone that is locally hosted on this server. (DNS_RPC_FLAG_AUTH_ZONE_ROOT)
        /// </summary>
        AuthoritativeZoneRoot = 0x2000,

        /// <summary>
        /// The record came from the cache. (DNS_RPC_FLAG_CACHE_DATA)
        /// </summary>
        CacheData = 0x8000,

        /// <summary>
        /// The record SHOULD be treated as a resource record of unknown type ([RFC3597] section 2) by the DNS server. (DNS_RPC_FLAG_RECORD_WIRE_FORMAT)
        /// </summary>
        RecordWireFormat = 0x0010
    }
}
