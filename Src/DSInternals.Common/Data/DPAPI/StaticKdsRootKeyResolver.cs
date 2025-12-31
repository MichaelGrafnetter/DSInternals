namespace DSInternals.Common.Data;

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
        // Cache the single root key
        _kdsRootKey = rootKey ?? throw new ArgumentNullException(nameof(rootKey));
    }

    public bool SupportsLookupAll => true;

    public KdsRootKey? GetKdsRootKey(Guid id)
    {
        return _kdsRootKey.KeyId == id ? _kdsRootKey : null;
    }

    public KdsRootKey? GetKdsRootKey(DateTime effectiveTime)
    {
        return _kdsRootKey.EffectiveTime <= effectiveTime ? _kdsRootKey : null;
    }

    public IEnumerable<KdsRootKey> GetKdsRootKeys()
    {
        yield return _kdsRootKey;
    }

    public bool SupportsLookupByEffectiveTime => true;
}
