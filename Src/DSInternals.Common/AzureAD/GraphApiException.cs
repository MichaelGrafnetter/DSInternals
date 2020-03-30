using System;

namespace DSInternals.Common.AzureAD
{
    public class GraphApiException : Exception
    {
        public GraphApiException(string message) : base(message)
        {
        }

        public GraphApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GraphApiException(ODataError error) : base(error.Message.Value)
        {
            this.Data.Add("ErrorCode", error.Code);
        }
    }
}
