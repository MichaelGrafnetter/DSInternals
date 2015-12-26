namespace DSInternals.Common.Exceptions
{
    using DSInternals.Common.Properties;
    using System;

    [Serializable]
    public sealed class SchemaAttributeNotFoundException : DirectoryException
    {
        public object AttributeIdentifier
        {
            get;
            private set;
        }
        public SchemaAttributeNotFoundException(string attributeName)
        {
            this.AttributeIdentifier = attributeName;
        }
        public SchemaAttributeNotFoundException(int attributeId)
        {
            this.AttributeIdentifier = attributeId;
        }
        public override string Message
        {
            get
            {
                return string.Format(Resources.AttributeNotFoundMessageFormat, this.AttributeIdentifier);
            }
        }
    }
}