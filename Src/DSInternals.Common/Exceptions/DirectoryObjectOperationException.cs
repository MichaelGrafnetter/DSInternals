namespace DSInternals.Common.Exceptions
{
    [Serializable]
    /// <summary>
    /// Represents a DirectoryObjectOperationException.
    /// </summary>
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
