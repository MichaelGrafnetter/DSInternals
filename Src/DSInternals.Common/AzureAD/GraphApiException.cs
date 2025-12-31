namespace DSInternals.Common.AzureAD;

public class GraphApiException : Exception
{
    public string ErrorCode
    {
        get;
        protected set;
    }

    public GraphApiException(string message, string errorCode = null) : base(message)
    {
        this.ErrorCode = errorCode;
    }

    public GraphApiException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public GraphApiException(ODataError error) : base(error.Message.Value)
    {
        this.ErrorCode = error.Code;
    }
}
