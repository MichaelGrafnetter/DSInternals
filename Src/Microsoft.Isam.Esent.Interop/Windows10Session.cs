//-----------------------------------------------------------------------
// <copyright file="Windows10Session.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// An extension class to provide Values of <see cref="JET_sesparam"/> that were introduced in Windows 10.
    /// </summary>
    /// <seealso cref="JET_sesparam"/>
    public static class Windows10Session
    {
        /// <summary>
        /// Gets the current number of nested levels of transactions begun. A value of zero indicates that
        /// the session is not currently in a transaction. This parameter is read-only.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to query.</param>
        /// <returns>The current transaction level of the specified database session.</returns>
        public static int GetTransactionLevel(this Session session)
        {
            int output;
            Windows8Api.JetGetSessionParameter(session.JetSesid, Windows10Sesparam.TransactionLevel, out output);
            return output;
        }

        /// <summary>
        /// A client context of type <see cref="JET_OPERATIONCONTEXT"/> that the engine uses to track
        /// and trace operations (such as IOs).
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to query.</param>
        /// <returns>The operation context of the specified database session.</returns>
        public static JET_OPERATIONCONTEXT GetOperationContext(this Session session)
        {
            JET_OPERATIONCONTEXT output;
            Windows10Api.JetGetSessionParameter(session.JetSesid, Windows10Sesparam.OperationContext, out output);
            return output;
        }

        /// <summary>
        /// A client context of type <see cref="JET_OPERATIONCONTEXT"/> that the engine uses to track
        /// and trace operations (such as IOs).
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to query.</param>
        /// <param name="operationcontext">The operation context to set.</param>
        public static void SetOperationContext(
            this Session session,
            JET_OPERATIONCONTEXT operationcontext)
        {
            Windows10Api.JetSetSessionParameter(session.JetSesid, Windows10Sesparam.OperationContext, operationcontext);
        }

        /// <summary>
        /// A 32-bit integer ID that is logged in traces and can be used by clients to
        /// correlate ESE actions with their activity.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to query.</param>
        /// <returns>The corrlation identifer of the specified database session.</returns>
        public static int GetCorrelationID(this Session session)
        {
            int output;
            Windows8Api.JetGetSessionParameter(session.JetSesid, Windows10Sesparam.CorrelationID, out output);
            return output;
        }

        /// <summary>
        /// A 32-bit integer ID that is logged in traces and can be used by clients to
        /// correlate ESE actions with their activity.
        /// </summary>
        /// <param name="session">The <see cref="Session"/> to set.</param>
        /// <param name="correlationId">The value to set. Internally, this is a 32-bit unsigned integer.</param>
        public static void SetCorrelationID(this Session session, int correlationId)
        {
            Windows8Api.JetSetSessionParameter(session.JetSesid, Windows10Sesparam.CorrelationID, correlationId);
        }
    }
}