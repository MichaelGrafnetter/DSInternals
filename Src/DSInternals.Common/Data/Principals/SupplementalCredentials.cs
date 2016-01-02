namespace DSInternals.Common.Data
{
    using DSInternals.Common.Properties;
    using System;
    using System.IO;
    using System.Text;

    // https://msdn.microsoft.com/en-us/library/cc245500.aspx
    public class SupplementalCredentials
    {
        private const int Reserved4Size = 96;
        // Prefix := Reserved1 + Length + Reserved2 + Reserved3
        private const int PrefixSize = 2 * sizeof(int) + 2 * sizeof(short);
        // Header := Reserved1 + Length + Reserved2 + Reserved3 + Reserved4 + PropertySignature
        private const int HeaderSize = PrefixSize + Reserved4Size + 2 * sizeof(short);
        // Footer := Reserved5
        private const int FooterSize = sizeof(byte);
        private const int EmptyStructureLength = PrefixSize + Reserved4Size + sizeof(short) + FooterSize;
        private const string PropertyPackages = "Packages";
        private const string PropertyKerberos = "Primary:Kerberos";
        private const string PropertyWDigest = "Primary:WDigest";
        private const string PropertyCleartext = "Primary:CLEARTEXT";
        private const string PropertyKerberosNew = "Primary:Kerberos-Newer-Keys";
        private const string PropertyNTLMStrongHash = "Primary:NTLM-Strong-NTOWF";
        public KerberosCredential Kerberos
        {
            get;
            private set;
        }
        public KerberosCredentialNew KerberosNew
        {
            get;
            private set;
        }
        public byte[][] WDigest
        {
            get;
            private set;
        }
        public string ClearText
        {
            get;
            private set;
        }

        /// <remarks>
        /// New in Windows Server 2016 TP4.
        /// </remarks>
        public byte[] NTLMStrongHash
        {
            get;
            private set;
        }

        public SupplementalCredentials(byte[] blob)
        {
            if(blob == null || blob.Length <= EmptyStructureLength)
            {
                // Do not continue, as there are no hashes present in this structure.
                return;
            }
            // TODO: Better parsing and validation of SupplementalCredentials
            // Empty structures can be 13 and 111 bytes long, perhaps even something in between
            // Validator.AssertMinLength(blob, EmptyStructureLength, "blob");
            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // This value MUST be set to zero and MUST be ignored by the recipient.
                    int reserved1 = reader.ReadInt32();
                    // This value MUST be set to the length, in bytes, of the entire structure, starting from the Reserved4 field.
                    int length = reader.ReadInt32();
                    int expectedLength = blob.Length - (PrefixSize + FooterSize);
                    if(length != expectedLength)
                    {
                        throw new ArgumentOutOfRangeException("blob", length, Resources.UnexpectedSupplementalCredsLengthMessage);
                    }
                    // This value MUST be set to zero and MUST be ignored by the recipient.
                    short reserved2 = reader.ReadInt16();
                    // This value MUST be set to zero and MUST be ignored by the recipient.
                    short reserved3 = reader.ReadInt16();
                    //  This value MUST be ignored by the recipient and MAY<19> contain arbitrary values. 
                    byte[] reserved4 = reader.ReadBytes(Reserved4Size);
                    // This field MUST be the value 0x50, in little-endian byte order.
                    short propertySignature = reader.ReadInt16();
                    if (propertySignature != 0x50)
                    {
                        throw new ArgumentOutOfRangeException(Resources.UnexpectedSupplementalCredsSignatureMessage, propertySignature, "blob");
                    }
                    // The number of USER_PROPERTY elements in the UserProperties field.
                    short propertyCount = reader.ReadInt16();
                    int userPropertiesSize = blob.Length - HeaderSize - sizeof(byte);
                    for (int i = 0; i < propertyCount; i++)
                    {
                        // The number of bytes, in little-endian byte order, of PropertyName. 
                        short nameLength = reader.ReadInt16();
                        // The number of bytes contained in PropertyValue.
                        short valueLength = reader.ReadInt16();
                        //  This value MUST be ignored by the recipient and MAY<21> be set to arbitrary values on update.
                        short reserved = reader.ReadInt16();
                        //  The name of this property as a UTF-16 encoded string.
                        byte[] binaryPropertyName = reader.ReadBytes(nameLength);
                        //  The value of this property. The value MUST be hexadecimal-encoded using an 8-bit character size, and the values '0' through '9' inclusive and 'a' through 'f' inclusive (the specification of 'a' through 'f' is case-sensitive).
                        byte[] binaryPropertyValue = reader.ReadBytes(valueLength);
                        string propertyName = Encoding.Unicode.GetString(binaryPropertyName);
                        string hexPropertyValue = Encoding.ASCII.GetString(binaryPropertyValue);
                        byte[] decodedPropertyValue = hexPropertyValue.HexToBinary();
                        switch (propertyName)
                        {
                            case PropertyCleartext:
                                // The cleartext password.
                                this.ClearText = Encoding.Unicode.GetString(decodedPropertyValue);
                                break;
                            case PropertyKerberos:
                                // Cryptographic hashes of the cleartext password for the Kerberos authentication protocol.
                                this.Kerberos = new KerberosCredential(decodedPropertyValue);
                                break;
                            case PropertyKerberosNew:
                                // Cryptographic hashes of the cleartext password for the Kerberos authentication protocol.
                                this.KerberosNew = new KerberosCredentialNew(decodedPropertyValue);
                                break;
                            case PropertyWDigest:
                                // Cryptographic hashes of the cleartext password for the Digest authentication protocol.
                                this.WDigest = ReadWDiget(decodedPropertyValue);
                                break;
                            case PropertyPackages:
                                // A list of the credential types that are stored as properties in decryptedSecret.
                                // We can safely ignore them.
                                break;
                            case PropertyNTLMStrongHash:
                                // This is a new thing in Windows Server 2016 and it is currently not clear how to interpret the value.
                                this.NTLMStrongHash = decodedPropertyValue;
                                break;
                            default:
                                // Unknown package. This should never happen
                                break;
                        }
                    }
                    // This value SHOULD<20> be set to zero and MUST be ignored by the recipient.
                    byte reserved5 = reader.ReadByte();
                }
            }
        }
        private static byte[][] ReadWDiget(byte[] blob)
        {
            using (Stream stream = new MemoryStream(blob))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // This value MUST be ignored by the recipient and MAY<22> be set to arbitrary values upon an update to the decryptedSecret attribute.
                    byte reserved1 = reader.ReadByte();
                    // This value MUST be ignored by the recipient and MUST be set to zero.
                    byte reserved2 = reader.ReadByte();
                    // This value MUST be set to 1.
                    byte version = reader.ReadByte();
                    // This value MUST be set to 29 because there are 29 hashes in the array.
                    byte numberOfHashes = reader.ReadByte();
                    // This value MUST be ignored by the recipient and MUST be set to zero. 
                    byte[] reserved3 = reader.ReadBytes(12);
                    // Process hashes:
                    byte[][] hashes = new byte[numberOfHashes][];
                    for(int i = 0; i < numberOfHashes; i++)
                    {
                        hashes[i] = reader.ReadBytes(16);
                    }
                    return hashes;
                }
            }
        }
    }
}
