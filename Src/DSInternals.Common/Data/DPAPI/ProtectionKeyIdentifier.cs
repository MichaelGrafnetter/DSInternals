using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DSInternals.Common.Data;

/// <summary>
/// The Protection Key Identifier data structure is used to store metadata about keys used to cryptographically wrap DPAPI-NG encryption keys and to derive managed passwords.
/// </summary>
/// <see>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-dnsp/98a575da-ca48-4afd-ba79-f77a8bed4e4e</see>
public struct ProtectionKeyIdentifier
{
    private const string KdsKeyMagic = "KDSK";
    private const int ExpectedVersion = 1;
    private static readonly int StructureHeaderSize = Marshal.SizeOf<ProtectionKeyIdentifierHeader>();

    public int L0KeyId
    {
        get;
        private set;
    }

    public int L1KeyId
    {
        get;
        private set;
    }

    public int L2KeyId
    {
        get;
        private set;
    }

    public Guid RootKeyId
    {
        get;
        private set;
    }

    public string? DomainName
    {
        get;
        private set;
    }

    public string? ForestName
    {
        get;
        private set;
    }

    public ReadOnlyMemory<byte>? PublicKey
    {
        get;
        private set;
    }

    public GroupKeyEnvelopeFlags Flags
    {
        get;
        set;
    }

    /// <summary>
    /// Struct header
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ProtectionKeyIdentifierHeader
    {
        public int Version;
        public uint Magic;
        public GroupKeyEnvelopeFlags Flags;
        public int L0KeyId;
        public int L1KeyId;
        public int L2KeyId;
        public Guid RootKeyId;
        public int PublicKeyLength;
        public int DomainNameLength;
        public int ForestNameLength;

        // Variable length strings follow
    }

    public ProtectionKeyIdentifier(ReadOnlyMemory<byte> blob)
    {
        Validator.AssertMinLength(blob, StructureHeaderSize, nameof(blob));

        // Parse the fixed length structure header
        var header = MemoryMarshal.Read<ProtectionKeyIdentifierHeader>(blob.Span);

        // Version must be 0x00000001
        ArgumentOutOfRangeException.ThrowIfNotEqual(header.Version, ExpectedVersion);

        // Magic must be 0x4B53444B
        string magic = ParseMagic(header.Magic);
        ArgumentOutOfRangeException.ThrowIfNotEqual(magic, KdsKeyMagic);

        // Copy daya fields
        this.Flags = header.Flags;
        this.RootKeyId = header.RootKeyId;
        this.L0KeyId = header.L0KeyId;
        this.L1KeyId = header.L1KeyId;
        this.L2KeyId = header.L2KeyId;

        // Validate variable data length
        int expectedLength = StructureHeaderSize + header.PublicKeyLength + header.DomainNameLength + header.ForestNameLength;
        Validator.AssertMinLength(blob, expectedLength, nameof(blob));

        // Read the variable length data
        var remainingSlice = blob.Slice(StructureHeaderSize);

        if (header.PublicKeyLength > 0)
        {
            // Additional info used in key derivation
            this.PublicKey = remainingSlice.Slice(0, header.PublicKeyLength);

            // Advance the current position.
            remainingSlice = remainingSlice.Slice(header.PublicKeyLength);
        }

        if (header.DomainNameLength > 0)
        {
            // DNS-style name of the Active Directory domain in which this identifier was created.
            var binaryDomainName = remainingSlice.Slice(0, header.DomainNameLength);
            this.DomainName = ParseUnicodeString(binaryDomainName.Span);

            // Advance the current position.
            remainingSlice = remainingSlice.Slice(header.DomainNameLength);
        }

        if (header.ForestNameLength > 0)
        {
            // DNS-style name of the Active Directory forest in which this identifier was created.
            var binaryForestName = remainingSlice.Slice(0, header.ForestNameLength);
            this.ForestName = ParseUnicodeString(binaryForestName.Span);
        }
    }

    public ProtectionKeyIdentifier(Guid rootKeyId, DateTime effectiveTime, string? domain = null, string? forest = null)
    {
        (this.L0KeyId, this.L1KeyId, this.L2KeyId) = KdsRootKey.GetKeyId(effectiveTime);

        this.RootKeyId = rootKeyId;
        this.DomainName = domain;
        this.ForestName = forest;
    }

    public ProtectionKeyIdentifier(Guid rootKeyId, int l0KeyId, int l1KeyId, int l2KeyId, string? domain = null, string? forest = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(l0KeyId);

        ArgumentOutOfRangeException.ThrowIfNegative(l1KeyId);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(l1KeyId, KdsRootKey.L1KeyModulus);

        ArgumentOutOfRangeException.ThrowIfNegative(l2KeyId);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(l2KeyId, KdsRootKey.L2KeyModulus);

        this.RootKeyId = rootKeyId;
        this.L0KeyId = l0KeyId;
        this.L1KeyId = l1KeyId;
        this.L2KeyId = l2KeyId;
        this.DomainName = domain;
        this.ForestName = forest;
    }

    public override string ToString()
    {
        DateTime cycle = KdsRootKey.GetKeyStartTime(this.L0KeyId, this.L1KeyId, this.L2KeyId);

        return $"RootKey={this.RootKeyId}, Cycle={cycle} (L0={this.L0KeyId}, L1={this.L1KeyId}, L2={this.L2KeyId})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    unsafe private static string ParseUnicodeString(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* stringPointer = buffer)
        {
            // Trim \0
            return Encoding.Unicode.GetString(stringPointer, buffer.Length - sizeof(char));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    unsafe private static string ParseMagic(uint magic)
    {
        Span<byte> binaryMagic = stackalloc byte[sizeof(uint)];
        MemoryMarshal.Write(binaryMagic, ref magic);

        fixed (byte* stringPointer = binaryMagic)
        {
            return Encoding.ASCII.GetString(stringPointer, sizeof(uint));
        }
    }
}
