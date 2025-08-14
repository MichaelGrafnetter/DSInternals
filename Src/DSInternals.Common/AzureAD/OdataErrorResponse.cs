using System;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    public class OdataErrorResponse
    {
        [JsonPropertyName("odata.error")]
        [JsonRequired]
        public ODataError Error
        {
            get;
            private set;
        }

        public Exception GetException()
        {
            return new GraphApiException(this.Error);
        }
    }
}
