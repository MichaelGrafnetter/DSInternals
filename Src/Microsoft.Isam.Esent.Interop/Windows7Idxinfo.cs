//-----------------------------------------------------------------------
// <copyright file="Windows7IdxInfo.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows7
{
    using Win8 = Microsoft.Isam.Esent.Interop.Windows8;

    /// <summary>
    /// Index info levels that have been added to the Windows 7 version of ESENT.
    /// </summary>
    /// <seealso cref="JET_IdxInfo"/>
    /// <seealso cref="Win8.Windows8IdxInfo"/>
    public static class Windows7IdxInfo
    {
        /// <summary>
        /// Introduced in Windows 7. Returns a <see cref="JET_INDEXCREATE"/> structure suitable
        /// for use by <see cref="Api.JetCreateIndex2"/>.
        /// </summary>
        /// <remarks>Not currently implemented in this layer.</remarks>
        internal const JET_IdxInfo CreateIndex = (JET_IdxInfo)11;

        /// <summary>
        /// Introduced in Windows 7. Returns a JET_INDEXCREATE2 structure (similar to <see cref="JET_INDEXCREATE"/> structure,
        /// but it contains a <see cref="JET_SPACEHINTS"/> member called pSpacehints).
        /// This structure is suitable
        /// for use by <see cref="Api.JetCreateIndex2"/>.
        /// </summary>
        /// <remarks>Not currently implemented in this layer.</remarks>
        internal const JET_IdxInfo CreateIndex2 = (JET_IdxInfo)12;
    }
}
