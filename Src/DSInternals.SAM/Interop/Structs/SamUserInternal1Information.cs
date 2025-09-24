using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop;

/// <summary>
/// The <see cref="SamUserInternal1Information"/> structure contains the NT and LM hashed passwords for a user account.
/// </summary>
/// <remarks>
/// Corresponds to SAMPR_USER_INTERNAL1_INFORMATION in the Windows API.
/// See https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-samr/50d17755-c6b8-40bd-8cac-bd6cfa31adf2
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct SamUserInternal1Information
{
    private const int NTHashSize = 16;
    private const int LMHashSize = 16;

    /// <summary>
    /// Initializes a new instance of the <see cref="SamUserInternal1Information"/> struct.
    /// </summary>
    /// <param name="ntHash">The NT hash of the password.</param>
    /// <param name="lmHash">Optional LM hash of the password.</param>
    internal SamUserInternal1Information(byte[] ntHash, byte[] lmHash = null)
    {
        if (ntHash == null || ntHash.Length != NTHashSize)
        {
            throw new ArgumentException($"NT hash must be a byte array of length {NTHashSize}.", nameof(ntHash));
        }

        this.NtOwfPassword = ntHash;
        this.NtPasswordPresent = true;

        if (lmHash != null)
        {
            if (lmHash.Length != LMHashSize)
            {
                throw new ArgumentException($"LM hash must be a byte array of length {LMHashSize}.", nameof(lmHash));
            }

            this.LmOwfPassword = lmHash;
            this.LmPasswordPresent = true;
        }
        else
        {
            this.LmOwfPassword = null;
            this.LmPasswordPresent = false;
        }

        // Unexpire the password by default
        this.PasswordExpired = false;
    }

    /// <summary>
    /// The NT hash of the password of the user account.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = NTHashSize)]
    internal byte[] NtOwfPassword;

    /// <summary>
    /// The LM hash of the password of the user account.
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = LMHashSize)]
    internal byte[] LmOwfPassword;

    /// <summary>
    /// Indicates whether the NT hashed password is present.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    internal bool NtPasswordPresent;

    /// <summary>
    /// Indicates whether the LM hashed password is present.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    internal bool LmPasswordPresent;

    /// <summary>
    /// Indicates that the password has expired.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    internal bool PasswordExpired;
}
