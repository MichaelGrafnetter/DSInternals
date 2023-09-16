
using System;

namespace DSInternals.DataStore
{
    /// <summary>
    /// Domain, forest or DC functional level. 
    /// </summary>
    /// <remarks>
    /// We do not want to be dependent on System.DirectoryServices.ActiveDirectory, so we implement our own enum.
    /// </remarks>
    /// <see>https://msdn.microsoft.com/en-us/library/cc223743.aspx</see>
    public enum FunctionalLevel : int
    {
        Win2000 = 0,
        Win2003Mixed = 1,
        Win2003 = 2,
        Win2008 = 3,
        Win2008R2 = 4,
        Win2012 = 5,
        Win2012R2 = 6,
        Win2016 = 7,
        [Obsolete]
        WinThreshold = Win2016,
        Win2025 = 10
    }
}
