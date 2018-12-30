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
        public CryptoBuffer(IntPtr buffer, int length) : this(buffer, (uint) length)
        {
        }

        public uint Length;
        public uint MaximumLength;
        public IntPtr Buffer;
    }
}
