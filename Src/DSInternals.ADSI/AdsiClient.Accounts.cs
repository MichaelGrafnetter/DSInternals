using System.DirectoryServices;
using System.Globalization;
using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed partial class AdsiClient
{
    private const string AccountsFilter = "(objectClass=user)";
    private const string AccountBySamAccountNameFilterFormat = "(&(objectClass=user)(sAMAccountName={0}))";
    private const string AccountByUserPrincipalNameFilterFormat = "(&(objectClass=user)(userPrincipalName={0}))";
    private const string AccountByObjectGuidFilterFormat = "(&(objectClass=user)(objectGUID={0}))";
    private const string AccountByObjectSidFilterFormat = "(&(objectClass=user)(objectSid={0}))";
    private const string AccountByDistinguishedNameFilterFormat = "(&(objectClass=user)(distinguishedName={0}))";

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
        string[] propertiesToLoad = BuildAccountPropertiesToLoad(propertySets);

        using DirectorySearcher accountSearcher = new(_domainNamingContext, AccountsFilter, propertiesToLoad, SearchScope.Subtree);
        using SearchResultCollection searchResults = accountSearcher.FindAll();

        foreach (SearchResult searchResult in searchResults)
        {
            var account = CreateAccount(searchResult, propertySets);

            if (account != null)
            {
                yield return account;
            }
        }
    }

    /// <summary>
    /// Retrieves a single directory service account identified by its sAMAccountName.
    /// </summary>
    /// <param name="samAccountName">The sAMAccountName of the account to retrieve.</param>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the result.</param>
    /// <returns>A <see cref="DSAccount"/> object representing the matched account.</returns>
    /// <exception cref="DirectoryObjectNotFoundException">Thrown when no account with the specified identifier is found.</exception>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the matched object is not a security principal.</exception>
    public DSAccount GetAccount(string samAccountName, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentException.ThrowIfNullOrEmpty(samAccountName);

        string filter = string.Format(CultureInfo.InvariantCulture, AccountBySamAccountNameFilterFormat, EscapeLdapFilterString(samAccountName));
        return FindSingleAccount(filter, samAccountName, propertySets);
    }

    /// <summary>
    /// Retrieves a single directory service account identified by its userPrincipalName.
    /// </summary>
    /// <param name="userPrincipalName">The user principal name (UPN) of the account to retrieve.</param>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the result.</param>
    /// <returns>A <see cref="DSAccount"/> object representing the matched account.</returns>
    /// <exception cref="DirectoryObjectNotFoundException">Thrown when no account with the specified identifier is found.</exception>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the matched object is not a security principal.</exception>
    public DSAccount GetAccountByUpn(string userPrincipalName, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentException.ThrowIfNullOrEmpty(userPrincipalName);

        string filter = string.Format(CultureInfo.InvariantCulture, AccountByUserPrincipalNameFilterFormat, EscapeLdapFilterString(userPrincipalName));
        return FindSingleAccount(filter, userPrincipalName, propertySets);
    }

    /// <summary>
    /// Retrieves a single directory service account identified by its objectGUID.
    /// </summary>
    /// <param name="objectGuid">The objectGUID of the account to retrieve.</param>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the result.</param>
    /// <returns>A <see cref="DSAccount"/> object representing the matched account.</returns>
    /// <exception cref="DirectoryObjectNotFoundException">Thrown when no account with the specified identifier is found.</exception>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the matched object is not a security principal.</exception>
    public DSAccount GetAccount(Guid objectGuid, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        // Guid.ToString("D") only produces hex digits and hyphens, so no LDAP filter escaping is required.
        string filter = string.Format(CultureInfo.InvariantCulture, AccountByObjectGuidFilterFormat, objectGuid.ToString("D"));
        return FindSingleAccount(filter, objectGuid, propertySets);
    }

    /// <summary>
    /// Retrieves a single directory service account identified by its objectSid.
    /// </summary>
    /// <param name="objectSid">The security identifier (SID) of the account to retrieve.</param>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the result.</param>
    /// <returns>A <see cref="DSAccount"/> object representing the matched account.</returns>
    /// <exception cref="DirectoryObjectNotFoundException">Thrown when no account with the specified identifier is found.</exception>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the matched object is not a security principal.</exception>
    public DSAccount GetAccount(SecurityIdentifier objectSid, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentNullException.ThrowIfNull(objectSid);

        // SecurityIdentifier.Value produces an SDDL string ("S-1-5-..."), so no LDAP filter escaping is required.
        string filter = string.Format(CultureInfo.InvariantCulture, AccountByObjectSidFilterFormat, objectSid.Value);
        return FindSingleAccount(filter, objectSid, propertySets);
    }

    /// <summary>
    /// Retrieves a single directory service account identified by its distinguished name.
    /// </summary>
    /// <param name="distinguishedName">The distinguished name of the account to retrieve.</param>
    /// <param name="propertySets">A set of flags that determines which groups of account properties to include in the result.</param>
    /// <returns>A <see cref="DSAccount"/> object representing the matched account.</returns>
    /// <exception cref="DirectoryObjectNotFoundException">Thrown when no account with the specified identifier is found.</exception>
    /// <exception cref="DirectoryObjectOperationException">Thrown when the matched object is not a security principal.</exception>
    public DSAccount GetAccount(DistinguishedName distinguishedName, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        ArgumentNullException.ThrowIfNull(distinguishedName);

        string dnString = distinguishedName.ToString();
        string filter = string.Format(CultureInfo.InvariantCulture, AccountByDistinguishedNameFilterFormat, EscapeLdapFilterString(dnString));
        return FindSingleAccount(filter, dnString, propertySets);
    }

    private DSAccount FindSingleAccount(string filter, object identifier, AccountPropertySets propertySets)
    {
        string[] propertiesToLoad = BuildAccountPropertiesToLoad(propertySets);

        using DirectorySearcher accountSearcher = new(_domainNamingContext, filter, propertiesToLoad, SearchScope.Subtree);
        accountSearcher.CacheResults = false;
        SearchResult? searchResult = accountSearcher.FindOne();

        if (searchResult == null)
        {
            throw new DirectoryObjectNotFoundException(identifier);
        }

        var account = CreateAccount(searchResult, propertySets);

        if (account == null)
        {
            throw new DirectoryObjectOperationException("The object is not a security principal.", identifier);
        }

        return account;
    }

    private DSAccount? CreateAccount(SearchResult searchResult, AccountPropertySets propertySets)
    {
        var adsiObject = new AdsiObjectAdapter(searchResult);
        // Secret attributes are never sent over LDAP, so the pek argument is always null.
        return AccountFactory.CreateAccount(adsiObject, _netbiosDomainName.Value, pek: null, _kdsRootKeyResolver, propertySets);
    }

    private static string[] BuildAccountPropertiesToLoad(AccountPropertySets propertySets)
    {
        // Not all property sets work as secret attributes are never sent over LDAP.
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

        return accountPropertiesToLoad.ToArray();
    }
}
