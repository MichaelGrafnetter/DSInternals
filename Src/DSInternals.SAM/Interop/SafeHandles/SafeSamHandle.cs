using System.Security;
using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Represents a wrapper class for a SAM object handle.
/// </summary>
[SecurityCritical]
internal sealed class SafeSamHandle : SafeHandleZeroOrMinusOneIsInvalid
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
