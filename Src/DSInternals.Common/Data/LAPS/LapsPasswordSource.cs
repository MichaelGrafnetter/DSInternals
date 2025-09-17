namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies the source and type of a LAPS password (legacy, encrypted, DSRM, etc.).
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
