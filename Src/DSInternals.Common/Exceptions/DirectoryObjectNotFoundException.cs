namespace DSInternals.Common.Exceptions
{
    using System;

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
                    String.Format("Object with identity '{0}' has not been found.", this.ObjectIdentifier) :
                    "Could not find the requested object.";
            }
        }
    }
}
