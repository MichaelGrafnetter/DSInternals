using System.Globalization;
using DSInternals.Common;
using DSInternals.Common.Interop;
using Microsoft.Win32;

namespace DSInternals.DataStore;

public static class BootKeyRetriever
{
    public const int BootKeyLength = 16;
    // AD DS Constants:
    private const string CurrentControlSetKey = "Select";
    private const string CurrentControlSetValue = "Current";
    private const string SystemKey = "SYSTEM";
    private const string LsaKeyFormat = @"ControlSet{0:D3}\Control\Lsa\";
    private const int DefaultControlSetId = 1;
    private static readonly string[] BootKeySubKeyNames = { "JD", "Skew1", "GBG", "Data" };
    private static readonly int[] KeyPermutation = { 8, 5, 4, 2, 11, 9, 13, 3, 0, 6, 1, 12, 14, 10, 15, 7 };
    // AD LDS Constants:
    private const int AdamPekListLength = 40;
    private static readonly int[] AdamRootPekListPermutation = { 2, 4, 25, 9, 7, 27, 5, 11 };
    private static readonly int[] AdamSchemaPekListPermutation = { 37, 2, 17, 36, 20, 11, 22, 7 };

    /// <summary>
    /// Gets the boot key from an offline SYSTEM registry hive.
    /// </summary>
    /// <param name="hiveFilePath">SYSTEM hive file path.</param>
    /// <returns>Boot Key</returns>
    public static byte[] GetBootKey(string hiveFilePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hiveFilePath);
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
            using (var system = hklm.OpenSubKey(SystemKey))
            {
                return GetBootKey(system);
            }
        }
    }

    /// <summary>
    /// Gets the boot key from the ADAM root and schema objects.
    /// </summary>
    /// <returns>Boot Key</returns>
    public static byte[] GetBootKey(byte[] rootPekList, byte[] schemaPekList)
    {
        Validator.AssertLength(rootPekList, AdamPekListLength);
        Validator.AssertLength(schemaPekList, AdamPekListLength);

        byte[] result = new byte[BootKeyLength];

        // Construct the first 8 bytes of bootkey:
        for (int i = 0; i < AdamRootPekListPermutation.Length; i++)
        {
            result[i] = rootPekList[AdamRootPekListPermutation[i]];
        }

        // Construct the remaining 8 bytes of bootkey:
        for (int i = 0; i < AdamSchemaPekListPermutation.Length; i++)
        {
            result[i + AdamRootPekListPermutation.Length] = schemaPekList[AdamSchemaPekListPermutation[i]];
        }

        return result;
    }

    private static byte[] GetBootKey(RegistryKey systemKey)
    {
        int currentControlSetId = GetCurrentControlSetId(systemKey);
        string lsaKeyName = String.Format(CultureInfo.InvariantCulture, LsaKeyFormat, currentControlSetId);
        byte[] bootKey = new byte[BootKeyLength];
        using (var lsa = systemKey.OpenSubKey(lsaKeyName))
        {
            for (int i = 0; i < BootKeySubKeyNames.Length; i++)
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
            if (ccsKey != null)
            {
                return (int)ccsKey.GetValue(CurrentControlSetValue, DefaultControlSetId);
            }
            else
            {
                // The "Select" value may be absent in SYSTEM registry hives that were copied from live systems without the corresponding transaction logs.
                return DefaultControlSetId;
            }
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
