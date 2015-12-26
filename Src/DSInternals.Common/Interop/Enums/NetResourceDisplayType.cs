namespace DSInternals.Common.Interop
{
    /// <summary>
    /// The display options for the network object in a network browsing user interface.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385353.aspx</see>
    internal enum NetResourceDisplayType : uint
    {
        /// <summary>
        /// The method used to display the object does not matter.
        /// </summary>
        Generic = 0x00000000U,
        /// <summary>
        /// The object should be displayed as a domain.
        /// </summary>
        Domain = 0x00000001U,
        /// <summary>
        /// The object should be displayed as a server.
        /// </summary>
        Server = 0x00000002U,
        /// <summary>
        /// The object should be displayed as a share.
        /// </summary>
        Share = 0x00000003U,
        /// <summary>
        /// The object should be displayed as a file.
        /// </summary>
        File = 0x00000004U,
        /// <summary>
        /// The object should be displayed as a group.
        /// </summary>
        Group = 0x00000005U,
        /// <summary>
        /// The object should be displayed as a network.
        /// </summary>
        Network = 0x00000006U,
        /// <summary>
        /// The object should be displayed as a logical root for the entire network.
        /// </summary>
        Root = 0x00000007U,
        /// <summary>
        /// The object should be displayed as a administrative share.
        /// </summary>
        ShareAdmin = 0x00000008U,
        /// <summary>
        /// The object should be displayed as a directory.
        /// </summary>
        Directory = 0x00000009U,
        /// <summary>
        /// The object should be displayed as a tree. This display type was used for a NetWare Directory Service (NDS) tree by the NetWare Workstation service supported on Windows XP and earlier.
        /// </summary>
        Tree = 0x0000000AU,
        /// <summary>
        /// The object should be displayed as a Netware Directory Service container. This display type was used by the NetWare Workstation service supported on Windows XP and earlier.
        /// </summary>
        NDSContainer = 0x0000000BU
    }
}