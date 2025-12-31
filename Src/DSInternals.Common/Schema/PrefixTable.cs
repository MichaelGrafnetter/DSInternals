using System.Globalization;
using System.Numerics;
using System.Text;

/// <summary>
/// Any OID-valued quantity is stored as a 32-bit unsigned integer.
/// </summary>
using ATTRTYP = uint;

using PrefixIndex = ushort;

namespace DSInternals.Common.Schema;
public class PrefixTable
{
    public const int LastBuiltInPrefixIndex = 38;
    private const int MinBlobLength = 2 * sizeof(uint);
    private const long LongLimit = (long.MaxValue >> 7) - 0x7f;

    private SortedDictionary<PrefixIndex, string> _forwardMap;
    private SortedDictionary<string, PrefixIndex> _reverseMap;

    public PrefixTable(byte[] blob = null, bool prePopulate = true)
    {
        _forwardMap = new SortedDictionary<PrefixIndex, string>();
        _reverseMap = new SortedDictionary<string, PrefixIndex>(StringComparer.Ordinal);

        if (prePopulate)
        {
            // Add hardcoded prefixes
            this.AddBuiltinPrefixes();
        }

        // Add user prefixes, if any
        if (blob != null)
        {
            this.LoadFromBlob(blob);
        }
    }

    public string? this[PrefixIndex prefixIndex]
    {
        get
        {
            // Return null if not found
            _ = _forwardMap.TryGetValue(prefixIndex, out string? value);
            return value;
        }
    }

    public int Count
    {
        get
        {
            return _forwardMap.Count;
        }
    }

    /// <summary>
    /// Maps an attribute object identifier to its ATTRTYP representation.
    /// </summary>
    public AttributeType? TranslateToAttributeType(string attributeOid)
    {
        return (AttributeType?)this.Translate(attributeOid);
    }

    /// <summary>
    /// Maps a class object identifier to its ATTRTYP representation.
    /// </summary>
    public ClassType? TranslateToClassType(string classOid)
    {
        return (ClassType?)this.Translate(classOid);
    }

    /// <summary>
    /// Maps an object identifier to its ATTRTYP representation.
    /// </summary>
    /// <remarks>Implements the MakeAttid procedure from https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/6f53317f-2263-48ee-86c1-4580bf97232c</remarks>
    public ATTRTYP? Translate(string oid)
    {
        if (string.IsNullOrEmpty(oid)) throw new ArgumentNullException(nameof(oid));

        // Get the last value in the original OID (the value after the last '.')
        int separatorIndex = oid.LastIndexOf('.');

        if (separatorIndex < 1 || separatorIndex >= oid.Length - 1)
        {
            // '.' was either not found or it is the first or last character
            throw new ArgumentOutOfRangeException(nameof(oid), "The input is not an OID string.");
        }

        string prefix = oid.Substring(0, separatorIndex);

        // Search the prefix table
        bool found = _reverseMap.TryGetValue(prefix, out PrefixIndex upperWord);

        if (!found)
        {
            // OID cannot be translated, as it is missing from the prefix table.
            return null;
        }

        string lastValueString = oid.Substring(separatorIndex + 1);
        uint lastValue = Convert.ToUInt32(lastValueString, NumberFormatInfo.InvariantInfo);

        ATTRTYP lowerWord = lastValue % 0x4000u; // 16384

        if (lastValue >= 0x4000u)
        {
            // Mark it so that it is known to not be the whole lastValue.
            lowerWord += 0x8000u; // 32768
        }

        // Compose the attid
        return upperWord * 0x10000u + lowerWord; // 65536
    }

    public string? Translate(AttributeType encodedOid)
    {
        if (!encodedOid.IsCompressedOid())
        {
            throw new ArgumentOutOfRangeException(nameof(encodedOid));
        }

        return Translate((ATTRTYP)encodedOid);
    }

    public string? Translate(ClassType encodedOid)
    {
        return Translate((ATTRTYP)encodedOid);
    }

    public string? Translate(ATTRTYP encodedOid)
    {
        // Decode Oid from attribute id (:= prefixIndex + suffix)
        const ATTRTYP wordSeparator = 0x10000; // 65536

        // Prefix index is encoded as upper word
        PrefixIndex prefixIndex = (PrefixIndex)(encodedOid / wordSeparator);

        bool prefixKnown = _forwardMap.TryGetValue(prefixIndex, out string prefix);

        if (!prefixKnown)
        {
            // This ATTRTYP value cannot be translated.
            return null;
        }

        // Suffix (last node) is encoded as lower word
        ATTRTYP suffix = encodedOid % wordSeparator;

        if (suffix >= 0x8000) // 32768
        {
            // Remove mark
            suffix += 0x4000; // 16384
        }

        // Combine prefix with suffix into the final OID
        return $"{prefix}.{suffix}";
    }


    public static string Translate(AttributeSyntax encodedOid)
    {
        // This is a static mapping, because the prefix table might not be available, when this is first needed.
        ATTRTYP lastOctet = encodedOid - AttributeSyntax.Undefined;
        return $"2.5.5.{lastOctet}";
    }

    public void Add(PrefixIndex index, string oidPrefix)
    {
        ArgumentNullException.ThrowIfNull(oidPrefix);

        _forwardMap[index] = oidPrefix;
        _reverseMap[oidPrefix] = index;
    }

    public void Add(PrefixIndex index, byte[] oidPrefix)
    {
        ArgumentNullException.ThrowIfNull(oidPrefix);

        string oidString = MakeOidStringFromBytes(oidPrefix);
        _forwardMap[index] = oidString;
        _reverseMap[oidString] = index;
    }

    public void Add(PrefixTable other)
    {
        ArgumentNullException.ThrowIfNull(other);

        // Merge the other prefix table into the current one
        foreach (var kvp in other._forwardMap)
        {
            this.Add(kvp.Key, kvp.Value);
        }
    }

    public void LoadFromBlob(byte[] blob)
    {
        Validator.AssertMinLength(blob, MinBlobLength, nameof(blob));

        using (var stream = new MemoryStream(blob))
        {
            using (var reader = new BinaryReader(stream))
            {
                uint prefixCount = reader.ReadUInt32();
                uint mapSize = reader.ReadUInt32();
                Validator.AssertLength(blob, mapSize, nameof(blob));

                // Read all prefixes, one by one
                for (int i = 0; i < prefixCount; i++)
                {
                    PrefixIndex prefixIndex = reader.ReadUInt16();
                    ushort prefixSize = reader.ReadUInt16();
                    byte[] prefix = reader.ReadBytes(prefixSize);
                    this.Add(prefixIndex, prefix);
                }
            }
        }
    }

    /// <summary>
    /// Indicates if the ATTRTYP maps to an OID via the prefix table.
    /// </summary>
    public static bool IsCompressedOid(AttributeType attrtyp) => attrtyp <= AttributeType.LastCompressedOid;


    /// <see>https://msdn.microsoft.com/en-us/library/cc228445.aspx</see>
    private void AddBuiltinPrefixes()
    {
        // These prefixes are hardcoded in AD, without being present in the map stored in DB.
        this.Add(0, "2.5.4"); // 0x5504
        this.Add(1, "2.5.6"); // 0x5506
        this.Add(2, "1.2.840.113556.1.2"); // 0x2A864886F7140102
        this.Add(3, "1.2.840.113556.1.3"); // 0x2A864886F7140103
        this.Add(4, "2.16.840.1.101.2.2.1"); // 0x6086480165020201
        this.Add(5, "2.16.840.1.101.2.2.3"); // 0x6086480165020203
        this.Add(6, "2.16.840.1.101.2.1.5"); // 0x6086480165020105
        this.Add(7, "2.16.840.1.101.2.1.4"); // 0x6086480165020104
        this.Add(8, "2.5.5"); // 0x5505
        this.Add(9, "1.2.840.113556.1.4"); // 0x2A864886F7140104
        this.Add(10, "1.2.840.113556.1.5"); // 0x2A864886F7140105
        this.Add(11, "1.2.840.113556.1.4.260"); // 0x2A864886F71401048204
        this.Add(12, "1.2.840.113556.1.5.56"); // 0x2A864886F714010538
        this.Add(13, "1.2.840.113556.1.4.262"); // 0x2A864886F71401048206
        this.Add(14, "1.2.840.113556.1.5.57"); // 0x2A864886F714010539
        this.Add(15, "1.2.840.113556.1.4.263"); // 0x2A864886F71401048207
        this.Add(16, "1.2.840.113556.1.5.58"); // 0x2A864886F71401053A
        this.Add(17, "1.2.840.113556.1.5.73"); // 0x2A864886F714010549
        this.Add(18, "1.2.840.113556.1.4.305"); // 0x2A864886F71401048231
        this.Add(19, "0.9.2342.19200300.100"); // 0x0992268993F22C64
        this.Add(20, "2.16.840.1.113730.3"); // 0x6086480186F84203
        this.Add(21, "0.9.2342.19200300.100.1"); // 0x0992268993F22C6401
        this.Add(22, "2.16.840.1.113730.3.1"); // 0x6086480186F8420301
        this.Add(23, "1.2.840.113556.1.5.7000"); // 0x2A864886F7140105B658
        this.Add(24, "2.5.21"); // 0x5515
        this.Add(25, "2.5.18"); // 0x5512
        this.Add(26, "2.5.20"); // 0x5514
        this.Add(27, "1.3.6.1.4.1.1466.101.119"); // 0x2B060104018B3A6577
        this.Add(28, "2.16.840.1.113730.3.2"); // 0x6086480186F8420302
        this.Add(29, "1.3.6.1.4.1.250.1"); // 0x2B06010401817A01
        this.Add(30, "1.2.840.113549.1.9"); // 0x2A864886F70D0109
        this.Add(31, "0.9.2342.19200300.100.4"); // 0x0992268993F22C6404
        this.Add(32, "1.2.840.113556.1.6.23"); // 0x2A864886F714010617
        this.Add(33, "1.2.840.113556.1.6.18.1"); // 0x2A864886F71401061201
        this.Add(34, "1.2.840.113556.1.6.18.2"); // 0x2A864886F71401061202
        this.Add(35, "1.2.840.113556.1.6.13.3"); // 0x2A864886F71401060D03
        this.Add(36, "1.2.840.113556.1.6.13.4"); // 0x2A864886F71401060D04
        this.Add(37, "1.3.6.1.1.1.1"); // 0x2B0601010101
        this.Add(38, "1.3.6.1.1.1.2"); // 0x2B0601010102
    }

    /*
     * The following function has been copied from Org.BouncyCastle.Asn1.DerObjectIdentifier.
     * We could not use it directly, because it is private and we do not want to use Reflection that much.
     * Project site: http://www.bouncycastle.org/csharp/
     */
    /// <summary>
    /// Converts binary ASN1 encoded OID into string.
    /// </summary>
    /// <param name="bytes">ASN1 encoded OID</param>
    /// <returns>OID string</returns>
    private static string MakeOidStringFromBytes(byte[] bytes)
    {
        StringBuilder objId = new StringBuilder();
        long value = 0;
        BigInteger bigValue = 0;
        bool first = true;

        for (int i = 0; i != bytes.Length; i++)
        {
            byte currentByte = bytes[i];

            if (value <= LongLimit)
            {
                value += (currentByte & 0x7f);
                if ((currentByte & 0x80) == 0) // end of number reached
                {
                    if (first)
                    {
                        if (value < 40)
                        {
                            objId.Append('0');
                        }
                        else if (value < 80)
                        {
                            objId.Append('1');
                            value -= 40;
                        }
                        else
                        {
                            objId.Append('2');
                            value -= 80;
                        }
                        first = false;
                    }

                    objId.Append('.');
                    objId.Append(value);
                    value = 0;
                }
                else
                {
                    value <<= 7;
                }
            }
            else
            {
                if (bigValue.IsZero)
                {
                    bigValue = value;
                }
                bigValue = bigValue | (currentByte & 0x7f);
                if ((currentByte & 0x80) == 0)
                {
                    if (first)
                    {
                        objId.Append('2');
                        bigValue = bigValue - 80;
                        first = false;
                    }

                    objId.Append('.');
                    objId.Append(bigValue);
                    bigValue = 0;
                    value = 0;
                }
                else
                {
                    bigValue = bigValue << 7;
                }
            }
        }

        return objId.ToString();
    }
}
