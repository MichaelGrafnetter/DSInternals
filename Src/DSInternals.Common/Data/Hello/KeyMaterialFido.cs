using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace DSInternals.Common.Data
{
    public class KeyMaterialFido
    {
        /// <summary>
        /// Version is an integer that specifies the version of the structure.
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// AuthData is a WebAuthn authenticator data structure.
        /// <see>https://www.w3.org/TR/webauthn/#sec-authenticator-data</see>
        /// </summary>
        [JsonProperty("authData")]
        public string AuthData { get; set; }

        /// <summary>
        /// X5c is an array of attestation certificates associated with the authenticator.
        /// </summary>
        [JsonProperty("x5c")]
        public string[] X5c { get; set; }

        /// <summary>
        /// Display name is a user provided string which can help the user differentiate between multiple registered authenticators.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        // Attestation certificates can be helpful for establishing a chain of trust.
        public X509Certificate2Collection AttestationCertificates
        {
            get
            {
                X509Certificate2Collection certs = new X509Certificate2Collection();
                foreach (string s in X5c)
                {
                    certs.Add(new X509Certificate2(System.Convert.FromBase64String(s)));
                }
                return certs;
            }
        }
        // Authenticator data contains information about the registered authenticator device.
        public Fido.AuthenticatorData AuthenticatorData
        {
            get
            {
                return new Fido.AuthenticatorData(System.Convert.FromBase64String(AuthData));
            }
        }
    }
}
