namespace DSInternals.Common.Data
{
    /// <summary>
    /// Key Source
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220501.aspx</see>
    public enum KeySource : byte
    {
        /// <summary>
        /// On Premises Key Trust
        /// </summary>
        ActiveDirectory = 0x00,

        /// <summary>
        /// Hybrid Azure AD Key Trust
        /// </summary>
        AzureActiveDirectory = 0x01
    }
}