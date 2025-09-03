namespace DSInternals.Common.Exceptions
{
    using System;

    /// <summary>
    /// Abstract base class for exceptions related to Active Directory operations and data processing.
    /// </summary>
    [Serializable]
    public abstract class DirectoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DirectoryException class with an optional inner exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DirectoryException(Exception innerException = null) : base(null, innerException)
        {
        }
    }
}