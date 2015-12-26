
namespace DSInternals.DataStore
{
    /// <summary>
    /// Format of the Password Encryption Key.
    /// </summary>
    public enum PekListFlags : uint
    {
        /// <summary>
        /// The PEK is not encrypted. This is a transient state between dcpromo and first boot.
        /// </summary>
        Clear = 0,
        /// <summary>
        /// The PEK is encrypted.
        /// </summary>
        Encrypted = 1
    }
}
