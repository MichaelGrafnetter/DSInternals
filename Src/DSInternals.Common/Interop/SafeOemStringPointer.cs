using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common.Interop
{
    //TODO: SafeOemStringPointer documentation
    /// <summary>
    /// Represents a wrapper class for...
    /// </summary>
    [SecurityCritical]
    public class SafeOemStringPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        public int Length
        {
            get;
            private set;
        }

        public static SafeOemStringPointer Allocate(int length)
        {
            IntPtr nativeMemory = Marshal.AllocHGlobal(length);
            return new SafeOemStringPointer(nativeMemory, true, length);
        }

        private SafeOemStringPointer()
            : base(true)
        {
        }

        public SafeOemStringPointer(IntPtr preexistingHandle, bool ownsHandle, int length)
            : this(preexistingHandle, ownsHandle)
        {
            this.Length = length;
        }

        public SafeOemStringPointer(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            this.ZeroFill();
            Marshal.FreeHGlobal(this.handle);
            return true;
        }

        [SecurityCritical]
        protected void ZeroFill()
        {
            // We know the size of the buffer so we can fill it with zeros for security reasons.
            for (int i = 0; i < this.Length; i++)
            {
                Marshal.WriteByte(this.handle, i, byte.MinValue);
            }
        }

        public override string ToString()
        {
            return Marshal.PtrToStringAnsi(this.handle);
        }
    }
}