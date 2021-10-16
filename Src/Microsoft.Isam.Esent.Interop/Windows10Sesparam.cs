//-----------------------------------------------------------------------
// <copyright file="Windows10Sesparam.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Values of <see cref="JET_sesparam"/> that were introduced in Windows 10.
    /// </summary>
    /// <seealso cref="JET_sesparam"/>
    public static class Windows10Sesparam
    {
        /// <summary>
        /// Gets the current number of nested levels of transactions begun. A value of zero indicates that
        /// the session is not currently in a transaction. This parameter is read-only.
        /// </summary>
        public const JET_sesparam TransactionLevel = (JET_sesparam)4099;

        /// <summary>
        /// A client context of type <see cref="JET_OPERATIONCONTEXT"/> that the engine uses to track and trace operations (such as IOs).
        /// </summary>
        public const JET_sesparam OperationContext = (JET_sesparam)4100;

        /// <summary>
        /// A 32-bit integer ID that is logged in traces and can be used by clients to correlate ESE actions with their activity.
        /// </summary>
        public const JET_sesparam CorrelationID = (JET_sesparam)4101;
    }
}