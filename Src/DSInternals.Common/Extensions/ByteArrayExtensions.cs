namespace DSInternals.Common
{
    using CryptSharp.Utility;
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class ByteArrayExtensions
    {
        private const string hexPattern = "^[0-9a-fA-F]+$";

        public static void ZeroFill(this byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }

        public static byte[] HexToBinary(this string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return null;
            }

            // CryptSharp does not validate if the input, so we need to do it ourselves.
            bool isHexString = Regex.IsMatch(hex, hexPattern);
            if(!isHexString)
            {
                // TODO: Extract string as resource.
                throw new ArgumentException("Parameter is not a hexadecimal string.");
            }

            // Finally, do the conversion:
            return Base16Encoding.Hex.GetBytes(hex);
        }

        public static string ToHex(this byte[] bytes, bool caps = false)
        {
            if (bytes == null)
            {
                return null;
            }
            string hex = Base16Encoding.Hex.GetString(bytes);
            if (caps)
            {
                // Base16Encoding returns uppercase string by default
                return hex;
            }
            else
            {
                return hex.ToLower();
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