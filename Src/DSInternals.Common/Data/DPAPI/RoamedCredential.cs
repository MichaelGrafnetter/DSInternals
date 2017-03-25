namespace DSInternals.Common.Data
{
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Text;

    public class RoamedCredential : DPAPIObject
    {
        private const int MinLength = 132;
        private const int IdentifierMaxSize = 93;
        private const int HashSize = 20;

        public RoamedCredential(byte[] blob, string accountName, SecurityIdentifier accountSid)
        {
            // Validate the input
            Validator.AssertNotNull(blob, "blob");
            Validator.AssertMinLength(blob, MinLength, "blob");
            Validator.AssertNotNull(accountName, "accountName");
            Validator.AssertNotNull(accountSid, "accountSid");

            this.AccountName = accountName;
            this.AccountSid = accountSid;

            // Parse the blob
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    // The 1st byte is always '%'
                    char c1 = reader.ReadChar();
                    // TODO: Validate %

                    // The 2nd char encodes the type of the roamed credential
                    this.Type = (RoamedCredentialType)(reader.ReadChar() - '0');

                    // The 3rd char is always '\\'
                    char c3 = reader.ReadChar();
                    // TODO: Validate \

                    // Now comes the identifier padded with zeros
                    var sb = new StringBuilder(IdentifierMaxSize);
                    for(int i = 0; i < IdentifierMaxSize; i++)
                    {
                        char currentChar = reader.ReadChar();
                        if(currentChar != '\0')
                        {
                            sb.Append(currentChar);
                        }
                        else
                        {
                            // We have reached the end of the string, so we skip the padding
                            stream.Seek(IdentifierMaxSize - i - 1, SeekOrigin.Current);
                            break;
                        }
                    }
                    this.Id = sb.ToString();

                    // Time of the last modification
                    long modifiedFileTime = reader.ReadInt64();
                    this.ModifiedTime = DateTime.FromFileTime(modifiedFileTime);

                    // Flags
                    this.Flags = (RoamedCredentialFlags)reader.ReadInt16();

                    // Size of additional data, typically 4B
                    short providerDataSize = reader.ReadInt16();

                    // SHA1 hash of the stored data
                    byte[] hash = reader.ReadBytes(HashSize);

                    // Data size
                    int dataSize = reader.ReadInt32();

                    // The actual roamed data
                    this.Data = reader.ReadBytes(dataSize);

                    if(providerDataSize > 0)
                    {
                        byte[] providerData = reader.ReadBytes(providerDataSize);
                    }
                }
            }
        }

        public RoamedCredentialType Type
        {
            get;
            private set;
        }

        public RoamedCredentialFlags Flags
        {
            get;
            private set;
        }

        public string Id
        {
            get;
            private set;
        }

        public DateTime ModifiedTime
        {
            get;
            private set;
        }

        public string AccountName
        {
            get;
            private set;
        }

        public SecurityIdentifier AccountSid
        {
            get;
            private set;
        }

        public override void SaveTo(string directoryPath)
        {
            // The target directory must exist
            Validator.AssertDirectoryExists(directoryPath);

            string fullFilePath = Path.Combine(directoryPath, this.FileName);

            // Some blobs need to be saved in a specific folder structure that we have to create first
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath));

            // Finally, we can save the file
            File.WriteAllBytes(fullFilePath, this.Data);
        }

        /// <summary>
        /// Gets the path to the credential file.
        /// </summary>
        public string FileName
        {
            get
            {
                var sb = new StringBuilder();

                // Common base path in user's profile (we are skipping AppData\Roaming\Microsoft\)
                sb.AppendFormat(@"{0}\", this.AccountName);

                // Select the type-specific folder
                switch(this.Type)
                {
                    case RoamedCredentialType.DPAPIMasterKey:
                        sb.AppendFormat(@"Protect\{0}\", this.AccountSid);
                        break;
                    case RoamedCredentialType.CryptoApiCertificate:
                    case RoamedCredentialType.CNGCertificate:
                        sb.Append(@"SystemCertificates\My\Certificates\");
                        break;
                    case RoamedCredentialType.CryptoApiRequest:
                    case RoamedCredentialType.CNGRequest:
                        sb.Append(@"SystemCertificates\Request\Certificates\");
                        break;
                    case RoamedCredentialType.RSAPrivateKey:
                        sb.AppendFormat(@"Crypto\RSA\{0}\", this.AccountSid);
                        break;
                    case RoamedCredentialType.DSAPrivateKey:
                        sb.AppendFormat(@"Crypto\DSS\{0}\", this.AccountSid);
                        break;
                    case RoamedCredentialType.CNGPrivateKey:
                        sb.Append(@"Crypto\Keys\");
                        break;
                    default:
                        // Unknown type
                        break;
                }

                // The identifier of the blob is also its file name
                sb.Append(this.Id);

                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.Type, this.FileName);
        }
    }
}