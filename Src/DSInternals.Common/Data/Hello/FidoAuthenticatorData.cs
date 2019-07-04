using System;
using System.IO;
using System.Linq;

namespace DSInternals.Common.Data.Fido
{
    public class AuthenticatorData
    {
        /// <summary>
        /// Minimum length of the authenticator data structure.
        /// <see cref="https://www.w3.org/TR/webauthn/#sec-authenticator-data"/>
        /// </summary>
        private const int MinLength = 37;

        private const int SHA256HashLenBytes = 32; // 256 bits, 8 bits per byte
        /// <summary>
        /// SHA-256 hash of the RP ID the credential is scoped to.
        /// </summary>
        public byte[] RpIdHash;

        /// <summary>
        /// Flags contains information from the authenticator about the authentication 
        /// and whether or not certain data is present in the authenticator data.
        /// </summary>
        public AuthenticatorFlags Flags;

        /// <summary>
        /// Signature counter, 32-bit unsigned big-endian integer. 
        /// </summary>
        public uint SignCount;

        /// <summary>
        /// Attested credential data is a variable-length byte array added to the 
        /// authenticator data when generating an attestation object for a given credential.
        /// </summary>
        public AttestedCredentialData AttestedCredentialData;

        /// <summary>
        /// Optional extensions to suit particular use cases.
        /// </summary>
        public Extensions Extensions;

        public AuthenticatorData(byte[] authData)
        {
            // Input validation
            Validator.AssertNotNull(authData, "authData");
            Validator.AssertMinLength(authData, MinLength, "authData");

            // Input parsing
            using (var stream = new MemoryStream(authData, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    RpIdHash = reader.ReadBytes(SHA256HashLenBytes);

                    Flags = (AuthenticatorFlags)reader.ReadByte();

                    var signCountBytes = reader.ReadBytes(sizeof(UInt32));
                    if (BitConverter.IsLittleEndian)
                    {
                        // Sign count is provided by the authenticator as big endian, convert if we are on little endian system
                        signCountBytes = signCountBytes.Reverse().ToArray();
                    }
                    SignCount = BitConverter.ToUInt32(signCountBytes, 0);

                    // Attested credential data is only present if the AT flag is set
                    if (Flags.HasFlag(AuthenticatorFlags.AT))
                    {
                        // Decode attested credential data, which starts at the next byte past the minimum length of the structure.
                        AttestedCredentialData = new AttestedCredentialData(reader);
                    }

                    // Extensions data is only present if the ED flag is set
                    if (Flags.HasFlag(AuthenticatorFlags.ED))
                    {

                        // "CBORObject.Read: This method will read from the stream until the end 
                        // of the CBOR object is reached or an error occurs, whichever happens first."
                        //
                        // Read the CBOR object from the stream
                        var ext = PeterO.Cbor.CBORObject.Read(reader.BaseStream);

                        // Encode the CBOR object back to a byte array.
                        Extensions = new Extensions(ext.EncodeToBytes(new PeterO.Cbor.CBOREncodeOptions(false, false, true)));
                    }
                    // There should be no bytes left over after decoding all data from the structure
                    Validator.Equals(stream.Position, stream.Length);
                }
            }
        }
    }
}
