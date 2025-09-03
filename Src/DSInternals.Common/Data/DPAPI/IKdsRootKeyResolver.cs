using System;
using System.Collections.Generic;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Defines the contract for IKdsRootKeyResolver.
    /// </summary>
    public interface IKdsRootKeyResolver
    {
        /// <summary>
        /// GetKdsRootKey implementation.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(Guid id);
        /// <summary>
        /// GetKdsRootKey implementation.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(DateTime effectiveTime);
        /// <summary>
        /// GetKdsRootKeys implementation.
        /// </summary>
        public IEnumerable<KdsRootKey> GetKdsRootKeys();
        /// <summary>
        /// Gets or sets the SupportsLookupAll.
        /// </summary>
        public bool SupportsLookupAll { get; }
        /// <summary>
        /// Gets or sets the SupportsLookupByEffectiveTime.
        /// </summary>
        public bool SupportsLookupByEffectiveTime { get; }
    }
}
