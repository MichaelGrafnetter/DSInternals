namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;

    public class DPAPIBackupKey : DPAPIObject
    {
        private const int KeyVersionOffset = 0;
        private const int KeyVersionSize = sizeof(int);
        private const int RSAPrivateKeySizeOffset = KeyVersionOffset + KeyVersionSize;
        private const int RSACertificateSizeOffset = RSAPrivateKeySizeOffset + sizeof(int);
        private const int RSAPrivateKeyOffset = RSACertificateSizeOffset + sizeof(int);
        private const string BackupKeyNameFormat = "G$BCKUPKEY_{0}";
        private const string BackupKeyDNFormat = "CN=BCKUPKEY_{0} Secret,CN=System,{1}";
        // Examples:
        // CN=BCKUPKEY_P Secret,CN=System,DC=contoso,DC=com
        // CN=BCKUPKEY_PREFERRED Secret,CN=System,DC=contoso,DC=com
        // CN=BCKUPKEY_PREFERRED Secret\0ACNF:26c8edbb-6b48-4f11-9e13-9ddbccedab5a,CN=System,DC=contoso,DC=com
        // CN=BCKUPKEY_ac9e427c-fa85-4b78-8db1-771d94c03bad Secret,CN=System,DC=contoso,DC=com
        private const string BackupKeyDNRegex = "CN=BCKUPKEY_(.+) Secret(\\\\0ACNF:[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})?,CN=System,.+";
        private const string PreferredLegacyKeyPointerName = "P";
        private const string PreferredRSAKeyPointerName = "PREFERRED";
        private const string TemporaryKeyContainerName = "DSInternals";
        private const string RSAKeyFileNameFormat = "ntds_capi_{0}.pvk";
        private const string RSACertFileNameFormat = "ntds_capi_{0}.cer";
        private const string RSAP12FileNameFormat = "ntds_capi_{0}.pfx";
        private const string LegacyKeyFileNameFormat = "ntds_legacy_{0}.key";
        private const string UnknownKeyFileNameFormat = "ntds_unknown_{0}_{1}.key";
        private const string KiwiCommandFormat = "REM Add this parameter to at least the first dpapi::masterkey command: /pvk:\"{0}\"";
        private const int PVKHeaderSize = 6 * sizeof(int);
        private const uint PVKHeaderMagic = 0xb0b5f11e;
        private const uint PVKHeaderVersion = 0;
        private const uint PVKHeaderKeySpec = 1; // = AT_KEYEXCHANGE

        public DPAPIBackupKey(DirectoryObject dsObject, DirectorySecretDecryptor pek)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");
            Validator.AssertNotNull(pek, "pek");
            // TODO: Test Object type

            // Decrypt the secret value
            byte[] encryptedSecret;
            dsObject.ReadAttribute(CommonDirectoryAttributes.CurrentValue, out encryptedSecret);
            byte[] decryptedBlob = pek.DecryptSecret(encryptedSecret);

            // Initialize properties
            this.Initialize(dsObject.DistinguishedName, decryptedBlob);
        }

        public DPAPIBackupKey(string distinguishedName, byte[] blob)
        {
            // Validate the input
            Validator.AssertNotNullOrWhiteSpace(distinguishedName, "distinguishedName");
            Validator.AssertNotNull(blob, "blob");

            this.Initialize(distinguishedName, blob);
        }

        public DPAPIBackupKey(Guid keyId, byte[] blob)
        {
            Validator.AssertNotNull(blob, "blob");
            this.KeyId = keyId;
            this.Type = GetKeyType(blob);
            this.Data = blob;
        }

        public override string FilePath
        {
            get
            {
                switch(this.Type)
                {
                    case DPAPIBackupKeyType.RSAKey:
                        // .pvk file
                        return String.Format(RSAKeyFileNameFormat, this.KeyId);
                    case DPAPIBackupKeyType.LegacyKey:
                        // .key file
                        return String.Format(LegacyKeyFileNameFormat, this.KeyId);
                    case DPAPIBackupKeyType.Unknown:
                        // Generate an additional random ID to prevent potential filename conflicts
                        int rnd = new Random().Next();
                        return String.Format(UnknownKeyFileNameFormat, this.KeyId, rnd);
                    default:
                        // Saving pointers or other domain key types to files is not supported.
                        return null;
                }
            }
        }

        public override string KiwiCommand
        {
            get
            {
                return this.Type == DPAPIBackupKeyType.RSAKey ? String.Format(KiwiCommandFormat, this.FilePath) : null;
            }
        }

        public DPAPIBackupKeyType Type
        {
            get;
            private set;
        }
        public string DistinguishedName
        {
            get;
            private set;
        }

        public Guid KeyId
        {
            get;
            private set;
        }

        public override void Save(string directoryPath)
        {
            // The target directory must exist
            Validator.AssertDirectoryExists(directoryPath);

            string fullFilePath;

            switch (this.Type)
            {
                case DPAPIBackupKeyType.RSAKey:
                    // Parse the public and private keys
                    int privateKeySize = BitConverter.ToInt32(this.Data, RSAPrivateKeySizeOffset);
                    int certificateSize = BitConverter.ToInt32(this.Data, RSACertificateSizeOffset);
                    
                    byte[] privateKey = this.Data.Cut(RSAPrivateKeyOffset, privateKeySize);
                    byte[] certificate = this.Data.Cut(RSAPrivateKeyOffset + privateKeySize, certificateSize);
                    
                    // Create PVK file
                    fullFilePath = Path.Combine(directoryPath, this.FilePath);
                    byte[] pvk = EncapsulatePvk(privateKey);
                    File.WriteAllBytes(fullFilePath, pvk);

                    // Create PFX file
                    byte[] pkcs12 = CreatePfx(certificate, privateKey);
                    var pfxFile = String.Format(RSAP12FileNameFormat, this.KeyId);
                    fullFilePath = Path.Combine(directoryPath, pfxFile);
                    File.WriteAllBytes(fullFilePath, pkcs12);

                    // Create CER file
                    var cerFile = String.Format(RSACertFileNameFormat, this.KeyId);
                    fullFilePath = Path.Combine(directoryPath, cerFile);
                    File.WriteAllBytes(fullFilePath, certificate);
                    break;
                case DPAPIBackupKeyType.LegacyKey:
                    // We create one KEY file, while cropping out the key version.
                    fullFilePath = Path.Combine(directoryPath, this.FilePath);
                    File.WriteAllBytes(fullFilePath, this.Data.Cut(KeyVersionSize));
                    break;
                case DPAPIBackupKeyType.Unknown:
                    fullFilePath = Path.Combine(directoryPath, this.FilePath);
                    File.WriteAllBytes(fullFilePath, this.Data);
                    break;
                case DPAPIBackupKeyType.PreferredLegacyKeyPointer:
                case DPAPIBackupKeyType.PreferredRSAKeyPointer:
                default:
                    // Do not save these pointer keys
                    break;
            }
        }

        /// <summary>
        /// Object initializer that is shared between multiple constructors.
        /// </summary>
        /// <param name="distinguishedName">Distinguished name of the DPAPI backup key object.</param>
        /// <param name="blob">Decrypted data blob.</param>
        private void Initialize(string distinguishedName, byte[] blob)
        {
            this.DistinguishedName = distinguishedName;
            this.Data = blob;

            // Parse DN to get key ID or pointer type:
            var keyName = GetSecretNameFromDN(distinguishedName);
            switch (keyName)
            {
                case null:
                    // We could not parse the DN, so exit with Unknown as the key type
                    this.Type = DPAPIBackupKeyType.Unknown;
                    break;
                case PreferredRSAKeyPointerName:
                    this.Type = DPAPIBackupKeyType.PreferredRSAKeyPointer;
                    // Interpret the raw data as Guid
                    this.KeyId = new Guid(blob);
                    break;
                case PreferredLegacyKeyPointerName:
                    this.Type = DPAPIBackupKeyType.PreferredLegacyKeyPointer;
                    // Interpret the raw data as Guid
                    this.KeyId = new Guid(blob);
                    break;
                default:
                    // Actual Key, so we parse its Guid and version
                    this.KeyId = Guid.Parse(keyName);
                    this.Type = GetKeyType(blob);
                    break;
            }
        }

        public static string PreferredRSAKeyName
        {
            get
            {
                return String.Format(BackupKeyNameFormat, PreferredRSAKeyPointerName);
            }
        }

        public static string PreferredLegacyKeyName
        {
            get
            {
                return String.Format(BackupKeyNameFormat, PreferredLegacyKeyPointerName);
            }
        }

        public static string GetKeyDN(Guid keyId, string domainDN)
        {
            return String.Format(BackupKeyDNFormat, keyId, domainDN);
        }

        public static string GetKeyName(Guid keyId)
        {
            return String.Format(BackupKeyNameFormat, keyId);
        }

        public static string GetPreferredRSAKeyPointerDN(string domainDN)
        {
            return String.Format(BackupKeyDNFormat, PreferredRSAKeyPointerName, domainDN);
        }

        public static string GetPreferredLegacyKeyPointerDN(string domainDN)
        {
            return String.Format(BackupKeyDNFormat, PreferredLegacyKeyPointerName, domainDN);
        }

        private static string GetSecretNameFromDN(string distinguishedName)
        {
            var match = Regex.Match(distinguishedName, BackupKeyDNRegex);
            bool success = match.Success && (match.Groups.Count >= 2);
            return success ? match.Groups[1].Value : null;
        }

        private static byte[] CreatePfx(byte[] certificate, byte[] privateKey)
        {
            // The PFX export only works if the key is stored in a named container
            var cspParameters = new CspParameters();
            cspParameters.KeyContainerName = TemporaryKeyContainerName;
            using (var keyContainer = new RSACryptoServiceProvider(cspParameters))
            {
                // Make the key temporary
                keyContainer.PersistKeyInCsp = false;
                keyContainer.ImportCspBlob(privateKey);
                // Combine the private and public keys
                var combinedCertificate = new X509Certificate2(certificate);
                combinedCertificate.PrivateKey = keyContainer;
                // Convert to binary PFX
                return combinedCertificate.Export(X509ContentType.Pfx);
            }
        }

        private static byte[] EncapsulatePvk(byte[] privateKey)
        {
            // We do a quick and dirty encapsulation of the private key into the PVK format.
            // See: http://www.drh-consultancy.demon.co.uk/pvk.html
            // TODO: Extract PVK code to a distinct class.
            int pvkSize = PVKHeaderSize + privateKey.Length;
            byte[] pvk = new byte[pvkSize];

            using (var stream = new MemoryStream(pvk, true))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Write PVK header
                    writer.Write(PVKHeaderMagic);
                    writer.Write(PVKHeaderVersion);
                    writer.Write(PVKHeaderKeySpec);
                    writer.Write((int)PrivateKeyEncryptionType.None);
                    writer.Write((int)0); // Size of salt 
                    writer.Write(privateKey.Length);

                    // Write the actual data
                    writer.Write(privateKey);
                }
            }

            return pvk;
        }

        private static DPAPIBackupKeyType GetKeyType(byte[] blob)
        {
            return (DPAPIBackupKeyType)BitConverter.ToInt32(blob, KeyVersionOffset);
        }
    }
}
