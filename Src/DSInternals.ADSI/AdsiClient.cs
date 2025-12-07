using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Net;
using DSInternals.Common;
using DSInternals.Common.Data;
using DSInternals.Common.Schema;

namespace DSInternals.ADSI;

public sealed class AdsiClient : IDisposable
{
    private const string NetbiosNameFilterFormat = "(&(objectCategory=crossRef)(nETBIOSName=*)(dnsroot={0}))";
    private static readonly string[] NetbiosNamePropertiesToLoad = [ CommonDirectoryAttributes.NetBIOSName ];
    private const string AccountsFilter = "(objectClass=user)";

    private DirectoryContext _directoryContext;
    private DirectoryEntry _domainNamingContext;
    private DirectoryEntry _configurationNamingContext;
    private string _dnsDomainName;
    private string _netbiosDomainName;
    private IKdsRootKeyResolver _kdsRootKeyResolver;

    /// <summary>
    /// Initializes a new instance of the AdsiClient class and establishes a connection to an Active Directory domain or
    /// directory server.
    /// </summary>
    /// <param name="server">The DNS name of the directory server to connect to. If null, the client connects to the default domain.</param>
    /// <param name="credential">The network credentials used to authenticate with the directory server. If null, the current user's credentials
    /// are used.</param>
    public AdsiClient(string server = null, NetworkCredential credential = null)
    {
        // Connect to the DC
        if (!String.IsNullOrEmpty(server))
        {
            _directoryContext = credential != null ?
                new(DirectoryContextType.DirectoryServer, server, credential.GetLogonName(), credential.Password) :
                new(DirectoryContextType.DirectoryServer, server);

            using (var dc = DomainController.GetDomainController(_directoryContext))
            {
                // Resolve the domain partition
                _domainNamingContext = dc.Domain.GetDirectoryEntry();
                _dnsDomainName = dc.Domain.Name;
                
                // Resolve the configuration partition
                _configurationNamingContext = GetConfigurationNamingContext(dc);
            }
        }
        else
        {
            _directoryContext = credential != null ?
                new(DirectoryContextType.Domain, credential.GetLogonName(), credential.Password) :
                new(DirectoryContextType.Domain);

            // Resolve the domain partition
            using (var domain = Domain.GetDomain(_directoryContext))
            {
                _domainNamingContext = domain.GetDirectoryEntry();
                _dnsDomainName = domain.Name;

                // Resolve the configuration partition
                _configurationNamingContext = GetConfigurationNamingContext(domain.PdcRoleOwner);
            }
        }

        // Fetch the domain NetBIOS name
        _netbiosDomainName = GetNetbiosDomainName();

        // Locate KDS root keys
        _kdsRootKeyResolver = new KdsRootKeyCache(new AdsiKdsRootKeyResolver(_configurationNamingContext));
    }

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
        var accountPropertiesToLoad = new List<string>();

        // Always frtch these attributes:
        accountPropertiesToLoad.AddRange([
            CommonDirectoryAttributes.ServicePrincipalName,
            CommonDirectoryAttributes.ObjectGuid,
            CommonDirectoryAttributes.ObjectSid,
            CommonDirectoryAttributes.SamAccountName,
            CommonDirectoryAttributes.SamAccountType,
            CommonDirectoryAttributes.UserAccountControl,
            CommonDirectoryAttributes.AdminCount,
            CommonDirectoryAttributes.SupportedEncryptionTypes,
            CommonDirectoryAttributes.PrimaryGroupId
            ]);

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
                    var account = AccountFactory.CreateAccount(adsiObject, _netbiosDomainName, pek: null, _kdsRootKeyResolver, propertySets);

                    if (account != null)
                    {
                        yield return account;
                    }
                }
            }
        }
    }

    #region IDisposable Support
    /// <summary>
    /// Releases all resources used by the current instance.
    /// </summary>
    /// <remarks>Call this method when you are finished using the object to free unmanaged resources and
    /// perform other cleanup operations. After calling Dispose, the object should not be used.</remarks>
    public void Dispose()
    {
        _domainNamingContext?.Dispose();
        _domainNamingContext = null;

        _configurationNamingContext?.Dispose();
        _configurationNamingContext = null;
    }
    #endregion

    /// <summary>
    /// Retrieves the NetBIOS domain name corresponding to the current DNS domain name from Active Directory.
    /// </summary>
    /// <remarks>This method queries Active Directory using the configured naming context and DNS domain name.
    /// The returned NetBIOS name is typically used for legacy compatibility or environments where short domain names
    /// are required.</remarks>
    /// <returns>A string containing the NetBIOS domain name associated with the configured DNS domain name.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the NetBIOS domain name cannot be found for the current DNS domain name.</exception>
    private string GetNetbiosDomainName()
    {
        string netbiosNameFilter = string.Format(NetbiosNameFilterFormat, _dnsDomainName);

        using (var netbiosNameSearcher = new DirectorySearcher(_configurationNamingContext, netbiosNameFilter, NetbiosNamePropertiesToLoad, SearchScope.Subtree))
        {
            netbiosNameSearcher.CacheResults = false;

            // There can only be one matching object
            SearchResult searchResult = netbiosNameSearcher.FindOne();

            if (searchResult == null)
            {
                throw new InvalidOperationException($"Could not locate the NetBIOS name for domain '{_dnsDomainName}'.");
            }

            return searchResult.Properties[CommonDirectoryAttributes.NetBIOSName][0].ToString();
        }
    }

    /// <summary>
    /// Retrieves the configuration naming context entry from the specified domain controller.
    /// </summary>
    /// <remarks>The returned <see cref="DirectoryEntry"/> must be disposed by the caller when no longer
    /// needed. This method traverses the directory hierarchy to locate the configuration naming context associated with
    /// the provided domain controller.</remarks>
    /// <param name="dc">The domain controller from which to obtain the configuration naming context. Cannot be null.</param>
    /// <returns>A <see cref="DirectoryEntry"/> representing the configuration naming context of the domain controller.</returns>
    private static DirectoryEntry GetConfigurationNamingContext(DomainController dc)
    {
        DirectoryEntry currentEntry = dc.GetDirectoryEntry();

        // Locate the configuration naming context by traversing upwards from the child entry.
        do
        {
            DirectoryEntry childEntry = currentEntry;
            try
            {
                currentEntry = currentEntry.Parent;
            }
            finally
            {
                childEntry.Dispose();
            }
        } while (currentEntry.SchemaClassName != CommonDirectoryClasses.Configuration);

        return currentEntry;
    }
}
