namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBBackupKey")]
    [OutputType(typeof(DSInternals.Common.Data.DPAPIBackupKey))]
    public class GetADDBBackupKeyCommand : ADDBCommandBase
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "TODO"
        )]
        [ValidateNotNullOrEmpty]
        [ValidateHexString(BootKeyRetriever.BootKeyLength)]
        [Alias("key", "SysKey")]
        public string BootKey
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            byte[] binaryBootKey = this.BootKey.HexToBinary();
            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                foreach(var secret in directoryAgent.GetDPAPIBackupKeys(binaryBootKey))
                {
                    this.WriteObject(secret);
                }
            }
            // TODO: Exception handling
        }
    }
}