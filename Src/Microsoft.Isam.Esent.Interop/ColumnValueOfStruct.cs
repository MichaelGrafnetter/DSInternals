//-----------------------------------------------------------------------
// <copyright file="ColumnValueOfStruct.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;

    /// <summary>
    /// Set a column of a struct type (e.g. <see cref="Int32"/>/<see cref="Guid"/>).
    /// </summary>
    /// <typeparam name="T">Type to set.</typeparam>
    public abstract class ColumnValueOfStruct<T> : ColumnValue where T : struct, IEquatable<T>
    {
        /// <summary>
        /// Internal value.
        /// </summary>
        private T? internalValue;

        /// <summary>
        /// Gets the last set or retrieved value of the column. The
        /// value is returned as a generic object.
        /// </summary>
        public override object ValueAsObject
        {
            get
            {
                return BoxedValueCache<T>.GetBoxedValue(this.Value);
            }
        }

        /// <summary>
        /// Gets or sets the value in the struct.
        /// </summary>
        public T? Value
        {
            get
            {
                return this.internalValue;
            }

            set
            {
                this.internalValue = value;
                this.Error = value == null ? JET_wrn.ColumnNull : JET_wrn.Success;
            }
        }

        /// <summary>
        /// Gets the byte length of a column value, which is zero if column is null, otherwise
        /// it matches the Size for this fixed-size column.
        /// </summary>
        public override int Length
        {
            get { return this.Value.HasValue ? this.Size : 0; }
        }

        /// <summary>
        /// Gets a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <summary>
        /// Make sure the retrieved data is exactly the size needed for
        /// the structure. An exception is thrown if there is a mismatch.
        /// </summary>
        /// <param name="count">The size of the retrieved data.</param>
        protected void CheckDataCount(int count)
        {
            if (this.Size != count)
            {
                throw new EsentInvalidColumnException();
            }
        }
    }
}
