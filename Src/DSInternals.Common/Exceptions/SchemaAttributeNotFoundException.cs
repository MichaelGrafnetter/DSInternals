namespace DSInternals.Common.Exceptions
{
    using DSInternals.Common.Schema;
    using System;

    [Serializable]
    public sealed class SchemaAttributeNotFoundException : DirectoryException
    {
        public object AttributeIdentifier
        {
            get;
            private set;
        }
        /// <summary>
        /// Initializes a new instance of the SchemaAttributeNotFoundException class with the specified attribute name.
        /// </summary>
        /// <param name="attributeName">The name of the attribute that was not found.</param>
        public SchemaAttributeNotFoundException(string attributeName) : base(null)
        {
            this.AttributeIdentifier = attributeName;
        }
        /// <summary>
        /// Initializes a new instance of the SchemaAttributeNotFoundException class with the specified attribute type.
        /// </summary>
        /// <param name="attributeId">The attribute type that was not found.</param>
        public SchemaAttributeNotFoundException(AttributeType attributeId)
            : base(null)
        {
            this.AttributeIdentifier = attributeId;
        }
        public override string Message
        {
            get
            {
                return string.Format("Directory schema does not contain attribute '{0}'.", this.AttributeIdentifier);
            }
        }
    }
}
