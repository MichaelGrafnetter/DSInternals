namespace DSInternals.Common.Cryptography
{
    using System;
    using System.IO;
    using System.Text;

    public class SortedFileSearcher : IDisposable
    {
        /// <summary>
        /// Size of the file read buffer. We are using the smallest possible value.
        /// </summary>
        private const int BufferSize = 128;

        private StreamReader reader;

        public SortedFileSearcher(string filePath)
        {
            Validator.AssertNotNullOrWhiteSpace(filePath, nameof(filePath));
            Validator.AssertFileExists(filePath);

            this.reader = new StreamReader(filePath, Encoding.ASCII, false, BufferSize);
        }

        public SortedFileSearcher(Stream inputStream)
        {
            Validator.AssertNotNull(inputStream, nameof(inputStream));
            this.reader = new StreamReader(inputStream, Encoding.ASCII, true, BufferSize, true);
        }

        public bool FindString(string query)
        {
            Validator.AssertNotNullOrWhiteSpace(query, nameof(query));

            // Span the entire file
            long leftBound = 0;
            long rightBound = this.reader.BaseStream.Length - 1;

            while (leftBound <= rightBound)
            {
                // Seek to the middle of the interval
                long middlePosition = (leftBound + rightBound) / 2;
                this.reader.BaseStream.Seek(middlePosition, SeekOrigin.Begin);
                this.reader.DiscardBufferedData();

                // Seek the beginning of a new line
                this.SeekLineBeginning();
                long lineBeginningPosition = this.reader.BaseStream.Position;

                // Read the current line
                string line = this.reader.ReadLine();

                if(line == null)
                {
                    // We have reached the end of the file, so the search is over
                    break;
                }

                switch (String.Compare(line, 0, query, 0, query.Length, true))
                {
                    case -1:
                        // Continue with the right half
                        leftBound = lineBeginningPosition + line.Length + 1;
                        break;
                    case 1:
                        // Continue with the left half
                        rightBound = lineBeginningPosition - 2;
                        break;
                    case 0:
                    default:
                        // We have found a match
                        return true;
                }
            }

            // We have not found anything
            return false;
        }

        private void SeekLineBeginning()
        {
            var stream = this.reader.BaseStream;

            while (true)
            {
                if (stream.Position == 0)
                {
                    // We are at the beginning of the file, so there is no need to seek further.
                    break;
                }

                int c = stream.ReadByte();

                if (c == (int)'\n')
                {
                    // We have just arrived to the beginning of a new line.
                    break;
                }

                // Rewind the character we just read and one more
                stream.Seek(-2, SeekOrigin.Current);
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (this.reader != null && disposing)
            {
                this.reader.Dispose();
                this.reader = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
