using System.Buffers.Binary;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Communicates information about authentication between trusted domains. (LSAPR_AUTH_INFORMATION)
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/dfe16abb-4dfb-402d-bc54-84fcc9932fad</remarks>
public struct TrustAuthenticationInformation
{
    private const int StructHeaderSize = sizeof(ulong) + sizeof(TrustAuthenticationInformationType) + sizeof(uint);

    /// <summary>
    /// Gets the last time that the authentication information was set.
    /// </summary>
    public DateTime LastUpdateTime { get; private set; }

    /// <summary>
    /// Gets the type of AuthInfo that is being stored.
    /// </summary>
    public TrustAuthenticationInformationType AuthType { get; private set; }

    /// <summary>
    /// Gets the authentication data.
    /// </summary>
    public ReadOnlyMemory<byte> AuthInfo { get; private set; }

    public static (TrustAuthenticationInformation authInfo, int bytesRead) Parse(ReadOnlyMemory<byte> blob)
    {
        if (blob.Length < StructHeaderSize)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), $"Blob is too small to contain a TrustAuthenticationInformation structure. Minimum size is {StructHeaderSize} bytes.");
        }

        // Parse the binary structure
        TrustAuthenticationInformation result = default;
        int currentPosition = 0;

        // LastUpdateTime (8 bytes)
        long lastUpdateTimeValue = BinaryPrimitives.ReadInt64LittleEndian(blob.Slice(currentPosition, sizeof(long)).Span);
        result.LastUpdateTime = DateTime.FromFileTimeUtc(lastUpdateTimeValue);
        currentPosition += sizeof(ulong);

        // AuthType (4 bytes)
        uint authTypeValue = BinaryPrimitives.ReadUInt32LittleEndian(blob.Slice(currentPosition, sizeof(uint)).Span);
        result.AuthType = (TrustAuthenticationInformationType)authTypeValue;
        currentPosition += sizeof(uint);

        // AuthInfoLength (4 bytes)
        int authInfoLength = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(currentPosition, sizeof(int)).Span);
        currentPosition += sizeof(uint);

        if (authInfoLength <= 0 || authInfoLength + currentPosition > blob.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), $"Blob is too small to contain the AuthenticationInformation. Expected length is {authInfoLength} bytes, but only {blob.Length - currentPosition} bytes are available.");
        }

        // AuthInfo (variable): A BYTE field containing authentication data.
        result.AuthInfo = blob.Slice(currentPosition, authInfoLength);

        // Padding (variable) - structure length must be a multiple of sizeof(int)
        int roundedAuthInfoLength = (authInfoLength + sizeof(int) - 1) & ~(sizeof(int) - 1);
        currentPosition += roundedAuthInfoLength;

        return (result, currentPosition);
    }
}
