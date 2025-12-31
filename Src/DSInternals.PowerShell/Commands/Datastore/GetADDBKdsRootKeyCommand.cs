using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands;
[Cmdlet(VerbsCommon.Get, "ADDBKdsRootKey", DefaultParameterSetName = GetADDBKdsRootKeyCommand.AllKeysParameterSet)]
[OutputType(typeof(DSInternals.Common.Data.KdsRootKey))]
public class GetADDBKdsRootKeyCommand : ADDBCommandBase
{
    private const string AllKeysParameterSet = "All";
    private const string ByGuidParameterSet = "ByGuid";

    [Parameter(Mandatory = true, ParameterSetName = ByGuidParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 1)]
    [Alias("Id", "KeyId")]
    public Guid RootKeyId { get; set; }

    [Parameter(Mandatory = false, ParameterSetName = AllKeysParameterSet)]
    public SwitchParameter All { get; set; }

    protected override void ProcessRecord()
    {
        base.ProcessRecord();

        bool findSingle = this.ParameterSetName == ByGuidParameterSet;
        DatastoreRootKeyResolver resolver = new(this.DirectoryContext);

        if (findSingle)
        {
            KdsRootKey? rootKey = resolver.GetKdsRootKey(this.RootKeyId);

            if (rootKey != null)
            {
                this.WriteObject(rootKey);
            }
            else
            {
                // No key with the given ID has been found. Write non-terminating error.
                var exception = new DirectoryObjectNotFoundException(this.RootKeyId);
                var error = new ErrorRecord(exception, "Database_KdsRootKeyIdNotFound", ErrorCategory.ObjectNotFound, this.RootKeyId);
                this.WriteError(error);
            }
        }
        else
        {
            foreach (var rootKey in resolver.GetKdsRootKeys())
            {
                this.WriteObject(rootKey);
            }
        }
    }
}
