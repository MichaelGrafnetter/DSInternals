using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Identifies the machine account whose security policy is to be queried or set.
/// </summary>
/// <remarks>
/// Corresponds to the LSAPR_POLICY_MACHINE_ACCT_INFO structure.
/// See https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lsad/e05c1489-e8c9-4b6c-8b5e-f95d5dd7b1b2
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct LsaMachineAccountInformation
{
    /// <summary>
    /// The RID of the machine account.
    /// </summary>
    internal uint Rid;

    /// <summary>
    /// The SID of the machine account.
    /// </summary>
    internal IntPtr Sid;

    [SecurityCritical]
    public SecurityIdentifier? GetSid()
    {
        return this.Sid != IntPtr.Zero ? new SecurityIdentifier(this.Sid) : null;
    }
}
