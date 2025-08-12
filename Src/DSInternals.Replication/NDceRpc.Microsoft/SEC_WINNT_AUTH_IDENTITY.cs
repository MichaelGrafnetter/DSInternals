using System;
using System.Net;
using System.Runtime.InteropServices;

namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// Handle to the structure containing the client's authentication and authorization credentials appropriate for the selected authentication and authorization service.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [System.Diagnostics.DebuggerDisplay("{Domain}\\{User}")]
    public struct SEC_WINNT_AUTH_IDENTITY
    {
        public SEC_WINNT_AUTH_IDENTITY(NetworkCredential cred)
            : this(cred.Domain, cred.UserName, cred.Password)
        {
        }

        public SEC_WINNT_AUTH_IDENTITY(string domain, string user, string password)
        {
            User = user;
            UserLength = (uint) user.Length;
            Domain = domain;
            DomainLength = (uint) domain.Length;
            Password = password;
            PasswordLength = (uint) password.Length;
            Flags = SEC_WINNT_AUTH_IDENTITY_UNICODE;
        }

        //private const uint SEC_WINNT_AUTH_IDENTITY_ANSI = 0x1;
        private const uint SEC_WINNT_AUTH_IDENTITY_UNICODE = 0x2;

        private readonly String User;
        private readonly uint UserLength;
        private readonly String Domain;
        private readonly uint DomainLength;
        private readonly String Password;
        private readonly uint PasswordLength;
        private readonly uint Flags;
    }
}