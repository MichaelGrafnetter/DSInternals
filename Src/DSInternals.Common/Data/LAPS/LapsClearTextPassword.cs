using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

/// <summary>
/// Represents the clear-text password of a local administrator account managed by LAPS.
/// </summary>
/// <example>
/// {"n":"Administrator","t":"1d8161b41c41cde","p":"A6a3#7%eb!57be4a4B95Z43394ba956de69e5d8975#$8a6d)4f82da6ad500HGx"}
/// </example>
/// <seealso>https://learn.microsoft.com/en-us/windows-server/identity/laps/laps-technical-reference</seealso>
public class LapsClearTextPassword
{
    /// <summary>
    /// Contains the name of the managed local administrator account.
    /// </summary>
    [JsonPropertyName("n")]
    [JsonRequired]
    public string AccountName { get; set; }

    /// <summary>
    /// Contains the UTC password update time represented as a 64-bit hexadecimal number.
    /// </summary>
    [JsonPropertyName("t")]
    [JsonRequired]
    public string UpdateTimestampString { get; set; }

    /// <summary>
    /// Contains the clear-text password.
    /// </summary>
    [JsonPropertyName("p")]
    [JsonRequired]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the password update time as a <see cref="DateTime"/> object.
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTimestamp
    {
        get
        {
            if (String.IsNullOrEmpty(UpdateTimestampString))
            {
                return null;
            }

            return DateTime.FromFileTimeUtc(long.Parse(UpdateTimestampString, NumberStyles.HexNumber, CultureInfo.InvariantCulture));
        }
        set
        {
            if (value.HasValue)
            {
                UpdateTimestampString = value.Value.ToFileTimeUtc().ToString("x", DateTimeFormatInfo.InvariantInfo);
            }
            else
            {
                UpdateTimestampString = null;
            }
        }
    }

    /// <summary>
    /// Parses a JSON string containing the clear-text password information.
     /// </summary>
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <returns>>A <see cref="LapsClearTextPassword"/> object containing the parsed information.</returns>
    public static LapsClearTextPassword? Parse(string json)
    {
        return JsonSerializer.Deserialize(json, LapsSerializationContext.Default.LapsClearTextPassword);
    }

    /// <summary>
    /// Parses a binary JSON string containing the clear-text password information.
    /// The binary JSON data is expected to be UTF-16 encoded with an optional trailing \0 character.
    /// </summary>
    /// <param name="binaryJson">The binary JSON data to parse.</param>
    /// <returns>A <see cref="LapsClearTextPassword"/> object containing the parsed information.</returns>
    public static LapsClearTextPassword? Parse(ReadOnlySpan<byte> binaryJson, bool isUtf16 = false)
    {
        // The JSON data is expected to be UTF-16 encoded with an optional trailing \0x00 byte.
        if (binaryJson.Length == 0)
        {
            return null;
        }

        if (isUtf16)
        {
            if (binaryJson.Length % 2 != 0)
            {
                throw new FormatException("The input data is not a valid UTF-16 encoded JSON string.");
            }

            if (binaryJson[^1] == 0 || binaryJson[^2] == 0)
            {
                binaryJson = binaryJson[..^2]; // Trim the optional trailing \0x00 bytes
            }

            string json = Encoding.Unicode.GetString(binaryJson);
            return Parse(json);
        }
        else
        {
            // UTF-8 parsing is more efficient
            return JsonSerializer.Deserialize(binaryJson, LapsSerializationContext.Default.LapsClearTextPassword);
        }
    }
}
