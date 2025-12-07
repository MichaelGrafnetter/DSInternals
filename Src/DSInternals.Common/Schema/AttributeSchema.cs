using System;

namespace DSInternals.Common.Schema
{
    /// <summary>
    /// The AttributeSchema class represents a schema property definition that is contained in the schema partition.
    /// </summary>
    public class AttributeSchema
    {
        /// <summary>
        /// Gets the ldapDisplayName of the attribute.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Gets the Common Name (CN) of the attribute.
        /// </summary>
        public readonly string? CommonName;

        /// <summary>
        /// Gets the unique X.500 OID for identifying an attribute, encoded as ATTRTYP.
        /// </summary>
        public readonly AttributeType AttributeId;

        /// <summary>
        /// Gets the unique X.500 OID for identifying an attribute.
        /// </summary>
        public readonly string? AttributeOid;

        /// <summary>
        /// Gets the optional internal ID of the attribute that is used as column and name suffix.
        /// </summary>
        public readonly AttributeType? InternalId;

        /// <summary>
        /// Gets the value for the link identifier when the schema property is linked.
        /// </summary>
        /// <remarks>An even integer denotes a forward link; an odd integer denotes a back link.</remarks>
        public readonly int? LinkId;

        /// <summary>
        /// Gets additional properties of the attribute.
        /// </summary>
        public readonly AttributeSystemFlags SystemFlags;

        /// <summary>
        /// Gets the minimum value or length that the schema property can have.
        /// </summary>
        public readonly int? RangeLower;

        /// <summary>
        /// Gets the maximum value or length that the schema property can have.
        /// </summary>
        public readonly int? RangeUpper;

        public readonly bool IsDefunct;

        /// <summary>
        /// Gets a value indicating whether the schema property is contained in the global catalog.
        /// </summary>
        public readonly bool IsInGlobalCatalog;

        /// <summary>
        /// Gets or sets a value indicating whether the schema property is single-valued.
        /// </summary>
        public readonly bool IsSingleValued;

        /// <summary>
        /// This attribute specifies whether an attribute is indexed, among other things.
        /// </summary>
        public readonly AttributeSearchFlags SearchFlags;

        /// <summary>
        /// Gets a value indicating whether only the system can modify this attribute.
        /// </summary>
        public readonly bool IsSystemOnly;

        /// <summary>
        /// Gets the property type (syntax) of the Attribute object.
        /// </summary>
        public readonly AttributeSyntax Syntax;

        /// <summary>
        /// Gets the schemaIDGuid for the attribute.
        /// </summary>
        public readonly Guid SchemaGuid;


        public readonly AttributeOmSyntax OmSyntax;

        public string? ColumnName;

        public string? IndexName;

        public string? ContainerizedIndexName;

        public string? TupleIndexName;

        public string? DerivedColumnName => (!IsConstructed && LinkType == LinkType.None) ? (InternalId ?? AttributeId).DeriveColumnName(Syntax) : null;

        public string? DerivedIndexName => (IsIndexed && LinkType == LinkType.None) ? (InternalId ?? AttributeId).DeriveIndexName() : null;

        public string? DerivedContainerizedIndexName => IsIndexedOverContainer ? (InternalId ?? AttributeId).DeriveContainerizedIndexName() : null;

        public string? DerivedTupleIndexName => IsTupleIndexed ? (InternalId ?? AttributeId).DeriveTupleIndexName() : null;

        /// <summary>
        /// Gets the link type for linked-value attributes.
        /// </summary>
        public LinkType LinkType
        {
            get
            {
                if (!this.LinkId.HasValue)
                {
                    return LinkType.None;
                }

                if (this.LinkId.Value % 2 == 0)
                {
                    return Schema.LinkType.ForwardLink;
                }
                else
                {
                    return LinkType.BackLink;
                }
            }
        }

        /// <summary>
        /// Gets the base number for forward links and backlinks.
        /// </summary>
        /// <remarks>Removes the rightmost bit that indicates if this is a forward link or a backlink.</remarks>
        public int? LinkBase => LinkId.HasValue ? this.LinkId.Value >> 1 : null;

        /// <summary>
        /// Gets the OID of the attribute syntax.
        /// </summary>
        public string SyntaxOid => PrefixTable.Translate(this.Syntax);

        public bool IsConstructed => this.SystemFlags.HasFlag(AttributeSystemFlags.Constructed);

        /// <summary>
        /// Gets a value indicating whether there is a tuple index for this schema property.
        /// </summary>
        public bool IsTupleIndexed => SearchFlags.HasFlag(AttributeSearchFlags.TupleIndex);

        /// <summary>
        /// Gets a value indicating whether the schema property is in the ANR set.
        /// </summary>
        public bool IsInAnr => SearchFlags.HasFlag(AttributeSearchFlags.AmbiguousNameResolution);

        /// <summary>
        /// Gets a value indicating whether the schema property is indexed in all containers.
        /// </summary>
        public bool IsIndexedOverContainer => SearchFlags.HasFlag(AttributeSearchFlags.ContainerIndex);

        /// <summary>
        /// Gets a value indicating whether the schema property is in the tombstone object that contains deleted properties.
        /// </summary>
        public bool IsOnTombstonedObject => SearchFlags.HasFlag(AttributeSearchFlags.PreserveOnDelete);

        /// <summary>
        /// Gets a value indicating whether the schema property is marked as confidential.
        /// </summary>
        public bool IsConfidential => SearchFlags.HasFlag(AttributeSearchFlags.Confidential);

        /// <summary>
        /// Gets a value indicating whether the schema property is indexed in the Active Directory Domain Services store.
        /// </summary>
        public bool IsIndexed => SearchFlags.HasFlag(AttributeSearchFlags.AttributeIndex);

        public override string ToString()
        {
            return String.Format("Att: {0}, Col: {1}", Name, ColumnName);
        }

        public AttributeSchema(
            string ldapDisplayName,
            string? attributeOid,
            AttributeType attributeId,
            AttributeSyntax syntax,
            AttributeSearchFlags searchFlags = AttributeSearchFlags.None)
        {
            if (ldapDisplayName == null)
            {
                throw new ArgumentNullException(nameof(ldapDisplayName));
            }

            this.Name = ldapDisplayName;
            this.AttributeOid = attributeOid;
            this.AttributeId = attributeId;
            this.Syntax = syntax;
            this.SearchFlags = searchFlags;
        }

        public AttributeSchema(
            string ldapDisplayName,
            string commonName,
            string? attributeOid,
            Guid schemaIdGuid,
            AttributeType attributeId,
            AttributeType? internalId,
            int? linkId,
            AttributeSyntax syntax,
            AttributeOmSyntax omSyntax,
            AttributeSearchFlags searchFlags,
            AttributeSystemFlags systemFlags,
            bool isSystemOnly,
            int? rangeLower,
            int? rangeUpper,
            bool isInGlobalCatalog,
            bool isSingleValued,
            bool isDefunct)
        {
            if (commonName == null)
            {
                throw new ArgumentNullException(nameof(commonName));
            }

            if (ldapDisplayName == null)
            {
                throw new ArgumentNullException(nameof(ldapDisplayName));
            }

            this.Name = ldapDisplayName;
            this.CommonName = commonName;
            this.AttributeOid = attributeOid;
            this.SchemaGuid = schemaIdGuid;
            this.RangeLower = rangeLower;
            this.RangeUpper = rangeUpper;
            this.AttributeId = attributeId;
            this.InternalId = internalId;
            this.LinkId = linkId;
            this.Syntax = syntax;
            this.OmSyntax = omSyntax;
            this.SystemFlags = systemFlags;
            this.SearchFlags = searchFlags;
            this.IsSystemOnly = isSystemOnly;
            this.IsInGlobalCatalog = isInGlobalCatalog;
            this.IsSingleValued = isSingleValued;
            this.IsDefunct = isDefunct;
        }

        public static AttributeSchema Create(string ldapDisplayName, PrefixTable prefixTable)
        {
            if (prefixTable == null)
            {
                throw new ArgumentNullException(nameof(prefixTable));
            }

            // The Translate method does nullability validation and throws the ArgumentNullException.
            AttributeType attributeId = CommonDirectoryAttributes.Translate(ldapDisplayName)
                ?? throw new ArgumentOutOfRangeException(nameof(ldapDisplayName), $"Could not translate the attribute {ldapDisplayName} to ATTRTYP.");

            AttributeSyntax syntax = attributeId.GetSyntax()
                ?? throw new ArgumentOutOfRangeException(nameof(ldapDisplayName), $"The syntax of attribute {ldapDisplayName} is unknown.");

            string attributeOid = prefixTable.Translate(attributeId)
                ?? throw new ArgumentOutOfRangeException(nameof(ldapDisplayName), $"Could not translate the {ldapDisplayName} attribute to OID.");

            AttributeSearchFlags searchFlags = attributeId.GetSearchFlags();

            return new AttributeSchema(
                ldapDisplayName,
                attributeOid,
                attributeId,
                syntax,
                searchFlags);
        }

        public static AttributeSchema Create(
            string ldapDisplayName,
            string attributeOid,
            AttributeSyntax syntax,
            PrefixTable prefixTable)
        {
            if (prefixTable == null)
            {
                throw new ArgumentNullException(nameof(prefixTable));
            }

            // The TranslateToAttributeType method already checks the attributeOid value for null.
            AttributeType attributeId = prefixTable.TranslateToAttributeType(attributeOid)
                ?? throw new ArgumentOutOfRangeException(nameof(attributeOid), "Could not translate the OID to ATTRTYP.");

            // The constructor already checks the ldapDisplayName value for null.
            return new AttributeSchema(ldapDisplayName, attributeOid, attributeId, syntax);
        }
    }
}
