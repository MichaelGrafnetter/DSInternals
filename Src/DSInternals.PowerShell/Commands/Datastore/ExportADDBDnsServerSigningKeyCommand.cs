using System.Management.Automation;
using DSInternals.Common.DNS;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsData.Export, "ADDBDnsServerSigningKey")]
[Alias("Export-ADDBDnsSigningKey")]
[OutputType("None")]
public class ExportADDBDnsServerSigningKeyCommand : ADDBDnsCommandBase
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

    private string? _absoluteDirectoryPath;

    protected override void BeginProcessing()
    {
        base.BeginProcessing();

        try
        {
            _absoluteDirectoryPath = this.ResolveDirectoryPath(this.DirectoryPath);
        }
        catch (Exception ex)
        {
            var error = new ErrorRecord(ex, "ResolveDirectoryPathError", ErrorCategory.ObjectNotFound, this.DirectoryPath);
            this.ThrowTerminatingError(error);
        }
    }

    protected override void ProcessRecord()
    {
        foreach (DnsSigningKey signingKey in this.DirectoryAgent.GetDnsSigningKeys(this.ZoneName))
        {
            ExportSigningKey(signingKey, _absoluteDirectoryPath, this.Force.IsPresent, this);
        }
    }

    internal static void ExportSigningKey(DnsSigningKey signingKey, string absoluteDirectoryPath, bool force, PSCmdlet cmdlet)
    {
        if (signingKey.DecryptionStatus != DnsDecryptionStatus.Success)
        {
            var exception = new InvalidOperationException($"Decryption failed for DNS signing key {signingKey.Guid} in zone '{signingKey.DnsZone}' (status: {signingKey.DecryptionStatus}).");
            cmdlet.WriteError(new ErrorRecord(exception, "ExportDnsSigningKey_DecryptionFailed", ErrorCategory.SecurityError, signingKey));
            return;
        }

        try
        {
            string filePath = signingKey.Save(absoluteDirectoryPath, force);
            cmdlet.WriteVerbose($"Wrote DNS signing key {signingKey.Guid} to '{filePath}'.");
        }
        catch (IOException exception)
        {
            cmdlet.WriteError(new ErrorRecord(exception, "ExportDnsSigningKey_WriteFailed", ErrorCategory.WriteError, signingKey));
        }
        catch (UnauthorizedAccessException exception)
        {
            cmdlet.WriteError(new ErrorRecord(exception, "ExportDnsSigningKey_WriteFailed", ErrorCategory.PermissionDenied, signingKey));
        }
    }
}
