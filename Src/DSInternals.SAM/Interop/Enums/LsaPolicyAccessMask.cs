using Windows.Win32;

namespace DSInternals.SAM;

/// <summary>
/// Specifies the access rights for a policy object.
/// </summary>
[Flags]
public enum LsaPolicyAccessMask : int
{
    /// <summary>
    /// No access.
    /// </summary>
    NoAccess = 0,

    /// <summary>
    /// This access type is needed to read the target system's miscellaneous security policy information. This includes the default quota, auditing, server state and role information, and trust information. This access type is also needed to enumerate trusted domains, accounts, and privileges.
    /// </summary>
    ViewLocalInformation = PInvoke.POLICY_VIEW_LOCAL_INFORMATION,

    /// <summary>
    /// This access type is needed to view audit trail or audit requirements information.
    /// </summary>
    ViewAuditInformation = PInvoke.POLICY_VIEW_AUDIT_INFORMATION,

    /// <summary>
    /// This access type is needed to view sensitive information, such as the names of accounts established for trusted domain relationships.
    /// </summary>
    GetPrivateInformation = PInvoke.POLICY_GET_PRIVATE_INFORMATION,

    /// <summary>
    /// This access type is needed to change the account domain or primary domain information.
    /// </summary>
    TrustAdmin = PInvoke.POLICY_TRUST_ADMIN,

    /// <summary>
    /// This access type is needed to create a new Account object.
    /// </summary>
    CreateAccount = PInvoke.POLICY_CREATE_ACCOUNT,

    /// <summary>
    /// This access type is needed to create a new Private Data object.
    /// </summary>
    CreateSecret = PInvoke.POLICY_CREATE_SECRET,

    /// <summary>
    /// Not yet supported.
    /// </summary>
    CreatePrivilege = PInvoke.POLICY_CREATE_PRIVILEGE,

    /// <summary>
    /// Set the default system quotas that are applied to user accounts.
    /// </summary>
    SetDefaultQuotaLimits = PInvoke.POLICY_SET_DEFAULT_QUOTA_LIMITS,

    /// <summary>
    /// This access type is needed to update the auditing requirements of the system.
    /// </summary>
    SetAuditRequirements = PInvoke.POLICY_SET_AUDIT_REQUIREMENTS,

    /// <summary>
    /// This access type is needed to change the characteristics of the audit trail such as its maximum size or the retention period for audit records, or to clear the log.
    /// </summary>
    AuditLogAdmin = PInvoke.POLICY_AUDIT_LOG_ADMIN,

    /// <summary>
    /// This access type is needed to modify the server state or role (master/replica) information.It is also needed to change the replica source and account name information.
    /// </summary>
    ServerAdmin = PInvoke.POLICY_SERVER_ADMIN,

    /// <summary>
    /// This access type is needed to translate between names and SIDs.
    /// </summary>
    LookupNames = PInvoke.POLICY_LOOKUP_NAMES,

    /// <summary>
    /// This access type is needed to be notified of policy changes.
    /// </summary>
    Notification = PInvoke.POLICY_NOTIFICATION
}
