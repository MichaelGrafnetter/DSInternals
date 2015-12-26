using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common
{    public static class SecureStringExtensions
    {
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
