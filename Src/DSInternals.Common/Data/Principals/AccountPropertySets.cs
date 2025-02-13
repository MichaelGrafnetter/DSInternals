using System;

namespace DSInternals.Common.Data
{
    [Flags]
    public enum AccountPropertySets : int
    {
        None = 0,
        DistinguishedName = 1 << 0,
        GenericAccountInfo = 1 << 1,
        GenericUserInfo = 1 << 2,
        GenericComputerInfo = 1 << 3,
        GenericInfo = GenericAccountInfo | GenericUserInfo | GenericComputerInfo,
        SecurityDescriptor = 1 << 4,
        NTHash = 1 << 5,
        LMHash = 1 << 6,
        PasswordHashes = NTHash | LMHash,
        NTHashHistory = 1 << 7,
        LMHashHistory = 1 << 8,
        PasswordHashHistory = NTHashHistory | LMHashHistory,
        SupplementalCredentials = 1 << 9,
        Secrets = PasswordHashes | PasswordHashHistory | SupplementalCredentials,
        KeyCredentials = 1 << 10,
        RoamedCredentials = 1 << 11,
        WindowsLAPS = 1 << 12,
        LegacyLAPS = 1 << 13,
        LAPS = WindowsLAPS | LegacyLAPS,
        ManagedBy = 1 << 14,
        Manager = 1 << 15,
        All = DistinguishedName | GenericInfo | SecurityDescriptor | Secrets | KeyCredentials | RoamedCredentials | LAPS | ManagedBy | Manager
    }
}
