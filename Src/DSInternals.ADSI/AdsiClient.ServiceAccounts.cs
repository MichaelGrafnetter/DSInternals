using System.DirectoryServices;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    private const string ServiceAccountsFilter = "(|(objectClass=msDS-GroupManagedServiceAccount)(objectClass=msDS-DelegatedManagedServiceAccount))";

    private static readonly string[] ServiceAccountProperties = [
        CommonDirectoryAttributes.DistinguishedName,
        CommonDirectoryAttributes.ObjectGuid,
        CommonDirectoryAttributes.ObjectSid,
        CommonDirectoryAttributes.SamAccountName,
        CommonDirectoryAttributes.UserAccountControl,
        CommonDirectoryAttributes.Description,
        CommonDirectoryAttributes.ServicePrincipalName,
        CommonDirectoryAttributes.SupportedEncryptionTypes,
        CommonDirectoryAttributes.PasswordLastSet,
        CommonDirectoryAttributes.WhenCreated,
        CommonDirectoryAttributes.IsDeleted,
        CommonDirectoryAttributes.ManagedPasswordId,
        CommonDirectoryAttributes.ManagedPasswordPreviousId,
        CommonDirectoryAttributes.ManagedPasswordInterval
    ];

    /// <summary>
    /// Retrieves all Group Managed Service Accounts (gMSAs) and Delegated Managed Service Accounts (dMSAs)
    /// from the directory and derives their managed passwords from the available KDS root keys.
    /// </summary>
    /// <param name="effectiveTime">
    /// The date and time at which the managed passwords should be calculated. When <see langword="null"/>,
    /// the interval ID stored in <c>msDS-ManagedPasswordId</c> is used as-is, with no password-cycle math.
    /// </param>
    /// <returns>An enumerable collection of <see cref="GroupManagedServiceAccount"/> objects.</returns>
    public IEnumerable<GroupManagedServiceAccount> GetGroupManagedServiceAccounts(DateTime? effectiveTime = null)
    {
        // Only resolve the latest KDS root key when an effective time is specified;
        // when it is not, the ID stored on each account already pins the root key to use.
        KdsRootKey? latestRootKey = effectiveTime.HasValue
            ? _kdsRootKeyResolver.GetKdsRootKey(effectiveTime.Value)
            : null;

        using var serviceAccountSearcher = new DirectorySearcher(
            _domainNamingContext,
            ServiceAccountsFilter,
            ServiceAccountProperties,
            SearchScope.Subtree)
        {
            CacheResults = false,
            PageSize = LdapPageSize
        };

        using var searchResults = serviceAccountSearcher.FindAll();

        foreach (SearchResult searchResult in searchResults)
        {
            var adsiObject = new AdsiObjectAdapter(searchResult);
            var gmsa = new GroupManagedServiceAccount(adsiObject);

            if (gmsa.ManagedPasswordId.HasValue)
            {
                KdsRootKey? rootKeyToUse;

                if (effectiveTime.HasValue)
                {
                    DateTime previousPasswordChange = gmsa.PasswordLastSet ?? gmsa.WhenCreated;
                    DateTime nextPasswordChange = previousPasswordChange.AddDays(gmsa.ManagedPasswordInterval ?? 30);
                    rootKeyToUse = nextPasswordChange <= effectiveTime.Value
                        ? latestRootKey
                        : _kdsRootKeyResolver.GetKdsRootKey(gmsa.ManagedPasswordId.Value.RootKeyId);
                }
                else
                {
                    // No effective time — use the root key bound to the stored ManagedPasswordId.
                    rootKeyToUse = _kdsRootKeyResolver.GetKdsRootKey(gmsa.ManagedPasswordId.Value.RootKeyId);
                }

                if (rootKeyToUse != null)
                {
                    gmsa.CalculatePassword(rootKeyToUse, effectiveTime);
                }
            }

            yield return gmsa;
        }
    }
}
