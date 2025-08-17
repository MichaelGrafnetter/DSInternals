using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Serialization
{
    internal static class DsiJson
    {
        // One place to set behavior for all JSON in DSInternals
        internal static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            IncludeFields = true, // LAPS uses fields (n/t/p)
            NumberHandling = JsonNumberHandling.AllowReadingFromString, // Newtonsoft parity for quoted numbers
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        static DsiJson()
        {
            Options.Converters.Add(new JsonStringEnumConverter());
        }

        // ---------- STRING INPUT ----------
        internal static T? DeserializeLenient<T>(string json)
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

        // ---------- BINARY INPUT ----------
        internal static T? DeserializeLenient<T>(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
        {
            if (binaryJson.Length == 0) return default;

            var trimmed = TrimZeroTerminator(binaryJson, utf16);

            if (!utf16)
            {
                // UTF-8 path
                trimmed = TrimUtf8Bom(trimmed);

#if NET8_0_OR_GREATER
                // Fast-path: no string allocation if it already looks like proper JSON
                if (!LooksLikeSingleQuotedJsonUtf8(trimmed))
                    return JsonSerializer.Deserialize<T>(trimmed, Options);

                string s = Encoding.UTF8.GetString(trimmed);
                return DeserializeLenient<T>(s);
#else
                // .NET Framework: decode to string and reuse string path
                byte[] buf = new byte[trimmed.Length];
                for (int i = 0; i < trimmed.Length; i++) buf[i] = trimmed[i];
                string s = Encoding.UTF8.GetString(buf);
                return DeserializeLenient<T>(s);
#endif
            }
            else
            {
                // UTF-16LE path
                // Copy span to array (net48-safe), then transcode to UTF-8 using Encoding.Convert
                byte[] utf16le = new byte[trimmed.Length];
                for (int i = 0; i < trimmed.Length; i++) utf16le[i] = trimmed[i];

                byte[] utf8 = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, utf16le, 0, utf16le.Length);

#if NET8_0_OR_GREATER
                ReadOnlySpan<byte> utf8Span = utf8;
                utf8Span = TrimUtf8Bom(utf8Span);
                if (!LooksLikeSingleQuotedJsonUtf8(utf8Span))
                    return JsonSerializer.Deserialize<T>(utf8Span, Options);
#endif
                // Fallback: go through string to apply single-quote normalization if needed
                string s = Encoding.UTF8.GetString(utf8);
                return DeserializeLenient<T>(s);
            }
        }

        // ---------- Helpers ----------
        private static ReadOnlySpan<byte> TrimZeroTerminator(ReadOnlySpan<byte> input, bool utf16)
        {
            if (input.Length == 0) return input;

            if (utf16)
            {
                int len = input.Length;
                // remove trailing 0x00 0x00 pairs
                while (len >= 2 && input[len - 1] == 0 && input[len - 2] == 0)
                    len -= 2;

                // ensure even count for UTF-16
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

        private static ReadOnlySpan<byte> TrimUtf8Bom(ReadOnlySpan<byte> data)
        {
            if (data.Length >= 3 && data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
                return data.Slice(3);
            return data;
        }

        private static bool LooksLikeSingleQuotedJson(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            // manual trim-start (no allocation)
            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == ' ' || c == '\t' || c == '\r' || c == '\n') { i++; continue; }
                break;
            }
            if (i >= s.Length) return false;

            char first = s[i];
            if (first != '{' && first != '[') return false;

            // heuristic: has at least one ' and no "
            return s.IndexOf('"', i) < 0 && s.IndexOf('\'', i) >= 0;
        }

        private static bool LooksLikeSingleQuotedJsonUtf8(ReadOnlySpan<byte> s)
        {
            int i = 0;
            while (i < s.Length)
            {
                byte b = s[i];
                if (b == (byte)' ' || b == (byte)'\t' || b == (byte)'\r' || b == (byte)'\n') { i++; continue; }
                break;
            }
            if (i >= s.Length) return false;

            byte first = s[i];
            if (first != (byte)'{' && first != (byte)'[') return false;

            bool hasDouble = false, hasSingle = false;
            for (int j = i; j < s.Length; j++)
            {
                byte b = s[j];
                if (b == (byte)'"') { hasDouble = true; break; }
                if (b == (byte)'\'') { hasSingle = true; }
            }
            return !hasDouble && hasSingle;
        }

        // Converts '…' to "…" and preserves apostrophes inside strings (\' -> ')
        private static string NormalizeSingleQuotedJson(string input)
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
}
