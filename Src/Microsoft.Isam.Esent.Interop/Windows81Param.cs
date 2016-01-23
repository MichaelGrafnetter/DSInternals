//-----------------------------------------------------------------------
// <copyright file="Windows81Param.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows81
{
    /// <summary>
    /// System parameters that were introduced in Windows 8.1.
    /// </summary>
    public static class Windows81Param
    {
        /// <summary>
        /// Whether to free space back to the OS after deleting data. This may free space
        /// in the middle of files (done in the units of database extents). This uses Sparse Files,
        /// which is available on NTFS and ReFS (not FAT). The exact method of releasing space is an
        /// implementation detail and is subject to change.
        /// </summary>
        public const JET_param EnableShrinkDatabase = (JET_param)184;
    }
}
