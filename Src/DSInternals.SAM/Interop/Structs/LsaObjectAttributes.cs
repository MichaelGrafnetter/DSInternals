using System.Runtime.InteropServices;

namespace DSInternals.SAM.Interop;

/// <summary>
/// Used with the LsaOpenPolicy function to specify the attributes of the connection to the Policy object.
/// </summary>
/// <remarks>
/// Corresponds to the LSA_OBJECT_ATTRIBUTES structure.
/// See https://learn.microsoft.com/en-us/windows/win32/api/lsalookup/ns-lsalookup-lsa_object_attributes
/// </remarks>
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
