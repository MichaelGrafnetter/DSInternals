namespace DSInternals.Common.Interop
{
    /// <see>https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-gkdi/4cac87a3-521e-4918-a272-240f8fabed39</see>
    internal enum GroupKeyLevel : uint
    {
        /// <summary>
        /// L0 key
        /// </summary>
        L0 = 0,
        /// <summary>
        /// L1 key
        /// </summary>
        L1 = 1,
        /// <summary>
        /// L2 key
        /// </summary>
        L2 = 2
    }
}
