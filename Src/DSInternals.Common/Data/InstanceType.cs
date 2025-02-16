using System;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// A bit field that dictates how the object is instantiated on a particular domain controller.
    /// The value of this attribute can differ on different replicas even if the replicas are in sync.
    /// This attribute can be zero or a combination of one or more of the following bit flags.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/cc219986.aspx</see>
    [Flags]
    public enum InstanceType : uint
    {
        /// <summary>
        /// The object is not writable on this directory and is not a naming context.
        /// </summary>
        None = 0,
        /// <summary>
        /// The head of naming context.
        /// </summary>
        NamingContextHead = Windows.Win32.PInvoke.DS_INSTANCETYPE_IS_NC_HEAD,
        /// <summary>
        /// This replica is not instantiated.
        /// </summary>
        NotInstantiated = 0x00000002,
        /// <summary>
        /// The object is writable on this directory.
        /// </summary>
        Writable = Windows.Win32.PInvoke.DS_INSTANCETYPE_NC_IS_WRITEABLE,
        /// <summary>
        /// The naming context above this one on this directory is held.
        /// </summary>
        NamingContextAbove = 0x00000008,
        /// <summary>
        /// The naming context is being constructed for the first time via replication.
        /// </summary>
        Constructing = Windows.Win32.PInvoke.DS_INSTANCETYPE_NC_COMING,
        /// <summary>
        /// The naming context is being removed from the local directory system agent (DSA).
        /// </summary>
        Removing = Windows.Win32.PInvoke.DS_INSTANCETYPE_NC_GOING
    }
}
