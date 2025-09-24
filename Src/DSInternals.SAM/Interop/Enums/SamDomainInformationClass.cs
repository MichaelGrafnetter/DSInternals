namespace DSInternals.SAM.Interop
{
    /// <summary>
    /// Indindicates how to interpret the Buffer parameter for SamrSetInformationDomain and SamrQueryInformationDomain.
    /// </summary>
    /// <remarks>Corresponds to DOMAIN_INFORMATION_CLASS in the Windows API.</remarks>
    /// <see>https://msdn.microsoft.com/en-us/library/cc245570.aspx</see>
    internal enum SamDomainInformationClass
    {
        /// <summary>
        /// Password policy.
        /// </summary>
        PasswordInformation = 1,

        /// <summary>
        /// General information.
        /// </summary>
        GeneralInformation = 2,

        /// <summary>
        /// Policy setting for the amount of time that an interactive logon session is allowed to continue.
        /// </summary>
        LogoffInformation = 3,

        /// <summary>
        /// There are no known scenarios that use this field.
        /// </summary>
        OemInformation = 4,

        /// <summary>
        /// Name of the domain.
        /// </summary>
        NameInformation = 5,

        /// <summary>
        /// NetBIOS name of the primary domain controller (PDC) at the time of upgrade from Windows NT 4.0 to Windows 2000.
        /// </summary>
        ReplicationInformation = 6,

        /// <summary>
        /// Role of the server in the domain.
        /// </summary>
        ServerRoleInformation = 7,

        /// <summary>
        /// Counters related to the Windows NT 4.0 operating system replication protocol.
        /// </summary>
        ModifiedInformation = 8,

        /// <summary>
        /// Enabled/disabled state of the server.
        /// </summary>
        StateInformation = 9,

        /// <summary>
        /// GeneralInformation with additional fields.
        /// </summary>
        GeneralInformation2 = 11,

        /// <summary>
        /// Account lockout policy.
        /// </summary>
        LockoutInformation = 12,

        /// <summary>
        /// Counters related to the Windows NT 4.0 operating system replication protocol.
        /// </summary>
        ModifiedInformation2 = 13
    }
}
