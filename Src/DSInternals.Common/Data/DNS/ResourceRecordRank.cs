using System;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies DNS record rank. 
    /// </summary>
    [Flags]
    public enum ResourceRecordRank : byte
    {
        /// <summary>
        /// No flags are set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The record came from the cache. (RANK_CACHE_BIT)
        /// </summary>
        Cache = 0x00000001,

        /// <summary>
        /// The record is a preconfigured root hint. (RANK_ROOT_HINT)
        /// </summary>
        RootHint = 0x00000008,

        /// <summary>
        /// This value is not used. (RANK_OUTSIDE_GLUE)
        /// </summary>
        OutsideGlue = 0x00000020,

        /// <summary>
        /// The record was cached from the additional section of a non-authoritative response. (RANK_CACHE_NA_ADDITIONAL)
        /// </summary>
        CacheNonAuthoritativeAdditional = 0x00000031,

        /// <summary>
        /// The record was cached from the authority section of a non-authoritative response. (RANK_CACHE_NA_AUTHORITY)
        /// </summary>
        CacheNonAuthoritative = 0x00000041,

        /// <summary>
        /// The record was cached from the additional section of an authoritative response. (RANK_CACHE_A_ADDITIONAL)
        /// </summary>
        CacheAuthoritativeAdditional = 0x00000051,

        /// <summary>
        /// The record was cached from the answer section of a non-authoritative response. (RANK_CACHE_NA_ANSWER)
        /// </summary>
        CacheNonAuthoritativeAnswer = 0x00000061,

        /// <summary>
        /// The record was cached from the authority section of an authoritative response. (RANK_CACHE_A_AUTHORITY)
        /// </summary>
        CacheAuthoritative = 0x00000071,

        /// <summary>
        /// The record is a glue record in an authoritative zone. (RANK_GLUE)
        /// </summary>
        Glue = 0x00000080,

        /// <summary>
        /// The record is a delegation (type NS) record in an authoritative zone. (RANK_NS_GLUE)
        /// </summary>
        NameServerGlue = 0x00000082,

        /// <summary>
        /// The record was cached from the answer section of an authoritative response. (RANK_CACHE_A_ANSWER)
        /// </summary>
        CacheAuthoritativeAnswer = 0x000000C1,

        /// <summary>
        /// The record comes from an authoritative zone. (RANK_ZONE)
        /// </summary>
        Zone = 0x000000F0
    }
}
