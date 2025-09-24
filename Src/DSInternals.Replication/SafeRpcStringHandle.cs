using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.System.Rpc;

namespace DSInternals.Replication;

/// <summary>
/// Represents a wrapper class for RPC string handles.
/// </summary>
internal class SafeRpcStringHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SafeRpcStringHandle"/> class.
    /// </summary>
    private SafeRpcStringHandle() : base(true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeRpcStringHandle"/> class.
    /// </summary>
    /// <param name="preexistingHandle">The preexisting handle.</param>
    /// <param name="ownsHandle">Indicates whether the handle is owned by this instance.</param>
    public SafeRpcStringHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
    {
        this.SetHandle(preexistingHandle);
    }

    /// <summary>
    /// Gets the string value represented by this handle.
    /// </summary>
    public string Value => Marshal.PtrToStringUni(this.handle);

    /// <summary>
    /// Executes the code required to free the handle.
    /// </summary>
    protected override bool ReleaseHandle()
    {
        RPC_STATUS status = NativeMethods.RpcStringFree(ref this.handle);
        return status == RPC_STATUS.RPC_S_OK;
    }
}
