namespace DSInternals.Replication
{
    using DSInternals.Replication.Model;

    public delegate void ReplicationProgressHandler(ReplicationCookie cookie, int processedObjectCount, int totalObjectCount);
}