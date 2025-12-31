using DSInternals.Common.Schema;
using DSInternals.Replication.Model;

namespace DSInternals.Replication;

public class ReplicationSchema : BaseSchema
{
    public void AddSchemaObject(ReplicaObject schemaObject)
    {
        ArgumentNullException.ThrowIfNull(schemaObject);

        if (schemaObject.IsDeleted)
        {
            // Ignore deleted schema objects
            return;
        }

        schemaObject.ReadAttribute(AttributeType.IsDefunct, out bool isDefunct);

        if (isDefunct)
        {
            // Ignore decommissioned attributes and classes
            return;
        }


        ClassType[] objectClass = schemaObject.ObjectClass;

        if (objectClass.Contains(ClassType.ClassSchema))
        {
            // This is a class schema
            // TODO: Read additional class attributes
            schemaObject.ReadAttribute(AttributeType.GovernsId, out uint? governsId);
            schemaObject.ReadAttribute(AttributeType.LdapDisplayName, out string? ldapDisplayName);

            // These attributes must always be populated for class schema objects
            if (ldapDisplayName != null && governsId.HasValue)
            {
                this.ClassesByName[ldapDisplayName] = (ClassType)governsId.Value;
            }
        }
        else if (objectClass.Contains(ClassType.AttributeSchema))
        {
            // This is an attribute schema
            schemaObject.ReadAttribute(AttributeType.LdapDisplayName, out string? ldapDisplayName);
            schemaObject.ReadAttribute(AttributeType.RDN, out string? commonName);
            schemaObject.ReadAttribute(AttributeType.SystemOnly, out bool isSystemOnly);
            schemaObject.ReadAttribute(AttributeType.RangeLower, out int? rangeLower);
            schemaObject.ReadAttribute(AttributeType.RangeUpper, out int? rangeUpper);
            schemaObject.ReadAttribute(AttributeType.LinkId, out int? linkId);
            schemaObject.ReadAttribute(AttributeType.IsInGlobalCatalog, out bool isInGlobalCatalog);
            schemaObject.ReadAttribute(AttributeType.IsSingleValued, out bool isSingleValued);

            schemaObject.ReadAttribute(AttributeType.AttributeId, out uint? numericAttributeId);
            AttributeType? attributeId = numericAttributeId.HasValue ? (AttributeType?)numericAttributeId.Value : null;

            schemaObject.ReadAttribute(AttributeType.InternalId, out uint? numericInternalId);
            AttributeType? internalId = numericInternalId.HasValue ? (AttributeType?)numericInternalId.Value : null;

            schemaObject.ReadAttribute(AttributeType.SystemFlags, out int? numericSystemFlags);
            AttributeSystemFlags systemFlags = numericSystemFlags.HasValue ? (AttributeSystemFlags)numericSystemFlags.Value : AttributeSystemFlags.None;

            schemaObject.ReadAttribute(AttributeType.SchemaIdGuid, out Guid? schemaIdGuid);

            schemaObject.ReadAttribute(AttributeType.SearchFlags, out int? numericSearchFlags);
            AttributeSearchFlags searchFlags = numericSearchFlags.HasValue ? (AttributeSearchFlags)numericSearchFlags.Value : AttributeSearchFlags.None;

            schemaObject.ReadAttribute(AttributeType.AttributeSyntax, out uint? numericSyntax);
            AttributeSyntax syntax = numericSyntax.HasValue ? (AttributeSyntax)numericSyntax.Value : AttributeSyntax.Undefined;

            schemaObject.ReadAttribute(AttributeType.OMSyntax, out int? numericOmSyntax);
            AttributeOmSyntax omSyntax = numericOmSyntax.HasValue ? (AttributeOmSyntax)numericOmSyntax.Value : AttributeOmSyntax.Undefined;

            string? attributeOid = this.PrefixTable.Translate(attributeId.Value);

            // Construct the attribute
            AttributeSchema attribute = new(
                ldapDisplayName,
                commonName,
                attributeOid,
                schemaIdGuid ?? Guid.Empty, // This should never happen, as all AD attributes must have a GUID.
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

            AddAttribute(attribute);
        }
    }

    // Clean AD contains 1500+ attributes and Exchange adds many more, but 3K should be enough for everyone.
    protected override int InitialAttributeDictionaryCapacity => 3000;

    // Clean AD contains 250+ classes and Exchange adds many more, but 500 should be enough for everyone.
    protected override int InitialClassDictionaryCapacity => 500;
}
