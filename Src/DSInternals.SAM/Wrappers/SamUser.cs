using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DSInternals.SAM
{
    /// <summary>
    /// Represents a SamUser.
    /// </summary>
    public class SamUser : SamObject
    {
        internal SamUser(SafeSamHandle handle) : base(handle)
        {
        }

        /// <summary>
        /// SetPasswordHash implementation.
        /// </summary>
        public void SetPasswordHash(string ntHash, string lmHash = null)
        {
            byte[] binaryNTHash = ntHash.HexToBinary();
            byte[] binaryLMHash = lmHash.HexToBinary();
            this.SetPasswordHash(binaryNTHash, binaryLMHash);
        }

        /// <summary>
        /// SetPasswordHash implementation.
        /// </summary>
        public void SetPasswordHash(byte[] ntHash, byte[] lmHash = null)
        {
            Validator.AssertNotNull(ntHash, "ntHash");
            SamUserInternal1Information passwordInfo = new SamUserInternal1Information(ntHash);
            NtStatus result = NativeMethods.SamSetInformationUser(this.Handle, ref passwordInfo);
            Validator.AssertSuccess(result);
        }
    }
}
