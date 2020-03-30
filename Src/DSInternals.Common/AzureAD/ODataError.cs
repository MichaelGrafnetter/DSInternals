using Newtonsoft.Json;

namespace DSInternals.Common.AzureAD
{
    public class ODataError
    {
        [JsonProperty("code")]
        public string Code
        {
            get;
            private set;
        }

        [JsonProperty("message", Required = Required.Always)]
        public ODataErrorMessage Message
        {
            get;
            private set;
        }
    }
}
