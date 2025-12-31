using System.Buffers.Binary;
using System.Text;
using DSInternals.Common.Cryptography;
using DSInternals.Common.Data;

namespace DSInternals.Common.Kerberos;

/// <summary>
/// Represents the trustAuthInfo data structure used in the trustAuthIncoming and trustAuthOutgoing attributes of the trustedDomain object.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/c964fca9-c50e-426a-9173-5bf3cb720e2e</remarks>
public struct TrustAuthInfos
{
    private const int StructHeaderSize = 2 * sizeof(uint);

    public ReadOnlyMemory<byte> CurrentPasswordBytes { get; private set; }

    public byte[] CurrentNTHash { get; private set; }

    public ReadOnlyMemory<byte> PreviousPasswordBytes { get; private set; }

    public byte[] PreviousNTHash { get; private set; }

    public string CurrentPassword
    {
        get
        {
            return GetPassword(this.CurrentPasswordBytes);
        }
    }

    public string PreviousPassword
    {
        get
        {
            return GetPassword(this.PreviousPasswordBytes);
        }
    }

    private TrustAuthInfos(ReadOnlyMemory<byte> currentPasswordBytes, ReadOnlyMemory<byte> previousPasswordBytes, byte[] currentNTHash, byte[] previousNTHash)
    {
        this.CurrentPasswordBytes = currentPasswordBytes;
        this.PreviousPasswordBytes = previousPasswordBytes;
        this.CurrentNTHash = currentNTHash;
        this.PreviousNTHash = previousNTHash;
    }

    public static TrustAuthInfos Parse(ReadOnlyMemory<byte> blob)
    {
        if (blob.Length < StructHeaderSize)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), $"Blob is too small to contain a TrustAuthInfo structure. Minimum size is {StructHeaderSize} bytes.");
        }

        // Parse the binary structure
        int currentPosition = 0;

        // Count of auth infos (4 bytes)
        int count = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(currentPosition, sizeof(int)).Span);
        currentPosition += sizeof(int);

        // Byte offset to AuthenticationInformation (4 bytes)
        int currentOffset = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(currentPosition, sizeof(int)).Span);
        currentPosition += sizeof(int);

        // Byte offset to PreviousAuthenticationInformation (4 bytes)
        int previousOffset = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(currentPosition, sizeof(int)).Span);
        currentPosition += sizeof(int);

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), "The count of auth infos cannot be negative.");
        }

        if (currentOffset < currentPosition || currentOffset > blob.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), $"The byte offset to AuthenticationInformation ({currentOffset}) does not fall into the expected range.");
        }

        if (previousOffset < currentOffset || previousOffset > blob.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(blob), $"The byte offset to PreviousAuthenticationInformation ({previousOffset}) does not fall into the expected range.");
        }

        // AuthenticationInformation(variable)
        currentPosition = currentOffset;
        ReadOnlyMemory<byte> currentPasswordBytes = default;
        byte[] currentNTHash = null;

        for (int i = 0; i < count; i++)
        {
            var authInfoBlob = blob.Slice(currentPosition);
            (TrustAuthenticationInformation authInfo, int bytesRead) = TrustAuthenticationInformation.Parse(authInfoBlob);

            switch (authInfo.AuthType)
            {
                case TrustAuthenticationInformationType.NTHash:
                    currentNTHash = authInfo.AuthInfo.ToArray();
                    break;
                case TrustAuthenticationInformationType.CleartextPassword:
                    currentPasswordBytes = authInfo.AuthInfo;
                    break;
                case TrustAuthenticationInformationType.Version:
                default:
                    // Ignore other types of authentication information.
                    break;
            }

            // Move to the next auth info
            currentPosition += bytesRead;
        }

        // PreviousAuthenticationInformation (variable)
        currentPosition = previousOffset;
        ReadOnlyMemory<byte> previousPasswordBytes = default;
        byte[] previousNTHash = null;

        for (int i = 0; i < count; i++)
        {
            var previousAuthInfoBlob = blob.Slice(currentPosition);
            (TrustAuthenticationInformation previousAuthInfo, int bytesRead) = TrustAuthenticationInformation.Parse(previousAuthInfoBlob);

            switch (previousAuthInfo.AuthType)
            {
                case TrustAuthenticationInformationType.NTHash:
                    previousNTHash = previousAuthInfo.AuthInfo.ToArray();
                    break;
                case TrustAuthenticationInformationType.CleartextPassword:
                    previousPasswordBytes = previousAuthInfo.AuthInfo;
                    break;
                case TrustAuthenticationInformationType.Version:
                default:
                    // Ignore other types of authentication information.
                    break;
            }

            // Move to the next auth info
            currentPosition += bytesRead;
        }

        if (currentNTHash == null && currentPasswordBytes.Length > 0)
        {
            // If the current password is provided, compute its NTHash.
            currentNTHash = NTHash.ComputeHash(currentPasswordBytes);
        }

        if (previousNTHash == null && previousPasswordBytes.Length > 0)
        {
            // If the previous password is provided, compute its NTHash.
            previousNTHash = NTHash.ComputeHash(previousPasswordBytes);
        }

        return new TrustAuthInfos(currentPasswordBytes, previousPasswordBytes, currentNTHash, previousNTHash);
    }

    public KerberosCredentialNew? DeriveKerberosKeys(string salt)
    {
        return this.CurrentPasswordBytes.IsEmpty ? null : KerberosCredentialNew.Derive(this.CurrentPasswordBytes, this.PreviousPasswordBytes, salt);
    }

    unsafe private static string? GetPassword(ReadOnlyMemory<byte> passwordBytes)
    {
        if (passwordBytes.IsEmpty)
        {
            return null;
        }

        using (var pinned = passwordBytes.Pin())
        {
            return Encoding.Unicode.GetString((byte*)pinned.Pointer, passwordBytes.Length);
        }
    }
}
