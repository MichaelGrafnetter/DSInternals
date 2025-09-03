﻿using System;

namespace DSInternals.DataStore
{
    [Flags]
    /// <summary>
    /// Defines values for DomainControllerOptions.
    /// </summary>
    public enum DomainControllerOptions : int
    {
        None = 0,
        GlobalCatalog = 1,
        DisableInboundReplication = 2,
        DisableOutboundReplication = 4,
        DisableConnectionTranslation = 8
    }
}
