namespace DSInternals.Common.Exceptions
{
    using DSInternals.Common.Properties;
    using System;
    using System.Security.Principal;

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
                    String.Format(Resources.ObjectWithIdentityNotFoundMessageFormat, this.ObjectIdentifier) :
                    Resources.ObjectNotFoundMessage;
            }
        }
    }
}
