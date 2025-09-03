using System;

namespace DSInternals.DataStore
{
    /// <summary>
    /// Represents a InvalidDatabaseStateException.
    /// </summary>
    public class InvalidDatabaseStateException : Exception
    {
        private const string FilePathDataKey = "FilePath";

        /// <summary>
        /// this implementation.
        /// </summary>
        public InvalidDatabaseStateException(string message, string filePath) : this(message, filePath, null)
        {
        }

        /// <summary>
        /// base implementation.
        /// </summary>
        public InvalidDatabaseStateException(string message, string filePath, Exception innerException) : base(message, innerException)
        {
            this.Data[FilePathDataKey] = filePath;
        }
    }
}