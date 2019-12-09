using System.IO;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Represents the Microsoft Software Key Storage Provider private key blob (NCRYPT_OPAQUETRANSPORT_BLOB).
    /// </summary>
    public class CngSoftwareProviderTransportBlob
    {
        private const int BlobMinLength = 4 * sizeof(byte) + 4 * sizeof(int) + 48 + 2;
        private const string Magic = "MIB1";

        public string KeyContainerName
        {
            get;
            private set;
        }

        public byte[] KeyData
        {
            get;
            private set;
        }

        public CngSoftwareProviderTransportBlob(byte[] blob)
        {
            Validator.AssertNotNull(blob, nameof(blob));
            Validator.AssertMinLength(blob, BlobMinLength, nameof(blob));

            // Header: | "MIB1" | Container Name Length | Unknown | Private Key Length | 60B | Container Name | Actual CNG Private Key Blob | Unknown
            // Parse the blob
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream, Encoding.Unicode))
                {
                    var actualMagic = reader.ReadBytes(Magic.Length);
                    // TODO: Validate magic

                    int nameLength = reader.ReadInt32();
                    int unknown1 = reader.ReadInt32();
                    int privateKeyLength = reader.ReadInt32();
                    byte[] unknown2 = reader.ReadBytes(52);
                    this.KeyContainerName = reader.ReadChars(nameLength).ToString();
                    this.KeyData = reader.ReadBytes(privateKeyLength);
                    // TODO: What is the rest of the data structure?
                }
            }
        }
    }
}
