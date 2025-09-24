using System.Net;
using Windows.Win32.System.Rpc;

namespace DSInternals.Replication;

/// <summary>
/// Provides a connection-based wrapper around the RPC binding.
/// </summary>
public sealed class RpcBinding : IDisposable
{
    private SafeRpcBindingHandle _bindingHandle;

    /// <summary>
    /// String representation of a binding handle.
    /// </summary>
    public readonly string StringBinding;

    /// <summary>
    /// Creates a new RPC binding to the specified network address.
    /// </summary>
    /// <param name="networkAddress">The network address of the RPC server.</param>
    /// <param name="protseq">The RPC protocol sequence to use.</param>
    /// <param name="endpoint">The RPC endpoint to use.</param>
    /// <param name="options">Additional options for the RPC binding.</param>
    public RpcBinding(string networkAddress,
        RpcProtseq protseq = RpcProtseq.ncacn_ip_tcp,
        string endpoint = null,
        string options = null
        )
    {
        // Combine rpc binding info into a single string
        RPC_STATUS composeResult = NativeMethods.RpcStringBindingCompose(
            objUuid: null,
            protseq,
            networkAddress,
            endpoint,
            options,
            out string binding);
        RpcException.ThrowIfError(composeResult);

        // Cache the composed string binding
        this.StringBinding = binding;

        // Create and cache a handle representing this binding
        RPC_STATUS bindingResult = NativeMethods.RpcBindingFromStringBinding(binding, out this._bindingHandle);
        RpcException.ThrowIfError(bindingResult);
    }

    /// <summary>
    /// Adds authentication information to the RPC binding.
    /// </summary>
    public void AuthenticateAs(
        string serverPrincipalName,
        NetworkCredential credential = null,
        RpcAuthenticationLevel authenticationLevel = RpcAuthenticationLevel.PacketPrivacy,
        RpcAuthenticationType authenticationType = RpcAuthenticationType.Negotiate)
    {
        RPC_STATUS result = NativeMethods.RpcBindingSetAuthInfo(this._bindingHandle, serverPrincipalName, authenticationLevel, authenticationType, credential);
        RpcException.ThrowIfError(result);
    }

    /// <summary>
    /// Gets the underlying RPC binding handle.
    /// </summary>
    /// <returns>The underlying RPC binding handle.</returns>
    public IntPtr DangerousGetHandle() => this._bindingHandle?.DangerousGetHandle() ?? IntPtr.Zero;

    /// <summary>
    /// Releases memory used by the server binding handle.
    /// </summary>
    public void Dispose()
    {
        _bindingHandle?.Dispose();
        _bindingHandle = null;
        GC.SuppressFinalize(this);
    }
}
