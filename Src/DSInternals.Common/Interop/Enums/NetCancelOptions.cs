namespace DSInternals.Common.Interop
{
    using System;

    /// <summary>
    /// A set of connection options.
    /// </summary>
    /// <see>http://msdn.microsoft.com/library/windows/desktop/aa385413.aspx</see>
    internal enum NetCancelOptions : uint
    {
        /// <summary>
        /// The system does not update information about the connection. 
        /// </summary>
        NoUpdate = 0U,

        /// <summary>
        /// The system updates the user profile with the information that the connection is no longer a persistent one. 
        /// </summary>
        UpdateProfile = 0x00000001U
     }
}