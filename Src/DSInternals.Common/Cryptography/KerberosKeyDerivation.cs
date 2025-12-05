using System.Security;
using System.Text;
using DSInternals.Common.Data;
using DSInternals.Common.Interop;

namespace DSInternals.Common.Cryptography
{

    /// <summary>
    /// Implements Kerberos Key Derivation Functions
    /// </summary>
    public static class KerberosKeyDerivation
    {
        public const int DefaultIterationCount = 4096;

        /// <summary>
        /// Derives a Kerberos key from a password.
        /// </summary>
        /// <see>https://tools.ietf.org/html/rfc3961</see>
        public static byte[] DeriveKey(KerberosKeyType type, SecureString password, string salt, int iterations = DefaultIterationCount)
        {
            Validator.AssertNotNull(password, nameof(password));
            Validator.AssertNotNull(salt, nameof(salt));

            KerberosCryptoSystem cryptoSystem;
            var status = NativeMethods.CDLocateCSystem(type, out cryptoSystem);

            if (status != NtStatus.Success)
            {
                // On the latest Windows versions, some older KDFs (like DES) are no longer supported natively.
                // The failure code is typically 0x80080341, which is undocumented.
                throw new NotSupportedException("The requested key derivation function is not supported by the current OS.");
            }

            switch(type)
            {
                case KerberosKeyType.DES_CBC_MD5:
                case KerberosKeyType.DES_CBC_CRC:
                    // Some older native implementations (including DES) ignore the salt and expect it to be already appended to the password.
                    password = password.Copy();
                    password.Append(salt);
                    break;
            }

            byte[] key;
            status = cryptoSystem.DeriveKey(password, salt, iterations, out key);
            Validator.AssertSuccess(status);

            return key;
        }

        /// <summary>
        /// Derives a Kerberos key from a password.
        /// </summary>
        /// <see>https://tools.ietf.org/html/rfc3961</see>
        public static byte[] DeriveKey(KerberosKeyType type, ReadOnlyMemory<byte> password, string salt, int iterations = DefaultIterationCount)
        {
            if (password.IsEmpty)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            KerberosCryptoSystem cryptoSystem;
            var status = NativeMethods.CDLocateCSystem(type, out cryptoSystem);
            Validator.AssertSuccess(status);

            switch (type)
            {
                case KerberosKeyType.DES_CBC_MD5:
                case KerberosKeyType.DES_CBC_CRC:
                    // Some older native implementations (including DES) ignore the salt and expect it to be already appended to the password.
                    byte[] binarySalt = Encoding.Unicode.GetBytes(salt);
                    var passwordAndSalt = new Memory<byte>(new byte[password.Length + binarySalt.Length]);

                    // Push password
                    password.CopyTo(passwordAndSalt);

                    // Append salt
                    binarySalt.CopyTo(passwordAndSalt.Slice(password.Length));

                    // Replace the original password with the concatenated string.
                    password = passwordAndSalt; 
                    break;
            }

            byte[] key;
            status = cryptoSystem.DeriveKey(password, salt, iterations, out key);
            Validator.AssertSuccess(status);

            return key;
        }

        /// <summary>
        /// Derives salt from the account name.
        /// </summary>
        public static string DeriveSalt(string principal, string realm)
        {
            Validator.AssertNotNull(principal, nameof(principal));
            Validator.AssertNotNull(realm, nameof(realm));

            bool isComputerAccount = principal.EndsWith("$");

            if (isComputerAccount)
            {
                // Computer / managed service account salt, e.g. CONTOSO.COMhostdc01.contoso.com
                return string.Format("{0}host{1}.{2}",
                    realm.ToUpperInvariant(),
                    principal.TrimEnd('$').ToLowerInvariant(),
                    realm.ToLowerInvariant());
            }
            else
            {
                // User account salt, e.g. CONTOSO.COMjohn
                return realm.ToUpperInvariant() + principal;
            }
        }
    }
}
