using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace DSInternals.SAM
{
    public class SamDomain : SamObject
    {
        internal SamDomain(SafeSamHandle handle) : base(handle)
        {
        }

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
        public SamUser OpenUser(string name, SamUserAccessMask desiredAccess)
        {
            int rid = this.LookupUser(name);
            return this.OpenUser(rid, desiredAccess);
        }

        public  SamUser OpenUser(SecurityIdentifier sid, SamUserAccessMask desiredAccess)
        {
            int rid = sid.GetRid();
            return this.OpenUser(rid, desiredAccess);
        }

        public SamUser OpenUser(int rid, SamUserAccessMask desiredAccess)
        {
            SafeSamHandle userHandle;
            NtStatus result = NativeMethods.SamOpenUser(this.Handle, desiredAccess, rid, out userHandle);
            Validator.AssertSuccess(result);
            return new SamUser(userHandle);
        }

        public SamDomainPasswordInformation GetPasswordPolicy()
        {
            SamDomainPasswordInformation passwordInfo;
            NtStatus result = NativeMethods.SamQueryInformationDomain(this.Handle, out passwordInfo);
            Validator.AssertSuccess(result);
            return passwordInfo;
        }
    }
}
