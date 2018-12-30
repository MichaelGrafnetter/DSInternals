namespace DSInternals.SAM.Interop
{
    using DSInternals.Common.Interop;
    using System;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecurityCritical]
    internal static partial class NativeMethods
    {
        private const string NtdsApi = "ntdsapi.dll";

        /// <summary>
        /// Dses the make password credentials.
        /// </summary>
        /// <param name="user">Pointer to a null-terminated string that contains the user name to use for the credentials.</param>
        /// <param name="domain">Pointer to a null-terminated string that contains the domain that the user is a member of.</param>
        /// <param name="password">Pointer to a null-terminated string that contains the password to use for the credentials.</param>
        /// <param name="authIdentity">The authentication identity.</param>
        /// <returns></returns>
        [DllImport(NtdsApi, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern Win32ErrorCode DsMakePasswordCredentials(string user, string domain, SafeUnicodeSecureStringPointer password, out SafeRpcAuthIdentityHandle authIdentity);

        internal static Win32ErrorCode DsMakePasswordCredentials(string user, string domain, SecureString password, out SafeRpcAuthIdentityHandle authIdentity)
        {
            using (var passwordPointer = new SafeUnicodeSecureStringPointer(password))
            {
                return DsMakePasswordCredentials(user, domain, passwordPointer, out authIdentity);
            }
        }

        internal static Win32ErrorCode DsMakePasswordCredentials(NetworkCredential credential, out SafeRpcAuthIdentityHandle authIdentity)
        {
            return DsMakePasswordCredentials(credential.UserName, credential.Domain, credential.SecurePassword, out authIdentity);
        }

        /// <summary>
        /// The DsFreePasswordCredentials function frees memory allocated for a credentials structure by the DsMakePasswordCredentials function.
        /// </summary>
        /// <param name="authIdentity">Handle of the credential structure to be freed.</param>
        /// <see>https://msdn.microsoft.com/en-us/library/ms675979(v=vs.85).aspx</see>
        [DllImport(NtdsApi)]
        internal static extern void DsFreePasswordCredentials([In] IntPtr authIdentity);
    }
}