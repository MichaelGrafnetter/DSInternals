using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Common.Data;

/// <summary>
/// Represents FIDO2/WebAuthn key material stored in Active Directory.
/// </summary>
/// <seealso href="https://www.w3.org/TR/webauthn/"/>
public class KeyMaterialFido
{
    // All PEM certificates that are less than 16,383B long start with MII.
    private const string X509CertificateHeader = "MII";

    /// <summary>
    /// Version is an integer that specifies the version of the structure.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("version")]
    public int Version
    {
        get;
        internal set;
    }

    /// <summary>
    /// AuthData is a WebAuthn authenticator data structure.
    /// <see>https://www.w3.org/TR/webauthn/#sec-authenticator-data</see>
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("authData")]
    public string? AuthenticatorDataRaw
    {
        get;
        internal set;
    }

    /// <summary>
    /// X5c is an array of attestation certificates associated with the authenticator.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("x5c")]
    public string[]? AttestationCertificatesRaw
    {
        get;
        internal set;
    }

    /// <summary>
    /// Display name is a user provided string which can help the user differentiate between multiple registered authenticators.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("displayName")]
    public string? DisplayName
    {
        get;
        internal set;
    }

    /// <summary>
    /// Attestation certificates can be helpful for establishing a chain of trust.
    /// </summary>
    [JsonIgnore]
    public X509Certificate2Collection AttestationCertificates
    {
        get
        {
            X509Certificate2Collection certs = [];
            foreach (string s in this.AttestationCertificatesRaw ?? [])
            {
                // In AAD, some x5c values are not really certificates, so we need to filter them out.
                if (s.StartsWith(X509CertificateHeader, StringComparison.InvariantCulture))
                {
                    certs.Add(new X509Certificate2(Convert.FromBase64String(s)));
                }
            }
            return certs;
        }
    }

    /// <summary>
    /// Authenticator data contains information about the registered authenticator device.
    /// </summary>
    [JsonIgnore]
    public Fido.AuthenticatorData? AuthenticatorData
    {
        get
        {
            if (this.AuthenticatorDataRaw == null)
            {
                return null;
            }

            return new Fido.AuthenticatorData(Convert.FromBase64String(this.AuthenticatorDataRaw));
        }
    }

    public static KeyMaterialFido? ParseJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        return JsonSerializer.Deserialize(json, KeyCredentialSerializationContext.Default.KeyMaterialFido);
    }

    public static KeyMaterialFido? ParseJson(ReadOnlySpan<byte> jsonData)
    {
        if (jsonData.IsEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize(jsonData, KeyCredentialSerializationContext.Default.KeyMaterialFido);
    }
}
