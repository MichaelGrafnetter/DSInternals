namespace DSInternals.Replication.Model;

/// <summary>
/// Specifies how the SID history is added via IDL_DRSAddSidHistory.
/// </summary>
/// <remarks>
/// Original enum name: DRS_ADDSID_FLAGS.
/// </remarks>
/// <seealso href="https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-drsr/76d50efe-d165-42ee-b8e4-5face33fe081" />
[Flags]
public enum AddSidHistoryOptions : uint
{
    /// <summary>
    /// No special behavior.
    /// </summary>
    None = 0,

    /// <summary>
    /// Verifies whether the channel is secure and returns the result in the response.
    /// </summary>
    /// <remarks>
    /// Original flag name: DS_ADDSID_FLAG_PRIVATE_CHK_SECURE.
    /// </remarks>
    CheckSecureChannel = 0x40000000,

    /// <summary>
    /// Appends the source object's SID history to the destination and deletes the source object.
    /// </summary>
    /// <remarks>
    /// Original flag name: DS_ADDSID_FLAG_PRIVATE_DEL_SRC_OBJ.
    /// </remarks>
    DeleteSourceObject = 0x80000000
}
