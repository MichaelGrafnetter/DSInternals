namespace DSInternals.Common.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // TODO: Rename HashEqualityComparer to ByteArrayEqualityComparer
    /// <summary>
    /// Represents a HashEqualityComparer.
    /// </summary>
    public class HashEqualityComparer : IEqualityComparer<byte[]>
    {
        // Singleton
        private static HashEqualityComparer instance;

        /// <summary>
        /// GetInstance implementation.
        /// </summary>
        public static HashEqualityComparer GetInstance()
        {
            if(instance == null)
            {
                 instance = new HashEqualityComparer();
            }
            return instance;
        }

        private HashEqualityComparer() {}

        /// <summary>
        /// Equals implementation.
        /// </summary>
        public bool Equals(byte[] x, byte[] y)
        {
            if (x == null || y == null)
            {
                return x == y;
            }
            if(x.LongLength != y.LongLength)
            {
                return false;
            }
            return x.SequenceEqual(y);
        }

        /// <summary>
        /// GetHashCode implementation.
        /// </summary>
        public int GetHashCode(byte[] obj)
        {
            if(obj == null || obj.LongLength == 0)
            {
                return 0;
            }
            if(obj.LongLength >= sizeof(int))
            {
                return BitConverter.ToInt32(obj, 0);
            }
            else if(obj.LongLength >= sizeof(short))
            {
                // Length == 2 || Length == 3
                return BitConverter.ToInt16(obj, 0);
            }
            else
            {
                // Length == 1, so we return the value of the only byte.
                return obj[0];
            }
        }
    }
}