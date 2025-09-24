using Windows.Win32;
using Windows.Win32.Storage.FileSystem;

namespace DSInternals.SAM;

/// <summary>
/// Common Access Mask
/// <para>These values specify an access control that is applicable to all object types exposed by this protocol.</para>
/// <see>http://msdn.microsoft.com/en-us/library/cc245511.aspx</see>
/// </summary>
[Flags]
public enum SamCommonAccessMask : uint
{
    /// <summary>
    /// Indicates that the caller is requesting the most access possible to the object.
    /// </summary>
    MaximumAllowed = PInvoke.MAXIMUM_ALLOWED,

    /// <summary>
    /// Specifies access to the system security portion of the security descriptor.
    /// </summary>
    AccessSystemSecurity = PInvoke.ACCESS_SYSTEM_SECURITY,

    /// <summary>
    /// Specifies the ability to update the Owner field of the security descriptor.
    /// </summary>
    WriteOwner = FILE_ACCESS_RIGHTS.WRITE_OWNER,

    /// <summary>
    /// Specifies the ability to update the discretionary access control list (DACL) of the security descriptor.
    /// </summary>
    WriteDacl = FILE_ACCESS_RIGHTS.WRITE_DAC,

    /// <summary>
    /// Specifies the ability to read the security descriptor.
    /// </summary>
    ReadControl = FILE_ACCESS_RIGHTS.READ_CONTROL,

    /// <summary>
    /// Specifies the ability to delete the object.
    /// </summary>
    Delete = FILE_ACCESS_RIGHTS.DELETE
}
