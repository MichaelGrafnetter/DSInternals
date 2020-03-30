using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    public class ODataErrorMessage
    {
        [JsonProperty("lang")]
        public string Language
        {
            get;
            private set;
        }

        [JsonProperty("value", Required = Required.Always)]
        public string Value
        {
            get;
            private set;
        }
    }
}
