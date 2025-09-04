using System;

namespace DSInternals.DataStore
{
    /// <summary>
    /// Specifies configuration options and capabilities for Active Directory domain controllers.
    /// </summary>
    [Flags]
    public enum DomainControllerOptions : int
    {
        None = 0,
        GlobalCatalog = 1,
        DisableInboundReplication = 2,
        DisableOutboundReplication = 4,
        DisableConnectionTranslation = 8
    }
}
