using System;

namespace DSInternals.Common.Interop
{
    /// <summary>
    /// A set of connection options.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385413.aspx</see>
    [Flags]
    internal enum NetConnectOptions : uint
    {
        /// <summary>
        /// The network resource connection should be remembered.
        /// </summary>
        UpdateProfile = 0x00000001U,
        /// <summary>
        /// The network resource connection should not be put in the recent connection list.
        /// </summary>
        UpdateRecent = 0x00000002U,
        /// <summary>
        /// The network resource connection should not be remembered.
        /// </summary>
        Temporary = 0x00000004U,
        /// <summary>
        /// If this flag is set, the operating system may interact with the user for authentication purposes.
        /// </summary>
        Interactive = 0x00000008U,
        /// <summary>
        /// This flag instructs the system not to use any default settings for user names or passwords without offering the user the opportunity to supply an alternative.
        /// </summary>
        Prompt = 0x00000010U,
        /// <summary>
        /// This flag forces the redirection of a local device when making the connection.
        /// </summary>
        Redirect = 0x00000080,
        /// <summary>
        /// If this flag is set, then the operating system does not start to use a new media to try to establish the connection (initiate a new dial up connection, for example).
        /// </summary>
        CurrentMedia = 0x00000200U,
        /// <summary>
        /// ///If this flag is set, the operating system prompts the user for authentication using the command line instead of a graphical user interface (GUI).
        /// </summary>
        CommandLine = 0x00000800U,
        /// <summary>
        /// If this flag is set, and the operating system prompts for a credential, the credential should be saved by the credential manager.
        /// </summary>
        CmdSaveCred = 0x00001000U,
        /// <summary>
        /// If this flag is set, and the operating system prompts for a credential, the credential is reset by the credential manager.
        /// </summary>
        CredReset = 0x00002000U
    }
}