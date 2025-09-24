using Windows.Win32.System.Rpc;

namespace DSInternals.Replication;

/// <summary>
/// This exception is thrown when an RPC error occurs.
/// </summary>
public class RpcException : System.ComponentModel.Win32Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RpcException"/> class.
    /// </summary>
    internal RpcException(RPC_STATUS errorsCode)
        : base(unchecked((int)errorsCode))
    {
    }

    /// <summary>
    /// Throws an <see cref="RpcException"/> if the provided error code is not <see cref="RPC_STATUS.RPC_S_OK"/>.
    /// </summary>
    /// <param name="code">The error code to check.</param>
    /// <exception cref="RpcException">Thrown when the error code is not <see cref="RPC_STATUS.RPC_S_OK"/>.</exception>
    internal static void ThrowIfError(RPC_STATUS code)
    {
        if (code != RPC_STATUS.RPC_S_OK)
        {
            throw new RpcException(code);
        }
    }
}
