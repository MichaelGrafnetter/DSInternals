namespace DSInternals.Common.Data
{
    public enum RoamedCredentialType : byte
    {
        /// <summary>
        /// DPAPI Master Key
        /// </summary>
        DPAPIMasterKey = 0,

        /// <summary>
        /// CAPI RSA Private Key
        /// </summary>
        RSAPrivateKey = 1,
        
        /// <summary>
        /// CAPI DSA Private Key
        /// </summary>
        DSAPrivateKey = 2,
        
        /// <summary>
        /// CAPI Certificate
        /// </summary>
        CryptoApiCertificate = 3,
        
        /// <summary>
        /// CAPI Certificate Signing Request
        /// </summary>
        CryptoApiRequest = 4,  
        
        /// <summary>
        /// CNG Certificate
        /// </summary>
        CNGCertificate = 7,
        
        /// <summary>
        /// CNG Certificate Signing Request
        /// </summary>
        CNGRequest = 8,
        
        /// <summary>
        /// CNG Private Key
        /// </summary>
        CNGPrivateKey = 9
    }
}