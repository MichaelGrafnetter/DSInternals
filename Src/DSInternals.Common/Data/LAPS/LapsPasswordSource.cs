namespace DSInternals.Common.Data
{
    /// <summary>
    /// Defines values for LapsPasswordSource.
    /// </summary>
    public enum LapsPasswordSource
    {
        LegacyLapsCleartextPassword,
        CleartextPassword,
        EncryptedPassword,
        EncryptedPasswordHistory,
        EncryptedDSRMPassword,
        EncryptedDSRMPasswordHistory
    }
}
