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
        KeyCredentials,
        RoamedCredentials,
        LAPS,
        All = DistinguishedName | GenericInformation | SecurityDescriptor | PasswordHashes | PasswordHashHistory | SupplementalCredentials | KeyCredentials | RoamedCredentials | LAPS,
        Default = All
    }
}
