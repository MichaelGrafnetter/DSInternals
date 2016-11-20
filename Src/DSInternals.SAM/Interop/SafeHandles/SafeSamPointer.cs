using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;
using System;
using System.Security;

namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// Represents a wrapper class for buffers allocated by SAM RPC.
    /// </summary>
    [SecurityCritical]
    public class SafeSamPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeSamPointer() : base(true)
        {
        }
        public SafeSamPointer(IntPtr preexistingPointer, bool ownsPointer)
            : base(ownsPointer)
        {
            this.SetHandle(preexistingPointer);
        }
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.SamFreeMemory(this.handle) == NtStatus.Success;
        }
    }
}
