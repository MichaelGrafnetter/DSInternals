// ---------------------------------------------------------------------------
// <copyright file="Key.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System;
    using System.Collections;

    /// <summary>
    /// This collection is used to construct keys using generic values for each
    /// key segment comprising the key.  Each key is NOT tied to any specific
    /// schema so the same key could be used with different indices.  However,
    /// the key must be compatible with the index with which it is used or else
    /// an argument exception will be thrown.  Type coercion will be performed
    /// as appropriate for each key segment when the key is used with an index
    /// based on the type of each key segment in the key versus the type of each
    /// key column in that index.  If type coercion fails, an invalid cast
    /// exception will be thrown when the key is used.
    /// </summary>
    public class Key : CollectionBase
    {
        /// <summary>
        /// The prefix
        /// </summary>
        private bool prefix = false;

        /// <summary>
        /// The wildcard
        /// </summary>
        private bool wildcard = false;

        /// <summary>
        /// Gets a key that can be used to represent the start of an index.
        /// </summary>
        public static Key Start
        {
            get
            {
                return new Key();
            }
        }

        /// <summary>
        /// Gets a key that can be used to represent the end of an index.
        /// </summary>
        public static Key End
        {
            get
            {
                return new Key();
            }
        }

        /// <summary>
        /// Gets a value indicating whether [has prefix].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has prefix]; otherwise, <c>false</c>.
        /// </value>
        internal bool HasPrefix
        {
            get
            {
                return this.prefix;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [has wildcard].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has wildcard]; otherwise, <c>false</c>.
        /// </value>
        internal bool HasWildcard
        {
            get
            {
                return this.wildcard == true || List.Count == 0;
            }
        }

        /// <summary>
        /// Returns a key containing one ordinary key segment per given value.
        /// </summary>
        /// <param name="values">the values for each key column that will be used to compose the key</param>
        /// <returns>A key containing one ordinary key segment per given value.</returns>
        /// <seealso cref="ComposeWildcard"/>
        /// <seealso cref="ComposePrefix"/>
        public static Key Compose(params object[] values)
        {
            Key key = new Key();
            foreach (object value in values)
            {
                key.Add(value);
            }

            return key;
        }

        /// <summary>
        /// Returns a key containing one ordinary key segment per given value
        /// except that the last given value becomes a prefix key segment.
        /// </summary>
        /// <param name="values">the values for each key column that will be used to compose the key</param>
        /// <returns>A key containing one ordinary key segment per given value
        /// except that the last given value becomes a prefix key segment.</returns>
        /// <seealso cref="Compose"/>
        /// <seealso cref="ComposeWildcard"/>
        public static Key ComposePrefix(params object[] values)
        {
            Key key = new Key();
            for (int i = 0; i < values.Length - 1; i++)
            {
                key.Add(values[i]);
            }

            key.AddPrefix(values[values.Length - 1]);
            return key;
        }

        /// <summary>
        /// Returns a key containing one ordinary key segment per given value
        /// and one wildcard key segment.
        /// </summary>
        /// <param name="values">the values for each key column that will be used to compose the key</param>
        /// <returns>A key containing one ordinary key segment per given value
        /// and one wildcard key segment.</returns>
        /// <seealso cref="Compose"/>
        /// <seealso cref="ComposePrefix"/>
        public static Key ComposeWildcard(params object[] values)
        {
            Key key = new Key();
            foreach (object value in values)
            {
                key.Add(value);
            }

            key.AddWildcard();
            return key;
        }

        /// <summary>
        /// Sets the next key segment of the key to the given value.
        /// </summary>
        /// <param name="value">the value for the next key column that will be used to compose the key</param>
        public void Add(object value)
        {
            List.Add(new KeySegment(value, false, false, false));
        }

        /// <summary>
        /// Sets the next key segment of the key to the given value as a
        /// prefix.  A prefix key segment will match any column value that
        /// has a prefix that matches the given value.
        /// </summary>
        /// <param name="value">the value for the next key column that will be used to compose the key</param>
        /// <remarks>
        /// Prefix key segments only make sense in the context of a variable
        /// length column.  Any other use will result in an argument exception
        /// when the key is used.
        /// <para>
        /// A key may contain only one prefix key segment.  Further, a prefix
        /// key segment may not be followed by an ordinary key segment.
        /// </para>
        /// </remarks>
        public void AddPrefix(object value)
        {
            List.Add(new KeySegment(value, true, false, false));
            this.prefix = true;
        }

        /// <summary>
        /// Sets the next key segment of the key to the given value as a
        /// wildcard.  A wildcard key segment will match any column value.
        /// </summary>
        /// <remarks>
        /// A key may contain any number of wildcard key segments.  Further, a
        /// wildcard key segment may be followed only by other wildcard key
        /// segments.  Finally, extra wildcard key segments will be ignored
        /// when the key is used.
        /// <para>
        /// A key with no key segments acts as if it contains a single wildcard
        /// key segment.
        /// </para>
        /// </remarks>
        public void AddWildcard()
        {
            // flag current last key segment as saying that the next key segment
            // will be a wildcard
            if (List.Count > 0)
            {
                KeySegment segment = (KeySegment)List[List.Count - 1];
                this.List[List.Count - 1] = new KeySegment(segment.Value, segment.Prefix, segment.Wildcard, true);
            }

            try
            {
                // add a wildcard key segment
                List.Add(new KeySegment(null, false, true, false));
                this.wildcard = true;
            }
            finally
            {
                // flag the new last key segment as saying that the next key segment
                // will not be a wildcard.  this will handle the case where the
                // insertion of the new wildcard fails
                if (List.Count > 0)
                {
                    KeySegment segment = (KeySegment)List[List.Count - 1];
                    this.List[List.Count - 1] = new KeySegment(segment.Value, segment.Prefix, segment.Wildcard, false);
                }
            }
        }

        /// <summary>
        /// Performs additional custom processes when validating a value.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <exception cref="System.ArgumentException">
        /// value must be of type KeySegment;value
        /// or
        /// value cannot be a prefix if the key already contains a prefix;value
        /// or
        /// value cannot be a prefix if the key already contains a wildcard;value
        /// or
        /// value must be a wildcard if the key already contains a prefix;value
        /// or
        /// value must be a wildcard if the key already contains a wildcard;value
        /// </exception>
        protected override void OnValidate(object value)
        {
            bool sawPrefix = false;
            bool sawWildcard = false;
            foreach (KeySegment segment in this)
            {
                sawPrefix = sawPrefix || segment.Prefix == true;
                sawWildcard = sawWildcard || segment.Wildcard == true;
            }

            if (!(value is KeySegment))
            {
                throw new ArgumentException("value must be of type KeySegment", "value");
            }

            if (((KeySegment)value).Prefix == true && sawPrefix == true)
            {
                throw new ArgumentException("value cannot be a prefix if the key already contains a prefix", "value");
            }

            if (((KeySegment)value).Prefix == true && sawWildcard == true)
            {
                throw new ArgumentException("value cannot be a prefix if the key already contains a wildcard", "value");
            }

            if (((KeySegment)value).Prefix == false && ((KeySegment)value).Wildcard == false && sawPrefix == true)
            {
                throw new ArgumentException("value must be a wildcard if the key already contains a prefix", "value");
            }

            if (((KeySegment)value).Prefix == false && ((KeySegment)value).Wildcard == false && sawWildcard == true)
            {
                throw new ArgumentException("value must be a wildcard if the key already contains a wildcard", "value");
            }
        }
    }
}
