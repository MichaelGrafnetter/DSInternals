namespace DSInternals.Common
{
    using DSInternals.Common.Interop;
    using System.Security;
    using System.Security.AccessControl;

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
