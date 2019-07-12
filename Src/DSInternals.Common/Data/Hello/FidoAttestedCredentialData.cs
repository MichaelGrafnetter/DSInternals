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
        public Guid AaGuid { get; private set; }

        /// <summary>
        /// A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.
        /// <see cref="https://www.w3.org/TR/webauthn/#credential-id"/>
        /// </summary>
        public byte[] CredentialID { get; private set; }

        /// <summary>
        /// The credential public key encoded in COSE_Key format, as defined in 
        /// Section 7 of RFC8152, using the CTAP2 canonical CBOR encoding form.
        /// <see cref="https://www.w3.org/TR/webauthn/#credential-public-key"/>
        /// </summary>
        public CredentialPublicKey CredentialPublicKey { get; private set; }

        /// <summary>
        /// AAGUID is sent as big endian byte array, this converter is for little endian systems.
        /// </summary>
        private static Guid FromBigEndian(byte[] Aaguid)
        {
            Aaguid.SwapBytes(0, 3);
            Aaguid.SwapBytes(1, 2);
            Aaguid.SwapBytes(4, 5);
            Aaguid.SwapBytes(6, 7);

            return new Guid(Aaguid);
        }

        /// <summary>
        /// Decodes attested credential data.
        /// </summary>
        public AttestedCredentialData(BinaryReader reader)
        {
            // First 16 bytes is AAGUID
            var aaguidBytes = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));

            if (BitConverter.IsLittleEndian)
            {
                // GUID from authenticator is big endian. If we are on a little endian system, convert.
                AaGuid = FromBigEndian(aaguidBytes);
            }
            else
                AaGuid = new Guid(aaguidBytes);

            // Byte length of Credential ID, 16-bit unsigned big-endian integer. 
            var credentialIDLenBytes = reader.ReadBytes(sizeof(UInt16));

            if (BitConverter.IsLittleEndian)
            {
                // Credential ID length from authenticator is big endian.  If we are on little endian system, convert.
                Array.Reverse(credentialIDLenBytes);
            }

            // Convert the read bytes to uint16 so we know how many bytes to read for the credential ID
            var credentialIDLen = BitConverter.ToUInt16(credentialIDLenBytes, 0);

            // Read the credential ID bytes
            CredentialID = reader.ReadBytes(credentialIDLen);

            // "Determining attested credential data's length, which is variable, involves determining 
            // credentialPublicKey's beginning location given the preceding credentialId's length, and 
            // then determining the credentialPublicKey's length"

            // "CBORObject.Read: This method will read from the stream until the end 
            // of the CBOR object is reached or an error occurs, whichever happens first."
            
            // Read the CBOR object from the stream
            var cpk = PeterO.Cbor.CBORObject.Read(reader.BaseStream);

            // Encode the CBOR object back to a byte array.
            CredentialPublicKey = new CredentialPublicKey(cpk);
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
