
using System.IO;
using System.Security;
namespace DSInternals.Common
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string input, string suffix)
        {
            if(! string.IsNullOrEmpty(input) && ! string.IsNullOrEmpty(suffix) && input.EndsWith(suffix))
            {
                int trimmedLength = input.Length-suffix.Length;
                return input.Remove(trimmedLength);
            }
            else
            {
                return input;
            }
        }
        public static SecureString ToSecureString(this string input)
        {
            if (input == null)
            {
                return null;
            }
            SecureString output = new SecureString();
            foreach (char c in input.ToCharArray())
            {
                output.AppendChar(c);
            }
            output.MakeReadOnly();
            return output;
        }
    }
}
