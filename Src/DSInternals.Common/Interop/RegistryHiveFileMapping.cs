using Microsoft.Win32;
using System.ComponentModel;
using Microsoft.Win32.SafeHandles;
using System.Security.Principal;
using Windows.Win32.Foundation;
using System.Runtime.InteropServices;
using Windows.Win32;

namespace DSInternals.Common.Interop;
public class RegistryHiveFileMapping : IDisposable
{
    public RegistryHiveFileMapping(string hiveFilePath)
    {
        if (string.IsNullOrWhiteSpace(hiveFilePath))
        {
            throw new ArgumentNullException(nameof(hiveFilePath));
        }

        SetProcessPrivilege(PInvoke.SE_BACKUP_NAME, true);
        SetProcessPrivilege(PInvoke.SE_RESTORE_NAME, true);

        this.UsersSubKey = $"DSInternals_{Guid.NewGuid()}";
        Win32ErrorCode result = NativeMethods.RegLoadKey(RegistryHive.Users, this.UsersSubKey, hiveFilePath);
        Validator.AssertSuccess(result);
    }

    public RegistryKey RootKey
    {
        get
        {
            using (var users = Registry.Users)
            {
                return users.OpenSubKey(this.UsersSubKey);
            }
        }
    }

    public string UsersSubKey
    {
        get;
        private set;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RegistryHiveFileMapping()
    {
        this.Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.UsersSubKey != null)
        {
            // Ignore any errors
            NativeMethods.RegUnLoadKey(RegistryHive.Users, this.UsersSubKey);
            this.UsersSubKey = null;

            SetProcessPrivilege(PInvoke.SE_BACKUP_NAME, false);
            SetProcessPrivilege(PInvoke.SE_RESTORE_NAME, false);
        }
    }

    private static void SetProcessPrivilege(string privilege, bool enabled)
    {
        SafeProcessHandle currentProcess = NativeMethods.GetCurrentProcess();
        bool success = NativeMethods.OpenProcessToken(currentProcess, TokenAccessLevels.AdjustPrivileges, out SafeAccessTokenHandle tokenHandle);

        if (!success)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to open the current process token.");
        }

        try
        {
            success = NativeMethods.LookupPrivilegeValue(null, privilege, out LUID luid);

            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), $"Failed to lookup the {privilege} privilege.");
            }

            success = NativeMethods.AdjustTokenPrivileges(tokenHandle, luid, enabled);

            // AdjustTokenPrivileges can return true even if it failed to set the privilege, so we need to use GetLastError.
            int lastError = Marshal.GetLastWin32Error();
            success = success && (lastError == (int)Win32ErrorCode.Success);

            if (!success)
            {
                throw new Win32Exception(lastError, $"Failed to adjust the {privilege} privilege.");
            }
        }
        finally
        {
            tokenHandle.Close();
        }
    }
}
