//-----------------------------------------------------------------------
// <copyright file="Windows10Api.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Api calls introduced in Windows 10.
    /// </summary>
    public static class Windows10Api
    {
        #region Session Parameters
        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to retrieve.</param>
        /// <param name="operationContext">An operation context to retrieve.</param>
        /// <seealso cref="JET_OPERATIONCONTEXT"/>
        public static void JetGetSessionParameter(
            JET_SESID sesid,
            JET_sesparam sesparamid,
            out JET_OPERATIONCONTEXT operationContext)
        {
            Api.Check(Api.Impl.JetGetSessionParameter(sesid, sesparamid, out operationContext));
        }

        /// <summary>
        /// Sets a parameter on the provided session state, used for the lifetime of this session or until reset.
        /// </summary>
        /// <param name="sesid">The session to set the parameter on.</param>
        /// <param name="sesparamid">The ID of the session parameter to set.</param>
        /// <param name="operationContext">An operation context to set.</param>
        /// <seealso cref="JET_OPERATIONCONTEXT"/>
        public static void JetSetSessionParameter(JET_SESID sesid, JET_sesparam sesparamid, JET_OPERATIONCONTEXT operationContext)
        {
            Api.Check(Api.Impl.JetSetSessionParameter(sesid, sesparamid, operationContext));
        }
        #endregion

        #region Sessions

        /// <summary>
        /// Retrieves performance information from the database engine for the
        /// current thread. Multiple calls can be used to collect statistics
        /// that reflect the activity of the database engine on this thread
        /// between those calls.
        /// </summary>
        /// <param name="threadstats">Returns the thread statistics data.</param>
        public static void JetGetThreadStats(out JET_THREADSTATS2 threadstats)
        {
            Api.Check(Api.Impl.JetGetThreadStats(out threadstats));
        }

        #endregion
    }
}
