using System;

namespace DSInternals.DataStore
{
    public class InvalidDatabaseStateException : Exception
    {
        private const string FilePathDataKey = "FilePath";

        public InvalidDatabaseStateException(string message, string filePath) : base(message)
        {
            this.Data[FilePathDataKey] = filePath;
        }
    }
}
