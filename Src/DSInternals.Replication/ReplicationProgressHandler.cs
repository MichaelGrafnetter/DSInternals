using DSInternals.Replication.Model;

namespace DSInternals.Replication;

/// <summary>
/// Represents a method that will handle replication progress events.
/// </summary>
public delegate void ReplicationProgressHandler(ReplicationCookie cookie, int processedObjectCount, int totalObjectCount);
