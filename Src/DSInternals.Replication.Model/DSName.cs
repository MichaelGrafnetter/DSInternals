using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;

namespace DSInternals.Replication
{
    /// <summary>
    /// Identifies a directory object using the values of one or more of its LDAP attributes: objectGUID, objectSid, or distinguishedName.
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/385d478f-3eb6-4d2c-ac58-f25c4debdd86</remarks>
    public class DSName
    {
        private const int GuidLength = 16;
        private const int SidMaxLength = 28;
        private const int DSNameHeaderSize = 2 * sizeof(uint) + GuidLength + SidMaxLength + sizeof(uint);

        public SecurityIdentifier? ObjectSid { get; private set; }

        public Guid? ObjectGuid { get; private set; }

        public string? DistinguishedName { get; private set; }

        public DSName(SecurityIdentifier? objectSid = null, Guid? objectGuid = null, string? distinguishedName = null)
        {
            this.ObjectSid = objectSid;
            this.ObjectGuid = objectGuid;
            this.DistinguishedName = distinguishedName;
        }

        private DSName()
        {
            // This constructor is only used by the static parse method.
        }

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
            uint structLen = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(currentPosition, sizeof(uint)));
            currentPosition += sizeof(uint);

            if (structLen != buffer.Length)
            {
                throw new ArgumentException($"Invalid DSNAME structure length. Expected {structLen} bytes, but got {buffer.Length} bytes.", nameof(buffer));
            }

            // The number of bytes in the Sid field used to represent the object's objectSid attribute value.
            uint sidLen = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(currentPosition, sizeof(uint)));
            currentPosition += sizeof(uint);

            if (sidLen > SidMaxLength)
            {
                throw new ArgumentException($"Invalid SID length. Expected at most {SidMaxLength} bytes, but got {sidLen} bytes.", nameof(buffer));
            }

            // The value of the object's objectGUID attribute.
            // TODO: For .NET Core, omit Span.ToArray()
            Guid parsedGuid = new Guid(buffer.Slice(currentPosition, GuidLength).ToArray());
            currentPosition += GuidLength;

            if (parsedGuid != Guid.Empty)
            {
                result.ObjectGuid = parsedGuid;
            }

            // The value of the object's objectSid attribute.
            if (sidLen > 0)
            {
                result.ObjectSid = ParseSecurityIdentifier(buffer.Slice(currentPosition, (int)sidLen));
            }

            // The SID has a constant length, even if not all bytes are used.
            currentPosition += SidMaxLength;

            // The number of characters in the StringName field, not including the terminating null character.
            uint nameLen = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Slice(currentPosition, sizeof(uint)));
            currentPosition += sizeof(uint);
            uint expectedNameLen = (structLen - DSNameHeaderSize) / sizeof(char) - 1;

            if (nameLen != expectedNameLen)
            {
                throw new ArgumentException($"Invalid Name length. Expected {expectedNameLen} characters, but got {nameLen} characters.", nameof(buffer));
            }

            if (nameLen > 0)
            {
                // A null-terminated Unicode value of the object's distinguishedName attribute.
                result.DistinguishedName = ParseUnicodeString(buffer.Slice(currentPosition, (int)(nameLen * sizeof(char))));
            }

            return result;
        }

        unsafe private static string ParseUnicodeString(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return Encoding.Unicode.GetString(pBuffer, buffer.Length);
            }
        }

        unsafe private static SecurityIdentifier ParseSecurityIdentifier(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* pBuffer = buffer)
            {
                return new SecurityIdentifier((IntPtr)pBuffer);
            }
        }
    }
}
