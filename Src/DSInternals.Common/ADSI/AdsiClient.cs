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
            CommonDirectoryAttributes.PKIRoamingTimeStamp
        };

        private DirectoryEntry searchRoot;

        public AdsiClient(string server = null, NetworkCredential credential = null)
        {
            DirectoryContext context;
            if(!String.IsNullOrEmpty(server))
            {
                if(credential != null)
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
                    }
                }
            }
            else
            {
                // Discover the server for the current domain
                if(credential != null)
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
                }
            }
        }

        public IEnumerable<DSAccount> GetAccounts()
        {
            using (var searcher = new DirectorySearcher(this.searchRoot, AccountsFilter, AccountPropertiesToLoad, SearchScope.Subtree))
            {
                using (var searchResults = searcher.FindAll())
                {
                    foreach(var searchResult in searchResults.Cast<SearchResult>())
                    {
                        var obj = new AdsiObjectAdapter(searchResult);
                        var account = new DSAccount(obj, null);
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
                if(this.searchRoot != null)
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
