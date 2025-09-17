using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DSInternals.SAM
{
    /// <summary>
    /// Represents a user account in the Windows Security Account Manager (SAM) database.
    /// </summary>
    public class SamUser : SamObject
    {
        internal SamUser(SafeSamHandle handle) : base(handle)
        {
        }

        /// <summary>
        /// Sets the password hashes for this user account using hexadecimal string representations.
        /// </summary>
        /// <param name="ntHash">The NT hash as a hexadecimal string.</param>
        /// <param name="lmHash">The LM hash as a hexadecimal string, or null if not provided.</param>
        public void SetPasswordHash(string ntHash, string lmHash = null)
        {
            byte[] binaryNTHash = ntHash.HexToBinary();
            byte[] binaryLMHash = lmHash.HexToBinary();
            this.SetPasswordHash(binaryNTHash, binaryLMHash);
        }

        /// <summary>
        /// Sets the password hashes for this user account using byte arrays.
        /// </summary>
        /// <param name="ntHash">The NT hash as a byte array.</param>
        /// <param name="lmHash">The LM hash as a byte array, or null if not provided.</param>
        public void SetPasswordHash(byte[] ntHash, byte[] lmHash = null)
        {
            Validator.AssertNotNull(ntHash, "ntHash");
            SamUserInternal1Information passwordInfo = new SamUserInternal1Information(ntHash);
            NtStatus result = NativeMethods.SamSetInformationUser(this.Handle, ref passwordInfo);
            Validator.AssertSuccess(result);
        }
    }
}
