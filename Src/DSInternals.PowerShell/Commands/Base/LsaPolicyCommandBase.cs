using System.Management.Automation;
using DSInternals.SAM;

namespace DSInternals.PowerShell.Commands;
public abstract class LsaPolicyCommandBase : PSCmdlet, IDisposable
{
    protected LsaPolicy LsaPolicy
    {
        get;
        private set;
    }

    #region Parameters
    [Parameter(Mandatory = false, Position = 1)]
    [Alias("Server", "ServerName", "Computer", "Machine", "MachineName", "System", "SystemName")]
    [ValidateNotNullOrEmpty]
    public string ComputerName
    {
        get;
        set;
    }
    #endregion Parameters

    #region Cmdlet Overrides

    protected override void BeginProcessing()
    {
        string serverName = this.ComputerName ?? "localhost";
        this.WriteDebug($"Connecting to LSA service running on {serverName}.");
        // TODO: Exception handling (process error category)
        this.LsaPolicy = new LsaPolicy(this.ComputerName, this.RequiredAccessMask);
    }
    #endregion Cmdlet Overrides

    protected abstract LsaPolicyAccessMask RequiredAccessMask
    {
        get;
    }

    public void Dispose()
    {
        this.Dispose(true);
        System.GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && this.LsaPolicy != null)
        {
            this.LsaPolicy.Dispose();
            this.LsaPolicy = null;
        }
    }
}
