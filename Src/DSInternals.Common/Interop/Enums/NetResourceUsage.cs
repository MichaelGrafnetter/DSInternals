using System;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// A set of bit flags describing how the resource can be used.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385353.aspx</see>
    [Flags]
    internal enum NetResourceUsage : uint
    {
        /// <summary>
        /// The resource is a connectable resource.
        /// </summary>
        Connectable = 0x00000001U,
        /// <summary>
        /// The resource is a container resource.
        /// </summary>
        Container = 0x00000002U,
        /// <summary>
        /// The resource is not a local device.
        /// </summary>
        NoLocalDevice = 0x00000004U,
        /// <summary>
        /// The resource is a sibling. This value is not used by Windows.
        /// </summary>
        Sibling = 0x00000008U,
        /// <summary>
        /// The resource must be attached.
        /// </summary>
        Attached = 0x00000010U,
        All = Connectable | Container | Attached,
        Reserved = 0x80000000U
    }
}