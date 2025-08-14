using System.Text.Json.Serialization;

namespace DSInternals.Common.AzureAD
{
    public class ODataError
    {
        [JsonPropertyName("code")]
        public string Code
        {
            get;
            private set;
        }

        [JsonPropertyName("message")]
        [JsonRequired]
        public ODataErrorMessage Message
        {
            get;
            private set;
        }
    }
}
