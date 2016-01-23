//-----------------------------------------------------------------------
// <copyright file="Windows81InstanceParameters.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using Microsoft.Isam.Esent.Interop.Windows81;

    /// <summary>
    /// This class provides static properties to set and get
    /// per-instance ESENT system parameters.
    /// </summary>
    public partial class InstanceParameters
    {
        /// <summary>
        /// Gets or sets whether to free space back to the OS after deleting data. This may free space
        /// in the middle of files (done in the units of database extents). This uses Sparse Files,
        /// which is available on NTFS and ReFS (not FAT). The exact method of releasing space is an
        /// implementation detail and is subject to change.
        /// </summary>
        public ShrinkDatabaseGrbit EnableShrinkDatabase
        {
            get
            {
                return (ShrinkDatabaseGrbit)this.GetIntegerParameter(Windows81Param.EnableShrinkDatabase);
            }

            set
            {
                this.SetIntegerParameter(Windows81Param.EnableShrinkDatabase, (int)value);
            }
        }
    }
}
