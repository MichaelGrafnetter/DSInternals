using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common
{    public static class SecureStringExtensions
    {
        /// <summary>
        /// Converts a SecureString to its Unicode string representation.
        /// </summary>
        /// <param name="input">The SecureString to convert.</param>
        /// <returns>The Unicode string representation of the SecureString contents.</returns>
        public static string ToUnicodeString(this SecureString input)
        {
            IntPtr ptr = Marshal.SecureStringToBSTR(input);
            try
            {
                return Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }
        /// <summary>
        /// Returns the byte array representation of the data.
        /// </summary>
        public static byte[] ToByteArray(this SecureString input)
        {
            int numBytes = input.Length * 2;
            byte[] byteArray = new byte[numBytes];
            IntPtr unmanagedByteArray = Marshal.SecureStringToGlobalAllocUnicode(input);
            try
            {
                Marshal.Copy(unmanagedByteArray, byteArray, 0, numBytes);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedByteArray);
            }
            return byteArray;
        }
        /// <summary>
        /// Appends a string to the end of a SecureString.
        /// </summary>
        /// <param name="input">The SecureString to append to.</param>
        /// <param name="suffix">The string to append to the SecureString.</param>
        public static void Append(this SecureString input, string suffix)
        {
            if(suffix != null)
            {
                char[] chars = suffix.ToCharArray();
                foreach (char c in chars)
                {
                    input.AppendChar(c);
                }
            }
        }
    }
}
