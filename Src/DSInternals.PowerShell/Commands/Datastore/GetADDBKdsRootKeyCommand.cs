namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBKdsRootKey")]
    [OutputType(typeof(DSInternals.Common.Data.KdsRootKey))]
    public class GetADDBKdsRootKeyCommand : ADDBCommandBase
    {
        // TODO: Add optional Guid parameter

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            // TODO: Remove this from the final commit:
            this.Host.UI.WriteLine("DSInternals 2.17 Preview [In cooperation with CQURE Team for DPAPI-NG decryption]");
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