namespace DSInternals.Common.Interop
{
    using DSInternals.Common.Data;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Represents the native implementation of a Kerberos encryption type. It is returned by CDLocateCSystem.
    /// </summary>
    /// <remarks>This structure is undocumented. Kudos to Benjamin Delpy for figuring out the most important parts of it.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KerberosCryptoSystem
    {
        internal KerberosKeyType Type;
        int BlockSize;
        KerberosKeyType Type1;
        internal int KeySize;
        int Size;
        int Unknown2;
        int Unknown3;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string AlgorithmName;
        IntPtr Initialize;
        IntPtr Encrypt;
        IntPtr Decrypt;
        IntPtr Finish;
        private KerberosKeyDerivationFunction KeyDerivationFunction;
        IntPtr RandomKey;
        IntPtr Control;
        IntPtr Unknown4;
        IntPtr Unknown5;
        IntPtr Unknown6;

        delegate NtStatus KerberosKeyDerivationFunction([In] ref SecureUnicodeString password, [In] ref UnicodeString salt, int iterations, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] key);

        internal NtStatus DeriveKey(SecureString password, string salt, int iterations, out byte[] key)
        {
            key = new byte[this.KeySize];
            var unicodeSalt = new UnicodeString(salt);

            using (var passwordPointer = new SafeUnicodeSecureStringPointer(password))
            {
                var unicodePassword = new SecureUnicodeString(passwordPointer);
                return this.KeyDerivationFunction(ref unicodePassword, ref unicodeSalt, iterations, key);
            }
        }
    }
}
