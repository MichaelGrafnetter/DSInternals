namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Exception thrown when Azure AD Graph API operations fail.
    /// </summary>
    public class GraphApiException : Exception
    {
        /// <summary>
        /// Gets the Azure AD error code associated with this exception.
        /// </summary>
        public string ErrorCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes a new instance of the GraphApiException class with a specified error message and optional error code.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="errorCode">The Azure AD error code, if available.</param>
        public GraphApiException(string message, string errorCode = null) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the GraphApiException class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public GraphApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphApiException class from an OData error.
        /// </summary>
        /// <param name="error">The OData error containing error details.</param>
        public GraphApiException(ODataError error) : base(error.Message.Value)
        {
            this.ErrorCode = error.Code;
        }
    }
}
