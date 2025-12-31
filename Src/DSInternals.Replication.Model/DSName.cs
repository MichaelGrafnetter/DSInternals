using System.Buffers.Binary;
using System.Security.Principal;
using System.Text;

namespace DSInternals.Replication;

/// <summary>
/// Identifies a directory object using the values of one or more of its LDAP attributes: objectGUID, objectSid, or distinguishedName.
/// </summary>
/// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/385d478f-3eb6-4d2c-ac58-f25c4debdd86</remarks>
public class DSName
{
    private const int GuidLength = 16;
    private const int SidMaxLength = 28;
    private const int DSNameHeaderSize = 2 * sizeof(uint) + GuidLength + SidMaxLength + sizeof(uint);

    /// <summary>
    /// The value of the object's objectSid attribute, its security identifier.
    /// </summary>
    public SecurityIdentifier? ObjectSid { get; private set; }

    /// <summary>
    /// The value of the object's objectGUID attribute
    /// </summary>
    public Guid? ObjectGuid { get; private set; }

    /// <summary>
    /// The object's distinguishedName attribute.
    /// </summary>
    public string? DistinguishedName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DSName"/> class.
    /// </summary>
    /// <param name="objectSid">The object's security identifier (SID).</param>
    /// <param name="objectGuid">The object's globally unique identifier (GUID).</param>
    /// <param name="distinguishedName">The object's distinguished name (DN).</param>
    public DSName(SecurityIdentifier? objectSid = null, Guid? objectGuid = null, string? distinguishedName = null)
    {
        this.ObjectSid = objectSid;
        this.ObjectGuid = objectGuid;
        this.DistinguishedName = distinguishedName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DSName"/> class.
    /// </summary>
    private DSName()
    {
        // This constructor is only used by the static parse method.
    }

    /// <summary>
    /// Parses a binary representation of a DSName structure.
    /// </summary>
    /// <param name="buffer">The binary data to parse.</param>
    /// <returns>A parsed <see cref="DSName"/> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the buffer is too small.</exception>
    /// <exception cref="ArgumentException">Thrown when the buffer contains invalid data.</exception>
    public static DSName Parse(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < DSNameHeaderSize)
        {
            throw new ArgumentOutOfRangeException($"Buffer is too small to contain a DSName structure. Minimum size is {DSNameHeaderSize} bytes.", nameof(buffer));
        }

        // Parse the binary structure
        DSName result = new DSName();
        int currentPosition = 0;

        // The length, in bytes, of the entire data structure.
        // Lengths in this structure are always in little-endian format.
        // Although the lengths are defined as unsigned integers, we treat them as signed, because .NET API expects lengths to be signed integers.
        int structLen = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(currentPosition, sizeof(int)));
        currentPosition += sizeof(int);

        if (structLen != buffer.Length)
        {
            throw new ArgumentException($"Invalid DSNAME structure length. Expected {structLen} bytes, but got {buffer.Length} bytes.", nameof(buffer));
        }

        // The number of bytes in the Sid field used to represent the object's objectSid attribute value.
        int sidLen = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(currentPosition, sizeof(int)));
        currentPosition += sizeof(int);

        if (sidLen > SidMaxLength || sidLen < 0)
        {
            throw new ArgumentException($"Invalid SID length. Expected at most {SidMaxLength} bytes, but got {sidLen} bytes.", nameof(buffer));
        }

        // The value of the object's objectGUID attribute.
#if NET
        // This more efficient constructor is available in Core 2.1 and later.
        var binaryGuid = buffer.Slice(currentPosition, GuidLength);
#else
        var binaryGuid = buffer.Slice(currentPosition, GuidLength).ToArray();
#endif
        Guid parsedGuid = new Guid(binaryGuid);
        currentPosition += GuidLength;

        if (parsedGuid != Guid.Empty)
        {
            // If the values for all fields in the GUID structure are zero,
            // this indicates that the DSNAME does not identify the objectGUID value of the directory object.
            result.ObjectGuid = parsedGuid;
        }

        // The value of the object's objectSid attribute.
        if (sidLen > 0)
        {
            // Span.ToArray() does not seem efficient, but SecurityIdentifier(IntPtr) internally does the same and has a risk of buffer overflow.
            byte[] binarySid = buffer.Slice(currentPosition, sidLen).ToArray();
            result.ObjectSid = new SecurityIdentifier(binarySid, offset: 0);
        }

        // The SID has a constant length, even if not all bytes are used.
        currentPosition += SidMaxLength;

        // The number of characters in the StringName field, not including the terminating null character.
        int nameLen = BinaryPrimitives.ReadInt32LittleEndian(buffer.Slice(currentPosition, sizeof(int)));
        currentPosition += sizeof(int);
        int expectedNameLen = (structLen - DSNameHeaderSize) / sizeof(char) - 1;

        if (nameLen != expectedNameLen)
        {
            throw new ArgumentException($"Invalid Name length. Expected {expectedNameLen} characters, but got {nameLen} characters.", nameof(buffer));
        }

        if (nameLen > 0)
        {
            // A null-terminated Unicode value of the object's distinguishedName attribute.
            result.DistinguishedName = ParseUnicodeString(buffer.Slice(currentPosition, nameLen * sizeof(char)));
        }

        return result;
    }

    /// <summary>
    /// Parses a Unicode string from a byte buffer.
    /// </summary>
    /// <param name="buffer">The byte buffer containing the Unicode string.</param>
    /// <returns>The parsed Unicode string.</returns>
    unsafe private static string ParseUnicodeString(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* pBuffer = buffer)
        {
            return Encoding.Unicode.GetString(pBuffer, buffer.Length);
        }
    }
}
