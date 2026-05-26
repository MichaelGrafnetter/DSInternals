#nullable enable

using System.Management.Automation;

namespace DSInternals.PowerShell.Commands;

/// <summary>
/// Base class for cmdlets that read DNS data from a live Active Directory server via ADSI.
/// </summary>
public abstract class ADSIDnsCommandBase : ADSICommandBase
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
}
