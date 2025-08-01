using System;
using System.Runtime.InteropServices;
using System.Security;
using DSInternals.Common.Data;
using Windows.Win32.Foundation;

namespace DSInternals.Common.Interop
{
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

        delegate NtStatus KerberosKeyDerivationFunction([In] ref UNICODE_STRING password, [In] ref UNICODE_STRING salt, int iterations, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] key);

        internal unsafe NtStatus DeriveKey(SecureString password, string salt, int iterations, out byte[] key)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (salt == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (password.Length > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(password), "The password is too long.");
            }

            if (salt.Length > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(salt), "The salt is too long.");
            }

            key = new byte[this.KeySize];

            using (var passwordPointer = new SafeUnicodeSecureStringPointer(password))
            fixed (char* saltPointer = salt)
            {
                UNICODE_STRING passwordUnicodeString = new UNICODE_STRING
                {
                    Length = (ushort)passwordPointer.NumBytes,
                    MaximumLength = (ushort)passwordPointer.NumBytesTotal,
                    Buffer = new PWSTR(passwordPointer.DangerousGetHandle())
                };

                ushort saltBinaryLength = (ushort)(salt.Length * sizeof(char));

                UNICODE_STRING saltUnicodeString = new UNICODE_STRING
                {
                    Length = saltBinaryLength,
                    MaximumLength = saltBinaryLength,
                    Buffer = saltPointer
                };

                return this.KeyDerivationFunction(ref passwordUnicodeString, ref saltUnicodeString, iterations, key);
            }
        }

        internal unsafe NtStatus DeriveKey(ReadOnlyMemory<byte> password, string salt, int iterations, out byte[] key)
        {
            if (password.IsEmpty)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            if (password.Length > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(password), "The password is too long.");
            }

            if (salt.Length > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(salt), "The salt is too long.");
            }

            key = new byte[this.KeySize];

            using (var passwordHandle = password.Pin())
            fixed (char* saltPointer = salt)
            {
                UNICODE_STRING passwordUnicodeString = new UNICODE_STRING
                {
                    Length = (ushort)password.Length,
                    MaximumLength = (ushort)password.Length,
                    Buffer = (char*)passwordHandle.Pointer
                };

                ushort saltBinaryLength = (ushort)(salt.Length * sizeof(char));

                UNICODE_STRING saltUnicodeString = new UNICODE_STRING
                {
                    Length = saltBinaryLength,
                    MaximumLength = saltBinaryLength,
                    Buffer = saltPointer
                };

                return this.KeyDerivationFunction(ref passwordUnicodeString, ref saltUnicodeString, iterations, key);
            }
        }
    }
}
