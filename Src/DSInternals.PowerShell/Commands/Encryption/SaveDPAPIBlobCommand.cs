namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using System;
    using System.Linq;
    using System.Management.Automation;

    [Cmdlet(VerbsData.Save, "DPAPIBlob")]
    // TODO: Export None as resource.
    [OutputType("None")]
    public class SaveDPAPIBlobCmdlet : PSCmdletEx
    {
        private const string VerboseMessageFormat = "Creating DPAPI file {0}.";
        private const string AccountParameterSet = "FromAccount";
        private const string ObjectParameterSet = "FromObject";

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = ObjectParameterSet
        )]
        [ValidateNotNullOrEmpty]
        [Alias("DPAPIBlob", "Object", "Blob", "BackupKey")]
        public DPAPIObject DPAPIObject
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = AccountParameterSet
        )]
        [ValidateNotNullOrEmpty]
        public DSAccount Account
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            Position = 0
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Path", "OutputPath")]
        public string DirectoryPath
        {
            get;
            set;
        }

        private string AbsoluteDirectoryPath
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            this.AbsoluteDirectoryPath = this.ResolveDirectoryPath(this.DirectoryPath);
        }

        protected override void ProcessRecord()
        {
            switch(this.ParameterSetName)
            {
                case ObjectParameterSet:
                    this.ProcessSingleObject(this.DPAPIObject);
                    break;
                case AccountParameterSet:
                    // Extract all roamed credentials from a user account. Other account types do not have roamed credentials.
                    var user = this.Account as DSUser;

                    if(user?.RoamedCredentials != null)
                    {
                        foreach (var blob in user.RoamedCredentials)
                        {
                            this.ProcessSingleObject(blob);
                        }
                    }
                    
                    break;
            }
        }

        private void ProcessSingleObject(DPAPIObject blob)
        {
            string filePath = blob.FilePath;

            if (String.IsNullOrEmpty(filePath))
            {
                // There is nothing to save
                return;
            }

            // Save the blob
            string verboseMessage = String.Format(VerboseMessageFormat, filePath);
            this.WriteVerbose(verboseMessage);
            blob.Save(this.AbsoluteDirectoryPath);

            // Append the Mimikatz command to a script file
            blob.SaveKiwiCommand(this.AbsoluteDirectoryPath);
        }
    }
}
