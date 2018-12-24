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
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                foreach(var secret in directoryAgent.GetDPAPIBackupKeys(this.BootKey))
                {
                    this.WriteObject(secret);
                }
            }
            // TODO: Exception handling
        }
    }
}