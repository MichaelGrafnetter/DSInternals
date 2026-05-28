using DSInternals.Replication.Model;

namespace DSInternals.Replication;

/// <summary>
/// Progress information reported during a replication operation.
/// </summary>
/// <param name="Cookie">The current replication cookie, which captures the position of the replication cursor.</param>
/// <param name="ProcessedObjectCount">The number of directory objects processed so far.</param>
/// <param name="TotalObjectCount">The estimated total number of directory objects to be replicated, as reported by the source domain controller.</param>
public readonly record struct ReplicationProgress(
    ReplicationCookie Cookie,
    int ProcessedObjectCount,
    int TotalObjectCount);
