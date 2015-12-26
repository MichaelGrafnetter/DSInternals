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
        private const string linkIndex = "link_present_active_index";

        private Cursor cursor;
        private DirectorySchema schema;

        public LinkResolver(IsamDatabase database, DirectorySchema schema)
        {
            this.schema = schema;
            this.cursor = database.OpenCursor(ADConstants.LinkTableName);
            cursor.SetCurrentIndex(linkIndex);
            // TODO: Load column ids instead of names
        }

        public int? GetLinkedDNTag(int dnTag, string attributeName)
        {
            int[] result = this.GetLinkedDNTags(dnTag, attributeName);
            switch(result.Length)
            {
                case 0:
                    return null;
                case 1:
                    return result[0];
                default:
                    // TODO: Make this a special exception type. Move message to resources.
                    throw new Exception("More than 1 objects have been found.");
            }
        }

        public int[] GetLinkedDNTags(int dnTag, string attributeName)
        {
            var result = new List<int>();
            SchemaAttribute attr = this.schema.FindAttribute(attributeName);
            if(!attr.LinkId.HasValue)
            {
                // This is not a linked multivalue attribute.
                //TODO: Throw a proper exception
                throw new Exception();

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
                result.Add(foundTag);
            }
            return result.ToArray();
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
