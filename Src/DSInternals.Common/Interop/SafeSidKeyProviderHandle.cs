using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// Represents a wrapper class for SID Key Provider object handles.
    /// </summary>
    [SecurityCritical]
    internal class SafeSidKeyProviderHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeSidKeyProviderHandle() : base(true)
        {
        }

        public SafeSidKeyProviderHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }

        public byte[] ToArray(int size)
        {
            if(this.IsInvalid)
            {
                return null;
            }

            byte[] binaryData = new byte[size];
            Marshal.Copy(this.handle, binaryData, 0, size);

            return binaryData;
        }

        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            NativeMethods.SIDKeyProvFree(this.handle);

            // Presume that the memory release has been successful.
            return true;
        }
    }
}
