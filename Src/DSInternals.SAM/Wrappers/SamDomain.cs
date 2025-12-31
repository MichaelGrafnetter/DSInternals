using System.ComponentModel;
using System.Security.Principal;
using DSInternals.Common;
using DSInternals.Common.Exceptions;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using Windows.Win32.Security;

namespace DSInternals.SAM;

/// <summary>
/// Represents a domain in the SAM database.
/// </summary>
public sealed class SamDomain : SamObject
{
    internal SamDomain(SafeSamHandle handle) : base(handle)
    {
    }

    /// <summary>
    /// Looks up a user by name and returns its RID.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="throwIfNotFound">If true, an exception is thrown when the user is not found. If false, null is returned when the user is not found.</param>
    /// <returns>The RID of the user.</returns>
    /// <exception cref="Win32Exception">Thrown when the requested SAM object is not an account.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null or empty.</exception>
    public int? LookupUser(string name, bool throwIfNotFound = true)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        NtStatus result = NativeMethods.SamLookupNameInDomain(this.Handle, name, out int? rid, out SID_NAME_USE? sidType);

        if (result == NtStatus.NoneMapped && throwIfNotFound == false)
        {
            return null;
        }

        // Process error code
        Validator.AssertSuccess(result);

        // Test the returned SAM object type:
        switch (sidType.Value)
        {
            case SID_NAME_USE.SidTypeComputer:
            case SID_NAME_USE.SidTypeUser:
                // OK, account found
                break;
            default:
                // Principal type mismatch
                if (throwIfNotFound)
                {
                    throw new DirectoryObjectNotFoundException(name);
                }
                break;
        }

        return rid;
    }

    /// <summary>
    /// Opens a user by name.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="desiredAccess">The desired access rights.</param>
    /// <returns>The user account.</returns>
    public SamUser OpenUser(string name, SamUserAccessMask desiredAccess = SamUserAccessMask.MaximumAllowed)
    {
        int? rid = this.LookupUser(name, throwIfNotFound: true);
        return this.OpenUser(rid.Value, desiredAccess);
    }

    /// <summary>
    /// Opens a user by its SID.
    /// </summary>
    /// <param name="sid">The SID of the user.</param>
    /// <param name="desiredAccess">The desired access rights.</param>
    /// <returns>The user account.</returns>
    public SamUser OpenUser(SecurityIdentifier sid, SamUserAccessMask desiredAccess = SamUserAccessMask.MaximumAllowed)
    {
        ArgumentNullException.ThrowIfNull(sid);

        int rid = sid.GetRid();
        return this.OpenUser(rid, desiredAccess);
    }

    /// <summary>
    /// Opens a user by its RID.
    /// </summary>
    /// <param name="rid">The RID of the user.</param>
    /// <param name="desiredAccess">The desired access rights.</param>
    /// <returns>The user account.</returns>
    public SamUser OpenUser(int rid, SamUserAccessMask desiredAccess = SamUserAccessMask.MaximumAllowed)
    {
        NtStatus result = NativeMethods.SamOpenUser(this.Handle, desiredAccess, rid, out SafeSamHandle userHandle);
        Validator.AssertSuccess(result);
        return new SamUser(userHandle);
    }

    /// <summary>
    /// Retrieves the domain password policy.
    /// </summary>
    /// <returns>The domain password policy.</returns>
    public SamDomainPasswordInformation GetPasswordPolicy()
    {
        NtStatus result = NativeMethods.SamQueryInformationDomain(this.Handle, out SamDomainPasswordInformation? passwordInfo);
        Validator.AssertSuccess(result);
        return passwordInfo.Value;
    }
}
