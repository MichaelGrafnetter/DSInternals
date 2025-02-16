using System;
using System.Buffers.Binary;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using DSInternals.Common.Properties;

namespace DSInternals.Common.Data
{
    public class DnsResourceRecord
    {
        private const int StructVersion = 0x05;
        private static readonly int DnsResourceRecordHeaderSize = Marshal.SizeOf<DnsResourceRecordHeader>();
        private static readonly int SrvResourceRecordHeaderSize = Marshal.SizeOf<SrvResourceRecordHeader>();
        private static readonly int SoaResourceRecordHeaderSize = Marshal.SizeOf<SoaResourceRecordHeader>();
        private const int ZoneFileFirstColumnWidth = 24;
        private static readonly string ZoneFileFirstColumnBlank = new string(' ', ZoneFileFirstColumnWidth);
        private static readonly string ZoneFileFirstThreeColumnsBlank = $"{ZoneFileFirstColumnBlank}\t\t";

        /// <summary>
        /// The type of the resource record.
        /// </summary>
        public ResourceRecordType Type;

        /// <summary>
        /// Resource record properties.
        /// </summary>
        public ResourceRecordRank Rank;

        /// <summary>
        /// The serial number of the SOA record of the zone containing this resource record.
        /// </summary>
        public uint Serial;

        /// <summary>
        /// The duration after which this record will expire.
        /// </summary>
        public TimeSpan TTL;

        /// <summary>
        /// The time stamp for the record when it received the last update.
        /// </summary>
        public DateTime? TimeStamp;

        /// <summary>
        /// The resource record's data.
        /// </summary>
        public string Data;

        private DnsResourceRecord(DnsResourceRecordHeader header, string data)
        {
            this.Type = header.Type;
            this.Rank = header.Rank;
            this.Rank = header.Rank;
            this.Serial = header.Serial;
            this.TTL = TimeSpan.FromSeconds(header.TtlSeconds);

            // The file time is in nanoseconds, while the time stamp is in hours.
            this.TimeStamp = header.TimeStamp == 0 ? null : DateTime.FromFileTimeUtc((long)header.TimeStamp * 60 * 60 * 10000000);

            this.Data = data;
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
            /// Not used. The value MUST be 0x0000.
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

        public static DnsResourceRecord Parse(ReadOnlySpan<byte> binaryRecord)
        {
            // Parse the binary structure header
            var header = MemoryMarshal.Read<DnsResourceRecordHeader>(binaryRecord);

            if (header.Version != StructVersion)
            {
                // The version number associated with the resource record attribute.The value MUST be 0x05.
                throw new ArgumentOutOfRangeException(nameof(header.Version), header.Version, "Unsupported DNS_RPC_RECORD version.");
            }

            if (BitConverter.IsLittleEndian)
            {
                // This field uses big-endian byte order.
                header.TtlSeconds = BinaryPrimitives.ReverseEndianness(header.TtlSeconds);
            }

            // We now know the length, in bytes, of the Data field.
            var binaryData = binaryRecord.Slice(DnsResourceRecordHeaderSize, header.DataLength);

            // Type-specific conversion of the binary data to a string.
            string data = header.Type switch
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
                ResourceRecordType.ATMA or
                ResourceRecordType.KEY or
                ResourceRecordType.MINFO or
                ResourceRecordType.NXT or
                ResourceRecordType.RP or // Responsible Person.
                ResourceRecordType.WKS or // Well Known Services. Deprecated in favour of SRV.
                ResourceRecordType.WINS or
                ResourceRecordType.WINSR or
                ResourceRecordType.GPOS or
                ResourceRecordType.DHCID or
                ResourceRecordType.NAPTR or
                ResourceRecordType.RRSIG or
                ResourceRecordType.DNSKEY or
                ResourceRecordType.DS or
                ResourceRecordType.NSEC or
                ResourceRecordType.NSEC3 or
                ResourceRecordType.NSEC3PARAM or
                ResourceRecordType.TLSA or
                ResourceRecordType.CERT or
                ResourceRecordType.TKEY or
                ResourceRecordType.TSIG or
                _ => ParseUnknown(binaryData)
            };

            return new DnsResourceRecord(header, data);
        }

        /// <summary>
        /// Parses the DNS_RPC_RECORD_A structure.
        /// </summary>
        private static string ParseA(ReadOnlySpan<byte> binaryData)
        {
            int binaryIPAddress = BinaryPrimitives.ReadInt32LittleEndian(binaryData);
            return new IPAddress(binaryIPAddress).ToString();
        }

        /// <summary>
        /// Parses the DNS_RPC_RECORD_AAAA structure.
        /// </summary>
        private static string ParseAAAA(ReadOnlySpan<byte> binaryData)
        {
            // This conversion is less efficient because of backwards compatibility with .NET Framework.
            return new IPAddress(binaryData.ToArray()).ToString();
        }

        /// <summary>
        /// Parses the DNS_COUNT_NAME structure.
        /// </summary>
        private static string ParseFQDN(ReadOnlySpan<byte> data)
        {
            if (data == null || data.Length <= 2 || data[0] == 0)
            {
                // To represent an empty string, cchNameLength MUST be zero and dnsName MUST be empty.
                return string.Empty;
            }

            var result = new StringBuilder(data.Length);
            int currentOffset = 0;

            byte length = data[currentOffset++];
            if(data.Length - 2 != length)
            {
                // Note: The length in the data structure excludes itself and the trailing zero.
                throw new ArgumentException("Unexpected DNS_COUNT_NAME structure length.", nameof(data));
            }

            byte labelCount = data[currentOffset++];

            for (byte i = 0; i < labelCount; i++)
            {
                byte labelLength = data[currentOffset++];
                string label = ParseUTF8String(data.Slice(currentOffset, labelLength));
                currentOffset += labelLength;
                result.Append(label);

                if(labelCount > 0)
                {
                    // DNS name segment delimiter and FQDN terminator. Omit for single-label CNAMEs.
                    result.Append('.');
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Parses a sequence of DNS_RPC_NAME structures.
        /// </summary>
        private static string ParseTXT(ReadOnlySpan<byte> buffer)
        {
            if (buffer == null || buffer.Length <= 1 || buffer[0] == 0)
            {
                return string.Empty;
            }

            var result = new StringBuilder(buffer.Length);
            int currentOffset = 0;

            while (currentOffset < buffer.Length)
            {
                byte length = buffer[currentOffset++];
                string label = ParseUTF8String(buffer.Slice(currentOffset, length));
                currentOffset += length;

                // Strings must be enclosed in double quotes
                result.Append('"');
                result.Append(label);
                result.Append('"');

                // String delimiter
                result.Append(' ');
            }

            // Remove the trailing space
            result.Remove(result.Length - 1, 1);

            // Example: "google-site-verification=" "rXOxyZounnZasA8Z7oaD3c14JdjS9aKSWvsR1EbUSIQ"
            return result.ToString();
        }

        /// <summary>
        /// Parses the DNS_RPC_RECORD_SRV structure.
        /// </summary>
        private static string ParseSRV(ReadOnlySpan<byte> buffer)
        {
            var header = MemoryMarshal.Read<SrvResourceRecordHeader>(buffer);

            if(BitConverter.IsLittleEndian)
            {
                // These fields use big-endian byte order.
                header.Priority = BinaryPrimitives.ReverseEndianness(header.Priority);
                header.Weight = BinaryPrimitives.ReverseEndianness(header.Weight);
                header.Port = BinaryPrimitives.ReverseEndianness(header.Port);
            }

            string nameTarget = ParseFQDN(buffer.Slice(SrvResourceRecordHeaderSize));

            // Example: 10 20 389 dc01.contoso.com
            return $"{header.Priority} {header.Weight} {header.Port}\t{nameTarget}";
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
        /// Parses the DNS_RPC_RECORD_NAME_PREFERENCE structure.
        /// </summary>
        private static string ParseMX(ReadOnlySpan<byte> buffer)
        {
            ushort preference = BinaryPrimitives.ReadUInt16BigEndian(buffer);
            string nameExchange = ParseFQDN(buffer.Slice(sizeof(ushort)));

            // Example: 10 mail.contoso.com
            return $"{preference} {nameExchange}";
        }

        /// <summary>
        /// Parses the DNS_RPC_RECORD_TS structure.
        /// </summary>
        private static string ParseZERO(ReadOnlySpan<byte> buffer)
        {
            // Extract the tombstoned timestamp in the FILETIME format.
            long fileTime = BinaryPrimitives.ReadInt64LittleEndian(buffer);
            return DateTime.FromFileTimeUtc(fileTime).ToString("u");
        }

        /// <summary>
        /// Parses the DNS_RPC_RECORD_UNKNOWN and DNS_RPC_RECORD_NULL structures.
        /// </summary>
        /// <remarks>
        /// Unknown record type, e.g., CAA (TYPE257). Return its length and data as a hexadecimal string.
        /// </remarks>
        private static string ParseUnknown(ReadOnlySpan<byte> buffer)
        {
            if(buffer == null || buffer.Length == 0)
            {
                return string.Empty;
            }

            // Example: \\# 18 00056973737565656e74727573742e6e6574
            string hexString = buffer.ToHex(false);
            return $"\\# {buffer.Length} {hexString}";
        }

        unsafe private static string ParseUTF8String(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* labelPointer = buffer)
            {
                return Encoding.UTF8.GetString(labelPointer, buffer.Length);
            }
        }
    }
}
