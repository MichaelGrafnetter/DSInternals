using System;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The UnicodeString structure is used to define Unicode strings.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/windows/win32/api/ntdef/ns-ntdef-_unicode_string</see>
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
                throw new ArgumentOutOfRangeException(nameof(text));
            }
            else
            {
                this.Buffer = text;

                // Length of the unicode string.
                this.Length = (ushort)(text.Length * UnicodeCharLength);

                // Length of the unicode string, including the trailing null character.
                // Important: Some Windows components, including the Local Security Authority (LSA) do not behave properly if Length==MaximumLength.
                this.MaximumLength = (ushort)(this.Length + UnicodeCharLength);
            }
        }

        /// <summary>
        /// The length, in bytes, of the string stored in Buffer.
        /// </summary>
        /// <remarks>
        /// If the string is null-terminated, Length does not include the trailing null character.
        /// </remarks>
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
