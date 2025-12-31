using System.Globalization;
using System.Net;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Schema;
using DSInternals.Replication.Interop;
using DSInternals.Replication.Model;

namespace DSInternals.Replication;

/// <summary>
/// Provides methods for connecting to a domain controller and replicating directory objects.
/// </summary>
public class DirectoryReplicationClient : IDisposable, IKdsRootKeyResolver
{
    /// <summary>
    /// Service principal name (SPN) of the destination server.
    /// </summary>
    private const string ServicePrincipalNameFormat = "ldap/{0}";

    /// <summary>
    /// Named pipe used for DRSR communication.
    /// </summary>
    private const string DrsNamedPipeName = @"\pipe\lsass";

    /// <summary>
    /// Prefix of the configuration naming context.
    /// </summary>
    private const string ConfigurationNamingContextPrefix = "CN=Configuration,DC=";

    /// <summary>
    /// Prefix of the schema naming context.
    /// </summary>
    private const string SchemaNamingContextPrefix = "CN=Schema,CN=Configuration,DC=";

    /// <summary>
    /// Identifier of Windows Server 2000 dcpromo.
    /// </summary>
    private static readonly Guid DcPromoGuid2k = new("6abec3d1-3054-41c8-a362-5a0c5b7d5d71");

    /// <summary>
    /// Identifier of Windows Server 2003+ dcpromo.
    /// </summary>
    private static readonly Guid DcPromoGuid2k3 = new("6afab99c-6e26-464a-975f-f58f105218bc");

    /// <summary>
    /// Non-DC client identifier.
    /// </summary>
    private static readonly Guid NtdsApiClientGuid = new("e24d201a-4fd6-11d1-a3da-0000f875ae0d");

    private bool _isFullSchemaLoaded = false;
    private RpcBinding _rpcBinding;
    private DrsConnection _drsConnection;
    private IKdsRootKeyResolver _rootKeyResolver;
    private string _domainNamingContext;
    private string _netBIOSDomainName;
    private string[] _namingContexts;

    /// <summary>
    /// The domain naming context of the connected server.
    /// </summary>
    public string DomainNamingContext
    {
        get
        {
            if (_domainNamingContext == null)
            {
                // Lazy loading
                this.LoadDomainInfo();
            }

            return _domainNamingContext;
        }
    }

    /// <summary>
    /// The NetBIOS domain name of the connected server.
    /// </summary>
    public string NetBIOSDomainName
    {
        get
        {
            if (_netBIOSDomainName == null)
            {
                // Lazy loading
                this.LoadDomainInfo();
            }

            return _netBIOSDomainName;
        }
    }

    /// <summary>
    /// The naming contexts (partitions) hosted by the connected server.
    /// </summary>
    public string[] NamingContexts
    {
        get
        {
            if (_namingContexts == null)
            {
                // Lazy loading
                _namingContexts = this._drsConnection.ListNamingContexts();
            }

            return _namingContexts;
        }
    }

    /// <summary>
    /// The configuration naming context of the connected server.
    /// </summary>
    public string ConfigurationNamingContext
    {
        get
        {
            // TODO: It would be more elegant to load the ConfigNC based on its GUID.
            return this.NamingContexts.
                Where(context => context.StartsWith(ConfigurationNamingContextPrefix, StringComparison.InvariantCultureIgnoreCase)).
                First();
        }
    }

    /// <summary>
    /// The schema naming context of the connected server.
    /// </summary>
    public string SchemaNamingContext
    {
        get
        {
            return this.NamingContexts.
                Where(context => context.StartsWith(SchemaNamingContextPrefix, StringComparison.InvariantCultureIgnoreCase)).
                First();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryReplicationClient"/> class.
    /// </summary>
    /// <param name="server">The FQDN or IP address of the domain controller.</param>
    /// <param name="credential">The credentials to use for authentication.</param>
    public DirectoryReplicationClient(string server, NetworkCredential credential = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(server);
        var schema = BaseSchema.Create();
        this._rpcBinding = new RpcBinding(server, RpcProtseq.ncacn_ip_tcp);
        string spn = String.Format(CultureInfo.InvariantCulture, ServicePrincipalNameFormat, server);
        this._rpcBinding.AuthenticateAs(spn, credential, RpcAuthenticationLevel.PacketPrivacy, RpcAuthenticationType.Negotiate);
        this._drsConnection = new DrsConnection(this._rpcBinding.DangerousGetHandle(), NtdsApiClientGuid, schema);

        // The replication client can fetch root keys. Cache them for performance.
        this._rootKeyResolver = new KdsRootKeyCache(this);
    }

    /// <summary>
    /// Gets the replication cursors for the specified naming context.
    /// </summary>
    /// <param name="namingContext">The naming context to retrieve replication cursors for.</param>
    /// <returns>An array of replication cursors.</returns>
    public ReplicationCursor[] GetReplicationCursors(string namingContext)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(namingContext);
        return this._drsConnection.GetReplicationCursors(namingContext);
    }

    /// <summary>
    /// Retrieves all accounts from the specified domain partition.
    /// </summary>
    /// <param name="domainNamingContext">The distinguished name of the domain partition.</param>
    /// <param name="progressReporter">The progress reporter to report replication progress.</param>
    /// <param name="propertySets">The set of properties to retrieve for each account.</param>
    /// <returns>An enumerable collection of directory service accounts.</returns>
    public IEnumerable<DSAccount> GetAccounts(string domainNamingContext, ReplicationProgressHandler progressReporter = null, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(domainNamingContext);

        return ReplicateAllObjects(domainNamingContext, progressReporter)
            .Select(dsObject => AccountFactory.CreateAccount(dsObject, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets))
            .Where(account => account != null); // CreateAccount returns null for other object types
    }

    /// <summary>
    /// Retrieves all directory objects from the specified naming context.
    /// </summary>
    /// <param name="namingContext">Partition to replicate.</param>
    /// <param name="progressReporter">Progress reporter for replication progress.</param>
    /// <returns>An enumerable collection of directory service objects.</returns>
    public IEnumerable<ReplicaObject> ReplicateAllObjects(string namingContext, ReplicationProgressHandler progressReporter = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(namingContext);
        ReplicationCookie currentCookie = new(namingContext);
        ReplicationResult result;
        int processedObjectCount = 0;

        do
        {
            // Perform one replication cycle
            result = this._drsConnection.ReplicateAllObjects(currentCookie);

            // Report replication progress
            if (progressReporter != null)
            {
                processedObjectCount += result.Objects.Count;
                progressReporter(result.Cookie, processedObjectCount, result.TotalObjectCount);
            }

            // Pass-through the returned objects
            foreach (var obj in result.Objects)
            {
                yield return obj;
            }

            // Update the position of the replication cursor
            currentCookie = result.Cookie;
        } while (result.HasMoreData);
    }

    /// <summary>
    /// Retrieves a single account by its object GUID.
    /// </summary>
    /// <param name="objectGuid">The object GUID of the account to retrieve.</param>
    /// <param name="propertySets">The set of properties to retrieve for the account.</param>
    /// <returns>An enumerable collection of directory service accounts.</returns>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the object is not an account.</exception>
    public DSAccount GetAccount(Guid objectGuid, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ReplicaObject obj = this._drsConnection.ReplicateSingleObject(objectGuid);
        var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets);

        if (account == null)
        {
            // If the target object is not an account, CreateAccount returns null
            throw new DirectoryObjectOperationException("The object is not an account.", objectGuid);
        }

        return account;
    }

    /// <summary>
    /// Retrieves a single account by its distinguished name.
    /// </summary>
    /// <param name="distinguishedName">The distinguished name of the account to retrieve.</param>
    /// <param name="propertySets">The set of properties to retrieve for the account.</param>
    /// <returns>An enumerable collection of directory service accounts.</returns>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the object is not an account.</exception>
    public DSAccount GetAccount(string distinguishedName, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ReplicaObject obj = this._drsConnection.ReplicateSingleObject(distinguishedName);
        return AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets) ??
            throw new DirectoryObjectOperationException("The object is not an account.", distinguishedName);
    }

    /// <summary>
    /// Retrieves a single account by its NT account name.
    /// </summary>
    /// <param name="accountName">The NT account name of the account to retrieve.</param>
    /// <param name="propertySets">The set of properties to retrieve for the account.</param>
    /// <returns>An enumerable collection of directory service accounts.</returns>
    public DSAccount GetAccount(NTAccount accountName, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        Guid objectGuid = this._drsConnection.ResolveGuid(accountName);
        return this.GetAccount(objectGuid, propertySets);
    }

    /// <summary>
    /// Retrieves a single account by its security identifier (SID).
    /// </summary>
    /// <param name="sid">The security identifier (SID) of the account to retrieve.</param>
    /// <param name="propertySets">The set of properties to retrieve for the account.</param>
    /// <returns>An enumerable collection of directory service accounts.</returns>
    public DSAccount GetAccount(SecurityIdentifier sid, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        Guid objectGuid = this._drsConnection.ResolveGuid(sid);
        return this.GetAccount(objectGuid, propertySets);
    }

    /// <summary>
    /// Retrieves the KDS root key with the specified identifier.
    /// </summary>
    /// <param name="rootKeyId">The identifier of the KDS root key to retrieve.</param>
    /// <param name="suppressNotFoundException">Whether to suppress the not found exception.</param>
    /// <returns></returns>
    public KdsRootKey? GetKdsRootKey(Guid rootKeyId, bool suppressNotFoundException = false)
    {
        // Derive the full path to the object
        // Example: CN=4dd60361-9394-492a-b11d-51a955f02b06,CN=Master Root Keys,CN=Group Key Distribution Service,CN=Services,CN=Configuration,DC=contoso,DC=com
        string rootKeyDN = KdsRootKey.GetDistinguishedName(rootKeyId, this.ConfigurationNamingContext);

        try
        {
            var rootKeyObject = _drsConnection.ReplicateSingleObject(rootKeyDN);
            return new KdsRootKey(rootKeyObject);
        }
        catch (DirectoryObjectNotFoundException)
        {
            if (suppressNotFoundException)
            {
                return null;
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Retrieves all DPAPI backup keys from the specified domain partition.
    /// </summary>
    /// <param name="domainNamingContext">The distinguished name of the domain partition.</param>
    /// <returns>An enumerable collection of DPAPI backup keys.</returns>
    public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(string domainNamingContext)
    {
        // TODO: Split this function into RSA and Legacy Part so that exception in one of them does not crash the whole process

        // Fetch the legacy pointer first, because there is a higher chance that it is present than the RSA one.
        string legacyPointerDN = DPAPIBackupKey.GetPreferredLegacyKeyPointerDN(domainNamingContext);
        var legacyPointer = this.GetLSASecret(legacyPointerDN);
        yield return legacyPointer;

        string legacyKeyDN = DPAPIBackupKey.GetKeyDN(legacyPointer.KeyId, domainNamingContext);
        var legacyKey = this.GetLSASecret(legacyKeyDN);
        yield return legacyKey;

        string rsaPointerDN = DPAPIBackupKey.GetPreferredRSAKeyPointerDN(domainNamingContext);
        var rsaPointer = this.GetLSASecret(rsaPointerDN);
        yield return rsaPointer;

        string rsaKeyDN = DPAPIBackupKey.GetKeyDN(rsaPointer.KeyId, domainNamingContext);
        var rsaKey = this.GetLSASecret(rsaKeyDN);
        yield return rsaKey;
    }

    /// <summary>
    /// Retrieves a single LSA secret by its distinguished name.
    /// </summary>
    /// <param name="distinguishedName">The distinguished name of the LSA secret.</param>
    /// <returns>The DPAPI backup key.</returns>
    private DPAPIBackupKey GetLSASecret(string distinguishedName)
    {
        var secretObj = this._drsConnection.ReplicateSingleObject(distinguishedName);
        return new DPAPIBackupKey(secretObj, this.SecretDecryptor);
    }

    /// <summary>
    /// Writes the NGC public key to the specified account.
    /// </summary>
    /// <param name="objectGuid">The GUID of the account to write the key to.</param>
    /// <param name="publicKey">The NGC public key to write.</param>
    public void WriteNgcKey(Guid objectGuid, byte[] publicKey)
    {
        string distinguishedName = this._drsConnection.ResolveDistinguishedName(objectGuid);
        this.WriteNgcKey(distinguishedName, publicKey);
    }

    /// <summary>
    /// Writes the NGC public key to the specified account.
    /// </summary>
    /// <param name="accountName">The name of the account to write the key to.</param>
    /// <param name="publicKey">The NGC public key to write.</param>
    public void WriteNgcKey(NTAccount accountName, byte[] publicKey)
    {
        string distinguishedName = this._drsConnection.ResolveDistinguishedName(accountName);
        this.WriteNgcKey(distinguishedName, publicKey);
    }

    /// <summary>
    /// Writes the NGC public key to the specified account.
    /// </summary>
    /// <param name="sid">The security identifier of the account to write the key to.</param>
    /// <param name="publicKey">The NGC public key to write.</param>
    public void WriteNgcKey(SecurityIdentifier sid, byte[] publicKey)
    {
        string distinguishedName = this._drsConnection.ResolveDistinguishedName(sid);
        this.WriteNgcKey(distinguishedName, publicKey);
    }

    /// <summary>
    /// Writes the NGC public key to the specified account.
    /// </summary>
    /// <param name="accountDN">The distinguished name of the account to write the key to.</param>
    /// <param name="publicKey">The NGC public key to write.</param>
    public void WriteNgcKey(string accountDN, byte[] publicKey)
    {
        this._drsConnection.WriteNgcKey(accountDN, publicKey);
    }

    /// <summary>
    /// Replicates the entire schema partition.
    /// </summary>
    /// <param name="progressReporter">Replication progress reporter.</param>
    public void FetchFullSchema(ReplicationProgressHandler progressReporter = null)
    {
        if (_isFullSchemaLoaded)
        {
            // Full schema only needs to be replicated once.
            return;
        }

        // Create a blank schema representation
        ReplicationSchema schema = new();

        // Replicate the entire schema partition
        ReplicationCookie currentCookie = new(SchemaNamingContext);
        ReplicationResult result;
        int processedObjectCount = 0;

        do
        {
            // Perform one replication cycle
            result = this._drsConnection.ReplicateAllObjects(currentCookie);

            // Report replication progress
            if (progressReporter != null)
            {
                processedObjectCount += result.Objects.Count;
                progressReporter(result.Cookie, processedObjectCount, result.TotalObjectCount);
            }

            // Merge the prefix tables
            if (result.PrefixTable != null)
            {
                schema.PrefixTable.Add(result.PrefixTable);
            }

            // Try to add the object to the schema if it is an attribute or class definition
            foreach (var schemaObject in result.Objects)
            {
                schema.AddSchemaObject(schemaObject);
            }

            // Update the position of the replication cursor
            currentCookie = result.Cookie;
        } while (result.HasMoreData);

        _drsConnection.UpdateSchemaCache(schema);

        _isFullSchemaLoaded = true;
    }

    /// <summary>
    /// Gets the secret decryptor used to decrypt sensitive attributes.
    /// </summary>
    private DirectorySecretDecryptor SecretDecryptor
    {
        get
        {
            // TODO: Cache the decryptor
            return new ReplicationSecretDecryptor(this._drsConnection.SessionKey);
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="DirectoryReplicationClient"/>.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="DirectoryReplicationClient"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        if (this._drsConnection != null)
        {
            this._drsConnection.Dispose();
            this._drsConnection = null;
        }

        if (this._rpcBinding != null)
        {
            this._rpcBinding.Dispose();
            this._rpcBinding = null;
        }
    }

    /// <summary>
    /// Loads the domain naming context and NetBIOS domain name of the connected server.
    /// </summary>
    private void LoadDomainInfo()
    {
        // These is no direct way of retrieving current DC's domain info, so we are using a combination of 3 calls.

        // We first retrieve FSMO roles. The PDC emulator lies in the same domain as the current server.
        var fsmoRoles = _drsConnection.ListRoles();

        // We need the DC object of the PDC Emulator. It is the parent of the NTDS Settings object.
        string pdcEmulator = new DistinguishedName(fsmoRoles.PdcEmulator).Parent.ToString();

        // Get the PDC account object from the domain partition.
        var pdcInfo = _drsConnection.ListInfoForServer(pdcEmulator);
        string pdcAccountDN = pdcInfo.ServerReference;

        // Get the PDC Emulator's domain naming context.
        _domainNamingContext = new DistinguishedName(pdcAccountDN).RootNamingContext.ToString();

        // Get the PDC Emulator's NetBIOS account name and extract the domain part.
        NTAccount pdcAccount = _drsConnection.ResolveAccountName(pdcAccountDN);
        _netBIOSDomainName = pdcAccount.NetBIOSDomainName();
    }

    #region IKdsRootKeyResolver
    /// <summary>
    /// Gets the KDS root key with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the KDS root key.</param>
    /// <returns>The KDS root key, or null if not found.</returns>
    KdsRootKey? IKdsRootKeyResolver.GetKdsRootKey(Guid id) => this.GetKdsRootKey(id, suppressNotFoundException: true);

    /// <summary>
    /// Gets a value indicating whether the resolver supports looking up all root keys.
    /// </summary>
    bool IKdsRootKeyResolver.SupportsLookupAll => false;

    /// <summary>
    /// Gets a value indicating whether the resolver supports looking up root keys by effective time.
    /// </summary>
    bool IKdsRootKeyResolver.SupportsLookupByEffectiveTime => false;

    /// <summary>
    /// Gets the KDS root key that was effective at the specified time.
    /// </summary>
    /// <exception cref="NotSupportedException">Search by effective time is not supported by the MS-DRSR protocol.</exception>
    KdsRootKey? IKdsRootKeyResolver.GetKdsRootKey(DateTime effectiveTime) => throw new NotSupportedException("Search by effective time is not supported by the MS-DRSR protocol.");

    /// <summary>
    /// Gets all KDS root keys.
    /// </summary>
    /// <exception cref="NotSupportedException">Search by class type is not supported by the MS-DRSR protocol.</exception>
    IEnumerable<KdsRootKey> IKdsRootKeyResolver.GetKdsRootKeys() => throw new NotSupportedException("Search by class type is not supported by the MS-DRSR protocol.");

    #endregion IKdsRootKeyResolver
}
