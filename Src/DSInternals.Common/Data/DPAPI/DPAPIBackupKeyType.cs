namespace DSInternals.Common.Data;

public enum DPAPIBackupKeyType : byte
{
    Unknown = 0,
    LegacyKey,
    RSAKey,
    PreferredLegacyKeyPointer,
    PreferredRSAKeyPointer
}
