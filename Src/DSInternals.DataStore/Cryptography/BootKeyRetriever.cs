using DSInternals.Common;
using DSInternals.Common.Interop;
using Microsoft.Win32;
using System;
using System.Globalization;

namespace DSInternals.DataStore
{
    public static class BootKeyRetriever
    {
        public const int BootKeyLength = 16;
        private const string CurrentControlSetKey = "Select";
        private const string CurrentControlSetValue = "Current";
        private const string SystemKey = "SYSTEM";
        private const string LsaKeyFormat = @"ControlSet{0:D3}\Control\Lsa\";
        private const int DefaultControlSetId = 1;
        private static readonly string[] BootKeySubKeyNames = { "JD", "Skew1", "GBG", "Data" };
        private static readonly int[] KeyPermutation = { 8, 5, 4, 2, 11, 9, 13, 3, 0, 6, 1, 12, 14, 10, 15, 7 };

        /// <summary>
        /// Gets the boot key from an offline SYSTEM registry hive.
        /// </summary>
        /// <param name="hiveFilePath">SYSTEM hive file path.</param>
        /// <returns>Boot Key</returns>
        public static byte[] GetBootKey(string hiveFilePath)
        {
            Validator.AssertNotNullOrWhiteSpace(hiveFilePath, "hiveFilePath");
            Validator.AssertFileExists(hiveFilePath);
            using (var hive = new RegistryHiveFileMapping(hiveFilePath))
            {
                using (var system = hive.RootKey)
                {
                    return GetBootKey(system);
                }
            }
        }

        /// <summary>
        /// Gets the boot key of the local computer.
        /// </summary>
        /// <returns>Boot Key</returns>
        public static byte[] GetBootKey()
        {
            using (var hklm = Registry.LocalMachine)
            {
                using(var system = hklm.OpenSubKey(SystemKey))
                {
                    return GetBootKey(system);
                }
            }
        }

        private static byte[] GetBootKey(RegistryKey systemKey)
        {
            int currentControlSetId = GetCurrentControlSetId(systemKey);
            string lsaKeyName = String.Format(LsaKeyFormat, currentControlSetId);
            byte[] bootKey = new byte[BootKeyLength];
            using(var lsa = systemKey.OpenSubKey(lsaKeyName))
            {
                for(int i = 0; i < BootKeySubKeyNames.Length; i++)
                {
                    using (var bootKeyPartKey = lsa.OpenSubKey(BootKeySubKeyNames[i]))
                    {
                        byte[] bootKeyPart = bootKeyPartKey.GetClass().HexToBinary();
                        // Append this part to the boot key:
                        Array.Copy((Array)bootKeyPart, 0, (Array)bootKey, i * bootKeyPart.Length, bootKeyPart.Length);
                    }
                }
            }
            return DecodeKey(bootKey);
        }

        private static int GetCurrentControlSetId(RegistryKey systemKey)
        {
            using (var ccsKey = systemKey.OpenSubKey(CurrentControlSetKey))
            {
                return (int)ccsKey.GetValue(CurrentControlSetValue, DefaultControlSetId);
            }
        }

        private static byte[] DecodeKey(byte[] bootKey)
        {
            // Apply permutation to BootKey
            byte[] decodedBootKey = new byte[BootKeyLength];
            for (int i = 0; i < BootKeyLength; i++)
            {
                decodedBootKey[i] = bootKey[KeyPermutation[i]];
            }
            return decodedBootKey;
        }
    }
}
