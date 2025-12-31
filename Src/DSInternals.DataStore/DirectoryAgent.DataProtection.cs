using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.DataStore;
public partial class DirectoryAgent : IDisposable
{
    #region Data Protection API (DPAPI)
    public IEnumerable<DPAPIBackupKey> GetDPAPIBackupKeys(byte[] bootKey)
    {
        ArgumentNullException.ThrowIfNull(bootKey);
        var pek = this.GetSecretDecryptor(bootKey);

        DNTag? secretObjectCategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.Secret);

        if (!secretObjectCategory.HasValue)
        {
            // The schema does not even support secrets
            yield break;
        }

        foreach (var secret in this.FindObjectsByCategory(secretObjectCategory.Value))
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
    public IEnumerable<GroupManagedServiceAccount> GetGroupManagedServiceAccounts(DateTime effectiveTime)
    {
        // Support for gMSAs has been added in Windows Server 2012
        DNTag? gMSAcategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.GroupManagedServiceAccount);

        // Support for dMSAs has been added in Windows Server 2025
        DNTag? dMSAcategory = this.context.Schema.FindObjectCategory(CommonDirectoryClasses.DelegatedManagedServiceAccount);

        if (!gMSAcategory.HasValue)
        {
            // The AD schema does not support gMSAs, on which dMSAs depend as well.
            yield break;
        }

        var rootKeyResolver = new KdsRootKeyCache(new DatastoreRootKeyResolver(this.context), preloadCache: true);
        KdsRootKey? latestRootKey = rootKeyResolver.GetKdsRootKey(effectiveTime);

        var gmsaObjects = this.FindObjectsByCategory(gMSAcategory.Value);

        if (dMSAcategory.HasValue)
        {
            // dMSAs are an extension of gMSAs, so we can treat them the same.
            var dmsaObjects = this.FindObjectsByCategory(dMSAcategory.Value);
            gmsaObjects = gmsaObjects.Concat(dmsaObjects);
        }

        // Now fetch all gMSAs and dMSAs and associate them with the KDS root keys.
        foreach (var gmsaObject in gmsaObjects)
        {
            var gmsa = new GroupManagedServiceAccount(gmsaObject);

            if (gmsa.ManagedPasswordId.HasValue)
            {
                DateTime nextPasswordChange = gmsa.PasswordLastSet.Value.AddDays(gmsa.ManagedPasswordInterval.Value);
                KdsRootKey? rootKeyToUse;
                if (nextPasswordChange <= effectiveTime)
                {
                    // The existing password has already expired, so generate the managed password based on the latest Root Key
                    rootKeyToUse = latestRootKey;
                }
                else
                {
                    // Generate the managed password based on the Root Key currently associated with it
                    Guid associateRootKeyId = gmsa.ManagedPasswordId.Value.RootKeyId;
                    rootKeyToUse = rootKeyResolver.GetKdsRootKey(associateRootKeyId);
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
