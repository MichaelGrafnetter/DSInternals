using DSInternals.Common.Schema;

namespace DSInternals.Common.Exceptions;

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
    public SchemaAttributeNotFoundException(AttributeType attributeId)
        : base(null)
    {
        this.AttributeIdentifier = attributeId;
    }
    public override string Message
    {
        get
        {
            return $"Directory schema does not contain attribute '{this.AttributeIdentifier}'.";
        }
    }
}
