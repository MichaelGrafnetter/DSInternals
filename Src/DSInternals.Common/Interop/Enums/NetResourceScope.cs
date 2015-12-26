namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The scope of the enumeration.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385353.aspx</see>
    internal enum NetResourceScope : uint
    {
        /// <summary>
        /// Enumerate currently connected resources. The dwUsage member cannot be specified.
        /// </summary>
        Connected = 0x00000001U,
        /// <summary>
        /// Enumerate all resources on the network. The dwUsage member is specified.
        /// </summary>
        Globalnet = 0x00000002U,
        /// <summary>
        /// Enumerate remembered (persistent) connections. The dwUsage member cannot be specified.
        /// </summary>
        Remembered = 0x00000003U,
        Recent = 0x00000004U,
        Context = 0x00000005U
    }
}