using DSInternals.Common;
using DSInternals.Common.Interop;
using DSInternals.SAM.Interop;

namespace DSInternals.SAM;

/// <summary>
/// Represents a user account in the SAM database.
/// </summary>
public sealed class SamUser : SamObject
{
    internal SamUser(SafeSamHandle handle) : base(handle)
    {
    }

    /// <summary>
    /// Sets the password hash for this user.
    /// </summary>
    /// <param name="ntHash">The NT hash.</param>
    /// <param name="lmHash">Optional LM hash.</param>
    public void SetPasswordHash(byte[] ntHash, byte[] lmHash = null)
    {
        // Validation is performed in SamUserInternal1Information
        SamUserInternal1Information passwordInfo = new(ntHash, lmHash);
        NtStatus result = NativeMethods.SamSetInformationUser(this.Handle, ref passwordInfo);
        Validator.AssertSuccess(result);
    }
}
