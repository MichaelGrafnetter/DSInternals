using System.Globalization;
using System;
using System.Text;
using System.Text.Json.Serialization;
using DSInternals.Common.Serialization;

namespace DSInternals.Common.Data
{
    /// <example>
    /// {"n":"Administrator","t":"1d8161b41c41cde","p":"A6a3#7%eb!57be4a4B95Z43394ba956de69e5d8975#$8a6d)4f82da6ad500HGx"}
    /// </example>
    /// <seealso>https://learn.microsoft.com/en-us/windows-server/identity/laps/laps-technical-reference</seealso>
    public class LapsClearTextPassword
    {
        [JsonPropertyName("n")]
        public string AccountName;

        [JsonPropertyName("t")]
        public string UpdateTimestampString;

        [JsonPropertyName("p")]
        public string Password;

        [JsonIgnore]
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
            return LenientJsonSerializer.DeserializeLenient<LapsClearTextPassword>(json);
        }

        public static LapsClearTextPassword Parse(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
        {
            return LenientJsonSerializer.DeserializeLenient<LapsClearTextPassword>(binaryJson, utf16);
        }
    }
}
