namespace DSInternals.Common.Data;

/// <summary>
/// Resolver that serves a fixed, in-memory set of KDS Root Keys.
/// </summary>
/// <remarks>
/// Useful for tests and for scenarios where the keys are obtained out-of-band, e.g., manually replicated.
/// </remarks>
public sealed class StaticKdsRootKeyResolver : IKdsRootKeyResolver
{
    private readonly IReadOnlyDictionary<Guid, KdsRootKey> _kdsRootKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="StaticKdsRootKeyResolver"/> class with a single KDS Root Key.
    /// </summary>
    /// <param name="rootKey">The KDS Root Key to expose through this resolver.</param>
    public StaticKdsRootKeyResolver(KdsRootKey rootKey)
        : this(new[] { rootKey ?? throw new ArgumentNullException(nameof(rootKey)) })
    {
    }

    public StaticKdsRootKeyResolver(IEnumerable<KdsRootKey> rootKeys)
    {
        ArgumentNullException.ThrowIfNull(rootKeys);
        _kdsRootKeys = rootKeys.ToDictionary(key => key.KeyId);
    }

    /// <summary>
    /// Gets a value indicating whether this resolver supports enumerating all available KDS Root Keys.
    /// </summary>
    public bool SupportsLookupAll => true;

    /// <summary>
    /// Gets a value indicating whether this resolver supports looking up KDS Root Keys by effective time.
    /// </summary>
    public bool SupportsLookupByEffectiveTime => true;

    /// <summary>
    /// Gets a KDS Root Key by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the KDS Root Key to retrieve.</param>
    /// <returns>The KDS Root Key with the specified identifier, or <c>null</c> if not found.</returns>
    public KdsRootKey? GetKdsRootKey(Guid id)
    {
        return _kdsRootKeys.TryGetValue(id, out KdsRootKey rootKey) ? rootKey : null;
    }

    /// <summary>
    /// Gets a KDS Root Key by its effective time.
    /// </summary>
    /// <param name="effectiveTime">The effective time of the KDS Root Key to retrieve.</param>
    /// <returns>The KDS Root Key with the specified effective time, or <c>null</c> if not found.</returns>
    public KdsRootKey? GetKdsRootKey(DateTime effectiveTime)
    {
        // Find the most recently created key whose EffectiveTime is on or before the requested time.
        KdsRootKey latestEffectiveKey = null;

        foreach (KdsRootKey candidate in _kdsRootKeys.Values)
        {
            if (candidate.EffectiveTime <= effectiveTime &&
                (latestEffectiveKey == null || latestEffectiveKey.CreationTime < candidate.CreationTime))
            {
                latestEffectiveKey = candidate;
            }
        }

        return latestEffectiveKey;
    }

    /// <summary>
    /// Gets all available KDS Root Keys.
    /// </summary>
    /// <returns>An enumerable collection of all KDS Root Keys.</returns>
    public IEnumerable<KdsRootKey> GetKdsRootKeys() => _kdsRootKeys.Values;
}
