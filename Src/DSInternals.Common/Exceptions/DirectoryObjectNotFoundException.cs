namespace DSInternals.Common.Exceptions;
[Serializable]
public sealed class DirectoryObjectNotFoundException : DirectoryObjectException
{
    public DirectoryObjectNotFoundException(object objectIdentifier = null, Exception innerExcetion = null)
        : base(objectIdentifier, innerExcetion)
    {
    }

    public override string Message
    {
        get
        {
            return this.ObjectIdentifier != null ?
                $"Object with identity '{this.ObjectIdentifier}' has not been found." :
                "Could not find the requested object.";
        }
    }
}
