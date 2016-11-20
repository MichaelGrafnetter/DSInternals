using DSInternals.Common.Interop;
using System;
using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// The SAMPR_RID_ENUMERATION structure holds the name and RID information about an account.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SamRidEnumeration
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
}
