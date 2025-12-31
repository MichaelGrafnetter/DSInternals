namespace DSInternals.Common.Data;

public interface IKdsRootKeyResolver
{
    public KdsRootKey? GetKdsRootKey(Guid id);
    public KdsRootKey? GetKdsRootKey(DateTime effectiveTime);
    public IEnumerable<KdsRootKey> GetKdsRootKeys();
    public bool SupportsLookupAll { get; }
    public bool SupportsLookupByEffectiveTime { get; }
}
