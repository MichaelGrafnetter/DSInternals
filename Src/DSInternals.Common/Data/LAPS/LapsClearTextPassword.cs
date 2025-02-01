using Newtonsoft.Json;

namespace DSInternals.Common.Data
{
    public class LapsClearTextPassword
    {
        [JsonProperty("n")]
        public string AccountName;

        [JsonProperty("t")]
        public string UpdateTimestamp;

        [JsonProperty("p")]
        public string Password;

        public static LapsClearTextPassword Parse(string json)
        {
            return JsonConvert.DeserializeObject<LapsClearTextPassword>(json);
        }
    }
}
