namespace DSInternals.DataStore
{
    using System;
    using DSInternals.Common.Data;

    public interface ISchemaAttributeExt : ISchemaAttribute
    {
        string ColumnName { get; }
        string CommonName { get; }
        string Index { get; }
        bool IsConstructed { get; }
        bool IsDefunct { get; }
        bool IsInAnr { get; }
        bool IsIndexed { get; }
        bool IsIndexedOverContainer { get; }
        bool IsInGlobalCatalog { get; }
        bool IsOnTombstonedObject { get; }
        bool IsSingleValued { get; }
        bool IsSystemOnly { get; }
        bool IsTupleIndexed { get; }
        int? LinkId { get; }
        LinkType? LinkType { get; }
        AttributeOmSyntax OmSyntax { get; }
        int? RangeLower { get; }
        int? RangeUpper { get; }
        SearchFlags SearchFlags { get; }
        Guid SchemaGuid { get; }
        string SyntaxOid { get; }
        AttributeSystemFlags SystemFlags { get; }
    }
}
