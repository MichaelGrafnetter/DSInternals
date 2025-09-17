using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents an OData error from Azure AD Graph API responses.
    /// </summary>
    public class ODataError
    {
        /// <summary>
        /// Gets the error code returned by the Azure AD Graph API.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the detailed error message information returned by the Azure AD Graph API.
        /// </summary>
        [JsonPropertyName("message")]
        [JsonRequired]
        public ODataErrorMessage Message
        {
            get;
            private set;
        }
    }
}
