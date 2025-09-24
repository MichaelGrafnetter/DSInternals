using Microsoft.Win32.SafeHandles;
using Windows.Win32.System.Rpc;

namespace DSInternals.Replication;

/// <summary>
/// Represents a wrapper class for RPC binding handles.
/// </summary>
internal class SafeRpcBindingHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SafeRpcBindingHandle"/> class.
    /// </summary>
    private SafeRpcBindingHandle() : base(true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SafeRpcBindingHandle"/> class.
    /// </summary>
    /// <param name="preexistingHandle">The preexisting handle.</param>
    /// <param name="ownsHandle">Indicates whether the handle is owned by this instance.</param>
    public SafeRpcBindingHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
    {
        this.SetHandle(preexistingHandle);
    }

    /// <summary>
    /// Executes the code required to free the handle.
    /// </summary>
    unsafe protected override bool ReleaseHandle()
    {
        RPC_STATUS status = NativeMethods.RpcBindingFree(ref this.handle);
        return status == RPC_STATUS.RPC_S_OK;
    }
}
