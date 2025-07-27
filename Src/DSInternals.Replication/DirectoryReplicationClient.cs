using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Interop;
using DSInternals.Common.Properties;
using DSInternals.Common.Schema;
using DSInternals.Replication.Interop;
using DSInternals.Replication.Model;
using NDceRpc;
using NDceRpc.Microsoft.Interop;
using NDceRpc.Native;

namespace DSInternals.Replication
{
    public class DirectoryReplicationClient : IDisposable, IKdsRootKeyResolver
    {
        /// <summary>
        /// Service principal name (SPN) of the destination server.
        /// </summary>
        private const string ServicePrincipalNameFormat = "ldap/{0}";
        private const string DrsNamedPipeName = @"\pipe\lsass";
        private const string ConfigurationNamingContextPrefix = "CN=Configuration,DC=";

        /// <summary>
        /// Identifier of Windows Server 2000 dcpromo.
        /// </summary>
        private static readonly Guid DcPromoGuid2k = new Guid("6abec3d1-3054-41c8-a362-5a0c5b7d5d71");

        /// <summary>
        /// Identifier of Windows Server 2003+ dcpromo.
        /// </summary>
        private static readonly Guid DcPromoGuid2k3 = new Guid("6afab99c-6e26-464a-975f-f58f105218bc");

        /// <summary>
        /// Non-DC client identifier.
        /// </summary>
        private static readonly Guid NtdsApiClientGuid = new Guid("e24d201a-4fd6-11d1-a3da-0000f875ae0d");

        private NativeClient _rpcConnection;
        private DrsConnection _drsConnection;
        private NamedPipeConnection _npConnection;
        private IKdsRootKeyResolver _rootKeyResolver;
        private string _domainNamingContext;
        private string _netBIOSDomainName;
        private string[] _namingContexts;

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

        public DirectoryReplicationClient(string server, RpcProtocol protocol = RpcProtocol.TCP, NetworkCredential credential = null)
        {
            Validator.AssertNotNullOrWhiteSpace(server, nameof(server));
            var schema = BaseSchema.Create();
            this.CreateRpcConnection(server, protocol, credential);
            this._drsConnection = new DrsConnection(this._rpcConnection.Binding, NtdsApiClientGuid, schema);
            this._rootKeyResolver = new KdsRootKeyCache(this);
        }

        public ReplicationCursor[] GetReplicationCursors(string namingContext)
        {
            Validator.AssertNotNullOrWhiteSpace(namingContext, nameof(namingContext));
            return this._drsConnection.GetReplicationCursors(namingContext);
        }

        public IEnumerable<DSAccount> GetAccounts(string domainNamingContext, ReplicationProgressHandler progressReporter = null, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Validator.AssertNotNullOrWhiteSpace(domainNamingContext, nameof(domainNamingContext));
            ReplicationCookie cookie = new ReplicationCookie(domainNamingContext);
            return GetAccounts(cookie, progressReporter, propertySets);
        }

        public IEnumerable<DSAccount> GetAccounts(ReplicationCookie initialCookie, ReplicationProgressHandler progressReporter = null, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Validator.AssertNotNull(initialCookie, nameof(initialCookie));

            propertySets = SkipUnsupportedProperties(propertySets);

            var currentCookie = initialCookie;
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

                // Process the returned objects
                foreach (var obj in result.Objects)
                {
                    var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets);

                    if (account != null)
                    {
                        // CreateAccount returns null for other object types
                        yield return account;
                    }
                }

                // Update the position of the replication cursor
                currentCookie = result.Cookie;
            } while (result.HasMoreData);
        }

        public DSAccount GetAccount(Guid objectGuid, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            propertySets = SkipUnsupportedProperties(propertySets);

            var obj = this._drsConnection.ReplicateSingleObject(objectGuid);
            var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets);

            if (account == null)
            {
                // If the target object is not an account, CreateAccount returns null
                throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, objectGuid);
            }

            return account;
        }

        public DSAccount GetAccount(string distinguishedName, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            propertySets = SkipUnsupportedProperties(propertySets);

            var obj = this._drsConnection.ReplicateSingleObject(distinguishedName);
            var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, _rootKeyResolver, propertySets);

            if (account == null)
            {
                // If the target object is not an account, CreateAccount returns null
                throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, distinguishedName);
            }

            return account;
        }

        public DSAccount GetAccount(NTAccount accountName, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Guid objectGuid = this._drsConnection.ResolveGuid(accountName);
            return this.GetAccount(objectGuid, propertySets);
        }

        public DSAccount GetAccount(SecurityIdentifier sid, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Guid objectGuid = this._drsConnection.ResolveGuid(sid);
            return this.GetAccount(objectGuid, propertySets);
        }

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

        private DPAPIBackupKey GetLSASecret(string distinguishedName)
        {
            var secretObj = this._drsConnection.ReplicateSingleObject(distinguishedName);
            return new DPAPIBackupKey(secretObj, this.SecretDecryptor);
        }

        public void WriteNgcKey(Guid objectGuid, byte[] publicKey)
        {
            string distinguishedName = this._drsConnection.ResolveDistinguishedName(objectGuid);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(NTAccount accountName, byte[] publicKey)
        {
            string distinguishedName = this._drsConnection.ResolveDistinguishedName(accountName);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(SecurityIdentifier sid, byte[] publicKey)
        {
            string distinguishedName = this._drsConnection.ResolveDistinguishedName(sid);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(string accountDN, byte[] publicKey)
        {
            this._drsConnection.WriteNgcKey(accountDN, publicKey);
        }

        private DirectorySecretDecryptor SecretDecryptor
        {
            get
            {
                return new ReplicationSecretDecryptor(this._drsConnection.SessionKey);
            }
        }

        private void CreateRpcConnection(string server, RpcProtocol protocol, NetworkCredential credential = null)
        {
            EndpointBindingInfo binding;
            switch (protocol)
            {
                case RpcProtocol.TCP:
                    binding = new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp, server, null);
                    break;
                case RpcProtocol.SMB:
                    binding = new EndpointBindingInfo(RpcProtseq.ncacn_np, server, DrsNamedPipeName);
                    if (credential != null)
                    {
                        // Connect named pipe
                        this._npConnection = new NamedPipeConnection(server, credential);
                    }
                    break;
                default:
                    // TODO: Extract as string
                    throw new NotImplementedException("The requested RPC protocol is not supported.");
            }
            this._rpcConnection = new NativeClient(binding);

            NetworkCredential rpcCredential = credential ?? Client.Self;
            string spn = String.Format(ServicePrincipalNameFormat, server);
            this._rpcConnection.AuthenticateAs(spn, rpcCredential, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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

            if (this._rpcConnection != null)
            {
                this._rpcConnection.Dispose();
                this._rpcConnection = null;
            }

            if (this._npConnection != null)
            {
                this._npConnection.Dispose();
                this._npConnection = null;
            }
        }

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

        private static AccountPropertySets SkipUnsupportedProperties(AccountPropertySets propertySets)
        {
            // TODO: Retrieval of linked objects is not yet implemented in the replication client.
            propertySets &= ~AccountPropertySets.ManagedBy;
            propertySets &= ~AccountPropertySets.Manager;

            return propertySets;
        }

        #region IKdsRootKeyResolver
        public KdsRootKey? GetKdsRootKey(Guid rootKeyId)
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
                // Suppress the exception.
                return null;
            }
        }

        bool IKdsRootKeyResolver.SupportsLookupAll => false;
        bool IKdsRootKeyResolver.SupportsLookupByEffectiveTime => false;

        KdsRootKey? IKdsRootKeyResolver.GetKdsRootKey(DateTime effectiveTime) => throw new NotSupportedException("Search by effective time is not supported by the MS-DRSR protocol.");

        IEnumerable<KdsRootKey> IKdsRootKeyResolver.GetKdsRootKeys() => throw new NotSupportedException("Search by class type is not supported by the MS-DRSR protocol.");

        #endregion IKdsRootKeyResolver
    }
}
