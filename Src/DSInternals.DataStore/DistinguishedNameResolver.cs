using DSInternals.Common.Data;
using Microsoft.Database.Isam;
using System;
using System.Linq;
using System.Collections.Generic;
using DSInternals.Common.Exceptions;

namespace DSInternals.DataStore
{
    // TODO: DistinguishedNameResolver interface
    public class DistinguishedNameResolver : IDisposable
    {
        private Cursor cursor;
        private DirectorySchema schema;
        private IDictionary<int, string> dnCache;

        public DistinguishedNameResolver(IsamDatabase database, DirectorySchema schema)
        {
            this.dnCache = new Dictionary<int, string>();
            this.schema = schema;
            this.cursor = database.OpenCursor(ADConstants.DataTableName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dnTag"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public DistinguishedName Resolve(int dnTag)
        {
            if(dnTag < ADConstants.RootDNTag)
            {
                throw new ArgumentOutOfRangeException("dnTag");
            }
            if(dnTag == ADConstants.RootDNTag)
            {
                // TODO: or null?
                return new DistinguishedName();
            }
            // TODO: Move to constructor?
            var dntColId = schema.FindColumnId(CommonDirectoryAttributes.DNTag);
            var pdntColId = schema.FindColumnId(CommonDirectoryAttributes.ParentDNTag);
            var rdnColId = schema.FindColumnId(CommonDirectoryAttributes.RDN);
            var rdnTypeColId = schema.FindColumnId(CommonDirectoryAttributes.RDNType);

            DistinguishedName dn = new DistinguishedName();
            cursor.CurrentIndex = schema.FindIndexName(CommonDirectoryAttributes.DNTag);
            int currentDNTag = dnTag;
            do
            {
                bool found = cursor.GotoKey(Key.Compose(currentDNTag));
                if(!found)
                {
                    throw new DirectoryObjectNotFoundException(dnTag);
                }
                string name = cursor.RetrieveColumnAsString(rdnColId);
                int rdnType = cursor.RetrieveColumnAsInt(rdnTypeColId).Value;
                string rdnAtt = schema.FindAttribute(rdnType).Name.ToUpper();
                dn.AddParent(rdnAtt, name);
                currentDNTag = cursor.RetrieveColumnAsDNTag(pdntColId).Value;
            } while (currentDNTag != ADConstants.RootDNTag);
            
            // TODO: Parent DN Caching
            return dn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public int Resolve(string dn)
        {
            var parsed = new DistinguishedName(dn);
            return this.Resolve(parsed);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dn"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryObjectNotFoundException"></exception>
        public int Resolve(DistinguishedName dn)
        {
            if(dn.Components.Count == 0)
            {
                throw new ArgumentException("Empty distinguished name provided.", "dn");
            }
            
            // Get the PDNT_index
            cursor.CurrentIndex = this.schema.FindIndexName(CommonDirectoryAttributes.ParentDNTag);

            // Start at the root object
            int currentDNTag = ADConstants.RootDNTag;
            foreach(var component in dn.Components.Reverse())
            {
                // Indexed columns: PDNT_col, name
                bool found = cursor.GotoKey(Key.Compose(currentDNTag, component.Value));
                if(!found)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Test AttrTyp
                int foundRdnAttId = cursor.RetrieveColumnAsInt(schema.FindColumnId(CommonDirectoryAttributes.RDNType)).Value;
                string foundRdnAttName = schema.FindAttribute(foundRdnAttId).Name;

                // Compare the found isRDN attribute with the requested one. Case insensitive.
                if(String.Compare(component.Name, foundRdnAttName, true) != 0)
                {
                    throw new DirectoryObjectNotFoundException(dn);
                }

                // Move to the found object
                currentDNTag = cursor.RetrieveColumnAsDNTag(schema.FindColumnId(CommonDirectoryAttributes.DNTag)).Value;
            }
            return currentDNTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dnTags"></param>
        /// <returns></returns>
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
            if(disposing && cursor != null)
            {
                cursor.Dispose();
                cursor = null;
            }
        }
    }
}