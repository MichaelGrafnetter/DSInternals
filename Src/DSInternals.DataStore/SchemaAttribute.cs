namespace DSInternals.DataStore
{
    using System;
    using Microsoft.Database.Isam;
    using DSInternals.Common.Data;

    /// <summary>
    /// The ActiveDirectorySchemaAttribute class represents a schema property definition that is contained in the schema partition.
    /// </summary>
    //TODO: Rename Attribute to ActiveDirectorySchemaAttribute
    public class SchemaAttribute : ISchemaAttributeExt
    {
        /// <summary>
        /// Gets the ldapDisplayName of the ActiveDirectorySchemaAttribute object.
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets or sets the Common Name (CN) of the ActiveDirectorySchemaProperty object.
        /// </summary>
        public string CommonName
        {
            get;
            internal set;
        }

        // Contains compressed OID
        public int? Id
        {
            get;
            internal set;
        }

        // Corresponds to column name suffix
        public int? InternalId
        {
            get;
            internal set;
        }

        public string Oid
        {
            get;
            internal set;
        }

        public string ColumnName
        {
            get;
            internal set;
        }
        public Columnid ColumnID
        {
            get;
            internal set;
        }

        public string Index
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the value for the link identifier when the schema property is linked.
        /// </summary>
        /// <remarks>An even integer denotes a forward link; an odd integer denotes a back link.</remarks>
        public int? LinkId
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets or sets flags that define additional properties of the attribute.
        /// </summary>
        public AttributeSystemFlags SystemFlags
        {
            get;
            internal set;
        }

        public LinkType? LinkType
        {
            get
            {
                if(!this.LinkId.HasValue)
                {
                    return null;
                }
                if(this.LinkId.Value % 2 == 0)
                {
                    return DataStore.LinkType.ForwardLink;
                }
                else
                {
                    return DataStore.LinkType.BackLink;
                }
            }
        }

        public int? LinkBase
        {
            get
            {
                if (!this.LinkId.HasValue)
                {
                    // This attribute is not a linked value.
                    return null;
                }

                // Remove the rightmost bit that indicates if this is a forward link or a backlink.
                return this.LinkId.Value >> 1;
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the minimum value or length that the schema property can have.
        /// </summary>
        public int? RangeLower
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets or sets a value that represents the maximum value or length that the schema property can have.
        /// </summary>
        public int? RangeUpper
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the schema property is indexed in the Active Directory Domain Services store.
        /// </summary>
        public bool IsIndexed
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.AttributeIndex);
            }
        }

        public bool IsDefunct
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the schema property is contained in the global catalog.
        /// </summary>
        public bool IsInGlobalCatalog
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the schema property is single-valued.
        /// </summary>
        public bool IsSingleValued
        {
            get;
            internal set;
        }
        /// <summary>
        /// This attribute specifies whether an attribute is indexed, among other things.
        /// </summary>
        public SearchFlags SearchFlags
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets a value indicating whether there is a tuple index for this schema property.
        /// </summary>
        public bool IsTupleIndexed
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.TupleIndex);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the schema property is in the ANR set.
        /// </summary>
        public bool IsInAnr
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.AmbiguousNameResolution);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the schema property is indexed in all containers.
        /// </summary>
        public bool IsIndexedOverContainer
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.ContainerIndex);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the schema property is in the tombstone object that contains deleted properties.
        /// </summary>
        public bool IsOnTombstonedObject
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.PreserveOnDelete);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the schema property is marked as confidential.
        /// </summary>
        public bool IsConfidential
        {
            get
            {
                return this.SearchFlags.HasFlag(SearchFlags.Confidential);
            }
        }

        /// <summary>
        /// Gets a value indicating whether only the system can modify this attribute.
        /// </summary>
        public bool IsSystemOnly
        {
            get;
            internal set;
        }

        public bool IsConstructed
        {
            get
            {
                return this.SystemFlags.HasFlag(AttributeSystemFlags.Constructed);
            }
        }

        /// <summary>
        /// Gets or sets an ActiveDirectorySyntax object indicating the property type (syntax) of the Attribute object.
        /// </summary>
        public AttributeSyntax Syntax
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets or sets the schemaIDGuid for the ActiveDirectorySchemaProperty object.
        /// </summary>
        public Guid SchemaGuid
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the OID of the attribute syntax.
        /// </summary>
        public string SyntaxOid
        {
            get
            {
                return PrefixMap.GetAttributeSyntaxOid(this.Syntax);
            }
        }

        public AttributeOmSyntax OmSyntax
        {
            get;
            internal set;
        }

        public override string ToString()
        {
            return String.Format("Att: {0}, Col: {1}", Name, ColumnName);
        }
    }
}
