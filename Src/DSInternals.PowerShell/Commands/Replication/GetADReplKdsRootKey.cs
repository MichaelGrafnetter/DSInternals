using System;
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADReplKdsRootKey")]
    [OutputType(typeof(KdsRootKey))]
    public class GetADReplKdsRootKeyCommand : ADReplCommandBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
        [Alias("Id", "KeyId")]
        public Guid RootKeyId { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                // Try to fetch the object
                var rootKey = this.ReplicationClient.GetKdsRootKey(this.RootKeyId, suppressNotFoundException: false);
                this.WriteObject(rootKey);
            }
            catch (Exception ex)
            {
                // This typically means that the object has not been found or that the user does not have sufficient permissions.
                // TODO: Differentiate between exception types.
                var error = new ErrorRecord(ex, "Replication_KdsRootKeyIdNotFound", ErrorCategory.ObjectNotFound, this.RootKeyId);
                this.WriteError(error);
            }
        }
    }
}
