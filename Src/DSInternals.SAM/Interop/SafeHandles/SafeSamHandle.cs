using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;
using System;
using System.Security;

namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// Represents a wrapper class for a SAM object handle.
    /// </summary>
    [SecurityCritical]
    public class SafeSamHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeSamHandle() : base(true)
        {
        }
        public SafeSamHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            this.SetHandle(preexistingHandle);
        }
        [SecurityCritical]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.SamCloseHandle(this.handle) == NtStatus.Success;
        }
    }
}
