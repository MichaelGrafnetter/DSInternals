namespace DSInternals.Common.Serialization
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal static class DsiJson
    {
        internal static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        internal static T DeserializeLenient<T>(string json)
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
                    string normalized = NormalizeSingleQuotedJson(json);
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

            ReadOnlySpan<char> t = s.AsSpan().TrimStart();
            return (t.Length > 0 && (t[0] == '{' || t[0] == '[')) &&
                   s.IndexOf('"') < 0 &&
                   s.IndexOf('\'') >= 0;
        }

        private static string NormalizeSingleQuotedJson(string input)
        {
            var sb = new StringBuilder(input.Length);
            bool inString = false;
            char quote = '\0';
            bool escaped = false;

            foreach (char ch in input)
            {
                if (escaped)
                {
                    sb.Append(ch);
                    escaped = false;
                    continue;
                }

                if (ch == '\\')
                {
                    sb.Append(ch);
                    escaped = true;
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
