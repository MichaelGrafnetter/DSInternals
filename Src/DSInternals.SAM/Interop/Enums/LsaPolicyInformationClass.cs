
namespace DSInternals.SAM.Interop
{
    using System;

    /// <summary>
    /// Defines values that indicate the type of information to set or query in a Policy object.
    /// </summary>
    /// <see>https://docs.microsoft.com/en-us/windows/desktop/api/ntsecapi/ne-ntsecapi-_policy_information_class</see>
    internal enum LsaPolicyInformationClass : int
    {
        /// <summary>
        /// Information about audit log.
        /// </summary>
        [Obsolete()]
        AuditLogInformation = 1,

        /// <summary>
        /// Query or set the auditing rules of the system.
        /// </summary>
        AuditEventsInformation,

        /// <summary>
        /// Primary domain information.
        /// </summary>
        [Obsolete()]
        PrimaryDomainInformation,

        [Obsolete()]
        PdAccountInformation,

        /// <summary>
        /// Query or set the name and SID of the account domain of the system.
        /// </summary>
        AccountDomainInformation,

        /// <summary>
        /// Query or set the role of an LSA server.
        /// </summary>
        LsaServerRoleInformation,

        [Obsolete()]
        ReplicaSourceInformation,

        [Obsolete()]
        DefaultQuotaInformation,

        /// <summary>
        /// Query or set information about the creation time and last modification of the LSA database.
        /// </summary>
        ModificationInformation,

        [Obsolete()]
        AuditFullSetInformation,

        /// <summary>
        ///  Audit log state.
        /// </summary>
        [Obsolete()]
        AuditFullQueryInformation,

        /// <summary>
        /// Query or set Domain Name System (DNS) information about the account domain associated with a Policy object.
        /// </summary>
        DnsDomainInformation,

        /// <summary>
        /// DNS domain information.
        /// </summary>
        DnsDomainInformationInt,

        /// <summary>
        /// Local account domain information.
        /// </summary>
        LocalAccountDomainInformation,

        /// <summary>
        /// Machine account information.
        /// </summary>
        MachineAccountInformation
    }
}