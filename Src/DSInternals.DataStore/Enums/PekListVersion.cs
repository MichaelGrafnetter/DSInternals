
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
        Current = 2
    }
}
