using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DSInternals.Common.Data.Fido
{
    /// <summary>
    /// Attested credential data is a variable-length byte array added to the authenticator 
    /// data when generating an attestation object for a given credential.
    /// <see cref="https://www.w3.org/TR/webauthn/#sec-attested-credential-data"/>
    /// </summary>
    public class AttestedCredentialData
    {
        /// <summary>
        /// The AAGUID of the authenticator. Can be used to identify the make and model of the authenticator.
        /// <see cref="https://www.w3.org/TR/webauthn/#aaguid"/>
        /// </summary>
        public Guid AaGuid
        {
            get;
            private set;
        }

        /// <summary>
        /// A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.
        /// <see cref="https://www.w3.org/TR/webauthn/#credential-id"/>
        /// </summary>
        public byte[] CredentialID
        {
            get;
            private set;
        }

        /// <summary>
        /// The credential public key encoded in COSE_Key format, as defined in 
        /// Section 7 of RFC8152, using the CTAP2 canonical CBOR encoding form.
        /// <see cref="https://www.w3.org/TR/webauthn/#credential-public-key"/>
        /// </summary>
        public CredentialPublicKey CredentialPublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Decodes attested credential data.
        /// </summary>
        public AttestedCredentialData(BinaryReader reader)
        {
            // First 16 bytes is AAGUID
            byte[] aaguidBytes = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));

            // GUID from authenticator is big endian. If we are on a little endian system, convert.
            this.AaGuid = aaguidBytes.ToGuidBigEndian();

            // Byte length of Credential ID, 16-bit unsigned big-endian integer. 
            byte[] credentialIDLenBytes = reader.ReadBytes(sizeof(UInt16));

            // Credential ID length from authenticator is big endian.  If we are on little endian system, convert.
            ushort credentialIDLen = credentialIDLenBytes.ToUInt16BigEndian();

            // Read the credential ID bytes
            this.CredentialID = reader.ReadBytes(credentialIDLen);

            // "Determining attested credential data's length, which is variable, involves determining 
            // credentialPublicKey's beginning location given the preceding credentialId's length, and 
            // then determining the credentialPublicKey's length"

            // "CBORObject.Read: This method will read from the stream until the end 
            // of the CBOR object is reached or an error occurs, whichever happens first."
            
            // Read the CBOR object from the stream
            var cpk = PeterO.Cbor.CBORObject.Read(reader.BaseStream);

            // Encode the CBOR object back to a byte array.
            this.CredentialPublicKey = new CredentialPublicKey(cpk);
        }

        public override string ToString()
        {
            return string.Format("AAGUID: {0}, CredentialID: {1}, CredentialPublicKey: {2}",
                AaGuid.ToString(),
                CredentialID.ToHex(true),
                CredentialPublicKey.ToString());
        }
    }
}
