// ---------------------------------------------------------------------------
// <copyright file="TempTableHandleEnumerator.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------
// <summary>
// </summary>
// ---------------------------------------------------------------------

namespace Microsoft.Database.Isam
{
    using System.Collections;

    /// <summary>
    /// A class used to enumerate all the table handles in this database.
    /// </summary>
    internal class TempTableHandleEnumerator : IEnumerator
    {
        /// <summary>
        /// The enumerator
        /// </summary>
        private readonly IDictionaryEnumerator enumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TempTableHandleEnumerator"/> class.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        internal TempTableHandleEnumerator(IDictionaryEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        /// <summary>
        /// Finalizes an instance of the TempTableHandleEnumerator class
        /// </summary>
        ~TempTableHandleEnumerator()
        {
            this.Reset();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        /// <returns>The current element in the collection.</returns>
        /// <remarks>
        /// This is the type safe version that may not work in other CLR
        /// languages.
        /// </remarks>
        public TempTableHandle Current
        {
            get
            {
                return (TempTableHandle)this.enumerator.Value;
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
            this.enumerator.Reset();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            return this.enumerator.MoveNext();
        }
    }
}
