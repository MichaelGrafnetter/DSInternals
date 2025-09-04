namespace DSInternals.Common.Interop
{
    using System;
    using Microsoft.Win32;
    using System.Diagnostics;
    using System.Reflection;
    using System.ComponentModel;

    /// <summary>
    /// Provides functionality for mapping registry hive files to temporary registry keys for read-only access.
    /// </summary>
    public class RegistryHiveFileMapping : IDisposable
    {
        private const string SubKeyFormat = "DSInternals_{0}";
        private const char PathSeparator = '\\';
        private const string BackupPrivilege = "SeBackupPrivilege";
        private const string RestorePrivilege = "SeRestorePrivilege";
        /// <summary>
        /// Initializes a new instance of the RegistryHiveFileMapping class by loading the specified hive file.
        /// </summary>
        /// <param name="hiveFilePath">The path to the registry hive file to load.</param>
        public RegistryHiveFileMapping(string hiveFilePath)
        {
            Validator.AssertNotNullOrWhiteSpace(hiveFilePath, "hiveFilePath");
            SetProcessPrivilege(BackupPrivilege, true);
            SetProcessPrivilege(RestorePrivilege, true);
            this.UsersSubKey = string.Format(SubKeyFormat, Guid.NewGuid());
            Win32ErrorCode result = NativeMethods.RegLoadKey((IntPtr)RegistryHive.Users, this.UsersSubKey, hiveFilePath);
            Validator.AssertSuccess(result);
        }

        /// <summary>
        /// Gets the root registry key for accessing the loaded hive.
        /// </summary>
        public RegistryKey RootKey
        {
            get
            {
                using(var users = Registry.Users)
                {
                    return users.OpenSubKey(this.UsersSubKey);
                }
            }
        }

        /// <summary>
        /// Gets the temporary subkey name used to mount the hive under HKEY_USERS.
        /// </summary>
        public string UsersSubKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
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
            if(this.UsersSubKey != null)
            {
                // Ignore any errors
                Win32ErrorCode result = NativeMethods.RegUnLoadKey((IntPtr)RegistryHive.Users, this.UsersSubKey);
                SetProcessPrivilege(RestorePrivilege, false);
                SetProcessPrivilege(BackupPrivilege, false);
                this.UsersSubKey = null;
            }
        }
        // TODO: Extract as ProcessPrivilege object and member of registryHiveFileMapping.
        private static void SetProcessPrivilege(string privilege, bool enabled)
        {
            // HACK: Private member Process.SetPrivilege is used.
            // TODO: Reimplement SetPrivilege using pinvoke.
            // Method signature: private static void System.Diagnostics.Process.SetPrivilege(string privilegeName, int attrib)
            var SetPrivilegeMethod = typeof(Process).GetMethod("SetPrivilege", BindingFlags.Static | BindingFlags.NonPublic);
            int attrib = enabled ? 2 : 0;
            try
            {
                SetPrivilegeMethod.Invoke(null, new object[] { privilege, attrib });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is Win32Exception && ((Win32Exception)ex.InnerException).NativeErrorCode == (int)Win32ErrorCode.NOT_ALL_ASSIGNED)
                {
                    // Convert Win32Exception to UnauthorizedAccessException
                    // TODO: Extract as resource
                    string message = String.Format("Could not acquire privilege {0}. Try running this program with administrative rights.", privilege);
                    throw new UnauthorizedAccessException(message, ex.InnerException);
                }
                else
                {
                    throw ex.InnerException;
                }
            }
        }

    }
}