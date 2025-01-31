using Newtonsoft.Json;

namespace DSInternals.Common.Data
{
    public class LapsPassword
    {
        [JsonProperty("n")]
        public string AccountName;

        [JsonProperty("t")]
        public string UpdateTimestamp;

        [JsonProperty("p")]
        public string Password;

        public static LapsPassword Parse(string json)
        {
            return JsonConvert.DeserializeObject<LapsPassword>(json);
        }
    }
}
