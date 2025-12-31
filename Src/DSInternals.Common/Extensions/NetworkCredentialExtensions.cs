using System.Net;

namespace DSInternals.Common;


public static class NetworkCredentialExtensions
{
    /// <summary>
    /// Gets either UPN or Down-Level Logon Name
    /// </summary>
    public static string GetLogonName(this NetworkCredential credential)
    {
        string logonName = null;

        if (credential != null)
        {
            if (String.IsNullOrEmpty(credential.Domain))
            {
                // The domain is either not specified or the UserName already contains it in the form of a UPN.
                logonName = credential.UserName;
            }
            else
            {
                // Combine Domain with UserName.
                logonName = $@"{credential.Domain}\{credential.UserName}";
            }
        }

        return logonName;
    }
}
