namespace DSInternals.Common.Exceptions;
[Serializable]
public abstract class DirectoryException : Exception
{
    public DirectoryException(Exception innerException = null) : base(null, innerException)
    {
    }
}
