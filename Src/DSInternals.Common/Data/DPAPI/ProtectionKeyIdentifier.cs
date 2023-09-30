namespace DSInternals.Common.Data
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The Protection Key Identifier data structure is used to store metadata about keys used to cryptographically wrap DPAPI-NG encryption keys and to derive managed passwords.
    /// </summary>
    /// <see>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-dnsp/98a575da-ca48-4afd-ba79-f77a8bed4e4e</see>
    public class ProtectionKeyIdentifier
    {
        private const string KdsKeyMagic = "KDSK";
        private const int ExpectedVersion = 1;
        private const int StructureHeaderLength = 9 * sizeof(int) + 16;

        public int L0KeyId
        {
            get;
            private set;
        }

        public int L1KeyId
        {
            get;
            private set;
        }

        public int L2KeyId
        {
            get;
            private set;
        }

        public Guid RootKeyId
        {
            get;
            private set;
        }

        public string DomainName
        {
            get;
            private set;
        }

        public string ForestName
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format("RootKey={0}, Cycle={1} (L0={2}, L1={3}, L2={4})",
                this.RootKeyId,
                KdsRootKey.GetRootIntervalStart(this.L0KeyId, this.L1KeyId, this.L2KeyId),
                this.L0KeyId,
                this.L1KeyId,
                this.L2KeyId
                );
        }

        public ProtectionKeyIdentifier(byte[] blob)
        {
            Validator.AssertMinLength(blob, StructureHeaderLength, nameof(blob));

            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Version must be 0x00000001
                    int version = reader.ReadInt32();
                    Validator.AssertEquals(ExpectedVersion, version, nameof(version));

                    // Magic must be 0x4B53444B
                    byte[] binaryMagic = reader.ReadBytes(sizeof(int));
                    string magic = ASCIIEncoding.ASCII.GetString(binaryMagic);
                    Validator.AssertEquals(KdsKeyMagic, magic, nameof(magic));

                    // Flags must be 0x00000000
                    int flags = reader.ReadInt32();

                    // An L0 index
                    this.L0KeyId = reader.ReadInt32();

                    // An L1 index
                    this.L1KeyId = reader.ReadInt32();

                    // An L2 index
                    this.L2KeyId = reader.ReadInt32();

                    // A root key identifier
                    byte[] binaryRootKeyId = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));
                    this.RootKeyId = new Guid(binaryRootKeyId);

                    // Variable data lengths
                    int additionalInfoLength = reader.ReadInt32();
                    int domainNameLength = reader.ReadInt32();
                    int forestNameLength = reader.ReadInt32();

                    // Validate variable data length
                    int expectedLength = StructureHeaderLength + additionalInfoLength + domainNameLength + forestNameLength;
                    Validator.AssertMinLength(blob, expectedLength, nameof(blob));

                    if (additionalInfoLength > 0)
                    {
                        // Additional info used in key derivation
                        byte[] additionalInfo = reader.ReadBytes(additionalInfoLength);
                    }

                    if(domainNameLength > 0)
                    {
                        // DNS-style name of the Active Directory domain in which this identifier was created.
                        byte[] binaryDomainName = reader.ReadBytes(domainNameLength);
                        // Trim \0
                        this.DomainName = Encoding.Unicode.GetString(binaryDomainName, 0, binaryDomainName.Length - sizeof(char));
                    }

                    if(forestNameLength > 0)
                    {
                        // DNS-style name of the Active Directory forest in which this identifier was created.
                        byte[] binaryForestName = reader.ReadBytes(forestNameLength);
                        // Trim \0
                        this.ForestName = Encoding.Unicode.GetString(binaryForestName, 0, binaryForestName.Length - sizeof(char));
                    }
                }
            }
        }
    }
}
