//-----------------------------------------------------------------------
// <copyright file="Windows10IJetApi.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Implementation
{
    using Microsoft.Isam.Esent.Interop.Windows10;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// This interface describes all the Windows10 methods which have a
    /// P/Invoke implementation. Concrete instances of this interface provide
    /// methods that call ESENT.
    /// </summary>
    internal partial interface IJetApi
    {
        #region Session Parameters
        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to retrieve.</param>
        /// <param name="operationContext">An operation context to retrieve.</param>
        /// <seealso cref="JET_OPERATIONCONTEXT"/>
        /// <returns>An error code.</returns>
        int JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            out JET_OPERATIONCONTEXT operationContext);

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="operationContext">An operation context to set.</param>
        /// <returns>An error code.</returns>
        int JetSetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            JET_OPERATIONCONTEXT operationContext);

        #endregion

        #region Sessions

        /// <summary>
        /// Retrieves performance information from the database engine for the
        /// current thread. Multiple calls can be used to collect statistics
        /// that reflect the activity of the database engine on this thread
        /// between those calls. 
        /// </summary>
        /// <param name="threadstats">
        /// Returns the thread statistics.
        /// </param>
        /// <returns>An error code if the operation fails.</returns>
        int JetGetThreadStats(out JET_THREADSTATS2 threadstats);

        #endregion
    }
}
