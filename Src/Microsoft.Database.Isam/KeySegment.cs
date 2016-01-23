// ---------------------------------------------------------------------------
// <copyright file="KeySegment.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    /// <summary>
    /// A key is composed of a collection of key segments.  Each key segment
    /// describes the value of a corresponding key column in an index.
    /// </summary>
    public class KeySegment
    {
        /// <summary>
        /// The value
        /// </summary>
        private readonly object value;

        /// <summary>
        /// The prefix
        /// </summary>
        private readonly bool prefix;

        /// <summary>
        /// The wildcard
        /// </summary>
        private readonly bool wildcard;

        /// <summary>
        /// Whether the next <see cref="KeySegment"/> is a wildcard.
        /// </summary>
        private readonly bool wildcardNext;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeySegment"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="prefix">if set to <c>true</c> [prefix].</param>
        /// <param name="wildcard">if set to <c>true</c> [wildcard].</param>
        /// <param name="wildcardNext">if set to <c>true</c> [wildcard next].</param>
        internal KeySegment(object value, bool prefix, bool wildcard, bool wildcardNext)
        {
            this.value = value;
            this.prefix = prefix;
            this.wildcard = wildcard;
            this.wildcardNext = wildcardNext;
        }

        /// <summary>
        /// Gets the value of this key segment.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value of
        /// this key segment can match any value of a corresponding key column
        /// in an index that starts with its value.
        /// </summary>
        public bool Prefix
        {
            get
            {
                return this.prefix;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value of
        /// this key segment can match any value of a corresponding key column
        /// in an index.
        /// </summary>
        public bool Wildcard
        {
            get
            {
                return this.wildcard;
            }
        }

        /// <summary>
        /// Gets whether the next <see cref="KeySegment"/> is a wildcard.
        /// </summary>
        /// <returns>
        /// Whether the next <see cref="KeySegment"/> is a wildcard.
        /// </returns>
        internal bool WildcardIsNext()
        {
            return this.wildcardNext;
        }
    }
}
