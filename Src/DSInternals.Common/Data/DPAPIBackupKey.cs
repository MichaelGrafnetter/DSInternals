namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Principal;
    using System.Text.RegularExpressions;

    public class DPAPIBackupKey
    {
        private const int KeyVersionOffset = 0;
        private const int KeyVersionSize = sizeof(int);
        private const int RSAPrivateKeySizeOffset = KeyVersionOffset + KeyVersionSize;
        private const int RSACertificateSizeOffset = RSAPrivateKeySizeOffset + sizeof(int);
        private const int RSAPrivateKeyOffset = RSACertificateSizeOffset + sizeof(int);
        private const string BackupKeyDNFormat = "CN=BCKUPKEY_{0} Secret,CN=System,{1}";
        private const string BackupKeyDNRegex = "CN=BCKUPKEY_(.*) Secret,CN=System,.*";
        private const string PreferredLegacyKeyPointerName = "P";
        private const string PreferredRSAKeyPointerName = "PREFERRED";
        private const string TemporaryKeyContainerName = "DSInternals";
        private const string RSAKeyFileNameFormat = "ntds_capi_{0}.pfx";
        private const string LegacyKeyFileNameFormat = "ntds_legacy_{0}.key";
        private const string UnknownKeyFileNameFormat = "ntds_unknown_{0}_{1}.key";
        public DPAPIBackupKey(DirectoryObject dsObject, DirectorySecretDecryptor pek)
        {
            // Parameter validation
            Validator.AssertNotNull(dsObject, "dsObject");
            Validator.AssertNotNull(pek, "pek");
            // TODO: Test Object type

            // Decrypt the secret value
            byte[] encryptedSecret;
            dsObject.ReadAttribute(CommonDirectoryAttributes.CurrentValue, out encryptedSecret);
            this.RawKeyData = pek.DecryptSecret(encryptedSecret);

            // Parse DN to get key ID or pointer type:
            this.DistinguishedName = dsObject.DistinguishedName;
            var keyName = GetSecretNameFromDN(this.DistinguishedName);

            switch(keyName)
            {
                case null:
                    // We could not parse the DN, so exit with Unknown as the key type
                    this.Type = DPAPIBackupKeyType.Unknown;
                    break;
                case PreferredRSAKeyPointerName:
                    this.Type = DPAPIBackupKeyType.PreferredRSAKeyPointer;
                    // Interpret the raw data as Guid
                    this.KeyId = new Guid(this.RawKeyData);
                    break;
                case PreferredLegacyKeyPointerName:
                    this.Type = DPAPIBackupKeyType.PreferredLegacyKeyPointer;
                    // Interpret the raw data as Guid
                    this.KeyId = new Guid(this.RawKeyData);
                    break;
                default:
                    // Actual Key, so we parse its Guid and version
                    this.KeyId = Guid.Parse(keyName);
                    int version = BitConverter.ToInt32(this.RawKeyData, KeyVersionOffset);
                    switch(version)
                    {
                        case 1:
                            this.Type = DPAPIBackupKeyType.LegacyKey;
                            // Cut the version out of the data
                            this.RawKeyData = this.RawKeyData.Cut(KeyVersionSize);
                            break;
                        case 2:
                            this.Type = DPAPIBackupKeyType.RSAKey;
                            // Combine the certificate and key into PFX and replace the original decrypted data
                            this.RawKeyData = ConvertRSASecretToPFX(this.RawKeyData);
                            break;
                    }
                    break;
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

        public byte[] RawKeyData
        {
            get;
            private set;
        }

        public Guid KeyId
        {
            get;
            private set;
        }

        public void SaveTo(string directoryPath)
        {
            string fileName;
            switch(this.Type)
            {
                case DPAPIBackupKeyType.RSAKey:
                    fileName = String.Format(RSAKeyFileNameFormat, this.KeyId);
                    break;
                case DPAPIBackupKeyType.LegacyKey:
                    fileName = String.Format(LegacyKeyFileNameFormat, this.KeyId);
                    break;
                case DPAPIBackupKeyType.Unknown:
                    // Generate an additional random ID to prevent potential filename conflicts
                    int rnd = new Random().Next();
                    fileName = String.Format(UnknownKeyFileNameFormat, this.KeyId, rnd);
                    break;
                case DPAPIBackupKeyType.PreferredLegacyKeyPointer:
                case DPAPIBackupKeyType.PreferredRSAKeyPointer:
                default:
                    // Do not save these pointer keys
                    return;
            }
            Validator.AssertDirectoryExists(directoryPath);
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllBytes(filePath, this.RawKeyData);
        }

        public void Save()
        {
            this.SaveTo(Directory.GetCurrentDirectory());
        }

        public static string GetKeyDN(Guid keyId, string domainDN)
        {
            return String.Format(BackupKeyDNFormat, keyId, domainDN);
        }

        public static string GetPreferredRSAKeyPointerDN(string domainDN)
        {
            return String.Format(BackupKeyDNFormat, PreferredRSAKeyPointerName, domainDN);
        }

        public static string GetPreferredLegacyKeyPointerDN(string domainDN)
        {
            return String.Format(BackupKeyDNFormat, PreferredLegacyKeyPointerName, domainDN);
        }

        private static byte[] ConvertRSASecretToPFX(byte[] secret)
        {
            int privateKeySize = BitConverter.ToInt32(secret, RSAPrivateKeySizeOffset);
            int certificateSize = BitConverter.ToInt32(secret, RSACertificateSizeOffset);
            // TODO: Test that sizes match the array
            byte[] binaryPrivateKey = secret.Cut(RSAPrivateKeyOffset, privateKeySize);
            byte[] binaryCertificate = secret.Cut(RSAPrivateKeyOffset + privateKeySize, certificateSize);
            // The PFX export only works if the key is stored in a named container
            var cspParameters = new CspParameters();
            cspParameters.KeyContainerName = TemporaryKeyContainerName;
            using(var privateKey = new RSACryptoServiceProvider(cspParameters))
            {
                // Make the key temporary
                privateKey.PersistKeyInCsp = false;
                privateKey.ImportCspBlob(binaryPrivateKey);
                // Combine the private and public keys
                var certificate = new X509Certificate2(binaryCertificate);
                certificate.PrivateKey = privateKey;
                // Convert to binary PFX
                return certificate.Export(X509ContentType.Pfx);
            }
        }
        private static string GetSecretNameFromDN(string distinguishedName)
        {
            var match = Regex.Match(distinguishedName, BackupKeyDNRegex);
            bool success = match.Success && (match.Groups.Count == 2);
            return success ? match.Groups[1].Value : null;
        }
    }
}
