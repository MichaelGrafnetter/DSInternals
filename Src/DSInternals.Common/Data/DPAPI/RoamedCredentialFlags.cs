namespace DSInternals.Common.Data
{
    using System;

    [Flags]
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
