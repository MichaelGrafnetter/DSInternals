using System.Runtime.InteropServices;
using System.Text;
using DSInternals.Common.Data;
using Windows.Win32.Security.Cryptography;

namespace DSInternals.Common.Interop;

internal static partial class NativeMethods
{
    private const string NCrypt = "ncrypt.dll";
    private const string KdsCli = "KdsCli.dll";

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

    /// <param name="rootKeyId">Root key identifier of the requested key. It can be set to NULL.</param>
    /// <param name="l0KeyId">L0 index of the requested group key. It MUST be a signed 32-bit integer greater than or equal to -1.</param>
    /// <param name="l1KeyId">L1 index of the requested group key. It MUST be a signed 32-bit integer between -1 and 31 (inclusive).</param>
    /// <param name="l2KeyId">L2 index of the requested group key. It MUST be a 32-bit integer between -1 and 31 (inclusive).</param>
    /// <param name="level">Group key level.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    internal static Win32ErrorCode GenerateKDFContext(
        Guid rootKeyId,
        int l0KeyId,
        int l1KeyId,
        int l2KeyId,
        GroupKeyLevel level,
        out byte[] context,
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
        string label,
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

    [DllImport(KdsCli, CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern Win32ErrorCode GenerateDerivedKey(
        string kdfAlgorithmName,
        byte[] kdfParameters,
        int kdfParametersLength,
        byte[] secret,
        int secretLength,
        byte[] context,
        int contextLength,
        IntPtr counterOffset,
        string label,
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
        out string userStorageArea,
        out string sidKeyCacheFolder,
        bool isLowBox = false)
    {
        Win32ErrorCode result = GetSIDKeyCacheFolder(targetSecurityDescriptor, targetSecurityDescriptor?.Length ?? 0, isLowBox, out var userStorageAreaHandle, out var sidKeyCacheFolderHandle);

        userStorageArea = userStorageAreaHandle.StringValue;
        sidKeyCacheFolder = sidKeyCacheFolderHandle.StringValue;

        return result;
    }

    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode GetSIDKeyCacheFolder(byte[] targetSecurityDescriptor, int targetSecurityDescriptorLength, bool isLowBox, out SafeSidKeyProviderHandle userStorageArea, out SafeSidKeyProviderHandle sidKeyCacheFolder);

    internal static Win32ErrorCode GetSIDKeyFileName(Guid rootKeyId, int l0KeyId, bool publicKey, string sidKeyFolder, out string sidKeyFileName)
    {
        Win32ErrorCode result = GetSIDKeyFileName(rootKeyId, l0KeyId, publicKey, sidKeyFolder, out SafeSidKeyProviderHandle sidKeyFileNameHnadle);
        sidKeyFileName = sidKeyFileNameHnadle.StringValue;
        return result;
    }

    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode GetSIDKeyFileName(Guid rootKeyId, int l0KeyId, bool publicKey, string sidKeyFolder, out SafeSidKeyProviderHandle sidKeyFileName);

    internal static Win32ErrorCode WriteSIDKeyInCache(byte[] sidKey, byte[] targetSecurityDescriptor, string sidKeyFolder, string sidKeyStorageArea)
    {
        return WriteSIDKeyInCache(sidKey, sidKey?.Length ?? 0, targetSecurityDescriptor, targetSecurityDescriptor?.Length ?? 0, sidKeyFolder, sidKeyStorageArea);
    }


    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    private static extern Win32ErrorCode WriteSIDKeyInCache(byte[] sidKey, int sidKeyLength, byte[] targetSecurityDescriptor, int targetSecurityDescriptorLength, string sidKeyFolder, string sidKeyStorageArea);

    [DllImport(KdsCli, CharSet = CharSet.Unicode)]
    internal static extern Win32ErrorCode DeleteAllCachedKeys(string sidFromCaller = null);

    internal static unsafe Win32ErrorCode NCryptUnprotectSecret(ReadOnlySpan<byte> protectedBlob, out ReadOnlySpan<byte> data)
    {
        fixed (byte* protectedBlobPtr = protectedBlob)
        {
            Win32ErrorCode result = NCryptUnprotectSecret(
                descriptorHandle: IntPtr.Zero,
                NCRYPT_FLAGS.NCRYPT_SILENT_FLAG,
                protectedBlobPtr,
                protectedBlob.Length,
                memPara: IntPtr.Zero,
                windowHandle: IntPtr.Zero,
                out SafeSidKeyProviderHandle dataHandle,
                out int dataLength
                );

            data = new ReadOnlySpan<byte>(dataHandle.ToArray(dataLength));
            dataHandle.Close();
            return result;
        }
    }

    [DllImport(NCrypt)]
    private static unsafe extern Win32ErrorCode NCryptUnprotectSecret(
        IntPtr descriptorHandle,
        NCRYPT_FLAGS flags,
        byte* protectedBlob,
        int protectedBlobLength,
        IntPtr memPara,
        IntPtr windowHandle,
        out SafeSidKeyProviderHandle data,
        out int dataLength);
}
