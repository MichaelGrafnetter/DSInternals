namespace DSInternals.Common.Data
{
    using DSInternals.Common;
    using System;
    using System.Text;

    public class DistinguishedNameComponent
    {
        public string Name;
        public string Value;

        public DistinguishedNameComponent(string name, string value)
        {
            Validator.AssertNotNullOrWhiteSpace(name, "name");
            Validator.AssertNotNullOrWhiteSpace(value, "value");
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0}={1}", EscapeValue(this.Name), EscapeValue(this.Value));
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
                    result.AppendFormat("\\{0:X2}", (int)currentChar);
                }
                else if (currentChar >= 128)
                {
                    // Escape multibyte chars
                    byte[] bytes = Encoding.UTF8.GetBytes(currentChar.ToString());

                    foreach (byte currentByte in bytes)
                    {
                        result.AppendFormat("\\{0:X2}", currentByte);
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
            switch(c)
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
}