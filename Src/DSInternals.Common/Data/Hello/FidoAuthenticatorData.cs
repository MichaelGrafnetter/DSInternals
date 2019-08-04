using System;
using System.IO;
using PeterO.Cbor;

namespace DSInternals.Common.Data.Fido
{
    public class AuthenticatorData
    {
        /// <summary>
        /// Minimum length of the authenticator data structure.
        /// <see cref="https://www.w3.org/TR/webauthn/#sec-authenticator-data"/>
        /// </summary>
        private const int MinLength = SHA256HashLenBytes + sizeof(AuthenticatorFlags) + sizeof(UInt32);

        private const int SHA256HashLenBytes = 32; // 256 bits, 8 bits per byte

        /// <summary>
        /// SHA-256 hash of the RP ID the credential is scoped to.
        /// </summary>
        public byte[] RelyingPartyIdHash
        {
            get;
            private set;
        }

        /// <summary>
        /// Flags contains information from the authenticator about the authentication 
        /// and whether or not certain data is present in the authenticator data.
        /// </summary>
        public AuthenticatorFlags Flags
        {
            get;
            private set;
        }

        /// <summary>
        /// Signature counter, 32-bit unsigned big-endian integer. 
        /// </summary>
        public uint SignatureCount {
            get;
            private set;
        }

        /// <summary>
        /// Attested credential data is a variable-length byte array added to the 
        /// authenticator data when generating an attestation object for a given credential.
        /// </summary>
        public AttestedCredentialData AttestedCredentialData
        {
            get;
            private set;
        }

        /// <summary>
        /// Optional extensions to suit particular use cases.
        /// </summary>
        /// <see cref="https://www.w3.org/TR/webauthn/#extensions"/>
        public CBORObject Extensions
        {
            get;
            private set;
        }

        public AuthenticatorData(byte[] authData)
        {
            // Input validation
            Validator.AssertNotNull(authData, nameof(authData));
            Validator.AssertMinLength(authData, MinLength, nameof(authData));

            // Input parsing
            using (var stream = new MemoryStream(authData, false))
            {
                using (var reader = new BinaryReader(stream))
                {
                    this.RelyingPartyIdHash = reader.ReadBytes(SHA256HashLenBytes);

                    this.Flags = (AuthenticatorFlags)reader.ReadByte();

                    // Sign count is provided by the authenticator as big endian, convert if we are on little endian system
                    byte[] signCountBytes = reader.ReadBytes(sizeof(UInt32));
                    this.SignatureCount = signCountBytes.ToUInt32BigEndian();

                    // Attested credential data is only present if the AT flag is set
                    if (this.Flags.HasFlag(AuthenticatorFlags.AttestationData))
                    {
                        // Decode attested credential data, which starts at the next byte past the minimum length of the structure.
                        this.AttestedCredentialData = new AttestedCredentialData(reader);
                    }

                    // Extensions data is only present if the ED flag is set
                    if (this.Flags.HasFlag(AuthenticatorFlags.ExtensionData))
                    {

                        // "CBORObject.Read: This method will read from the stream until the end 
                        // of the CBOR object is reached or an error occurs, whichever happens first."
                        //
                        // Read the CBOR object from the stream
                        this.Extensions = CBORObject.Read(reader.BaseStream);
                    }

                    if(stream.Position != stream.Length)
                    {
                        // There should be no bytes left over after decoding all data from the structure
                        throw new ArgumentException("Unexpected FIDO authenticator data format.", nameof(authData));
                    }
                }
            }
        }
    }
}
