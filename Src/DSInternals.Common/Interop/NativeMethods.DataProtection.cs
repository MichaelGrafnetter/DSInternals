using System.Runtime.InteropServices;
using System.Text;
using DSInternals.Common.Data;

namespace DSInternals.Common.Interop;

internal static partial class NativeMethods
{
    private const string NCrypt = "ncrypt.dll";
    private const string KdsCli = "KdsCli.dll";
    private const uint NCRYPT_PROTECTION_INFO_TYPE_DESCRIPTOR_STRING = 1;

    /// <summary>
    /// Converts a string-format security descriptor into a self-relative security descriptor.
    /// </summary>
    /// <param name="stringSecurityDescriptor">A string that contains the security descriptor to convert.</param>
    /// <param name="stringSDRevision">The revision level of <paramref name="stringSecurityDescriptor" />.</param>
    /// <param name="securityDescriptor">When this method returns, contains the converted security descriptor. This parameter is treated as uninitialized.</param>
    /// <param name="securityDescriptorSize">When this method returns, contains the size, in bytes, of <paramref name="securityDescriptor" />. This parameter is treated as uninitialized.</param>
    /// <returns><see langword="true" /> if the conversion succeeds; otherwise, <see langword="false" />.</returns>
    [DllImport(Advapi, CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string stringSecurityDescriptor, uint stringSDRevision, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] out byte[] securityDescriptor, out uint securityDescriptorSize);

    internal static Win32ErrorCode ConvertStringSecurityDescriptorToSecurityDescriptor(string stringSecurityDescriptor, uint stringSDRevision, out byte[] securityDescriptor)
    {
        uint securityDescriptorSize;
        bool result = ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, stringSDRevision, out securityDescriptor, out securityDescriptorSize);
        if (result)
        {
            return Win32ErrorCode.Success;
        }
        else
        {
            return (Win32ErrorCode)Marshal.GetLastWin32Error();
        }
    }

    /// <summary>
    /// Generates key derivation context data for a group key envelope.
    /// </summary>
    /// <param name="rootKeyId">Root key identifier of the requested key. It can be set to NULL.</param>
    /// <param name="l0KeyId">L0 index of the requested group key. It MUST be a signed 32-bit integer greater than or equal to -1.</param>
    /// <param name="l1KeyId">L1 index of the requested group key. It MUST be a signed 32-bit integer between -1 and 31 (inclusive).</param>
    /// <param name="l2KeyId">L2 index of the requested group key. It MUST be a 32-bit integer between -1 and 31 (inclusive).</param>
    /// <param name="level">Group key level.</param>
    /// <param name="context">When this method returns, contains the generated context. This parameter is treated as uninitialized.</param>
    /// <param name="counterOffset">When this method returns, contains the counter offset for the generated context. This parameter is treated as uninitialized.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    internal static Win32ErrorCode GenerateKDFContext(
        Guid rootKeyId,
        int l0KeyId,
        int l1KeyId,
        int l2KeyId,
        GroupKeyLevel level,
        out byte[]? context,
        out int counterOffset)
    {
        var result = GenerateKDFContext(
            rootKeyId,
            l0KeyId,
            l1KeyId,
            l2KeyId,
            level,
            out SafeSidKeyProviderHandle contextHandle,
            out int contextLength,
            out counterOffset
        );

        try
        {
            context = contextHandle.ToArray(contextLength);
        }
        finally
        {
            contextHandle.Close();
        }

        return result;
    }

    /// <summary>
    /// Generates key derivation context data for a group key envelope.
    /// </summary>
    /// <param name="rootKeyId">A root key identifier of the requested key.</param>
    /// <param name="l0KeyId">An L0 index of the requested group key.</param>
    /// <param name="l1KeyId">An L1 index of the requested group key.</param>
    /// <param name="l2KeyId">An L2 index of the requested group key.</param>
    /// <param name="level">One of the enumeration values that specifies the group key level.</param>
    /// <param name="context">When this method returns, contains a handle to the generated context. This parameter is treated as uninitialized.</param>
    /// <param name="contextLength">When this method returns, contains the size, in bytes, of <paramref name="context" />. This parameter is treated as uninitialized.</param>
    /// <param name="counterOffset">When this method returns, contains the counter offset for the generated context. This parameter is treated as uninitialized.</param>
    /// <returns>A Win32 error code that indicates success or failure.</returns>
    [DllImport(KdsCli, SetLastError = true)]
    private static extern Win32ErrorCode GenerateKDFContext(
        Guid rootKeyId,
        int l0KeyId,
        int l1KeyId,
        int l2KeyId,
        GroupKeyLevel level,
        out SafeSidKeyProviderHandle context,
        out int contextLength,
        out int counterOffset);

    internal static Win32ErrorCode GenerateDerivedKey(
        string kdfAlgorithmName,
        byte[] kdfParameters,
        byte[] secret,
        byte[] context,
        int? counterOffset,
        string? label,
        int iteration,
        int desiredKeyLength,
        out byte[] derivedKey,
        out string invalidAttribute)
    {
        int kdfParametersLength = kdfParameters?.Length ?? 0;
        int secretLength = secret?.Length ?? 0;
        int contextLength = context?.Length ?? 0;
        int labelLength = label != null ? Encoding.Unicode.GetMaxByteCount(label.Length) : 0; // size of the unicode string, including the trailing zero
        byte[] derivedKeyBuffer = new byte[desiredKeyLength];
        StringBuilder invalidAttributeBuffer = new StringBuilder(byte.MaxValue);

        // Deal with the optional int parameter
        int counterOffsetValue = counterOffset.GetValueOrDefault();
        var counterOffsetHandle = GCHandle.Alloc(counterOffsetValue, GCHandleType.Pinned);

        try
        {
            Win32ErrorCode result = GenerateDerivedKey(
                kdfAlgorithmName,
                kdfParameters,
                kdfParametersLength,
                secret,
                secretLength,
                context,
                contextLength,
                (counterOffset.HasValue ? counterOffsetHandle.AddrOfPinnedObject() : IntPtr.Zero),
                label,
                labelLength,
                iteration,
                derivedKeyBuffer,
                desiredKeyLength,
                ref invalidAttributeBuffer
            );

            derivedKey = derivedKeyBuffer;
            invalidAttribute = invalidAttributeBuffer.ToString();
            return result;
        }
        finally
        {
            counterOffsetHandle.Free();
        }
    }

    /// <summary>
    /// Generates a derived key by using the Key Distribution Service client.
    /// </summary>
    /// <param name="kdfAlgorithmName">A key derivation function algorithm name.</param>
    /// <param name="kdfParameters">A buffer that contains key derivation function parameters.</param>
    /// <param name="kdfParametersLength">The number of bytes in <paramref name="kdfParameters" />.</param>
    /// <param name="secret">A buffer that contains the secret from which to derive the key.</param>
    /// <param name="secretLength">The number of bytes in <paramref name="secret" />.</param>
    /// <param name="context">A buffer that contains key derivation context data.</param>
    /// <param name="contextLength">The number of bytes in <paramref name="context" />.</param>
    /// <param name="counterOffset">A pointer to the counter offset value, or <see cref="IntPtr.Zero" /> to omit it.</param>
    /// <param name="label">A key derivation label.</param>
    /// <param name="labelLength">The size, in bytes, of <paramref name="label" />, including the terminating null character.</param>
    /// <param name="iteration">An iteration number for the derived key.</param>
    /// <param name="key">A buffer that receives the derived key.</param>
    /// <param name="keyLength">The number of bytes in <paramref name="key" />.</param>
    /// <param name="invalidAttribute">A buffer that receives the name of an invalid attribute.</param>
    /// <returns>A Win32 error code that indicates success or failure.</returns>
    [DllImport(KdsCli, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern Win32ErrorCode GenerateDerivedKey(
        string kdfAlgorithmName,
        byte[]? kdfParameters,
        int kdfParametersLength,
        byte[]? secret,
        int secretLength,
        byte[]? context,
        int contextLength,
        IntPtr counterOffset,
        string? label,
        int labelLength,
        int iteration,
        [MarshalAs(UnmanagedType.LPArray)] byte[] key,
        int keyLength,
        ref StringBuilder invalidAttribute);

    /// <summary>
    /// Frees memory allocated for a credentials structure by the GenerateKDFContext and GenerateDerivedKey functions.
    /// </summary>
    /// <param name="memory">Memory to be freed.</param>
    [DllImport(KdsCli)]
    internal static extern void SIDKeyProvFree([In] IntPtr memory);

    internal static Win32ErrorCode GetSIDKeyCacheFolder(
        byte[] targetSecurityDescriptor,
        out string? userStorageArea,
        out string? sidKeyCacheFolder,
        bool isLowBox = false)
    {
        Win32ErrorCode result = GetSIDKeyCacheFolder(targetSecurityDescriptor, targetSecurityDescriptor?.Length ?? 0, isLowBox, out var userStorageAreaHandle, out var sidKeyCacheFolderHandle);

        userStorageArea = userStorageAreaHandle.StringValue;
        sidKeyCacheFolder = sidKeyCacheFolderHandle.StringValue;

        return result;
    }

    /// <summary>
    /// Retrieves the SidKey cache storage area and folder for a target security descriptor.
    /// </summary>
    /// <param name="targetSecurityDescriptor">A target security descriptor.</param>
    /// <param name="targetSecurityDescriptorLength">The number of bytes in <paramref name="targetSecurityDescriptor" />.</param>
    /// <param name="isLowBox"><see langword="true" /> to retrieve the low box cache folder; otherwise, <see langword="false" />.</param>
    /// <param name="userStorageArea">When this method returns, contains a handle to the user storage area string. This parameter is treated as uninitialized.</param>
    /// <param name="sidKeyCacheFolder">When this method returns, contains a handle to the SidKey cache folder string. This parameter is treated as uninitialized.</param>
    /// <returns>A Win32 error code that indicates success or failure.</returns>
    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode GetSIDKeyCacheFolder(byte[] targetSecurityDescriptor, int targetSecurityDescriptorLength, bool isLowBox, out SafeSidKeyProviderHandle userStorageArea, out SafeSidKeyProviderHandle sidKeyCacheFolder);

    internal static Win32ErrorCode GetSIDKeyFileName(Guid rootKeyId, int l0KeyId, bool publicKey, string sidKeyFolder, out string? sidKeyFileName)
    {
        Win32ErrorCode result = GetSIDKeyFileName(rootKeyId, l0KeyId, publicKey, sidKeyFolder, out SafeSidKeyProviderHandle sidKeyFileNameHnadle);
        sidKeyFileName = sidKeyFileNameHnadle.StringValue;
        return result;
    }

    /// <summary>
    /// Retrieves the SidKey cache file name for a root key.
    /// </summary>
    /// <param name="rootKeyId">A root key identifier.</param>
    /// <param name="l0KeyId">An L0 index of the requested group key.</param>
    /// <param name="publicKey"><see langword="true" /> to retrieve the public key file name; otherwise, <see langword="false" />.</param>
    /// <param name="sidKeyFolder">A SidKey cache folder path.</param>
    /// <param name="sidKeyFileName">When this method returns, contains a handle to the SidKey file name string. This parameter is treated as uninitialized.</param>
    /// <returns>A Win32 error code that indicates success or failure.</returns>
    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode GetSIDKeyFileName(Guid rootKeyId, int l0KeyId, bool publicKey, string sidKeyFolder, out SafeSidKeyProviderHandle sidKeyFileName);

    internal static Win32ErrorCode WriteSIDKeyInCache(byte[] sidKey, byte[] targetSecurityDescriptor, string sidKeyFolder, string sidKeyStorageArea)
    {
        return WriteSIDKeyInCache(sidKey, sidKey?.Length ?? 0, targetSecurityDescriptor, targetSecurityDescriptor?.Length ?? 0, sidKeyFolder, sidKeyStorageArea);
    }


    /// <summary>
    /// Writes a SidKey to the local SidKey cache.
    /// </summary>
    /// <param name="sidKey">A SidKey value to write to the cache.</param>
    /// <param name="sidKeyLength">The number of bytes in <paramref name="sidKey" />.</param>
    /// <param name="targetSecurityDescriptor">A target security descriptor.</param>
    /// <param name="targetSecurityDescriptorLength">The number of bytes in <paramref name="targetSecurityDescriptor" />.</param>
    /// <param name="sidKeyFolder">A SidKey cache folder path.</param>
    /// <param name="sidKeyStorageArea">A SidKey storage area path.</param>
    /// <returns>A Win32 error code that indicates success or failure.</returns>
    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode WriteSIDKeyInCache(byte[] sidKey, int sidKeyLength, byte[] targetSecurityDescriptor, int targetSecurityDescriptorLength, string sidKeyFolder, string sidKeyStorageArea);

    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    internal static extern Win32ErrorCode DeleteAllCachedKeys(string? sidFromCaller = null);

    internal static Win32ErrorCode NCryptProtectSecret(string descriptor, ReadOnlySpan<byte> data, out byte[] protectedBlob, WindowHandle windowHandle = default)
    {
        return NCryptProtectSecret(descriptor, CreateProtectionDescriptorFlags.None, data, out protectedBlob, windowHandle);
    }

    internal static Win32ErrorCode NCryptProtectSecret(string descriptorName, bool machine, ReadOnlySpan<byte> data, out byte[] protectedBlob, WindowHandle windowHandle = default)
    {
        CreateProtectionDescriptorFlags flags = CreateProtectionDescriptorFlags.NamedDescriptor;
        if (machine)
        {
            flags |= CreateProtectionDescriptorFlags.MachineKey;
        }

        return NCryptProtectSecret(descriptorName, flags, data, out protectedBlob, windowHandle);
    }

    internal static Win32ErrorCode NCryptUnprotectSecret(ReadOnlySpan<byte> protectedBlob, out byte[] data, WindowHandle windowHandle = default)
    {
        Win32ErrorCode result = NCryptUnprotectSecret(protectedBlob, decryptData: true, windowHandle, out data, out NCryptDescriptorSafeHandle descriptorHandle);
        descriptorHandle?.Dispose();
        return result;
    }

    internal static Win32ErrorCode NCryptGetProtectionDescriptorInfo(ReadOnlySpan<byte> protectedBlob, out string descriptor)
    {
        Win32ErrorCode result = NCryptUnprotectSecret(protectedBlob, decryptData: false, WindowHandle.Null, out _, out NCryptDescriptorSafeHandle descriptorHandle);
        using (descriptorHandle)
        {
            if (result != Win32ErrorCode.Success)
            {
                descriptor = string.Empty;
                return result;
            }

            return NCryptGetProtectionDescriptorInfo(descriptorHandle, out descriptor);
        }
    }

    internal static Win32ErrorCode NCryptRegisterProtectionDescriptorName(string descriptorName, string descriptor, bool machine)
    {
        return NCryptRegisterProtectionDescriptorName(descriptorName, descriptor, machine ? DescriptorNameFlags.MachineKey : DescriptorNameFlags.None);
    }

    internal static unsafe Win32ErrorCode NCryptQueryProtectionDescriptorName(string descriptorName, bool machine, out string descriptor)
    {
        descriptor = string.Empty;
        DescriptorNameFlags flags = machine ? DescriptorNameFlags.MachineKey : DescriptorNameFlags.None;
        UIntPtr requiredLength = UIntPtr.Zero;
        Win32ErrorCode result = NCryptQueryProtectionDescriptorName(descriptorName, null, ref requiredLength, flags);
        if (result != Win32ErrorCode.Success)
        {
            return result;
        }

        if (requiredLength == UIntPtr.Zero)
        {
            return result;
        }

        ulong requiredLengthValue = requiredLength.ToUInt64();
        if (requiredLengthValue > int.MaxValue)
        {
            return (Win32ErrorCode)Marshal.GetHRForException(new OutOfMemoryException());
        }

        char[] buffer = new char[(int)requiredLengthValue];
        fixed (char* bufferPointer = buffer)
        {
            result = NCryptQueryProtectionDescriptorName(descriptorName, bufferPointer, ref requiredLength, flags);
            if (result != Win32ErrorCode.Success)
            {
                return result;
            }
        }

        int descriptorLength = (int)Math.Min((ulong)buffer.Length, requiredLength.ToUInt64());
        while (descriptorLength > 0 && buffer[descriptorLength - 1] == '\0')
        {
            descriptorLength--;
        }

        descriptor = new string(buffer, 0, descriptorLength);
        return result;
    }

    /// <summary>
    /// Protects data by using an existing protection descriptor rule and flag set.
    /// </summary>
    /// <param name="descriptor">A protection descriptor rule string or registered display name.</param>
    /// <param name="flags">A bitwise combination of the enumeration values that specifies descriptor creation options.</param>
    /// <param name="data">A buffer that contains the data to protect.</param>
    /// <param name="windowHandle">A parent window handle for any user interface to display.</param>
    /// <param name="protectedBlob">When this method returns, contains the encrypted data blob. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    private static Win32ErrorCode NCryptProtectSecret(string descriptor, CreateProtectionDescriptorFlags flags, ReadOnlySpan<byte> data, out byte[] protectedBlob, WindowHandle windowHandle)
    {
        protectedBlob = Array.Empty<byte>();
        Win32ErrorCode result = NCryptCreateProtectionDescriptor(descriptor, flags, out NCryptDescriptorSafeHandle descriptorHandle);
        using (descriptorHandle)
        {
            if (result != Win32ErrorCode.Success)
            {
                return result;
            }

            return NCryptProtectSecret(descriptorHandle, data, out protectedBlob, windowHandle);
        }
    }

    /// <summary>
    /// Protects data by using an existing protection descriptor handle.
    /// </summary>
    /// <param name="descriptorHandle">A handle to the protection descriptor object.</param>
    /// <param name="data">A buffer that contains the data to protect.</param>
    /// <param name="windowHandle">A parent window handle for any user interface to display.</param>
    /// <param name="protectedBlob">When this method returns, contains the encrypted data blob. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    private static unsafe Win32ErrorCode NCryptProtectSecret(NCryptDescriptorSafeHandle descriptorHandle, ReadOnlySpan<byte> data, out byte[] protectedBlob, WindowHandle windowHandle)
    {
        protectedBlob = Array.Empty<byte>();
        ProtectSecretFlags flags = windowHandle.IsValid ? ProtectSecretFlags.None : ProtectSecretFlags.Silent;

        Win32ErrorCode result;
        uint protectedBlobLength;
        SafeLocalAllocHandle protectedBlobHandle;
        fixed (byte* dataPointer = data)
        {
            result = NCryptProtectSecret(
                descriptorHandle,
                flags,
                dataPointer,
                (uint)data.Length,
                IntPtr.Zero,
                windowHandle,
                out protectedBlobHandle,
                out protectedBlobLength);
        }

        using (protectedBlobHandle)
        {
            if (result != Win32ErrorCode.Success)
            {
                return result;
            }

            protectedBlob = protectedBlobHandle.ToArray(protectedBlobLength) ?? Array.Empty<byte>();
            return result;
        }
    }

    /// <summary>
    /// Unprotects a protected data blob and optionally retrieves only its descriptor.
    /// </summary>
    /// <param name="protectedBlob">A buffer that contains the protected data blob.</param>
    /// <param name="decryptData"><see langword="true" /> to decrypt the data; otherwise, <see langword="false" /> to decode only the descriptor.</param>
    /// <param name="windowHandle">A parent window handle for any user interface to display.</param>
    /// <param name="data">When this method returns, contains the decrypted data. This parameter is treated as uninitialized.</param>
    /// <param name="descriptorHandle">When this method returns, contains a handle to the protection descriptor. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    private static unsafe Win32ErrorCode NCryptUnprotectSecret(
        ReadOnlySpan<byte> protectedBlob,
        bool decryptData,
        WindowHandle windowHandle,
        out byte[] data,
        out NCryptDescriptorSafeHandle descriptorHandle)
    {
        UnprotectSecretFlags flags = windowHandle.IsValid ? UnprotectSecretFlags.None : UnprotectSecretFlags.Silent;
        if (!decryptData)
        {
            flags |= UnprotectSecretFlags.NoDecrypt;
        }

        Win32ErrorCode result;
        uint dataLength;
        SafeLocalAllocHandle dataHandle;
        fixed (byte* protectedBlobPointer = protectedBlob)
        {
            result = NCryptUnprotectSecret(
                out descriptorHandle,
                flags,
                protectedBlobPointer,
                (uint)protectedBlob.Length,
                IntPtr.Zero,
                windowHandle,
                out dataHandle,
                out dataLength);
        }

        using (dataHandle)
        {
            if (result != Win32ErrorCode.Success)
            {
                descriptorHandle?.Dispose();
                data = Array.Empty<byte>();
                return result;
            }

            data = decryptData ? dataHandle.ToArray(dataLength) ?? Array.Empty<byte>() : Array.Empty<byte>();
            return result;
        }
    }

    /// <summary>
    /// Retrieves the rule string from a protection descriptor handle.
    /// </summary>
    /// <param name="descriptorHandle">A protection descriptor handle created by <c>NCryptCreateProtectionDescriptor</c>.</param>
    /// <param name="descriptor">When this method returns, contains a protection descriptor rule string. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    private static Win32ErrorCode NCryptGetProtectionDescriptorInfo(NCryptDescriptorSafeHandle descriptorHandle, out string descriptor)
    {
        descriptor = string.Empty;
        Win32ErrorCode result = NCryptGetProtectionDescriptorInfo(
            descriptorHandle,
            IntPtr.Zero,
            NCRYPT_PROTECTION_INFO_TYPE_DESCRIPTOR_STRING,
            out SafeLocalAllocHandle infoHandle);

        using (infoHandle)
        {
            if (result != Win32ErrorCode.Success)
            {
                return result;
            }

            descriptor = infoHandle.StringValue ?? string.Empty;
            return result;
        }
    }

    /// <summary>
    /// Registers the display name and associated rule string for a protection descriptor.
    /// </summary>
    /// <param name="pwszName">A null-terminated Unicode string that contains the descriptor display name to register.</param>
    /// <param name="pwszDescriptorString">A null-terminated Unicode string that contains a protection descriptor rule. A <see langword="null" /> or empty value deletes the registry value for <paramref name="pwszName" />.</param>
    /// <param name="dwFlags">A bitwise combination of the enumeration values that specifies the registry hive under which to register the entry.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode NCryptRegisterProtectionDescriptorName(
        string pwszName,
        string pwszDescriptorString,
        DescriptorNameFlags dwFlags);

    /// <summary>
    /// Retrieves the protection descriptor rule string associated with a registered descriptor display name.
    /// </summary>
    /// <param name="pwszName">A registered display name for the protection descriptor.</param>
    /// <param name="pwszDescriptorString">A buffer that receives the null-terminated Unicode protection descriptor rule string.</param>
    /// <param name="pcDescriptorString">The number of characters in <paramref name="pwszDescriptorString" /> on input; when this method returns, contains the number of characters required or retrieved. This parameter is treated as initialized.</param>
    /// <param name="dwFlags">A bitwise combination of the enumeration values that specifies the registry hive to query for the registered name.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static unsafe extern Win32ErrorCode NCryptQueryProtectionDescriptorName(
        string pwszName,
        char* pwszDescriptorString,
        ref UIntPtr pcDescriptorString,
        DescriptorNameFlags dwFlags);

    /// <summary>
    /// Retrieves a handle to a protection descriptor object.
    /// </summary>
    /// <param name="pwszDescriptorString">A null-terminated Unicode string that contains a protection descriptor rule string or a registered display name for the rule.</param>
    /// <param name="dwFlags">A bitwise combination of the enumeration values that specifies whether <paramref name="pwszDescriptorString" /> is a display name and where to search for its associated rule string.</param>
    /// <param name="phDescriptor">When this method returns, contains a handle to the protection descriptor object. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode NCryptCreateProtectionDescriptor(
        string pwszDescriptorString,
        CreateProtectionDescriptorFlags dwFlags,
        out NCryptDescriptorSafeHandle phDescriptor);

    /// <summary>
    /// Zeros and frees a protection descriptor object and releases its handle.
    /// </summary>
    /// <param name="hDescriptor">A protection descriptor handle created by <c>NCryptCreateProtectionDescriptor</c>.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true)]
    internal static extern Win32ErrorCode NCryptCloseProtectionDescriptor(IntPtr hDescriptor);

    /// <summary>
    /// Encrypts data to a specified protection descriptor.
    /// </summary>
    /// <param name="hDescriptor">A handle to the protection descriptor object.</param>
    /// <param name="dwFlags">A bitwise combination of the enumeration values that specifies protection options.</param>
    /// <param name="pbData">A pointer to the byte array to protect.</param>
    /// <param name="cbData">The number of bytes in <paramref name="pbData" />.</param>
    /// <param name="pMemPara">A pointer to custom memory management functions. <see cref="IntPtr.Zero" /> uses <c>LocalAlloc</c>, and the caller must release the returned memory with <c>LocalFree</c>.</param>
    /// <param name="hWnd">A parent window handle for any user interface to display.</param>
    /// <param name="ppbProtectedBlob">When this method returns, contains a pointer to the encrypted data. This parameter is treated as uninitialized.</param>
    /// <param name="pcbProtectedBlob">When this method returns, contains the size, in bytes, of <paramref name="ppbProtectedBlob" />. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true)]
    private static unsafe extern Win32ErrorCode NCryptProtectSecret(
        NCryptDescriptorSafeHandle hDescriptor,
        ProtectSecretFlags dwFlags,
        byte* pbData,
        uint cbData,
        IntPtr pMemPara,
        WindowHandle hWnd,
        out SafeLocalAllocHandle ppbProtectedBlob,
        out uint pcbProtectedBlob);

    /// <summary>
    /// Decrypts data to a specified protection descriptor.
    /// </summary>
    /// <param name="phDescriptor">When this method returns, contains a handle to the protection descriptor. This parameter is treated as uninitialized.</param>
    /// <param name="dwFlags">A bitwise combination of the enumeration values that specifies unprotection options.</param>
    /// <param name="pbProtectedBlob">A pointer to an array of bytes that contains the data to decrypt.</param>
    /// <param name="cbProtectedBlob">The number of bytes in <paramref name="pbProtectedBlob" />.</param>
    /// <param name="pMemPara">A pointer to custom memory management functions. <see cref="IntPtr.Zero" /> uses <c>LocalAlloc</c>, and the caller must release the returned memory with <c>LocalFree</c>.</param>
    /// <param name="hWnd">A parent window handle for any user interface to display.</param>
    /// <param name="ppbData">When this method returns, contains a pointer to the decrypted data. This parameter is treated as uninitialized.</param>
    /// <param name="pcbData">When this method returns, contains the size, in bytes, of <paramref name="ppbData" />. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true)]
    private static unsafe extern Win32ErrorCode NCryptUnprotectSecret(
        out NCryptDescriptorSafeHandle phDescriptor,
        UnprotectSecretFlags dwFlags,
        byte* pbProtectedBlob,
        uint cbProtectedBlob,
        IntPtr pMemPara,
        WindowHandle hWnd,
        out SafeLocalAllocHandle ppbData,
        out uint pcbData);

    /// <summary>
    /// Retrieves a protection descriptor rule string.
    /// </summary>
    /// <param name="hDescriptor">A protection descriptor handle created by <c>NCryptCreateProtectionDescriptor</c>.</param>
    /// <param name="pMemPara">A pointer to custom memory management functions. <see cref="IntPtr.Zero" /> uses <c>LocalAlloc</c>, and the caller must release the returned memory with <c>LocalFree</c>.</param>
    /// <param name="dwInfoType">A value that specifies how to return descriptor information to <paramref name="ppvInfo" />.</param>
    /// <param name="ppvInfo">When this method returns, contains a pointer to the descriptor information. This parameter is treated as uninitialized.</param>
    /// <returns>A status code that indicates success or failure.</returns>
    [DllImport(NCrypt, ExactSpelling = true)]
    private static extern Win32ErrorCode NCryptGetProtectionDescriptorInfo(
        NCryptDescriptorSafeHandle hDescriptor,
        IntPtr pMemPara,
        uint dwInfoType,
        out SafeLocalAllocHandle ppvInfo);

}
