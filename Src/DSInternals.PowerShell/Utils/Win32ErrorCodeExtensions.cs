using DSInternals.Common.Interop;
using System.Management.Automation;

namespace DSInternals.PowerShell
{
    internal static class Win32ErrorCodeExtensions
    {
        static internal ErrorCategory ToPSCategory(this Win32ErrorCode code)
        {
            ErrorCategory category;
            switch (code)
            {
                case Win32ErrorCode.ACCESS_DENIED:
                    category = ErrorCategory.PermissionDenied;
                    break;

                case Win32ErrorCode.LOGON_FAILURE:
                    category = ErrorCategory.AuthenticationError;
                    break;

                case Win32ErrorCode.BAD_NETPATH:
                case Win32ErrorCode.RPC_S_SERVER_UNAVAILABLE:
                    category = ErrorCategory.ConnectionError;
                    break;

                case Win32ErrorCode.NOT_FOUND:
                case Win32ErrorCode.NO_SUCH_USER:
                case Win32ErrorCode.NO_SUCH_DOMAIN:
                case Win32ErrorCode.NONE_MAPPED:
                    category = ErrorCategory.ObjectNotFound;
                    break;

                case Win32ErrorCode.PASSWORD_RESTRICTION:
                    category = ErrorCategory.SecurityError;
                    break;

                case Win32ErrorCode.MORE_DATA:
                    category = ErrorCategory.LimitsExceeded;
                    break;

                default:
                    category = ErrorCategory.NotSpecified;
                    break;
            }
            return category;
        }
    }
}