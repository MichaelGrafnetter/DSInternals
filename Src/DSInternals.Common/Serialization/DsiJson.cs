using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Serialization
{
    // Public so tests/benchmarks can call it; keep surface small (methods only).
    public static class DsiJson
    {
        // --- READ: lenient & tolerant ---
        internal static readonly JsonSerializerOptions ReaderOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            IncludeFields = true, // LAPS uses fields (n/t/p)
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            // Encoder is irrelevant for reads; keep default (safe).
        };

        // --- WRITE: strict & deterministic ---
        internal static readonly JsonSerializerOptions WriterOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,                  // preserve names
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            IncludeFields = false,                        // do not serialize fields by default
            // Keep safe defaults: no trailing commas, no comments.
            // If you explicitly want '+' instead of '\u002B', you can opt-in:
            // Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // Compatibility property for existing code
        internal static JsonSerializerOptions Options => ReaderOptions;

        static DsiJson()
        {
            var converter = new JsonStringEnumConverter();
            ReaderOptions.Converters.Add(converter);
            WriterOptions.Converters.Add(converter);
        }

        // ------------ PUBLIC API (READ) ------------

        // Generic string path (direct first; normalize on failure)
        public static T? DeserializeLenient<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;

            // Strip UTF BOM (U+FEFF) if present
            if (json.Length > 0 && json[0] == '\uFEFF')
                json = json.Substring(1);

            // Fast attempt
            try
            {
                return JsonSerializer.Deserialize<T>(json, ReaderOptions);
            }
            catch (JsonException)
            {
                // Only do the extra work if it actually looks single-quoted
                if (LooksLikeSingleQuotedJson(json))
                {
                    var normalized = NormalizeSingleQuotedJson(json);
                    return JsonSerializer.Deserialize<T>(normalized, ReaderOptions);
                }
                throw;
            }
        }

        // Generic bytes path (UTF-8 span fast path; UTF-16: decode to string)
        public static T? DeserializeLenient<T>(ReadOnlySpan<byte> binaryJson, bool utf16 = false)
        {
            if (binaryJson.Length == 0) return default;

            var trimmed = TrimZeroTerminator(binaryJson, utf16);

            if (!utf16)
            {
                // UTF-8: try span-based parse directly; fallback to string for single-quoted inputs
                trimmed = TrimUtf8Bom(trimmed);

#if NET8_0_OR_GREATER
                if (!LooksLikeSingleQuotedJsonUtf8(trimmed))
                {
                    return JsonSerializer.Deserialize<T>(trimmed, ReaderOptions);
                }
                string s = Encoding.UTF8.GetString(trimmed);
                return DeserializeLenient<T>(s);
#else
                // net48: go via string (fast enough; avoids API differences)
                byte[] buf = new byte[trimmed.Length];
                for (int i = 0; i < trimmed.Length; i++) buf[i] = trimmed[i];
                string s = Encoding.UTF8.GetString(buf);
                return DeserializeLenient<T>(s);
#endif
            }
            else
            {
                // UTF-16LE: benchmarks show string decode is faster & allocates less than transcoding
                byte[] buf = new byte[trimmed.Length];
                for (int i = 0; i < trimmed.Length; i++) buf[i] = trimmed[i];
                string s = Encoding.Unicode.GetString(buf);
                return DeserializeLenient<T>(s);
            }
        }

        // Typed convenience overloads that use source-generated contexts when available
        // (Keeps generic fallback for net48 and for non-generated types)
        public static DSInternals.Common.Data.LapsClearTextPassword? DeserializeLaps(ReadOnlySpan<byte> data, bool utf16 = false)
        {
#if NET8_0_OR_GREATER
            var trimmed = TrimZeroTerminator(data, utf16);
            if (!utf16)
            {
                trimmed = TrimUtf8Bom(trimmed);
                if (!LooksLikeSingleQuotedJsonUtf8(trimmed))
                {
                    return JsonSerializer.Deserialize(trimmed, DsiJsonContext.Default.LapsClearTextPassword);
                }
                string s = Encoding.UTF8.GetString(trimmed);
                if (s.Length > 0 && s[0] == '\uFEFF') s = s.Substring(1);
                try { return JsonSerializer.Deserialize(s, DsiJsonContext.Default.LapsClearTextPassword); }
                catch (JsonException)
                {
                    if (LooksLikeSingleQuotedJson(s))
                    {
                        var normalized = NormalizeSingleQuotedJson(s);
                        return JsonSerializer.Deserialize(normalized, DsiJsonContext.Default.LapsClearTextPassword);
                    }
                    throw;
                }
            }
            else
            {
                byte[] buf = new byte[trimmed.Length];
                for (int i = 0; i < trimmed.Length; i++) buf[i] = trimmed[i];
                string s = Encoding.Unicode.GetString(buf);
                if (s.Length > 0 && s[0] == '\uFEFF') s = s.Substring(1);
                try { return JsonSerializer.Deserialize(s, DsiJsonContext.Default.LapsClearTextPassword); }
                catch (JsonException)
                {
                    if (LooksLikeSingleQuotedJson(s))
                    {
                        var normalized = NormalizeSingleQuotedJson(s);
                        return JsonSerializer.Deserialize(normalized, DsiJsonContext.Default.LapsClearTextPassword);
                    }
                    throw;
                }
            }
#else
            return DeserializeLenient<DSInternals.Common.Data.LapsClearTextPassword>(data, utf16);
#endif
        }

        // ------------ PUBLIC API (WRITE) ------------

        public static string Serialize(object value)
            => JsonSerializer.Serialize(value, WriterOptions);

        public static string Serialize<T>(T value)
            => JsonSerializer.Serialize(value, WriterOptions);

        // ------------ Helpers (private) ------------

        private static ReadOnlySpan<byte> TrimZeroTerminator(ReadOnlySpan<byte> input, bool utf16)
        {
            if (input.Length == 0) return input;

            if (utf16)
            {
                int len = input.Length;
                while (len >= 2 && input[len - 1] == 0 && input[len - 2] == 0) len -= 2;
                if ((len & 1) == 1) len -= 1; // ensure even count
                return input.Slice(0, len);
            }
            else
            {
                int len = input.Length;
                while (len > 0 && input[len - 1] == 0) len--;
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

            // Manual TrimStart without allocating
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

            // Scan only a bounded prefix (prevents large-string slowdown)
            int end = s.Length - i;
            if (end > 2048) end = 2048;
            bool hasDouble = false, hasSingle = false;
            for (int j = 0; j < end; j++)
            {
                char c = s[i + j];
                if (c == '"') { hasDouble = true; break; }
                if (c == '\'') hasSingle = true;
            }
            return !hasDouble && hasSingle;
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

            // Bound the scan to avoid O(N) on huge inputs
            int end = s.Length - i;
            if (end > 2048) end = 2048;

            bool hasDouble = false, hasSingle = false;
            for (int j = 0; j < end; j++)
            {
                byte b = s[i + j];
                if (b == (byte)'"') { hasDouble = true; break; }
                if (b == (byte)'\'') hasSingle = true;
            }
            return !hasDouble && hasSingle;
        }

        // Converts '…' to "…" and preserves apostrophes (handles escaped backslashes)
        private static string NormalizeSingleQuotedJson(string input)
        {
            var sb = new StringBuilder(input.Length);
            bool inString = false, single = false;
            int backslashes = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];

                if (!inString)
                {
                    if (ch == '\'') { inString = true; single = true; sb.Append('"'); continue; }
                    if (ch == '"')  { inString = true; single = false; sb.Append('"'); continue; }
                    sb.Append(ch);
                    continue;
                }

                if (ch == '\\')
                {
                    backslashes++;
                    sb.Append('\\');
                    continue;
                }

                bool escaped = (backslashes & 1) == 1;
                backslashes = 0;

                if (single)
                {
                    if (ch == '\'' && !escaped)
                    {
                        inString = false; single = false; sb.Append('"');
                    }
                    else if (ch == '"')
                    {
                        sb.Append("\\\"");
                    }
                    else if (ch == '\'' && escaped)
                    {
                        if (sb.Length > 0 && sb[sb.Length - 1] == '\\') sb.Length--;
                        sb.Append('\'');
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }
                else
                {
                    if (ch == '"' && !escaped)
                    {
                        inString = false; sb.Append('"');
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }
            }
            return sb.ToString();
        }
    }
}