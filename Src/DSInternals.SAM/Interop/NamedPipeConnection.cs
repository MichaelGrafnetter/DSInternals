using System.Net;
using DSInternals.Common;
using DSInternals.Common.Interop;
using Windows.Win32.NetworkManagement.WNet;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Represents an authenticated SMB connection to a remote server's IPC$ share,
/// to be used by SAM RPC over named pipes (ncacn_np).
/// </summary>
internal sealed class NamedPipeConnection : IDisposable
{
    private readonly string _shareName;

    internal NamedPipeConnection(string server, NetworkCredential? credential)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(server);

        _shareName = $"\\\\{server}\\IPC$";

        // Disconnect from the IPC share first in case of a preexisting connection. Ignore any errors.
        Disconnect();

        // Connect using provided credentials
        Win32ErrorCode result = NativeMethods.WNetAddConnection2(
            _shareName,
            credential,
            NET_CONNECT_FLAGS.CONNECT_TEMPORARY
        );

        Validator.AssertSuccess(result);
    }

    private void Disconnect()
    {
        // Ignore errors during disconnect
        NativeMethods.WNetCancelConnection2(_shareName, NetCancelOptions.NoUpdate, force: true);
    }

    public void Dispose()
    {
        Disconnect();
        GC.SuppressFinalize(this);
    }

    ~NamedPipeConnection()
    {
        Disconnect();
    }
}
