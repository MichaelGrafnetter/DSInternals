//-----------------------------------------------------------------------
// <copyright file="LegacyFileNames.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Vista
{
    /// <summary>
    /// Options for LegacyFileNames.
    /// </summary>
    public enum LegacyFileNames
    {
        /// <summary>
        /// When this option is present then the database engine will use the following naming conventions for its files:
        ///   o Transaction Log files will use .LOG for their file extension.
        ///   o Checkpoint files will use .CHK for their file extension.
        /// </summary>
        ESE98FileNames = 0x00000001,

        /// <summary>
        /// Preserve the 8.3 naming syntax for as long as possible. (this should not be changed, w/o ensuring there are no log files).
        /// </summary>
        EightDotThreeSoftCompat = 0x00000002,
    }
}
