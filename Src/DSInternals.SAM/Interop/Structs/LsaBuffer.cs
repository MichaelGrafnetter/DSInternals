namespace DSInternals.SAM.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Used by various Local Security Authority (LSA) functions to specify a Unicode string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LsaBuffer
    {
        /// <summary>
        /// Specifies the length, in bytes, of the string pointed to by the Buffer member, not including the terminating null character, if any.
        /// </summary>
        public ushort Length;

        /// <summary>
        /// Specifies the total size, in bytes, of the memory allocated for Buffer. Up to MaximumLength bytes can be written into the buffer without trampling memory.
        /// </summary>
        public ushort MaximumLength;

        /// <summary>
        /// Pointer to a buffer used to contain a string of wide characters.
        /// </summary>
        public IntPtr Buffer;

        public byte[] GetBytes()
        {
            byte[] binaryBuffer = new byte[this.Length];
            Marshal.Copy(this.Buffer, binaryBuffer, 0, (int)this.Length);
            return binaryBuffer;
        }
    }
}