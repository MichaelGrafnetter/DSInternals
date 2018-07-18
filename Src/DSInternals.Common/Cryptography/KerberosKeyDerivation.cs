namespace DSInternals.Common.Cryptography
{
    using DSInternals.Common.Data;
    using DSInternals.Common.Interop;
    using System.Security;

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
            Validator.AssertNotNull(password, "password");
            Validator.AssertNotNull(salt, "salt");

            KerberosCryptoSystem cryptoSystem;
            var status = NativeMethods.CDLocateCSystem(type, out cryptoSystem);
            Validator.AssertSuccess(status);

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
        /// Derives salt from the account name.
        /// </summary>
        public static string DeriveSalt(string principal, string realm)
        {
            Validator.AssertNotNull(principal, "principal");
            Validator.AssertNotNull(realm, "realm");
            return realm.ToUpper() + principal;
        }
    }
}
