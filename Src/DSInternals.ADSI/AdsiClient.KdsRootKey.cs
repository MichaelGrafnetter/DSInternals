using DSInternals.Common.Data;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    /// <summary>
    /// Retrieves all KDS root keys stored in the configuration naming context.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="KdsRootKey"/> objects.</returns>
    public IEnumerable<KdsRootKey> GetKdsRootKeys()
    {
        return _kdsRootKeyResolver.GetKdsRootKeys();
    }

    /// <summary>
    /// Retrieves a single KDS root key by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the KDS root key to retrieve.</param>
    /// <returns>The corresponding <see cref="KdsRootKey"/> or <c>null</c> if no key with the specified identifier exists.</returns>
    public KdsRootKey? GetKdsRootKey(Guid id)
    {
        return _kdsRootKeyResolver.GetKdsRootKey(id);
    }
}
