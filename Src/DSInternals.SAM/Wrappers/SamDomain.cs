using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace DSInternals.SAM
{
    /// <summary>
    /// Represents a domain in the Windows Security Account Manager (SAM) database.
    /// </summary>
    public class SamDomain : SamObject
    {
        internal SamDomain(SafeSamHandle handle) : base(handle)
        {
        }

        /// <summary>
        /// Looks up a user account by name and returns the relative identifier (RID).
        /// </summary>
        /// <param name="name">The name of the user to look up.</param>
        /// <returns>The relative identifier (RID) of the user account.</returns>
        public int LookupUser(string name)
        {
            int rid;
            SamSidType sidType;
            NtStatus result = NativeMethods.SamLookupNameInDomain(this.Handle, name, out rid, out sidType);
            // Test the returned SAM object type:
            switch(sidType)
            {
                case SamSidType.Computer:
                case SamSidType.User:
                    // OK, account found
                    break;
                case SamSidType.Group:
                case SamSidType.Domain:
                case SamSidType.Alias:
                case SamSidType.Label:
                case SamSidType.WellKnownGroup:
                    throw new Win32Exception("The requested SAM object is not an account.");
                default:
                    // Process error code:
                    Validator.AssertSuccess(result);
                    break;
            }
            return rid;
        }
        /// <summary>
        /// Opens a user account by name for the specified access rights.
        /// </summary>
        /// <param name="name">The name of the user to open.</param>
        /// <param name="desiredAccess">The access rights to request on the user account.</param>
        /// <returns>A SamUser object representing the opened user account.</returns>
        public SamUser OpenUser(string name, SamUserAccessMask desiredAccess)
        {
            int rid = this.LookupUser(name);
            return this.OpenUser(rid, desiredAccess);
        }

        /// <summary>
        /// Opens a user account by security identifier (SID) for the specified access rights.
        /// </summary>
        /// <param name="sid">The security identifier (SID) of the user to open.</param>
        /// <param name="desiredAccess">The access rights to request on the user account.</param>
        /// <returns>A SamUser object representing the opened user account.</returns>
        public  SamUser OpenUser(SecurityIdentifier sid, SamUserAccessMask desiredAccess)
        {
            int rid = sid.GetRid();
            return this.OpenUser(rid, desiredAccess);
        }

        /// <summary>
        /// Opens a user account by relative identifier (RID) for the specified access rights.
        /// </summary>
        /// <param name="rid">The relative identifier (RID) of the user to open.</param>
        /// <param name="desiredAccess">The access rights to request on the user account.</param>
        /// <returns>A SamUser object representing the opened user account.</returns>
        public SamUser OpenUser(int rid, SamUserAccessMask desiredAccess)
        {
            SafeSamHandle userHandle;
            NtStatus result = NativeMethods.SamOpenUser(this.Handle, desiredAccess, rid, out userHandle);
            Validator.AssertSuccess(result);
            return new SamUser(userHandle);
        }

        /// <summary>
        /// Retrieves the password policy settings for this domain.
        /// </summary>
        /// <returns>A SamDomainPasswordInformation structure containing the domain's password policy.</returns>
        public SamDomainPasswordInformation GetPasswordPolicy()
        {
            SamDomainPasswordInformation passwordInfo;
            NtStatus result = NativeMethods.SamQueryInformationDomain(this.Handle, out passwordInfo);
            Validator.AssertSuccess(result);
            return passwordInfo;
        }
    }
}
