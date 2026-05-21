using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;

namespace DSInternals.PowerShell.Commands;

[Cmdlet(VerbsCommon.Get, "ADSIKdsRootKey", DefaultParameterSetName = AllKeysParameterSet)]
[OutputType(typeof(KdsRootKey))]
public class GetADSIKdsRootKeyCommand : ADSICommandBase
{
    private const string AllKeysParameterSet = "All";
    private const string ByGuidParameterSet = "ByGuid";

    [Parameter(Mandatory = true, ParameterSetName = ByGuidParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
    [Alias("Id", "KeyId")]
    public Guid RootKeyId { get; set; }

    [Parameter(Mandatory = false, ParameterSetName = AllKeysParameterSet)]
    public SwitchParameter All { get; set; }

    protected override void ProcessRecord()
    {
        if (this.ParameterSetName == ByGuidParameterSet)
        {
            KdsRootKey? rootKey = this.Client.GetKdsRootKey(this.RootKeyId);

            if (rootKey != null)
            {
                this.WriteObject(rootKey);
            }
            else
            {
                // No key with the given ID has been found. Write non-terminating error.
                var exception = new DirectoryObjectNotFoundException(this.RootKeyId);
                var error = new ErrorRecord(exception, "ADSI_KdsRootKeyIdNotFound", ErrorCategory.ObjectNotFound, this.RootKeyId);
                this.WriteError(error);
            }
        }
        else
        {
            foreach (KdsRootKey rootKey in this.Client.GetKdsRootKeys())
            {
                this.WriteObject(rootKey);
            }
        }
    }
}
