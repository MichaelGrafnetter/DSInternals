namespace DSInternals.DataStore
{
    using DSInternals.Common.Data;
    using DSInternals.Common.Exceptions;
    using Microsoft.Database.Isam;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Performs translation between Distinguished Names and DN Tags.
    /// </summary>
    public class DistinguishedNameResolver : IDisposable
    {
        private const string DnsZoneContainer = "MicrosoftDNS";

        private Cursor cursor;
        private DirectorySchema schema;

        // Cached column identifiers and index names to make DN lookups slightly faster.
        private Columnid DistinguishedNameTagColumnId;
        private Columnid ParentDistinguishedNameTagColumnId;
        private Columnid RelativeDistinguishedNameColumnId;
        private Columnid RelativeDistinguishedNameTypeColumnId;
        private string DistinguishedNameTagIndex;
        private string ParentDistinguishedNameTagIndex;

        /// <summary>
        ///  Distinguished name cache
        /// </summary>
        private IDictionary<int, DistinguishedName> dnCache;

        public DistinguishedNameResolver(IsamDatabase database, DirectorySchema schema)
        {
            // Initialize the DN cache, while pre-caching the root DN as a sentinel.
            this.dnCache = new Dictionary<int, DistinguishedName>
            {
                { ADConstants.RootDNTag, new DistinguishedName() }
            };

            // Cache AD schema and datatable cursor
            this.schema = schema;
            this.cursor = database.OpenCursor(ADConstants.DataTableName);

            // Cache frequently accessed datatable column IDs from the schema
            this.DistinguishedNameTagColumnId = schema.FindColumnId(CommonDirectoryAttributes.DNTag);
            this.ParentDistinguishedNameTagColumnId = schema.FindColumnId(CommonDirectoryAttributes.ParentDNTag);
            this.RelativeDistinguishedNameColumnId = schema.FindColumnId(CommonDirectoryAttributes.RDN);
            this.RelativeDistinguishedNameTypeColumnId = schema.FindColumnId(CommonDirectoryAttributes.RDNType);
            this.DistinguishedNameTagIndex = schema.FindIndexName(CommonDirectoryAttributes.DNTag);
            this.ParentDistinguishedNameTagIndex = schema.FindIndexName(CommonDirectoryAttributes.ParentDNTag);
        }

        /// <summary>
        /// Recursively resolves a DN Tag to a full distinguished name.
        /// </summary>
        /// <param name="dnTag">Distinguished name tag</param>
        /// <returns>Resolved DN</returns>
        /// <exception cref="DirectoryObjectNotFoundException" />
        public DistinguishedName Resolve(int dnTag)
        {
            // Check the DN cache first
            DistinguishedName dnFromCache = this.TryResolveFromCache(dnTag);
            if (dnFromCache != null)
            {
                // We just return the DN from cache.
                return dnFromCache;
            }

            if (dnTag < ADConstants.RootDNTag)
            {
                // Minimum DN Tag is 2.
                throw new ArgumentOutOfRangeException(nameof(dnTag));
            }

            // Set index to the Distinguished Name Tag (~primary key)
            cursor.CurrentIndex = this.DistinguishedNameTagIndex;

            // We will build the DN from leaf to root
            DistinguishedName result = new DistinguishedName();
            int currentDNTag = dnTag;
            while (currentDNTag != ADConstants.RootDNTag)
            {
                // Move cursor to the current object
                bool found = cursor.GotoKey(Key.Compose(currentDNTag));
                if (!found)
                {
                    throw new DirectoryObjectNotFoundException(dnTag);
                }

                // Retrieve the current object's RDN, e.g. CN=Administrator
                string name = cursor.RetrieveColumnAsString(this.RelativeDistinguishedNameColumnId);
                int rdnType = cursor.RetrieveColumnAsInt(this.RelativeDistinguishedNameTypeColumnId).Value;
                string rdnAtt = schema.FindAttribute(rdnType).Name.ToUpper();
                var currentRDN = new DistinguishedNameComponent(rdnAtt, name);

                // Concat the current RDN with the child RDN in the resulting DN
                result.AddParent(currentRDN);

                // Identify the current object's parent
                int parentDNTag = cursor.RetrieveColumnAsDNTag(this.ParentDistinguishedNameTagColumnId).Value;

                // Check the DN cache
                DistinguishedName parentDN = this.TryResolveFromCache(parentDNTag);
                if(parentDN != null)
                {
                    // We have found the parent object in DN cache.
                    result.AddParent(parentDN);

                    // Speed-up future DN lookups by caching root containers, all OUs, naming contexts, and DNS zones.
                    // Add the current object to the cache if any of the following conditions is met:
                    // 1) The parent object is the root DN. (DC=com)
                    // 2) The current object is an organizational unit. (OU=Employees,DC=contoso,DC=com)
                    // 3) The current object is the DNS zone container. (CN=MicrosoftDNS,CN=System,DC=contoso,DC=com)
                    // 4) The current object is a domain component, while the parent object is not. (DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com)
                    // 5) The parent object is a domain component, while the current object is not. (CN=Computers,DC=contoso,DC=com)
                    // 6) Both the current and parent objects are domain components, but the current object is not the leaf object. (...,DC=contoso,DC=com)
                    // Note: The last condition should prevent individual DNS records from being cached unnecessarily. (DC=www,DC=contoso.com,CN=MicrosoftDNS,DC=DomainDnsZones,DC=contoso,DC=com)
                    bool shoulCache = parentDN.Components.Count == 0 ||
                        String.Equals(rdnAtt, CommonDirectoryAttributes.OrganizationalUnitName, StringComparison.OrdinalIgnoreCase) ||
                        String.Equals(name, DnsZoneContainer, StringComparison.OrdinalIgnoreCase);
                    bool isLeaf = currentDNTag == dnTag;

                    if (!shoulCache)
                    {
                        bool isDomainComponent = String.Equals(rdnAtt, CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase);
                        bool isParentDomainComponent = String.Equals(parentDN.Components[0].Name, CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase);
                        shoulCache =
                            isDomainComponent && !isParentDomainComponent ||
                            !isDomainComponent && isParentDomainComponent ||
                            isDomainComponent && isParentDomainComponent && !isLeaf;
                    }

                    if (shoulCache)
                    {
                        if(isLeaf)
                        {
                            // We have resolved the entire DN, so we can cache it.
                            this.dnCache.Add(dnTag, result);
                        }
                        else
                        {
                            // We have resolved a part of the DN, so we can cache it as well.
                            var currentDN = new DistinguishedName(currentRDN);
                            currentDN.AddParent(parentDN);
                            this.dnCache.Add(currentDNTag, currentDN);
                        }
                    }
                    
                    // We can stop the recursion as we have resolved the entire DN using cache.
                    break;
                }

                // Move upwards to the object's parent as we have not found it in the DN cache.
                currentDNTag = parentDNTag;
            }

            return result;
        }

        /// <summary>
        /// Translates a distinguished name to a its DN Tag.
        /// </summary>
        /// <param name="dn">Distinguished name to translate</param>
        /// <returns>DN Tag</returns>
        /// <exception cref="DirectoryObjectNotFoundException" />
        public int Resolve(string dn)
        {
            var parsed = new DistinguishedName(dn);
            return this.Resolve(parsed);
        }

        /// <summary>
        /// Translates a distinguished name to a its DN Tag.
        /// </summary>
        /// <param name="dn">Distinguished name to translate</param>
        /// <returns>DN Tag</returns>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public int Resolve(DistinguishedName dn)
        {
            if (dn.Components.Count == 0)
            {
                throw new ArgumentException("Empty distinguished name provided.", "dn");
            }

            // Get the PDNT_index
            cursor.CurrentIndex = this.ParentDistinguishedNameTagIndex;

            // Start at the root object
            int currentDNTag = ADConstants.RootDNTag;
            foreach (var component in dn.Components.Reverse())
            {
                // Indexed columns: PDNT_col, name
                bool found = cursor.GotoKey(Key.Compose(currentDNTag, component.Value));
                if (!found)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Test AttrTyp
                int foundRdnAttId = cursor.RetrieveColumnAsInt(this.RelativeDistinguishedNameTypeColumnId).Value;
                string foundRdnAttName = schema.FindAttribute(foundRdnAttId).Name;

                // Compare the found isRDN attribute with the requested one. Case insensitive.
                if (String.Compare(component.Name, foundRdnAttName, true) != 0)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Move to the found object
                currentDNTag = cursor.RetrieveColumnAsDNTag(this.DistinguishedNameTagColumnId).Value;
            }
            return currentDNTag;
        }

        /// <summary>
        /// Recursively resolves multiple DN Tags to full distinguished names.
        /// </summary>
        /// <param name="dnTags">DN Tags to resolve.</param>
        /// <returns>Distinguished names</returns>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public IEnumerable<DistinguishedName> Resolve(IEnumerable<int> dnTags)
        {
            foreach (int dnTag in dnTags)
            {
                yield return this.Resolve(dnTag);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && cursor != null)
            {
                cursor.Dispose();
                cursor = null;
            }
        }

        /// <summary>
        /// Resolves a DN Tag to a full distinguished name using the cache.
        /// </summary>
        /// <param name="dnTag">DN Tag</param>
        /// <returns>Distinguished name</returns>
        private DistinguishedName TryResolveFromCache(int dnTag)
        {
            DistinguishedName dnFromCache;
            bool found = this.dnCache.TryGetValue(dnTag, out dnFromCache);
            return found ? dnFromCache : null;
        }
    }
}
