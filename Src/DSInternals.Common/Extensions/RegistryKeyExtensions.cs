using DSInternals.Common.Interop;
using Microsoft.Win32;
using System;

namespace DSInternals.Common
{
    public static class RegistryKeyExtensions
    {
        public static string GetClass(this RegistryKey key)
        {
            string keyClass;
            DateTime lastWrite;
            Win32ErrorCode result = NativeMethods.RegQueryInfoKey(key.Handle, out keyClass, out lastWrite);
            Validator.AssertSuccess(result);
            return keyClass;                
        }
    }
}
