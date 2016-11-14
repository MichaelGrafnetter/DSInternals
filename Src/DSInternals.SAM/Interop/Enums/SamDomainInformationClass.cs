
namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// The DOMAIN_INFORMATION_CLASS enumeration indicates how to interpret the Buffer parameter for SamrSetInformationDomain and SamrQueryInformationDomain.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245570.aspx</see>
    internal enum SamDomainInformationClass
    {
        PasswordInformation = 1,
        GeneralInformation = 2,
        LogoffInformation = 3,
        OemInformation = 4,
        NameInformation = 5,
        ReplicationInformation = 6,
        ServerRoleInformation = 7,
        ModifiedInformation = 8,
        StateInformation = 9,
        GeneralInformation2 = 11,
        LockoutInformation = 12,
        ModifiedInformation2 = 13
    }
}
