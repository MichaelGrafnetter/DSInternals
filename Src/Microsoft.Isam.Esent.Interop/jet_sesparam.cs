//-----------------------------------------------------------------------
// <copyright file="jet_sesparam.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    /// <summary>
    /// ESENT session parameters.
    /// </summary>
    /// <seealso cref="Windows10.Windows10Sesparam"/>
    public enum JET_sesparam
    {
        /// <summary>
        /// This parameter is not meant to be used. 
        /// </summary>
        Base = 4096,

        /// <summary>
        /// This parameter sets the grbits for commit.  It is functionally the same as the
        /// system parameter JET_param.CommitDefault when used with an instance and a sesid.
        /// Note: JET_param.CommitDefault is not currently exposed in the ESE interop layer.
        /// </summary>
        CommitDefault = 4097,

        /// <summary>
        /// This parameter sets a user specific commit context that will be placed in the
        /// transaction log on commit to level 0.
        /// </summary>
        CommitGenericContext = 4098,
    }
}