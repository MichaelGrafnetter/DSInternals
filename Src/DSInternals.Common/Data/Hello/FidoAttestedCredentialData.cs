using System.Buffers.Binary;

namespace DSInternals.Common.Data.Fido;

/// <summary>
/// Attested credential data is a variable-length byte array added to the authenticator
/// data when generating an attestation object for a given credential.
/// <see cref="https://www.w3.org/TR/webauthn/#sec-attested-credential-data"/>
/// </summary>
public sealed class AttestedCredentialData : IDisposable
{
    private const int MinStructSize = 16 + sizeof(ushort) + 1;

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
    public ReadOnlyMemory<byte> CredentialID
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

    public AttestedCredentialData(Guid aaGuid, ReadOnlyMemory<byte> credentialId, CredentialPublicKey publicKey)
    {
        this.AaGuid = aaGuid;
        this.CredentialID = credentialId;
        this.CredentialPublicKey = publicKey;
    }

    /// <summary>
    /// Decodes attested credential data.
    /// </summary>
    public static (AttestedCredentialData data, int bytesRead) Parse(ReadOnlyMemory<byte> attestedCredentialData)
    {
        if (attestedCredentialData.Length < MinStructSize)
        {
            throw new ArgumentException("The attested credential data structure is too short.", nameof(attestedCredentialData));
        }

        int currentPosition = 0;
        ReadOnlySpan<byte> attestedCredentialDataSpan = attestedCredentialData.Span;

        // aaguid (16B big endian)
        int guidA = BinaryPrimitives.ReadInt32BigEndian(attestedCredentialDataSpan);
        currentPosition += sizeof(int);

        short guidB = BinaryPrimitives.ReadInt16BigEndian(attestedCredentialDataSpan.Slice(currentPosition));
        currentPosition += sizeof(short);

        short guidC = BinaryPrimitives.ReadInt16BigEndian(attestedCredentialDataSpan.Slice(currentPosition)); ;
        currentPosition += sizeof(short);

        byte[] guidD = attestedCredentialDataSpan.Slice(currentPosition, sizeof(long)).ToArray();
        currentPosition += sizeof(long);

        Guid aaGuid = new Guid(guidA, guidB, guidC, guidD);

        // credentialIdLength (2B)
        ushort credentialIdLength = BinaryPrimitives.ReadUInt16BigEndian(attestedCredentialDataSpan.Slice(currentPosition));
        currentPosition += sizeof(ushort);

        // credentialId (credentialIdLength B)
        ReadOnlyMemory<byte> credentialId = attestedCredentialData.Slice(currentPosition, credentialIdLength);
        currentPosition += credentialIdLength;

        // credentialPublicKey (variable)
        ReadOnlyMemory<byte> remainingData = attestedCredentialData.Slice(currentPosition);
        (var credentialPublicKey, int bytesRead) = CredentialPublicKey.Parse(remainingData);
        currentPosition += bytesRead;

        var result = new AttestedCredentialData(aaGuid, credentialId, credentialPublicKey);
        return (result, currentPosition);
    }

    public override string ToString() => $"AAGUID: {AaGuid}, CredentialID: {CredentialID.Span.ToHex(caps: true)}";

    public void Dispose()
    {
        CredentialPublicKey?.Dispose();
        CredentialPublicKey = null;
    }
}
