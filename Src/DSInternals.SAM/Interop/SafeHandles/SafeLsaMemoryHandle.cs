using System.Runtime.InteropServices;
using System.Security;
using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.SAM.Interop;

[SecurityCritical]
internal sealed class SafeLsaMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SafeLsaMemoryHandle() : base(true)
    {
    }

    public SafeLsaMemoryHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
    {
        this.SetHandle(preexistingHandle);
    }

    public T MarshalAs<T>() where T : struct
    {
        if (this.IsInvalid)
        {
            throw new InvalidOperationException("The handle is invalid.");
        }

        return Marshal.PtrToStructure<T>(this.handle);
    }

    [SecurityCritical]
    override protected bool ReleaseHandle()
    {
        return NativeMethods.LsaFreeMemory(this.handle) == NtStatus.Success;
    }
}
