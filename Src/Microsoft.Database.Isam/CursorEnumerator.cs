// ---------------------------------------------------------------------------
// <copyright file="CursorEnumerator.cs" company="Microsoft">
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
    using System.Collections;

    /// <summary>
    /// Enumerates the records visible to a given cursor.
    /// </summary>
    public class CursorEnumerator : IEnumerator
    {
        /// <summary>
        /// The cursor
        /// </summary>
        private Cursor cursor;

        /// <summary>
        /// The moved
        /// </summary>
        private bool moved = false;

        /// <summary>
        /// The current
        /// </summary>
        private bool current = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CursorEnumerator"/> class.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        internal CursorEnumerator(Cursor cursor)
        {
            this.cursor = cursor;
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// after last record in cursor
        /// or
        /// before first record in cursor
        /// </exception>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public FieldCollection Current
        {
            get
            {
                if (this.current == false)
                {
                    if (this.moved == true)
                    {
                        throw new InvalidOperationException("after last record in cursor");
                    }
                    else
                    {
                        throw new InvalidOperationException("before first record in cursor");
                    }
                }

                return this.cursor.Fields;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <remarks>
        /// This is the standard version that will work with other CLR
        /// languages.
        /// </remarks>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            this.cursor.MoveBeforeFirst();
            this.moved = false;
            this.current = false;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            if (this.moved == false)
            {
                this.Reset();
            }

            this.current = this.cursor.MoveNext();
            this.moved = true;
            return this.current;
        }
    }
}
