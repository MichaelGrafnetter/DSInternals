﻿namespace DSInternals.Common.Exceptions
{
    using System;

    [Serializable]
    public sealed class SchemaAttributeNotFoundException : DirectoryException
    {
        public object AttributeIdentifier
        {
            get;
            private set;
        }
        public SchemaAttributeNotFoundException(string attributeName) : base(null)
        {
            this.AttributeIdentifier = attributeName;
        }
        public SchemaAttributeNotFoundException(int attributeId)
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
