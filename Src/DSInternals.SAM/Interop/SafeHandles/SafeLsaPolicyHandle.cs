using System.Security;
using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.SAM.Interop;

[SecurityCritical]
internal sealed class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SafeLsaPolicyHandle() : base(true)
    {
    }

    public SafeLsaPolicyHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
    {
        this.SetHandle(preexistingHandle);
    }

    [SecurityCritical]
    override protected bool ReleaseHandle()
    {
        return NativeMethods.LsaClose(this.handle) == NtStatus.Success;
    }
}
