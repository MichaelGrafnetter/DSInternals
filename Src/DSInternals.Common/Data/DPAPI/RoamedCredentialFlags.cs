namespace DSInternals.Common.Data
{
    using System;

    [Flags]
    /// <summary>
    /// Defines values for RoamedCredentialFlags.
    /// </summary>
    public enum RoamedCredentialFlags : short
    {
        Tombstone = 1,
        Unreadable = 2,
        Unwritable = 4,
        Unroamable = 8,
        KnownType = 16,
        EncryptionKey = 32
    }
}
