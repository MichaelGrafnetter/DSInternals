using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace DSInternals.Common.Data;

/// <summary>
/// Represents a signing key for zone signing and key signing on a DNS server.
/// </summary>
/// <remarks>Corresponds to the DNS_RPC_SKD structure.</remarks>
public class DnsSigningKeyDescriptor
{
    private static readonly int DescriptorPart2Size = Marshal.SizeOf<DescriptorPart2>();
    private static readonly int GuidSize = Marshal.SizeOf<Guid>();
    private static readonly int MinimumSize = sizeof(int) + GuidSize + sizeof(char) + DescriptorPart2Size + 3 * (sizeof(char) + sizeof(DnsSigningKeyScope));
    private const int ExtectedHeaderVersion = 1;
    private const int GuidStringLengthInBytes = 78; // "{f86d0667-f864-4276-8788-86fbf1c1234b}" with \0 terminator
    private const int MaxKspNameLengthInBytes = 256; // Must fit "Microsoft Software Key Storage Provider"

    /// <summary>
    /// The name of the zone to which the key is assigned.
    /// </summary>
    public string ZoneName
    {
        get;
        private set;
    }

    /// <summary>
    /// The unique identifier of the key.
    /// </summary>
    public Guid KeyId
    {
        get;
        private set;
    }

    /// <summary>
    /// The state of the key.
    /// </summary>
    public DnsSigningKeyState CurrentState
    {
        get;
        private set;
    }

    /// <summary>
    /// Indicates whether the key is stored in a zone object in Active Directory.
    /// </summary>
    public bool StoreKeysInAD
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration in which the signatures that cover DNSKEY record sets are valid.
    /// </summary>
    public TimeSpan DnsKeySignatureValidityPeriod
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration in which the signatures that cover DS record sets are valid.
    /// </summary>
    public TimeSpan DSSignatureValidityPeriod
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration in which the signatures that cover all other record sets are valid.
    /// </summary>
    public TimeSpan ZoneSignatureValidityPeriod
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration for which the first scheduled key rollover is delayed. This allows key rollovers to be staggered.
    /// </summary>
    public TimeSpan InitialRolloverOffset
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration between scheduled key rollovers.
    /// </summary>
    public TimeSpan RolloverPeriod
    {
        get;
        private set;
    }

    /// <summary>
    /// The key rollover type.
    /// </summary>
    public DnsSigningKeyRolloverType RolloverType
    {
        get;
        private set;
    }

    /// <summary>
    /// The action to take for the next key rollover event.
    /// </summary>
    public DnsSigningKeyRolloverAction NextRolloverAction
    {
        get;
        private set;
    }

    /// <summary>
    /// The time at which the last rollover event was performed.
    /// </summary>
    public DateTime LastRolloverTime
    {
        get;
        private set;
    }

    /// <summary>
    /// The time at which the next rollover action must take place.
    /// </summary>
    public DateTime NextRolloverTime
    {
        get;
        private set;
    }

    /// <summary>
    /// The time at which the next key was added to the zone.
    /// </summary>
    public DateTime NextKeyGenerationTime
    {
        get;
        private set;
    }

    /// <summary>
    /// The state of the key.
    /// </summary>
    public DnsSigningKeyRolloverStatus CurrentRolloverStatus
    {
        get;
        private set;
    }

    /// <summary>
    /// The Key Storage Provider (KSP) used to generate keys.
    /// </summary>
    public string KeyStorageProvider
    {
        get;
        private set;
    }

    /// <summary>
    /// Signing key scope for the SKD's active key.
    /// </summary>
    public Guid? ActiveKey
    {
        get;
        private set;
    }

    /// <summary>
    /// Signing key scope for the SKD's active key.
    /// </summary>
    public DnsSigningKeyScope ActiveKeyScope
    {
        get;
        private set;
    }

    /// <summary>
    /// This key will be used during the next key rollover event.
    /// </summary>
    public Guid? NextKey
    {
        get;
        private set;
    }

    /// <summary>
    /// Signing key scope for the SKD's next key.
    /// </summary>
    public DnsSigningKeyScope NextKeyScope
    {
        get;
        private set;
    }

    /// <summary>
    /// The signing key descriptor's standby key.
    /// </summary>
    public Guid? StandbyKey
    {
        get;
        private set;
    }

    /// <summary>
    /// Signing key scope for the SKD's standby key.
    /// </summary>
    public DnsSigningKeyScope StandbyKeyScope
    {
        get;
        private set;
    }

    /// <summary>
    /// The length, in bits, of the key.
    /// </summary>
    public int KeyLength
    {
        get;
        private set;
    }

    /// <summary>
    /// The type of the key.
    /// </summary>
    public DnsSigningKeyType KeyType
    {
        get;
        private set;
    }

    /// <summary>
    /// The type of DNSSEC signature generation algorithm used by the key.
    /// </summary>
    public DnsSigningAlgorithm CryptoAlgorithm
    {
        get;
        private set;
    }

    /// <summary>
    /// Deserializes a DNS signing key descriptor from the specified binary data.
    /// </summary>
    public static DnsSigningKeyDescriptor Decode(string dnsZone, ReadOnlySpan<byte> binaryData)
    {
        if (string.IsNullOrEmpty(dnsZone))
        {
            throw new ArgumentException("The DNS zone name cannot be null or empty.", nameof(dnsZone));
        }

        // Deserialize the descriptor
        DnsSigningKeyDescriptor descriptor = new();
        descriptor.ZoneName = dnsZone;

        if (binaryData.Length < MinimumSize)
        {
            throw new ArgumentException("The binary data is too short to contain a valid DNS signing key descriptor.", nameof(binaryData));
        }

        // Read the structure version
        int currentOffset = 0;
        int version = BinaryPrimitives.ReadInt32LittleEndian(binaryData);
        currentOffset += sizeof(int);

        if (version != ExtectedHeaderVersion)
        {
            throw new ArgumentException($"Unsupported DNS signing key descriptor version: {version}.", nameof(binaryData));
        }

        // The fIsKsk seems to be absent in the actual structures, contrary to the documentation.

        // Read GUID (16 bytes)
#if NETCOREAPP2_1_OR_GREATER
        descriptor.KeyId = new(binaryData.Slice(currentOffset, GuidSize));
#else
        descriptor.KeyId = new(binaryData.Slice(currentOffset, GuidSize).ToArray());
#endif
        currentOffset += GuidSize;

        // Read the key storage provider (KSP) name as a null-terminated Unicode string.
        (string? kspName, int? kspNameLength) = ParseUnicodeString(binaryData.Slice(currentOffset), MaxKspNameLengthInBytes);

        if (kspNameLength is null || String.IsNullOrEmpty(kspName))
        {
            throw new ArgumentException("The binary data does not contain a valid null-terminated KSP name.", nameof(binaryData));
        }

        descriptor.KeyStorageProvider = kspName;
        currentOffset += kspNameLength.Value;

        // Read fixed-size data
        var part2 = MemoryMarshal.Read<DescriptorPart2>(binaryData.Slice(currentOffset));
        currentOffset += DescriptorPart2Size;

        descriptor.KeyLength = part2.KeyLength;
        descriptor.KeyType = part2.IsKSK;
        descriptor.CryptoAlgorithm = part2.SigningAlgorithm;
        descriptor.StoreKeysInAD = part2.StoreKeysInDirectory != 0;
        descriptor.InitialRolloverOffset = TimeSpan.FromSeconds(part2.InitialRolloverOffset);
        descriptor.DnsKeySignatureValidityPeriod = TimeSpan.FromSeconds(part2.DNSKEYSignatureValidityPeriod);
        descriptor.DSSignatureValidityPeriod = TimeSpan.FromSeconds(part2.DSSignatureValidityPeriod);
        descriptor.ZoneSignatureValidityPeriod = TimeSpan.FromSeconds(part2.StandardSignatureValidityPeriod);
        descriptor.RolloverPeriod = TimeSpan.FromSeconds(part2.RolloverPeriod);
        descriptor.LastRolloverTime = DateTime.FromFileTimeUtc(part2.LastRolloverTime);
        descriptor.NextRolloverTime = DateTime.FromFileTimeUtc(part2.NextRolloverTime);
        descriptor.CurrentState = part2.State;
        descriptor.RolloverType = part2.RolloverType;
        descriptor.CurrentRolloverStatus = part2.CurrentRolloverStatus;
        descriptor.NextRolloverAction = part2.NextRolloverAction;
        DateTime nextKeyGenerationTime = DateTime.FromFileTimeUtc(part2.NextKeyGenerationTime);

        // Read pwszActiveKey (variable)
        (string? activeKeyName, int? activeKeyLength) = ParseUnicodeString(binaryData.Slice(currentOffset), GuidStringLengthInBytes);

        if (activeKeyLength is null)
        {
            throw new ArgumentException("The binary data does not contain a valid null-terminated active key identifier.", nameof(binaryData));
        }

        descriptor.ActiveKey = activeKeyName != null ? Guid.Parse(activeKeyName) : null;
        currentOffset += activeKeyLength.Value;

        // Read ActiveKeyScope (4 bytes)
        descriptor.ActiveKeyScope = (DnsSigningKeyScope)BinaryPrimitives.ReadUInt32LittleEndian(binaryData.Slice(currentOffset));
        currentOffset += sizeof(int);

        // Read pwszStandbyKey (variable)
        (string? standbyKeyName, int? standbyKeyLength) = ParseUnicodeString(binaryData.Slice(currentOffset), GuidStringLengthInBytes);

        if (standbyKeyLength is null)
        {
            throw new ArgumentException("The binary data does not contain a valid null-terminated standby key identifier.", nameof(binaryData));
        }

        descriptor.StandbyKey = standbyKeyName != null ? Guid.Parse(standbyKeyName) : null;
        currentOffset += standbyKeyLength.Value;

        // Read StandbyKeyScope (4 bytes)
        descriptor.StandbyKeyScope = (DnsSigningKeyScope)BinaryPrimitives.ReadUInt32LittleEndian(binaryData.Slice(currentOffset));
        currentOffset += sizeof(int);

        // Read pwszNextKey (variable)
        (string? nextKeyName, int? nextKeyLength) = ParseUnicodeString(binaryData.Slice(currentOffset), GuidStringLengthInBytes);

        if (nextKeyLength is null)
        {
            throw new ArgumentException("The binary data does not contain a valid null-terminated next key identifier.", nameof(binaryData));
        }

        descriptor.NextKey = nextKeyName != null ? Guid.Parse(nextKeyName) : null;
        currentOffset += nextKeyLength.Value;

        // Read NextKeyScope (4 bytes)
        descriptor.NextKeyScope = (DnsSigningKeyScope)BinaryPrimitives.ReadUInt32LittleEndian(binaryData.Slice(currentOffset));
        currentOffset += sizeof(int);

        if (part2.RevokedOrSwappedRecordCount > 0)
        {
            // TODO: Read RevokedOrSwappedDnskeys (variable)    
        }

        if (part2.FinalRecordCount > 0)
        {
            // TODO: Read FinalDnskeys (variable)
        }

        return descriptor;
    }

    /// <summary>
    /// Partial body of the DNS_RPC_SKD structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DescriptorPart2
    {
        /// <summary>
        /// Indicates whether the DNS server exports keys generated for this signing key descriptor and stores them on the DNS zone object in Active Directory.
        /// </summary>
        public int StoreKeysInDirectory;

        /// <summary>
        /// Indicates whether this descriptor describes a key signing key (KSK) or a zone signing key (ZSK).
        /// </summary>
        /// <remarks>
        /// Based on the documentation, this field should be positioned closer to the beginning of the structure.
        /// </remarks>
        public DnsSigningKeyType IsKSK;

        /// <summary>
        /// The cryptographic algorithm used to generate signing keys.
        /// </summary>
        public DnsSigningAlgorithm SigningAlgorithm;

        /// <summary>
        /// The length, in bits, of cryptographic signing keys.
        /// </summary>
        public int KeyLength;

        /// <summary>
        /// The amount of time, in seconds, to delay the first scheduled key rollover for this signing key descriptor.
        /// </summary>
        public int InitialRolloverOffset;

        /// <summary>
        /// The number of seconds that signatures covering DNSKEY record sets generated for this signing key descriptor's keys are valid
        /// </summary>
        public int DNSKEYSignatureValidityPeriod;

        /// <summary>
        /// The number of seconds that signatures covering DS record sets generated for this signing key descriptor's keys are valid.
        /// </summary>
        public int DSSignatureValidityPeriod;

        /// <summary>
        /// The number of seconds that signatures covering record sets not of type DNSKEY or DS generated for this signing key descriptor's keys are valid.
        /// </summary>
        public int StandardSignatureValidityPeriod;

        /// <summary>
        /// Specifies possible key rollover.
        /// </summary>
        public DnsSigningKeyRolloverType RolloverType;

        /// <summary>
        /// The number of seconds between scheduled key rollovers, or 0xFFFFFFFF to disable automatic key rollovers.
        /// </summary>
        public int RolloverPeriod;

        /// <summary>
        /// This value describes the next key rollover action that the DNS server will take for this signing key descriptor.
        /// </summary>
        public DnsSigningKeyRolloverAction NextRolloverAction;

        /// <summary>
        /// The time at which the last successful rollover event was performed for this signing key descriptor.
        /// </summary>
        public long LastRolloverTime;

        /// <summary>
        /// The time at which the next rollover for this signing key descriptor is scheduled. This MAY be 0 if no rollover event is scheduled.
        /// </summary>
        public long NextRolloverTime;

        /// <summary>
        /// The current state of this signing key descriptor.
        /// </summary>
        public DnsSigningKeyState State;

        /// <summary>
        /// The current rollover status of this signing key descriptor.
        /// </summary>
        public DnsSigningKeyRolloverStatus CurrentRolloverStatus;

        /// <summary>
        /// This value corresponds to the next step in a signing key descriptor's rollover process.
        /// </summary>
        public int CurrentRollState;

        /// <summary>
        ///  This value MUST be set to 0x00000001 in response to a successful ZonePerformKeyRollover operation on a signing key descriptor.When the SKD completes its rollover, this value MUST be set to 0x00000000.
        /// </summary>
        public int ManualTrigger;

        /// <summary>
        /// This value MUST be set to 0x00000001 when 90 percent of the dwRolloverPeriod for a signing key descriptor whose fIsKSK flag is 0x00000001 has elapsed. It MUST be set to 0x00000002 when 95 percent of this rollover period has elapsed, and it MUST be set to 0x00000003 when there is less than 1 day remaining before such a signing key descriptor begins its key rollover process.Otherwise, this value MUST be 0x00000000.
        /// </summary>
        public int PreRollEventFired;

        /// <summary>
        /// This value represents the time at which the most recent value of the pwszNextKey field of a signing key descriptor whose fIsKSK flag is 0x00000000 was generated.
        /// </summary>
        public long NextKeyGenerationTime;

        /// <summary>
        ///  This value MUST indicate the number of values present in the list of records in the RevokedOrSwappedDnskeys field.
        /// </summary>
        public int RevokedOrSwappedRecordCount;

        /// <summary>
        /// This value MUST indicate the number of values present in the list of records in the FinalRecordCount field.
        /// </summary>
        public int FinalRecordCount;
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

    private static (string? text, int? lengthInBytes) ParseUnicodeString(ReadOnlySpan<byte> buffer, int maxLengthInBytes)
    {
        maxLengthInBytes = Math.Min(maxLengthInBytes, buffer.Length);

        int? terminatorIndex = GetUnicodeStringTerminatorIndex(buffer);

        if (terminatorIndex is null)
        {
            // No null terminator found
            return (null, null);
        }

        if (terminatorIndex + sizeof(char) > maxLengthInBytes)
        {
            // Null terminator is beyond the maximum length.
            // Consider it not found.
            return (null, null);
        }

        if (terminatorIndex == 0)
        {
            // Empty string, only null terminator
            return (null, sizeof(char));
        }

        ReadOnlySpan<byte> textData = buffer.Slice(0, terminatorIndex.Value);

#if NETCOREAPP2_1_OR_GREATER
        string text = Encoding.Unicode.GetString(textData);
#else
        string text = ParseUnicodeString(textData);
#endif

        return (text, terminatorIndex + sizeof(char));
    }

    private static int? GetUnicodeStringTerminatorIndex(ReadOnlySpan<byte> buffer)
    {
        // Find \0\0 at even locations
        for (int i = 0; i < buffer.Length; i += sizeof(char))
        {
            if (buffer[i] == 0 && buffer[i + 1] == 0)
            {
                return i;
            }
        }

        // Null terminator not found
        return null;
    }
}
