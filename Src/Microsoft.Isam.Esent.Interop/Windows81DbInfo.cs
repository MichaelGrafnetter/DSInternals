//-----------------------------------------------------------------------
// <copyright file="Windows81DbInfo.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows81
{
    using System;

    /// <summary>
    /// ColumnDatabase info levels that have been added to the Windows 8.1 version of ESENT.
    /// </summary>
    public static class Windows81DbInfo
    {
        /// <summary>
        /// Introduced in Windows 8.1.
        /// Returns the filesize of the database that is allocated by the operating system, in pages.
        /// This may be smaller than <see cref="JET_DbInfo.Filesize"/> if the file system supports
        /// compressed or sparse files (Int32).
        /// </summary>
        public const JET_DbInfo FilesizeOnDisk = (JET_DbInfo)21;
    }
}
