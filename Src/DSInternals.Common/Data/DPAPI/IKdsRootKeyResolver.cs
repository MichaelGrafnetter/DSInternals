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
        /// Gets the KDS root key from the directory store.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(Guid id);
        /// <summary>
        /// Gets the KDS root key from the directory store.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(DateTime effectiveTime);
        /// <summary>
        /// Gets all KDS root keys from the directory store.
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
