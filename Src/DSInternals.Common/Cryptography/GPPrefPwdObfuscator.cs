using DSInternals.Common;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    public static class GPPrefPwdObfuscator
    {
        /// <summary>
        /// All passwords are encrypted using this 32-byte AES key:
        /// </summary>
        private static readonly byte[] AesKey = {   0x4e, 0x99, 0x06, 0xe8, 0xfc, 0xb6, 0x6c, 0xc9,
                                                    0xfa, 0xf4, 0x93, 0x10, 0x62, 0x0f, 0xfe, 0xe8,
                                                    0xf4, 0x96, 0xe8, 0x06, 0xcc, 0x05, 0x79, 0x90,
                                                    0x20, 0x9b, 0x09, 0xa4, 0x33, 0xb6, 0x6c, 0x1b };
        public static string Decrypt(string input)
        {
            Validator.AssertNotNullOrWhiteSpace(input, "input");
            // Add Base64 padding
            string paddedInput = RestoreBase64Padding(input);
            // Decode from Base64
            byte[] encryptedBytes = Convert.FromBase64String(paddedInput);
            // Init AES decryptor
            var aes = new AesCryptoServiceProvider();
            // Set nulled IV
            aes.IV = new byte[aes.IV.Length];
            aes.Key = AesKey;
            ICryptoTransform aesDecryptor = aes.CreateDecryptor();
            // Decrypt
            byte[] decryptedBytes = aesDecryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            // Decode from UTF-16
            string plainText = UnicodeEncoding.Unicode.GetString(decryptedBytes);
            return plainText;
        }

        public static string Encrypt(SecureString input)
        {
            Validator.AssertNotNull(input, "input");
            if(input.Length == 0)
            {
                return String.Empty;
            }
            byte[] bytes = input.ToByteArray();
            try
            {
                // Encrypt
                var aes = new AesCryptoServiceProvider();
                // Set nulled IV
                aes.IV = new byte[aes.IV.Length];
                aes.Key = AesKey;
                ICryptoTransform aesEncryptor = aes.CreateEncryptor();
                // Decrypt
                byte[] encryptedBytes = aesEncryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                // Convert to Base64
                string base64 = Convert.ToBase64String(encryptedBytes);
                // Remove Base64 padding
                string result = RemoveBase64Padding(base64);
                return result;
            }
            finally
            {
                bytes.ZeroFill();
            }
        }

        private static string RestoreBase64Padding(string input)
        {
            int length = input.Length;
            int lengthMod4 = length % 4;
            string paddedInput;
            switch (lengthMod4)
            {
                case 0:
                    paddedInput = input;
                    break;
                case 1:
                    // TODO: Test this branch
                    paddedInput = input.Substring(0, length - 1);
                    break;
                case 2:
                case 3:
                    int padToLength = length + 4 - lengthMod4;
                    paddedInput = input.PadRight(padToLength, '=');
                    break;
                default:
                    // This should never happen.
                    throw new ApplicationException();
            }
            return paddedInput;
        }

        private static string RemoveBase64Padding(string input)
        {
            return input.TrimEnd('=');
        }
    }
}
