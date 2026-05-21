using System.DirectoryServices;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    private const string AccountsFilter = "(objectClass=user)";

    /// <summary>
    /// Retrieves a collection of directory service accounts with properties specified by the provided property sets.
    /// </summary>
    /// <remarks>Some property sets may include attributes that are not available via LDAP, such as secret
    /// attributes, which will not be returned. The method always includes a core set of account attributes regardless
    /// of the specified property sets.</remarks>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the results. Defaults to <see
    /// cref="AccountPropertySets.All"/> to include all supported property sets.</param>
    /// <returns>An enumerable collection of <see cref="DSAccount"/> objects representing the accounts found in the directory.
    /// The collection will be empty if no accounts are found.</returns>
    public IEnumerable<DSAccount> GetAccounts(AccountPropertySets propertySets = AccountPropertySets.All)
    {
        // Not all property sets work as secret attributes are never sent ove LDAP.
        List<string> accountPropertiesToLoad = [
            CommonDirectoryAttributes.ServicePrincipalName,
            CommonDirectoryAttributes.ObjectGuid,
            CommonDirectoryAttributes.ObjectSid,
            CommonDirectoryAttributes.SamAccountName,
            CommonDirectoryAttributes.SamAccountType,
            CommonDirectoryAttributes.UserAccountControl,
            CommonDirectoryAttributes.AdminCount,
            CommonDirectoryAttributes.SupportedEncryptionTypes,
            CommonDirectoryAttributes.PrimaryGroupId
            ];

        if (propertySets.HasFlag(AccountPropertySets.DistinguishedName) || propertySets.HasFlag(AccountPropertySets.KeyCredentials))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.DistinguishedName);
        }

        if (propertySets.HasFlag(AccountPropertySets.WindowsLAPS))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsPasswordExpirationTime);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsPassword);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsCurrentPasswordVersion);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsEncryptedPassword);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsEncryptedPasswordHistory);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPassword);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsEncryptedDsrmPasswordHistory);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsCurrentPasswordVersion);
        }

        if (propertySets.HasFlag(AccountPropertySets.LegacyLAPS))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.LAPSPassword);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.LAPSPasswordExpirationTime);
        }

        if (propertySets.HasFlag(AccountPropertySets.GenericAccountInfo))
        {
            accountPropertiesToLoad.AddRange([
                CommonDirectoryAttributes.UserPrincipalName,
                CommonDirectoryAttributes.SidHistory,
                CommonDirectoryAttributes.LastLogon,
                CommonDirectoryAttributes.LastLogonTimestamp,
                CommonDirectoryAttributes.Description,
                CommonDirectoryAttributes.PasswordLastSet
            ]);
        }

        if (propertySets.HasFlag(AccountPropertySets.GenericUserInfo))
        {
            accountPropertiesToLoad.AddRange([
                CommonDirectoryAttributes.DisplayName,
                CommonDirectoryAttributes.GivenName,
                CommonDirectoryAttributes.Surname,
                CommonDirectoryAttributes.Initials,
                CommonDirectoryAttributes.EmployeeId,
                CommonDirectoryAttributes.EmployeeNumber,
                CommonDirectoryAttributes.EmailAddress,
                CommonDirectoryAttributes.Street,
                CommonDirectoryAttributes.City,
                CommonDirectoryAttributes.State,
                CommonDirectoryAttributes.PostalCode,
                CommonDirectoryAttributes.Country,
                CommonDirectoryAttributes.PostOfficeBox,
                CommonDirectoryAttributes.TelephoneNumber,
                CommonDirectoryAttributes.HomePhone,
                CommonDirectoryAttributes.Mobile,
                CommonDirectoryAttributes.PagerNumber,
                CommonDirectoryAttributes.IpPhone,
                CommonDirectoryAttributes.WebPage,
                CommonDirectoryAttributes.JobTitle,
                CommonDirectoryAttributes.Department,
                CommonDirectoryAttributes.Office,
                CommonDirectoryAttributes.Company,
                CommonDirectoryAttributes.HomeDirectory,
                CommonDirectoryAttributes.HomeDrive,
                CommonDirectoryAttributes.ProfilePath,
                CommonDirectoryAttributes.ScriptPath,
                CommonDirectoryAttributes.UnixHomeDirectory,
                CommonDirectoryAttributes.Comment
            ]);
        }

        if (propertySets.HasFlag(AccountPropertySets.GenericComputerInfo))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.Location);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.OperatingSystemName);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.OperatingSystemVersion);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.OperatingSystemHotfix);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.OperatingSystemServicePack);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.DnsHostName);
        }

        if (propertySets.HasFlag(AccountPropertySets.KeyCredentials))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.KeyCredentialLink);
        }

        if (propertySets.HasFlag(AccountPropertySets.Manager))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.Manager);
        }

        if (propertySets.HasFlag(AccountPropertySets.ManagedBy))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.ManagedBy);
        }

        if (propertySets.HasFlag(AccountPropertySets.RoamedCredentials))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.PKIAccountCredentials);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.PKIDPAPIMasterKeys);
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.PKIRoamingTimeStamp);
        }

        if (propertySets.HasFlag(AccountPropertySets.SecurityDescriptor))
        {
            accountPropertiesToLoad.Add(CommonDirectoryAttributes.SecurityDescriptor);
        }

        using (DirectorySearcher accountSearcher = new(_domainNamingContext, AccountsFilter, accountPropertiesToLoad.ToArray(), SearchScope.Subtree))
        {
            using (var searchResults = accountSearcher.FindAll())
            {
                foreach (SearchResult searchResult in searchResults)
                {
                    var adsiObject = new AdsiObjectAdapter(searchResult);
                    var account = AccountFactory.CreateAccount(adsiObject, _netbiosDomainName.Value, pek: null, _kdsRootKeyResolver, propertySets);

                    if (account != null)
                    {
                        yield return account;
                    }
                }
            }
        }
    }
}
