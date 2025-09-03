namespace DSInternals.Replication
{
    using DSInternals.Replication.Model;

    /// <summary>
    /// ReplicationProgressHandler implementation.
    /// </summary>
    public delegate void ReplicationProgressHandler(ReplicationCookie cookie, int processedObjectCount, int totalObjectCount);
}