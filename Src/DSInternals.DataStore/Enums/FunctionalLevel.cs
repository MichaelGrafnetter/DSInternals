using Windows.Win32;

namespace DSInternals.DataStore;

/// <summary>
/// Domain, forest or DC functional level.
/// </summary>
/// <remarks>
/// We do not want to be dependent on System.DirectoryServices.ActiveDirectory, so we implement our own enum.
/// </remarks>
/// <see>https://msdn.microsoft.com/en-us/library/cc223743.aspx</see>
public enum FunctionalLevel : uint
{
    /// <summary>
    /// Windows 2000 Native or Mixed Mode.
    /// </summary>
    Win2000 = PInvoke.DS_BEHAVIOR_WIN2000,

    /// <summary>
    /// Windows Server 2003 with Mixed Domains (also known as Windows 2003 Interim).
    /// </summary>
    Win2003Mixed = PInvoke.DS_BEHAVIOR_WIN2003_WITH_MIXED_DOMAINS,

    /// <summary>
    /// Windows Server 2003.
    /// </summary>
    Win2003 = PInvoke.DS_BEHAVIOR_WIN2003,

    /// <summary>
    /// Windows Server 2008.
    /// </summary>
    Win2008 = PInvoke.DS_BEHAVIOR_WIN2008,

    /// <summary>
    /// Windows Server 2008 R2.
    /// </summary>
    Win2008R2 = PInvoke.DS_BEHAVIOR_WIN2008R2,

    /// <summary>
    /// Windows Server 2012.
    /// </summary>
    Win2012 = PInvoke.DS_BEHAVIOR_WIN2012,

    /// <summary>
    /// Windows Server 2012 R2.
    /// </summary>
    Win2012R2 = PInvoke.DS_BEHAVIOR_WIN2012R2,

    /// <summary>
    /// Windows Server 2016.
    /// </summary>
    Win2016 = PInvoke.DS_BEHAVIOR_WIN2016,

    /// <summary>
    /// Windows Server 2025.
    /// </summary>
    Win2025 = 10
}
