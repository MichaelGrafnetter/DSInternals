using System.Diagnostics.CodeAnalysis;
using Windows.Win32;

namespace DSInternals.Common.DNS;

/// <summary>
/// Specifies the mapping behavior flags for WINS and WINSR resource records (DNS_WINS_FLAG_*).
/// </summary>
[Flags]
public enum WinsMappingFlags : uint
{
    /// <summary>
    /// No flags set.
    /// </summary>
    None = 0,

    /// <summary>
    /// The record is replicated only within the local subnet. (DNS_WINS_FLAG_LOCAL)
    /// </summary>
    Local = PInvoke.DNS_WINS_FLAG_LOCAL,

    /// <summary>
    /// The record is scoped to the WINS lookup operation. (DNS_WINS_FLAG_SCOPE)
    /// </summary>
    Scope = PInvoke.DNS_WINS_FLAG_SCOPE,
}
