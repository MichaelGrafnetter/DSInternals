namespace DSInternals.Common.Interop
{
    using System;

    /// <summary>
    /// Access rights for registry key objects.
    /// </summary>
    [Flags]
    public enum RegistryKeyRights : int
    {
        /// <summary>
        /// Combines the STANDARD_RIGHTS_REQUIRED, KEY_QUERY_VALUE, KEY_SET_VALUE, KEY_CREATE_SUB_KEY, KEY_ENUMERATE_SUB_KEYS, KEY_NOTIFY, and KEY_CREATE_LINK access rights.
        /// </summary>
        AllAccess = 0xF003F,

        /// <summary>
        /// Reserved for system use.
        /// </summary>
        CreateLink = 0x0020,

        /// <summary>
        /// Required to create a subkey of a registry key.
        /// </summary>
        CreateSubKey = 0x0004,

        /// <summary>
        /// Required to enumerate the subkeys of a registry key.
        /// </summary>
        EnumerateSubKeys = 0x0008,

        /// <summary>
        /// Equivalent to KEY_READ.
        /// </summary>
        Execute = Read,

        /// <summary>
        /// Required to request change notifications for a registry key or for subkeys of a registry key.
        /// </summary>
        Notify = 0x0010,

        /// <summary>
        /// Required to query the values of a registry key.
        /// </summary>
        QueryValue = 0x0001,

        /// <summary>
        /// Combines the STANDARD_RIGHTS_READ, KEY_QUERY_VALUE, KEY_ENUMERATE_SUB_KEYS, and KEY_NOTIFY values.
        /// </summary>
        Read = 0x20019,

        /// <summary>
        /// Required to create, delete, or set a registry value.
        /// </summary>
        SetValue = 0x0002,

        /// <summary>
        /// Indicates that an application on 64-bit Windows should operate on the 32-bit registry view. This flag is ignored by 32-bit Windows.
        /// </summary>
        Wow6432Key = 0x0200,

        /// <summary>
        /// Indicates that an application on 64-bit Windows should operate on the 64-bit registry view. This flag is ignored by 32-bit Windows.
        /// </summary>
        Wow6464Key = 0x0100,

        /// <summary>
        /// Combines the STANDARD_RIGHTS_WRITE, KEY_SET_VALUE, and KEY_CREATE_SUB_KEY access rights.
        /// </summary>
        Write = 0x20006
    }
}