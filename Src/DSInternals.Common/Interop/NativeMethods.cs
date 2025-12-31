using System.Runtime.InteropServices;
using DSInternals.Common.Data;
using Windows.Win32.Foundation;

namespace DSInternals.Common.Interop;

internal static partial class NativeMethods
{
    internal const int NTHashNumBits = 128;
    internal const int NTHashNumBytes = NTHashNumBits / 8;
    internal const int LMHashNumBits = 128;
    internal const int LMHashNumBytes = NTHashNumBits / 8;
    internal const int LMPasswordMaxChars = 14;
    internal const int NTPasswordMaxChars = 128;

    private const int MaxRegistryKeyClassSize = 256;
    private const string Advapi = "advapi32.dll";
    private const string CryptDll = "cryptdll.Dll";
    private const string Ntdll = "ntdll.dll";
    private const string LMOwfInternalName = "SystemFunction006";
    private const string NTOwfInternalName = "SystemFunction007";
    private const string LMOwfEncryptInternalName = "SystemFunction024";
    private const string LMOwfDecryptInternalName = "SystemFunction025";
    private const string NTOwfEncryptInternalName = "SystemFunction026";
    private const string NTOwfDecryptInternalName = "SystemFunction027";
    private const string RC4EncryptInternalName = "SystemFunction032";
    private const string RC4DecryptInternalName = "SystemFunction033";

    /// <summary>
    /// Converts the specified NTSTATUS code to its equivalent system error code.
    /// </summary>
    /// <param name="status">The NTSTATUS code to be converted.</param>
    /// <returns>The function returns the corresponding system error code.</returns>
    [DllImport(Ntdll)]
    internal static extern Win32ErrorCode RtlNtStatusToDosError(NtStatus status);

    /// <summary>
    /// Takes the passed NtPassword and performs a one-way-function on it.
    /// Uses the RSA MD4 function
    /// </summary>
    /// <param name="password">The password to perform the one-way-function on. </param>
    /// <param name="hash">The hashed password is returned here.</param>
    /// <returns>STATUS_SUCCESS - The function was completed successfully. The hashed password is in hash.</returns>
    /// <see>https://github.com/wine-mirror/wine/blob/master/dlls/advapi32/crypt_md4.c</see>
    [DllImport(Advapi, SetLastError = true, EntryPoint = NTOwfInternalName, CharSet = CharSet.Unicode)]
    private static extern NtStatus RtlCalculateNtOwfPassword([In] ref SecureUnicodeString password, [MarshalAs(UnmanagedType.LPArray, SizeConst = NTHashNumBytes), In, Out] byte[] hash);

    /// <summary>
    /// Takes the passed NtPassword and performs a one-way-function on it.
    /// Uses the RSA MD4 function
    /// </summary>
    /// <param name="password">The password to perform the one-way-function on. </param>
    /// <param name="hash">The hashed password is returned here.</param>
    /// <returns>STATUS_SUCCESS - The function was completed successfully. The hashed password is in hash.</returns>
    /// <see>https://github.com/wine-mirror/wine/blob/master/dlls/advapi32/crypt_md4.c</see>
    [DllImport(Advapi, SetLastError = true, EntryPoint = NTOwfInternalName, CharSet = CharSet.Unicode)]
    private static extern NtStatus RtlCalculateNtOwfPassword([In] ref UnicodeString password, [MarshalAs(UnmanagedType.LPArray, SizeConst = NTHashNumBytes), In, Out] byte[] hash);

    [DllImport(Advapi, SetLastError = true, EntryPoint = NTOwfInternalName, CharSet = CharSet.Unicode)]
    private static extern NtStatus RtlCalculateNtOwfPassword([In] ref UNICODE_STRING password, [MarshalAs(UnmanagedType.LPArray, SizeConst = NTHashNumBytes), In, Out] byte[] hash);

    unsafe internal static NtStatus RtlCalculateNtOwfPassword(ReadOnlyMemory<byte> password, out byte[] hash)
    {
        if (password.Length % 2 != 0)
        {
            throw new ArgumentException("Password length must be even (UTF-16 encoding).", nameof(password));
        }

        if (password.Length > ushort.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(password), "Password length exceeds maximum allowed length.");
        }

        // Allocate output buffer
        hash = new byte[NTHashNumBytes];

        using (var pinnedPassword = password.Pin())
        {
            UNICODE_STRING unicodeString = new UNICODE_STRING
            {
                Length = (ushort)(password.Length), // The length, in bytes
                MaximumLength = (ushort)(password.Length), // Does not contain the trailing zero
                Buffer = new PWSTR((char*)pinnedPassword.Pointer)
            };

            return RtlCalculateNtOwfPassword(ref unicodeString, hash);
        }
    }

    internal static NtStatus RtlCalculateNtOwfPassword(SafeUnicodeSecureStringPointer password, out byte[] hash)
    {
        SecureUnicodeString unicodePassword = new SecureUnicodeString(password);
        // Allocate output buffer
        hash = new byte[NTHashNumBytes];
        return RtlCalculateNtOwfPassword(ref unicodePassword, hash);
    }

    internal static NtStatus RtlCalculateNtOwfPassword(string password, out byte[] hash)
    {
        UnicodeString unicodePassword = new UnicodeString(password);
        // Allocate output buffer
        hash = new byte[NTHashNumBytes];
        return RtlCalculateNtOwfPassword(ref unicodePassword, hash);
    }

    /// <summary>
    /// Takes the passed password and performs a one-way-function on it.
    /// The current implementation does this by using the password as a key to encrypt a known block of text.
    /// </summary>
    /// <param name="password">The password to perform the one-way-function on.</param>
    /// <param name="hash">The hashed password is returned here</param>
    /// <returns>
    /// STATUS_SUCCESS - The function was completed successfully. The hashed password is in LmOwfPassword.
    /// STATUS_UNSUCCESSFUL - Something failed. The LmOwfPassword is undefined.
    /// </returns>
    /// <see>https://github.com/wine-mirror/wine/blob/master/dlls/advapi32/crypt_lmhash.c</see>
    [DllImport(Advapi, SetLastError = true, EntryPoint = LMOwfInternalName)]
    private static extern NtStatus RtlCalculateLmOwfPassword(SafeOemStringPointer password, [MarshalAs(UnmanagedType.LPArray, SizeConst = LMHashNumBytes), In, Out] byte[] hash);

    internal static NtStatus RtlCalculateLmOwfPassword(SafeOemStringPointer password, out byte[] hash)
    {
        // Allocate output buffer
        hash = new byte[LMHashNumBytes];
        return RtlCalculateLmOwfPassword(password, hash);
    }

    /// <summary>
    /// This function upper cases the specified unicode source string
    /// and then converts it into an oem string. The translation is done with respect
    /// to the OEM code page (OCP).
    /// </summary>
    /// <param name="destinationString">Returns an oem string that is equivalent to the unicode source string. The maximum length field is only set if AllocateDestinationString is TRUE.</param>
    /// <param name="sourceString">Supplies the unicode source string that is to be converted to oem.</param>
    /// <param name="allocateDestinationString">Supplies a flag that controls whether or not this API allocates the buffer space for the destination string. If it does, then the buffer must be deallocated using RtlFreeAnsiString (note that only storage for DestinationString->Buffer is allocated by this API).</param>
    /// <returns>SUCCESS if the conversion was successful.</returns>
    [DllImport(Ntdll, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern NtStatus RtlUpcaseUnicodeStringToOemString([In, Out] ref OemString destinationString, [In] ref SecureUnicodeString sourceString, [MarshalAs(UnmanagedType.Bool)] bool allocateDestinationString);

    internal static NtStatus RtlUpcaseUnicodeStringToOemString(OemString destinationString, SecureUnicodeString sourceString)
    {
        return RtlUpcaseUnicodeStringToOemString(ref destinationString, ref sourceString, false);
    }

    /// <summary>
    /// Decrypt NtOwfPassword using an index as the key
    /// </summary>
    [DllImport(Advapi, EntryPoint = NTOwfDecryptInternalName, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    private static extern NtStatus RtlDecryptNtOwfPwdWithIndex([In] byte[] encryptedNtOwfPassword, [In] ref int index, [In, Out] byte[] ntOwfPassword);
    internal static NtStatus RtlDecryptNtOwfPwdWithIndex(byte[] encryptedNtOwfPassword, int index, out byte[] ntOwfPassword)
    {
        ntOwfPassword = new byte[NTHashNumBytes];
        // Wrap to get rid of the unnecessary pointer to int
        return RtlDecryptNtOwfPwdWithIndex(encryptedNtOwfPassword, ref index, ntOwfPassword);
    }

    /// <summary>
    /// Encrypt NtOwfPassword using an index as the key
    /// </summary>
    [DllImport(Advapi, EntryPoint = NTOwfEncryptInternalName, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    private static extern NtStatus RtlEncryptNtOwfPwdWithIndex([In] byte[] ntOwfPassword, [In] ref int index, [In, Out] byte[] encryptedNtOwfPassword);
    internal static NtStatus RtlEncryptNtOwfPwdWithIndex(byte[] ntOwfPassword, int index, out byte[] encryptedNtOwfPassword)
    {
        encryptedNtOwfPassword = new byte[NTHashNumBytes];
        // Wrap to get rid of the unnecessary pointer to int
        return RtlEncryptNtOwfPwdWithIndex(ntOwfPassword, ref index, encryptedNtOwfPassword);
    }

    /// <summary>
    /// Decrypt LmOwfPassword using an index as the key
    /// </summary>
    [DllImport(Advapi, EntryPoint = LMOwfDecryptInternalName, SetLastError = true)]
    private static extern NtStatus RtlDecryptLmOwfPwdWithIndex([In] byte[] encryptedLmOwfPassword, [In] ref int index, [In, Out] byte[] lmOwfPassword);
    internal static NtStatus RtlDecryptLmOwfPwdWithIndex(byte[] encryptedLmOwfPassword, int index, out byte[] lmOwfPassword)
    {
        lmOwfPassword = new byte[LMHashNumBytes];
        // Wrap to get rid of the unnecessary pointer to int
        return RtlDecryptLmOwfPwdWithIndex(encryptedLmOwfPassword, ref index, lmOwfPassword);
    }

    /// <summary>
    /// Encrypt LmOwfPassword using an index as the key
    /// </summary>
    [DllImport(Advapi, EntryPoint = LMOwfEncryptInternalName, SetLastError = true)]
    private static extern NtStatus RtlEncryptLmOwfPwdWithIndex([In] byte[] lmOwfPassword, [In] ref int index, [In, Out] byte[] encryptedLmOwfPassword);
    internal static NtStatus RtlEncryptLmOwfPwdWithIndex(byte[] lmOwfPassword, int index, out byte[] encryptedLmOwfPassword)
    {
        encryptedLmOwfPassword = new byte[LMHashNumBytes];
        // Wrap to get rid of the unnecessary pointer to int
        return RtlEncryptLmOwfPwdWithIndex(lmOwfPassword, ref index, encryptedLmOwfPassword);
    }

    /// <summary>
    /// Faster arbitrary length data encryption function (using RC4)
    /// </summary>
    [DllImport(Advapi, EntryPoint = RC4DecryptInternalName, SetLastError = true)]
    private static extern NtStatus RtlDecryptData2(ref CryptoBuffer data, ref CryptoBuffer key);

    internal static NtStatus RtlDecryptData2(byte[] encryptedData, byte[] key, out byte[] decryptedData)
    {
        // RtlDecryptData2 writes data to the input buffer, so we make a copy of it to be safe
        decryptedData = (byte[])encryptedData.Clone();

        GCHandle dataPin = GCHandle.Alloc((object)decryptedData, GCHandleType.Pinned);
        GCHandle keyPin = GCHandle.Alloc((object)key, GCHandleType.Pinned);
        try
        {
            CryptoBuffer keyBuffer = new CryptoBuffer(keyPin.AddrOfPinnedObject(), key.Length);
            CryptoBuffer dataBuffer = new CryptoBuffer(dataPin.AddrOfPinnedObject(), decryptedData.Length);

            NtStatus result = RtlDecryptData2(ref dataBuffer, ref keyBuffer);
            return result;
        }
        finally
        {
            if (dataPin.IsAllocated)
            {
                dataPin.Free();
            }
            if (keyPin.IsAllocated)
            {
                keyPin.Free();
            }
        }
    }

    [DllImport(CryptDll, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern NtStatus CDLocateCSystem(KerberosKeyType type, out IntPtr cryptoSystem);

    internal static NtStatus CDLocateCSystem(KerberosKeyType type, out KerberosCryptoSystem cryptoSystem)
    {
        IntPtr cryptoSystemPtr;
        NtStatus status = CDLocateCSystem(type, out cryptoSystemPtr);

        cryptoSystem = (status == NtStatus.Success) ? Marshal.PtrToStructure<KerberosCryptoSystem>(cryptoSystemPtr) : null;
        return status;
    }
}
