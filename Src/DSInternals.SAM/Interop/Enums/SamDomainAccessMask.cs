namespace DSInternals.SAM;

/// <summary>
/// Domain Access Mask
/// <para>These are the specific values available to describe the access control on a domain object.</para>
/// <see>http://msdn.microsoft.com/en-us/library/cc245522.aspx</see>
/// </summary>
[Flags]
public enum SamDomainAccessMask : uint
{
    /// <summary>
    /// Specifies access control to read password policy.
    /// </summary>
    ReadPasswordParameters = 0x00000001U,

    /// <summary>
    /// Specifies access control to write password policy.
    /// </summary>
    WritePasswordParameters = 0x00000002U,

    /// <summary>
    /// Specifies access control to read attributes not related to password policy.
    /// </summary>
    ReadOtherParameters = 0x00000004U,

    /// <summary>
    /// Specifies access control to write attributes not related to password policy.
    /// </summary>
    WriteOtherParameters = 0x00000008U,

    /// <summary>
    /// Specifies access control to create a user object.
    /// </summary>
    CreateUser = 0x00000010U,

    /// <summary>
    /// Specifies access control to create a group object.
    /// </summary>
    CreateGroup = 0x00000020U,

    /// <summary>
    /// Specifies access control to create an alias object.
    /// </summary>
    CreateAlias = 0x00000040U,

    /// <summary>
    /// Specifies access control to read the alias membership of a set of SIDs.
    /// </summary>
    GetAliasMembership = 0x00000080U,

    /// <summary>
    /// Specifies access control to enumerate objects.
    /// </summary>
    ListAccounts = 0x00000100U,

    /// <summary>
    /// Specifies access control to look up objects by name and SID.
    /// </summary>
    Lookup = 0x00000200U,

    /// <summary>
    /// Specifies access control to various administrative operations on the server.
    /// </summary>
    AdministerServer = 0x00000400U,

    /// <summary>
    /// The specified accesses for a GENERIC_ALL request.
    /// </summary>
    AllAccess = 0x000F07FFU,

    /// <summary>
    /// The specified accesses for a GENERIC_READ request.
    /// </summary>
    Read = 0x00020084U,

    /// <summary>
    /// The specified accesses for a GENERIC_WRITE request.
    /// </summary>
    Write = 0x0002047AU,

    /// <summary>
    /// The specified accesses for a GENERIC_EXECUTE request.
    /// </summary>
    Execute = 0x00020301U,

    /// <summary>
    /// Indicates that the caller is requesting the most access possible to the object.
    /// </summary>
    MaximumAllowed = 0x02000000U,

    /// <summary>
    /// Specifies access to the system security portion of the security descriptor.
    /// </summary>
    AccessSystemSecurity = 0x01000000U,

    /// <summary>
    /// Specifies the ability to update the Owner field of the security descriptor.
    /// </summary>
    WriteOwner = 0x00080000U,

    /// <summary>
    /// Specifies the ability to update the discretionary access control list (DACL) of the security descriptor.
    /// </summary>
    WriteDacl = 0x00040000U,

    /// <summary>
    /// Specifies the ability to read the security descriptor.
    /// </summary>
    ReadControl = 0x00020000U,

    /// <summary>
    /// Specifies the ability to delete the object.
    /// </summary>
    Delete = 0x00010000U
}
