using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    /// <summary>
    /// Represents an OData error message with language and value information.
    /// </summary>
    public class ODataErrorMessage
    {
        [JsonPropertyName("lang")]
        public string Language
        {
            get;
            private set;
        }

        [JsonPropertyName("value")]
        [JsonRequired]
        public string Value
        {
            get;
            private set;
        }
    }
}
