namespace DSInternals.SAM.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Used with the LsaOpenPolicy function to specify the attributes of the connection to the Policy object.
    /// </summary>
    /// <see>https://docs.microsoft.com/en-us/windows/desktop/api/lsalookup/ns-lsalookup-_lsa_object_attributes</see>
    [StructLayout(LayoutKind.Sequential)]
    internal struct LsaObjectAttributes
    {
        /// <summary>
        /// Specifies the size, in bytes, of the structure.
        /// </summary>
        uint Length;

        /// <summary>
        /// Should be NULL.
        /// </summary>
        IntPtr RootDirectory;

        /// <summary>
        /// Should be NULL.
        /// </summary>
        IntPtr ObjectName;

        /// <summary>
        /// Should be zero.
        /// </summary>
        uint Attributes;

        /// <summary>
        /// Should be NULL.
        /// </summary>
        IntPtr SecurityDescriptor;

        /// <summary>
        /// Should be NULL.
        /// </summary>
        IntPtr SecurityQualityOfService;
    }
}
