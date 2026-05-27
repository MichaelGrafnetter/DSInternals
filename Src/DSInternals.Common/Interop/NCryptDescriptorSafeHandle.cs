using System.Security;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Common.Interop;

/// <summary>
/// Represents an <c>NCRYPT_DESCRIPTOR_HANDLE</c> returned by DPAPI-NG native functions.
/// </summary>
[SecurityCritical]
internal sealed class NCryptDescriptorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private NCryptDescriptorSafeHandle()
        : base(true)
    {
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
        return NativeMethods.NCryptCloseProtectionDescriptor(this.handle) == Win32ErrorCode.Success;
    }
}
