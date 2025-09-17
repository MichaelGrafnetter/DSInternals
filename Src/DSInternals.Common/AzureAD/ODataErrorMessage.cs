using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents an OData error message with language and value information.
    /// </summary>
    public class ODataErrorMessage
    {
        /// <summary>
        /// Gets the language code for the error message.
        /// </summary>
        [JsonPropertyName("lang")]
        public string Language
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the actual error message text from the Azure AD Graph API.
        /// </summary>
        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value
        {
            get;
            private set;
        }
    }
}
