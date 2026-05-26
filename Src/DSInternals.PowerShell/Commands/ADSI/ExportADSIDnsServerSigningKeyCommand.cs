using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.Export, "ADSIDnsServerSigningKey")]
[Alias("Export-ADSIDnsSigningKey")]
[OutputType("None")]
public class ExportADSIDnsServerSigningKeyCommand : ADSIDnsCommandBase
{
    [Parameter(Mandatory = true, Position = 0)]
    [ValidateNotNullOrEmpty]
    [Alias("Path", "OutputPath", "OutputDirectory", "OutDir")]
    public string DirectoryPath
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    public SwitchParameter Force
    {
        get;
        set;
    }

    [Parameter(Mandatory = false)]
    [ValidateNotNullOrEmpty]
    [Alias("KdsRootKeys", "RootKey", "RootKeys")]
    public KdsRootKey[] KdsRootKey
    {
        get;
        set;
    }

    protected override KdsRootKey[] KdsRootKeysOverride => this.KdsRootKey;

    private string AbsoluteDirectoryPath
    {
        get;
        set;
    }

    protected override void BeginProcessing()
    {
        base.BeginProcessing();

        try
        {
            this.AbsoluteDirectoryPath = this.ResolveDirectoryPath(this.DirectoryPath);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "ResolveDirectoryPathError", ErrorCategory.ObjectNotFound, this.DirectoryPath);
            this.ThrowTerminatingError(error);
        }
    }

    protected override void ProcessRecord()
    {
        foreach (DnsSigningKey signingKey in this.Client.GetDnsSigningKeys(this.ZoneName))
        {
            ExportADDBDnsServerSigningKeyCommand.ExportSigningKey(signingKey, this.AbsoluteDirectoryPath, this.Force.IsPresent, this);
        }
    }
}
