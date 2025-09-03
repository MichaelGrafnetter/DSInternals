namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Defines values for PrivateKeyEncryptionType.
    /// </summary>
    public enum PrivateKeyEncryptionType : int
    {
        None = 0,
        PasswordRC4 = 1,
        PasswordRC2CBC = 2
    }
}