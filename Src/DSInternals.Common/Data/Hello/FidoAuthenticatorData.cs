using System.Buffers.Binary;

namespace DSInternals.Common.Data.Fido;

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
    public ReadOnlyMemory<byte> RelyingPartyIdHash
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
    public uint SignatureCount
    {
        get;
        private set;
    }

    /// <summary>
    /// Attested credential data is a variable-length byte array added to the
    /// authenticator data when generating an attestation object for a given credential.
    /// </summary>
    public AttestedCredentialData? AttestedCredentialData
    {
        get;
        private set;
    }

    /// <summary>
    /// Optional extensions to suit particular use cases.
    /// </summary>
    /// <see cref="https://www.w3.org/TR/webauthn/#extensions"/>
    public string? Extensions
    {
        get;
        private set;
    }

    public AuthenticatorData(ReadOnlyMemory<byte> authData)
    {
        // Input validation
        Validator.AssertMinLength(authData, MinLength);

        // Read the authenticator data structure, as defined by W3C
        int currentPosition = 0;
        ReadOnlySpan<byte> authDataSpan = authData.Span;

        // rpIdHash (32B)
        this.RelyingPartyIdHash = authData.Slice(currentPosition, SHA256HashLenBytes);
        currentPosition += SHA256HashLenBytes;

        // flags (1B)
        this.Flags = (AuthenticatorFlags)authDataSpan[currentPosition];
        currentPosition += sizeof(byte);

        // signCount (4B)
        this.SignatureCount = BinaryPrimitives.ReadUInt32BigEndian(authDataSpan.Slice(currentPosition));
        currentPosition += sizeof(uint);

        // Attested credential data is only present if the AT flag is set
        if (this.Flags.HasFlag(AuthenticatorFlags.AttestationData))
        {
            // Decode attested credential data, which starts at the next byte past the minimum length of the structure.
            (this.AttestedCredentialData, int bytesRead) = AttestedCredentialData.Parse(authData.Slice(currentPosition));
            currentPosition += bytesRead;
        }

        // Extensions data is only present if the ED flag is set
        if (this.Flags.HasFlag(AuthenticatorFlags.ExtensionData))
        {
            (var map, int bytesRead) = CborMap.Parse(authData.Slice(currentPosition));
            currentPosition += bytesRead;
            this.Extensions = map.ToJson();
        }

        if (currentPosition != authData.Length)
        {
            // There should be no bytes left over after decoding all data from the structure
            throw new ArgumentException("Unexpected FIDO authenticator data format.", nameof(authData));
        }
    }
}
