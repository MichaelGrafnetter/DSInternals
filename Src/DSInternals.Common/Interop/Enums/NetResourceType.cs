namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The type of resource.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385353.aspx</see>
    internal enum NetResourceType : uint
    {
        /// <summary>
        /// All resources.
        /// </summary>
        Any = 0x00000000U,
        /// <summary>
        /// Disk resources.
        /// </summary>
        Disk = 0x00000001U,
        /// <summary>
        /// Print resources.
        /// </summary>
        Print = 0x00000002U,
        Reserved = 0x00000008U,
        Unknown = 0xFFFFFFFFU
    }
}