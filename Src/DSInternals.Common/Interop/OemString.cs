using System;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OemString
    {
        public OemString(SafeOemStringPointer oemStringPtr)
        {
            this.Buffer = oemStringPtr;
            if (oemStringPtr == null)
            {
                this.Length = 0;
                this.MaximumLength = 0;
            }
            else if (oemStringPtr.Length >= ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException("oemStringPtr");
            }
            else
            {
                // Length of the oem string.
                this.Length = (ushort)oemStringPtr.Length;
                this.MaximumLength = (ushort)oemStringPtr.Length;
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
        /// Pointer to a buffer used to contain a string of OEM characters.
        /// </summary>
        public SafeOemStringPointer Buffer;
    }
}