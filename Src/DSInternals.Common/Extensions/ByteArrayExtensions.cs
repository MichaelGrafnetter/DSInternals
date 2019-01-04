namespace DSInternals.Common
{
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Text;

    public static class ByteArrayExtensions
    {
        private const string HexDigits = "0123456789ABCDEF";
        private static readonly byte[] HexValues = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        public static void ZeroFill(this byte[] array)
        {
            Array.Clear(array, 0, array.Length);
        }

        public static byte[] HexToBinary(this string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return null;
            }
            // Test that input is a hex string
            Validator.AssertIsHex(hex, "hex");

            // Finally, do the conversion:
            byte[] bytes = new byte[hex.Length / 2];
            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
            {
                bytes[x] = (byte)(HexValues[Char.ToUpper(hex[i + 0]) - '0'] << 4 |
                                  HexValues[Char.ToUpper(hex[i + 1]) - '0']);
            }

            return bytes;
        }

        public static string ToHex(this byte[] bytes, bool caps = false)
        {
            if (bytes == null)
            {
                return null;
            }

            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach(byte currentByte in bytes)
            {
                hex.Append(HexDigits[(int)(currentByte >> 4)]);
                hex.Append(HexDigits[(int)(currentByte & 0xF)]);
            }

            if (caps)
            {
                // Uppercase string is calculated by default
                return hex.ToString();
            }
            else
            {
                return hex.ToString().ToLower();
            }
        }

        public static string ReadWString(this byte[] buffer, int startIndex)
        {
            Validator.AssertNotNull(buffer, "buffer");
            // TODO: Assert startIndex > 0
            int maxLength = buffer.Length - startIndex;
            var sb = new StringBuilder(maxLength);
            for (int i = startIndex; i < buffer.Length; i += UnicodeEncoding.CharSize)
            {
                char c = BitConverter.ToChar(buffer, i);
                if (c == Char.MinValue)
                {
                    // End of string reached
                    return sb.ToString();
                }
                sb.Append(c);
            }
            // If we reached this point, the \0 char has not been found, so throw an exception.
            // TODO: Add a reasonable exception message
            throw new ArgumentException();
        }

        public static void SwapBytes(this byte[] bytes, int index1, int index2)
        {
            byte temp = bytes[index1];
            bytes[index1] = bytes[index2];
            bytes[index2] = temp;
        }

        /// <summary>
        /// Encodes an integer into a 4-byte array, in big endian.
        /// </summary>
        /// <param name="number">The integer to encode.</param>
        /// <returns>Array of bytes, in big endian order.</returns>
        public static byte[] GetBigEndianBytes(this uint number)
        {
            byte[] bytes = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian)
            {
                bytes.SwapBytes(0, 3);
                bytes.SwapBytes(1, 2);
            }
            return bytes;
        }

        public static SecurityIdentifier ToSecurityIdentifier(this byte[] binarySid, bool bigEndianRid = false)
        {
            if(binarySid == null)
            {
                return null;
            }
            byte[] output = binarySid;
            if (bigEndianRid)
            {
                // Clone the binary SID so we do not perform byte spapping on the original value.
                byte[] binarySidCopy = (byte[])binarySid.Clone();
                int lastByteIndex = binarySidCopy.Length -1;
                // Convert RID from big endian to little endian (Reverse the order of the last 4 bytes)
                binarySidCopy.SwapBytes(lastByteIndex - 3, lastByteIndex);
                binarySidCopy.SwapBytes(lastByteIndex - 2, lastByteIndex - 1);
                output = binarySidCopy;
            }
            return new SecurityIdentifier(output, 0);
        }

        public static byte[] Cut(this byte[] blob, int offset)
        {
            Validator.AssertNotNull(blob, "blob");
            return blob.Cut(offset, blob.Length - offset);
        }

        public static byte[] Cut(this byte[] blob, int offset, int count)
        {
            Validator.AssertNotNull(blob, "blob");
            Validator.AssertMinLength(blob, offset + count, "blob");
            // TODO: Check that offset and count are positive using Validator
            byte[] result = new byte[count];
            Buffer.BlockCopy((Array)blob, offset, (Array)result, 0, count);
            return result;
        }

        public static byte[] ReadToEnd(this MemoryStream stream)
        {
            long remainingBytes = stream.Length - stream.Position;
            if(remainingBytes > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("stream");
            }
            byte[] buffer = new byte[remainingBytes];
            stream.Read(buffer, 0, (int)remainingBytes);
            return buffer;
        }
    }
}
