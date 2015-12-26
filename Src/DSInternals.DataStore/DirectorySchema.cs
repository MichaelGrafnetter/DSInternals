namespace DSInternals.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using DSInternals.Common.Data;
    using Microsoft.Database.Isam;
    using DSInternals.Common;
    using DSInternals.Common.Exceptions;

    /// <summary>
    /// The ActiveDirectorySchema class represents the schema partition for a particular domain.
    /// </summary>
    public class DirectorySchema
    {
        private const string AttributeColPrefix = "ATT";
        private const int AttributeColPrefixLength = 4;
        private const string SystemColSuffix = "_col";
        private const int SystemColSuffixLength = 4;

        private IDictionary<string, SchemaAttribute> attributesByColumnName;
        private IDictionary<int, SchemaAttribute> attributesById;
        private IDictionary<string, SchemaAttribute> attributesByName;
        private IDictionary<string, int> classesByName;

        // TODO: Internal?
        // TODO: ISchema
        public DirectorySchema(IsamDatabase database)
        {
            TableDefinition dataTable = database.Tables[ADConstants.DataTableName];
            this.LoadAttributeList(dataTable.Columns);
            this.LoadAttributeIndices(dataTable.Indices);
            this.LoadAttributeProperties(database);
            // We do not need to search by column name, so let garbage collector do its job.
            this.attributesByColumnName = null;
            this.LoadClassList(database);
            // TODO: Load Ext-Int Map from hiddentable
        }

        /// <summary>
        /// Gets the partition name.
        /// </summary>
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // TODO: AttributeCollection class?
        public ICollection<SchemaAttribute> FindAllAttributes()
        {
            return this.attributesByName.Values;
        }

        public SchemaAttribute FindAttribute(string attributeName)
        {
            Validator.AssertNotNullOrWhiteSpace(attributeName, "attributeName");
            // Make it case-insensitive by always lowering the name:
            string lowerAttName = attributeName.ToLower();
            if (this.attributesByName.ContainsKey(lowerAttName))
            {
                return this.attributesByName[lowerAttName];
            }
            else
            {
                throw new SchemaAttributeNotFoundException(attributeName);
            }
        }

        public SchemaAttribute FindAttribute(int attributeId)
        {
            if (this.attributesById.ContainsKey(attributeId))
            {
                return this.attributesById[attributeId];
            }
            else
            {
                throw new SchemaAttributeNotFoundException(attributeId);
            }
        }

        public Columnid FindColumnId(string attributeName)
        {
            return this.FindAttribute(attributeName).ColumnID;
        }

        public string FindIndexName(string attributeName)
        {
            return this.FindAttribute(attributeName).Index;
        }

        /// <summary>
        /// Refreshes the schema cache.
        /// </summary>
        public void RefreshSchema()
        {
            throw new NotImplementedException();
        }

        public int FindClassId(string className)
        {
            if(this.classesByName.ContainsKey(className))
            {
                return this.classesByName[className];
            }
            else
            {
                // TODO: Class not found exception
                throw new Exception("Class not found.");
            }
        }

        private void LoadAttributeIndices(IndexCollection indices)
        {
            foreach (IndexDefinition index in indices)
            {
                if (index.KeyColumns.Count == 1)
                {
                    // We support only simple indexes
                    string columnName = index.KeyColumns[0].Name;
                    SchemaAttribute attr = this.attributesByColumnName[columnName];
                    attr.Index = index.Name;
                }
            }
        }

        private void LoadAttributeList(ColumnCollection columns)
        {
            this.attributesByName = new Dictionary<string, SchemaAttribute>(columns.Count);
            this.attributesById = new Dictionary<int, SchemaAttribute>(columns.Count);
            this.attributesByColumnName = new Dictionary<string, SchemaAttribute>(columns.Count);

            foreach (var column in columns)
            {
                string columnName = column.Name;
                var attr = new SchemaAttribute();
                attr.ColumnName = columnName;
                attr.ColumnID = column.Columnid;
                this.attributesByColumnName.Add(columnName, attr);
                if (columnName.StartsWith(AttributeColPrefix, false, CultureInfo.CurrentCulture))
                {
                    // Column is mapped to LDAP attribute
                    // Strip the ATTx prefix from column name to get the numeric attribute ID
                    string attributeIdStr = columnName.Substring(AttributeColPrefixLength, columnName.Length - AttributeColPrefixLength);
                    attr.Id = Int32.Parse(attributeIdStr);
                    attributesById.Add(attr.Id.Value, attr);
                }
                else
                {
                    // System column
                    attr.IsSystemOnly = true;
                    attr.SystemFlags = AttributeSystemFlags.NotReplicated | AttributeSystemFlags.Base | AttributeSystemFlags.DisallowRename | AttributeSystemFlags.Operational;
                    // Approximate Syntax from ColumnId
                    attr.Syntax = GetSyntaxFromColumnType(column.Columnid);
                    attr.OmSyntax = AttributeOmSyntax.Undefined;
                    if (columnName.EndsWith(SystemColSuffix))
                    {
                        // Strip the _col suffix
                        attr.Name = columnName.Substring(0, columnName.Length - SystemColSuffixLength);
                    }
                    else
                    {
                        attr.Name = columnName;
                    }
                    // Make it case-insensitive by always lowering the name:
                    this.attributesByName.Add(attr.Name.ToLower(), attr);
                }
            }
        }

        private void LoadAttributeProperties(IsamDatabase database)
        {
            Columnid attributeIdCol = this.attributesById[CommonDirectoryAttributes.AttributeIdId].ColumnID;
            Columnid ldapDisplayNameCol = this.attributesById[CommonDirectoryAttributes.LdapDisplayNameId].ColumnID;

            string attributeIdIndex = this.attributesById[CommonDirectoryAttributes.AttributeIdId].Index;
            string attributeNameIndex = this.attributesById[CommonDirectoryAttributes.LdapDisplayNameId].Index;

            using (Cursor cursor = database.OpenCursor(ADConstants.DataTableName))
            {
                cursor.SetCurrentIndex(attributeNameIndex);
                Columnid linkIdCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.LinkId);
                Columnid isSingleValuedCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.IsSingleValued);
                Columnid attributeSyntaxCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.AttributeSyntax);
                Columnid isInGlobalCatalogCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.IsInGlobalCatalog);
                Columnid searchFlagsCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.SearchFlags);
                Columnid systemOnlyCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.SystemOnly);
                Columnid syntaxCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.AttributeSyntax);
                Columnid omSyntaxCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.AttributeOmSyntax);
                Columnid cnCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.CommonName);
                Columnid rangeLowerCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.RangeLower);
                Columnid rangeUpperCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.RangeUpper);
                Columnid schemaGuidCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.SchemaGuid);
                Columnid systemFlagsCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.SystemFlags);
                Columnid isDefunctCol = this.LoadColumnIdByAttributeName(cursor, CommonDirectoryAttributes.IsDefunct);

                // Now traverse through all schema attributes and load their properties
                cursor.SetCurrentIndex(attributeIdIndex);
                cursor.MoveBeforeFirst();
                while (cursor.MoveNext())
                {
                    int attributeId = cursor.RetrieveColumnAsInt(attributeIdCol).Value;
                    SchemaAttribute attribute;
                    if (this.attributesById.ContainsKey(attributeId))
                    {
                        // Load additional info about an existing attribute
                        attribute = this.attributesById[attributeId];
                    }
                    else
                    {
                        // Load info about a new attribute
                        attribute = new SchemaAttribute();
                        attribute.Id = attributeId;
                        this.attributesById.Add(attributeId, attribute);
                    }
                    attribute.Name = cursor.RetrieveColumnAsString(ldapDisplayNameCol);
                    attribute.CommonName = cursor.RetrieveColumnAsString(cnCol);
                    attribute.RangeLower = cursor.RetrieveColumnAsInt(rangeLowerCol);
                    attribute.RangeUpper = cursor.RetrieveColumnAsInt(rangeUpperCol);
                    attribute.SchemaGuid = cursor.RetrieveColumnAsGuid(schemaGuidCol).Value;
                    attribute.IsDefunct = cursor.RetrieveColumnAsBoolean(isDefunctCol);
                    attribute.SystemFlags = cursor.RetrieveColumnAsAttributeSystemFlags(systemFlagsCol);
                    attribute.LinkId = cursor.RetrieveColumnAsInt(linkIdCol);
                    attribute.IsInGlobalCatalog = cursor.RetrieveColumnAsBoolean(isInGlobalCatalogCol);
                    attribute.IsSingleValued = cursor.RetrieveColumnAsBoolean(isSingleValuedCol);
                    attribute.SearchFlags = cursor.RetrieveColumnAsSearchFlags(searchFlagsCol);
                    attribute.IsSystemOnly = cursor.RetrieveColumnAsBoolean(systemOnlyCol);
                    attribute.Syntax = cursor.RetrieveColumnAsAttributeSyntax(syntaxCol);
                    attribute.OmSyntax = cursor.RetrieveColumnAsAttributeOmSyntax(omSyntaxCol);
                    // Make it case-insensitive by always lowering the name:
                    this.attributesByName.Add(attribute.Name.ToLower(), attribute);
                }
            }
        }

        private Columnid LoadColumnIdByAttributeName(Cursor cursor, string attributeName)
        {
            Columnid attributeIdCol = this.attributesById[CommonDirectoryAttributes.AttributeIdId].ColumnID;
            // Assume that attributeNameIndex is set as the current index
            cursor.GotoKey(Key.Compose(attributeName));
            int attributeId = cursor.RetrieveColumnAsInt(attributeIdCol).Value;
            return this.attributesById[attributeId].ColumnID;
        }

        private void LoadClassList(IsamDatabase database)
        {
            SchemaAttribute governsIdAttribute = this.FindAttribute(CommonDirectoryAttributes.GovernsId);
            Columnid ldapDisplayNameCol = this.FindColumnId(CommonDirectoryAttributes.LDAPDisplayName);
            Columnid dntCol = this.FindColumnId(CommonDirectoryAttributes.DNTag);
            this.classesByName = new Dictionary<string, int>();
            using (Cursor cursor = database.OpenCursor(ADConstants.DataTableName))
            {
                cursor.SetCurrentIndex(governsIdAttribute.Index);
                while (cursor.MoveNext())
                {
                    // TODO: Load more data about classes
                    int classId = cursor.RetrieveColumnAsDNTag(dntCol).Value;
                    string className = cursor.RetrieveColumnAsString(ldapDisplayNameCol);
                    classesByName.Add(className, classId);
                }
            }
        }

        private static AttributeSyntax GetSyntaxFromColumnType(Columnid column)
        {
            Type colType = column.Type;
            if(colType == typeof(int))
            {
                return AttributeSyntax.Int;
            }
            else if(colType == typeof(Int64))
            {
                return AttributeSyntax.Int64;
            }
            else if (colType == typeof(byte))
            {
                return AttributeSyntax.Bool;
            }
            else if (colType == typeof(byte[]))
            {
                return AttributeSyntax.OctetString;
            }
            else
            {
                return AttributeSyntax.Undefined;
            }
        }
    }
}