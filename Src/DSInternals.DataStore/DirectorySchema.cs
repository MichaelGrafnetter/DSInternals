using System.Globalization;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Schema;
using Microsoft.Database.Isam;

namespace DSInternals.DataStore
{
    /// <summary>
    /// The DirectorySchema class represents the schema partition for a particular domain.
    /// </summary>
    public sealed class DirectorySchema : BaseSchema
    {
        private const string AttributeColumnPrefix = "ATT";
        private const string AttributeIndexPrefix = "INDEX_";
        public const string DistinguishedNameTagColumn = "DNT_col";
        public const string DistinguishedNameTagIndex = "DNT_index";
        public const string ParentDistinguishedNameTagColumn = "PDNT_col";
        public const string ParentDistinguishedNameTagIndex = "PDNT_index";
        public const string NamingContextDistinguishedNameTagColumn = "NCDNT_col";
        public const string RelativeDistinguishedNameTypeColumn = "RDNtyp_col";
        public const string ObjectColumn = "OBJ_col";
        public const string PartitionedAccountNameIndex = "NC_Acc_Type_Name";
        public const string PartitionedAccountSidIndex = "NC_Acc_Type_Sid";
        public const string PartitionedGuidIndex = "nc_guid_Index";
        public const string AncestorsColumn = "Ancestors_col";
        public const string AncestorsIndex = "Ancestors_index";

        public Columnid DistinguishedNameTagColumnId
        {
            get;
            private set;
        }

        public Columnid ParentDistinguishedNameTagColumnId
        {
            get;
            private set;
        }

        public Columnid RelativeDistinguishedNameColumnId
        {
            get;
            private set;
        }

        public Columnid RelativeDistinguishedNameTypeColumnId
        {
            get;
            private set;
        }

        public Columnid NamingContextDistinguishedNameTagColumnId
        {
            get;
            private set;
        }

        public Columnid ObjectColumnId
        {
            get;
            private set;
        }

        private IDictionary<string, Columnid> ColumnsByAttributeName
        {
            get;
            set;
        }

        private IDictionary<string, DNTag> ObjectCategoriesByName
        {
            get;
            set;
        }

        private IDictionary<ClassType, DNTag> ObjectCategoriesByGovernsId
        {
            get;
            set;
        }

        private DirectorySchema()
        {
            // Initialize value maps for fast lookups
            ColumnsByAttributeName = new Dictionary<string, Columnid>(InitialAttributeDictionaryCapacity, StringComparer.InvariantCultureIgnoreCase);
            ObjectCategoriesByName = new Dictionary<string, DNTag>(InitialClassDictionaryCapacity, StringComparer.InvariantCultureIgnoreCase);
            ObjectCategoriesByGovernsId = new Dictionary<ClassType, DNTag>(InitialClassDictionaryCapacity);
        }

        public Columnid? FindColumnId(string attributeName)
        {
            bool found = ColumnsByAttributeName.TryGetValue(attributeName, out Columnid column);
            return found ? column : null;
        }

        public string? FindIndexName(string attributeName)
        {
            return this.FindAttribute(attributeName)?.IndexName;
        }

        public DNTag? FindObjectCategory(string className)
        {
            bool found = ObjectCategoriesByName.TryGetValue(className, out DNTag objectCategory);
            return found ? objectCategory : null;
        }

        public DNTag? FindObjectCategory(ClassType governsId)
        {
            bool found = ObjectCategoriesByGovernsId.TryGetValue(governsId, out DNTag objectCategory);
            return found ? objectCategory : null;
        }

        public static DirectorySchema Create(IsamDatabase database)
        {
            // Create a hardcoded schema with base attribute set.
            var baseSchema = BaseSchema.Create();

            // Now load the actual full schema from the database.
            var schema = new DirectorySchema();

            // Cache column IDs, as we are going to loop through 1500+ rows
            var dataTable = database.Tables[ADConstants.DataTableName];
            var columns = dataTable.Columns;

            // Cache system columns
            schema.DistinguishedNameTagColumnId = GetColumnId(DistinguishedNameTagColumn, columns);
            schema.ParentDistinguishedNameTagColumnId = GetColumnId(ParentDistinguishedNameTagColumn, columns);
            schema.NamingContextDistinguishedNameTagColumnId = GetColumnId(NamingContextDistinguishedNameTagColumn, columns);
            schema.RelativeDistinguishedNameTypeColumnId = GetColumnId(RelativeDistinguishedNameTypeColumn, columns);
            schema.ObjectColumnId = GetColumnId(ObjectColumn, columns);

            AttributeSchema? nameColumn = baseSchema.FindAttribute(CommonDirectoryAttributes.RDN);
            schema.RelativeDistinguishedNameColumnId = GetColumnId(nameColumn.DerivedColumnName, columns);

            // Cache columns mapped to AD attributes
            Columnid governsIdColumn = GetColumnId(AttributeType.GovernsId, columns, baseSchema);
            Columnid attributeIdColumn = GetColumnId(AttributeType.AttributeId, columns, baseSchema);
            Columnid internalIdColumn = GetColumnId(AttributeType.InternalId, columns, baseSchema);
            Columnid linkIdColumn = GetColumnId(AttributeType.LinkId, columns, baseSchema);
            Columnid isSingleValuedColumn = GetColumnId(AttributeType.IsSingleValued, columns, baseSchema);
            Columnid attributeSyntaxColumn = GetColumnId(AttributeType.AttributeSyntax, columns, baseSchema);
            Columnid isInGlobalCatalogColumn = GetColumnId(AttributeType.IsInGlobalCatalog, columns, baseSchema);
            Columnid searchFlagsColumn = GetColumnId(AttributeType.SearchFlags, columns, baseSchema);
            Columnid systemOnlyColumn = GetColumnId(AttributeType.SystemOnly, columns, baseSchema);
            Columnid syntaxColumn = GetColumnId(AttributeType.AttributeSyntax, columns, baseSchema);
            Columnid omSyntaxColumn = GetColumnId(AttributeType.OMSyntax, columns, baseSchema);
            Columnid commonNameColumn = GetColumnId(AttributeType.CommonName, columns, baseSchema);
            Columnid rangeLowerColumn = GetColumnId(AttributeType.RangeLower, columns, baseSchema);
            Columnid rangeUpperColumn = GetColumnId(AttributeType.RangeUpper, columns, baseSchema);
            Columnid schemaIdGuidColumn = GetColumnId(AttributeType.SchemaIdGuid, columns, baseSchema);
            Columnid systemFlagsColumn = GetColumnId(AttributeType.SystemFlags, columns, baseSchema);
            Columnid isDefunctColumn = GetColumnId(AttributeType.IsDefunct, columns, baseSchema);
            Columnid prefixMapColumn = GetColumnId(AttributeType.PrefixMap, columns, baseSchema);
            Columnid objectCategoryColumn = GetColumnId(AttributeType.ObjectCategory, columns, baseSchema);
            Columnid ldapDisplayNameColumn = GetColumnId(AttributeType.LdapDisplayName, columns, baseSchema);

            using (var cursor = database.OpenCursor(ADConstants.DataTableName))
            {
                // Find the Schema Naming Context
                // Corresponding LDAP filter: (objectClass=dMD)
                cursor.FindAllRecords();
                cursor.CurrentIndex = baseSchema.FindAttribute(AttributeType.ObjectClass).DerivedIndexName;
                bool schemaFound = cursor.GotoKey(Key.Compose(ClassType.Schema));

                if (!schemaFound)
                {
                    throw new DirectoryObjectNotFoundException(CommonDirectoryClasses.Schema);
                }

                // Load the prefix table from the Schema NC
                byte[] binaryPrefixMap = cursor.RetrieveColumnAsByteArray(prefixMapColumn);

                if (binaryPrefixMap != null && binaryPrefixMap.Length > 0)
                {
                    // Clean older ADs might not contain any prefix table
                    schema.PrefixTable.LoadFromBlob(binaryPrefixMap);
                }

                // Load the list of attributes and classes.
                // Corresponding LDAP filter: (lDAPDisplayName=*)
                cursor.CurrentIndex = baseSchema.FindAttribute(AttributeType.LdapDisplayName).DerivedIndexName;
                while (cursor.MoveNext())
                {
                    bool isDefunct = cursor.RetrieveColumnAsBoolean(isDefunctColumn);

                    if (isDefunct)
                    {
                        // We will ignore deleted attributes and classes
                        continue;
                    }

                    string? ldapDisplayName = cursor.RetrieveColumnAsString(ldapDisplayNameColumn, unicode: true);
                    string? commonName = cursor.RetrieveColumnAsString(commonNameColumn);

                    AttributeType? attributeId = cursor.RetrieveColumnAsAttributeType(attributeIdColumn);
                    bool isSystemOnly = cursor.RetrieveColumnAsBoolean(systemOnlyColumn);

                    if (attributeId.HasValue)
                    {
                        // This must be an attribute
                        AttributeType? internalId = cursor.RetrieveColumnAsAttributeType(internalIdColumn);
                        int? rangeLower = cursor.RetrieveColumnAsInt(rangeLowerColumn);
                        int? rangeUpper = cursor.RetrieveColumnAsInt(rangeUpperColumn);
                        Guid? schemaIdGuid = cursor.RetrieveColumnAsGuid(schemaIdGuidColumn);
                        AttributeSystemFlags systemFlags = cursor.RetrieveColumnAsAttributeSystemFlags(systemFlagsColumn);
                        int? linkId = cursor.RetrieveColumnAsInt(linkIdColumn);
                        bool isInGlobalCatalog = cursor.RetrieveColumnAsBoolean(isInGlobalCatalogColumn);
                        bool isSingleValued = cursor.RetrieveColumnAsBoolean(isSingleValuedColumn);
                        AttributeSearchFlags searchFlags = cursor.RetrieveColumnAsSearchFlags(searchFlagsColumn);
                        AttributeSyntax syntax = cursor.RetrieveColumnAsAttributeSyntax(syntaxColumn);
                        AttributeOmSyntax omSyntax = cursor.RetrieveColumnAsAttributeOmSyntax(omSyntaxColumn);

                        // We have already loaded the full prefix table, so the translation should succeed.
                        string? attributeOid = schema.PrefixTable.Translate(attributeId.Value);

                        // Construct the attribute
                        var attribute = new AttributeSchema(
                            ldapDisplayName,
                            commonName,
                            attributeOid,
                            schemaIdGuid ?? Guid.Empty, // This should never happen
                            attributeId.Value,
                            internalId,
                            linkId,
                            syntax,
                            omSyntax,
                            searchFlags,
                            systemFlags,
                            isSystemOnly,
                            rangeLower,
                            rangeUpper,
                            isInGlobalCatalog,
                            isSingleValued,
                            isDefunct
                            );

                        // Populate all relevant indices
                        schema.AddAttribute(attribute);
                    }
                    else
                    {
                        // This must be a class
                        ClassType? governsId = cursor.RetrieveColumnAsObjectCategory(governsIdColumn);
                        DNTag? classDNT = cursor.RetrieveColumnAsDNTag(schema.DistinguishedNameTagColumnId);

                        // Populate all relevant indices
                        schema.ClassesByName[ldapDisplayName] = governsId.Value;
                        schema.ObjectCategoriesByName[ldapDisplayName] = classDNT.Value;
                        schema.ObjectCategoriesByGovernsId[governsId.Value] = classDNT.Value;
                    }
                }
            }

            // Populate column names
            foreach (var column in columns)
            {
                // Try to map the column to an attribute internal ID
                AttributeType? attributeType = GetAttributeTypeFromColumnName(column.Name);

                if (attributeType.HasValue)
                {
                    // This column should be mapped to an attribute either through attributeID or msDS-IntId.
                    AttributeSchema? attribute = schema.FindAttribute(attributeType.Value);

                    if (attribute != null)
                    {
                        attribute.ColumnName = column.Name;
                        schema.ColumnsByAttributeName[attribute.Name] = column.Columnid;
                    }
                }
            }

            // Populate attribute indices
            // HACK: We are using the low-level IndexInfo instead of high-level IndexCollection.
            // There is a bug in Isam IndexCollection enumerator, which causes it to loop indefinitely
            // through the first few indices under some very rare circumstances.
            foreach (var index in dataTable.GetIndices2(database))
            {
                // Parse the index name.
                (AttributeType? attributeType, char? indexTypeCode) = FindAttributeTypeByIndexName(index.Name);

                if (attributeType.HasValue)
                {
                    // Try to map the index name to an attribute either through attributeID or msDS-IntId.
                    AttributeSchema? attribute = schema.FindAttribute(attributeType.Value);

                    if (attribute != null) // Attribute found
                    {
                        switch (indexTypeCode)
                        {
                            case 'P':
                                // This should be a containerized index over PDNT_col + ATTxNNNNN
                                attribute.ContainerizedIndexName = index.Name;
                                break;
                            case 'T':
                                // This should be a tuple index
                                attribute.TupleIndexName = index.Name;
                                break;
                            case null:
                                // This index should be over a column
                                attribute.IndexName = index.Name;
                                break;
                            case 'S': // We do not support subtree indices yet.
                            default:
                                // Some other index type which we do not support yet.
                                break;
                        }
                    }
                }
            }

            // TODO: Load Ext-Int Map from hiddentable
            return schema;
        }

        private static (AttributeType?, char?) FindAttributeTypeByIndexName(string indexName)
        {
            // Indices mapped to AD attributes start with INDEX_
            bool isAttributeIndex = indexName.StartsWith(AttributeIndexPrefix, ignoreCase: false, CultureInfo.InvariantCulture);

            if (isAttributeIndex)
            {
                // Examples: INDEX_0009001C, INDEX_P_000907CE
                int lastSeparatorIndex = indexName.LastIndexOf('_');

                if (lastSeparatorIndex > 1)
                {
                    string hexId = indexName.Substring(lastSeparatorIndex + 1);
                    bool parsed = UInt32.TryParse(hexId, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat, out uint numericInternalId);

                    if (parsed)
                    {
                        AttributeType internalId = (AttributeType) numericInternalId;

                        if (lastSeparatorIndex > AttributeIndexPrefix.Length)
                        {
                            // Multi-column index, e.g., INDEX_P_000907CE
                            char indexTypeCode = indexName[lastSeparatorIndex - 1];
                            return (internalId, indexTypeCode);
                        }
                        else
                        {
                            // Single-column index, e.g., INDEX_0009001C
                            return (internalId, null);
                        }
                    }
                }
            }

            // In all other cases, when we could not parse the index name:
            return (null, null);
        }

        private static AttributeType? GetAttributeTypeFromColumnName(string columnName)
        {
            // Columns mapped to AD attributes start with ATT
            bool isAttributeColumn = columnName.StartsWith(AttributeColumnPrefix, ignoreCase: false, CultureInfo.InvariantCulture);

            if (!isAttributeColumn)
            {
                // This must be a system attribute
                return null;
            }

            // Strip the ATTx prefix from column name to get the numeric attribute ID
            string attributeIdStr = columnName.Substring(AttributeColumnPrefix.Length + 1, columnName.Length - AttributeColumnPrefix.Length - 1);

            // Parse the rest as int. May be negative (and non-default attributes like LAPS or Exchange are).
            bool parsed = Int32.TryParse(attributeIdStr, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out int numericAttributeId);

            // Re-interpret the value as unsigned int
            return parsed ? (AttributeType)unchecked((uint)numericAttributeId) : null;
        }

        private static Columnid GetColumnId(AttributeType attributeId, ColumnCollection columns, BaseSchema schema)
        {
            var attribute = schema.FindAttribute(attributeId);
            string? columnName = attribute?.DerivedColumnName;

            if (columnName != null && columns.Contains(columnName))
            {
                var column = columns[columnName];
                return column.Columnid;
            }
            else
            {
                throw new SchemaAttributeNotFoundException(attributeId);
            }
        }

        private static Columnid GetColumnId(string columnName, ColumnCollection columns)
        {
            if (columns.Contains(columnName))
            {
                var column = columns[columnName];
                return column.Columnid;
            }
            else
            {
                throw new SchemaAttributeNotFoundException(columnName);
            }
        }

        // Clean AD contains 1500+ attributes and Exchange adds many more, but 3K should be enough for everyone.
        protected override int InitialAttributeDictionaryCapacity => 3000;

        // Clean AD contains 250+ classes and Exchange adds many more, but 500 should be enough for everyone.
        protected override int InitialClassDictionaryCapacity => 500;
    }
}
