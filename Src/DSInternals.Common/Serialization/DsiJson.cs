using System;
using System.Text;
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
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        /// <summary>
        /// Try deserialize; if it looks like single-quoted JSON (Newtonsoft-style), normalize and retry.
        /// </summary>
        internal static T? DeserializeLenient<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

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

        private static bool LooksLikeSingleQuotedJson(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            var t = s.AsSpan().TrimStart();
            return (t.Length > 0 && (t[0] == '{' || t[0] == '['))
                   && s.IndexOf('"') < 0
                   && s.IndexOf('\'') >= 0;
        }

        // Hardened: converts '…' to "…" and collapses \' -> ' inside single-quoted strings
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
                    i++;
                    continue;
                }

                if (ch == '\'' || ch == '"')
                {
                    if (!inString)
                    {
                        inString = true;
                        quote = ch;
                        sb.Append('"');
                        continue;
                    }
                    else if (ch == quote)
                    {
                        inString = false;
                        sb.Append('"');
                        continue;
                    }
                }

                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}
