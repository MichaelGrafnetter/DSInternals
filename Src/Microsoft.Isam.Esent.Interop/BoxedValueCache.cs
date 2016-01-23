//-----------------------------------------------------------------------
// <copyright file="BoxedValueCache.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Isam.Esent.Interop
{
    using System;

    /// <summary>
    /// A cache for boxed values.
    /// </summary>
    /// <typeparam name="T">The type of object to cache.</typeparam>
    internal static class BoxedValueCache<T> where T : struct, IEquatable<T>
    {
        /// <summary>
        /// Number of boxed values to cache.
        /// </summary>
        private const int NumCachedBoxedValues = 257;

        /// <summary>
        /// Cached boxed values.
        /// </summary>
        private static readonly object[] BoxedValues = new object[NumCachedBoxedValues];

        /// <summary>
        /// Gets a boxed version of the value. A cached copy is used if possible.
        /// </summary>
        /// <param name="value">The value to box.</param>
        /// <returns>A boxed version of the value.</returns>
        public static object GetBoxedValue(T? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            T valueToBox = value.Value;
            int index = (valueToBox.GetHashCode() & 0x7fffffff) % NumCachedBoxedValues;
            object boxedValue = BoxedValues[index];
            if (null == boxedValue || !((T)boxedValue).Equals(valueToBox))
            {
                boxedValue = valueToBox;
                BoxedValues[index] = boxedValue;
            }

            return boxedValue;
        }
    }
}