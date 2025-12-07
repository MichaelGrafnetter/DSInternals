using System.DirectoryServices;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

/// <summary>
/// Resolves KDS root keys from Active Directory using LDAP queries.
/// </summary>
public sealed class AdsiKdsRootKeyResolver : IKdsRootKeyResolver
{
    private const string KdsRootKeyFilter = "(objectClass=msKds-ProvRootKey)";
    private const string KdsRootKeyByIdFilterFormat = "(&(objectClass=msKds-ProvRootKey)(cn={0}))";

    private static readonly string[] KdsRootKeyProperties = [
        CommonDirectoryAttributes.RDN,
        CommonDirectoryAttributes.KdsVersion,
        CommonDirectoryAttributes.KdsDomainController,
        CommonDirectoryAttributes.KdsRootKeyData,
        CommonDirectoryAttributes.KdsCreationTime,
        CommonDirectoryAttributes.KdsEffectiveTime,
        CommonDirectoryAttributes.KdsKdfAlgorithmId,
        CommonDirectoryAttributes.KdsKdfParameters,
        CommonDirectoryAttributes.KdsSecretAgreementAlgorithmId,
        CommonDirectoryAttributes.KdsSecretAgreementParameters,
        CommonDirectoryAttributes.KdsSecretAgreementPrivateKeyLength,
        CommonDirectoryAttributes.KdsSecretAgreementPublicKeyLength
    ];

    private DirectoryEntry _configurationNamingContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdsiKdsRootKeyResolver"/> class.
    /// </summary>
    /// <param name="configurationNamingContext">The distinguished name of the configuration naming context.</param>
    public AdsiKdsRootKeyResolver(DirectoryEntry configurationNamingContext)
    {
        _configurationNamingContext = configurationNamingContext ?? throw new ArgumentNullException(nameof(configurationNamingContext));
    }

    public bool SupportsLookupAll => true;

    public bool SupportsLookupByEffectiveTime => false;

    public KdsRootKey? GetKdsRootKey(Guid id)
    {
        string rootKeyFilter = string.Format(KdsRootKeyByIdFilterFormat, id.ToString("D"));
        using var singleRootKeySearcher = new DirectorySearcher(
            _configurationNamingContext,
            rootKeyFilter,
            KdsRootKeyProperties,
            SearchScope.Subtree);

        singleRootKeySearcher.CacheResults = false;
        var result = singleRootKeySearcher.FindOne();

        if (result == null)
        {
            // KDS root key not found
            return null;
        }

        var adapter = new AdsiObjectAdapter(result);
        return new KdsRootKey(adapter);
    }

    public KdsRootKey? GetKdsRootKey(DateTime effectiveTime)
    {
        // Direct search by effective time requires iterating all keys
        // Use the caching resolver for efficient lookups by effective time
        throw new NotSupportedException("Direct search by effective time is not supported via LDAP. Use the caching resolver instead.");
    }

    public IEnumerable<KdsRootKey> GetKdsRootKeys()
    {
        using var rootKeySearcher = new DirectorySearcher(
            _configurationNamingContext,
            KdsRootKeyFilter,
            KdsRootKeyProperties,
            SearchScope.Subtree);

        rootKeySearcher.CacheResults = false;

        using var results = rootKeySearcher.FindAll();

        foreach (SearchResult result in results)
        {
            var adapter = new AdsiObjectAdapter(result);
            yield return new KdsRootKey(adapter);
        }
    }
}
