#nullable enable

using System.Management.Automation;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands;

/// <summary>
/// Base class for cmdlets that read DNS data from an offline <c>ntds.dit</c> database.
/// Opens a <see cref="DSInternals.DataStore.DirectoryAgent"/> in <see cref="BeginProcessing"/> and exposes it to derived
/// cmdlets via the <see cref="DirectoryAgent"/> property. The agent is disposed when the cmdlet is disposed.
/// </summary>
public abstract class ADDBDnsCommandBase : ADDBCommandBase
{
    /// <summary>
    /// When supplied, restricts the result to records belonging to the named DNS zone.
    /// Matched case-insensitively against the zone's FQDN.
    /// </summary>
    [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
    [ValidateNotNullOrEmpty]
    [Alias("Zone", "DnsZone")]
    public string? ZoneName
    {
        get;
        set;
    }

    /// <summary>
    /// The directory agent opened against the database referenced by <see cref="ADDBCommandBase.DatabasePath"/>.
    /// Available after <see cref="BeginProcessing"/> has completed successfully.
    /// </summary>
    protected DirectoryAgent? DirectoryAgent
    {
        get;
        private set;
    }

    protected override void BeginProcessing()
    {
        // Open the directory context first.
        base.BeginProcessing();

        try
        {
            this.DirectoryAgent = new DirectoryAgent(this.DirectoryContext);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "DirectoryAgentError", ErrorCategory.OpenError, this.DatabasePath);
            this.ThrowTerminatingError(error);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && this.DirectoryAgent is not null)
        {
            this.DirectoryAgent.Dispose();
            this.DirectoryAgent = null;
        }

        base.Dispose(disposing);
    }
}
