
namespace DSInternals.DataStore
{
    /// <summary>
    /// Password Encryption Key List Version
    /// </summary>
    public enum PekListVersion : uint
    {
        /// <summary>
        /// Version used before Windows 2000 RC2.
        /// </summary>
        PreW2kRC2 = 1,

        /// <summary>
        /// Version used since Windows 2000 RC2.
        /// </summary>
        W2k = 2,

        /// <summary>
        /// Version used since Windows Server 2016 TP4
        /// </summary>
        W2016 = 3
    }
}
