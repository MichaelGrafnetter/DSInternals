using DSInternals.Common.Data;
using System;
using System.Security.Principal;

namespace DSInternals.Common.Exceptions
{
    [Serializable]
    public abstract class DirectoryObjectException : DirectoryException
    {
        public object ObjectIdentifier
        {
            get;
            private set;
        }

        /// <summary>
        /// base implementation.
        /// </summary>
        public DirectoryObjectException(object objectIdentifier, Exception innerException = null) : base(innerException)
        {
            this.ObjectIdentifier = objectIdentifier;
        }
    }
}
