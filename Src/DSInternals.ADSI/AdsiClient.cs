using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Net;
using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient : IDisposable
{
    private DirectoryContext _directoryContext;
    private DirectoryEntry _domainNamingContext;
    private DirectoryEntry _configurationNamingContext;
    private Forest _forest;
    private Lazy<List<DirectoryEntry>> _applicationPartitions;
    private string _dnsDomainName;
    private Lazy<string> _netbiosDomainName;
    private IKdsRootKeyResolver _kdsRootKeyResolver;

    /// <summary>
    /// Initializes a new instance of the AdsiClient class and establishes a connection to an Active Directory domain or
    /// directory server.
    /// </summary>
    /// <param name="server">The DNS name of the directory server to connect to. If null, the client connects to the default domain.</param>
    /// <param name="credential">The network credentials used to authenticate with the directory server. If null, the current user's credentials
    /// are used.</param>
    /// <param name="kdsRootKeys">An optional set of KDS root keys that overrides the default LDAP-based resolver. When supplied, these keys
    /// are served by an in-memory resolver instead of being read from the configuration naming context.</param>
    public AdsiClient(string? server = null, NetworkCredential? credential = null, KdsRootKey[]? kdsRootKeys = null)
    {
        // Connect to the DC
        if (!String.IsNullOrEmpty(server))
        {
            _directoryContext = credential != null
                ? new DirectoryContext(DirectoryContextType.DirectoryServer, server, credential.GetLogonName(), credential.Password)
                : new DirectoryContext(DirectoryContextType.DirectoryServer, server);

            using var dc = DomainController.GetDomainController(_directoryContext);

            // Resolve the domain partition
            _domainNamingContext = dc.Domain.GetDirectoryEntry();
            _dnsDomainName = dc.Domain.Name;

            // Resolve the configuration partition
            _configurationNamingContext = GetConfigurationNamingContext(dc);

            // Cache the forest for later use (disposed in Dispose).
            _forest = dc.Forest;
        }
        else
        {
            _directoryContext = credential != null
                ? new DirectoryContext(DirectoryContextType.Domain, credential.GetLogonName(), credential.Password)
                : new DirectoryContext(DirectoryContextType.Domain);

            // Resolve the domain partition
            using var domain = Domain.GetDomain(_directoryContext);
            _domainNamingContext = domain.GetDirectoryEntry();
            _dnsDomainName = domain.Name;

            // Resolve the configuration partition
            _configurationNamingContext = GetConfigurationNamingContext(domain.PdcRoleOwner);

            // Cache the forest for later use (disposed in Dispose).
            _forest = domain.Forest;
        }

        // Defer the application partition and NetBIOS name lookups until they are first accessed.
        _applicationPartitions = new Lazy<List<DirectoryEntry>>(GetApplicationPartitionEntries);
        _netbiosDomainName = new Lazy<string>(GetNetbiosDomainName);

        // Locate KDS root keys. Caller-supplied keys override the default LDAP-based resolver.
        _kdsRootKeyResolver = kdsRootKeys != null
            ? new StaticKdsRootKeyResolver(kdsRootKeys)
            : new KdsRootKeyCache(new AdsiKdsRootKeyResolver(_configurationNamingContext));
    }

    #region IDisposable Support
    /// <summary>
    /// Releases all resources used by the current instance.
    /// </summary>
    /// <remarks>Call this method when you are finished using the object to free unmanaged resources and
    /// perform other cleanup operations. After calling Dispose, the object should not be used.</remarks>
    public void Dispose()
    {
        _domainNamingContext?.Dispose();
        _configurationNamingContext?.Dispose();
        _forest.Dispose();

        if (_applicationPartitions != null && _applicationPartitions.IsValueCreated)
        {
            foreach (DirectoryEntry partition in _applicationPartitions.Value)
            {
                partition.Dispose();
            }
        }
    }
    #endregion

    /// <summary>
    /// Retrieves the NetBIOS domain name corresponding to the current DNS domain name from Active Directory.
    /// </summary>
    /// <remarks>This method queries Active Directory using the configured naming context and DNS domain name.
    /// The returned NetBIOS name is typically used for legacy compatibility or environments where short domain names
    /// are required.</remarks>
    /// <returns>A string containing the NetBIOS domain name associated with the configured DNS domain name.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the NetBIOS domain name cannot be found for the current DNS domain name.</exception>
    private string GetNetbiosDomainName()
    {
        string netbiosNameFilter = $"(&(objectCategory=crossRef)(nETBIOSName=*)(dnsroot={_dnsDomainName}))";

        using var netbiosNameSearcher = new DirectorySearcher(
            _configurationNamingContext,
            netbiosNameFilter,
            [CommonDirectoryAttributes.NetBIOSName],
            SearchScope.Subtree);
        netbiosNameSearcher.CacheResults = false;

        // There can only be one matching object
        SearchResult? searchResult = netbiosNameSearcher.FindOne();

        return searchResult?.Properties[CommonDirectoryAttributes.NetBIOSName][0] as string
            ?? throw new InvalidOperationException($"Could not locate the NetBIOS name for domain '{_dnsDomainName}'.");
    }

    /// <summary>
    /// Materializes a <see cref="DirectoryEntry"/> bound to the NC head of every application partition in the
    /// cached forest. The returned entries inherit the credentials of the originating <see cref="DirectoryContext"/>
    /// and must be disposed by the caller.
    /// </summary>
    /// <returns>A list of directory entries, one per application partition.</returns>
    private List<DirectoryEntry> GetApplicationPartitionEntries()
    {
        ApplicationPartitionCollection partitions = _forest.ApplicationPartitions;
        var entries = new List<DirectoryEntry>(partitions.Count);

        foreach (ApplicationPartition partition in partitions)
        {
            try
            {
                entries.Add(partition.GetDirectoryEntry());
            }
            finally
            {
                partition.Dispose();
            }
        }

        return entries;
    }

    /// <summary>
    /// Retrieves the configuration naming context entry from the specified domain controller.
    /// </summary>
    /// <remarks>The returned <see cref="DirectoryEntry"/> must be disposed by the caller when no longer
    /// needed. This method traverses the directory hierarchy to locate the configuration naming context associated with
    /// the provided domain controller.</remarks>
    /// <param name="dc">The domain controller from which to obtain the configuration naming context. Cannot be null.</param>
    /// <returns>A <see cref="DirectoryEntry"/> representing the configuration naming context of the domain controller.</returns>
    private static DirectoryEntry GetConfigurationNamingContext(DomainController dc)
    {
        DirectoryEntry currentEntry = dc.GetDirectoryEntry();

        // Locate the configuration naming context by traversing upwards from the child entry.
        do
        {
            DirectoryEntry childEntry = currentEntry;
            try
            {
                currentEntry = currentEntry.Parent;
            }
            finally
            {
                childEntry.Dispose();
            }
        } while (currentEntry.SchemaClassName != CommonDirectoryClasses.Configuration);

        return currentEntry;
    }

}
