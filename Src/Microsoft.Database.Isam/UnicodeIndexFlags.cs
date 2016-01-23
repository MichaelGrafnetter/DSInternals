// ---------------------------------------------------------------------------
// <copyright file="UnicodeIndexFlags.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System;

    /// <summary>
    /// Flags for unicode index creation
    /// </summary>
    [Flags]
    internal enum UnicodeIndexFlags
    {
        /// <summary>
        /// No options.
        /// </summary>
        None = 0,

        /// <summary>
        /// Ignore case.
        /// </summary>
        CaseInsensitive = 0x00000001, // NORM_IGNORECASE

        /// <summary>
        /// Ignore nonspacing chars.
        /// </summary>
        AccentInsensitive = 0x00000002, // NORM_IGNORENONSPACE

        /// <summary>
        /// Ignore symbols.
        /// </summary>
        IgnoreSymbols = 0x00000004, // NORM_IGNORESYMBOLS

        /// <summary>
        /// Ignore kanatype.
        /// </summary>
        KanaInsensitive = 0x00010000, // NORM_IGNOREKANATYPE

        /// <summary>
        /// Ignore width.
        /// </summary>
        WidthInsensitive = 0x00020000, // NORM_IGNOREWIDTH

        /// <summary>
        /// Treat punctuation the same as symbols.
        /// </summary>
        StringSort = 0x00001000, // SORT_STRINGSORT
    }
}
