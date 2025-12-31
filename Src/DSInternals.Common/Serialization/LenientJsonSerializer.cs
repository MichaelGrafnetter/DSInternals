using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Serialization;

/// <summary>
/// Provides JSON serialization with relaxed parsing rules for handling AD/Azure AD data formats.
/// </summary>
/// <remarks>
/// This serializer adds capabilities not available in System.Text.Json:
/// - Single-quoted JSON normalization (converts '...' to "...")
/// - Binary JSON decoding with UTF-8/UTF-16 support
/// - Zero-terminator trimming for AD binary attributes
/// - UTF-16 BOM handling
/// </remarks>
public static class LenientJsonSerializer
{
    /// <summary>
    /// Shared JSON serializer options for all JSON operations in DSInternals.
    /// </summary>
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        IncludeFields = true, // LAPS uses fields (n/t/p)
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        Converters = { new JsonStringEnumConverter() }
    };

    // ---------- String input ----------
    public static T DeserializeLenient<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        // Strip UTF-8/UTF-16 BOM if present
        if (json.Length > 0 && json[0] == '\uFEFF')
            json = json.Substring(1);

        try
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }
        catch (JsonException)
        {
            if (LooksLikeSingleQuotedJson(json))
            {
                var normalized = NormalizeSingleQuotedJson(json);
                return JsonSerializer.Deserialize<T>(normalized, Options);
            }
            throw;
        }
    }

    // ---------- Binary input ----------
    public static T DeserializeLenient<T>(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
    {
        var json = DecodeJson(binaryJson, utf16);
        return DeserializeLenient<T>(json);
    }

    public static string DecodeJson(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
    {
        // Trim terminators/padding on BYTES BEFORE decoding.
        var trimmed = TrimZeroTerminator(binaryJson, utf16);

#if NET8_0_OR_GREATER
        string json = (utf16 ? Encoding.Unicode : Encoding.UTF8).GetString(trimmed);
#else
        // .NET Framework: no span GetString; copy to array explicitly.
        byte[] buf = trimmed.Length == 0 ? Array.Empty<byte>() : new byte[trimmed.Length];
        if (buf.Length != 0)
        {
            for (int i = 0; i < trimmed.Length; i++)
            {
                buf[i] = trimmed[i];
            }
        }
        string json = (utf16 ? Encoding.Unicode : Encoding.UTF8).GetString(buf);
#endif
        // Strip BOM if present
        if (json.Length > 0 && json[0] == '\uFEFF')
            json = json.Substring(1);

        return json;
    }

    public static ReadOnlySpan<byte> TrimZeroTerminator(ReadOnlySpan<byte> input, bool utf16)
    {
        if (input.Length == 0) return input;

        if (utf16)
        {
            // Remove any number of trailing 0x00 0x00 pairs.
            int len = input.Length;
            while (len >= 2 && input[len - 1] == 0 && input[len - 2] == 0)
                len -= 2;

            // Safety: ensure even byte count for UTF-16
            if ((len & 1) == 1) len -= 1;

            return input.Slice(0, len);
        }
        else
        {
            int len = input.Length;
            while (len > 0 && input[len - 1] == 0)
                len--;
            return input.Slice(0, len);
        }
    }

    public static bool LooksLikeSingleQuotedJson(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        var t = s.TrimStart();
        return (t.Length > 0 && (t[0] == '{' || t[0] == '['))
               && s.IndexOf('"') < 0
               && s.IndexOf('\'') >= 0;
    }

    // Converts '…' to "…" and preserves apostrophes inside strings (\' -> ')
    public static string NormalizeSingleQuotedJson(string input)
    {
        var sb = new StringBuilder(input.Length);
        bool inString = false;
        char quote = '\0';

        for (int i = 0; i < input.Length; i++)
        {
            char ch = input[i];

            // Inside a single-quoted string, turn \' into a literal apostrophe
            if (inString && quote == '\'' && ch == '\\' && i + 1 < input.Length && input[i + 1] == '\'')
            {
                sb.Append('\'');
                i++; // skip the '
                continue;
            }

            if (ch == '\'' || ch == '"')
            {
                if (!inString)
                {
                    inString = true;
                    quote = ch;
                    sb.Append('"'); // open
                    continue;
                }
                else if (ch == quote)
                {
                    inString = false;
                    sb.Append('"'); // close
                    continue;
                }
            }

            sb.Append(ch);
        }

        return sb.ToString();
    }
}
