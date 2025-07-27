using DSInternals.Common.Cryptography;
using DSInternals.Common.Schema;

namespace DSInternals.Common.Data
{
    public static class AccountFactory
    {
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
