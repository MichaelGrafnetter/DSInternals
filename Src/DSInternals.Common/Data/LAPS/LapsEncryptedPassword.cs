using System.Runtime.InteropServices;
using DSInternals.Common.Cryptography.Asn1.DpapiNg;

namespace DSInternals.Common.Data;

public class LapsEncryptedPassword
{
    private static readonly int StructHeaderSize = Marshal.SizeOf<LapsEncryptedPasswordHeader>();

    /// <summary>
    /// Contains the UTC timestamp specifying when this password was stored.
    /// </summary>
    public DateTime UpdateTimeStamp
    {
        get;
        private set;
    }

    public CngProtectedDataBlob EncryptedBlob
    {
        get;
        private set;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct LapsEncryptedPasswordHeader
    {
        public uint PasswordUpdateTimestampHigh;
        public uint PasswordUpdateTimestampLow;

        /// <summary>
        /// Specifies the size of the EncryptedPassword field in bytes.
        /// </summary>
        public int EncryptedPasswordSize;

        /// <summary>
        /// Reserved for future use. This field must be set to zero.
        /// </summary>
        public uint Reserved;
    }

    public LapsEncryptedPassword(ReadOnlyMemory<byte> buffer)
    {
        Validator.AssertMinLength(buffer, StructHeaderSize, nameof(buffer));

        // Parse the fixed length header
        var header = MemoryMarshal.Read<LapsEncryptedPasswordHeader>(buffer.Span);

        // Perform data conversion
        long timestamp = (long)header.PasswordUpdateTimestampHigh << 32 | (long)header.PasswordUpdateTimestampLow;
        this.UpdateTimeStamp = DateTime.FromFileTimeUtc(timestamp);

        // Extract variable-length data
        var encryptedPassword = buffer.Slice(StructHeaderSize, header.EncryptedPasswordSize);
        this.EncryptedBlob = CngProtectedDataBlob.Decode(encryptedPassword);
    }

    public LapsClearTextPassword Decrypt()
    {
        var binaryLapsPassword = EncryptedBlob.Decrypt();

        if (binaryLapsPassword.Length == 0)
        {
            // The encrypted password is empty
            return null;
        }

        return LapsClearTextPassword.Parse(binaryLapsPassword, utf16: true);
    }

    public bool TryDecrypt(out LapsClearTextPassword lapsPassword)
    {
        bool success = EncryptedBlob.TryDecrypt(out ReadOnlySpan<byte> binaryLapsPassword);

        if (!success || binaryLapsPassword.Length == 0)
        {
            // The decrypted password is empty or decryption failed
            lapsPassword = default;
            return success;
        }

        lapsPassword = LapsClearTextPassword.Parse(binaryLapsPassword, utf16: true);
        return true;
    }
}
