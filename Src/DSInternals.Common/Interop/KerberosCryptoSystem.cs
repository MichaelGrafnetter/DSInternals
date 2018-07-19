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
    internal class KerberosCryptoSystem
    {
        internal readonly KerberosKeyType Type;
        readonly int BlockSize;
        readonly KerberosKeyType Type1;
        internal readonly int KeySize;
        readonly int Size;
        readonly int Unknown2;
        readonly int Unknown3;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal readonly string AlgorithmName;
        readonly IntPtr Initialize;
        readonly IntPtr Encrypt;
        readonly IntPtr Decrypt;
        readonly IntPtr Finish;
        readonly KerberosKeyDerivationFunction KeyDerivationFunction;
        readonly IntPtr RandomKey;
        readonly IntPtr Control;
        readonly IntPtr Unknown4;
        readonly IntPtr Unknown5;
        readonly IntPtr Unknown6;

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
