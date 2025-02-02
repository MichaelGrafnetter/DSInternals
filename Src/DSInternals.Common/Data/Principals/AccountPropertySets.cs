using System;

namespace DSInternals.Common.Data
{
    [Flags]
    public enum AccountPropertySets : short
    {
        None = 0,
        DistinguishedName,
        GenericInformation,
        SecurityDescriptor,
        NTHash,
        LMHash,
        PasswordHashes = NTHash | LMHash,
        NTHashHistory,
        LMHashHistory,
        PasswordHashHistory = NTHashHistory | LMHashHistory,
        SupplementalCredentials,
        Secrets = PasswordHashes | PasswordHashHistory | SupplementalCredentials,
        KeyCredentials,
        RoamedCredentials,
        WindowsLAPS,
        LegacyLAPS,
        LAPS = WindowsLAPS | LegacyLAPS,
        All = DistinguishedName | GenericInformation | SecurityDescriptor | Secrets | KeyCredentials | RoamedCredentials | LAPS,
        Default = All
    }
}
