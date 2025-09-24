using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using Windows.Win32.System.Rpc;

namespace DSInternals.Common.Interop;

/// <summary>
/// Allows you to pass a particular user name and password to the run-time library for the purpose of authentication.
/// </summary>
/// <remarks>Corresponds to the SEC_WINNT_AUTH_IDENTITY_W strucure.</remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct WindowsAuthenticationIdentity : IDisposable
{
    /// <summary>
    /// String containing the user name.
    /// </summary>
    private string _user;

    /// <summary>
    /// Number of characters in User, excluding the terminating NULL.
    /// </summary>
    private uint _userLength;

    /// <summary>
    /// String containing the domain or workgroup name.
    /// </summary>
    private string _domain;

    /// <summary>
    /// Number of characters in Domain, excluding the terminating NULL.
    /// </summary>
    private uint _domainLength;

    /// <summary>
    /// String containing the user's password in the domain or workgroup.
    /// </summary>
    private SafeUnicodeSecureStringPointer _password;

    /// <summary>
    /// Number of characters in Password, excluding the terminating NULL.
    /// </summary>
    private uint _passwordLength;

    /// <summary>
    /// Flags used to specify ANSI or UNICODE.
    /// </summary>
    /// <remarks>
    /// This structure will always use Unicode strings.
    /// </remarks>
    private readonly SEC_WINNT_AUTH_IDENTITY _flags = SEC_WINNT_AUTH_IDENTITY.SEC_WINNT_AUTH_IDENTITY_UNICODE;

    /// <summary>
    /// User name.
    /// </summary>
    public string User
    {
        get
        {
            return _user;
        }
        set
        {
            _user = value;
            _userLength = value == null ? 0 : (uint)value.Length;
        }
    }

    /// <summary>
    /// Domain name.
    /// </summary>
    public string Domain
    {
        get
        {
            return _domain;
        }
        set
        {
            _domain = value;
            _domainLength = value == null ? 0 : (uint)value.Length;
        }
    }

    /// <summary>
    /// User's password.
    /// </summary>
    public SecureString Password
    {
        set
        {
            // Release any pre-existing password pointer
            _password?.Dispose();

            _password = new SafeUnicodeSecureStringPointer(value);
            _passwordLength = (uint)(value?.Length ?? 0);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsAuthenticationIdentity"/> struct.
    /// </summary>
    public WindowsAuthenticationIdentity(NetworkCredential credential)
    {
        if (credential != null)
        {
            User = credential.UserName;
            Domain = credential.Domain;
            Password = credential.SecurePassword;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsAuthenticationIdentity"/> struct.
    /// </summary>
    public WindowsAuthenticationIdentity(string domain, string user, SecureString password)
    {
        User = user;
        Domain = domain;
        Password = password;
    }

    public void Dispose()
    {
        if (this._password != null)
        {
            // Remove the clear text password from memory.
            this._password.Dispose();
            this._password = null;
        }
    }
}
