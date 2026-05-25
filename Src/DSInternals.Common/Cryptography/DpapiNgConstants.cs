using System.Security.Cryptography;

namespace DSInternals.Common.Cryptography;

internal static class DpapiNgConstants
{
    internal const string ProtectionInfo = "1.3.6.1.4.1.311.74.1";

    internal const string SidProtected = "1.3.6.1.4.1.311.74.1.1";
    internal const string KeyProtected = "1.3.6.1.4.1.311.74.1.3";
    internal const string AndCombinerProtected = "1.3.6.1.4.1.311.74.1.4";
    internal const string SddlProtected = "1.3.6.1.4.1.311.74.1.5";
    internal const string MsidcrlProtected = "1.3.6.1.4.1.311.74.1.6";
    internal const string WebCredentialsProtected = "1.3.6.1.4.1.311.74.1.7";
    internal const string LocalProtected = "1.3.6.1.4.1.311.74.1.8";
    internal const string DraCertificateProtected = "1.3.6.1.4.1.311.74.1.9";
    internal const string VaultCredentialsProtected = "1.3.6.1.4.1.311.74.1.10";
    internal const string CertificateProtected = "1.3.6.1.4.1.311.74.1.11";
    internal const string KeyFileProtected = "1.3.6.1.4.1.311.74.1.12";
    internal const string MicrosoftPfxSidProtectorSecretType = "1.3.6.1.4.1.311.17.4";

    internal const string OidSha256 = "2.16.840.1.101.3.4.2.1";
    internal const string OidAes256Wrap = "2.16.840.1.101.3.4.1.45";
    internal const string OidAes256Gcm = "2.16.840.1.101.3.4.1.46";

    internal const string SidName = "SID";
    internal const string SddlName = "SDDL";
    internal const string LocalName = "LOCAL";
    internal const string MsidcrlName = "MSIDCRL";
    internal const string WebCredentialsName = "WEBCREDENTIALS";
    internal const string VaultCredentialsName = "VAULTCREDENTIALS";
    internal const string CertificateName = "CERTIFICATE";
    internal const string DraCertificateName = "DRACERTIFICATE";
    internal const string KeyFileName = "KEYFILE";

    /// <summary>
    /// Creates an <see cref="Oid" /> from an algorithm OID string, filling in friendly names that
    /// the underlying platform fails to resolve.
    /// </summary>
    /// <param name="value">The dotted OID value.</param>
    /// <returns>An <see cref="Oid" /> instance.</returns>
    internal static Oid CreateAlgorithmOid(string value)
    {
        // .NET / CNG ships with no friendly name for AES-256-GCM, so override it.
        return value == OidAes256Gcm
            ? new Oid(value, "aes256gcm")
            : new Oid(value);
    }
}
