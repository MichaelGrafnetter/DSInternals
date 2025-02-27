namespace DSInternals.DataStore
{
    using System;
    using System.Collections.Generic;
    using DSInternals.Common;
    using DSInternals.Common.Data;

    public partial class DirectoryAgent : IDisposable
    {
        #region Data Protection API (DPAPI)
        public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(byte[] bootKey)
        {
            Validator.AssertNotNull(bootKey, "bootKey");
            var pek = this.GetSecretDecryptor(bootKey);

            foreach (var secret in this.FindObjectsByCategory(CommonDirectoryClasses.Secret))
            {
                // RODCs and partial replicas on GCs do not contain secrets
                if (secret.IsWritable)
                {
                    yield return new DPAPIBackupKey(secret, pek);
                }
            }
        }
        #endregion Data Protection API (DPAPI)

        #region DPAPI NG / Group Key Distribution Service
        public IEnumerable<KdsRootKey> GetKdsRootKeys()
        {
            // TODO: Test if schema contains the ms-Kds-Prov-RootKey class.
            foreach (var keyObject in this.FindObjectsByCategory(CommonDirectoryClasses.KdsRootKey))
            {
                if (keyObject.IsWritable)
                {
                    // RODCs do not contain key values
                    yield return new KdsRootKey(keyObject);
                }
            }
        }

        public IEnumerable<GroupManagedServiceAccount> GetGroupManagedServiceAccounts(DateTime effectiveTime)
        {
            // Fetch all KDS root keys first.
            var rootKeys = new Dictionary<Guid, KdsRootKey>();
            KdsRootKey latestRootKey = null;

            foreach (var rootKey in this.GetKdsRootKeys())
            {
                // Some servers, like RODCs might not contain key values
                if (rootKey.KeyValue != null)
                {
                    // Allow the key to be found by ID
                    rootKeys.Add(rootKey.KeyId, rootKey);

                    // Check if this key is the newest found yet
                    if (rootKey.EffectiveTime <= effectiveTime && (latestRootKey == null || latestRootKey.CreationTime < rootKey.CreationTime))
                    {
                        latestRootKey = rootKey;
                    }
                }
            }

            // Now fetch all gMSAs and associate them with the KDS root keys
            // TODO: Test if schema contains the msDS-GroupManagedServiceAccount class.
            foreach (var gmsaObject in this.FindObjectsByCategory(CommonDirectoryClasses.GroupManagedServiceAccount))
            {
                var gmsa = new GroupManagedServiceAccount(gmsaObject);

                if (gmsa.ManagedPasswordId != null)
                {
                    DateTime nextPasswordChange = gmsa.PasswordLastSet.Value.AddDays(gmsa.ManagedPasswordInterval.Value);
                    KdsRootKey rootKeyToUse;
                    if (nextPasswordChange <= effectiveTime)
                    {
                        // The existing password has already expired, so generate the managed password based on the latest Root Key
                        rootKeyToUse = latestRootKey;
                    }
                    else
                    {
                        // Generate the managed password based on the Root Key currently associated with it
                        Guid associateRootKeyId = gmsa.ManagedPasswordId.RootKeyId;
                        rootKeys.TryGetValue(associateRootKeyId, out rootKeyToUse);
                    }

                    if (rootKeyToUse != null)
                    {
                        gmsa.CalculatePassword(rootKeyToUse, effectiveTime);
                    }
                }

                yield return gmsa;
            }
        }
        #endregion DPAPI NG / Group Key Distribution Service
    }
}
