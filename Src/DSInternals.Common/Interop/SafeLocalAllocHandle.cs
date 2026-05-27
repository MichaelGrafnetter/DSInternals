using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Common.Interop;

/// <summary>
/// Represents memory allocated by Win32 APIs that must be released with <c>LocalFree</c>.
/// </summary>
[SecurityCritical]
internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private SafeLocalAllocHandle()
        : base(true)
    {
    }

    public SafeLocalAllocHandle(IntPtr preexistingHandle, bool ownsHandle)
        : base(ownsHandle)
    {
        this.SetHandle(preexistingHandle);
    }

    public byte[]? ToArray(uint size)
    {
        if (this.IsInvalid || size == 0)
        {
            return null;
        }

        byte[] binaryData = new byte[checked((int)size)];
        Marshal.Copy(this.handle, binaryData, 0, binaryData.Length);

        return binaryData;
    }

    public string? StringValue => this.IsInvalid ? null : Marshal.PtrToStringUni(this.handle);

    [SecurityCritical]
    protected override bool ReleaseHandle() => NativeMethods.LocalFree(this.handle) == IntPtr.Zero;
}
