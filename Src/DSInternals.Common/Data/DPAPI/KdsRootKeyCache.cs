using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DSInternals.Common.Data
{
    public sealed class KdsRootKeyCache : IKdsRootKeyResolver
    {
        /// <summary>
        /// Cached root keys from previous lookups.
        /// </summary>
        private IDictionary<Guid, KdsRootKey> _rootKeyCache;

        /// <summary>
        /// List of root keys that were not found in AD.
        /// </summary>
        /// <remarks>
        /// There is no concurrent HashSet, so we use ConcurrentDictionary with dummy byte values.
        /// </remarks>
        private IDictionary<Guid, byte>? _negativeCache;

        private IKdsRootKeyResolver _innerResolver;

        public KdsRootKeyCache(IKdsRootKeyResolver resolver, bool preloadCache = false)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));

            _innerResolver = resolver;

            if (preloadCache)
            {
                if (!resolver.SupportsLookupAll)
                {
                    // Eager loading has been requested, but it is not supported by the provider.
                    throw new InvalidOperationException("This provider cannot fetch all KDS root keys.");
                }

                // Populate the positive cache
                _rootKeyCache = _innerResolver.GetKdsRootKeys().ToDictionary(item => item.KeyId);

                // Do not perform online lookups
                _negativeCache = null;
            }
            else
            {
                _rootKeyCache = new ConcurrentDictionary<Guid, KdsRootKey>();
                _negativeCache = new ConcurrentDictionary<Guid, byte>();
            }
        }

        public bool SupportsLookupAll => _innerResolver.SupportsLookupAll;

        public bool SupportsLookupByEffectiveTime => _innerResolver.SupportsLookupAll || _innerResolver.SupportsLookupByEffectiveTime;

        public KdsRootKey? GetKdsRootKey(Guid id)
        {
            if (_rootKeyCache.TryGetValue(id, out KdsRootKey cachedRootKey))
            {
                // Found in positive cache.
                return cachedRootKey;
            }

            if (_negativeCache?.ContainsKey(id) ?? true)
            {
                // This root key was previously not found in AD, so there is no need to search for it again.
                // OR the negative cache is not even created, which means that eager loading must have already happened.
                return null;
            }

            // This is the first time this root key ID is seen. Perform an active lookup.
            var result = _innerResolver.GetKdsRootKey(id);

            if (result != null)
            {
                // Found. Add to positive cache before returning it.
                _rootKeyCache.Add(id, result);
            }
            else
            {
                // Not found. Add to negative cache.
                _negativeCache.Add(id, byte.MinValue);
            }

            return result;
        }

        public KdsRootKey? GetKdsRootKey(DateTime effectiveTime)
        {
            if (!_innerResolver.SupportsLookupAll)
            {
                if (_innerResolver.SupportsLookupByEffectiveTime)
                {
                    // Forward the query
                    return _innerResolver.GetKdsRootKey(effectiveTime);
                }
                else
                {
                    // We have not found the latest effective KDS root key.
                    return null;
                }
            }
            else
            {
                // Lookup all is supported
                if (_negativeCache != null)
                {
                    // Populate the positive cache
                    // TODO Merge collections instead, because of possible L0 key caches
                    _rootKeyCache = _innerResolver.GetKdsRootKeys().ToDictionary(item => item.KeyId);

                    // Do not perform further online lookups
                    _negativeCache = null;
                }

                KdsRootKey? latestEffectiveKey = null;

                foreach (var candidateKey in _rootKeyCache.Values)
                {
                    // Check if this key is the newest found yet
                    if (candidateKey.EffectiveTime <= effectiveTime && (latestEffectiveKey == null || latestEffectiveKey.CreationTime < candidateKey.CreationTime))
                    {
                        latestEffectiveKey = candidateKey;
                    }
                }

                return latestEffectiveKey;
            }
        }

        public IEnumerable<KdsRootKey> GetKdsRootKeys()
        {
            if (!this.SupportsLookupAll)
            {
                // The protocol does not support fetching all KDS root keys at once.
                yield break;
            }

            if (this._negativeCache == null)
            {
                // All KDS root keys have been eagerly preloaded.
                foreach (var rootKey in _rootKeyCache.Values)
                {
                    yield return rootKey;
                }
            }
            else
            {
                // Perform an active lookup
                foreach (var rootKey in _innerResolver.GetKdsRootKeys())
                {
                    // There might be some keys already pre-cached from previous lookups, possibly holding pre-calculated L0 key.
                    if (!_rootKeyCache.ContainsKey(rootKey.KeyId))
                    {
                        // Allow the key to be found by ID in the future
                        _rootKeyCache[rootKey.KeyId] = rootKey;
                    }

                    // Pass-through the value
                    yield return rootKey;
                }

                // Indicate that all keys are already in the cache.
                _negativeCache = null;
            }
        }
    }
}
