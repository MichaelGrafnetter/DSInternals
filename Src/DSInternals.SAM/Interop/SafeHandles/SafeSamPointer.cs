using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using DSInternals.Common.Interop;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Represents a wrapper class for buffers allocated by SAM RPC.
/// </summary>
[SecurityCritical]
internal sealed class SafeSamPointer : SafeHandleZeroOrMinusOneIsInvalid
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
    public T? MarshalAs<T>() where T : struct
    {
        if (this.IsInvalid)
        {
            // Nothing to marshal
            return null;
        }

        return Marshal.PtrToStructure<T>(this.handle);
    }

    [SecurityCritical]
    public SecurityIdentifier? MarshalAsSecurityIdentifier()
    {
        if (this.IsInvalid)
        {
            // Nothing to marshal
            return null;
        }

        return new SecurityIdentifier(this.handle);
    }

    [SecurityCritical]
    public T[]? MarshalAs<T>(uint count) where T : struct
    {
        if (this.IsInvalid || count == 0)
        {
            // Nothing to parse
            return null;
        }

        if (count > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Too many entries to marshal.");
        }

        var result = new T[count];

        for (int i = 0; i < count; i++)
        {
            var currentOffset = i * Marshal.SizeOf<T>();
            result[i] = Marshal.PtrToStructure<T>(this.handle + currentOffset);
        }

        return result;
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
        return NativeMethods.SamFreeMemory(this.handle) == NtStatus.Success;
    }
}
