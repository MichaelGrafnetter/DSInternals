using System.Security.Principal;

namespace DSInternals.Common;

public static class NTAccountExtensions
{
    private static readonly char[] DomainNameSeparator = { '\\' };

    public static string NetBIOSDomainName(this NTAccount account)
    {
        string[] parts = account.Value.Split(DomainNameSeparator, 2);
        return parts.Length == 2 ? parts[0] : String.Empty;
    }
}
