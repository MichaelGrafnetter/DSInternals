namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Specifies the encryption method used to protect private keys in the Active Directory database.
    /// </summary>
    public enum PrivateKeyEncryptionType : int
    {
        None = 0,
        PasswordRC4 = 1,
        PasswordRC2CBC = 2
    }
}