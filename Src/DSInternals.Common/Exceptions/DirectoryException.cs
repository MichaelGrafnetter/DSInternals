namespace DSInternals.Common.Exceptions
{
    using System;

    [Serializable]
    public abstract class DirectoryException : Exception
    {
        /// <summary>
        /// base implementation.
        /// </summary>
        public DirectoryException(Exception innerException = null) : base(null, innerException)
        {
        }
    }
}