namespace DSInternals.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an operation on a directory object fails for a specific reason.
    /// </summary>
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
                return $"{this.Reason} (Object identity: '{this.ObjectIdentifier}')";
            }
        }
    }
}
