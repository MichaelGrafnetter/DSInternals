//-----------------------------------------------------------------------
// <copyright file="OnlineMaintenanceHelpers.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Microsoft.Isam.Esent.Interop.Implementation;

    /// <summary>
    /// Helper methods for the ESENT API. These methods deal with database
    /// meta-data.
    /// </summary>
    public static partial class Api
    {
        /// <summary>
        /// Starts and stops database defragmentation tasks that improves data
        /// organization within a database.
        /// </summary>
        /// <param name="sesid">The session to use for the call.</param>
        /// <param name="dbid">The database to be defragmented.</param>
        /// <param name="tableName">
        /// Under some options defragmentation is performed for the entire database described by the given 
        /// database ID, and other options (such as <see cref="Windows7.Windows7Grbits.DefragmentBTree"/>) require
        /// the name of the table to defragment.
        /// </param>
        /// <param name="grbit">Defragmentation options.</param>
        /// <returns>A warning code.</returns>
        /// <seealso cref="Api.JetDefragment"/>
        public static JET_wrn Defragment(
            JET_SESID sesid,
            JET_DBID dbid,
            string tableName,
            DefragGrbit grbit)
        {
            return Api.Check(Impl.Defragment(sesid, dbid, tableName, grbit));
        }
    }
}
