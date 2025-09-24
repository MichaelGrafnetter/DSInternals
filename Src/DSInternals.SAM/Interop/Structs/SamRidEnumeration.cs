using System.Runtime.InteropServices;
using DSInternals.Common.Interop;

namespace DSInternals.SAM.Interop;

/// <summary>
/// The SamRidEnumeration structure holds the name and RID information about an account.
/// </summary>
/// <remarks>
/// Corresponds to the SAMPR_RID_ENUMERATION structure.
/// See https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-samr/5c94a35a-e7f2-4675-af34-741f5a8ee1a2
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal struct SamRidEnumeration
{
    /// <summary>
    /// A RID.
    /// </summary>
    public uint RelativeId;

    /// <summary>
    /// The UTF-16 encoded name of the account that is associated with RelativeId.
    /// </summary>
    public UnicodeString Name;
}
