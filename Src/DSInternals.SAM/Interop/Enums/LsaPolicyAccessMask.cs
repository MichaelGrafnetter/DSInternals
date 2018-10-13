namespace DSInternals.SAM.Interop
{
    using System;

    [Flags]
    public enum LsaPolicyAccessMask : int
    {
        /// <summary>
        /// This access type is needed to read the target system's miscellaneous security policy information. This includes the default quota, auditing, server state and role information, and trust information. This access type is also needed to enumerate trusted domains, accounts, and privileges.
        /// </summary>
        ViewLocalInformation = 0x00000001,

        /// <summary>
        /// This access type is needed to view audit trail or audit requirements information.
        /// </summary>
        ViewAuditInformation = 0x00000002,

        /// <summary>
        /// This access type is needed to view sensitive information, such as the names of accounts established for trusted domain relationships.
        /// </summary>
        GetPrivateInformation = 0x00000004,

        /// <summary>
        /// This access type is needed to change the account domain or primary domain information.
        /// </summary>
        TrustAdmin = 0x00000008,

        /// <summary>
        /// This access type is needed to create a new Account object.
        /// </summary>
        CreateAccount = 0x00000010,

        /// <summary>
        /// This access type is needed to create a new Private Data object.
        /// </summary>
        CreateSecret = 0x00000020,

        /// <summary>
        /// Not yet supported.
        /// </summary>
        CreatePrivilege = 0x00000040,

        /// <summary>
        /// Set the default system quotas that are applied to user accounts.
        /// </summary>
        SetDefaultQuotaLimits = 0x00000080,

        /// <summary>
        /// This access type is needed to update the auditing requirements of the system.
        /// </summary>
        SetAuditRequirements = 0x00000100,

        /// <summary>
        /// This access type is needed to change the characteristics of the audit trail such as its maximum size or the retention period for audit records, or to clear the log.
        /// </summary>
        AuditLogAdmin = 0x00000200,

        /// <summary>
        /// This access type is needed to modify the server state or role (master/replica) information.It is also needed to change the replica source and account name information.
        /// </summary>
        ServerAdmin = 0x00000400,

        /// <summary>
        /// This access type is needed to translate between names and SIDs.
        /// </summary>
        LookupNames = 0x00000800,

        Notification = 0x00001000,
    }
}