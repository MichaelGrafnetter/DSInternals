using System.Management.Automation;
using System.Net;
using DSInternals.ADSI;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands;
public abstract class ADSICommandBase : PSCmdletEx, IDisposable
{
    #region Parameters
    [Parameter(Mandatory = false)]
    [ValidateNotNullOrEmpty]
    [Alias("Host", "DomainController", "DC", "ComputerName")]
    public string Server
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    [ValidateNotNull]
    public PSCredential Credential
    {
        get;
        set;
    }

    #endregion Parameters

    /// <summary>
    /// When overridden in a derived cmdlet, supplies an explicit set of KDS root keys that
    /// replaces the default LDAP-based resolver used by the <see cref="AdsiClient"/>.
    /// Returns <c>null</c> by default, leaving the client's resolver unchanged.
    /// </summary>
    protected virtual KdsRootKey[]? KdsRootKeysOverride => null;

    protected AdsiClient Client
    {
        get;
        private set;
    }

    #region Cmdlet Overrides

    protected override void BeginProcessing()
    {
        // TODO: Debug output
        // TODO: Exception handling
        NetworkCredential netCredential = null;
        if (this.Credential != null)
        {
            // Convert PSCredential to NetworkCredential
            netCredential = this.Credential.GetNetworkCredential();
        }
        this.Client = new AdsiClient(this.Server, netCredential, this.KdsRootKeysOverride);
    }

    #endregion Cmdlet Overrides

    public void Dispose()
    {
        this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && this.Client != null)
        {
            this.Client.Dispose();
            this.Client = null;
        }
    }
}
