namespace DSInternals.DataStore
{
    using DSInternals.Common.Exceptions;
    using DSInternals.Common.Schema;
    using Microsoft.Database.Isam;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinkResolver
    {
        // Column names:
        private const string LinkDNTColumn = "link_DNT";
        private const string BacklinkDNTColumn = "backlink_DNT";
        private const string LinkBaseColumn = "link_base";
        private const string LinkDataColumn = "link_data";
        private const string LinkMetadataColumn = "link_metadata";
        private const string LinkDeletedTimeColumn = "link_deltime";
        private const string LinkDeactivatedTimeColumn = "link_deactivetime";
        // Index of valid links post Recycle Bin feature
        private const string LinkIndex2008 = "link_present_active_index";
        // Index of valid links before the Recycle Bin feature
        private const string LinkIndex2003 = "link_present_index";

        private IsamDatabase _database;
        private DirectorySchema _schema;
        private Columnid _backlinkDNTColumnId;
        private Columnid _linkDataColumnId;
        private string _linkIndex;

        public LinkResolver(IsamDatabase database, DirectorySchema schema)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            _database = database;
            _schema = schema;

            var linkTable = database.Tables[ADConstants.LinkTableName];

            // Cache column IDs for faster lookups
            _backlinkDNTColumnId = linkTable.Columns[BacklinkDNTColumn].Columnid;
            _linkDataColumnId = linkTable.Columns[LinkDataColumn].Columnid;

            // Fallback to the old index if the newer one does not exist
            bool contains2008Index = linkTable.Indices.Contains(LinkIndex2008);
            _linkIndex = contains2008Index ? LinkIndex2008 : LinkIndex2003;
        }

        public DNTag? GetLinkedDNTag(DNTag dnTag, string attributeName)
        {
            // Ignore the data and any additional links
            (DNTag backlink, _) = this.GetLinkedValues(dnTag, attributeName).FirstOrDefault();
            return backlink > DNTag.NotAnObject ? backlink : null;
        }

        public IEnumerable<DNTag> GetLinkedDNTags(DNTag dnTag, string attributeName)
        {
            // Ignore the data
            return this.GetLinkedValues(dnTag, attributeName).Select(link => link.Backlink);
        }

        public IEnumerable<(DNTag Backlink, byte[] LinkData)> GetLinkedValues(DNTag dnTag, string attributeName)
        {
            AttributeSchema? attribute = this._schema.FindAttribute(attributeName);

            if (attribute == null)
            {
                // If the schema does not contain this attribute at all, we pretend it has an empty value.
                yield break;
            }

            // Check that the attribute is DN or DN-Binary.
            if (!attribute.LinkId.HasValue)
            {
                throw new DirectoryObjectOperationException("This is not a linked value attribute.", attributeName);
            }

            using (var cursor = _database.OpenCursor(ADConstants.LinkTableName))
            {
                int linkBase = attribute.LinkBase.Value;
                cursor.CurrentIndex = _linkIndex;
                // Columns order in index: link_DNT, link_base, backlink_DNT, link_data
                cursor.FindRecords(MatchCriteria.EqualTo, Key.ComposeWildcard(dnTag, linkBase));

                // The cursor is now before the first record in the link_table
                while (cursor.MoveNext())
                {
                    // TODO: Check if the link is deleted or expired.
                    int rawBacklinkDNT = (int)cursor.IndexRecord[_backlinkDNTColumnId];
                    DNTag backlinkDNT = unchecked((DNTag)rawBacklinkDNT);
                    byte[] linkData = null;

                    if (attribute.Syntax == AttributeSyntax.DNWithBinary)
                    {
                        // Try to fetch the data as well
                        object foundValue = cursor.IndexRecord[_linkDataColumnId];
                        if (foundValue != DBNull.Value)
                        {
                            linkData = (byte[])foundValue;
                        }
                    }

                    // Return DN with binary
                    yield return (backlinkDNT, linkData);
                }
            }
        }
    }
}
