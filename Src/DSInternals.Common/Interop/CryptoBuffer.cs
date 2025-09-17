namespace DSInternals.Common.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a buffer structure used in cryptographic operations with pointer and length information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CryptoBuffer
    {
        /// <summary>
        /// Initializes a new instance of the CryptoBuffer structure with the specified buffer pointer and length.
        /// </summary>
        /// <param name="buffer">A pointer to the buffer data.</param>
        /// <param name="length">The length of the buffer in bytes.</param>
        public CryptoBuffer(IntPtr buffer, uint length)
        {
            this.Buffer = buffer;
            this.Length = length;
            this.MaximumLength = length;
        }
        /// <summary>
        /// Initializes a new instance of the CryptoBuffer structure with the specified buffer pointer and length.
        /// </summary>
        /// <param name="buffer">A pointer to the buffer data.</param>
        /// <param name="length">The length of the buffer in bytes.</param>
        public CryptoBuffer(IntPtr buffer, int length) : this(buffer, (uint) length)
        {
        }

        /// <summary>
        /// The current length of data in the buffer.
        /// </summary>
        public uint Length;
        /// <summary>
        /// The maximum capacity of the buffer.
        /// </summary>
        public uint MaximumLength;
        /// <summary>
        /// A pointer to the buffer data.
        /// </summary>
        public IntPtr Buffer;
    }
}
