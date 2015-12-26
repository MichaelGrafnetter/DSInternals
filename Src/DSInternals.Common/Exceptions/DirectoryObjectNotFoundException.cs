namespace DSInternals.Common.Exceptions
{
    using DSInternals.Common.Properties;
    using System;
    using System.Security.Principal;

    [Serializable]
    public sealed class DirectoryObjectNotFoundException : DirectoryObjectException
    {
        public DirectoryObjectNotFoundException(object objectIdentifier)
            : base(objectIdentifier)
        {
        }

        public override string Message
        {
            get
            {
                return String.Format(Resources.ObjectNotFoundMessageFormat,this.ObjectIdentifier);
            }
        }
    }
}
