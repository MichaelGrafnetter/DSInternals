using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Interop;
using DSInternals.Common.Properties;
using DSInternals.Replication.Interop;
using DSInternals.Replication.Model;
using NDceRpc;
using NDceRpc.Microsoft.Interop;
using NDceRpc.Native;

namespace DSInternals.Replication
{
    public class DirectoryReplicationClient : IDisposable
    {
        /// <summary>
        /// Service principal name (SPN) of the destination server.
        /// </summary>
        private const string ServicePrincipalNameFormat = "ldap/{0}";
        private const string DrsNamedPipeName = @"\pipe\lsass";

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

        private NativeClient rpcConnection;
        private DrsConnection drsConnection;
        private NamedPipeConnection npConnection;
        private string domainNamingContext;
        private string netBIOSDomainName;

        public string DomainNamingContext
        {
            get
            {
                if (this.domainNamingContext == null)
                {
                    // Lazy loading
                    this.LoadDomainInfo();
                }

                return this.domainNamingContext;
            }
        }

        public string NetBIOSDomainName
        {
            get
            {
                if (this.netBIOSDomainName == null)
                {
                    // Lazy loading
                    this.LoadDomainInfo();
                }

                return this.netBIOSDomainName;
            }
        }

        public DirectoryReplicationClient(string server, RpcProtocol protocol, NetworkCredential credential = null)
        {
            Validator.AssertNotNullOrWhiteSpace(server, nameof(server));
            this.CreateRpcConnection(server, protocol, credential);
            this.drsConnection = new DrsConnection(this.rpcConnection.Binding, NtdsApiClientGuid);
        }

        public ReplicationCursor[] GetReplicationCursors(string namingContext)
        {
            Validator.AssertNotNullOrWhiteSpace(namingContext, nameof(namingContext));
            return this.drsConnection.GetReplicationCursors(namingContext);
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

            // Create AD schema
            var schema = BasicSchemaFactory.CreateSchema();
            var currentCookie = initialCookie;
            ReplicationResult result;
            int processedObjectCount = 0;

            do
            {
                // Perform one replication cycle
                result = this.drsConnection.ReplicateAllObjects(currentCookie);

                // Report replication progress
                if (progressReporter != null)
                {
                    processedObjectCount += result.Objects.Count;
                    progressReporter(result.Cookie, processedObjectCount, result.TotalObjectCount);
                }

                // Process the returned objects
                foreach (var obj in result.Objects)
                {
                    obj.Schema = schema;

                    var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, propertySets);

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

            var obj = this.drsConnection.ReplicateSingleObject(objectGuid);
            var schema = BasicSchemaFactory.CreateSchema();
            obj.Schema = schema;
            var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, propertySets);

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

            var obj = this.drsConnection.ReplicateSingleObject(distinguishedName);
            var schema = BasicSchemaFactory.CreateSchema();
            obj.Schema = schema;
            var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, this.SecretDecryptor, propertySets);

            if (account == null)
            {
                // If the target object is not an account, CreateAccount returns null
                throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, distinguishedName);
            }

            return account;
        }

        public DSAccount GetAccount(NTAccount accountName, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Guid objectGuid = this.drsConnection.ResolveGuid(accountName);
            return this.GetAccount(objectGuid, propertySets);
        }

        public DSAccount GetAccount(SecurityIdentifier sid, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            Guid objectGuid = this.drsConnection.ResolveGuid(sid);
            return this.GetAccount(objectGuid, propertySets);
        }

        public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(string domainNamingContext)
        {
            // TODO: Move schema from constructor to property?
            // TODO: Split this function into RSA and Legacy Part so that exception in one of them does not crash the whole process
            var schema = BasicSchemaFactory.CreateSchema();

            // Fetch the legacy pointer first, because there is a higher chance that it is present than the RSA one.
            string legacyPointerDN = DPAPIBackupKey.GetPreferredLegacyKeyPointerDN(domainNamingContext);
            var legacyPointer = this.GetLSASecret(legacyPointerDN, schema);
            yield return legacyPointer;

            string legacyKeyDN = DPAPIBackupKey.GetKeyDN(legacyPointer.KeyId, domainNamingContext);
            var legacyKey = this.GetLSASecret(legacyKeyDN, schema);
            yield return legacyKey;

            string rsaPointerDN = DPAPIBackupKey.GetPreferredRSAKeyPointerDN(domainNamingContext);
            var rsaPointer = this.GetLSASecret(rsaPointerDN, schema);
            yield return rsaPointer;

            string rsaKeyDN = DPAPIBackupKey.GetKeyDN(rsaPointer.KeyId, domainNamingContext);
            var rsaKey = this.GetLSASecret(rsaKeyDN, schema);
            yield return rsaKey;
        }

        private DPAPIBackupKey GetLSASecret(string distinguishedName, BasicSchema schema)
        {
            var secretObj = this.drsConnection.ReplicateSingleObject(distinguishedName);
            secretObj.Schema = schema;
            return new DPAPIBackupKey(secretObj, this.SecretDecryptor);
        }

        public void WriteNgcKey(Guid objectGuid, byte[] publicKey)
        {
            string distinguishedName = this.drsConnection.ResolveDistinguishedName(objectGuid);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(NTAccount accountName, byte[] publicKey)
        {
            string distinguishedName = this.drsConnection.ResolveDistinguishedName(accountName);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(SecurityIdentifier sid, byte[] publicKey)
        {
            string distinguishedName = this.drsConnection.ResolveDistinguishedName(sid);
            this.WriteNgcKey(distinguishedName, publicKey);
        }

        public void WriteNgcKey(string accountDN, byte[] publicKey)
        {
            this.drsConnection.WriteNgcKey(accountDN, publicKey);
        }

        private DirectorySecretDecryptor SecretDecryptor
        {
            get
            {
                return new ReplicationSecretDecryptor(this.drsConnection.SessionKey);
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
                        this.npConnection = new NamedPipeConnection(server, credential);
                    }
                    break;
                default:
                    // TODO: Extract as string
                    throw new NotImplementedException("The requested RPC protocol is not supported.");
            }
            this.rpcConnection = new NativeClient(binding);

            NetworkCredential rpcCredential = credential ?? Client.Self;
            string spn = String.Format(ServicePrincipalNameFormat, server);
            this.rpcConnection.AuthenticateAs(spn, rpcCredential, RPC_C_AUTHN_LEVEL.RPC_C_AUTHN_LEVEL_PKT_PRIVACY, RPC_C_AUTHN.RPC_C_AUTHN_GSS_NEGOTIATE);
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

            if (this.drsConnection != null)
            {
                this.drsConnection.Dispose();
                this.drsConnection = null;
            }

            if (this.rpcConnection != null)
            {
                this.rpcConnection.Dispose();
                this.rpcConnection = null;
            }

            if (this.npConnection != null)
            {
                this.npConnection.Dispose();
                this.npConnection = null;
            }
        }

        private void LoadDomainInfo()
        {
            // These is no direct way of retrieving current DC's domain info, so we are using a combination of 3 calls.

            // We first retrieve FSMO roles. The PDC emulator lies in the same domain as the current server.
            var fsmoRoles = this.drsConnection.ListRoles();

            // We need the DC object of the PDC Emulator. It is the parent of the NTDS Settings object.
            string pdcEmulator = new DistinguishedName(fsmoRoles.PdcEmulator).Parent.ToString();

            // Get the PDC account object from the domain partition.
            var pdcInfo = this.drsConnection.ListInfoForServer(pdcEmulator);
            string pdcAccountDN = pdcInfo.ServerReference;

            // Get the PDC Emulator's domain naming context.
            this.domainNamingContext = new DistinguishedName(pdcAccountDN).RootNamingContext.ToString();

            // Get the PDC Emulator's NetBIOS account name and extract the domain part.
            NTAccount pdcAccount = this.drsConnection.ResolveAccountName(pdcAccountDN);
            this.netBIOSDomainName = pdcAccount.NetBIOSDomainName();
        }

        private static AccountPropertySets SkipUnsupportedProperties(AccountPropertySets propertySets)
        {
            // TODO: Retrieval of linked objects is not yet implemented in the replication client.
            propertySets &= ~AccountPropertySets.ManagedBy;
            propertySets &= ~AccountPropertySets.Manager;

            // TODO: LAPS-related attribute schema loading is not yet implemented.
            propertySets &= ~AccountPropertySets.LAPS;

            return propertySets;
        }
    }
}
