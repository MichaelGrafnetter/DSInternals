using System.Globalization;
using System;
using System.Text;
using Newtonsoft.Json;

namespace DSInternals.Common.Data
{
    /// <example>
    /// {"n":"Administrator","t":"1d8161b41c41cde","p":"A6a3#7%eb!57be4a4B95Z43394ba956de69e5d8975#$8a6d)4f82da6ad500HGx"}
    /// </example>
    /// <seealso>https://learn.microsoft.com/en-us/windows-server/identity/laps/laps-technical-reference</seealso>
    public class LapsClearTextPassword
    {
        [JsonProperty("n")]
        public string AccountName;

        [JsonProperty("t")]
        public string UpdateTimestampString;

        [JsonProperty("p")]
        public string Password;

        [JsonIgnore()]
        public DateTime? UpdateTimestamp
        {
            get
            {
                if(String.IsNullOrEmpty(UpdateTimestampString))
                {
                    return null;
                }

                return DateTime.FromFileTimeUtc(long.Parse(UpdateTimestampString, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
            }
            set
            {
                if(value.HasValue)
                {
                    UpdateTimestampString = value.Value.ToFileTimeUtc().ToString("x");
                }
                else
                {
                    UpdateTimestampString = null;
                }
            }
        }

        public static LapsClearTextPassword Parse(string json)
        {
            Validator.AssertNotNull(json, nameof(json));
            return JsonConvert.DeserializeObject<LapsClearTextPassword>(json);
        }

        public static LapsClearTextPassword Parse(byte[] binaryJson)
        {
            Validator.AssertNotNull(binaryJson, nameof(binaryJson));

            string json = Encoding.UTF8.GetString(binaryJson);
            return Parse(json);
        }
    }
}
