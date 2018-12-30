namespace DSInternals.SAM.Interop
{
    using DSInternals.Common;
    using DSInternals.Common.Interop;
    using System;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Security.Principal;

    internal static partial class NativeMethods
    {
        private const string SamLib = "samlib.dll";
        private const string SamSrv = "samsrv.dll";
        private const string spnPrefix = "cifs/";

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
        /// <param name="objectAttributes">Pointer to the set of object attributes to use for this connection.  Only the security Quality Of Service information is used and should provide SecurityIdentification level of impersonation.</param>
        /// <returns>
        /// STATUS_SUCCESS - The Service completed successfully.
        /// STATUS_ACCESS_DENIED - Access was denied.
        /// </returns>
        /// <see>http://msdn.microsoft.com/en-us/library/cc245753.aspx</see>
        [DllImport(SamLib, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern NtStatus SamConnect([In] ref UnicodeString serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, IntPtr objectAttributes);
        internal static NtStatus SamConnect(string serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask)
        {
            IntPtr objectAttributes = IntPtr.Zero;
            UnicodeString unicodeServerName = new UnicodeString(serverName);
            return SamConnect(ref unicodeServerName, out serverHandle, accessMask, objectAttributes);
        }

        [DllImport(SamLib, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern NtStatus SamConnectWithCreds([In] ref UnicodeString serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, IntPtr objectAttributes, SafeRpcAuthIdentityHandle authIdentity, [MarshalAs(UnmanagedType.LPWStr)] string servicePrincipalName, out uint unknown);

        internal static NtStatus SamConnectWithCreds(string serverName, out SafeSamHandle serverHandle, SamServerAccessMask accessMask, SafeRpcAuthIdentityHandle authIdentity)
        {
            uint unknown = 0;
            IntPtr objectAttributes = IntPtr.Zero;
            UnicodeString unicodeServerName = new UnicodeString(serverName);
            string servicePrincipalName = spnPrefix + serverName;
            NtStatus result = SamConnectWithCreds(ref unicodeServerName, out serverHandle, accessMask, objectAttributes, authIdentity, servicePrincipalName, out unknown);
            if (result == NtStatus.RpcUnknownAuthenticationService)
            {
                // Try it again, but without the SPN
                servicePrincipalName = null;
                return SamConnectWithCreds(ref unicodeServerName, out serverHandle, accessMask, objectAttributes, authIdentity, servicePrincipalName, out unknown);
            }
            else
            {
                return result;
            }
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
        internal static extern NtStatus SamEnumerateDomainsInSamServer(SafeSamHandle serverHandle, ref uint enumerationContext, out SafeSamEnumerationBufferPointer buffer, uint preferedMaximumLength, out uint countReturned);

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
        internal static NtStatus SamQueryInformationDomain(SafeSamHandle domainHandle, out SamDomainPasswordInformation domainPasswordInformation)
        {
            SafeSamPointer buffer;
            NtStatus result = SamQueryInformationDomain(domainHandle, SamDomainInformationClass.PasswordInformation, out buffer);
            domainPasswordInformation = buffer != null ? Marshal.PtrToStructure<SamDomainPasswordInformation>(buffer.DangerousGetHandle()) : new SamDomainPasswordInformation();
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
        private static extern NtStatus SamSetInformationUser(SafeSamHandle userHandle, SamUserInformationClass userInformationClass, [In] ref SamUserInternal1Information buffer);

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
        private static extern NtStatus SamLookupDomainInSamServer(SafeSamHandle serverHandle, [In] ref UnicodeString domainName, [MarshalAs(UnmanagedType.LPArray, SizeConst = 24)] out byte[] domainId);

        internal static NtStatus SamLookupDomainInSamServer(SafeSamHandle serverHandle, string domainName, out SecurityIdentifier domainSid)
        {
            UnicodeString unicodeDomainName = new UnicodeString(domainName);
            byte[] domainIdBinary;
            NtStatus result = SamLookupDomainInSamServer(serverHandle, ref unicodeDomainName, out domainIdBinary);
            if (domainIdBinary != null)
            {
                domainSid = new SecurityIdentifier(domainIdBinary, 0);
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
        /// <param name="use">Recieves a pointer to an array of Count SID_NAME_USE entries that have been filled in with what significance each name has.  The nth entry in this array indicates the meaning of the nth name passed.  This buffer must be freed when no longer needed using SamFreeMemory().</param>
        /// <returns>
        /// STATUS_SUCCESS - The Service completed successfully.
        /// STATUS_ACCESS_DENIED - Caller does not have the appropriate access to complete the operation.    
        /// STATUS_INVALID_HANDLE - The domain handle passed is invalid.
        /// STATUS_SOME_NOT_MAPPED - Some of the names provided could not be mapped. This is a successful return.
        /// STATUS_NONE_MAPPED - No names could be mapped. This is an error return.
        /// </returns>
        /// <see>http://msdn.microsoft.com/en-us/library/cc245712.aspx</see>
        [DllImport(SamLib, SetLastError = true)]
        private static extern NtStatus SamLookupNamesInDomain(SafeSamHandle domainHandle, int count, UnicodeString[] names, out SafeSamPointer relativeIds, out SafeSamPointer use);

        internal static NtStatus SamLookupNamesInDomain(SafeSamHandle domainHandle, string[] names, out int[] relativeIds, out SamSidType[] use)
        {
            Validator.AssertNotNull(names, "names");
            int count = names.Length;
            if (count > MaxNamesToLookup)
            {
                // TODO: Extract as resource
                throw new ArgumentOutOfRangeException("names", count, "Cannot translate more than 1000 names at once.");
            }

            // Prepare parameters
            SafeSamPointer relativeIdsPointer;
            SafeSamPointer usePointer;
            UnicodeString[] unicodeNames = new UnicodeString[count];
            for (int i = 0; i < count; i++)
            {
                unicodeNames[i] = new UnicodeString(names[i]);
            }

            // Call the native function
            NtStatus result = SamLookupNamesInDomain(domainHandle, count, unicodeNames, out relativeIdsPointer, out usePointer);

            if (result == NtStatus.Success)
            {
                // Marshal pointers into arrays
                relativeIds = new int[count];
                use = new SamSidType[count];
                Marshal.Copy(relativeIdsPointer.DangerousGetHandle(), relativeIds, 0, count);
                Marshal.Copy(usePointer.DangerousGetHandle(), (int[])(object)use, 0, count);
            }
            else
            {
                relativeIds = null;
                use = null;
            }

            return result;
        }

        internal static NtStatus SamLookupNameInDomain(SafeSamHandle domainHandle, string name, out int relativeId, out SamSidType sidType)
        {
            string[] names = new string[] { name };
            int[] relativeIds;
            SamSidType[] use;
            NtStatus result = SamLookupNamesInDomain(domainHandle, names, out relativeIds, out use);
            if (result == NtStatus.Success)
            {
                relativeId = relativeIds[0];
                sidType = use[0];
            }
            else
            {
                relativeId = -1;
                sidType = SamSidType.Unknown;
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
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(SamLib, SetLastError = true)]
        internal static extern NtStatus SamCloseHandle([In] IntPtr samHandle);

        [DllImport(SamLib, SetLastError = true)]
        internal static extern NtStatus SamFreeMemory([In] IntPtr buffer);

        [DllImport(SamSrv, SetLastError = true)]
        internal static extern NtStatus SamIFree_SAMPR_ENUMERATION_BUFFER([In] IntPtr buffer);
    }
}