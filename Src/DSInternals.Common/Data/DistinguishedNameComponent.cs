using System.Globalization;
using System.Text;

namespace DSInternals.Common.Data;

public class DistinguishedNameComponent
{
    public string Name
    {
        get;
        private set;
    }

    public string Value
    {
        get;
        private set;
    }

    public DistinguishedNameComponent(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        this.Name = name;
        this.Value = value;
    }

    public override string ToString()
    {
        return String.Format(CultureInfo.InvariantCulture, "{0}={1}", EscapeValue(this.Name), EscapeValue(this.Value));
    }

    private static string EscapeValue(string input)
    {
        var result = new StringBuilder(input.Length);

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];
            if (IsSpecialChar(currentChar))
            {
                // Escape special chars
                result.Append('\\');
                result.Append(currentChar);
            }
            else if (currentChar == ' ' && (i == 0 || i == input.Length - 1))
            {
                // Escape the leading or ending space
                result.Append("\\ ");
            }
            else if (currentChar < 32)
            {
                // Escape control chars
                result.AppendFormat(CultureInfo.InvariantCulture, "\\{0:X2}", (int)currentChar);
            }
            else if (currentChar >= 128)
            {
                // Escape multibyte chars
                byte[] bytes = Encoding.UTF8.GetBytes(currentChar.ToString());

                foreach (byte currentByte in bytes)
                {
                    result.AppendFormat(CultureInfo.InvariantCulture, "\\{0:X2}", currentByte);
                }
            }
            else
            {
                // Append the char without escaping
                result.Append(currentChar);
            }
        }

        return result.ToString();
    }

    private static bool IsSpecialChar(char c)
    {
        // RFC 2253: special = "," / "=" / "+" / "<" /  ">" / "#" / ";"
        switch (c)
        {
            case ',':
            case '=':
            case '+':
            case '<':
            case '>':
            case '#':
            case ';':
            case '\\': // Escape char
            case '"':  // Quote char
                return true;
            default:
                return false;
        }
    }
}
