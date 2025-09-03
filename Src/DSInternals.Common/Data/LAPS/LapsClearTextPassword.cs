using System.Globalization;
using System;
using System.Text;
using System.Text.Json.Serialization;
using DSInternals.Common.Serialization;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Represents a LAPS (Local Administrator Password Solution) clear text password with account name, timestamp, and password value.
    /// </summary>
    /// <example>
    /// {"n":"Administrator","t":"1d8161b41c41cde","p":"A6a3#7%eb!57be4a4B95Z43394ba956de69e5d8975#$8a6d)4f82da6ad500HGx"}
    /// </example>
    /// <seealso>https://learn.microsoft.com/en-us/windows-server/identity/laps/laps-technical-reference</seealso>
    public class LapsClearTextPassword
    {
        [JsonPropertyName("n")]
        /// <summary>
        /// Gets or sets the name of the account whose password is stored.
        /// </summary>
        public string AccountName;

        [JsonPropertyName("t")]
        /// <summary>
        /// Gets or sets the update timestamp as a hexadecimal string representation of file time.
        /// </summary>
        public string UpdateTimestampString;

        [JsonPropertyName("p")]
        /// <summary>
        /// Gets or sets the clear text password value.
        /// </summary>
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

        /// <summary>
        /// Parses LAPS password data from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string containing LAPS password data.</param>
        /// <returns>A LapsClearTextPassword object parsed from the JSON.</returns>
        public static LapsClearTextPassword Parse(string json)
        {
            Validator.AssertNotNull(json, nameof(json));
            return LenientJsonSerializer.DeserializeLenient<LapsClearTextPassword>(json);
        }

        /// <summary>
        /// Parses LAPS password data from binary JSON data.
        /// </summary>
        /// <param name="binaryJson">The binary JSON data containing LAPS password information.</param>
        /// <param name="utf16">True if the binary data is UTF-16 encoded; otherwise, false for UTF-8.</param>
        /// <returns>A LapsClearTextPassword object parsed from the binary JSON.</returns>
        public static LapsClearTextPassword Parse(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
        {
            return LenientJsonSerializer.DeserializeLenient<LapsClearTextPassword>(binaryJson, utf16);
        }
    }
}
