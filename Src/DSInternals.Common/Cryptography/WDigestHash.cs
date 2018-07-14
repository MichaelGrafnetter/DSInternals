namespace DSInternals.Common.Cryptography
{
    using DSInternals.Common;
    using System;
    using System.Collections.Generic;
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

        private const string MagicRealm = "Digest";

        // All strings are converted to ISO Latin I code page prior to the hashing
        private const string StringEncodingName = "ISO-8859-1";
        private static readonly Encoding StringEncoder = Encoding.GetEncoding(StringEncodingName);

        /// <summary>
        /// Calculates WDigest hashes of a password.
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
            Validator.AssertNotNullOrWhiteSpace(netBiosDomainName, "netBiosDomainName");
            Validator.AssertNotNullOrWhiteSpace(dnsDomainName, "dnsDomainName");
            
            // Note that a user does not need to have a UPN.
            if(String.IsNullOrEmpty(userPrincipalName))
            {
                // Construct the UPN as samAccountName@dnsDomainName
                userPrincipalName = String.Format(@"{0}@{1}", samAccountName, dnsDomainName);
            }

            // Construct the pre-Windows 2000 logon name as netBiosDomainName\samAccountName
            string logonName = String.Format(@"{0}\{1}", netBiosDomainName, samAccountName);

            // List of the resulting 29 hashes
            var result = new List<byte[]>(HashCount);

            using (var md5 = new MD5Cng())
            {
                // Hash1: MD5(sAMAccountName, NETBIOSDomainName, password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName, password)));

                // Hash2: MD5(LOWER(sAMAccountName), LOWER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToLower(), netBiosDomainName.ToLower(), password)));

                // Hash3: MD5(UPPER(sAMAccountName), UPPER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToUpper(), netBiosDomainName.ToUpper(), password)));

                // Hash4: MD5(sAMAccountName, UPPER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName.ToUpper(), password)));

                // Hash5: MD5(sAMAccountName, LOWER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, netBiosDomainName.ToLower(), password)));

                // Hash6: MD5(UPPER(sAMAccountName), LOWER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToUpper(), netBiosDomainName.ToLower(), password)));

                // Hash7: MD5(LOWER(sAMAccountName), UPPER(NETBIOSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToLower(), netBiosDomainName.ToUpper(), password)));

                // Hash8: MD5(sAMAccountName, DNSDomainName, password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, dnsDomainName, password)));

                // Hash9: MD5(LOWER(sAMAccountName), LOWER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToLower(), dnsDomainName.ToLower(), password)));

                // Hash10: MD5(UPPER(sAMAccountName), UPPER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToUpper(), dnsDomainName.ToUpper(), password)));

                // Hash11: MD5(sAMAccountName, UPPER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, dnsDomainName.ToUpper(), password)));

                // Hash12: MD5(sAMAccountName, LOWER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, dnsDomainName.ToLower(), password)));

                // Hash13: MD5(UPPER(sAMAccountName), LOWER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToUpper(), dnsDomainName.ToLower(), password)));

                // Hash14: MD5(LOWER(sAMAccountName), UPPER(DNSDomainName), password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToLower(), dnsDomainName.ToUpper(), password)));

                // Hash15: MD5(userPrincipalName, password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName, String.Empty, password)));

                // Hash16: MD5(LOWER(userPrincipalName), password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName.ToLower(), String.Empty, password)));

                // Hash17: MD5(UPPER(userPrincipalName), password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName.ToUpper(), String.Empty, password)));

                // Hash18: MD5(NETBIOSDomainName\sAMAccountName, password)
                result.Add(md5.ComputeHash(GetBytes(logonName, password)));

                // Hash19: MD5(LOWER(NETBIOSDomainName\sAMAccountName), password)
                result.Add(md5.ComputeHash(GetBytes(logonName.ToLower(), password)));

                // Hash20: MD5(UPPER(NETBIOSDomainName\sAMAccountName), password)
                result.Add(md5.ComputeHash(GetBytes(logonName.ToUpper(), password)));

                // Hash21: MD5(sAMAccountName, "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName, MagicRealm, password)));

                // Hash22: MD5(LOWER(sAMAccountName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToLower(), MagicRealm, password)));

                // Hash23: MD5(UPPER(sAMAccountName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(samAccountName.ToUpper(), MagicRealm, password)));

                // Hash24: MD5(userPrincipalName, "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName, MagicRealm, password)));

                // Hash25: MD5(LOWER(userPrincipalName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName.ToLower(), MagicRealm, password)));

                // Hash26: MD5(UPPER(userPrincipalName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(userPrincipalName.ToUpper(), MagicRealm, password)));

                // Hash27: MD5(NETBIOSDomainName\sAMAccountName, "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(logonName, MagicRealm, password)));

                // Hash28: MD5(LOWER(NETBIOSDomainName\sAMAccountName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(logonName.ToLower(), MagicRealm, password)));

                // Hash29: MD5(UPPER(NETBIOSDomainName\sAMAccountName), "Digest", password)
                result.Add(md5.ComputeHash(GetBytes(logonName.ToUpper(), MagicRealm, password)));
            }
            return result.ToArray();
        }

        private static byte[] GetBytes(string userName, SecureString password)
        {
            return GetBytes(userName, String.Empty, password);
        }

        private static byte[] GetBytes(string userName, string realm, SecureString password)
        {
            var concatenatedString = String.Format("{0}:{1}:{2}", userName, realm, password.ToUnicodeString());
            return StringEncoder.GetBytes(concatenatedString);
        }
    }
}