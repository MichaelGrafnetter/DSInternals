using System;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The UnicodeString structure is used to define Unicode strings.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/hardware/ff564879.aspx</see>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct UnicodeString
    {
        private const ushort UnicodeCharLength = 2;

        /// <summary>
        /// Maximum number of unicode characters that can fit into the buffer, excluding the trailing 0.
        /// </summary>
        public const ushort MaxLength = ushort.MaxValue / UnicodeCharLength - 1;

        public UnicodeString(string text)
        {
            if (text == null)
            {
                this.Length = this.MaximumLength = 0;
                this.Buffer = null;
            }
            else if (text.Length > MaxLength)
            {
                throw new ArgumentOutOfRangeException("text");
            }
            else
            {
                this.Buffer = text;
                // Length of the unicode string.
                this.Length = this.MaximumLength = (ushort)(text.Length * UnicodeCharLength);
            }
        }

        /// <summary>
        /// The length, in bytes, of the string stored in Buffer.
        /// </summary>
        public ushort Length;

        /// <summary>
        /// The length, in bytes, of Buffer.
        /// </summary>
        public ushort MaximumLength;

        /// <summary>
        /// Pointer to a buffer used to contain a string of wide characters.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Buffer;
    }
}