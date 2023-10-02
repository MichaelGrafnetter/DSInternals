namespace DSInternals.DataStore
{
    using DSInternals.Common;
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Properties;
    using Microsoft.Database.Isam;
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;

    public partial class DirectoryAgent : IDisposable
    {
        // 2^30
        public const int RidMax = 1 << 30;

        //TODO: SidCompatibilityVersion?
        // TODO: Add Rid range checks
        public const int RidMin = 1;

        private DirectoryContext context;
        private Cursor dataTableCursor;
        private bool ownsContext;

        public DirectoryAgent(DirectoryContext context, bool ownsContext = false)
        {
            this.context = context;
            this.ownsContext = ownsContext;
            this.dataTableCursor = context.OpenDataTable();
        }

        public void SetDomainControllerEpoch(int epoch)
        {
            using (var transaction = this.context.BeginTransaction())
            {
                this.context.DomainController.Epoch = epoch;
                transaction.Commit(true);
            }
        }

        public void SetDomainControllerUsn(long highestCommittedUsn)
        {
            using (var transaction = this.context.BeginTransaction())
            {
                this.context.DomainController.HighestCommittedUsn = highestCommittedUsn;
                transaction.Commit(true);
            }
        }

        public void SetDomainControllerBackupExpiration(DateTime expirationTime)
        {
            using (var transaction = this.context.BeginTransaction())
            {
                this.context.DomainController.BackupExpiration = expirationTime;
                transaction.Commit(true);
            }
        }

        public IEnumerable<DSAccount> GetAccounts(byte[] bootKey)
        {
            var pek = this.GetSecretDecryptor(bootKey);
            // TODO: Use a more suitable index?
            string samAccountTypeIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.SamAccountType);
            this.dataTableCursor.CurrentIndex = samAccountTypeIndex;
            // Find all objects with the right sAMAccountType that are writable and not deleted:
            // TODO: Lock cursor?
            while (this.dataTableCursor.MoveNext())
            {
                var obj = new DatastoreObject(this.dataTableCursor, this.context);
                // TODO: This probably does not work on RODCs:
                if (obj.IsDeleted || !obj.IsWritable || !obj.IsAccount)
                {
                    continue;
                }
                yield return new DSAccount(obj, this.context.DomainController.NetBIOSDomainName, pek);
            }
        }

        public DSAccount GetAccount(DistinguishedName dn, byte[] bootKey)
        {
            var obj = this.FindObject(dn);
            return this.GetAccount(obj, dn, bootKey);
        }

        public DSAccount GetAccount(SecurityIdentifier objectSid, byte[] bootKey)
        {
            var obj = this.FindObject(objectSid);
            return this.GetAccount(obj, objectSid, bootKey);
        }

        public DSAccount GetAccount(string samAccountName, byte[] bootKey)
        {
            var obj = this.FindObject(samAccountName);
            return this.GetAccount(obj, samAccountName, bootKey);
        }

        public DSAccount GetAccount(Guid objectGuid, byte[] bootKey)
        {
            var obj = this.FindObject(objectGuid);
            return this.GetAccount(obj, objectGuid, bootKey);
        }

        protected DSAccount GetAccount(DatastoreObject foundObject, object objectIdentifier, byte[] bootKey)
        {
            if (!foundObject.IsAccount)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotSecurityPrincipalMessage, objectIdentifier);
            }

            var pek = GetSecretDecryptor(bootKey);
            return new DSAccount(foundObject, this.context.DomainController.NetBIOSDomainName, pek);
        }

        public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(byte[] bootKey)
        {
            Validator.AssertNotNull(bootKey, "bootKey");
            var pek = this.GetSecretDecryptor(bootKey);
            // TODO: Refactor using Linq
            foreach (var secret in this.FindObjectsByCategory(CommonDirectoryClasses.Secret))
            {
                // RODCs and partial replicas on GCs do not contain secrets
                if (secret.IsWritable)
                {
                    yield return new DPAPIBackupKey(secret, pek);
                }
            }
        }

        public IEnumerable<KdsRootKey> GetKdsRootKeys()
        {
            // TODO: Refactor using Linq
            // TODO: Test if schema contains the ms-Kds-Prov-RootKey class.
            foreach (var keyObject in this.FindObjectsByCategory(CommonDirectoryClasses.KdsRootKey))
            {
                yield return new KdsRootKey(keyObject);
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
                    if(rootKey.EffectiveTime <= effectiveTime && (latestRootKey == null || latestRootKey.CreationTime < rootKey.CreationTime))
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

        public IEnumerable<DirectoryObject> FindObjectsByCategory(string className, bool includeDeleted = false)
        {
            // Find all objects with the right objectCategory
            string objectCategoryIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.ObjectCategory);
            this.dataTableCursor.CurrentIndex = objectCategoryIndex;
            int classId = this.context.Schema.FindClassId(className);
            this.dataTableCursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(classId));
            // TODO: Lock cursor?
            while (this.dataTableCursor.MoveNext())
            {
                var obj = new DatastoreObject(this.dataTableCursor, this.context);
                // Optionally skip deleted objects
                if (!includeDeleted && obj.IsDeleted)
                {
                    continue;
                }
                yield return obj;
            }
        }

        public bool AddSidHistory(DistinguishedName dn, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.AddSidHistory(obj, dn, sidHistory, skipMetaUpdate);
        }

        public bool AddSidHistory(string samAccountName, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.AddSidHistory(obj, samAccountName, sidHistory, skipMetaUpdate);
        }

        public bool AddSidHistory(SecurityIdentifier objectSid, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.AddSidHistory(obj, objectSid, sidHistory, skipMetaUpdate);
        }

        public bool AddSidHistory(Guid objectGuid, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.AddSidHistory(obj, objectGuid, sidHistory, skipMetaUpdate);
        }

        public void AuthoritativeRestore(Guid objectGuid, string[] attributeNames)
        {
            // TODO: Implement attribute-level authorirative restore.
            // TODO: Check attribute names
            // TODO: Check attribute types (not linked and not system?)
            var obj = this.FindObject(objectGuid);
            throw new NotImplementedException();
        }

        public void AuthoritativeRestore(DistinguishedName dn, string[] attributeNames)
        {
            // TODO: Check attribute names
            // TODO: Check attribute types (not linked and not system?)
            this.FindObject(dn);
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public DatastoreObject FindObject(string samAccountName)
        {
            string samAccountNameIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.SAMAccountName);
            this.dataTableCursor.CurrentIndex = samAccountNameIndex;
            this.dataTableCursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(samAccountName));

            // Find first object with the right sAMAccountName, that is writable and not deleted:
            while (this.dataTableCursor.MoveNext())
            {
                var currentObject = new DatastoreObject(this.dataTableCursor, this.context);
                if (currentObject.IsWritable && !currentObject.IsDeleted)
                {
                    return currentObject;
                }
            }
            // If the code execution comes here, we have not found any object matching the criteria.
            throw new DirectoryObjectNotFoundException(samAccountName);
        }

        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public DatastoreObject FindObject(SecurityIdentifier objectSid)
        {
            string sidIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.ObjectSid);
            this.dataTableCursor.CurrentIndex = sidIndex;
            byte[] binarySid = objectSid.GetBinaryForm(true);
            bool found = this.dataTableCursor.GotoKey(Key.Compose(binarySid));
            if (found)
            {
                return new DatastoreObject(this.dataTableCursor, this.context);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(objectSid);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dn"></param>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public DatastoreObject FindObject(DistinguishedName dn)
        {
            // This throws exception if the DN does not get resolved to dnt:
            int dnTag = this.context.DistinguishedNameResolver.Resolve(dn);
            return this.FindObject(dnTag);
        }

        public DatastoreObject FindObject(int dnTag)
        {
            string dntIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.DNTag);
            this.dataTableCursor.CurrentIndex = dntIndex;
            bool found = this.dataTableCursor.GotoKey(Key.Compose(dnTag));
            if (found)
            {
                return new DatastoreObject(this.dataTableCursor, this.context);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(dnTag);
            }
        }


        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public DatastoreObject FindObject(Guid objectGuid)
        {
            string objectGuidIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.ObjectGUID);
            this.dataTableCursor.CurrentIndex = objectGuidIndex;
            bool found = this.dataTableCursor.GotoKey(Key.Compose(objectGuid.ToByteArray()));
            if (found)
            {
                return new DatastoreObject(this.dataTableCursor, this.context);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(objectGuid);
            }
        }

        public void RemoveObject(Guid objectGuid)
        {
            var obj = this.FindObject(objectGuid);
            obj.Delete();
        }

        public void RemoveObject(DistinguishedName dn)
        {
            var obj = this.FindObject(dn);
            obj.Delete();
        }

        public bool SetAccountStatus(DistinguishedName dn, bool enabled, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.SetAccountStatus(obj, dn, enabled, skipMetaUpdate);
        }

        public bool SetAccountStatus(string samAccountName, bool enabled, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetAccountStatus(obj, samAccountName, enabled, skipMetaUpdate);
        }

        public bool SetAccountStatus(SecurityIdentifier objectSid, bool enabled, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.SetAccountStatus(obj, objectSid, enabled, skipMetaUpdate);
        }

        public bool SetAccountStatus(Guid objectGuid, bool enabled, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetAccountStatus(obj, objectGuid, enabled, skipMetaUpdate);
        }

        public bool UnlockAccount(DistinguishedName dn, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.UnlockAccount(obj, dn, skipMetaUpdate);
        }

        public bool UnlockAccount(string samAccountName, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.UnlockAccount(obj, samAccountName, skipMetaUpdate);
        }

        public bool UnlockAccount(SecurityIdentifier objectSid, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.UnlockAccount(obj, objectSid, skipMetaUpdate);
        }

        public bool UnlockAccount(Guid objectGuid, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.UnlockAccount(obj, objectGuid, skipMetaUpdate);
        }

        public bool SetPrimaryGroupId(DistinguishedName dn, int groupId, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.SetPrimaryGroupId(obj, dn, groupId, skipMetaUpdate);
        }

        public bool SetPrimaryGroupId(string samAccountName, int groupId, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetPrimaryGroupId(obj, samAccountName, groupId, skipMetaUpdate);
        }

        public bool SetPrimaryGroupId(SecurityIdentifier objectSid, int groupId, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.SetPrimaryGroupId(obj, objectSid, groupId, skipMetaUpdate);
        }

        public bool SetPrimaryGroupId(Guid objectGuid, int groupId, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetPrimaryGroupId(obj, objectGuid, groupId, skipMetaUpdate);
        }

        protected bool AddSidHistory(DatastoreObject targetObject, object targetObjectIdentifier, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
        {
            if (!targetObject.IsSecurityPrincipal)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotSecurityPrincipalMessage, targetObjectIdentifier);
            }
            using (var transaction = this.context.BeginTransaction())
            {
                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = targetObject.AddAttribute(CommonDirectoryAttributes.SIDHistory, sidHistory);
                this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.SIDHistory, transaction, hasChanged, skipMetaUpdate);
                return hasChanged;
            }
        }

        protected void CommitAttributeUpdate(DatastoreObject obj, string attributeName, IsamTransaction transaction, bool hasChanged, bool skipMetaUpdate)
        {
            this.CommitAttributeUpdate(obj, new string[] { attributeName }, transaction, hasChanged, skipMetaUpdate);
        }

        protected void CommitAttributeUpdate(DatastoreObject obj, string[] attributeNames, IsamTransaction transaction, bool haveChanged, bool skipMetaUpdate)
        {
            if (haveChanged)
            {
                if (!skipMetaUpdate)
                {
                    // Increment the current USN
                    long currentUsn = ++this.context.DomainController.HighestCommittedUsn;
                    DateTime now = DateTime.Now;
                    obj.UpdateAttributeMeta(attributeNames, currentUsn, now);
                }
                this.dataTableCursor.AcceptChanges();
                transaction.Commit();
            }
            else
            {
                // No changes have been made to the object
                this.dataTableCursor.RejectChanges();
                transaction.Abort();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (this.dataTableCursor != null)
            {
                this.dataTableCursor.Dispose();
                this.dataTableCursor = null;
            }
            if (this.ownsContext && this.context != null)
            {
                this.context.Dispose();
                this.context = null;
            }
        }

        protected bool SetAccountStatus(DatastoreObject targetObject, object targetObjectIdentifier, bool enabled, bool skipMetaUpdate)
        {
            using (var transaction = this.context.BeginTransaction())
            {
                // Read the current value first. We do not want to touch any other flags.
                int? numericUac;
                targetObject.ReadAttribute(CommonDirectoryAttributes.UserAccountControl, out numericUac);

                if (!numericUac.HasValue)
                {
                    // This object does not have the userAccountControl attribute, so it probably is not an account.
                    throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, targetObjectIdentifier);
                }

                var uac = (UserAccountControl)numericUac.Value;
                if (enabled)
                {
                    // Clear the ADS_UF_ACCOUNTDISABLE flag
                    uac &= ~UserAccountControl.Disabled;
                }
                else
                {
                    // Set the ADS_UF_ACCOUNTDISABLE flag
                    uac |= UserAccountControl.Disabled;
                }

                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = targetObject.SetAttribute<int>(CommonDirectoryAttributes.UserAccountControl, (int?)uac);
                this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.UserAccountControl, transaction, hasChanged, skipMetaUpdate);
                return hasChanged;
            }
        }

        protected bool UnlockAccount(DatastoreObject targetObject, object targetObjectIdentifier, bool skipMetaUpdate)
        {
            if (!targetObject.IsAccount)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, targetObjectIdentifier);
            }

            using (var transaction = this.context.BeginTransaction())
            {
                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = targetObject.SetAttribute(CommonDirectoryAttributes.LockoutTime, DateTime.MinValue);

                // Even if the account had previously been unlocked locally,
                // the current unlock operation still needs to be made authoritative for other DCs.
                hasChanged = hasChanged || !skipMetaUpdate;

                this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.LockoutTime, transaction, hasChanged, skipMetaUpdate);
                return hasChanged;
            }
        }

        protected bool SetPrimaryGroupId(DatastoreObject targetObject, object targetObjectIdentifier, int groupId, bool skipMetaUpdate)
        {
            if (!targetObject.IsAccount)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotAccountMessage, targetObjectIdentifier);
            }
            // TODO: Validator.ValidateRid
            // TODO: Test if the rid exists?
            using (var transaction = this.context.BeginTransaction())
            {
                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = targetObject.SetAttribute<int>(CommonDirectoryAttributes.PrimaryGroupId, groupId);
                this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.PrimaryGroupId, transaction, hasChanged, skipMetaUpdate);
                return hasChanged;
            }
        }
    }
}
