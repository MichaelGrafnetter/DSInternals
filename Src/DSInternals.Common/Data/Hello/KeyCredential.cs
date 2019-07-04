namespace DSInternals.Common.Data
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    ///  This class represents a single credential stored as a series of values, corresponding to the KEYCREDENTIALLINK_BLOB structure.
    /// </summary>
    /// <remarks>This structure is stored as the binary portion of the msDS-KeyCredentialLink DN-Binary attribute.</remarks>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220505.aspx</see>
    public class KeyCredential
    {
        /// <summary>
        /// Minimum length of the structure.
        /// </summary>
        private const int MinLength = sizeof(uint); // Version

        /// <summary>
        /// V0 structure alignment in bytes.
        /// </summary>
        private const ushort PackSize = 4;

        /// <summary>
        /// Defines the version of the structure.
        /// </summary>
        public KeyCredentialVersion Version
        {
            get;
            private set;
        }

        /// <summary>
        /// A SHA256 hash of the Value field of the KeyMaterial entry.
        /// </summary>
        public string Identifier
        {
            get;
            private set;
        }

        public KeyUsage Usage
        {
            get;
            private set;
        }

        public string LegacyUsage
        {
            get;
            private set;
        }

        public KeySource Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Key material of the credential.
        /// </summary>
        public byte[] KeyMaterial
        {
            get;
            private set;
        }

        public RSAParameters? NGCPublicKey
        {
            get
            {
                if (this.Usage == KeyUsage.NGC && this.KeyMaterial != null)
                {
                    // The key material is a 2048-bit RSA public key.
                    return this.KeyMaterial.ToRSAParameters();
                }
                else
                {
                    return null;
                }
            }
        }

        public string NGCModulus
        {
            get
            {
                var publicKey = this.NGCPublicKey;
                return publicKey.HasValue ? Convert.ToBase64String(publicKey.Value.Modulus) : null;
            }
        }

        public CustomKeyInformation CustomKeyInfo
        {
            get;
            private set;
        }

        public Guid? DeviceId
        {
            get;
            private set;
        }

        /// <summary>
        /// The approximate time this key was created.
        /// </summary>
        public DateTime CreationTime
        {
            get;
            private set;
        }

        /// <summary>
        /// The approximate time this key was last used.
        /// </summary>
        public DateTime? LastLogonTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Distinguished name of the AD object that holds this key credential.
        /// </summary>
        public string HolderDN
        {
            get;
            private set;
        }

        public KeyCredential(X509Certificate2 certificate, Guid deviceId, string holderDN) : this(certificate, deviceId, holderDN, DateTime.Now)
        {
        }

        public KeyCredential(X509Certificate2 certificate, Guid deviceId, string holderDN, DateTime currentTime)
        {
            Validator.AssertNotNull(certificate, nameof(certificate));

            byte[] publicKey = certificate.ExportPublicKeyBlob();
            this.Initialize(publicKey, deviceId, holderDN, currentTime);
        }

        public KeyCredential(byte[] publicKey, Guid deviceId, string holderDN) : this(publicKey, deviceId, holderDN, DateTime.Now)
        {
        }

        public KeyCredential(byte[] publicKey, Guid deviceId, string holderDN, DateTime currentTime)
        {
            Validator.AssertNotNull(publicKey, nameof(publicKey));
            this.Initialize(publicKey, deviceId, holderDN, currentTime);
        }

        private void Initialize(byte[] publicKey, Guid deviceId, string holderDN, DateTime currentTime)
        {
            // Prodess holder DN
            Validator.AssertNotNullOrEmpty(holderDN, nameof(holderDN));
            this.HolderDN = holderDN;

            // Initialize the Key Credential based on requirements stated in MS-KPP Processing Details:
            this.Version = KeyCredentialVersion.Version2;
            this.Identifier = ComputeKeyIdentifier(publicKey, this.Version);
            this.CreationTime = currentTime;
            this.LastLogonTime = currentTime;
            this.KeyMaterial = publicKey;
            this.Usage = KeyUsage.NGC;
            this.CustomKeyInfo = new CustomKeyInformation(KeyFlags.None);
            this.Source = KeySource.ActiveDirectory;
            this.DeviceId = deviceId;
        }

        public KeyCredential(byte[] blob, string holderDN)
        {
            // Input validation
            Validator.AssertNotNull(blob, nameof(blob));
            Validator.AssertMinLength(blob, MinLength, nameof(blob));
            Validator.AssertNotNullOrEmpty(holderDN, nameof(holderDN));
            
            // Init
            this.HolderDN = holderDN;

            // Parse binary input
            using (var stream = new MemoryStream(blob, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    this.Version = (KeyCredentialVersion) reader.ReadUInt32();

                    // Read all entries corresponding to the KEYCREDENTIALLINK_ENTRY structure:
                    do
                    {
                        // A 16-bit unsigned integer that specifies the length of the Value field.
                        ushort length = reader.ReadUInt16();

                        // An 8-bit unsigned integer that specifies the type of data that is stored in the Value field.
                        KeyCredentialEntryType entryType = (KeyCredentialEntryType) reader.ReadByte();

                        // A series of bytes whose size and meaning are defined by the Identifier field.
                        byte[] value = reader.ReadBytes(length);

                        if(this.Version == KeyCredentialVersion.Version0)
                        {
                            // Data used to be aligned to 4B in this legacy format.
                            int paddingLength = (PackSize - length % PackSize) % PackSize;
                            reader.ReadBytes(paddingLength);
                        }

                        // Now parse the value of the current entry based on its type:
                        switch (entryType)
                        {
                            case KeyCredentialEntryType.KeyID:
                                this.Identifier = ConvertFromBinaryIdentifier(value, this.Version);
                                break;
                            case KeyCredentialEntryType.KeyHash:
                                // We do not need to validate the integrity of the data by the hash
                                break;
                            case KeyCredentialEntryType.KeyMaterial:
                                this.KeyMaterial = value;
                                break;
                            case KeyCredentialEntryType.KeyUsage:
                                if(length == sizeof(byte))
                                {
                                    // This is apparently a V2 structure
                                    this.Usage = (KeyUsage)value[0];
                                }
                                else
                                {
                                    // This is a legacy structure that contains a string-encoded key usage instead of enum.
                                    this.LegacyUsage = System.Text.Encoding.UTF8.GetString(value);
                                }
                                break;
                            case KeyCredentialEntryType.KeySource:
                                this.Source = (KeySource)value[0];
                                break;
                            case KeyCredentialEntryType.DeviceId:
                                this.DeviceId = new Guid(value);
                                break;
                            case KeyCredentialEntryType.CustomKeyInformation:
                                this.CustomKeyInfo = new CustomKeyInformation(value);
                                break;
                            case KeyCredentialEntryType.KeyApproximateLastLogonTimeStamp:
                                this.LastLogonTime = ConvertFromBinaryTime(value, this.Source, this.Version);
                                break;
                            case KeyCredentialEntryType.KeyCreationTime:
                                this.CreationTime = ConvertFromBinaryTime(value, this.Source, this.Version);
                                break;
                            default:
                                // Unknown entry type. We will just ignore it.
                                break;
                        }
                    } while (reader.BaseStream.Position != reader.BaseStream.Length);
                }
            }
        }

        public override string ToString()
        {
            return String.Format(
                "Id: {0}, Source: {1}, Version: {2}, Usage: {3}, CreationTime: {4}",
                this.Identifier,
                this.Source,
                this.Version,
                this.Usage,
                this.CreationTime);
        }

        public byte[] ToByteArray()
        {
            // Note that we do not support the legacy V1 format.

            // Serialize properties 3-9 first, as property 2 must contain their hash:
            byte[] binaryProperties;
            using (var propertyStream = new MemoryStream())
            {
                using (var propertyWriter = new BinaryWriter(propertyStream))
                {
                    // Key Material
                    propertyWriter.Write((ushort)this.KeyMaterial.Length);
                    propertyWriter.Write((byte)KeyCredentialEntryType.KeyMaterial);
                    propertyWriter.Write(this.KeyMaterial);

                    // Key Usage
                    propertyWriter.Write((ushort)sizeof(KeyUsage));
                    propertyWriter.Write((byte)KeyCredentialEntryType.KeyUsage);
                    propertyWriter.Write((byte)this.Usage);

                    // Key Source
                    propertyWriter.Write((ushort)sizeof(KeySource));
                    propertyWriter.Write((byte)KeyCredentialEntryType.KeySource);
                    propertyWriter.Write((byte)this.Source);

                    // Device ID
                    if(this.DeviceId.HasValue)
                    {
                        byte[] binaryGuid = this.DeviceId.Value.ToByteArray();
                        propertyWriter.Write((ushort)binaryGuid.Length);
                        propertyWriter.Write((byte)KeyCredentialEntryType.DeviceId);
                        propertyWriter.Write(binaryGuid);
                    }

                    // Custom Key Information
                    if(this.CustomKeyInfo != null)
                    {
                        byte[] binaryKeyInfo = this.CustomKeyInfo.ToByteArray();
                        propertyWriter.Write((ushort)binaryKeyInfo.Length);
                        propertyWriter.Write((byte)KeyCredentialEntryType.CustomKeyInformation);
                        propertyWriter.Write(binaryKeyInfo);
                    }

                    // Last Logon Time
                    if(this.LastLogonTime.HasValue)
                    {
                        byte[] binaryLastLogonTime = ConvertToBinaryTime(this.LastLogonTime.Value, this.Source, this.Version);
                        propertyWriter.Write((ushort)binaryLastLogonTime.Length);
                        propertyWriter.Write((byte)KeyCredentialEntryType.KeyApproximateLastLogonTimeStamp);
                        propertyWriter.Write(binaryLastLogonTime);
                    }

                    // Creation Time
                    byte[] binaryCreationTime = ConvertToBinaryTime(this.CreationTime, this.Source, this.Version);
                    propertyWriter.Write((ushort)binaryCreationTime.Length);
                    propertyWriter.Write((byte)KeyCredentialEntryType.KeyCreationTime);
                    propertyWriter.Write(binaryCreationTime);
                }
                binaryProperties = propertyStream.ToArray();
            }

            using (var blobStream = new MemoryStream())
            {
                using (var blobWriter = new BinaryWriter(blobStream))
                {
                    // Version
                    blobWriter.Write((uint)this.Version);

                    // Key Identifier
                    byte[] binaryKeyId = ConvertToBinaryIdentifier(this.Identifier, this.Version);
                    blobWriter.Write((ushort)binaryKeyId.Length);
                    blobWriter.Write((byte)KeyCredentialEntryType.KeyID);
                    blobWriter.Write(binaryKeyId);

                    // Key Hash
                    byte[] keyHash = ComputeHash(binaryProperties);
                    blobWriter.Write((ushort)keyHash.Length);
                    blobWriter.Write((byte)KeyCredentialEntryType.KeyHash);
                    blobWriter.Write(keyHash);

                    // Append the remaining entries
                    blobWriter.Write(binaryProperties);
                }
                return blobStream.ToArray();
            }
        }

        public string ToDNWithBinary()
        {
            return new DNWithBinary(this.HolderDN, this.ToByteArray()).ToString();
        }

        public static KeyCredential Parse(string dnWithBinary)
        {
            Validator.AssertNotNullOrEmpty(dnWithBinary, nameof(dnWithBinary));
            var parsed = DNWithBinary.Parse(dnWithBinary);
            return new KeyCredential(parsed.Binary, parsed.DistinguishedName);
        }

        private static DateTime ConvertFromBinaryTime(byte[] binaryTime, KeySource source, KeyCredentialVersion version)
        {
            long timeStamp = BitConverter.ToInt64(binaryTime, 0);
            // AD and AAD use a different time encoding.
            switch (version)
            {
                case KeyCredentialVersion.Version0:
                    return new DateTime(timeStamp);
                case KeyCredentialVersion.Version1:
                    return DateTime.FromBinary(timeStamp);
                case KeyCredentialVersion.Version2:
                default:
                    return source == KeySource.ActiveDirectory ? DateTime.FromFileTime(timeStamp) : DateTime.FromBinary(timeStamp);
            }
        }

        private static byte[] ConvertToBinaryTime(DateTime time, KeySource source, KeyCredentialVersion version)
        {
            long timeStamp;
            switch (version)
            {
                case KeyCredentialVersion.Version0:
                    timeStamp = time.Ticks;
                    break;
                case KeyCredentialVersion.Version1:
                    timeStamp = time.ToBinary();
                    break;
                case KeyCredentialVersion.Version2:
                default:
                    timeStamp = source == KeySource.ActiveDirectory ? time.ToFileTime() : time.ToBinary();
                    break;
            }

            return BitConverter.GetBytes(timeStamp);
        }

        private static byte[] ComputeHash(byte[] data)
        {
            using (var sha256 = new SHA256Managed())
            {
                return sha256.ComputeHash(data);
            }
        }

        private static string ComputeKeyIdentifier(byte[] keyMaterial, KeyCredentialVersion version)
        {
            byte[] binaryId = ComputeHash(keyMaterial);
            return ConvertFromBinaryIdentifier(binaryId, version);
        }

        private static string ConvertFromBinaryIdentifier(byte[] binaryId, KeyCredentialVersion version)
        {
            switch (version)
            {
                case KeyCredentialVersion.Version0:
                case KeyCredentialVersion.Version1:
                    return binaryId.ToHex(true);
                case KeyCredentialVersion.Version2:
                default:
                    return Convert.ToBase64String(binaryId);
            }
        }

        private static byte[] ConvertToBinaryIdentifier(string keyIdentifier, KeyCredentialVersion version)
        {
            switch (version)
            {
                case KeyCredentialVersion.Version0:
                case KeyCredentialVersion.Version1:
                    return keyIdentifier.HexToBinary();
                case KeyCredentialVersion.Version2:
                default:
                    return Convert.FromBase64String(keyIdentifier);
            }
        }
    }
}
