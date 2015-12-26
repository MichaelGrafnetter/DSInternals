namespace DSInternals.Replication
{
    using DSInternals.Common.Data;
    using DSInternals.Replication.Model;
    using DSInternals.Replication.Interop;
    using NDceRpc;
    using NDceRpc.Microsoft.Interop;
    using NDceRpc.Native;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Principal;
    using DSInternals.Common.Interop;
    using DSInternals.Common.Cryptography;

    public class DirectoryReplicationClient : IDisposable
    {
        private const string DrsNamedPipeName = "\\pipe\\lsass";
        /// <summary>
        /// This magic guid is needed to fetch the whole tree from a Win 2000 Server DC with DsGetNCChanges() as Administrator.
        /// </summary>
        static readonly Guid DcPromoGuid2k = new Guid("6abec3d1-3054-41c8-a362-5a0c5b7d5d71");
        /// <summary>
        /// This magic guid is needed to fetch the whole tree from a Win 2003+ Server DC with DsGetNCChanges() as Administrator.
        /// </summary>
        static readonly Guid DcPromoGuid2k3 = new Guid("6afab99c-6e26-464a-975f-f58f105218bc");

        private NativeClient rpcConnection;
        private DrsConnection drsConnection;
        private NamedPipeConnection npConnection;

        public DirectoryReplicationClient(string server, RpcProtocol protocol, NetworkCredential credential = null)
        {
            this.CreateRpcConnection(server, protocol, credential);
            this.drsConnection = new DrsConnection(this.rpcConnection.Binding, DcPromoGuid2k3);
        }

        public IEnumerable<DSAccount> GetAccounts(string domainNamingContext)
        {
            ReplicationResult result;
            // Set Schema
            // TODO: Test bad cookie
            ReplicationCookie cookie = new ReplicationCookie(domainNamingContext);
            var schema = BasicSchemaFactory.CreateSchema();
            do
            {
                result = this.drsConnection.ReplicateAllObjects(cookie);
                foreach (var obj in result.Objects)
                {
                    obj.Schema = schema;
                    if(!obj.IsAccount)
                    {
                        continue;
                    }
                    var account = new DSAccount(obj, this.SecretDecryptor);
                    yield return account;
                }
                cookie = result.Cookie;
            } while (result.HasMoreData);
        }

        public DSAccount GetAccount(Guid objectGuid)
        {
            var obj = this.drsConnection.ReplicateSingleObject(objectGuid);
            var schema = BasicSchemaFactory.CreateSchema();
            obj.Schema = schema;
            return new DSAccount(obj, this.SecretDecryptor);
        }

        public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(string domainNamingContext)
        {
            // TODO: Move schema as a property
            // TODO: Split this function into RSA and Legacy Part so that exception in one of them does not crash the whole process
            var schema = BasicSchemaFactory.CreateSchema();

            string rsaPointerDN = DPAPIBackupKey.GetPreferredRSAKeyPointerDN(domainNamingContext);
            var rsaPointer = this.GetLSASecret(rsaPointerDN, schema);
            yield return rsaPointer;

            string rsaKeyDN = DPAPIBackupKey.GetKeyDN(rsaPointer.KeyId, domainNamingContext);
            var rsaKey = this.GetLSASecret(rsaKeyDN, schema);
            yield return rsaKey;

            string legacyPointerDN = DPAPIBackupKey.GetPreferredLegacyKeyPointerDN(domainNamingContext);
            var legacyPointer = this.GetLSASecret(legacyPointerDN, schema);
            yield return legacyPointer;

            string legacyKeyDN = DPAPIBackupKey.GetKeyDN(legacyPointer.KeyId, domainNamingContext);
            var legacyKey = this.GetLSASecret(legacyKeyDN, schema);
            yield return legacyKey;
        }

        private DPAPIBackupKey GetLSASecret(string distinguishedName, BasicSchema schema)
        {
            var secretObj = this.drsConnection.ReplicateSingleObject(distinguishedName);
            secretObj.Schema = schema;
            return new DPAPIBackupKey(secretObj, this.SecretDecryptor);
        }

        public DSAccount GetAccount(string distinguishedName)
        {
            var obj = this.drsConnection.ReplicateSingleObject(distinguishedName);
            // TODO: Extract?
            var schema = BasicSchemaFactory.CreateSchema();
            obj.Schema = schema;
            return new DSAccount(obj, this.SecretDecryptor);
        }

        public DSAccount GetAccount(NTAccount accountName)
        {
            Guid objectGuid = this.drsConnection.ResolveGuid(accountName);
            return this.GetAccount(objectGuid);
        }

        public DSAccount GetAccount(SecurityIdentifier sid)
        {
            Guid objectGuid = this.drsConnection.ResolveGuid(sid);
            return this.GetAccount(objectGuid);
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
            switch(protocol)
            {
                case RpcProtocol.TCP:
                    binding = new EndpointBindingInfo(RpcProtseq.ncacn_ip_tcp, server, null);
                    break;
                case RpcProtocol.SMB:
                    binding = new EndpointBindingInfo(RpcProtseq.ncacn_np, server, DrsNamedPipeName);
                    if(credential != null)
                    {
                        // Connect named pipe
                        this.npConnection = new NamedPipeConnection(server, credential);
                    }
                    break;
                default:
                    // TODO: Custom exception type
                    // TODO: Extract as string
                    throw new Exception("Unsupported RPC protocol");
            }
            NetworkCredential rpcCredential = credential ?? Client.Self;
            this.rpcConnection = new NativeClient(binding);
            this.rpcConnection.AuthenticateAs(rpcCredential);
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
            if(this.npConnection != null)
            {
                this.npConnection.Dispose();
                this.npConnection = null;
            }
        }
    }
}
