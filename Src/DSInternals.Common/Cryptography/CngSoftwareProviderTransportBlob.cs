using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Represents the Microsoft Software Key Storage Provider private key blob (NCRYPT_OPAQUETRANSPORT_BLOB / KSP_TRANSPORT_OPAQUE_BLOB).
    /// </summary>
    public class CngSoftwareProviderTransportBlob
    {
        private const int MasterKeyFileCount = 8;
        private const int BlobHeaderLength = (MasterKeyFileCount + 9) * sizeof(int);
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
            Validator.AssertMinLength(blob, BlobHeaderLength, nameof(blob));

            // Parse the blob
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream, Encoding.ASCII))
                {
                    string actualMagic = Encoding.ASCII.GetString(reader.ReadBytes(Magic.Length));
                    Validator.AssertEquals(actualMagic, Magic, nameof(blob));

                    int containerNameLength = reader.ReadInt32();
                    int fileNameLength = reader.ReadInt32();
                    int privateKeyLength = reader.ReadInt32();
                    int legacyFileNameLength = reader.ReadInt32();
                    int legacyPrivateKeyLength = reader.ReadInt32();

                    int[] masterKeyFileLengths = new int[MasterKeyFileCount];
                    for(int i = 0; i < MasterKeyFileCount; i++)
                    {
                        masterKeyFileLengths[i] = reader.ReadInt32();
                    }

                    int reserved1 = reader.ReadInt32();
                    int reserved2 = reader.ReadInt32();
                    int flags = reader.ReadInt32();

                    // Validate the exact size of the structure
                    int expectedStructSize = BlobHeaderLength +
                                             containerNameLength * sizeof(char) +
                                             fileNameLength +
                                             privateKeyLength +
                                             legacyFileNameLength +
                                             legacyPrivateKeyLength;
                    for (int i = 0; i < MasterKeyFileCount; i++)
                    {
                        if(masterKeyFileLengths[i] > 0)
                        {
                            expectedStructSize += masterKeyFileLengths[i] + Marshal.SizeOf(typeof(Guid));
                        }
                    }
                    Validator.AssertLength(blob, expectedStructSize, nameof(blob));

                    // Read variable length data
                    if (containerNameLength > 0)
                    {
                        // Read Unicode string
                        this.KeyContainerName = Encoding.Unicode.GetString(reader.ReadBytes(containerNameLength * sizeof(char)));
                    }

                    if(fileNameLength > 0)
                    {
                        // Read ANSI string
                        string fileName = new string(reader.ReadChars(fileNameLength));
                    }

                    if(privateKeyLength > 0)
                    {
                        this.KeyData = reader.ReadBytes(privateKeyLength);
                    }

                    // TODO: Expose all the additional data as properties
                    if (legacyFileNameLength > 0)
                    {
                        // Read ANSI string
                        string legacyFileName = new string(reader.ReadChars(fileNameLength));
                    }

                    if(legacyPrivateKeyLength > 0)
                    {
                        byte[] legacyPrivateKey = reader.ReadBytes(legacyPrivateKeyLength);
                    }

                    for (int i = 0; i < MasterKeyFileCount; i++)
                    {
                        if(masterKeyFileLengths[i] > 0)
                        {
                            // Read master key GUID
                            byte[] masterKeyGuidBytes = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));
                            // TODO: Is endianness conversion needed?
                            Guid masterKeyGuid = new Guid(masterKeyGuidBytes);

                            // Read master key data
                            byte[] masterKey = reader.ReadBytes(masterKeyFileLengths[i]);
                        }
                    }
                }
            }
        }
    }
}
