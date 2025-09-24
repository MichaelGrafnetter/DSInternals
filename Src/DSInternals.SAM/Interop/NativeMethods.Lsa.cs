using System.Runtime.InteropServices;
using DSInternals.Common.Interop;
using Windows.Win32.Security.Authentication.Identity;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Contains P/Invoke signatures for advapi32.dll functions.
/// </summary>
internal static partial class NativeMethods
{
    private const string Advapi = "advapi32.dll";

    /// <summary>
    /// Opens a handle to the Policy object on a local or remote system.
    /// </summary>
    /// <param name="systemName">A pointer to an LSA_UNICODE_STRING structure that contains the name of the target system. The name can have the form "ComputerName" or "\ComputerName". If this parameter is NULL, the function opens the Policy object on the local system.</param>
    /// <param name="objectAttributes">A pointer to an LSA_OBJECT_ATTRIBUTES structure that specifies the connection attributes. The structure members are not used; initialize them to NULL or zero.</param>
    /// <param name="desiredAccess">An ACCESS_MASK that specifies the requested access rights. The function fails if the DACL of the target system does not allow the caller the requested access. To determine the access rights that you need, see the documentation for the LSA functions with which you want to use the policy handle.</param>
    /// <param name="policyHandle">A pointer to an LSA_HANDLE variable that receives a handle to the Policy object.</param>
    /// <returns>If the function succeeds, the function returns STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern NtStatus LsaOpenPolicy([In] ref UnicodeString systemName, [In] ref LsaObjectAttributes objectAttributes, LsaPolicyAccessMask desiredAccess, out SafeLsaPolicyHandle policyHandle);

    internal static NtStatus LsaOpenPolicy(string systemName, LsaPolicyAccessMask desiredAccess, out SafeLsaPolicyHandle policyHandle)
    {
        UnicodeString unicodeName = new(systemName);
        LsaObjectAttributes attributes = new();
        return LsaOpenPolicy(ref unicodeName, ref attributes, desiredAccess, out policyHandle);
    }

    internal static NtStatus LsaRetrievePrivateData(SafeLsaPolicyHandle policyHandle, string keyName, out byte[] privateData)
    {
        UnicodeString unicodeKeyName = new(keyName);
        NtStatus result = LsaRetrievePrivateData(policyHandle, ref unicodeKeyName, out SafeLsaMemoryHandle privateDataPointer);

        if (result == NtStatus.Success)
        {
            try
            {
                LsaBuffer buffer = privateDataPointer.MarshalAs<LsaBuffer>();
                privateData = buffer.GetBytes();
            }
            finally
            {
                privateDataPointer.Dispose();
            }
        }
        else
        {
            // Some error occurred.
            privateData = null;
        }

        // Pass-through the result of the LsaRetrievePrivateData call.
        return result;
    }

    /// <summary>
    /// Retrieves LSA private data.
    /// </summary>
    /// <param name="policyHandle">A handle to a Policy object. The handle must have the POLICY_GET_PRIVATE_INFORMATION access right.</param>
    /// <param name="keyName">Pointer to an LSA_UNICODE_STRING structure that contains the name of the key under which the private data is stored.</param>
    /// <param name="privateData">Pointer to a variable that receives a pointer to an LSA_UNICODE_STRING structure that contains the private data. When you no longer need the information, pass the returned pointer to LsaFreeMemory.</param>
    /// <returns>If the function succeeds, the function returns STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern NtStatus LsaRetrievePrivateData(SafeLsaPolicyHandle policyHandle, [In] ref UnicodeString keyName, out SafeLsaMemoryHandle privateData);

    /// <summary>
    /// Retrieves information about a Policy object.
    /// </summary>
    /// <param name="policyHandle">A handle to a Policy object.</param>
    /// <param name="informationClass">Indicates the type of information to query.</param>
    /// <param name="buffer">Pointer to a variable that receives a pointer to a structure containing the requested information.</param>
    /// <returns>If the function succeeds, the return value is STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true)]
    internal static extern NtStatus LsaQueryInformationPolicy(SafeLsaPolicyHandle policyHandle, POLICY_INFORMATION_CLASS informationClass, out SafeLsaMemoryHandle buffer);

    /// <summary>
    /// Modifies information in a Policy object.
    /// </summary>
    /// <param name="policyHandle">A handle to a Policy object.</param>
    /// <param name="informationClass">Indicates the type of information to set.</param>
    /// <param name="buffer">Pointer to a structure containing the information to set.</param>
    /// <returns>If the function succeeds, the return value is STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true)]
    private static extern NtStatus LsaSetInformationPolicy(SafeLsaPolicyHandle policyHandle, POLICY_INFORMATION_CLASS informationClass, [In] ref LsaDnsDomainInformationNative buffer);

    internal static NtStatus LsaSetInformationPolicy(SafeLsaPolicyHandle policyHandle, LsaDnsDomainInformationNative buffer)
    {
        return LsaSetInformationPolicy(policyHandle, POLICY_INFORMATION_CLASS.PolicyDnsDomainInformation, ref buffer);
    }

    /// <summary>
    /// Frees memory allocated for an output buffer by an LSA function call. LSA functions that return variable-length output buffers always allocate the buffer on behalf of the caller. The caller must free this memory by passing the returned buffer pointer to LsaFreeMemory when the memory is no longer required.
    /// </summary>
    /// <param name="buffer">Pointer to memory buffer that was allocated by an LSA function call. If LsaFreeMemory is successful, this buffer is freed.</param>
    /// <returns>If the function succeeds, the return value is STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true)]
    internal static extern NtStatus LsaFreeMemory(IntPtr buffer);

    /// <summary>
    /// Closes a handle to a Policy or TrustedDomain object.
    /// </summary>
    /// <param name="handle">A handle to a Policy object returned by the LsaOpenPolicy function or to a TrustedDomain object returned by the LsaOpenTrustedDomainByName function.</param>
    /// <returns>If the function succeeds, the return value is STATUS_SUCCESS.</returns>
    [DllImport(Advapi, SetLastError = true)]
    internal static extern NtStatus LsaClose(IntPtr handle);
}
