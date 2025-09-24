namespace DSInternals.SAM;

/// <summary>
/// User Access Mask
/// <para>These are the specific values available to describe the access control on a user object.</para>
/// <see>http://msdn.microsoft.com/en-us/library/cc245525.aspx</see>
/// </summary>
[Flags]
public enum SamUserAccessMask : uint
{
    /// <summary>
    /// Specifies the ability to read sundry attributes.
    /// </summary>
    ReadGeneral = 0x00000001U,

    /// <summary>
    /// Specifies the ability to read general information attributes.
    /// </summary>
    ReadPreferences = 0x00000002U,

    /// <summary>
    /// Specifies the ability to write general information attributes.
    /// </summary>
    WritePreferences = 0x00000004U,

    /// <summary>
    /// Specifies the ability to read attributes related to logon statistics.
    /// </summary>
    ReadLogon = 0x00000008U,

    /// <summary>
    /// Specifies the ability to read attributes related to the administration of the user object.
    /// </summary>
    ReadAccount = 0x00000010U,

    /// <summary>
    /// Specifies the ability to write attributes related to the administration of the user object.
    /// </summary>
    WriteAccount = 0x00000020U,

    /// <summary>
    /// Specifies the ability to change the user's password.
    /// </summary>
    ChangePassword = 0x00000040U,

    /// <summary>
    /// Specifies the ability to set the user's password.
    /// </summary>
    ForcePasswordChange = 0x00000080U,

    /// <summary>
    /// Specifies the ability to query the membership of the user object.
    /// </summary>
    ListGroups = 0x00000100U,

    /// <summary>
    /// Does not specify any access control.
    /// </summary>
    ReadGroupInformation = 0x00000200U,

    /// <summary>
    /// Does not specify any access control.
    /// </summary>
    WriteGroupInformation = 0x00000400U,

    /// <summary>
    /// The specified accesses for a GENERIC_ALL request.
    /// </summary>
    AllAccess = 0x000F07FFU,

    /// <summary>
    /// The specified accesses for a GENERIC_READ request.
    /// </summary>
    Read = 0x0002031AU,

    /// <summary>
    /// The specified accesses for a GENERIC_WRITE request.
    /// </summary>
    Write = 0x00020044U,

    /// <summary>
    /// The specified accesses for a GENERIC_EXECUTE request.
    /// </summary>
    Execute = 0x00020041U,

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
