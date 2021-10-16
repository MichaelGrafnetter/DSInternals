//-----------------------------------------------------------------------
// <copyright file="Caches.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System.Diagnostics;

    /// <summary>
    /// Static class containing MemoryCaches for different ESENT buffers.
    /// Use these to avoid memory allocations when the memory will be
    /// used for a brief time.
    /// </summary>
    internal static class Caches
    {
        /// <summary>
        /// The maximum key size that any version of ESENT can have for
        /// any page size. This is also the maximum bookmark size.
        /// </summary>
        private const int KeyMostMost = 2000;

        /// <summary>
        /// Reserve 1 extra space for keys made with prefix or wildcard.
        /// </summary>
        private const int LimitKeyMostMost = KeyMostMost + 1;

        /// <summary>
        /// The maximum number of buffers we want in a cache.
        /// </summary>
        private const int MaxBuffers = 16;

        /// <summary>
        /// Cached buffers for columns.
        /// </summary>
        private static readonly MemoryCache TheColumnCache = new MemoryCache(128 * 1024, MaxBuffers);

        /// <summary>
        /// Cached buffers for keys and bookmarks.
        /// </summary>
        private static readonly MemoryCache TheBookmarkCache = new MemoryCache(LimitKeyMostMost, MaxBuffers);

        /// <summary>
        /// Cached buffers for keys and bookmarks.
        /// </summary>
        private static readonly MemoryCache TheSecondaryBookmarkCache = new MemoryCache(LimitKeyMostMost, MaxBuffers);

        /// <summary>
        /// Gets the cached buffers for columns.
        /// </summary>
        public static MemoryCache ColumnCache
        {
            [DebuggerStepThrough]
            get { return TheColumnCache; }
        }

        /// <summary>
        /// Gets the cached buffers for keys and bookmarks.
        /// </summary>
        public static MemoryCache BookmarkCache
        {
            [DebuggerStepThrough]
            get { return TheBookmarkCache; }
        }

        /// <summary>
        /// Gets the cached buffers for keys and secondary bookmarks.
        /// </summary>
        public static MemoryCache SecondaryBookmarkCache
        {
            [DebuggerStepThrough]
            get
            {
                return TheSecondaryBookmarkCache;
            }
        }
    }
}
