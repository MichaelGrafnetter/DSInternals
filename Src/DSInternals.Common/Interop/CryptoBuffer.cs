namespace DSInternals.Common.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct CryptoBuffer
    {
        public CryptoBuffer(IntPtr buffer, uint length)
        {
            this.Buffer = buffer;
            this.Length = length;
            this.MaximumLength = length;
        }
        /// <summary>
        /// this implementation.
        /// </summary>
        public CryptoBuffer(IntPtr buffer, int length) : this(buffer, (uint) length)
        {
        }

        /// <summary>
        /// The Length.
        /// </summary>
        public uint Length;
        /// <summary>
        /// The MaximumLength.
        /// </summary>
        public uint MaximumLength;
        /// <summary>
        /// The Buffer.
        /// </summary>
        public IntPtr Buffer;
    }
}
