//-----------------------------------------------------------------------
// <copyright file="Windows8IdxInfo.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop.Windows8
{
    using Win7 = Microsoft.Isam.Esent.Interop.Windows7;

    /// <summary>
    /// Index info levels that have been added to the Windows 8 version of ESENT.
    /// </summary>
    /// <seealso cref="JET_IdxInfo"/>
    /// <seealso cref="Win7.Windows7IdxInfo"/>
    public static class Windows8IdxInfo
    {
        /// <summary>
        /// Introduced in Windows 8. Returns a JET_INDEXCREATE3 structure. This is similar to
        /// <see cref="JET_INDEXCREATE"/> structure,  but it contains some additional
        /// members, and it also uses a locale name in the <see cref="JET_UNICODEINDEX"/>
        /// index definition, not an LCID.
        /// The returned object is suitable for use by <see cref="Windows8Api.JetCreateIndex4"/>.
        /// </summary>
        public const JET_IdxInfo InfoCreateIndex3 = (JET_IdxInfo)13;

        /// <summary>
        /// Introduced in Windows 8. Returns the locale name of the given index.
        /// </summary>
        public const JET_IdxInfo LocaleName = (JET_IdxInfo)14;
    }
}
