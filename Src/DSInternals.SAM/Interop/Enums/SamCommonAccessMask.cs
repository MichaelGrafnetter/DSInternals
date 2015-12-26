using System;

namespace DSInternals.SAM.Interop
{
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
}