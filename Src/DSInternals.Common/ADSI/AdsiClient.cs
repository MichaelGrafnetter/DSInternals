namespace DSInternals.Common.ADSI
{
    using DSInternals.Common.Data;
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.DirectoryServices.ActiveDirectory;
    using System.Linq;
    using System.Net;

    public class AdsiClient : IDisposable
    {
        private const string ConfigurationContainerRDN = "CN=Configuration";
        private const string CrossRefContainerRDN = "LDAP://{0}/CN=Partitions,{1}";
        private const string NetBIOSNameFilter = "(&(objectCategory=crossRef)(nETBIOSName=*)(dnsroot={0}))";
        private static readonly string[] NetBIOSNamePropertiesToLoad = new string[] {
            CommonDirectoryAttributes.NetBIOSName
        };
        private const string AccountsFilter = "(objectClass=user)";
        private static readonly string[] AccountPropertiesToLoad = new string[] {
            CommonDirectoryAttributes.CommonName,
            CommonDirectoryAttributes.DN,
            CommonDirectoryAttributes.DisplayName,
            CommonDirectoryAttributes.ServicePrincipalName,
            CommonDirectoryAttributes.UserPrincipalName,
            CommonDirectoryAttributes.ObjectGUID,
            CommonDirectoryAttributes.ObjectSid,
            CommonDirectoryAttributes.SAMAccountName,
            CommonDirectoryAttributes.SamAccountType,
            CommonDirectoryAttributes.AdminCount,
            CommonDirectoryAttributes.UserAccountControl,
            CommonDirectoryAttributes.SupportedEncryptionTypes,
            CommonDirectoryAttributes.LastLogon,
            CommonDirectoryAttributes.LastLogonTimestamp,
            CommonDirectoryAttributes.GivenName,
            CommonDirectoryAttributes.Surname,
            CommonDirectoryAttributes.Description,
            CommonDirectoryAttributes.SIDHistory,
            CommonDirectoryAttributes.SecurityDescriptor,
            CommonDirectoryAttributes.PrimaryGroupId,
            CommonDirectoryAttributes.PKIAccountCredentials,
            CommonDirectoryAttributes.PKIDPAPIMasterKeys,
            CommonDirectoryAttributes.PKIRoamingTimeStamp,
            CommonDirectoryAttributes.KeyCredentialLink
        };

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

        public IEnumerable<DSAccount> GetAccounts()
        {
            using (var searcher = new DirectorySearcher(this.searchRoot, AccountsFilter, AccountPropertiesToLoad, SearchScope.Subtree))
            {
                using (var searchResults = searcher.FindAll())
                {
                    foreach (var searchResult in searchResults.Cast<SearchResult>())
                    {
                        var obj = new AdsiObjectAdapter(searchResult);
                        var account = new DSAccount(obj, this.NetBIOSDomainName, null);
                        yield return account;
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
