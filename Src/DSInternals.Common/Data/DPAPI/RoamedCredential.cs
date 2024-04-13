namespace DSInternals.Common.Data
{
    using System;
    using System.IO;
    using System.Security.Principal;
    using System.Text;
    using DSInternals.Common.Cryptography;

    public class RoamedCredential : DPAPIObject
    {
        private const string MasterKeyCommandFormat = "dpapi::masterkey /in:\"{0}\" /sid:{1}";
        private const string CapiKeyCommandFormat = "dpapi::capi /in:\"{0}\"";
        private const string CNGKeyCommandFormat = "dpapi::cng /in:\"{0}\"";
        private const string CertificateCommandFormat = "crypto::system /file:\"{0}\" /export";
        private const string CurrentMasterKeyPointerId = "Preferred";
        private const string CapiRSAKeyDirectoryFormat = @"Crypto\RSA\{0}\";
        private const string CapiDSAKeyDirectoryFormat = @"Crypto\DSS\{0}\";
        private const string CngKeyDirectory = @"Crypto\Keys\";
        private const string CertificateDirectory = @"SystemCertificates\My\Certificates\";
        private const string CertificateRequestDirectory = @"SystemCertificates\Request\Certificates\";
        private const int MinLength = 132;
        private const int IdentifierMaxSize = 93;
        private const int HashSize = 20;

        public RoamedCredential(byte[] blob, string accountName, SecurityIdentifier accountSid)
        {
            // Validate the input
            Validator.AssertNotNull(blob, nameof(blob));
            Validator.AssertMinLength(blob, MinLength, nameof(blob));
            Validator.AssertNotNull(accountName, nameof(accountName));
            Validator.AssertNotNull(accountSid, nameof(accountSid));

            this.AccountName = accountName;
            this.AccountSid = accountSid;

            // Parse the blob
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    // The 1st byte is always '%'
                    char c1 = reader.ReadChar();
                    Validator.AssertEquals('%', c1, nameof(blob));

                    // The 2nd char encodes the type of the roamed credential
                    this.Type = (RoamedCredentialType)(reader.ReadChar() - '0');

                    // The 3rd char is always '\\'
                    char c3 = reader.ReadChar();
                    Validator.AssertEquals('\\', c3, nameof(blob));

                    // Now comes the identifier padded with zeros
                    var sb = new StringBuilder(IdentifierMaxSize);
                    for(int i = 0; i < IdentifierMaxSize; i++)
                    {
                        char currentChar = reader.ReadChar();
                        if(currentChar != Char.MinValue)
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

                    // Note: The data structure might be corrupted and Data.Length can be less than dataSize.
                    if (this.Type == RoamedCredentialType.CNGPrivateKey && this.Data.Length > 0)
                    {
                        // Remove Software KSP NCRYPT_OPAQUETRANSPORT_BLOB header
                        this.Data = new CngSoftwareProviderTransportBlob(this.Data).KeyData;
                    }

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

        public override void Save(string directoryPath)
        {
            // The target directory must exist
            Validator.AssertDirectoryExists(directoryPath);

            string fullFilePath = Path.Combine(directoryPath, this.FilePath);

            // Some blobs need to be saved in a specific folder structure that we have to create first
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath));

            // Finally, we can save the file
            File.WriteAllBytes(fullFilePath, this.Data);
        }

        /// <summary>
        /// Gets the path to the credential file.
        /// </summary>
        public override string FilePath
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
                        sb.Append(CertificateDirectory);
                        break;
                    case RoamedCredentialType.CryptoApiRequest:
                    case RoamedCredentialType.CNGRequest:
                        sb.Append(CertificateRequestDirectory);
                        break;
                    case RoamedCredentialType.RSAPrivateKey:
                        sb.AppendFormat(CapiRSAKeyDirectoryFormat, this.AccountSid);
                        break;
                    case RoamedCredentialType.DSAPrivateKey:
                        sb.AppendFormat(CapiDSAKeyDirectoryFormat, this.AccountSid);
                        break;
                    case RoamedCredentialType.CNGPrivateKey:
                        sb.Append(CngKeyDirectory);
                        break;
                    default:
                        // Unknown type
                        break;
                }

                // The identifier of the blob is also its file name. Any invalid characters must be remved first. 
                string fileName = string.Concat(this.Id.Split(Path.GetInvalidFileNameChars()));
                sb.Append(fileName);

                return sb.ToString();
            }
        }

        public override string KiwiCommand
        {
            get
            {
                switch(this.Type)
                {
                    case RoamedCredentialType.DPAPIMasterKey:
                        return this.Id != CurrentMasterKeyPointerId ? String.Format(MasterKeyCommandFormat, this.FilePath, this.AccountSid) : null;
                    case RoamedCredentialType.CNGCertificate:
                    case RoamedCredentialType.CNGRequest:
                    case RoamedCredentialType.CryptoApiCertificate:
                    case RoamedCredentialType.CryptoApiRequest:
                        return String.Format(CertificateCommandFormat, this.FilePath);
                    case RoamedCredentialType.RSAPrivateKey:
                    case RoamedCredentialType.DSAPrivateKey:
                        return String.Format(CapiKeyCommandFormat, this.FilePath);
                    case RoamedCredentialType.CNGPrivateKey:
                        return String.Format(CNGKeyCommandFormat, this.FilePath);
                    default:
                        // Unknown/future credential type
                        return null;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.Type, this.FilePath);
        }
    }
}
