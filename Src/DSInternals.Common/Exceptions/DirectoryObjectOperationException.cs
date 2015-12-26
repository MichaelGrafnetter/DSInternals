using DSInternals.Common.Properties;
using System;
using System.Security.Principal;

namespace DSInternals.Common.Exceptions
{
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
                return String.Format(Resources.OperationExceptionMessageFormat, this.Reason, this.ObjectIdentifier);
            }
        }
    }
}
