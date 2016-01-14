using Microsoft.Database.Isam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSInternals.DataStore
{
    public class LinkResolver : IDisposable
    {
        private const string linkDNCol = "link_DNT";
        private const string backlinkDNCol = "backlink_DNT";
        private const string linkBaseCol = "link_base";
        private const string linkDataCol = "link_data";
        private const string linkMetadataCol = "link_metadata";
        private const string linkDeletedTimeCol = "link_deltime";
        private const string linkDeactivatedTimeCol = "link_deactivetime";
        // Index of valid links post Recycle Bin feature
        private const string linkIndex2008 = "link_present_active_index";
        // Index of valid links before the Recycle Bin feature
        private const string linkIndex2003 = "link_present_index";

        private Cursor cursor;
        private DirectorySchema schema;

        public LinkResolver(IsamDatabase database, DirectorySchema schema)
        {
            this.schema = schema;
            this.cursor = database.OpenCursor(ADConstants.LinkTableName);
            if (this.cursor.TableDefinition.Indices.Contains(linkIndex2008))
            {
                this.cursor.SetCurrentIndex(linkIndex2008);
            }
            else
            {
                // Fallback to the old index if the newer one does not exist
                cursor.SetCurrentIndex(linkIndex2003);
            }

            // TODO: Load column ids instead of names
        }

        public int? GetLinkedDNTag(int dnTag, string attributeName)
        {
            try
            {
                // Cast int to int? to force null as default value instead of 0
                return this.GetLinkedDNTags(dnTag, attributeName).Cast<int?>().SingleOrDefault();
            }
            catch(InvalidOperationException)
            {
                // TODO: Make this a special exception type. Move message to resources.
                throw new Exception("More than 1 objects have been found.");
            }
        }

        public IEnumerable<int> GetLinkedDNTags(int dnTag, string attributeName)
        {
            SchemaAttribute attr = this.schema.FindAttribute(attributeName);
            if(!attr.LinkId.HasValue)
            {
                //TODO: Throw a proper exception
                // TODO: Check that attribute type is DN
                throw new Exception("This is not a linked multivalue attribute.");
            }
            int linkId = attr.LinkId.Value;
            // Remove the rightmost bit that indicates if this is a forward link or a backlink.
            int linkBase = linkId >> 1;
            // Columns order in index: link_DNT, link_base, backlink_DNT
            Key key = Key.Compose(dnTag, linkBase);
            key.AddWildcard();
            cursor.FindRecords(MatchCriteria.EqualTo, key);
            while(cursor.MoveNext())
            {
                // TODO: Not deactivated?
                int foundTag = (int)cursor.IndexRecord[backlinkDNCol];
                yield return foundTag;
            }
        }

        public IEnumerable<byte[]> GetDNBinaryValues(int dnTag, string attributeName)
        {
            // TODO: Implement DN-Binary attribute value retrieval from linktable.
            throw new NotImplementedException();
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
    }
}
