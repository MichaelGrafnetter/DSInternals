namespace DSInternals.Common.Data
{
    /// <summary>
    /// Specifies the volume type.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220496.aspx</see>
    public enum VolumeType : byte
    {
        /// <summary>
        /// Volume not specified.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Operating system volume.
        /// </summary>
        OSV = 0x01,

        /// <summary>
        /// Fixed data volume.
        /// </summary>
        FDV = 0x02,

        /// <summary>
        /// Removable data volume.
        /// </summary>
        RDV = 0x03
    }
}