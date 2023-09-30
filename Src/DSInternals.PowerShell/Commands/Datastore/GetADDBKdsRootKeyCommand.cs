namespace DSInternals.PowerShell.Commands
{
    using System.Management.Automation;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBKdsRootKey")]
    [OutputType(typeof(DSInternals.Common.Data.KdsRootKey))]
    public class GetADDBKdsRootKeyCommand : ADDBCommandBase
    {
        // TODO: Add optional Guid parameter

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                foreach(var rootKey in directoryAgent.GetKdsRootKeys())
                {
                    this.WriteObject(rootKey);
                }
            }
            // TODO: Exception handling
        }
    }
}
