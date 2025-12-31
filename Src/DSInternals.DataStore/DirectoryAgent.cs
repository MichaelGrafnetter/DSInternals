using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Kerberos;
using DSInternals.Common.Schema;
using Microsoft.Database.Isam;

namespace DSInternals.DataStore;

public partial class DirectoryAgent : IDisposable
{
    // 2^30
    public const int RidMax = 1 << 30;

    // TODO: SidCompatibilityVersion?
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
        DNTag? domainNC = this.context.DomainController.DomainNamingContextDNT;

        if (!domainNC.HasValue)
        {
            // The database does not contain any domain partitions.
            yield break;
        }

        bool sidProtectorSupported =
            propertySets.HasFlag(AccountPropertySets.WindowsLAPS) &&
            this.context.DomainController.ForestMode >= FunctionalLevel.Win2012;

        // Fetch all KDS root keys to decrypt encrypted LAPS passwords
        IKdsRootKeyResolver rootKeyResolver = propertySets.HasFlag(AccountPropertySets.WindowsLAPS) ? new KdsRootKeyCache(new DatastoreRootKeyResolver(this.context), preloadCache: true) : null;

        var pek = this.GetSecretDecryptor(bootKey);

        // Find all objects in the domain partition with the right sAMAccountType
        using (var cursor = this.context.OpenDataTable())
        {
            // Use the multi-column NC_Acc_Type_Sid index, without specifying the account name
            cursor.CurrentIndex = DirectorySchema.PartitionedAccountSidIndex;
            Key keyStart = Key.ComposeWildcard(domainNC, SamAccountType.User);
            Key keyEnd = Key.ComposeWildcard(domainNC, SamAccountType.Trust);
            // SamAccountType.Computer is between User and Trust
            cursor.FindRecordsBetween(keyStart, BoundCriteria.Inclusive, keyEnd, BoundCriteria.Inclusive);

            while (cursor.MoveNext())
            {
                var databaseObject = new DatastoreObject(cursor, this.context);
                var account = AccountFactory.CreateAccount(databaseObject, this.context.DomainController.NetBIOSDomainName, pek, rootKeyResolver, propertySets);
                yield return account;
            }
        }
    }

    public DSAccount GetAccount(DistinguishedName dn, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        // This throws exception if the DN does not get resolved to DNT:
        DNTag dnTag = this.context.DistinguishedNameResolver.Resolve(dn);

        using (var cursor = this.context.OpenDataTable())
        {
            cursor.CurrentIndex = DirectorySchema.DistinguishedNameTagIndex;
            bool found = cursor.GotoKey(Key.Compose(dnTag));

            if (found)
            {
                var databaseObject = new DatastoreObject(cursor, this.context);
                return this.GetAccount(databaseObject, dn, bootKey, propertySets);
            }
            else
            {
                // This should not happen as we have already resolved the DN tag
                throw new DirectoryObjectNotFoundException(dn);
            }
        }
    }

    public DSAccount GetAccount(SecurityIdentifier objectSid, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentNullException.ThrowIfNull(objectSid);

        DNTag domainNC = this.context.DomainController.DomainNamingContextDNT ??
            throw new DirectoryObjectNotFoundException(objectSid);

        using (var cursor = this.context.OpenDataTable())
        {
            // Use the multi-column NC_Acc_Type_Sid index
            cursor.CurrentIndex = DirectorySchema.PartitionedAccountSidIndex;
            byte[] binarySid = objectSid.GetBinaryForm(bigEndianRid: true);

            bool found =
                cursor.GotoKey(Key.Compose(domainNC, SamAccountType.User, binarySid)) ||
                cursor.GotoKey(Key.Compose(domainNC, SamAccountType.Computer, binarySid)) ||
                cursor.GotoKey(Key.Compose(domainNC, SamAccountType.Trust, binarySid));

            if (found)
            {
                var databaseObject = new DatastoreObject(cursor, this.context);
                return this.GetAccount(databaseObject, objectSid, bootKey, propertySets);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(objectSid);
            }
        }
    }

    public DSAccount GetAccount(string samAccountName, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentNullException.ThrowIfNull(samAccountName);

        DNTag domainNC = this.context.DomainController.DomainNamingContextDNT ??
            throw new DirectoryObjectNotFoundException(samAccountName);

        using (var cursor = this.context.OpenDataTable())
        {
            // Use the multi-column NC_Acc_Type_Name index
            cursor.CurrentIndex = DirectorySchema.PartitionedAccountNameIndex;

            // Do a lookup per all possible account types
            foreach (SamAccountType accountType in new[] { SamAccountType.User, SamAccountType.Computer, SamAccountType.Trust })
            {
                cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(domainNC, accountType, samAccountName));

                while (cursor.MoveNext())
                {
                    var databaseObject = new DatastoreObject(cursor, this.context);

                    // Account name uniqueness is only true for objects that are not deleted/recycled.
                    if (!databaseObject.IsDeleted)
                    {
                        return this.GetAccount(databaseObject, samAccountName, bootKey, propertySets);
                    }
                }
            }
        }

        // We still have not found the right object.
        throw new DirectoryObjectNotFoundException(samAccountName);
    }

    public DSAccount GetAccount(Guid objectGuid, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        DNTag domainNC = this.context.DomainController.DomainNamingContextDNT ??
            throw new DirectoryObjectOperationException("The database does not contain any domain partitions.", objectGuid);

        using (var cursor = this.context.OpenDataTable())
        {
            // Use the multi-column nc_guid_Index index
            cursor.CurrentIndex = DirectorySchema.PartitionedGuidIndex;
            byte[] binaryGuid = objectGuid.ToByteArray();
            bool found = cursor.GotoKey(Key.Compose(domainNC, binaryGuid));

            if (found)
            {
                var databaseObject = new DatastoreObject(cursor, this.context);
                return this.GetAccount(databaseObject, objectGuid, bootKey, propertySets);
            }
            else
            {
                throw new DirectoryObjectNotFoundException(objectGuid);
            }
        }
    }

    protected DSAccount GetAccount(DatastoreObject foundObject, object objectIdentifier, byte[] bootKey, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        var pek = GetSecretDecryptor(bootKey);

        // Fetch KDS root keys to decrypt encrypted LAPS passwords
        IKdsRootKeyResolver rootKeyResolver = propertySets.HasFlag(AccountPropertySets.WindowsLAPS) ? new KdsRootKeyCache(new DatastoreRootKeyResolver(context), preloadCache: false) : null;

        var account = AccountFactory.CreateAccount(foundObject, this.context.DomainController.NetBIOSDomainName, pek, rootKeyResolver, propertySets);

        if (account == null)
        {
            throw new DirectoryObjectOperationException("The object is not a security principal.", objectIdentifier);
        }

        return account;
    }

    public IEnumerable<TrustedDomain> GetTrusts(byte[]? bootKey)
    {
        var pek = this.GetSecretDecryptor(bootKey);

        DNTag? trustObjectCategory = this.context.Schema.FindObjectCategory(ClassType.TrustedDomain);

        if (trustObjectCategory == null)
        {
            // This must be initial or ADAM database.
            yield break;
        }

        foreach (var trustObject in this.FindObjectsByCategory(trustObjectCategory.Value))
        {
            yield return new TrustedDomain(
                trustObject,
                this.context.DomainController.DomainName,
                this.context.DomainController.NetBIOSDomainName,
                pek);
        }
    }

    public IEnumerable<DirectoryObject> FindObjectsByCategory(DNTag objectCategory, bool includeReadOnly = false, bool includeDeleted = false, bool includePhantoms = false)
    {
        using (var cursor = this.context.OpenDataTable())
        {
            // Find all objects with the right objectCategory
            string objectCategoryIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.ObjectCategory);
            cursor.CurrentIndex = objectCategoryIndex;
            cursor.FindRecords(MatchCriteria.EqualTo, Key.Compose(objectCategory));

            while (cursor.MoveNext())
            {
                var databaseObject = new DatastoreObject(cursor, this.context);

                // Optionally skip deleted objects
                if (!includeDeleted && databaseObject.IsDeleted)
                {
                    continue;
                }

                // Optionally skip phantom objects
                if (!includePhantoms && databaseObject.IsPhantomObject)
                {
                    continue;
                }

                // Optionally skip read-only objects
                if (!includeReadOnly && !databaseObject.IsWritable)
                {
                    continue;
                }

                yield return databaseObject;
            }
        }
    }

    public bool AddSidHistory(DistinguishedName dn, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(dn);
            return this.AddSidHistory(obj, dn, sidHistory, skipMetaUpdate);
        }
    }

    public bool AddSidHistory(string samAccountName, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {

            var obj = this.FindObject(samAccountName);
            return this.AddSidHistory(obj, samAccountName, sidHistory, skipMetaUpdate);
        }
    }

    public bool AddSidHistory(SecurityIdentifier objectSid, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectSid);
            return this.AddSidHistory(obj, objectSid, sidHistory, skipMetaUpdate);
        }
    }

    public bool AddSidHistory(Guid objectGuid, SecurityIdentifier[] sidHistory, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectGuid);
            return this.AddSidHistory(obj, objectGuid, sidHistory, skipMetaUpdate);
        }
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
        string samAccountNameIndex = this.context.Schema.FindIndexName(CommonDirectoryAttributes.SamAccountName);
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
        DNTag dnTag = this.context.DistinguishedNameResolver.Resolve(dn);
        return this.FindObject(dnTag);
    }

    public DatastoreObject FindObject(DNTag dnTag)
    {
        string dntIndex = DirectorySchema.DistinguishedNameTagIndex;
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
        return this.FindObject(CommonDirectoryAttributes.ObjectGuid, attributeValue);
    }

    /// <exception cref="DirectoryObjectNotFoundException"></exception>
    protected DatastoreObject FindObject(string attributeName, Guid attributeValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(attributeName);

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
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectGuid);
            obj.Delete();
        }
    }

    public void RemoveObject(DistinguishedName dn)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(dn);
            obj.Delete();
        }
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
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(dn);
            return this.SetAccountControl(obj, dn, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }
    }

    public bool SetAccountControl(string samAccountName, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetAccountControl(obj, samAccountName, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }
    }

    public bool SetAccountControl(SecurityIdentifier objectSid, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectSid);
            return this.SetAccountControl(obj, objectSid, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }
    }

    public bool SetAccountControl(Guid objectGuid, bool? enabled, bool? cannotChangePassword, bool? passwordNeverExpires, bool? smartcardLogonRequired, bool? useDESKeyOnly, bool? homedirRequired, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetAccountControl(obj, objectGuid, enabled, cannotChangePassword, passwordNeverExpires, smartcardLogonRequired, useDESKeyOnly, homedirRequired, skipMetaUpdate);
        }
    }

    public bool UnlockAccount(DistinguishedName dn, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(dn);
            return this.UnlockAccount(obj, dn, skipMetaUpdate);
        }
    }

    public bool UnlockAccount(string samAccountName, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(samAccountName);
            return this.UnlockAccount(obj, samAccountName, skipMetaUpdate);
        }
    }

    public bool UnlockAccount(SecurityIdentifier objectSid, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectSid);
            return this.UnlockAccount(obj, objectSid, skipMetaUpdate);
        }
    }

    public bool UnlockAccount(Guid objectGuid, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectGuid);
            return this.UnlockAccount(obj, objectGuid, skipMetaUpdate);
        }
    }

    public bool SetPrimaryGroupId(DistinguishedName dn, int groupId, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(dn);
            return this.SetPrimaryGroupId(obj, dn, groupId, skipMetaUpdate);
        }
    }

    public bool SetPrimaryGroupId(string samAccountName, int groupId, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(samAccountName);
            return this.SetPrimaryGroupId(obj, samAccountName, groupId, skipMetaUpdate);
        }
    }

    public bool SetPrimaryGroupId(SecurityIdentifier objectSid, int groupId, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {

            var obj = this.FindObject(objectSid);
            return this.SetPrimaryGroupId(obj, objectSid, groupId, skipMetaUpdate);
        }
    }

    public bool SetPrimaryGroupId(Guid objectGuid, int groupId, bool skipMetaUpdate)
    {
        lock (this.dataTableCursor)
        {
            var obj = this.FindObject(objectGuid);
            return this.SetPrimaryGroupId(obj, objectGuid, groupId, skipMetaUpdate);
        }
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
                throw new DirectoryObjectOperationException("The object is not a security principal.", targetObjectIdentifier);
        }

        using (var transaction = this.context.BeginTransaction())
        {
            this.dataTableCursor.BeginEditForUpdate();
            bool hasChanged = targetObject.AddAttribute(CommonDirectoryAttributes.SidHistory, sidHistory);
            this.CommitAttributeUpdate(targetObject, CommonDirectoryAttributes.SidHistory, transaction, hasChanged, skipMetaUpdate);
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
                throw new DirectoryObjectOperationException("The object is not an account.", targetObjectIdentifier);
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
            throw new DirectoryObjectOperationException("The object is not an account.", targetObjectIdentifier);
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
            throw new DirectoryObjectOperationException("The object is not an account.", targetObjectIdentifier);
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
