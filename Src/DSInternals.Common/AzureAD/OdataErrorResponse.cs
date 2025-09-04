using System;
using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents an OData error response container from Azure AD Graph API.
    /// </summary>
    public class OdataErrorResponse
    {
        [JsonPropertyName("odata.error")]
        [JsonRequired]
        public ODataError Error
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an exception from this error response.
        /// </summary>
        /// <returns>A GraphApiException containing the error details.</returns>
        public Exception GetException()
        {
            return new GraphApiException(this.Error);
        }
    }
}
