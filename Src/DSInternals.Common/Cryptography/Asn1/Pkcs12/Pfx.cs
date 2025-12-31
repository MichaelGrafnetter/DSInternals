
#pragma warning disable SA1028 // ignore whitespace warnings for generated code
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography.Asn1.Pkcs12;

[StructLayout(LayoutKind.Sequential)]
internal struct Pfx
{
    /// <summary>
    /// The version shall be v3 for RFC7292.
    /// </summary>
    private const int PfxVersionV3 = 3;

    public DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentInfo AuthSafe;
    public DSInternals.Common.Cryptography.Asn1.Pkcs12.MacData? MacData;

    /*
            public IList<ContentInfo> AuthSafeData
            {
                get
                {
                    if (this.AuthSafe.ContentType == DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentType.Data)
                    {
                        return new[] { this.AuthSafe };
                    }
                    else if (this.AuthSafe.ContentType == DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentType.SignedData)
                    {
                        return new[] { this.AuthSafe };
                    }
                    else if (this.AuthSafe.ContentType == DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentType.EnvelopedData)
                    {
                        return new[] { this.AuthSafe };
                    }
                    else
                    {
                        throw new NotSupportedException($"Unsupported AuthSafe content type: {this.AuthSafe.ContentType}");
                    }
                }
            }
    */
    public static Pfx Decode(ReadOnlyMemory<byte> encoded)
    {
        AsnReader reader = new AsnReader(encoded, AsnEncodingRules.DER);
        var decoded = Decode(reader);
        reader.ThrowIfNotEmpty();
        return decoded;
    }

    public static Pfx Decode(AsnReader reader)
    {
        /*
        https://tools.ietf.org/html/rfc7292#section-4

        PFX ::= SEQUENCE {
            version    INTEGER {v3(3)}(v3,...),
            authSafe   ContentInfo,
            macData    MacData OPTIONAL
        }
        */

        ArgumentNullException.ThrowIfNull(reader);

        Pfx decoded = default;
        AsnReader sequenceReader = reader.ReadSequence();


        if (!sequenceReader.TryReadInt32(out int version))
        {
            sequenceReader.ThrowIfNotEmpty();
        }

        if (version != PfxVersionV3)
        {
            throw new CryptographicException($"Unsupported PFX version: {version}. Expected version: {PfxVersionV3}.");
        }

        decoded.AuthSafe = DSInternals.Common.Cryptography.Asn1.Pkcs7.ContentInfo.Decode(sequenceReader);

        if (sequenceReader.HasData && sequenceReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
        {
            DSInternals.Common.Cryptography.Asn1.Pkcs12.MacData.Decode(sequenceReader, out MacData tmpMacData);
            decoded.MacData = tmpMacData;
        }

        sequenceReader.ThrowIfNotEmpty();

        return decoded;
    }
}
