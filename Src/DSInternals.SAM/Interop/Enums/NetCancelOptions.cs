using System;
using Windows.Win32.NetworkManagement.WNet;

    namespace DSInternals.SAM.Interop;

    /// <summary>
    /// Specifies the type of disconnection to perform when calling WNetCancelConnection2.
    /// </summary>
    /// <see>https://learn.microsoft.com/windows/win32/api/winnetwk/nf-winnetwk-wnetcancelconnection2w</see>
    [Flags]
    internal enum NetCancelOptions : uint
    {
        /// <summary>
        /// The system does not update the user profile with information about the disconnection.
        /// </summary>
        NoUpdate = 0U,

        /// <summary>
        /// The system updates the user profile with the information that the connection is no longer a persistent one.
        /// </summary>
        UpdateProfile = (uint)NET_CONNECT_FLAGS.CONNECT_UPDATE_PROFILE
    }
