using DSInternals.Common.Cryptography;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data
{
    /// <summary>
    /// Factory class for creating appropriate DSAccount-derived objects (users, computers, trusts) from directory objects.
    /// </summary>
    public static class AccountFactory
    {
        /// <summary>
        /// Creates the appropriate account object (DSUser, DSComputer, or DSAccount) based on the object's SAM account type.
        /// </summary>
        /// <param name="dsObject">The directory object to convert.</param>
        /// <param name="netBIOSDomainName">The NetBIOS domain name.</param>
        /// <param name="pek">The password encryption key for decrypting secrets.</param>
        /// <param name="rootKeyResolver">Optional KDS root key resolver for Group Managed Service Accounts.</param>
        /// <param name="propertySets">The property sets to load for the account.</param>
        /// <returns>A DSAccount-derived object, or null if the object is not an account.</returns>
        public static DSAccount? CreateAccount(DirectoryObject dsObject, string netBIOSDomainName, DirectorySecretDecryptor pek, IKdsRootKeyResolver rootKeyResolver = null, AccountPropertySets propertySets = AccountPropertySets.All)
        {
            // Validate the input.
            Validator.AssertNotNull(dsObject, nameof(dsObject));

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
}
