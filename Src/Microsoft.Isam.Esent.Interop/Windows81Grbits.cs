//-----------------------------------------------------------------------
// <copyright file="Windows81Grbits.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows81
{
    using System;

    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Options for <see cref="Windows81Param.EnableShrinkDatabase"/>.
    /// </summary>
    [Flags]
    public enum ShrinkDatabaseGrbit
    {
        /// <summary>
        /// Does not reduce the size of the database during normal operations.
        /// </summary>
        Off = 0x0,

        /// <summary>
        /// Turns on the database shrinking functionality. If this parameter is not
        /// set, then <see cref="Windows8Api.JetResizeDatabase"/> will be unable to reclaim
        /// space to the file system.
        /// Uses the file system's Sparse Files feature to release space
        /// in the middle of a file. When enough rows or tables get free up by
        /// the Version Store Cleanup task, and space is reclaimed, the database
        /// engine will attempt to return it to the file system, via sparse files.
        /// Sparse files are currently only available on NTFS and ReFS file systems.
        /// </summary>
        On = 0x1,

        /// <summary>
        /// After space is release from a table to a the root Available Extent, the database
        /// engine will attempt to release the space back to the file system. This parameter
        /// requires that <see cref="On"/> is also specified.
        /// </summary>
        Realtime = 0x2,
    }

    /// <summary>
    /// Options that have been introduced in Windows 8.1.
    /// </summary>
    public static class Windows81Grbits
    {
        /// <summary>
        /// Only shrink the database to the desired size.
        /// If the resize call would grow the database, do nothing.
        /// In order to use this functionality, <see cref="InstanceParameters.EnableShrinkDatabase"/>
        /// must be set to <see cref="ShrinkDatabaseGrbit.On"/>. Otherwise, an exception may
        /// be thrown.
        /// </summary>
        public const ResizeDatabaseGrbit OnlyShrink = (ResizeDatabaseGrbit)0x2;
    }
}
