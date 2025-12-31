using DSInternals.Common.Cryptography;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data;

/// <summary>
/// Provides methods for creating <see cref="DSAccount"/> instances from directory objects.
/// </summary>
public static class AccountFactory
{
    /// <summary>
    /// Creates a <see cref="DSAccount"/> instance from the specified directory object.
    /// </summary>
    /// <param name="dsObject">The directory object to create an account from.</param>
    /// <param name="netBIOSDomainName">The NetBIOS domain name.</param>
    /// <param name="pek">The secret decryptor used to decrypt password hashes. Can be <see langword="null"/> if decryption is not needed.</param>
    /// <param name="rootKeyResolver">The KDS root key resolver for decrypting LAPS passwords. Can be <see langword="null"/> if LAPS decryption is not needed.</param>
    /// <param name="propertySets">A bitwise combination of the enumeration values that specifies which property sets to load.</param>
    /// <returns>A <see cref="DSAccount"/> instance, or <see langword="null"/> if the object is not a security principal.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="dsObject"/> parameter is <see langword="null"/>.</exception>
    public static DSAccount? CreateAccount(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, IKdsRootKeyResolver rootKeyResolver = null, AccountPropertySets propertySets = AccountPropertySets.All)
    {
        // Validate the input.
        ArgumentNullException.ThrowIfNull(dsObject);

        // Check that the object is an account
        dsObject.ReadAttribute(CommonDirectoryAttributes.SamAccountType, out SamAccountType? accountType);

        switch (accountType)
        {
            case SamAccountType.User:
                return new DSUser(dsObject, netBIOSDomainName, pek, propertySets);
            case SamAccountType.Computer:
                return new DSComputer(dsObject, netBIOSDomainName, pek, rootKeyResolver, propertySets);
            case SamAccountType.Trust:
                return new DSAccount(dsObject, netBIOSDomainName, pek, propertySets);
            default:
                // This object is not an account
                return null;
        }
    }
}
