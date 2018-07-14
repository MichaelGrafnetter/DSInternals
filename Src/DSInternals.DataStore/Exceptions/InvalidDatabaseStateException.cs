using System;

namespace DSInternals.DataStore
{
    public class InvalidDatabaseStateException : Exception
    {
        private const string FilePathDataKey = "FilePath";

        public InvalidDatabaseStateException(string message, string filePath) : this(message, filePath, null)
        {
        }

        public InvalidDatabaseStateException(string message, string filePath, Exception innerException) : base(message, innerException)
        {
            this.Data[FilePathDataKey] = filePath;
        }
    }
}