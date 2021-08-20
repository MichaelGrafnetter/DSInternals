namespace DSInternals.PowerShell
{
    using DSInternals.Common.Data;
    using DSInternals.DataStore;
    public class SchemaAttribute : ISchemaAttributeExt
    {
        public string ColumnName
        {
            get;
            set;
        }

        public string CommonName
        {
            get;
            set;
        }

        public int? Id
        {
            get;
            set;
        }

        public int? InternalId
        {
            get;
            set;
        }

        public string Oid
        {
            get;
            set;
        }

        public string Index
        {
            get;
            set;
        }

        public bool IsConstructed
        {
            get;
            set;
        }

        public bool IsDefunct
        {
            get;
            set;
        }

        public bool IsInAnr
        {
            get;
            set;
        }

        public bool IsIndexed
        {
            get;
            set;
        }

        public bool IsIndexedOverContainer
        {
            get;
            set;
        }

        public bool IsInGlobalCatalog
        {
            get;
            set;
        }

        public bool IsOnTombstonedObject
        {
            get;
            set;
        }

        public bool IsSingleValued
        {
            get;
            set;
        }

        public bool IsSystemOnly
        {
            get;
            set;
        }

        public bool IsTupleIndexed
        {
            get;
            set;
        }

        public int? LinkId
        {
            get;
            set;
        }

        public LinkType? LinkType
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public AttributeOmSyntax OmSyntax
        {
            get;
            set;
        }

        public int? RangeLower
        {
            get;
            set;
        }

        public int? RangeUpper
        {
            get;
            set;
        }

        public SearchFlags SearchFlags
        {
            get;
            set;
        }

        public System.Guid SchemaGuid
        {
            get;
            set;
        }

        public AttributeSyntax Syntax
        {
            get;
            set;
        }

        public string SyntaxOid
        {
            get;
            set;
        }

        public AttributeSystemFlags SystemFlags
        {
            get;
            set;
        }
    }
}
