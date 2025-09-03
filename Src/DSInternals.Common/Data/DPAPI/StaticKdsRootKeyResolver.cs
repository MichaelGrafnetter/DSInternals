using System;
using System.Collections.Generic;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Dummy implementation of a resolver pointing to a single KDS Root Key.
    /// </summary>
    /// <remarks>
    /// The purpose of this class is mainly 
    /// </remarks>
    public sealed class StaticKdsRootKeyResolver : IKdsRootKeyResolver
    {
        private KdsRootKey _kdsRootKey;

        public StaticKdsRootKeyResolver(KdsRootKey rootKey)
        {
            if (rootKey == null)
                throw new ArgumentNullException(nameof(rootKey));

            // Cache the single root key
            _kdsRootKey = rootKey;
        }

        /// <summary>
        /// The true.
        /// </summary>
        public bool SupportsLookupAll => true;

        /// <summary>
        /// Gets the KDS root key from the directory store.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(Guid id)
        {
            return _kdsRootKey.KeyId == id ? _kdsRootKey : null;
        }

        /// <summary>
        /// Gets the KDS root key from the directory store.
        /// </summary>
        public KdsRootKey? GetKdsRootKey(DateTime effectiveTime)
        {
            return _kdsRootKey.EffectiveTime <= effectiveTime ? _kdsRootKey : null;
        }

        /// <summary>
        /// Gets all KDS root keys from the directory store.
        /// </summary>
        public IEnumerable<KdsRootKey> GetKdsRootKeys()
        {
            yield return _kdsRootKey;
        }

        /// <summary>
        /// The true.
        /// </summary>
        public bool SupportsLookupByEffectiveTime => true;
    }
}
