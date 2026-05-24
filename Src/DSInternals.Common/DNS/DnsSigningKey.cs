using System.Runtime.InteropServices;
using System.Text;
using DSInternals.Common.Cryptography;

namespace DSInternals.Common.DNS;

/// <summary>
/// Represents a DNSSEC signing key extracted from an Active Directory-integrated DNS zone.
/// </summary>
/// <remarks>
/// Wraps the Exported Key Pair structure used to persist a Zone Signing Key (ZSK)
/// on the DNS zone object in Active Directory, with the private key material protected by DPAPI-NG.
/// </remarks>
public class DnsSigningKey
{
    private static readonly int GuidSize = Marshal.SizeOf<Guid>();
    private static readonly int ExportedKeyPairHeaderSize = Marshal.SizeOf<ExportedKeyPairHeader>();
    private const uint ExpectedReserved1 = 0x00000010; // Documentation says 0x00000014, which is wrong.
    private const uint ExpectedReserved2 = 0x4B545250; // 'PRTK' in ASCII (Magic)

    /// <summary>
    /// The name of the DNS zone to which this signing key belongs.
    /// </summary>
    public string DnsZone { get; private set; }

    /// <summary>
    /// The unique identifier of the signing key.
    /// </summary>
    public Guid Guid { get; private set; }

    /// <summary>
    /// The name of the cryptographic algorithm associated with the key (e.g. <c>RSA</c>, <c>ECDSA_P256</c>, or <c>ECDSA_P384</c>).
    /// </summary>
    public string AlgorithmName { get; private set; }

    /// <summary>
    /// The DPAPI-NG-protected blob containing the private key material.
    /// </summary>
    public CngProtectedDataBlob ProtectedKeyBlob { get; private set; }

    /// <summary>
    /// Deserializes a DNS signing key from the specified binary data.
    /// </summary>
    /// <param name="dnsZone">The name of the DNS zone to which the key belongs.</param>
    /// <param name="binaryData">The raw binary representation of the signing key.</param>
    /// <returns>A populated <see cref="DnsSigningKey"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when the binary data is malformed or has an unexpected size or signature.</exception>
    public static DnsSigningKey Decode(string dnsZone, ReadOnlySpan<byte> binaryData)
    {
        if (binaryData.Length < GuidSize + ExportedKeyPairHeaderSize)
        {
            throw new ArgumentException("The binary data is too short to contain a valid DNS signing key.", nameof(binaryData));
        }

        DnsSigningKey result = new()
        {
            DnsZone = dnsZone
        };

        // GUID (16 bytes)
#if NETCOREAPP2_1_OR_GREATER
        result.Guid = new(binaryData.Slice(0, GuidSize));
#else
        result.Guid = new(binaryData.Slice(0, GuidSize).ToArray());
#endif
        // Exported Key Pair structure (variable size)
        ReadOnlySpan<byte> exportedKeyPairData = binaryData.Slice(GuidSize);
        var exportedKeyPairHeader = MemoryMarshal.Read<ExportedKeyPairHeader>(exportedKeyPairData);

        if (exportedKeyPairHeader.Reserved1 != ExpectedReserved1 || exportedKeyPairHeader.Reserved2 != ExpectedReserved2)
        {
            throw new ArgumentException("The binary data does not contain a valid Exported Key Pair signature.", nameof(binaryData));
        }

        int expectedTotalSize = GuidSize + ExportedKeyPairHeaderSize +
            exportedKeyPairHeader.LengthOfAlgorithmName +
            exportedKeyPairHeader.LengthOfProtectedKeyBlob;

        if (binaryData.Length != expectedTotalSize)
        {
            throw new ArgumentException("The binary data size does not match the expected size based on the Exported Key Pair header.", nameof(binaryData));
        }

        // Algorithm Name (RSA / ECDSA_P256 / ECDSA_P384), with trailing \0
        int currentOffset = ExportedKeyPairHeaderSize;
        ReadOnlySpan<byte> algorithmNameData = exportedKeyPairData.Slice(currentOffset, exportedKeyPairHeader.LengthOfAlgorithmName - sizeof(char));
        currentOffset += exportedKeyPairHeader.LengthOfAlgorithmName;

#if NETCOREAPP2_1_OR_GREATER
        result.AlgorithmName = Encoding.Unicode.GetString(algorithmNameData);
#else
        result.AlgorithmName = ParseUnicodeString(algorithmNameData);
#endif

        // Protected Key Blob (variable size)
        ReadOnlySpan<byte> protectedKeyBlob = exportedKeyPairData.Slice(currentOffset, exportedKeyPairHeader.LengthOfProtectedKeyBlob);
        currentOffset += exportedKeyPairHeader.LengthOfProtectedKeyBlob;

        // TODO: Avoid using ToArray() for performance reasons
        result.ProtectedKeyBlob = CngProtectedDataBlob.Decode(protectedKeyBlob.ToArray());

        return result;
    }

    /// <summary>
    /// Header of the Exported Key Pair structure.
    /// </summary>
    /// <remarks>
    /// The Exported Key Pair structure is used to wrap a ZSK for secure storage in the Active Directory database.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ExportedKeyPairHeader
    {
        /// <summary>
        /// MUST be 0x00000014.
        /// </summary>
        public uint Reserved1;

        /// <summary>
        /// MUST be 0x4B545250.
        /// </summary>
        public uint Reserved2;

        /// <summary>
        /// This MUST be equal to the length of the AlgorithmName field, in bytes.
        /// </summary>
        public int LengthOfAlgorithmName;

        /// <summary>
        /// This MUST be equal to the length of the ProtectedKeyBlob field, in bytes.
        /// </summary>
        public int LengthOfProtectedKeyBlob;

        // The documentation also mentions the LengthOfKeyName and KeyName fields, but they are not present in the actual structure.
    }

#if NETFRAMEWORK
    unsafe private static string ParseUnicodeString(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* bufferPointer = buffer)
        {
            return Encoding.Unicode.GetString(bufferPointer, buffer.Length);
        }
    }
#endif
}
