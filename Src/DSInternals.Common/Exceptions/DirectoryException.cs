namespace DSInternals.Common.Exceptions
{
    using System;

    [Serializable]
    public abstract class DirectoryException : Exception
    {
        public DirectoryException(Exception innerException = null) : base(null, innerException)
        {
        }
    }
}