namespace DSInternals.Common.Cryptography
{
    using DSInternals.Common;
    using System;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Calculates hash forms that are used in the digest authentication protocols.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245680.aspx</see>
    public static class WDigestHash
    {
        /// <summary>
        /// The size, in bytes, of the computed hash code.
        /// </summary>
        public const int HashSize = 16;

        /// <summary>
        /// Count of MD5 hashes that are calculated.
        /// </summary>
        public const int HashCount = 29;

        /// <summary>
        /// This string is used instead of the realm name when calculating some of the hashes.
        /// </summary>
        private const string MagicRealm = "Digest";

        private const byte CurrentVersion = 1;

        private const byte DefaultReserved1Value = (byte)'1';

        /// <summary>
        /// Calculates WDigest hashes of a password, as used by AD DS.
        /// </summary>
        /// <param name="password">User's password.</param>
        /// <param name="userPrincipalName">The userPrincipalName attribute value.</param>
        /// <param name="samAccountName">The sAMAccountName attribute value.</param>
        /// <param name="netBiosDomainName">The name attribute of the account domain object.</param>
        /// <param name="dnsDomainName">The fully qualified domain name (FQDN) of the domain.</param>
        /// <returns>29 MD5 hashes.</returns>
        /// <remarks>SecureString is copied into managed memory while calculating the hashes, which is not the best way to deal with it.</remarks>
        public static byte[][] ComputeHash(SecureString password, string userPrincipalName, string samAccountName, string netBiosDomainName, string dnsDomainName)
        {
            // Validate the input
            Validator.AssertNotNull(password, "password");
            Validator.AssertNotNullOrWhiteSpace(samAccountName, "samAccountName");
            Validator.AssertNotNull(netBiosDomainName, "netBiosDomainName");
            Validator.AssertNotNull(dnsDomainName, "dnsDomainName");
            
            // Note that a user does not need to have a UPN.
            if(userPrincipalName == null)
            {
                // Construct the UPN as samAccountName@dnsDomainName
                userPrincipalName = String.Format(@"{0}@{1}", samAccountName, dnsDomainName);
            }

            // Construct the pre-Windows 2000 logon name as netBiosDomainName\samAccountName
            string logonName = String.Format(@"{0}\{1}", netBiosDomainName, samAccountName);

            // Array of the resulting 29 hashes
            byte[][] result = new byte[HashCount][];
            
            using (var md5 = MD5.Create())
            {
                // Hash1: MD5(sAMAccountName, NETBIOSDomainName, password)
                result[0] = md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName, password));

                // Hash2: MD5(LOWER(sAMAccountName), LOWER(NETBIOSDomainName), password)
                result[1] = md5.ComputeHash(GetBytes(samAccountName.ToLower(), netBiosDomainName.ToLower(), password));

                // Hash3: MD5(UPPER(sAMAccountName), UPPER(NETBIOSDomainName), password)
                result[2] = md5.ComputeHash(GetBytes(samAccountName.ToUpper(), netBiosDomainName.ToUpper(), password));

                // Hash4: MD5(sAMAccountName, UPPER(NETBIOSDomainName), password)
                result[3] = md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName.ToUpper(), password));

                // Hash5: MD5(sAMAccountName, LOWER(NETBIOSDomainName), password)
                result[4] = md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName.ToLower(), password));

                // Hash6: MD5(UPPER(sAMAccountName), LOWER(NETBIOSDomainName), password)
                result[5] = md5.ComputeHash(GetBytes(samAccountName.ToUpper(), netBiosDomainName.ToLower(), password));

                // Hash7: MD5(LOWER(sAMAccountName), UPPER(NETBIOSDomainName), password)
                result[6] = md5.ComputeHash(GetBytes(samAccountName.ToLower(), netBiosDomainName.ToUpper(), password));

                // Hash8: MD5(sAMAccountName, DNSDomainName, password)
                result[7] = md5.ComputeHash(GetBytes(samAccountName, dnsDomainName, password));

                // Hash9: MD5(LOWER(sAMAccountName), LOWER(DNSDomainName), password)
                result[8] = md5.ComputeHash(GetBytes(samAccountName.ToLower(), dnsDomainName.ToLower(), password));

                // Hash10: MD5(UPPER(sAMAccountName), UPPER(DNSDomainName), password)
                result[9] = md5.ComputeHash(GetBytes(samAccountName.ToUpper(), dnsDomainName.ToUpper(), password));

                // Hash11: MD5(sAMAccountName, UPPER(DNSDomainName), password)
                result[10] = md5.ComputeHash(GetBytes(samAccountName, dnsDomainName.ToUpper(), password));

                // Hash12: MD5(sAMAccountName, LOWER(DNSDomainName), password)
                result[11] = md5.ComputeHash(GetBytes(samAccountName, dnsDomainName.ToLower(), password));

                // Hash13: MD5(UPPER(sAMAccountName), LOWER(DNSDomainName), password)
                result[12] = md5.ComputeHash(GetBytes(samAccountName.ToUpper(), dnsDomainName.ToLower(), password));

                // Hash14: MD5(LOWER(sAMAccountName), UPPER(DNSDomainName), password)
                result[13] = md5.ComputeHash(GetBytes(samAccountName.ToLower(), dnsDomainName.ToUpper(), password));

                // Hash15: MD5(userPrincipalName, password)
                result[14] = md5.ComputeHash(GetBytes(userPrincipalName, String.Empty, password));

                // Hash16: MD5(LOWER(userPrincipalName), password)
                result[15] = md5.ComputeHash(GetBytes(userPrincipalName.ToLower(), String.Empty, password));

                // Hash17: MD5(UPPER(userPrincipalName), password)
                result[16] = md5.ComputeHash(GetBytes(userPrincipalName.ToUpper(), String.Empty, password));

                // Hash18: MD5(NETBIOSDomainName\sAMAccountName, password)
                result[17] = md5.ComputeHash(GetBytes(logonName, password));

                // Hash19: MD5(LOWER(NETBIOSDomainName\sAMAccountName), password)
                result[18] = md5.ComputeHash(GetBytes(logonName.ToLower(), password));

                // Hash20: MD5(UPPER(NETBIOSDomainName\sAMAccountName), password)
                result[19] = md5.ComputeHash(GetBytes(logonName.ToUpper(), password));

                // Hash21: MD5(sAMAccountName, "Digest", password)
                result[20] = md5.ComputeHash(GetBytes(samAccountName, MagicRealm, password));

                // Hash22: MD5(LOWER(sAMAccountName), "Digest", password)
                result[21] = md5.ComputeHash(GetBytes(samAccountName.ToLower(), MagicRealm, password));

                // Hash23: MD5(UPPER(sAMAccountName), "Digest", password)
                result[22] = md5.ComputeHash(GetBytes(samAccountName.ToUpper(), MagicRealm, password));

                // Hash24: MD5(userPrincipalName, "Digest", password)
                result[23] = md5.ComputeHash(GetBytes(userPrincipalName, MagicRealm, password));

                // Hash25: MD5(LOWER(userPrincipalName), "Digest", password)
                result[24] = md5.ComputeHash(GetBytes(userPrincipalName.ToLower(), MagicRealm, password));

                // Hash26: MD5(UPPER(userPrincipalName), "Digest", password)
                result[25] = md5.ComputeHash(GetBytes(userPrincipalName.ToUpper(), MagicRealm, password));

                // Hash27: MD5(NETBIOSDomainName\sAMAccountName, "Digest", password)
                result[26] = md5.ComputeHash(GetBytes(logonName, MagicRealm, password));

                // Hash28: MD5(LOWER(NETBIOSDomainName\sAMAccountName), "Digest", password)
                result[27] = md5.ComputeHash(GetBytes(logonName.ToLower(), MagicRealm, password));

                // Hash29: MD5(UPPER(NETBIOSDomainName\sAMAccountName), "Digest", password)
                result[28] = md5.ComputeHash(GetBytes(logonName.ToUpper(), MagicRealm, password));
            }
            return result;
        }

        /// <summary>
        /// Calculates WDigest hashes of a password, as used by AD LDS.
        /// </summary>
        /// <param name="password">Account's password.</param>
        /// <param name="accountDN">Distinguished name of the account.</param>
        /// <param name="namingContext">Distinguished name of the account's naming context.</param>
        /// <returns>29 MD5 hashes.</returns>
        /// <remarks>SecureString is copied into managed memory while calculating the hashes, which is not the best way to deal with it.</remarks>
        public static byte[][] ComputeHash(SecureString password, string accountDN, string namingContext)
        {
            return ComputeHash(password, String.Empty, accountDN, String.Empty, namingContext);
        }

        /// <summary>
        /// Parses the WDIGEST_CREDENTIALS structure within the supplementalCredentials attribute.
        /// </summary>
        /// <see>https://msdn.microsoft.com/en-us/library/cc245502.aspx</see>
        public static byte[][] Parse(byte[] blob)
        {
            using (var stream = new MemoryStream(blob))
            {
                using (var reader = new BinaryReader(stream))
                {
                    // This value MUST be ignored by the recipient and MAY<22> be set to arbitrary values upon an update to the decryptedSecret attribute.
                    byte reserved1 = reader.ReadByte();

                    // This value MUST be ignored by the recipient and MUST be set to zero.
                    byte reserved2 = reader.ReadByte();

                    // This value MUST be set to 1.
                    byte version = reader.ReadByte();

                    // This value MUST be set to 29 because there are 29 hashes in the array.
                    byte numberOfHashes = reader.ReadByte();

                    // This value MUST be ignored by the recipient and MUST be set to zero. 
                    byte[] reserved3 = reader.ReadBytes(12);

                    // Process hashes:
                    byte[][] hashes = new byte[numberOfHashes][];
                    for (int i = 0; i < numberOfHashes; i++)
                    {
                        hashes[i] = reader.ReadBytes(WDigestHash.HashSize);
                    }

                    return hashes;
                }
            }
        }

        /// <summary>
        /// Creates the WDIGEST_CREDENTIALS structure for use within the supplementalCredentials attribute.
        /// </summary>
        public static byte[] Encode(byte[][] hashes)
        {
            Validator.AssertNotNull(hashes, "hashes");

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Reserved1(1 byte): This value MUST be ignored by the recipient and MAY<23 > be set to arbitrary values upon an update to the supplementalCredentials attribute.
                    writer.Write(DefaultReserved1Value);

                    // Reserved2(1 byte): This value MUST be ignored by the recipient and MUST be set to zero.
                    writer.Write(Byte.MinValue);

                    // Version(1 byte): This value MUST be set to 1.
                    writer.Write(CurrentVersion);

                    // NumberOfHashes(1 byte): This value MUST be set to 29 because there are 29 hashes in the array.
                    writer.Write((byte)hashes.Length);

                    // Reserved3(12 bytes): This value MUST be ignored by the recipient and MUST be set to zero.
                    writer.Write(UInt64.MinValue);
                    writer.Write(UInt32.MinValue);

                    foreach(byte[] hash in hashes)
                    {
                        // HashN (16 bytes)
                        writer.Write(hash);
                    }

                    return stream.ToArray();
                }
            }
        }

        private static byte[] GetBytes(string userName, SecureString password)
        {
            return GetBytes(userName, String.Empty, password);
        }

        private static byte[] GetBytes(string userName, string realm, SecureString password)
        {
            var concatenatedString = String.Join(":", userName, realm, password.ToUnicodeString());
            // Although the documentation says that strings are converted to ISO Latin I code page prior to the hashing, the AD implementation actually uses UTF-8 instead.
            return Encoding.UTF8.GetBytes(concatenatedString);
        }
    }
}
