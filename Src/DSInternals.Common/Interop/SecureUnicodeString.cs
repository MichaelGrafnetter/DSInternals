using System;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The UnicodeString structure is used to define Unicode strings.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/hardware/ff564879.aspx</see>
    [StructLayout(LayoutKind.Sequential)]
    public struct SecureUnicodeString
    {
        public SecureUnicodeString(SafeUnicodeSecureStringPointer passwordPtr)
        {
            this.Buffer = passwordPtr;
            if (passwordPtr == null)
            {
                this.Length = 0;
                this.MaximumLength = 0;
            }
            else if (passwordPtr.NumBytesTotal >= ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException("passwordPtr");
            }
            else
            {
                // Length of the unicode string (wchar_t).
                this.Length = (ushort)passwordPtr.NumBytes;
                this.MaximumLength = (ushort)passwordPtr.NumBytesTotal;
            }
        }

        /// <summary>
        /// The length, in bytes, of the string stored in Buffer.
        /// </summary>
        internal ushort Length;

        /// <summary>
        /// The length, in bytes, of Buffer.
        /// </summary>
        internal ushort MaximumLength;

        /// <summary>
        /// Pointer to a buffer used to contain a string of wide characters.
        /// </summary>
        public SafeUnicodeSecureStringPointer Buffer;
    }
}