namespace DSInternals.Common.Data
{
    using System.IO;
    using System.Text;

    public abstract class DPAPIObject
    {
        /// <summary>
        /// Path to the generated Mimikatz DPAPI batch processing script.
        /// </summary>
        public const string KiwiFilePath = "kiwiscript.txt";

        /// <summary>
        /// DPAPI blob.
        /// </summary>
        public byte[] Data
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the relative path to which this blob will be saved.
        /// </summary>
        public abstract string FilePath
        {
            get;
        }

        /// <summary>
        /// Gets the Mimikatz command that would process the DPAPI blob.
        /// </summary>
        public abstract string KiwiCommand
        {
            get;
        }

        /// <summary>
        /// Saves the DPAPI blob to an appropriate file in the current directory.
        /// </summary>
        public void Save()
        {
            this.Save(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Saves the DPAPI blob to an appropriate file in the specified directory.
        /// </summary>
        /// <param name="directoryPath">Directory to save the DPAPI blob to.</param>
        public abstract void Save(string directoryPath);

        /// <summary>
        /// Appends the Mimikatz command to a text file in the current directory.
        /// </summary>
        public void SaveKiwiCommand()
        {
            this.SaveKiwiCommand(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Appends the Mimikatz command to a text file in the specified directory.
        /// </summary>
        /// <param name="directoryPath">Directory to save the text file to.</param>
        public void SaveKiwiCommand(string directoryPath)
        {
            string command = this.KiwiCommand;

            if(string.IsNullOrEmpty(command))
            {
                // Mimikatz probably does not support this DPAPI object type, so there is nothing to write to the script file
                return;
            }

            // The target directory must exist
            Validator.AssertDirectoryExists(directoryPath);

            var filePath = Path.Combine(directoryPath, KiwiFilePath);
            using (var writer = File.AppendText(filePath))
            {
                writer.WriteLine(command);
            }
        }
    }
}