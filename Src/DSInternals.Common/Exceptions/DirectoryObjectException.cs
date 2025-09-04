using DSInternals.Common.Data;
using System;
using System.Security.Principal;

namespace DSInternals.Common.Exceptions
{
    /// <summary>
    /// Abstract base class for exceptions related to specific Active Directory objects.
    /// </summary>
    [Serializable]
    public abstract class DirectoryObjectException : DirectoryException
    {
        /// <summary>
        /// Gets the identifier of the directory object that caused the exception.
        /// </summary>
        public object ObjectIdentifier
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the DirectoryObjectException class with the specified object identifier.
        /// </summary>
        /// <param name="objectIdentifier">The identifier of the directory object that caused the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DirectoryObjectException(object objectIdentifier, Exception innerException = null) : base(innerException)
        {
            this.ObjectIdentifier = objectIdentifier;
        }
    }
}
