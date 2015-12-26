
using System.Runtime.InteropServices;
namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The NETRESOURCE structure contains information about a network resource.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385353.aspx</see>
    [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
    internal struct NetResource
    {
        internal NetResource(string shareName)
        {

            this.RemoteName = shareName; 
            this.Type = NetResourceType.Any;
            this.LocalName = null;
            this.Provider = null;
            this.Scope = NetResourceScope.Globalnet;
            this.Usage = NetResourceUsage.All;
            this.Comment = null;
            this.DisplayType = NetResourceDisplayType.Share;
        }
        /// <summary>
        /// The scope of the enumeration.
        /// </summary>
        internal NetResourceScope Scope;
        /// <summary>
        /// The type of resource.
        /// </summary>
        internal NetResourceType Type;
        /// <summary>
        /// The display options for the network object in a network browsing user interface.
        /// </summary>
        internal NetResourceDisplayType DisplayType;
        /// <summary>
        /// A set of bit flags describing how the resource can be used. 
        /// </summary>
        internal NetResourceUsage Usage;
        /// <summary>
        /// If the dwScope member is equal to RESOURCE_CONNECTED or RESOURCE_REMEMBERED, this member is a pointer to a null-terminated character string that specifies the name of a local device. This member is NULL if the connection does not use a device.
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        internal string LocalName;
        /// <summary>
        /// If the entry is a network resource, this member is a pointer to a null-terminated character string that specifies the remote network name.
        /// If the entry is a current or persistent connection, lpRemoteName member points to the network name associated with the name pointed to by the lpLocalName member.
        /// The string can be MAX_PATH characters in length, and it must follow the network provider's naming conventions.
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        internal string RemoteName;
        /// <summary>
        /// A pointer to a NULL-terminated string that contains a comment supplied by the network provider.
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        internal string Comment;
        /// <summary>
        /// A pointer to a NULL-terminated string that contains the name of the provider that owns the resource. This member can be NULL if the provider name is unknown.
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        internal string Provider;
    }
}
