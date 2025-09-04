namespace DSInternals.Common
{
    using DSInternals.Common.Interop;
    using System.Security;
    using System.Security.AccessControl;

    public static class StringExtensions
    {
        /// <summary>
        /// Removes the specified suffix from the end of a string if it exists.
        /// </summary>
        /// <param name="input">The string to trim.</param>
        /// <param name="suffix">The suffix to remove from the end of the string.</param>
        /// <returns>The string with the suffix removed, or the original string if the suffix is not found.</returns>
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

        /// <summary>
        /// Converts a string to a SecureString for secure handling of sensitive data.
        /// </summary>
        /// <param name="input">The string to convert to a SecureString.</param>
        /// <returns>A SecureString containing the input string, or null if the input is null.</returns>
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

        /// <summary>
        /// Converts a Security Descriptor Definition Language (SDDL) string to its binary representation.
        /// </summary>
        /// <param name="securityDescriptor">The SDDL string to convert.</param>
        /// <returns>A byte array containing the binary security descriptor.</returns>
        public static byte[] SddlToBinary(this string securityDescriptor)
        {
            Validator.AssertNotNullOrWhiteSpace(securityDescriptor, "securityDescriptor");
            byte[] binarySecurityDescriptor;
            Win32ErrorCode result = NativeMethods.ConvertStringSecurityDescriptorToSecurityDescriptor(securityDescriptor, GenericSecurityDescriptor.Revision, out binarySecurityDescriptor);
            Validator.AssertSuccess(result);
            return binarySecurityDescriptor;
        }
    }
}
