namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using System.Management.Automation;

    [Cmdlet(VerbsData.Save, "DPAPIBlob")]
    // TODO: Export None as resource.
    [OutputType("None")]
    public class SaveDPAPIBlobCmdlet : PSCmdletEx
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true
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
            Position = 0
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Path")]
        public string DirectoryPath
        {
            get;
            set;
        }
        protected override void ProcessRecord()
        {
            string resolvedPath = this.ResolveSinglePath(this.DirectoryPath);
            // TODO: Exception handling
            // TODO: Verbose
            // TODO: WhatIf
            this.DPAPIObject.SaveTo(resolvedPath);
        }
    }
}
