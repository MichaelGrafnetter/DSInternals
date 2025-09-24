using Windows.Win32;

namespace DSInternals.DataStore;

/// <summary>
/// Options of a domain controller.
/// </summary>
[Flags]
public enum DomainControllerOptions : uint
{
    /// <summary>
    /// No special options are set.
    /// </summary>
    None = 0,

    /// <summary>
    /// The DC is also a global catalog server.
    /// </summary>
    GlobalCatalog = PInvoke.NTDSDSA_OPT_IS_GC,

    /// <summary>
    /// Inbound replication is disabled.
    /// </summary>
    DisableInboundReplication = PInvoke.NTDSDSA_OPT_DISABLE_INBOUND_REPL,

    /// <summary>
    /// Outbound replication is disabled.
    /// </summary>
    DisableOutboundReplication = PInvoke.NTDSDSA_OPT_DISABLE_OUTBOUND_REPL,

    /// <summary>
    /// Connection object translation is disabled.
    /// </summary>
    DisableConnectionTranslation = PInvoke.NTDSDSA_OPT_DISABLE_NTDSCONN_XLATE
}
