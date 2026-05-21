using System.Buffers.Binary;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32;
using Windows.Win32.Networking.WinSock;

namespace DSInternals.Common.DNS;

public class DnsResourceRecord
{
    private const int StructVersion = 0x05;
    private static readonly int DnsResourceRecordHeaderSize = Marshal.SizeOf<DnsResourceRecordHeader>();
    private static readonly int SrvResourceRecordHeaderSize = Marshal.SizeOf<SrvResourceRecordHeader>();
    private static readonly int SoaResourceRecordHeaderSize = Marshal.SizeOf<SoaResourceRecordHeader>();
    private static readonly int DnsKeyResourceRecordHeaderSize = Marshal.SizeOf<DnsKeyResourceRecordHeader>();
    private static readonly int DsResourceRecordHeaderSize = Marshal.SizeOf<DsResourceRecordHeader>();
    private static readonly int TlsaResourceRecordHeaderSize = Marshal.SizeOf<TlsaResourceRecordHeader>();
    private static readonly int Nsec3ParamResourceRecordHeaderSize = Marshal.SizeOf<Nsec3ParamResourceRecordHeader>();
    private static readonly int Nsec3ResourceRecordHeaderSize = Marshal.SizeOf<Nsec3ResourceRecordHeader>();
    private static readonly int SignatureResourceRecordHeaderSize = Marshal.SizeOf<SignatureResourceRecordHeader>();
    private static readonly int WksResourceRecordHeaderSize = Marshal.SizeOf<WksResourceRecordHeader>();
    private static readonly int NaptrResourceRecordHeaderSize = Marshal.SizeOf<NaptrResourceRecordHeader>();
    private static readonly int WinsResourceRecordHeaderSize = Marshal.SizeOf<WinsResourceRecordHeader>();
    private static readonly int WinsrResourceRecordHeaderSize = Marshal.SizeOf<WinsrResourceRecordHeader>();

    // The first column must be wide enough for most SRV records to fit in.
    private const int ZoneFileNameColumnWidth = 60;
    private const int ZoneFileTtlColumnWidth = 6;
    private const int ZoneFileTypeColumnWidth = 4 + 5 + 1; // E.g. "IN  CNAME "

    /// <summary>
    /// Default TTL (in seconds) assumed for the zone. Per-record TTLs equal to this value are omitted from
    /// zone-file output to mimic Windows DNS Server's $TTL-aware emission.
    /// </summary>
    private const uint DefaultTtlSeconds = 3600;

    /// <summary>
    /// If the RDATA is of zero length, the text representation contains only the \# token and the single zero representing the length.
    /// </summary>
    private const string EmptyResourceData = "\\# 0";

    /// <summary>
    /// Length, in bytes, of an ATM End System Address (AESA) NSAP:
    /// AFI(1) + DCC|ICD(2) + HO-DSP(10) + ESI(6) + SEL(1), per ITU-T X.213 / ATM Forum.
    /// </summary>
    private const int AesaAddressLength = 20;

    private static readonly string ZoneFileTtlColumnBlank = new(' ', ZoneFileTtlColumnWidth);
    private static readonly string ZoneFileFirstThreeColumnsBlank = new(' ', ZoneFileNameColumnWidth + ZoneFileTtlColumnWidth + ZoneFileTypeColumnWidth);

    /// <summary>
    /// The DNS zone in which this record is located.
    /// </summary>
    public string Zone
    {
        get;
        private set;
    }

    /// <summary>
    /// The host name of the resource record.
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// The type of the resource record.
    /// </summary>
    public ResourceRecordType Type
    {
        get;
        private set;
    }

    /// <summary>
    /// Resource record properties.
    /// </summary>
    public ResourceRecordRank Rank
    {
        get;
        private set;
    } = ResourceRecordRank.Zone;

    /// <summary>
    /// Resource record flags.
    /// </summary>
    public ResourceRecordFlags Flags
    {
        get;
        private set;
    }

    /// <summary>
    /// The serial number of the SOA record of the zone containing this resource record.
    /// </summary>
    public uint Serial
    {
        get;
        private set;
    }

    /// <summary>
    /// The duration after which this record will expire.
    /// </summary>
    public TimeSpan TTL
    {
        get;
        private set;
    }

    /// <summary>
    /// The time stamp for the record when it received the last update.
    /// </summary>
    public DateTime? TimeStamp
    {
        get;
        private set;
    }

    /// <summary>
    /// The resource record's data.
    /// </summary>
    public string Data
    {
        get;
        private set;
    }

    private string TypeString
    {
        get
        {
            bool isKnownRecordType = this.Type switch
            {
                // Windows Server does not natively support CAA and SSHFP records.
                ResourceRecordType.CAA => false,
                ResourceRecordType.SSHFP => false,
                _ => Enum.IsDefined<ResourceRecordType>(this.Type)
            };

            if (this.Flags.HasFlag(ResourceRecordFlags.RecordWireFormat))
            {
                // Wire-format records are emitted using RFC 3597 generic type syntax.
                isKnownRecordType = false;
            }

            // Use the numeric value for unknown/unsupported types, e.g., TYPE257.
            return isKnownRecordType ? this.Type.ToString() : $"TYPE{(ushort)this.Type}";
        }
    }

    public DnsResourceRecord(string zone, string name, ResourceRecordType type, uint serial, string data, TimeSpan? ttl = null, DateTime? timeStamp = null, ResourceRecordRank rank = ResourceRecordRank.Zone, ResourceRecordFlags flags = ResourceRecordFlags.None)
    {
        if (string.IsNullOrWhiteSpace(zone))
        {
            throw new ArgumentNullException(nameof(zone));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (ttl.HasValue)
        {
            if (ttl.Value.TotalSeconds > uint.MaxValue)
            {
                // The TTL is stored as a 32-bit unsigned integer.
                throw new ArgumentOutOfRangeException(nameof(ttl));
            }

            this.TTL = ttl.Value;
        }

        else
        {
            // Defaults to 1h.
            this.TTL = TimeSpan.FromHours(1);
        }

        this.Zone = zone;
        this.Name = name;
        this.Type = type;
        this.Rank = rank;
        this.Flags = flags;
        this.Serial = serial;
        this.TimeStamp = timeStamp;
        this.Data = data;
    }

    /// <summary>
    /// Initializes a new instance from a parsed DNS_RPC_RECORD header and its already-decoded textual data.
    /// </summary>
    private DnsResourceRecord(string zone, string name, DnsResourceRecordHeader header, string data)
    {
        this.Zone = zone;
        this.Name = name;
        this.Type = header.Type;
        this.Rank = header.Rank;
        this.Flags = header.Flags;
        this.Serial = header.Serial;
        this.TTL = TimeSpan.FromSeconds(header.TtlSeconds);

        // header.TimeStamp is in hours since 1601-01-01 UTC; FILETIME is in 100-nanosecond intervals, so scale by 3600 × 10^7.
        this.TimeStamp = header.TimeStamp == 0 ? null : DateTime.FromFileTimeUtc((long)header.TimeStamp * 60 * 60 * 10000000);

        this.Data = data;
    }

    override public string ToString()
    {
        var result = new StringBuilder();

        if (this.Type == ResourceRecordType.ZERO)
        {
            // Tombstoned records will be commented out.
            result.Append($"; {this.Name,-ZoneFileNameColumnWidth + 3} ");
        }
        else
        {
            // Compensate the width for the trailing space
            result.Append($"{this.Name,-ZoneFileNameColumnWidth + 1} ");
        }

        // TTL: omitted only when equal to the zone's default, so it inherits $TTL. An explicit 0 is preserved
        // (e.g. WINS helper records) to match Windows DNS Server's zone-file emission.
        uint ttlSeconds = (uint)this.TTL.TotalSeconds;

        if (ttlSeconds != DefaultTtlSeconds)
        {
            result.Append($"{ttlSeconds,-ZoneFileTtlColumnWidth + 1} ");
        }
        else
        {
            // Omit the TTL
            result.Append(ZoneFileTtlColumnBlank);
        }

        // Class is only emitted for SOA (Windows DNS Server omits IN on every other record); blank-pad otherwise to keep the type column aligned.
        string classPrefix = this.Type == ResourceRecordType.SOA ? "IN  " : "    ";
        result.Append($"{classPrefix}{this.TypeString,-ZoneFileTypeColumnWidth + 5} {this.Data}");
        return result.ToString();
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DnsResourceRecordHeader
    {
        public ushort DataLength;
        public ResourceRecordType Type;
        public byte Version;
        public ResourceRecordRank Rank;

        /// <summary>
        /// Record flags such as DNS_RPC_FLAG_RECORD_WIRE_FORMAT. MS-DNSP §2.2.2.2.5 documents this as reserved (MUST be 0x0000),
        /// but Windows DNS sets DNS_RPC_FLAG_RECORD_WIRE_FORMAT (0x0010) for records stored in RFC 3597 generic-type format.
        /// </summary>
        public ResourceRecordFlags Flags;
        public uint Serial;
        public uint TtlSeconds;

        /// <summary>
        /// This field is reserved for future use. The value MUST be 0x00000000.
        /// </summary>
        public uint Reserved;
        public uint TimeStamp;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_SRV structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SrvResourceRecordHeader
    {
        /// <summary>
        /// The priority of the target host as specified in [RFC2782].
        /// </summary>
        public ushort Priority;

        /// <summary>
        /// The relative weight for the target host as specified in [RFC2782].
        /// </summary>
        public ushort Weight;

        /// <summary>
        /// The port number for the service on the target host as specified in [RFC2782].
        /// </summary>
        public ushort Port;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_DNSKEY and DNS_RPC_RECORD_KEY structures.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DnsKeyResourceRecordHeader
    {
        /// <summary>
        /// Key flags. Bit 7 (0x0100) is the Zone Key bit; bit 15 (0x0001) is the Secure Entry Point (SEP) bit.
        /// </summary>
        public DnsKeyFlags Flags;

        /// <summary>
        /// MUST be 0x03 for DNSSEC.
        /// </summary>
        public DnsKeyProtocol Protocol;

        /// <summary>
        /// The DNSSEC cryptographic algorithm.
        /// </summary>
        public DnsSigningAlgorithm Algorithm;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_SOA structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SoaResourceRecordHeader
    {
        /// <summary>
        /// The serial number of the SOA record.
        /// </summary>
        public uint SerialNo;

        /// <summary>
        /// The interval, in seconds, at which a secondary DNS server attempts to contact the primary DNS server for getting an update.
        /// </summary>
        public uint Refresh;

        /// <summary>
        /// The interval, in seconds, at which a secondary DNS server retries to check with the primary DNS server in case of failure.
        /// </summary>
        public uint Retry;

        /// <summary>
        /// The duration, in seconds, that a secondary DNS server continues attempts to get updates from the primary DNS server and if still unsuccessful assumes that the primary DNS server is unreachable.
        /// </summary>
        public uint Expire;

        /// <summary>
        /// The minimum duration, in seconds, for which record data in the zone is valid.
        /// </summary>
        public uint MinimumTTL;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_DS structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DsResourceRecordHeader
    {
        /// <summary>
        /// The key tag of the referenced DNSKEY. Stored in network byte order on the wire.
        /// </summary>
        public ushort KeyTag;

        /// <summary>
        /// The DNSSEC cryptographic algorithm of the referenced DNSKEY.
        /// </summary>
        public DnsSigningAlgorithm Algorithm;

        /// <summary>
        /// The digest algorithm used to construct the digest.
        /// </summary>
        public DnsDigestType DigestType;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_TLSA structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct TlsaResourceRecordHeader
    {
        public TlsaCertificateUsage CertificateUsage;
        public TlsaSelector Selector;
        public TlsaMatchingType MatchingType;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_NSEC3PARAM structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct Nsec3ParamResourceRecordHeader
    {
        public byte HashAlgorithm;
        public Nsec3Flags Flags;

        /// <summary>
        /// The number of additional hash iterations. Stored in network byte order on the wire.
        /// </summary>
        public ushort Iterations;

        public byte SaltLength;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_NSEC3 structure.
    /// </summary>
    /// <remarks>
    /// Per MS-DNSP §2.2.2.2.4.24, the salt length and hash length both appear before the variable-length salt and hash —
    /// this differs from the RFC 5155 wire format, where the hash length follows the salt.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct Nsec3ResourceRecordHeader
    {
        public byte HashAlgorithm;
        public Nsec3Flags Flags;

        /// <summary>
        /// The number of additional hash iterations. Stored in network byte order on the wire.
        /// </summary>
        public ushort Iterations;

        public byte SaltLength;
        public byte HashLength;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_SIG and DNS_RPC_RECORD_RRSIG structures.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SignatureResourceRecordHeader
    {
        /// <summary>
        /// The resource record type covered by the signature. Stored in network byte order on the wire.
        /// </summary>
        public ushort TypeCovered;

        /// <summary>
        /// The DNSSEC cryptographic algorithm used to produce the signature.
        /// </summary>
        public DnsSigningAlgorithm Algorithm;

        /// <summary>
        /// The number of labels in the original signed owner name.
        /// </summary>
        public byte LabelCount;

        /// <summary>
        /// The original TTL of the signed RRset. Stored in network byte order on the wire.
        /// </summary>
        public uint OriginalTtl;

        /// <summary>
        /// The signature expiration time as a Unix timestamp. Stored in network byte order on the wire.
        /// </summary>
        public uint SigExpiration;

        /// <summary>
        /// The signature inception time as a Unix timestamp. Stored in network byte order on the wire.
        /// </summary>
        public uint SigInception;

        /// <summary>
        /// The key tag of the DNSKEY used to produce the signature. Stored in network byte order on the wire.
        /// </summary>
        public ushort KeyTag;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_WKS structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct WksResourceRecordHeader
    {
        /// <summary>
        /// IPv4 address of the host in network byte order. Read separately from the buffer to avoid platform-dependent interpretation.
        /// </summary>
        public uint IpAddress;

        /// <summary>
        /// The IP protocol identifier (e.g., 6 for TCP, 17 for UDP).
        /// </summary>
        public byte Protocol;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_NAPTR structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct NaptrResourceRecordHeader
    {
        /// <summary>
        /// The order in which the NAPTR records MUST be processed. Stored in network byte order on the wire.
        /// </summary>
        public ushort Order;

        /// <summary>
        /// The relative preference among records of equal order. Stored in network byte order on the wire.
        /// </summary>
        public ushort Preference;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_WINS structure.
    /// </summary>
    /// <remarks>
    /// MS-DNSP §2.2.2.2.4.21 calls out only aipWinsServers as being in network byte order; the four preceding DWORD fields
    /// mirror the native windns.h DNS_WINS_DATA layout and are stored in little-endian (host) byte order in the dnsRecord blob.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct WinsResourceRecordHeader
    {
        /// <summary>
        /// WINS mapping behavior flags (DNS_WINS_FLAG_*).
        /// </summary>
        public WinsMappingFlags MappingFlag;

        /// <summary>
        /// Timeout, in seconds, after which WINS lookups are abandoned.
        /// </summary>
        public uint LookupTimeout;

        /// <summary>
        /// Duration, in seconds, for which positive WINS responses are cached.
        /// </summary>
        public uint CacheTimeout;

        /// <summary>
        /// Number of WINS server IPv4 addresses that follow this header.
        /// </summary>
        public uint WinsServerCount;
    }

    /// <summary>
    /// Header of the DNS_RPC_RECORD_WINSR structure.
    /// </summary>
    /// <remarks>
    /// The three DWORD fields use little-endian (host) byte order, matching native windns.h layout (see <see cref="WinsResourceRecordHeader"/>).
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct WinsrResourceRecordHeader
    {
        /// <summary>
        /// WINS-R mapping behavior flags (DNS_WINS_FLAG_*).
        /// </summary>
        public WinsMappingFlags MappingFlag;

        /// <summary>
        /// Timeout, in seconds, after which WINS-R lookups are abandoned.
        /// </summary>
        public uint LookupTimeout;

        /// <summary>
        /// Duration, in seconds, for which positive WINS-R responses are cached.
        /// </summary>
        public uint CacheTimeout;
    }

    /// <summary>
    /// Creates a DNS resource record instance from serialized DNS_RPC_RECORD data.
    /// </summary>
    /// <param name="zone">The DNS zone name.</param>
    /// <param name="name">The relative record name within the zone.</param>
    /// <param name="binaryRecordData">The binary DNS_RPC_RECORD payload.</param>
    /// <returns>A parsed <see cref="DnsResourceRecord"/> instance.</returns>
    /// <exception cref="ArgumentException"><paramref name="zone"/> or <paramref name="name"/> is null, empty, or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="binaryRecordData"/> is invalid or contains an unsupported structure version.</exception>
    public static DnsResourceRecord Create(string zone, string name, ReadOnlySpan<byte> binaryRecordData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(zone);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (binaryRecordData.IsEmpty || binaryRecordData.Length < DnsResourceRecordHeaderSize)
        {
            throw new ArgumentOutOfRangeException(nameof(binaryRecordData), binaryRecordData.Length, "Invalid DNS_RPC_RECORD data.");
        }

        // Parse the binary structure header
        var header = MemoryMarshal.Read<DnsResourceRecordHeader>(binaryRecordData);

        if (header.Version != StructVersion)
        {
            // The version number associated with the resource record attribute.The value MUST be 0x05.
            throw new ArgumentOutOfRangeException(nameof(binaryRecordData), header.Version, "Unsupported DNS_RPC_RECORD version.");
        }

        if (BitConverter.IsLittleEndian)
        {
            // This field uses big-endian byte order.
            header.TtlSeconds = BinaryPrimitives.ReverseEndianness(header.TtlSeconds);
        }

        // We now know the length, in bytes, of the Data field.
        var binaryData = binaryRecordData.Slice(DnsResourceRecordHeaderSize, header.DataLength);

        // Type-specific conversion of the binary data to a string.
        string data = header.Flags.HasFlag(ResourceRecordFlags.RecordWireFormat)
            ? ParseUnknown(binaryData)
            : header.Type switch
        {
            ResourceRecordType.A => ParseA(binaryData),
            ResourceRecordType.AAAA => ParseAAAA(binaryData),
            ResourceRecordType.SRV => ParseSRV(binaryData),
            ResourceRecordType.SOA => ParseSOA(binaryData),
            ResourceRecordType.ZERO => ParseZERO(binaryData),
            ResourceRecordType.PTR or
            ResourceRecordType.NS or
            ResourceRecordType.CNAME or
            ResourceRecordType.DNAME or
            ResourceRecordType.MB or
            ResourceRecordType.MR or
            ResourceRecordType.MG or
            ResourceRecordType.MD or
            ResourceRecordType.MF => ParseFQDN(binaryData),
            ResourceRecordType.TXT or
            ResourceRecordType.HINFO or
            ResourceRecordType.ISDN or
            ResourceRecordType.X25 or
            ResourceRecordType.LOC => ParseTXT(binaryData),
            ResourceRecordType.MX or
            ResourceRecordType.AFSDB or
            ResourceRecordType.RT => ParseMX(binaryData),
            ResourceRecordType.MINFO or
            ResourceRecordType.RP => ParseMAILERROR(binaryData),
            ResourceRecordType.DNSKEY or
            ResourceRecordType.KEY => ParseDNSKEY(binaryData),
            ResourceRecordType.DS => ParseDS(binaryData),
            ResourceRecordType.TLSA => ParseTLSA(binaryData),
            ResourceRecordType.DHCID => ParseDHCID(binaryData),
            ResourceRecordType.NSEC3PARAM => ParseNSEC3PARAM(binaryData),
            ResourceRecordType.NSEC => ParseNSEC(binaryData),
            ResourceRecordType.NSEC3 => ParseNSEC3(binaryData),
            ResourceRecordType.NXT => ParseNXT(binaryData),
            ResourceRecordType.SIG or
            ResourceRecordType.RRSIG => ParseSignature(binaryData),
            ResourceRecordType.ATMA => ParseATMA(binaryData),
            ResourceRecordType.NAPTR => ParseNAPTR(binaryData),
            ResourceRecordType.WKS => ParseWKS(binaryData),
            ResourceRecordType.WINS => ParseWINS(binaryData),
            ResourceRecordType.WINSR => ParseWINSR(binaryData),
            ResourceRecordType.NULL or
            ResourceRecordType.GPOS or
            ResourceRecordType.CERT or
            ResourceRecordType.TKEY or
            ResourceRecordType.TSIG or
            ResourceRecordType.CAA or
            ResourceRecordType.SSHFP or
            ResourceRecordType.SVCB or
            ResourceRecordType.HTTPS or
            _ => ParseUnknown(binaryData)
        };

        return new DnsResourceRecord(zone, name, header, data);
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_A structure.
    /// </summary>
    private static string ParseA(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length == 0)
        {
            return EmptyResourceData;
        }

        uint binaryIPAddress = BinaryPrimitives.ReadUInt32LittleEndian(buffer);
        return new IPAddress(binaryIPAddress).ToString();
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_AAAA structure.
    /// </summary>
    private static string ParseAAAA(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty)
        {
            return EmptyResourceData;
        }

#if NET8_0_OR_GREATER
        return new IPAddress(buffer).ToString();
#else
        return new IPAddress(buffer.ToArray()).ToString();
#endif
    }

    /// <summary>
    /// Parses the DNS_COUNT_NAME structure.
    /// </summary>
    private static string ParseFQDN(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length <= 2 || buffer[0] == 0)
        {
            // To represent an empty string, cchNameLength MUST be zero and dnsName MUST be empty.
            return string.Empty;
        }

        int currentOffset = 0;

        byte length = buffer[currentOffset++];
        if (buffer.Length - 2 != length)
        {
            // Note: The length in the data structure excludes itself and the trailing zero.
            throw new ArgumentException("Unexpected DNS_COUNT_NAME structure length.", nameof(buffer));
        }

        byte labelCount = buffer[currentOffset++];
        List<string> labels = new(labelCount);

        for (byte i = 0; i < labelCount; i++)
        {
            byte labelLength = buffer[currentOffset++];
            labels.Add(ParseUTF8String(buffer.Slice(currentOffset, labelLength)));
            currentOffset += labelLength;
        }

        // DNS name segments are dot-separated and the FQDN is dot-terminated.
        return labels.Count == 0 ? string.Empty : string.Join('.', labels) + ".";
    }

    /// <summary>
    /// Parses a sequence of DNS_RPC_NAME structures.
    /// </summary>
    private static string ParseTXT(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty || buffer[0] == 0)
        {
            return EmptyResourceData;
        }

        List<string> labels = [];
        int currentOffset = 0;

        while (currentOffset < buffer.Length)
        {
            byte length = buffer[currentOffset++];
            string label = ParseUTF8String(buffer.Slice(currentOffset, length));
            currentOffset += length;

            // RFC 1035 §5.1: literal " and \ inside a character-string must be backslash-escaped.
            labels.Add(QuoteDnsString(label));
        }

        // Example: ( "google-site-verification=" "rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ" )
        return $"( {string.Join(' ', labels)} )";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_SRV structure.
    /// </summary>
    private static string ParseSRV(ReadOnlySpan<byte> buffer)
    {
        var header = MemoryMarshal.Read<SrvResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // These fields use big-endian byte order.
            header.Priority = BinaryPrimitives.ReverseEndianness(header.Priority);
            header.Weight = BinaryPrimitives.ReverseEndianness(header.Weight);
            header.Port = BinaryPrimitives.ReverseEndianness(header.Port);
        }

        string nameTarget = ParseFQDN(buffer.Slice(SrvResourceRecordHeaderSize));

        // Example: 10 20 389  dc01.contoso.com
        return $"{header.Priority} {header.Weight} {header.Port,-4} {nameTarget}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_SOA structure.
    /// </summary>
    private static string ParseSOA(ReadOnlySpan<byte> buffer)
    {
        var header = MemoryMarshal.Read<SoaResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // These fields use big-endian byte order.
            header.Refresh = BinaryPrimitives.ReverseEndianness(header.Refresh);
            header.Retry = BinaryPrimitives.ReverseEndianness(header.Retry);
            header.Expire = BinaryPrimitives.ReverseEndianness(header.Expire);
            header.MinimumTTL = BinaryPrimitives.ReverseEndianness(header.MinimumTTL);
            header.SerialNo = BinaryPrimitives.ReverseEndianness(header.SerialNo);
        }

        // Add 2 bytes for the length of the primary server name and for the trailing zero.
        int primaryServerLength = buffer[SoaResourceRecordHeaderSize] + 2;
        string namePrimaryServer = ParseFQDN(buffer.Slice(SoaResourceRecordHeaderSize, primaryServerLength));

        int emailOffset = SoaResourceRecordHeaderSize + primaryServerLength;
        string zoneAdministratorEmail = ParseFQDN(buffer.Slice(emailOffset));

        // Example: ns1-05.azure-dns.com. azuredns-hostmaster.microsoft.com. (1 3600 300 2419200 300)
        return $@"{namePrimaryServer} {zoneAdministratorEmail} (
{ZoneFileFirstThreeColumnsBlank}{header.SerialNo,-7}      ; serial number
{ZoneFileFirstThreeColumnsBlank}{header.Refresh,-6}       ; refresh
{ZoneFileFirstThreeColumnsBlank}{header.Retry,-4}         ; retry
{ZoneFileFirstThreeColumnsBlank}{header.Expire,-5}        ; expire
{ZoneFileFirstThreeColumnsBlank}{header.MinimumTTL,-10} ) ; default TTL";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_DNSKEY and DNS_RPC_RECORD_KEY structures.
    /// </summary>
    private static string ParseDNSKEY(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < DnsKeyResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<DnsKeyResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // This field uses big-endian byte order.
            header.Flags = (DnsKeyFlags)BinaryPrimitives.ReverseEndianness((ushort)header.Flags);
        }

        // The public key follows the fixed-size header.
        ReadOnlySpan<byte> publicKey = buffer.Slice(DnsKeyResourceRecordHeaderSize);

        // The public key is presented in base64 form per RFC 4034 section 2.2.
        // TODO: Use Polyfill for Convert.ToBase64String that accepts ReadOnlySpan<byte> when targeting .NET Framework.
        string base64Key = Convert.ToBase64String(publicKey.ToArray());

        // The key tag is computed from the full RDATA per RFC 4034 Appendix B.
        ushort keyTag = ComputeKeyTag(buffer);

        // Example: 257 3 8 AwEAAcS2/eMJ7nLeh... ; key id = 12345
        return $"{(ushort)header.Flags} {(byte)header.Protocol} {(byte)header.Algorithm} {base64Key} ; key id = {keyTag}";
    }

    /// <summary>
    /// Computes the DNSSEC key tag for a DNSKEY/KEY RDATA blob per RFC 4034 Appendix B.
    /// </summary>
    private static ushort ComputeKeyTag(ReadOnlySpan<byte> rdata)
    {
        uint accumulator = 0;

        for (int i = 0; i < rdata.Length; i++)
        {
            accumulator += (i & 1) == 0 ? (uint)rdata[i] << 8 : rdata[i];
        }

        accumulator += (accumulator >> 16) & 0xFFFF;
        return (ushort)(accumulator & 0xFFFF);
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NAME_PREFERENCE structure.
    /// </summary>
    private static string ParseMX(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < sizeof(ushort))
        {
            return EmptyResourceData;
        }

        // The preference value uses big-endian byte order.
        ushort preference = BinaryPrimitives.ReadUInt16BigEndian(buffer);
        string nameExchange = ParseFQDN(buffer.Slice(sizeof(ushort)));

        // Example: 10 mail.contoso.com
        return $"{preference} {nameExchange}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_MAIL_ERROR structure.
    /// </summary>
    private static string ParseMAILERROR(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty)
        {
            return EmptyResourceData;
        }

        int offset = 0;

        if (!TryReadCountName(buffer, ref offset, out string nameMailBox) ||
            !TryReadCountName(buffer, ref offset, out string errorMailBox))
        {
            return EmptyResourceData;
        }

        // Example: hostmaster.contoso.com. errors.contoso.com.
        return $"{nameMailBox} {errorMailBox}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_TS structure.
    /// </summary>
    private static string ParseZERO(ReadOnlySpan<byte> buffer)
    {
        // Extract the tombstoned timestamp in the FILETIME format.
        long fileTime = BinaryPrimitives.ReadInt64LittleEndian(buffer);
        DateTime tombstonedTimeStamp = DateTime.FromFileTimeUtc(fileTime);

        // Send the timestamp in a comment.
        return $"\\# 0 ; Tombstoned at {tombstonedTimeStamp:u}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_UNKNOWN and DNS_RPC_RECORD_NULL structures.
    /// </summary>
    /// <remarks>
    /// Unknown record type, e.g., CAA (TYPE257). Return its length and data as a hexadecimal string.
    /// </remarks>
    private static string ParseUnknown(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty)
        {
            return EmptyResourceData;
        }

        // Example: \\# 18 00056973737565656e74727573742e6e6574
        string hexString = buffer.ToHex(false);
        return $"\\# {buffer.Length} {hexString}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_WKS structure.
    /// </summary>
    /// <remarks>
    /// MS-DNSP §2.2.2.2.4.13 stores bBitMask in the RFC 1035 §3.4.2 wire format: a big-endian bit map where
    /// bit 0 (MSB of byte 0) represents port 0, bit 1 represents port 1, and so on.
    /// </remarks>
    private static string ParseWKS(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < WksResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<WksResourceRecordHeader>(buffer);

        // The IPv4 address bytes are in network byte order on the wire; the byte-span/byte[] constructor
        // consumes them as-is, avoiding the platform-endianness quirks of IPAddress(long).
#if NET8_0_OR_GREATER
        IPAddress ipAddress = new(buffer.Slice(0, sizeof(uint)));
#else
        IPAddress ipAddress = new(buffer.Slice(0, sizeof(uint)).ToArray());
#endif
        var protocol = (IPPROTO)header.Protocol;
        string protocolName = protocol.ToProtocolName();

        ReadOnlySpan<byte> bitmap = buffer.Slice(WksResourceRecordHeaderSize);
        var services = new List<string>();

        for (int byteIndex = 0; byteIndex < bitmap.Length; byteIndex++)
        {
            byte bits = bitmap[byteIndex];

            if (bits == 0)
            {
                continue;
            }

            for (int bitIndex = 0; bitIndex < 8; bitIndex++)
            {
                // Test the bit corresponding to the current port. RFC 1035 §3.4.2 numbers bits left-to-right
                // (MSB first), so the mask starts at 0b1000_0000 (0x80) for bit 0 and shifts right for later bits.
                if ((bits & (0x80 >> bitIndex)) == 0)
                {
                    continue;
                }

                // Each bitmap byte covers 8 consecutive ports, so the port number is (byteIndex * 8) + bitIndex
                // (e.g., byte 3, bit 1 → port 25 = smtp).
                ushort port = (ushort)(byteIndex * 8 + bitIndex);
                services.Add(ServiceNameTranslator.GetServiceName(protocol, port));
            }
        }

        // Example: 192.0.2.20 tcp ( smtp domain http )
        return services.Count == 0
            ? $"{ipAddress} {protocolName}"
            : $"{ipAddress} {protocolName} ( {string.Join(' ', services)} )";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_ATMA structure.
    /// </summary>
    private static string ParseATMA(ReadOnlySpan<byte> buffer)
    {
        // 1 byte format + variable address.
        if (buffer.Length < 2)
        {
            return EmptyResourceData;
        }

        // The format byte values differ between MS-DNSP §2.2.2.2.4.19 (AESA = 0) and Microsoft's windns.h (DNS_ATMA_FORMAT_AESA = 2). Real records observed on Windows DNS servers use the windns.h value.
        byte format = buffer[0];
        ReadOnlySpan<byte> address = buffer.Slice(1);

        return format switch
        {
            (byte)PInvoke.DNS_ATMA_FORMAT_AESA => FormatAtmaAesa(address),
            (byte)PInvoke.DNS_ATMA_FORMAT_E164 => FormatAtmaE164(address),
            _ => $"{format} {address.ToHex(false)}"
        };
    }

    /// <summary>
    /// Formats an ATM End System Address (AESA) NSAP as the dot-delimited groups used by Windows DNS Server,
    /// falling back to the raw hex representation when the address length does not match the NSAP layout.
    /// </summary>
    private static string FormatAtmaAesa(ReadOnlySpan<byte> address)
    {
        if (address.Length != AesaAddressLength)
        {
            return address.ToHex(false);
        }

        return string.Concat(
            address.Slice(0, 1).ToHex(false), ".",
            address.Slice(1, 2).ToHex(false), ".",
            address.Slice(3, 10).ToHex(false), ".",
            address.Slice(13, 6).ToHex(false), ".",
            address.Slice(19, 1).ToHex(false));
    }

    /// <summary>
    /// Formats an ITU-T E.164 ATM address by prefixing <c>+</c> to the raw digits stored in the record.
    /// </summary>
    private static string FormatAtmaE164(ReadOnlySpan<byte> address)
    {
        return "+" + ParseUTF8String(address).TrimEnd('\0');
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NAPTR structure.
    /// </summary>
    /// <remarks>
    /// MS-DNSP §2.2.2.2.4.20 documents all four trailing name fields (nameFlags, nameService, nameSubstitution, nameReplacement)
    /// as DNS_RPC_NAME. The first three carry RFC 3403 character-string values (flags, services, regexp) and are read as such.
    /// The nameReplacement field is an FQDN; on Windows DNS servers it is observed on the wire as a DNS_COUNT_NAME (the same
    /// encoding used elsewhere in this file for NS/CNAME/SRV target/SOA primary), so we try DNS_COUNT_NAME first and fall back
    /// to the spec-documented DNS_RPC_NAME if the leading length byte does not describe a valid count-name.
    /// </remarks>
    private static string ParseNAPTR(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < NaptrResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<NaptrResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // These fields use big-endian byte order.
            header.Order = BinaryPrimitives.ReverseEndianness(header.Order);
            header.Preference = BinaryPrimitives.ReverseEndianness(header.Preference);
        }

        int offset = NaptrResourceRecordHeaderSize;

        if (!TryReadRpcString(buffer, ref offset, out string flags) ||
            !TryReadRpcString(buffer, ref offset, out string service) ||
            !TryReadRpcString(buffer, ref offset, out string substitution))
        {
            return EmptyResourceData;
        }

        // Prefer DNS_COUNT_NAME for the replacement FQDN; fall back to DNS_RPC_NAME per MS-DNSP §2.2.2.2.4.20.
        int replacementOffset = offset;

        if (!TryReadCountName(buffer, ref replacementOffset, out string replacement))
        {
            replacementOffset = offset;

            if (!TryReadRpcString(buffer, ref replacementOffset, out replacement))
            {
                return EmptyResourceData;
            }
        }

        // Example: 100 10 "u" "E2U+sip" "!^.*$!sip:info@example.com!" replacement.example.com.
        return $"{header.Order} {header.Preference} {QuoteDnsString(flags)} {QuoteDnsString(service)} {QuoteDnsString(substitution)} {replacement}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_WINS structure.
    /// </summary>
    /// <remarks>
    /// MS-DNSP §2.2.2.2.4.21 calls out only aipWinsServers as being in network byte order; the four preceding DWORD fields
    /// mirror the native windns.h DNS_WINS_DATA layout and are stored in little-endian (host) byte order in the dnsRecord blob.
    /// </remarks>
    private static string ParseWINS(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < WinsResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<WinsResourceRecordHeader>(buffer);

        if (!BitConverter.IsLittleEndian)
        {
            // These fields use little-endian (host) byte order.
            header.MappingFlag = (WinsMappingFlags)BinaryPrimitives.ReverseEndianness((uint)header.MappingFlag);
            header.LookupTimeout = BinaryPrimitives.ReverseEndianness(header.LookupTimeout);
            header.CacheTimeout = BinaryPrimitives.ReverseEndianness(header.CacheTimeout);
            header.WinsServerCount = BinaryPrimitives.ReverseEndianness(header.WinsServerCount);
        }

        if (header.WinsServerCount == 0 || header.WinsServerCount > (buffer.Length - WinsResourceRecordHeaderSize) / sizeof(uint))
        {
            return EmptyResourceData;
        }

        var servers = new List<IPAddress>((int)header.WinsServerCount);
        for (int i = 0; i < header.WinsServerCount; i++)
        {
            int ipOffset = WinsResourceRecordHeaderSize + i * sizeof(uint);
#if NET8_0_OR_GREATER
            servers.Add(new IPAddress(buffer.Slice(ipOffset, sizeof(uint))));
#else
            servers.Add(new IPAddress(buffer.Slice(ipOffset, sizeof(uint)).ToArray()));
#endif
        }

        var result = new StringBuilder();
        AppendWinsMappingFlags(result, header.MappingFlag);
        result.Append($"L{header.LookupTimeout} C{header.CacheTimeout} ( ");
        // Explicit generic argument forces the AppendJoin<T>(char, IEnumerable<T>) overload; without it,
        // net48 binds to AppendJoin(char, params object[]) and stringifies the list as a single element.
        result.AppendJoin<IPAddress>(' ', servers);
        result.Append(" )");

        // Example: LOCAL L5 C3600 ( 10.0.0.1 10.0.0.2 )
        return result.ToString();
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_WINSR structure.
    /// </summary>
    /// <remarks>
    /// The three DWORD fields use little-endian (host) byte order, matching native windns.h layout (see <see cref="ParseWINS"/>).
    /// </remarks>
    private static string ParseWINSR(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < WinsrResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<WinsrResourceRecordHeader>(buffer);

        if (!BitConverter.IsLittleEndian)
        {
            // These fields use little-endian (host) byte order.
            header.MappingFlag = (WinsMappingFlags)BinaryPrimitives.ReverseEndianness((uint)header.MappingFlag);
            header.LookupTimeout = BinaryPrimitives.ReverseEndianness(header.LookupTimeout);
            header.CacheTimeout = BinaryPrimitives.ReverseEndianness(header.CacheTimeout);
        }

        ReadOnlySpan<byte> domainBuffer = buffer.Slice(WinsrResourceRecordHeaderSize);
        string nameResultDomain = ParseCountNameOrRpcString(domainBuffer);

        if (string.IsNullOrEmpty(nameResultDomain))
        {
            return EmptyResourceData;
        }

        var result = new StringBuilder();
        AppendWinsMappingFlags(result, header.MappingFlag);
        result.Append($"L{header.LookupTimeout} C{header.CacheTimeout} {nameResultDomain}");

        // Example: L5 C3600 example.com.
        return result.ToString();
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_DS structure.
    /// </summary>
    private static string ParseDS(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < DsResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<DsResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // This field uses big-endian byte order.
            header.KeyTag = BinaryPrimitives.ReverseEndianness(header.KeyTag);
        }

        string digest = buffer.Slice(DsResourceRecordHeaderSize).ToHex(false);

        // Example: 60485 5 1 2bb183af5f22588179a53b0a98631fad1a292118
        return $"{header.KeyTag} {(byte)header.Algorithm} {(byte)header.DigestType} {digest}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_TLSA structure.
    /// </summary>
    private static string ParseTLSA(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < TlsaResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<TlsaResourceRecordHeader>(buffer);
        // RFC 6698 §2.2 — TLSA certificate-association data is emitted as uppercase hex, matching Windows DNS Server.
        string certData = buffer.Slice(TlsaResourceRecordHeaderSize).ToHex(true);

        // Example: 0 0 1 D2ABDE240D7CD3EE6B4B28C54DF034B97983A1D16E8A410E4561CB106618E971
        return $"{(byte)header.CertificateUsage} {(byte)header.Selector} {(byte)header.MatchingType} {certData}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_DHCID structure.
    /// </summary>
    private static string ParseDHCID(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty)
        {
            return EmptyResourceData;
        }

        // RFC 4701 section 3.3 presents DHCID RDATA as a single base64 blob.
        return Convert.ToBase64String(buffer.ToArray());
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NSEC3PARAM structure.
    /// </summary>
    private static string ParseNSEC3PARAM(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < Nsec3ParamResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<Nsec3ParamResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // This field uses big-endian byte order.
            header.Iterations = BinaryPrimitives.ReverseEndianness(header.Iterations);
        }

        if (buffer.Length < Nsec3ParamResourceRecordHeaderSize + header.SaltLength)
        {
            return EmptyResourceData;
        }

        // RFC 5155 §3.3: a zero-length salt is represented in zone files as a single dash.
        string salt = header.SaltLength == 0 ? "-" : buffer.Slice(Nsec3ParamResourceRecordHeaderSize, header.SaltLength).ToHex(false);

        // Example: 1 0 12 aabbccdd
        return $"{header.HashAlgorithm} {(byte)header.Flags} {header.Iterations} {salt}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NSEC structure.
    /// </summary>
    /// <remarks>
    /// The signer (next domain) name is encoded as a DNS_COUNT_NAME, followed by the type bit map.
    /// </remarks>
    private static string ParseNSEC(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < 2)
        {
            return EmptyResourceData;
        }

        // The first byte of a DNS_COUNT_NAME is the total length, excluding itself and the trailing zero.
        int nameLength = buffer[0] + 2;

        if (buffer.Length < nameLength)
        {
            return EmptyResourceData;
        }

        string nextName = ParseFQDN(buffer.Slice(0, nameLength));
        string typeList = ParseTypeBitMap(buffer.Slice(nameLength));

        // Example: next.example.com. A NS RRSIG NSEC
        return string.IsNullOrEmpty(typeList) ? nextName : $"{nextName} {typeList}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NXT structure.
    /// </summary>
    private static string ParseNXT(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < sizeof(ushort))
        {
            return EmptyResourceData;
        }

        // MS-DNSP §2.2.2.2.4.18: TypeWordCount is stored in host (little-endian) byte order.
        ushort typeWordCount = BinaryPrimitives.ReadUInt16LittleEndian(buffer);

        if (typeWordCount <= 1)
        {
            // MS-DNSP requires the type bitmap to contain more than one 16-bit word.
            return EmptyResourceData;
        }

        int typeWordsLength = typeWordCount * sizeof(ushort);
        int nextNameOffset = sizeof(ushort) + typeWordsLength;

        if (buffer.Length < nextNameOffset + 2)
        {
            return EmptyResourceData;
        }

        if (!TryParseNxtTypeBitMap(buffer.Slice(sizeof(ushort), typeWordsLength), out string typeList))
        {
            return EmptyResourceData;
        }

        int offset = nextNameOffset;

        if (!TryReadCountName(buffer, ref offset, out string nextName))
        {
            return EmptyResourceData;
        }

        // Example: medium.foo.nil. A MX SIG NXT
        return string.IsNullOrEmpty(typeList) ? nextName : $"{nextName} {typeList}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NSEC3 structure.
    /// </summary>
    private static string ParseNSEC3(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < Nsec3ResourceRecordHeaderSize)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<Nsec3ResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // This field uses big-endian byte order.
            header.Iterations = BinaryPrimitives.ReverseEndianness(header.Iterations);
        }

        int offset = Nsec3ResourceRecordHeaderSize;

        if (buffer.Length < offset + header.SaltLength + header.HashLength)
        {
            return EmptyResourceData;
        }

        string salt = header.SaltLength == 0 ? "-" : buffer.Slice(offset, header.SaltLength).ToHex(false);
        offset += header.SaltLength;

        string nextHashedOwner = ToBase32Hex(buffer.Slice(offset, header.HashLength));
        offset += header.HashLength;

        string typeList = ParseTypeBitMap(buffer.Slice(offset));

        // Example: 1 0 12 aabbccdd 2t7b4g4vsa5smi47k61mv5bv1a22bojr A NS SOA RRSIG DNSKEY NSEC3PARAM
        string head = $"{header.HashAlgorithm} {(byte)header.Flags} {header.Iterations} {salt} {nextHashedOwner}";
        return string.IsNullOrEmpty(typeList) ? head : $"{head} {typeList}";
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_SIG and DNS_RPC_RECORD_RRSIG structures.
    /// </summary>
    private static string ParseSignature(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < SignatureResourceRecordHeaderSize + 2)
        {
            return EmptyResourceData;
        }

        var header = MemoryMarshal.Read<SignatureResourceRecordHeader>(buffer);

        if (BitConverter.IsLittleEndian)
        {
            // These fields use big-endian byte order.
            header.TypeCovered = BinaryPrimitives.ReverseEndianness(header.TypeCovered);
            header.OriginalTtl = BinaryPrimitives.ReverseEndianness(header.OriginalTtl);
            header.SigExpiration = BinaryPrimitives.ReverseEndianness(header.SigExpiration);
            header.SigInception = BinaryPrimitives.ReverseEndianness(header.SigInception);
            header.KeyTag = BinaryPrimitives.ReverseEndianness(header.KeyTag);
        }

        int signerOffset = SignatureResourceRecordHeaderSize;

        if (!TryReadCountName(buffer, ref signerOffset, out string signer))
        {
            return EmptyResourceData;
        }

        string signature = Convert.ToBase64String(buffer.Slice(signerOffset).ToArray());

        // SIG/RRSIG timestamps are emitted as YYYYMMDDHHmmSS in UTC.
        string expirationString = DateTimeOffset.FromUnixTimeSeconds(header.SigExpiration).UtcDateTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        string inceptionString = DateTimeOffset.FromUnixTimeSeconds(header.SigInception).UtcDateTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);

        var typeCovered = (ResourceRecordType)header.TypeCovered;
        string typeCoveredString = Enum.IsDefined<ResourceRecordType>(typeCovered) ? typeCovered.ToString() : $"TYPE{header.TypeCovered}";

        // Example: A 8 2 86400 20210101000000 20201231000000 12345 example.com. abc...==
        return $"{typeCoveredString} {(byte)header.Algorithm} {header.LabelCount} {header.OriginalTtl} {expirationString} {inceptionString} {header.KeyTag} {signer} {signature}";
    }

    /// <summary>
    /// Parses an RFC 4034 §4.1.2 type bit map.
    /// </summary>
    private static string ParseTypeBitMap(ReadOnlySpan<byte> buffer)
    {
        if (buffer.IsEmpty)
        {
            return string.Empty;
        }

        var types = new List<string>();
        int offset = 0;

        while (offset + 2 <= buffer.Length)
        {
            byte windowBlock = buffer[offset++];
            byte bitmapLength = buffer[offset++];

            if (bitmapLength == 0 || bitmapLength > 32 || offset + bitmapLength > buffer.Length)
            {
                break;
            }

            for (int byteIndex = 0; byteIndex < bitmapLength; byteIndex++)
            {
                byte bits = buffer[offset + byteIndex];

                for (int bitIndex = 0; bitIndex < 8; bitIndex++)
                {
                    // RFC 4034 §4.1.2: bits are numbered in network bit order (bit 0 = MSB).
                    if ((bits & (0x80 >> bitIndex)) == 0)
                    {
                        continue;
                    }

                    int typeNumber = windowBlock * 256 + byteIndex * 8 + bitIndex;
                    var rrType = (ResourceRecordType)typeNumber;
                    types.Add(Enum.IsDefined<ResourceRecordType>(rrType) ? rrType.ToString() : $"TYPE{typeNumber}");
                }
            }

            offset += bitmapLength;
        }

        return string.Join(' ', types);
    }

    /// <summary>
    /// Parses the DNS_RPC_RECORD_NXT 16-bit type bit map from MS-DNSP.
    /// </summary>
    private static bool TryParseNxtTypeBitMap(ReadOnlySpan<byte> buffer, out string typeList)
    {
        typeList = string.Empty;

        if (buffer.IsEmpty || buffer.Length % sizeof(ushort) != 0)
        {
            return false;
        }

        var types = new List<string>();

        for (int wordIndex = 0; wordIndex < buffer.Length / sizeof(ushort); wordIndex++)
        {
            ushort bits = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(wordIndex * sizeof(ushort)));

            for (int bitIndex = 0; bitIndex < 16; bitIndex++)
            {
                if ((bits & (0x8000 >> bitIndex)) == 0)
                {
                    continue;
                }

                int typeNumber = wordIndex * 16 + bitIndex;

                if (typeNumber == 0)
                {
                    // RFC 2535 reserves this as an alternate bitmap-format marker; MS-DNSP requires it to be zero.
                    return false;
                }

                var rrType = (ResourceRecordType)typeNumber;
                types.Add(Enum.IsDefined<ResourceRecordType>(rrType) ? rrType.ToString() : $"TYPE{typeNumber}");
            }
        }

        typeList = string.Join(' ', types);
        return true;
    }

    /// <summary>
    /// Encodes a binary blob using the Extended Hex Alphabet base32 encoding defined in RFC 4648 §7, as required by NSEC3 (RFC 5155 §1.3).
    /// </summary>
    private static string ToBase32Hex(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            return string.Empty;
        }

        const string alphabet = "0123456789abcdefghijklmnopqrstuv";
        int outputLength = (data.Length * 8 + 4) / 5;
        var result = new StringBuilder(outputLength);

        int bitBuffer = 0;
        int bitCount = 0;

        for (int i = 0; i < data.Length; i++)
        {
            bitBuffer = (bitBuffer << 8) | data[i];
            bitCount += 8;

            while (bitCount >= 5)
            {
                bitCount -= 5;
                result.Append(alphabet[(bitBuffer >> bitCount) & 0x1F]);
            }
        }

        if (bitCount > 0)
        {
            result.Append(alphabet[(bitBuffer << (5 - bitCount)) & 0x1F]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Decodes a single length-prefixed DNS_RPC_NAME from the start of <paramref name="buffer"/>, returning an
    /// empty string when the buffer is empty or malformed.
    /// </summary>
    private static string ParseRpcString(ReadOnlySpan<byte> buffer)
    {
        int offset = 0;
        return TryReadRpcString(buffer, ref offset, out string value) ? value : string.Empty;
    }

    /// <summary>
    /// Decodes a name field that may be encoded as either a DNS_COUNT_NAME or a DNS_RPC_NAME, preferring the
    /// former and falling back to the latter — matching observed Windows DNS Server behavior for fields like
    /// WINSR's nameResultDomain.
    /// </summary>
    private static string ParseCountNameOrRpcString(ReadOnlySpan<byte> buffer)
    {
        int offset = 0;

        if (TryReadCountName(buffer, ref offset, out string countName))
        {
            return countName;
        }

        offset = 0;
        return TryReadRpcString(buffer, ref offset, out string rpcString) ? rpcString : string.Empty;
    }

    /// <summary>
    /// Tries to read a DNS_COUNT_NAME at <paramref name="offset"/> in <paramref name="buffer"/>; on success,
    /// advances <paramref name="offset"/> past the consumed bytes and returns the decoded FQDN.
    /// </summary>
    private static bool TryReadCountName(ReadOnlySpan<byte> buffer, ref int offset, out string value)
    {
        value = string.Empty;

        if (offset >= buffer.Length)
        {
            return false;
        }

        int nameLength = buffer[offset] + 2;

        if (nameLength < 2 || offset + nameLength > buffer.Length)
        {
            return false;
        }

        try
        {
            value = ParseFQDN(buffer.Slice(offset, nameLength));
        }
        catch (ArgumentException)
        {
            value = string.Empty;
            return false;
        }

        offset += nameLength;
        return true;
    }

    /// <summary>
    /// Tries to read a DNS_RPC_NAME (single length-prefixed UTF-8 string) at <paramref name="offset"/> in
    /// <paramref name="buffer"/>; on success, advances <paramref name="offset"/> past the consumed bytes.
    /// </summary>
    private static bool TryReadRpcString(ReadOnlySpan<byte> buffer, ref int offset, out string value)
    {
        value = string.Empty;

        if (offset >= buffer.Length)
        {
            return false;
        }

        byte length = buffer[offset++];

        if (length == 0)
        {
            return true;
        }

        if (offset + length > buffer.Length)
        {
            return false;
        }

        value = ParseUTF8String(buffer.Slice(offset, length));
        offset += length;
        return true;
    }

    /// <summary>
    /// Appends the textual representation of the WINS mapping flags (<c>LOCAL</c>, <c>SCOPE</c>, plus any unknown
    /// bits as <c>0x</c> hex) to <paramref name="result"/>, matching Windows DNS Server's WINS/WINSR formatting.
    /// </summary>
    private static void AppendWinsMappingFlags(StringBuilder result, WinsMappingFlags mappingFlag)
    {
        if (mappingFlag.HasFlag(WinsMappingFlags.Local))
        {
            result.Append("LOCAL ");
        }

        if (mappingFlag.HasFlag(WinsMappingFlags.Scope))
        {
            result.Append("SCOPE ");
        }

        uint unknownFlags = (uint)(mappingFlag & ~(WinsMappingFlags.Local | WinsMappingFlags.Scope));

        if (unknownFlags != 0)
        {
            result.Append($"0x{unknownFlags:x8} ");
        }
    }

    /// <summary>
    /// Wraps <paramref name="value"/> in double quotes for use as an RFC 1035 §5.1 character-string, escaping any
    /// literal <c>"</c> and <c>\</c> with a backslash.
    /// </summary>
    private static string QuoteDnsString(string value)
    {
        var result = new StringBuilder(value.Length + 2);
        result.Append('"');

        foreach (char c in value)
        {
            if (c is '"' or '\\')
            {
                result.Append('\\');
            }

            result.Append(c);
        }

        result.Append('"');
        return result.ToString();
    }

    /// <summary>
    /// Decodes a UTF-8 byte span to a string without copying the buffer, using the pointer-based
    /// <see cref="Encoding.GetString(byte*, int)"/> overload for compatibility with .NET Framework.
    /// </summary>
    unsafe private static string ParseUTF8String(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* labelPointer = buffer)
        {
            return Encoding.UTF8.GetString(labelPointer, buffer.Length);
        }
    }
}
