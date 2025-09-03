using System;
using System.Security.Principal;

namespace DSInternals.Common
{
    /// <summary>
    /// Provides extension methods for NTAccount objects to extract domain and account information.
    /// </summary>
    public static class NTAccountExtensions
    {
        private static readonly char[] DomainNameSeparator = { '\\' };

        /// <summary>
        /// Extracts the NetBIOS domain name from an NT account name.
        /// </summary>
        /// <param name="account">The NT account to extract the domain name from.</param>
        /// <returns>The NetBIOS domain name, or an empty string if no domain is specified.</returns>
        public static string NetBIOSDomainName(this NTAccount account)
        {
            string[] parts = account.Value.Split(DomainNameSeparator, 2);
            return parts.Length == 2 ? parts[0] : String.Empty;
        }
    }
}
