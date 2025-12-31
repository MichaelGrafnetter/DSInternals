namespace DSInternals.DataStore;

public enum DatabaseState
{
    /// <summary>
    /// The initial DIT is being created.
    /// </summary>
    Initial,
    /// <summary>
    /// The initial DIT has been created.
    /// </summary>
    Boot,
    /// <summary>
    /// DcPromo completed. Used in Windows Server 2000.
    /// </summary>
    Installed,
    /// <summary>
    /// DCPromo completed. Used since Windows Server 2003.
    /// </summary>
    Running,
    /// <summary>
    /// Snapshot is being created.
    /// </summary>
    BackedUp,
    /// <summary>
    /// DcPromo has failed.
    /// </summary>
    Error,
    /// <summary>
    /// The first phase of restore is done.
    /// </summary>
    RestoredPhaseI,
    /// <summary>
    /// DcPromo completed.
    /// </summary>
    RealInstalled,
    /// <summary>
    /// DcPromo is performing IFM
    /// </summary>
    Ifm,
    /// <summary>
    /// Demotion has begun.
    /// </summary>
    Demoting,
    /// <summary>
    /// Demotion has finished.
    /// </summary>
    Demoted
}
