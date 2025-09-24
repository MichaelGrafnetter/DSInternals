namespace DSInternals.SAM;

/// <summary>
/// Server Access Mask
/// <para>These are the specific values available to describe the access control on a server object.</para>
/// <see>http://msdn.microsoft.com/en-us/library/cc245521.aspx</see>
/// </summary>
[Flags]
public enum SamServerAccessMask : uint
{
    /// <summary>
    /// Specifies access control to obtain a server handle.
    /// </summary>
    Connect = 0x00000001U,

    /// <summary>
    /// Does not specify any access control.
    /// </summary>
    Shutdown = 0x00000002U,

    /// <summary>
    /// Does not specify any access control.
    /// </summary>
    Initialize = 0x00000004U,

    /// <summary>
    /// Does not specify any access control.
    /// </summary>
    CreateDomain = 0x00000008U,

    /// <summary>
    /// Specifies access control to view domain objects.
    /// </summary>
    EnumerateDomains = 0x00000010U,

    /// <summary>
    /// Specifies access control to perform SID-to-name translation.
    /// </summary>
    LookupDomain = 0x00000020U,

    /// <summary>
    /// The specified accesses for a GENERIC_ALL request.
    /// </summary>
    AllAccess = 0x000F003FU,

    /// <summary>
    /// The specified accesses for a GENERIC_READ request.
    /// </summary>
    Read = 0x00020010U,

    /// <summary>
    /// The specified accesses for a GENERIC_WRITE request.
    /// </summary>
    Write = 0x0002000EU,

    /// <summary>
    ///The specified accesses for a GENERIC_EXECUTE request.
    /// </summary>
    Execute = 0x00020021U,

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
