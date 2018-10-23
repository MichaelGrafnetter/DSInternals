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
        private Cursor cursor;
        private DirectorySchema schema;

        /// <summary>
        ///  Distinguished name cache
        /// </summary>
        private IDictionary<int, DistinguishedName> dnCache;

        public DistinguishedNameResolver(IsamDatabase database, DirectorySchema schema)
        {
            // Initialize the DN cache, while pre-caching the root DN as a sentinel.
            this.dnCache = new Dictionary<int, DistinguishedName>();
            this.dnCache.Add(ADConstants.RootDNTag, new DistinguishedName());

            // Cache AD schema and datatable cursor
            this.schema = schema;
            this.cursor = database.OpenCursor(ADConstants.DataTableName);
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
                throw new ArgumentOutOfRangeException("dnTag");
            }

            // Cache column IDs
            var dntColId = schema.FindColumnId(CommonDirectoryAttributes.DNTag);
            var pdntColId = schema.FindColumnId(CommonDirectoryAttributes.ParentDNTag);
            var rdnColId = schema.FindColumnId(CommonDirectoryAttributes.RDN);
            var rdnTypeColId = schema.FindColumnId(CommonDirectoryAttributes.RDNType);

            // Set index to the Distinguished Name Tag (~primary key)
            cursor.CurrentIndex = schema.FindIndexName(CommonDirectoryAttributes.DNTag);

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
                string name = cursor.RetrieveColumnAsString(rdnColId);
                int rdnType = cursor.RetrieveColumnAsInt(rdnTypeColId).Value;
                string rdnAtt = schema.FindAttribute(rdnType).Name.ToUpper();
                var currentRDN = new DistinguishedNameComponent(rdnAtt, name);

                // Concat the current RDN with the child RDN in result
                result.AddParent(currentRDN);

                // Identify the current object's parent
                int parentDNTag = cursor.RetrieveColumnAsDNTag(pdntColId).Value;

                // Check the DN cache
                DistinguishedName parentDN = this.TryResolveFromCache(parentDNTag);
                if(parentDN != null)
                {
                    // We have found the parent object in DN cache.
                    result.AddParent(parentDN);

                    // Add the current object to cache if the parent is DC or root.
                    bool shoulCache = parentDN.Components.Count == 0 ||
                        String.Equals(parentDN.Components[0].Name, CommonDirectoryAttributes.DomainComponent, StringComparison.OrdinalIgnoreCase);
                    if(shoulCache)
                    {
                        var currentDN = new DistinguishedName(currentRDN);
                        currentDN.AddParent(parentDN);
                        this.dnCache.Add(currentDNTag, currentDN);
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
            cursor.CurrentIndex = this.schema.FindIndexName(CommonDirectoryAttributes.ParentDNTag);

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
                int foundRdnAttId = cursor.RetrieveColumnAsInt(schema.FindColumnId(CommonDirectoryAttributes.RDNType)).Value;
                string foundRdnAttName = schema.FindAttribute(foundRdnAttId).Name;

                // Compare the found isRDN attribute with the requested one. Case insensitive.
                if (String.Compare(component.Name, foundRdnAttName, true) != 0)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Move to the found object
                currentDNTag = cursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.DNTag)).Value;
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