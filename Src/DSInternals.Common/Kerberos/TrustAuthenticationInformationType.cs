namespace DSInternals.Common.Kerberos
{
    /// <summary>
    /// Dictates the type of AuthInfo that is being stored.
    /// </summary>
    /// <remarks>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-adts/dfe16abb-4dfb-402d-bc54-84fcc9932fad</remarks>
    public enum TrustAuthenticationInformationType : uint
    {
        /// <summary>
        /// AuthInfo byte field is invalid/not relevant.
        /// </summary>
        /// <remarks>TRUST_AUTH_TYPE_NONE</remarks>
        None = 0,

        /// <summary>
        /// AuthInfo byte field contains an RC4 Key [RFC4757].
        /// </summary>
        /// <remarks>TRUST_AUTH_TYPE_NT4OWF</remarks>
        NTHash = 1,

        /// <summary>
        /// AuthInfo byte field contains a cleartext password, encoded as a Unicode string.
        /// </summary>
        /// <remarks>TRUST_AUTH_TYPE_CLEAR</remarks>
        CleartextPassword = 2,

        /// <summary>
        /// AuthInfo byte field contains a version number, used by Netlogon for versioning interdomain trust secrets.
        /// </summary>
        /// <remarks>TRUST_AUTH_TYPE_VERSION</remarks>
        Version = 3
    }
}
