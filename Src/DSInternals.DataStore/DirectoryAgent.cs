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

        public IEnumerable<DSAccount> GetAccounts(byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
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
                if (obj.IsDeleted || !obj.IsWritable)
                {
                    // Ignore deleted and non-writable objects (GC).
                    // TODO: Use an index with NC.
                    continue;
                }

                var account = AccountFactory.CreateAccount(obj, this.context.DomainController.NetBIOSDomainName, pek, propertySets);

                if (account == null)
                {
                    // Note: Factory returns null for objects that are not accounts.
                    continue;
                }

                yield return account;
            }
        }

        public DSAccount GetAccount(DistinguishedName dn, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            var obj = this.FindObject(dn);
            return this.GetAccount(obj, dn, bootKey, propertySets);
        }

        public DSAccount GetAccount(SecurityIdentifier objectSid, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            var obj = this.FindObject(objectSid);
            return this.GetAccount(obj, objectSid, bootKey, propertySets);
        }

        public DSAccount GetAccount(string samAccountName, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            var obj = this.FindObject(samAccountName);
            return this.GetAccount(obj, samAccountName, bootKey, propertySets);
        }

        public DSAccount GetAccount(Guid objectGuid, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            var obj = this.FindObject(objectGuid);
            return this.GetAccount(obj, objectGuid, bootKey, propertySets);
        }

        protected DSAccount GetAccount(DatastoreObject foundObject, object objectIdentifier, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            
            var pek = GetSecretDecryptor(bootKey);

            var account = AccountFactory.CreateAccount(foundObject, this.context.DomainController.NetBIOSDomainName, pek, propertySets);

            if (account == null)
            {
                throw new DirectoryObjectOperationException(Resources.ObjectNotSecurityPrincipalMessage, objectIdentifier);
            }

            return account;
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
        public DatastoreObject FindObject(Guid attributeValue)
        {
            return this.FindObject(CommonDirectoryAttributes.ObjectGUID, attributeValue);
        }

        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        protected DatastoreObject FindObject(string attributeName, Guid attributeValue)
        {
            Validator.AssertNotNullOrEmpty(attributeName, nameof(attributeName));

            // TODO: Check if the attribute has an index

            string attributeIndex = this.context.Schema.FindIndexName(attributeName);
            this.dataTableCursor.CurrentIndex = attributeIndex;
            bool found = this.dataTableCursor.GotoKey(Key.Compose(attributeValue.ToByteArray()));

            if (found)
            {
                return new DatastoreObject(this.dataTableCursor, this.context);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(attributeValue);
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
            return this.SetAccountControl(dn, enabled, null, null, null, null, null, skipMetaUpdate);
        }

        public bool SetAccountStatus(string samAccountName, bool enabled, bool skipMetaUpdate)
        {
            return this.SetAccountControl(samAccountName, enabled, null, null, null, null, null, skipMetaUpdate);
        }

        public bool SetAccountStatus(SecurityIdentifier objectSid, bool enabled, bool skipMetaUpdate)
        {
            return this.SetAccountControl(objectSid, enabled, null, null, null, null, null, skipMetaUpdate);
        }

        public bool SetAccountStatus(Guid objectGuid, bool enabled, bool skipMetaUpdate)
        {
            return this.SetAccountControl(objectGuid, enabled, null, null, null, null, null, skipMetaUpdate);
        }

        public bool SetAccountControl(DistinguishedName dn, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
        {
            var obj = this.FindObject(dn);
            return this.SetAccountControl(obj, dn, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }

        public bool SetAccountControl(string samAccountName, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetAccountControl(obj, samAccountName, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }

        public bool SetAccountControl(SecurityIdentifier objectSid, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectSid);
            return this.SetAccountControl(obj, objectSid, enabled,cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }

        public bool SetAccountControl(Guid objectGuid, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetAccountControl(obj, objectGuid, enabled,cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
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
            // Check if modification of SID history makes sense for this object type
            targetObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);
            switch (accountType)
            {
                case SamAccountType.User:
                case SamAccountType.Computer:
                case SamAccountType.SecurityGroup:
                case SamAccountType.Alias:
                    // OK, continue
                    break;
                default:
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

        protected bool SetAccountControl(DatastoreObject targetObject, object targetObjectIdentifier, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
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

                // Modify the flags
                bool? disabled = enabled.HasValue ? !enabled : null;
                uac.SetFlags(UserAccountControl.Disabled, disabled);
                uac.SetFlags(UserAccountControl.PasswordCantChange, cannotChangePassword);
                uac.SetFlags(UserAccountControl.PasswordNeverExpires, passwordNeverExpires);
                uac.SetFlags(UserAccountControl.SmartCardRequired, smartcardLogonRequired);
                uac.SetFlags(UserAccountControl.UseDesKeyOnly, useDESKeyOnly);
                uac.SetFlags(UserAccountControl.HomeDirRequired, homedirRequired);

                // Save the changes
                this.dataTableCursor.BeginEditForUpdate();
                bool hasChanged = targetObject.SetAttribute<int>(CommonDirectoryAttributes.UserAccountControl, (int?)uac);
                this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.UserAccountControl, transaction, hasChanged, skipMetaUpdate);
                return hasChanged;
            }
        }

        protected bool UnlockAccount(DatastoreObject targetObject, object targetObjectIdentifier, bool skipMetaUpdate)
        {
            // Check that the object is a user/computer account
            targetObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);

            if (accountType != SamAccountType.User && accountType != SamAccountType.Computer)
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
            // Check that the object is a user/computer account
            targetObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);

            if (accountType != SamAccountType.User && accountType != SamAccountType.Computer)
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
