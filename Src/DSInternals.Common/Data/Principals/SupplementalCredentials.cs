namespace DSInternals.Common.Data
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Properties;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;

    /// <summary>
    /// Stored credentials for use in authenticating.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245500.aspx</see>
    public class SupplementalCredentials
    {
        private const int Reserved4Size = 96;

        // Prefix := Reserved1 + Length + Reserved2 + Reserved3
        private const int PrefixSize = 2 * sizeof(int) + 2 * sizeof(short);

        // Header := Reserved1 + Length + Reserved2 + Reserved3 + Reserved4 + PropertySignature
        private const int HeaderSize = PrefixSize + Reserved4Size + sizeof(short);

        // The length goes right after Reserverd1
        private const int LengthOffset = sizeof(int);
        
        // Footer := Reserved5
        private const int FooterSize = sizeof(byte);

        // Empty structure does not contain property count
        private const int EmptyStructureLength = HeaderSize + FooterSize;

        private const short Signature = 0x50;
        private const string PropertyPackages = "Packages";
        private const string PropertyNamePrefix = "Primary:";
        private const string PropertyKerberos = PropertyNamePrefix + "Kerberos";
        private const string PropertyWDigest = PropertyNamePrefix + "WDigest";
        private const string PropertyCleartext = PropertyNamePrefix + "CLEARTEXT";
        private const string PropertyKerberosNew = PropertyNamePrefix + "Kerberos-Newer-Keys";
        private const string PropertyNTLMStrongHash = PropertyNamePrefix + "NTLM-Strong-NTOWF";

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

        protected byte Reserved5
        {
            get;
            private set;
        }

        public SupplementalCredentials(byte[] blob)
        {
            // Empty structures can be 13 and 111 bytes long, perhaps even something in between
            if (blob == null || blob.Length <= EmptyStructureLength)
            {
                // Do not continue, as there are no hashes present in this structure.
                return;
            }

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
                    if (propertySignature != Signature)
                    {
                        throw new ArgumentOutOfRangeException(Resources.UnexpectedSupplementalCredsSignatureMessage, propertySignature, "blob");
                    }

                    // Parse properties
                    int userPropertiesSize = blob.Length - HeaderSize - sizeof(short) - FooterSize;
                    this.ReadProperties(reader);

                    // This value SHOULD<20> be set to zero and MUST be ignored by the recipient.
                    // We have sometimes seen values different than 0.
                    this.Reserved5 = reader.ReadByte();
                }
            }
        }

        public SupplementalCredentials()
        {
            // We are creating an empty structure that containins no properties.
        }

        public SupplementalCredentials(SecureString password, string samAccountName, string userPrincipalName, string netBiosDomainName, string dnsDomainName)
        {
            // Input validation
            Validator.AssertNotNull(password, "password");
            Validator.AssertNotNull(samAccountName, "samAccountName");
            // Note that UPN can be NULL, as it is an optional user attribute.
            Validator.AssertNotNull(netBiosDomainName, "netBiosDomainName");
            Validator.AssertNotNull(dnsDomainName, "dnsDomainName");

            // Generate Kerberos keys
            this.Kerberos = new KerberosCredential(password, samAccountName, dnsDomainName);
            this.KerberosNew = new KerberosCredentialNew(password, samAccountName, dnsDomainName);

            // Generate WDigest hashes
            this.WDigest = WDigestHash.ComputeHash(password, userPrincipalName, samAccountName, netBiosDomainName, dnsDomainName);

            // Generate a random NTLM strong hash that is definitely not based on a password.
            this.NTLMStrongHash = NTHash.GetRandom();

            // Note that we do not want to store the password in cleartext.
        }

        public byte[] ToByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
                {
                    // Reserved1 (4 bytes): This value MUST be set to zero and MUST be ignored by the recipient.
                    writer.Write(UInt32.MinValue);

                    // Length (4 bytes): This value MUST be set to the length, in bytes, of the entire structure, starting from the Reserved4 field.
                    // We do not wkow the length of the structure yet, so we only put in a placeholder:
                    writer.Write(UInt32.MinValue);

                    // Reserved2 (2 bytes): This value MUST be set to zero and MUST be ignored by the recipient.
                    writer.Write(UInt16.MinValue);

                    // Reserved3 (2 bytes): This value MUST be set to zero and MUST be ignored by the recipient.
                    writer.Write(UInt16.MinValue);

                    // Reserved4 (96 bytes): This value MUST be ignored by the recipient and MAY<19> contain arbitrary values.
                    // Note: At least empty structures typically just contain Unicode-encoded spaces.
                    for (int i = 0; i < Reserved4Size / sizeof(char); i++)
                    {
                        writer.Write(' ');
                    }

                    // PropertySignature (2 bytes): This field MUST be the value 0x50, in little-endian byte order.
                    writer.Write(Signature);

                    // UserProperties (variable): An array of PropertyCount USER_PROPERTY elements.
                    WriteProperties(writer);

                    // Reserved5 (1 byte): This value SHOULD<20> be set to zero and MUST be ignored by the recipient.
                    writer.Write(this.Reserved5);

                    // We finally know the length of the entire structure, so we can go back and put in the right value
                    writer.Seek(LengthOffset, SeekOrigin.Begin);
                    writer.Write((int)stream.Length - (PrefixSize + FooterSize));

                    // Flush the buffer
                    return stream.ToArray();
                }
            }
        }

        private void ReadProperties(BinaryReader reader)
        {
            // The number of USER_PROPERTY elements in the UserProperties field.
            short propertyCount = reader.ReadInt16();

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
                        this.WDigest = WDigestHash.Parse(decodedPropertyValue);
                        break;
                    case PropertyPackages:
                        // A list of the credential types that are stored as properties in decryptedSecret.
                        var packages = Encoding.Unicode.GetString(decodedPropertyValue).Split(Char.MinValue);
                        break;
                    case PropertyNTLMStrongHash:
                        // This is a totally random value generated by DC on each password change, since Windows Server 2016.
                        this.NTLMStrongHash = decodedPropertyValue;
                        break;
                    default:
                        // Unknown package. This should never happen
                        break;
                }
            }
        }

        private void WriteProperties(BinaryWriter writer)
        {
            var propertyNames = new List<string>();
            var propertyValues = new List<byte[]>();

            if(this.ClearText != null)
            {
                propertyNames.Add(PropertyCleartext);
                byte[] encodedPassword = Encoding.Unicode.GetBytes(this.ClearText);
                propertyValues.Add(encodedPassword);
            }

            if(this.NTLMStrongHash != null)
            {
                propertyNames.Add(PropertyNTLMStrongHash);
                propertyValues.Add(this.NTLMStrongHash);
            }

            if (this.KerberosNew != null)
            {
                propertyNames.Add(PropertyKerberosNew);
                byte[] kerberosNewerCredentials = this.KerberosNew.ToByteArray();
                propertyValues.Add(kerberosNewerCredentials);
            }

            if (this.Kerberos != null)
            {
                propertyNames.Add(PropertyKerberos);
                byte[] kerberosCredentials = this.Kerberos.ToByteArray();
                propertyValues.Add(kerberosCredentials);
            }

            // Create a placeholder for the packages property
            propertyNames.Add(PropertyPackages);
            propertyValues.Add(null);

            if(this.WDigest != null)
            {
                propertyNames.Add(PropertyWDigest);
                byte[] encodedWDigest = WDigestHash.Encode(this.WDigest);
                propertyValues.Add(encodedWDigest);
            }

            // We now know the full list of properties, so we put it instead of the placeholder.
            var shortPropertyNames = propertyNames.Select(propertyName => propertyName.Replace(PropertyNamePrefix, String.Empty)).Where(propertyName => propertyName != PropertyPackages);
            string concatenatedPropertyNames = String.Join(Char.MinValue.ToString(), shortPropertyNames);
            byte[] encodedPropertyNames = Encoding.Unicode.GetBytes(concatenatedPropertyNames);
            int packageIndex = propertyNames.LastIndexOf(PropertyPackages);
            propertyValues[packageIndex] = encodedPropertyNames;

            // We only continue if there are any credential-related properties, not just the placeholder for their list.
            if (propertyNames.Count > 1)
            {
                // PropertyCount(2 bytes): The number of USER_PROPERTY elements in the UserProperties field. When there are zero USER_PROPERTY elements in the UserProperties field, this field MUST be omitted; the resultant USER_PROPERTIES structure has a constant size of 0x6F bytes.
                writer.Write((short)propertyNames.Count);

                // UserProperties (variable): An array of PropertyCount USER_PROPERTY elements.
                for(int i = 0; i < propertyNames.Count; i++)
                {
                    WriteProperty(writer, propertyNames[i], propertyValues[i]);
                }
            }
        }

        private static void WriteProperty(BinaryWriter writer, string name, byte[] value)
        {
            byte[] encodedName = Encoding.Unicode.GetBytes(name);
            byte[] encodedValue = Encoding.ASCII.GetBytes(value.ToHex(false));
            
            // NameLength(2 bytes): The number of bytes, in little - endian byte order, of PropertyName.The property name is located at an offset of zero bytes just following the Reserved field.
            writer.Write((short) encodedName.Length);

            // ValueLength(2 bytes): The number of bytes contained in PropertyValue.
            writer.Write((short)encodedValue.Length);

            // Reserved(2 bytes): This value MUST be ignored by the recipient and MAY < 22 > be set to arbitrary values on update.
            // In observed cases, Windows uses 2 for the packages list and 1 for other packages
            short reserved = (short)(name == PropertyPackages ? 2 : 1);
            writer.Write(reserved);

            // PropertyName(variable): The name of this property as a UTF - 16 encoded string.
            writer.Write(encodedName);

            // PropertyValue(variable): The value of this property.The value MUST be hexadecimal - encoded using an 8 - bit character size, and the values '0' through '9' inclusive and 'a' through 'f' inclusive(the specification of 'a' through 'f' is case-sensitive).
            writer.Write(encodedValue);
        }
    }
}