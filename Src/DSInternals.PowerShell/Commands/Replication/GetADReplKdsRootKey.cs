using System;
using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.Common.Exceptions;

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

            // Try to fetch the object
            var rootKey = this.ReplicationClient.GetKdsRootKey(this.RootKeyId);

            if (rootKey != null)
            {
                this.WriteObject(rootKey);
            }
            else
            {
                // KDS Root Key not found
                var exception = new DirectoryObjectNotFoundException(this.RootKeyId);
                var error = new ErrorRecord(exception, "Replication_KdsRootKeyIdNotFound", ErrorCategory.ObjectNotFound, this.RootKeyId);
                this.WriteError(error);
            }
        }
    }
}
