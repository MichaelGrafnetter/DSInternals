using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using DSInternals.Common.Interop;
using Microsoft.Win32;

namespace DSInternals.Common.Cryptography;

/// <summary>
/// Provides methods for protecting and unprotecting DPAPI-NG data and for managing named DPAPI-NG protection descriptors.
/// </summary>
public static class DpapiNg
{
    private const string NamedDescriptorsSubKey = @"Software\Microsoft\Cryptography\NProtect\NamedDescriptors";

    /// <summary>
    /// Protects binary data with a DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="descriptor">The DPAPI-NG protection descriptor rule string.</param>
    /// <param name="data">The cleartext data to protect.</param>
    /// <returns>The protected DPAPI-NG blob.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="descriptor" /> or <paramref name="data" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="CryptographicException">The cleartext data cannot be protected.</exception>
    public static byte[] ProtectSecret(string descriptor, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(descriptor);
        ArgumentNullException.ThrowIfNull(data);

        Win32ErrorCode resultCode = NativeMethods.NCryptProtectSecret(descriptor, data, out byte[] protectedBlob);
        Validator.AssertSuccess(resultCode);
        return protectedBlob;
    }

    /// <summary>
    /// Protects binary data with a named DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="descriptorName">The name of a registered DPAPI-NG protection descriptor.</param>
    /// <param name="machine">Specifies whether to resolve the descriptor name in the machine registry hive.</param>
    /// <param name="data">The cleartext data to protect.</param>
    /// <returns>The protected DPAPI-NG blob.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="descriptorName" /> or <paramref name="data" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="CryptographicException">The cleartext data cannot be protected.</exception>
    public static byte[] ProtectSecret(string descriptorName, bool machine, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(descriptorName);
        ArgumentNullException.ThrowIfNull(data);

        Win32ErrorCode resultCode = NativeMethods.NCryptProtectSecret(descriptorName, machine, data, out byte[] protectedBlob);
        Validator.AssertSuccess(resultCode);
        return protectedBlob;
    }

    /// <summary>
    /// Protects text with a DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="descriptor">The DPAPI-NG protection descriptor rule string.</param>
    /// <param name="cleartext">The cleartext to protect.</param>
    /// <param name="encoding">The text encoding used to convert <paramref name="cleartext" /> to bytes before encryption. Defaults to UTF-16 little-endian (<see cref="Encoding.Unicode" />).</param>
    /// <returns>The protected DPAPI-NG blob.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="descriptor" /> or <paramref name="cleartext" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="CryptographicException">The cleartext cannot be protected.</exception>
    public static byte[] ProtectSecret(string descriptor, string cleartext, Encoding? encoding = null)
    {
        ArgumentNullException.ThrowIfNull(descriptor);
        ArgumentNullException.ThrowIfNull(cleartext);

        encoding ??= Encoding.Unicode;
        return ProtectSecret(descriptor, encoding.GetBytes(cleartext));
    }

    /// <summary>
    /// Protects text with a named DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="descriptorName">The name of a registered DPAPI-NG protection descriptor.</param>
    /// <param name="machine">Specifies whether to resolve the descriptor name in the machine registry hive.</param>
    /// <param name="cleartext">The cleartext to protect.</param>
    /// <param name="encoding">The text encoding used to convert <paramref name="cleartext" /> to bytes before encryption. Defaults to UTF-16 little-endian (<see cref="Encoding.Unicode" />).</param>
    /// <returns>The protected DPAPI-NG blob.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="descriptorName" /> or <paramref name="cleartext" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="CryptographicException">The cleartext cannot be protected.</exception>
    public static byte[] ProtectSecret(string descriptorName, bool machine, string cleartext, Encoding? encoding = null)
    {
        ArgumentNullException.ThrowIfNull(descriptorName);
        ArgumentNullException.ThrowIfNull(cleartext);

        encoding ??= Encoding.Unicode;
        return ProtectSecret(descriptorName, machine, encoding.GetBytes(cleartext));
    }

    /// <summary>
    /// Decrypts a DPAPI-NG protected blob.
    /// </summary>
    /// <param name="protectedBlob">The DPAPI-NG protected blob.</param>
    /// <returns>The decrypted cleartext, or <see langword="null" /> if <paramref name="protectedBlob" /> is empty.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="protectedBlob" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="CryptographicException">The protected blob cannot be decrypted.</exception>
    public static byte[]? UnprotectSecret(byte[] protectedBlob)
    {
        ArgumentNullException.ThrowIfNull(protectedBlob);

        if (protectedBlob.Length == 0)
        {
            return null;
        }

        Win32ErrorCode resultCode = NativeMethods.NCryptUnprotectSecret(protectedBlob, out byte[] decryptedData);
        Validator.AssertSuccess(resultCode);
        return decryptedData;
    }

    /// <summary>
    /// Decrypts a DPAPI-NG protected blob and decodes the cleartext as text.
    /// </summary>
    /// <param name="protectedBlob">The DPAPI-NG protected blob.</param>
    /// <param name="encoding">The text encoding of the decrypted data.</param>
    /// <returns>The decrypted text, without a single trailing null terminator if one is present, or <see langword="null" /> if <paramref name="protectedBlob" /> is empty.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="protectedBlob" /> or <paramref name="encoding" /> parameter is <see langword="null" />.</exception>
    /// <exception cref="FormatException">The decrypted text is not valid for the specified UTF-16 encoding.</exception>
    /// <exception cref="CryptographicException">The protected blob cannot be decrypted.</exception>
    public static string? UnprotectSecret(byte[] protectedBlob, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(protectedBlob);
        ArgumentNullException.ThrowIfNull(encoding);

        byte[]? cleartext = UnprotectSecret(protectedBlob);

        if (cleartext == null)
        {
            return null;
        }

        if (cleartext.Length == 0)
        {
            return string.Empty;
        }

        bool isUtf16 = encoding.CodePage == Encoding.Unicode.CodePage || encoding.CodePage == Encoding.BigEndianUnicode.CodePage;
        if (isUtf16 && cleartext.Length % sizeof(char) != 0)
        {
            throw new FormatException("The decrypted text is not a valid UTF-16 string.");
        }

        string text = encoding.GetString(cleartext);
        return text.Length > 0 && text[^1] == '\0' ? text[..^1] : text;
    }

    /// <summary>
    /// Registers or updates a named DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="name">The descriptor name.</param>
    /// <param name="descriptor">The DPAPI-NG protection descriptor rule string.</param>
    /// <param name="machine">Specifies whether to register the descriptor in the machine registry hive.</param>
    public static void RegisterDescriptor(string name, string descriptor, bool machine = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(descriptor);

        Win32ErrorCode resultCode = NativeMethods.NCryptRegisterProtectionDescriptorName(name, descriptor, machine);
        Validator.AssertSuccess(resultCode);
    }

    /// <summary>
    /// Queries a named DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="name">The descriptor name.</param>
    /// <param name="machine">Specifies whether to query the machine registry hive.</param>
    /// <returns>The DPAPI-NG protection descriptor rule string.</returns>
    /// <exception cref="ArgumentException">The <paramref name="name" /> parameter is <see langword="null" /> or empty.</exception>
    /// <exception cref="System.ComponentModel.Win32Exception">No descriptor is registered with the specified <paramref name="name" />, or another native error occurred.</exception>
    public static string QueryDescriptor(string name, bool machine = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Win32ErrorCode resultCode = NativeMethods.NCryptQueryProtectionDescriptorName(name, machine, out string descriptor);
        Validator.AssertSuccess(resultCode);

        return descriptor;
    }

    /// <summary>
    /// Lists named DPAPI-NG protection descriptors.
    /// </summary>
    /// <param name="machine">Specifies whether to list descriptors from the machine registry hive.</param>
    /// <returns>A collection of descriptor names and rule strings.</returns>
    public static ReadOnlyCollection<KeyValuePair<string, string>> ListDescriptors(bool machine = false)
    {
        using RegistryKey key = OpenRegistryKey(machine, writable: false);
        var descriptors = new List<KeyValuePair<string, string>>();

        if (key == null)
        {
            return descriptors.AsReadOnly();
        }

        foreach (string valueName in key.GetValueNames())
        {
            if (key.GetValueKind(valueName) != RegistryValueKind.String)
            {
                continue;
            }

            if (key.GetValue(valueName) is string descriptor)
            {
                descriptors.Add(new KeyValuePair<string, string>(valueName, descriptor));
            }
        }

        return descriptors.AsReadOnly();
    }

    /// <summary>
    /// Deletes a named DPAPI-NG protection descriptor.
    /// </summary>
    /// <param name="name">The descriptor name.</param>
    /// <param name="machine">Specifies whether to delete the descriptor from the machine registry hive.</param>
    /// <returns><see langword="true" /> if the descriptor existed; otherwise, <see langword="false" />.</returns>
    public static bool DeleteDescriptor(string name, bool machine = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        using RegistryKey? key = OpenRegistryKey(machine, writable: true);
        if (key is null)
        {
            return false;
        }

        bool exists = key.GetValueNames().Contains(name, StringComparer.OrdinalIgnoreCase);
        if (exists)
        {
            key.DeleteValue(name, throwOnMissingValue: false);
        }

        return exists;
    }

    private static RegistryKey? OpenRegistryKey(bool machine, bool writable)
    {
        RegistryHive hive = machine ? RegistryHive.LocalMachine : RegistryHive.CurrentUser;
        RegistryView view = machine && Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default;
        using RegistryKey rootKey = RegistryKey.OpenBaseKey(hive, view);
        return rootKey.OpenSubKey(NamedDescriptorsSubKey, writable);
    }
}
