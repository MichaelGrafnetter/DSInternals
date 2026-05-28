namespace DSInternals.Replication.Model;

/// <summary>
/// Provides data for the event raised when the RPC session key is renegotiated.
/// </summary>
public class SessionKeyChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the newly negotiated session key.
    /// </summary>
    public byte[] SessionKey { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionKeyChangedEventArgs"/> class.
    /// </summary>
    /// <param name="sessionKey">The newly negotiated session key.</param>
    public SessionKeyChangedEventArgs(byte[] sessionKey)
    {
        this.SessionKey = sessionKey;
    }
}
