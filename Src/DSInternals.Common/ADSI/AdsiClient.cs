namespace DSInternals.Common.ADSI
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.DirectoryServices.ActiveDirectory;
    using System.Linq;
    using System.Net;
    using DSInternals.Common.Data;

    public class AdsiClient : IDisposable
    {
        private const string ConfigurationContainerRDN = "CN=Configuration";
        private const string CrossRefContainerRDN = "LDAP://{0}/CN=Partitions,{1}";
        private const string NetBIOSNameFilter = "(&(objectCategory=crossRef)(nETBIOSName=*)(dnsroot={0}))";
        private static readonly string[] NetBIOSNamePropertiesToLoad = new string[] {
            CommonDirectoryAttributes.NetBIOSName
        };
        private const string AccountsFilter = "(objectClass=user)";

        private DirectoryEntry searchRoot;

        private static String GetNetBIOSDomainName(Domain domain, string server)
        {
            // Find the configuration naming context in the list of partitions
            var configDN = domain.PdcRoleOwner.Partitions.Cast<string>()
                .Where(partition => partition.ToString().StartsWith(ConfigurationContainerRDN))
                .FirstOrDefault();

            // Format the cross reference container DN using the user provided server, or if omitted, the domain name, and the configuration naming context DN
            var crossRefContainerDN = String.Format(CrossRefContainerRDN, (String.IsNullOrEmpty(server) ? server : domain.Name), configDN);

            // Search the cross reference container for a crossRef with a nETBIOSName and a matching dnsRoot 
            using (var searcher = new DirectorySearcher(new DirectoryEntry(crossRefContainerDN), string.Format(NetBIOSNameFilter, domain.Name), NetBIOSNamePropertiesToLoad, SearchScope.OneLevel))
            {
                searcher.CacheResults = false;
                // There can only be one matching object
                var searchResult = searcher.FindOne();
                return searchResult.Properties[CommonDirectoryAttributes.NetBIOSName][0].ToString();
            }
        }

        public AdsiClient(string server = null, NetworkCredential credential = null)
        {
            DirectoryContext context;
            if (!String.IsNullOrEmpty(server))
            {
                if (credential != null)
                {
                    context = new DirectoryContext(DirectoryContextType.DirectoryServer, server, credential.GetLogonName(), credential.Password);
                }
                else
                {
                    context = new DirectoryContext(DirectoryContextType.DirectoryServer, server);
                }
                using (var dc = DomainController.GetDomainController(context))
                {
                    using (var domain = dc.Domain)
                    {
                        this.searchRoot = domain.GetDirectoryEntry();
                        this.NetBIOSDomainName = GetNetBIOSDomainName(domain, server);
                    }
                }
            }
            else
            {
                // Discover the server for the current domain
                if (credential != null)
                {
                    context = new DirectoryContext(DirectoryContextType.Domain, credential.GetLogonName(), credential.Password);
                }
                else
                {
                    context = new DirectoryContext(DirectoryContextType.Domain);
                }
                using (var domain = Domain.GetDomain(context))
                {
                    this.searchRoot = domain.GetDirectoryEntry();
                    this.NetBIOSDomainName = GetNetBIOSDomainName(domain, server);
                }
            }
        }

        public string NetBIOSDomainName
        {
            get;
            private set;
        }

        public IEnumerable<DSAccount> GetAccounts(AccountPropertySets propertySets = AccountPropertySets.All)
        {
            // Not all property sets work as secret attributes are never sent ove LDAP.
            var accountPropertiesToLoad = new List<string>();

            // Always frtch these attributes:
            accountPropertiesToLoad.AddRange([
                CommonDirectoryAttributes.ServicePrincipalName,
                CommonDirectoryAttributes.ObjectGUID,
                CommonDirectoryAttributes.ObjectSid,
                CommonDirectoryAttributes.SAMAccountName,
                CommonDirectoryAttributes.SamAccountType,
                CommonDirectoryAttributes.UserAccountControl,
                CommonDirectoryAttributes.AdminCount,
                CommonDirectoryAttributes.SupportedEncryptionTypes,
                CommonDirectoryAttributes.PrimaryGroupId
             ]);

            if (propertySets.HasFlag(AccountPropertySets.DistinguishedName) || propertySets.HasFlag(AccountPropertySets.KeyCredentials))
            {
                accountPropertiesToLoad.Add(CommonDirectoryAttributes.DN);
            }

            if (propertySets.HasFlag(AccountPropertySets.WindowsLAPS))
            {
                accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsPasswordExpirationTime);
                accountPropertiesToLoad.Add(CommonDirectoryAttributes.WindowsLapsPassword);
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
                    CommonDirectoryAttributes.SIDHistory,
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
                    CommonDirectoryAttributes.EmployeeID,
                    CommonDirectoryAttributes.EmployeeNumber,
                    CommonDirectoryAttributes.Email,
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
                accountPropertiesToLoad.Add(CommonDirectoryAttributes.DNSHostName);
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

            using (var searcher = new DirectorySearcher(this.searchRoot, AccountsFilter, accountPropertiesToLoad.ToArray(), SearchScope.Subtree))
            {
                using (var searchResults = searcher.FindAll())
                {
                    foreach (var searchResult in searchResults.Cast<SearchResult>())
                    {
                        var obj = new AdsiObjectAdapter(searchResult);
                        var account = AccountFactory.CreateAccount(obj, this.NetBIOSDomainName, null);

                        if (account != null)
                        {
                            yield return account;
                        }
                    }
                }
            }
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.searchRoot != null)
                {
                    this.searchRoot.Dispose();
                    this.searchRoot = null;
                }
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
