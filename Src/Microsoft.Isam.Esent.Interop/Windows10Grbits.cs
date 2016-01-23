//-----------------------------------------------------------------------
// <copyright file="Windows10Grbits.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows10
{
    using System;

    using Microsoft.Isam.Esent.Interop;
    using Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// System parameters that have been introduced in Windows 10.
    /// </summary>
    public static class Windows10Grbits
    {
        /// <summary>
        /// Do not write to the input structures, so that the structures can be stored in readonly memory.
        /// Additionally, do not return any auto-opened tableid.
        /// </summary>
        /// <seealso cref="CreateTableColumnIndexGrbit"/>
        /// <seealso cref="Api.JetCreateTableColumnIndex3"/>
        public const CreateTableColumnIndexGrbit TableCreateImmutableStructure = (CreateTableColumnIndexGrbit)0x8;

        /// <summary>
        /// Do not write to the input structures, so that the structures can be stored in readonly memory.
        /// </summary>
        /// <remarks>
        /// This was primarily introduced for the C API so that the input structures could be stored in read-only
        /// memory. It is of limited use in this managed code interop, since the input structures are generated
        /// on the fly. It is provided here for completeness.
        /// </remarks>
        /// <seealso cref="CreateIndexGrbit"/>
        /// <seealso cref="Api.JetCreateIndex2"/>
        public const CreateIndexGrbit IndexCreateImmutableStructure = (CreateIndexGrbit)0x80000;

        /// <summary>
        /// Passed back to durable commit callback to let it know that log is down (and all pending commits will not be flushed to disk).
        /// Used with <see cref="Windows8Param.DurableCommitCallback"/>.
        /// </summary>
        /// <seealso cref="DurableCommitCallbackGrbit"/>
        public const DurableCommitCallbackGrbit LogUnavailable = (DurableCommitCallbackGrbit)0x1;
    }
}
