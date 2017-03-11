using DSInternals.Common;
using DSInternals.Common.Data;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Text;

namespace DSInternals.DataStore
{
    public class PrefixMap
    {
        private const int MinBlobLength = 2 * sizeof(uint);
        private const long LongLimit = (long.MaxValue >> 7) - 0x7f;
        private const string AttributeSyntaxOidFormat = "2.5.5.{0}";

        private IDictionary<ushort, string> prefixTable;

        public PrefixMap(byte[] blob = null)
        {
            // Add hardcoded prefixes
            prefixTable = new SortedDictionary<ushort, string>();
            this.AddBuiltinPrefixes();

            // Add user prefixes, if any
            if(blob != null)
            {
                this.InitFromBlob(blob);
            }
        }

        public bool ContainsPrefix(ushort prefixIndex)
        {
            return this.prefixTable.ContainsKey(prefixIndex);
        }

        public string this[ushort prefixIndex]
        {
            get
            {
                return this.prefixTable[prefixIndex];
            }
        }

        public int Count
        {
            get
            {
                return this.prefixTable.Count;
            }
        }

        public string Translate(uint attributeId)
        {
            // Decode Oid from attribute id (:= prefixIndex + suffix)
            const int wordSeparator = 65536;

            // Prefix index is encoded as upper word
            ushort prefixIndex = (ushort) (attributeId / wordSeparator);
            bool prefixKnown = this.ContainsPrefix(prefixIndex);
            if(!prefixKnown)
            {
                throw new ArgumentOutOfRangeException("attributeId", "Unknown attribute prefix.");
            }
            string prefix = this[prefixIndex];

            // Suffix (last node) is encoded as lower word
            uint lowerWord = attributeId % wordSeparator;
            if (lowerWord >= 32768)
            {
                // Remove mark
                lowerWord += 16384;
            }
            uint suffix = lowerWord;

            // Combine prefix with suffix into the final OID
            string fullOid = String.Format("{0}.{1}", prefix, suffix);
            return fullOid;
        }

        public static string GetAttributeSyntaxOid(AttributeSyntax syntax)
        {
            // This is a static mapping, because the prefix table might not be available, when this is first needed.
            int lastOctet = syntax - AttributeSyntax.Undefined;
            return String.Format(AttributeSyntaxOidFormat, lastOctet);
        }

        private void InitFromBlob(byte[] blob)
        {
            Validator.AssertMinLength(blob, MinBlobLength, "blob");
            using (var stream = new MemoryStream(blob))
            {
                using (var reader = new BinaryReader(stream))
                {
                    uint prefixCount = reader.ReadUInt32();
                    uint mapSize = reader.ReadUInt32();
                    Validator.AssertLength(blob, mapSize, "blob");
                    // Read all prefixes, one by one
                    for (int i = 0; i < prefixCount; i++)
                    {
                        ushort prefixIndex = reader.ReadUInt16();
                        ushort prefixSize = reader.ReadUInt16();
                        byte[] prefix = reader.ReadBytes(prefixSize);
                        // Convert BER prefix to OID string
                        string prefixString = MakeOidStringFromBytes(prefix);
                        this.prefixTable.Add(prefixIndex, prefixString);
                    }
                }
            }
        }

        /// <see>https://msdn.microsoft.com/en-us/library/cc228445.aspx</see>
        private void AddBuiltinPrefixes()
        {
            // These prefixes are hardcoded in AD, without being present in the map stored in DB.
            this.AddBuiltinPrefix(0, "5504"); // 2.5.4
            this.AddBuiltinPrefix(1, "5506"); // 2.5.6
            this.AddBuiltinPrefix(2, "2A864886F7140102"); // 1.2.840.113556.1.2
            this.AddBuiltinPrefix(3, "2A864886F7140103"); // 1.2.840.113556.1.3
            this.AddBuiltinPrefix(4, "6086480165020201"); // 2.16.840.1.101.2.2.1
            this.AddBuiltinPrefix(5, "6086480165020203"); // 2.16.840.1.101.2.2.3
            this.AddBuiltinPrefix(6, "6086480165020105"); // 2.16.840.1.101.2.2.5
            this.AddBuiltinPrefix(7, "6086480165020104"); // 2.16.840.1.101.2.2.4
            this.AddBuiltinPrefix(8, "5505"); // 2.5.5
            this.AddBuiltinPrefix(9, "2A864886F7140104"); // 1.2.840.113556.1.4
            this.AddBuiltinPrefix(10, "2A864886F7140105"); // 1.2.840.113556.1.5
            this.AddBuiltinPrefix(11, "2A864886F71401048204"); // 1.2.840.113556.1.4.260
            this.AddBuiltinPrefix(12, "2A864886F714010538"); // 1.2.840.113556.1.5.56
            this.AddBuiltinPrefix(13, "2A864886F71401048206"); // 1.2.840.113556.1.4.262
            this.AddBuiltinPrefix(14, "2A864886F714010539"); // 1.2.840.113556.1.5.57
            this.AddBuiltinPrefix(15, "2A864886F71401048207"); // 1.2.840.113556.1.4.263
            this.AddBuiltinPrefix(16, "2A864886F71401053A"); // 1.2.840.113556.1.5.58
            this.AddBuiltinPrefix(17, "2A864886F714010549"); // 1.2.840.113556.1.5.73
            this.AddBuiltinPrefix(18, "2A864886F71401048231"); // 1.2.840.113556.1.4.305
            this.AddBuiltinPrefix(19, "0992268993F22C64"); // 0.9.2342.19200300.100
            this.AddBuiltinPrefix(20, "6086480186F84203"); // 2.16.840.1.113730.3
            this.AddBuiltinPrefix(21, "0992268993F22C6401"); // 0.9.234219200300.100.1
            this.AddBuiltinPrefix(22, "6086480186F8420301"); // 2.16.840.1.113730.3.1
            this.AddBuiltinPrefix(23, "2A864886F7140105B658"); // 1.2.840.113556.1.5.7000
            this.AddBuiltinPrefix(24, "5515"); // 2.5.21
            this.AddBuiltinPrefix(25, "5512"); // 2.5.18
            this.AddBuiltinPrefix(26, "5514"); // 2.5.20
            this.AddBuiltinPrefix(27, "2B060104018B3A6577"); // 1.3.6.1.4.1.1466.101.119
            this.AddBuiltinPrefix(28, "6086480186F8420302"); // 2.16.840.1.113730.3.2
            this.AddBuiltinPrefix(29, "2B06010401817A01"); // 1.3.6.1.4.1.250.1
            this.AddBuiltinPrefix(30, "2A864886F70D0109"); // 1.2.840.113549.1.9
            this.AddBuiltinPrefix(31, "0992268993F22C6404"); // 0.9.2342.19200300.100.4
            this.AddBuiltinPrefix(32, "2A864886F714010617"); // 1.2.840.113556.1.6.23
            this.AddBuiltinPrefix(33, "2A864886F71401061201"); // 1.2.840.113556.1.6.18.1
            this.AddBuiltinPrefix(34, "2A864886F71401061202"); // 1.2.840.113556.1.6.18.2
            this.AddBuiltinPrefix(35, "2A864886F71401060D03"); // 1.2.840.113556.1.6.13.3
            this.AddBuiltinPrefix(36, "2A864886F71401060D04"); // 1.2.840.113556.1.6.13.4
            this.AddBuiltinPrefix(37, "2B0601010101"); // 1.3.6.1.1.1.1
            this.AddBuiltinPrefix(38, "2B0601010102"); // 1.3.6.1.1.1.2
        }

        private void AddBuiltinPrefix(ushort index, string encodedPrefix)
        {
            // Convert the prefix to binary
            byte[] binaryPrefix = encodedPrefix.HexToBinary();
            // Now convert binary OID to string OID
            string prefix = MakeOidStringFromBytes(binaryPrefix);
            this.prefixTable.Add(index, prefix);
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
                int currentByte = bytes[i];

                if (value <= LongLimit)
                {
                    value += (currentByte & 0x7f);
                    if ((currentByte & 0x80) == 0)             // end of number reached
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
}
