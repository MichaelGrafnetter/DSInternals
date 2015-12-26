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
    public enum InstanceType : int
    {
        /// <summary>
        /// The object is not writable on this directory and is not a naming context.
        /// </summary>
        None = 0,
        /// <summary>
        /// The head of naming context.
        /// </summary>
        NamingContextHead = 0x00000001,
        /// <summary>
        /// This replica is not instantiated.
        /// </summary>
        NotInstantiated = 0x00000002,
        /// <summary>
        /// The object is writable on this directory.
        /// </summary>
        Writable = 0x00000004,
        /// <summary>
        /// The naming context above this one on this directory is held.
        /// </summary>
        NamingContextAbove = 0x00000008,
        /// <summary>
        /// The naming context is being constructed for the first time via replication.
        /// </summary>
        Constructing = 0x00000010,
        /// <summary>
        /// The naming context is being removed from the local directory system agent (DSA).
        /// </summary>
        Removing = 0x00000020
    }
}
