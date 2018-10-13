namespace DSInternals.SAM.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Identifies the machine account whose security policy is to be queried or set.
    /// </summary>
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
    }
}