namespace DSInternals.Replication.Model;

/// <summary>
/// The ReplicationCursor class represents a replication operation occurrence.
/// </summary>
public class ReplicationCursor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReplicationCursor"/> class.
    /// </summary>
    /// <param name="invocationId">The unique identifier of the replication invocation.</param>
    /// <param name="highestUsn">The highest update sequence number (USN) for the replication.</param>
    public ReplicationCursor(Guid invocationId, long highestUsn)
    {
        this.UpToDatenessUsn = highestUsn;
        this.SourceInvocationId = invocationId;
    }

    /// <summary>
    /// Gets or sets the invocation identifier of the replication source server.
    /// </summary>
    public Guid SourceInvocationId
    {
        get;
        protected set;
    }

    /// <summary>
    /// Gets or sets the maximum update sequence number (USN)
    /// for which the destination server has accepted changes from the source server.
    /// </summary>
    public long UpToDatenessUsn
    {
        get;
        protected set;
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    public override string ToString()
    {
        return $"{SourceInvocationId}: {UpToDatenessUsn}";
    }
}
