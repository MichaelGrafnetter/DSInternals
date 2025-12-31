namespace DSInternals.Replication.Model;

/// <summary>
/// Identifies specific roles within a domain.
/// </summary>
public class ActiveDirectoryRoleInformation
{
    /// <summary>
    /// Gets or sets the server with the infrastructure role.
    /// </summary>
    public string InfrastructureMaster { get; set; }

    /// <summary>
    /// Gets or sets the server with the domain naming master role.
    /// </summary>
    public string DomainNamingMaster { get; set; }

    /// <summary>
    /// Gets or sets the server with the primary domain controller (PDC) emulator role.
    /// </summary>
    public string PdcEmulator { get; set; }

    /// <summary>
    /// Gets or sets the server with the relative identifier (RID) master role.
    /// </summary>
    public string RidMaster { get; set; }

    /// <summary>
    /// Gets or sets the server with the schema master role.
    /// </summary>
    public string SchemaMaster { get; set; }
}
