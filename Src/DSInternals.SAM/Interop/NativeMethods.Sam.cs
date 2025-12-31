using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Interop;
using Windows.Win32.Security;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Contains P/Invoke signatures for samlib.dll and samsrv.dll functions.
/// </summary>
internal static partial class NativeMethods
{
    private const string SamLib = "samlib.dll";
    private const string SpnPrefix = "cifs/";

    /// <summary>
    ///  The maximum value of 1,000 is chosen to limit the amount of memory that the client can force the server to allocate.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245712.aspx</see>
    private const int MaxNamesToLookup = 1000;

    /// <summary>
    /// Establish a session with a SAM subsystem and subsequently open the SamServer object of that subsystem.
    /// The caller must have SAM_SERVER_CONNECT access to the SamServer object of the subsystem being connected to.
    /// </summary>
    /// <param name="serverName">Name of the server to use, or NULL if local.</param>
    /// <param name="serverHandle">A handle to be used in future requests.</param>
    /// <param name="accessMask">Is an access mask indicating which access types are desired to the SamServer.</param>
    /// <param name="objectAttributes">Pointer to the set of object attributes to use for this connection. Only the security Quality Of Service information is used and should provide SecurityIdentification level of impersonation.</param>
    /// <returns>
    /// STATUS_SUCCESS - The Service completed successfully.
    /// STATUS_ACCESS_DENIED - Access was denied.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245753.aspx</see>
    [DllImport(SamLib, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern NtStatus SamConnect([In] ref UnicodeString serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, IntPtr objectAttributes);
    internal static NtStatus SamConnect(string serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask)
    {
        UnicodeString unicodeServerName = new(serverName);
        return SamConnect(ref unicodeServerName, out serverHandle, accessMask, objectAttributes: IntPtr.Zero);
    }

    [DllImport(SamLib, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern NtStatus SamConnectWithCreds([In] ref UnicodeString serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, IntPtr objectAttributes, [In] ref WindowsAuthenticationIdentity authIdentity, [MarshalAs(UnmanagedType.LPWStr)] string servicePrincipalName, [MarshalAs(UnmanagedType.Bool)] out bool isWin2k);

    internal static NtStatus SamConnectWithCreds(string serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, NetworkCredential credential)
    {
        UnicodeString unicodeServerName = new(serverName);
        string servicePrincipalName = SpnPrefix + serverName;
        WindowsAuthenticationIdentity authIdentity = new(credential);

        NtStatus result = SamConnectWithCreds(ref unicodeServerName, out serverHandle, accessMask, objectAttributes: IntPtr.Zero, ref authIdentity, servicePrincipalName, out bool _);

        if (result == NtStatus.RpcUnknownAuthenticationService)
        {
            // Try it again, but without the SPN, to force NTLM instead of Negotiate.
            result = SamConnectWithCreds(ref unicodeServerName, out serverHandle, accessMask, objectAttributes: IntPtr.Zero, ref authIdentity, servicePrincipalName: null, out bool _);
        }

        return result;
    }

    /// <summary>
    /// The SamrEnumerateDomainsInSamServer method obtains a listing of all domains hosted by the server side of this protocol.
    /// </summary>
    /// <param name="serverHandle"> An RPC context handle representing a server object.</param>
    /// <param name="enumerationContext">This value is a cookie that the server can use to continue an enumeration on a subsequent call. It is an opaque value to the client. To initiate a new enumeration, the client sets EnumerationContext to zero. Otherwise the client sets EnumerationContext to a value returned by a previous call to the method.</param>
    /// <param name="buffer">A listing of domain information.</param>
    /// <param name="preferedMaximumLength"> The requested maximum number of bytes to return in buffer.</param>
    /// <param name="countReturned">The count of domain elements returned in buffer.</param>
    /// <returns></returns>
    [DllImport(SamLib, SetLastError = true)]
    internal static extern NtStatus SamEnumerateDomainsInSamServer(SafeSamHandle serverHandle, ref uint enumerationContext, out SafeSamPointer buffer, uint preferedMaximumLength, out uint countReturned);

    /// <summary>
    /// This API opens a domain object.  It returns a handle to the newly opened domain that must be used for successive operations on the domain. This handle may be closed with the SamCloseHandle API.
    /// </summary>
    /// <param name="serverHandle">Handle from a previous SamConnect() call.</param>
    /// <param name="desiredAccess">Is an access mask indicating which access types are desired to the domain.</param>
    /// <param name="domainId">The SID assigned to the domain to open.</param>
    /// <param name="domainHandle">Receives a handle referencing the newly opened domain. This handle will be required in successive calls to operate on the domain.</param>
    /// <returns>
    /// STATUS_SUCCESS - The domain was successfully opened.
    /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.
    /// STATUS_INVALID_SERVER_STATE - Indicates the SAM server is currently disabled.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245748.aspx</see>
    [DllImport(SamLib, SetLastError = true)]
    private static extern NtStatus SamOpenDomain(SafeSamHandle serverHandle, SamDomainAccessMask desiredAccess, [MarshalAs(UnmanagedType.LPArray)] byte[] domainId, out SafeSamHandle domainHandle);
    internal static NtStatus SamOpenDomain(SafeSamHandle serverHandle, SamDomainAccessMask desiredAccess, SecurityIdentifier domainSid, out SafeSamHandle domainHandle)
    {
        byte[] binarySid = domainSid.GetBinaryForm();
        return SamOpenDomain(serverHandle, desiredAccess, binarySid, out domainHandle);
    }

    /// <summary>
    /// The SamQueryInformationDomain method obtains attributes from a domain object.
    /// </summary>
    /// <param name="domainHandle">An RPC context handle, representing a domain object.</param>
    /// <param name="domainPasswordInformation">The requested attributes on output.</param>
    internal static NtStatus SamQueryInformationDomain(SafeSamHandle domainHandle, out SamDomainPasswordInformation? domainPasswordInformation)
    {
        NtStatus result = SamQueryInformationDomain(domainHandle, SamDomainInformationClass.PasswordInformation, out SafeSamPointer buffer);

        if (buffer != null)
        {
            try
            {
                domainPasswordInformation = buffer.MarshalAs<SamDomainPasswordInformation>();
            }
            finally
            {
                buffer.Dispose();
            }
        }
        else
        {
            domainPasswordInformation = null;
        }

        return result;
    }

    /// <summary>
    /// The SamQueryInformationDomain method obtains attributes from a domain object.
    /// </summary>
    /// <param name="domainHandle">An RPC context handle, representing a domain object.</param>
    /// <param name="domainInformationClass">An enumeration indicating which attributes to return.</param>
    /// <param name="buffer">The requested attributes on output.</param>
    [DllImport(SamLib, SetLastError = true)]
    private static extern NtStatus SamQueryInformationDomain(SafeSamHandle domainHandle, SamDomainInformationClass domainInformationClass, out SafeSamPointer buffer);

    /// <summary>
    /// This API opens an existing user in the account database.  The user is specified by SID value.  The operations that will be performed on the user must be declared at this time. This call returns a handle to the newly opened user that may be used for successive operations on the user.  This handle may be closed with the SamCloseHandle API.
    /// </summary>
    /// <param name="domainHandle">A domain handle returned from a previous call to SamOpenDomain.</param>
    /// <param name="desiredAccess">Is an access mask indicating which access types are desired to the user.</param>
    /// <param name="userId">Specifies the relative ID value of the user account to be opened.</param>
    /// <param name="userHandle">Receives a handle referencing the newly opened User. This handle will be required in successive calls to operate on the user.</param>
    /// <returns>
    /// STATUS_SUCCESS - The group was successfully opened.
    /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.
    /// STATUS_NO_SUCH_USER - The specified user does not exist.
    /// STATUS_INVALID_HANDLE - The domain handle passed is invalid.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245752.aspx</see>
    [DllImport(SamLib, SetLastError = true)]
    internal static extern NtStatus SamOpenUser(SafeSamHandle domainHandle, SamUserAccessMask desiredAccess, int userId, out SafeSamHandle userHandle);

    /// <summary>
    /// This API modifies information in a user record. The data modified is determined by the UserInformationClass parameter.
    /// </summary>
    /// <param name="userHandle">The handle of an opened user to operate on.</param>
    /// <param name="userInformationClass">Class of information provided.</param>
    /// <param name="passwordInformation">Buffer containing a user info struct.</param>
    /// <returns>
    /// STATUS_SUCCESS - The Service completed successfully.
    /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.
    /// STATUS_INVALID_HANDLE - The handle passed is invalid.
    /// STATUS_INVALID_INFO_CLASS - The class provided was invalid.
    /// STATUS_INVALID_DOMAIN_STATE - The domain server is not in the correct state (disabled or enabled) to perform the requested operation. The domain server must be enabled for this operation.
    /// STATUS_INVALID_DOMAIN_ROLE - The domain server is serving the incorrect role (primary or backup) to perform the requested operation.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245793.aspx</see>
    [DllImport(SamLib, SetLastError = true)]
    private static extern NtStatus SamSetInformationUser(SafeSamHandle userHandle, SamUserInformationClass userInformationClass, [In] ref SamUserInternal1Information passwordInformation);

    /// <summary>
    /// This service returns the SID corresponding to the specified domain. The domain is specified by name.
    /// </summary>
    /// <param name="serverHandle">Handle from a previous SamConnect() call.</param>
    /// <param name="domainName">The name of the domain whose ID is to be looked up.  A case-insensitive comparison of this name will be performed for the lookup operation.</param>
    /// <param name="domainId">Receives a pointer to a buffer containing the SID of the looked up domain. This buffer must be freed using SamFreeMemory() when no longer needed.</param>
    /// <returns>
    /// STATUS_SUCCESS - The service completed successfully.
    /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.  SAM_SERVER_LOOKUP_DOMAIN access is needed.
    /// STATUS_NO_SUCH_DOMAIN - The specified domain does not exist at this server.
    /// STATUS_INVALID_SERVER_STATE - Indicates the SAM server is currently disabled.
    /// STATUS_NONE_MAPPED
    /// </returns>
    [DllImport(SamLib, SetLastError = true)]
    private static extern NtStatus SamLookupDomainInSamServer(SafeSamHandle serverHandle, [In] ref UnicodeString domainName, out SafeSamPointer domainId);

    internal static NtStatus SamLookupDomainInSamServer(SafeSamHandle serverHandle, string domainName, out SecurityIdentifier? domainSid)
    {
        UnicodeString unicodeDomainName = new(domainName);
        NtStatus result = SamLookupDomainInSamServer(serverHandle, ref unicodeDomainName, out SafeSamPointer domainSidPointer);

        if (domainSidPointer != null)
        {
            try
            {
                domainSid = domainSidPointer.MarshalAsSecurityIdentifier();
            }
            finally
            {
                domainSidPointer.Dispose();
            }
        }
        else
        {
            domainSid = null;
        }

        return result;
    }

    /// <summary>
    /// This API attempts to find relative IDs corresponding to name strings.
    /// </summary>
    /// <param name="domainHandle">A domain handle returned from a previous call to SamOpenDomain.</param>
    /// <param name="count">Number of names to translate. </param>
    /// <param name="names">Pointer to an array of Count UNICODE_STRINGs that contain the names to map to relative IDs.  Case-insensitive comparisons of these names will be performed for the lookup operation.</param>
    /// <param name="relativeIds">Receives a pointer to an array of Count Relative IDs that have been filled in.  The relative ID of the nth name will be the nth entry in this array.  Any names that could not be translated will have a zero relative ID.  This buffer must be freed when no longer needed using SamFreeMemory().</param>
    /// <param name="use">Receives a pointer to an array of Count SID_NAME_USE entries that have been filled in with what significance each name has.  The nth entry in this array indicates the meaning of the nth name passed.  This buffer must be freed when no longer needed using SamFreeMemory().</param>
    /// <returns>
    /// STATUS_SUCCESS - The Service completed successfully.
    /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.
    /// STATUS_INVALID_HANDLE - The domain handle passed is invalid.
    /// STATUS_SOME_NOT_MAPPED - Some of the names provided could not be mapped. This is a successful return.
    /// STATUS_NONE_MAPPED - No names could be mapped. This is an error return.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245712.aspx</see>
    [DllImport(SamLib, SetLastError = true)]
    private static extern NtStatus SamLookupNamesInDomain(SafeSamHandle domainHandle, uint count, UnicodeString[] names, out SafeSamPointer relativeIds, out SafeSamPointer use);

    internal static NtStatus SamLookupNamesInDomain(SafeSamHandle domainHandle, string[] names, out int[]? relativeIds, out SID_NAME_USE[]? use)
    {
        ArgumentNullException.ThrowIfNull(names);

        uint count = (uint)names.Length;
        if (count > MaxNamesToLookup)
        {
            throw new ArgumentOutOfRangeException(nameof(names), count, $"Cannot translate more than {MaxNamesToLookup} names at once.");
        }

        // Prepare parameters
        UnicodeString[] unicodeNames = new UnicodeString[count];

        for (int i = 0; i < count; i++)
        {
            unicodeNames[i] = new(names[i]);
        }

        // Call the native function
        NtStatus result = SamLookupNamesInDomain(domainHandle, count, unicodeNames, out SafeSamPointer relativeIdsPointer, out SafeSamPointer usePointer);

        if (result == NtStatus.Success)
        {
            try
            {
                relativeIds = relativeIdsPointer.MarshalAs<int>(count);
                use = usePointer.MarshalAs<int>(count)?.Select(item => (SID_NAME_USE)item)?.ToArray();
            }
            finally
            {
                // Free unmanaged memory
                relativeIdsPointer.Dispose();
                usePointer.Dispose();
            }
        }
        else
        {
            relativeIds = null;
            use = null;
        }

        return result;
    }

    internal static NtStatus SamLookupNameInDomain(SafeSamHandle domainHandle, string name, out int? relativeId, out SID_NAME_USE? sidType)
    {
        string[] names = [name];
        NtStatus result = SamLookupNamesInDomain(domainHandle, names, out int[] relativeIds, out SID_NAME_USE[] use);

        if (result == NtStatus.Success)
        {
            relativeId = relativeIds[0];
            sidType = use[0];
        }
        else
        {
            relativeId = null;
            sidType = null;
        }

        return result;
    }

    internal static NtStatus SamSetInformationUser(SafeSamHandle userHandle, ref SamUserInternal1Information passwordInformation)
    {
        return SamSetInformationUser(userHandle, SamUserInformationClass.Internal1Information, ref passwordInformation);
    }

    /// <summary>
    /// This API closes a currently opened SAM object.
    /// </summary>
    /// <param name="samHandle">Specifies the handle of a previously opened SAM object to close.</param>
    /// <returns>
    /// STATUS_SUCCESS - The object was successfully closed.
    /// STATUS_INVALID_HANDLE - The handle passed is invalid.
    /// </returns>
    /// <see>http://msdn.microsoft.com/en-us/library/cc245722.aspx</see>
    [DllImport(SamLib, SetLastError = true)]
    internal static extern NtStatus SamCloseHandle([In] IntPtr samHandle);

    [DllImport(SamLib, SetLastError = true)]
    internal static extern NtStatus SamFreeMemory([In] IntPtr buffer);
}
