using System.Buffers.Binary;
using System.Security;
using DSInternals.Common.Cryptography;

namespace DSInternals.Common.Data;
/// <summary>
/// Represents a group-managed service account's password information.
/// </summary>
/// <see>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/a9019740-3d73-46ef-a9ae-3ea8eb86ac2e</see>
public class ManagedPassword
{
    private const int StructureHeaderSize = 6 * sizeof(ushort) + sizeof(uint);
    private const short ExpectedVersion = 1;

    /// <summary>
    /// Gets the current password.
    /// </summary>
    public SecureString SecureCurrentPassword
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the current password.
    /// </summary>
    public string CurrentPassword => SecureCurrentPassword.ToUnicodeString();

    /// <summary>
    /// Gets the NT hash of the current password.
    /// </summary>
    public byte[] CurrentNTHash => NTHash.ComputeHash(SecureCurrentPassword);

    /// <summary>
    /// Gets the previous password.
    /// </summary>
    public SecureString? SecurePreviousPassword
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the previous password.
    /// </summary>
    public string? PreviousPassword => SecurePreviousPassword?.ToUnicodeString();

    /// <summary>
    /// Gets the NT hash of the previous password.
    /// </summary>
    public byte[]? PreviousNTHash => SecurePreviousPassword != null ? NTHash.ComputeHash(SecurePreviousPassword) : null;

    /// <summary>
    /// Gets the length of time after which the receiver should requery the password.
    /// </summary>
    public TimeSpan QueryPasswordInterval
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the length of time before which password queries will always return this password value.
    /// </summary>
    public TimeSpan UnchangedPasswordInterval
    {
        get;
        private set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedPassword"/> class.
    /// </summary>
    public ManagedPassword(SecureString currentPassword, SecureString previousPassword, TimeSpan queryPasswordInterval, TimeSpan unchangedPasswordInterval)
    {
        if (currentPassword == null)
        {
            throw new ArgumentNullException(nameof(currentPassword), "The current password is mandatory.");
        }

        this.SecureCurrentPassword = currentPassword;
        this.SecurePreviousPassword = previousPassword;
        this.QueryPasswordInterval = queryPasswordInterval;
        this.UnchangedPasswordInterval = unchangedPasswordInterval;
    }

    /// <summary>
    /// Parses the MSDS-MANAGEDPASSWORD_BLOB binary structure and returns a new instance of the <see cref="ManagedPassword"/> class.
    /// </summary>
    /// <param name="blob">
    /// The MSDS-MANAGEDPASSWORD_BLOB, which is a representation
    /// of a group-managed service account's password information.
    /// This structure is returned as the msDS-ManagedPassword constructed attribute.
    /// </param>
    public static ManagedPassword Parse(Memory<byte> blob)
    {
        if (blob.Length < StructureHeaderSize)
        {
            throw new ArgumentException($"The provided blob is too short. Minimum length is {StructureHeaderSize} bytes.", nameof(blob));
        }

        // Parse the binary data structure
        int currentOffset = 0;

        // A 16-bit unsigned integer that defines the version of the msDS-ManagedPassword binary large object (BLOB).
        ushort version = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);
        currentOffset += sizeof(ushort);

        // The Version field MUST be set to 0x0001.
        if (version != 1)
        {
            throw new ArgumentException($"Invalid version number in the blob. Expected 1, but got {version}.", nameof(blob));
        }

        // A 16-bit unsigned integer that MUST be set to 0x0000.
        ushort reserved = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);
        currentOffset += sizeof(ushort);

        // We will be benevolent and not test that reserved == 0, as the meaning of this field is not clear.

        // A 32-bit unsigned integer that specifies the length, in bytes, of the msDS-ManagedPassword BLOB.
        int length = BinaryPrimitives.ReadInt32LittleEndian(blob.Slice(currentOffset, sizeof(int)).Span);
        currentOffset += sizeof(int);

        if (blob.Length != length)
        {
            throw new ArgumentException($"Invalid blob length. Expected {length} bytes, but got {blob.Length} bytes.", nameof(blob));
        }

        // A 16-bit offset, in bytes, from the beginning of this structure to the CurrentPassword field.
        ushort currentPasswordOffset = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);
        currentOffset += sizeof(ushort);

        // A 16-bit offset, in bytes, from the beginning of this structure to the PreviousPassword field.
        ushort previousPasswordOffset = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);
        currentOffset += sizeof(ushort);

        // A 16-bit offset, in bytes, from the beginning of this structure to the QueryPasswordInterval field.
        ushort queryPasswordIntervalOffset = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);
        currentOffset += sizeof(ushort);

        // A 16-bit offset, in bytes, from the beginning of this structure to the UnchangedPasswordInterval field.
        ushort unchangedPasswordIntervalOffset = BinaryPrimitives.ReadUInt16LittleEndian(blob.Slice(currentOffset, sizeof(ushort)).Span);

        bool offsetsValid =
            currentPasswordOffset >= StructureHeaderSize &&
            (previousPasswordOffset == 0 && currentPasswordOffset < queryPasswordIntervalOffset || currentPasswordOffset < previousPasswordOffset && previousPasswordOffset < queryPasswordIntervalOffset) &&
            queryPasswordIntervalOffset + sizeof(long) <= unchangedPasswordIntervalOffset &&
            unchangedPasswordIntervalOffset + sizeof(long) <= blob.Length;

        if (!offsetsValid)
        {
            throw new ArgumentException("Offsets in the MSDS-MANAGEDPASSWORD_BLOB are invalid.", nameof(blob));
        }

        // Read CurrentPassword: A null-terminated WCHAR string containing the cleartext current password for the account.
        int currentPasswordUpperBound = previousPasswordOffset > 0 ? previousPasswordOffset : queryPasswordIntervalOffset;
        ReadOnlySpan<byte> currentPasswordSlice = blob.Slice(currentPasswordOffset, currentPasswordUpperBound - currentPasswordOffset).Span;
        SecureString currentPassword = ReadSecureWString(currentPasswordSlice);

        SecureString previousPassword = null;

        if (previousPasswordOffset > 0)
        {
            // Read PreviousPassword (optional): A null-terminated WCHAR string containing the cleartext previous password for the account. 
            ReadOnlySpan<byte> previousPasswordSlice = blob.Slice(previousPasswordOffset, queryPasswordIntervalOffset - previousPasswordOffset).Span;
            previousPassword = ReadSecureWString(previousPasswordSlice);
        }

        // A 64-bit unsigned integer containing the length of time, in units of 10^(-7) seconds, after which the receiver MUST re-query the password.
        long queryPasswordIntervalBinary = BinaryPrimitives.ReadInt64LittleEndian(blob.Slice(queryPasswordIntervalOffset, sizeof(long)).Span);
        TimeSpan queryPasswordInterval = TimeSpan.FromTicks(queryPasswordIntervalBinary);

        // A 64-bit unsigned integer containing the length of time, in units of 10^(-7) seconds, before which password queries will always return this password value.
        long unchangedPasswordIntervalBinary = BinaryPrimitives.ReadInt64LittleEndian(blob.Slice(unchangedPasswordIntervalOffset, sizeof(long)).Span);
        TimeSpan unchangedPasswordInterval = TimeSpan.FromTicks(unchangedPasswordIntervalBinary);

        return new ManagedPassword(
            currentPassword,
            previousPassword,
            queryPasswordInterval,
            unchangedPasswordInterval);
    }

    /// <summary>
    /// Reads a null-terminated Unicode string from the specified byte array.
    /// </summary>
    private unsafe static SecureString ReadSecureWString(ReadOnlySpan<byte> blob)
    {
        fixed (byte* blobPointer = blob)
        {
            // Temporarily cast the byte array to a char sequence to find the null terminator.
            ReadOnlySpan<char> stringSlice = new ReadOnlySpan<char>(blobPointer, blob.Length);

            // Find the null terminator in the string slice
            int terminatorIndex = stringSlice.IndexOf(char.MinValue);

            if (terminatorIndex < 0)
            {
                // The string is not null-terminated.
                throw new ArgumentException("The field must be a null-terminated WCHAR string.", nameof(blob));
            }

            // Read the string, excluding the null character
            return new SecureString((char*)blobPointer, terminatorIndex);
        }
    }
}
