namespace DSInternals.Common.Exceptions;

[Serializable]
public class DirectoryObjectOperationException : DirectoryObjectException
{
    public string Reason
    {
        get;
        private set;
    }
    public DirectoryObjectOperationException(string reason, object objectIdentifier)
        : base(objectIdentifier)
    {
        this.Reason = reason;
    }

    public override string Message
    {
        get
        {
            return $"{this.Reason} (Object identity: '{this.ObjectIdentifier}')";
        }
    }
}
