namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.IO;
    using System.Management.Automation;
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBBitlockerRecoveryInfo")]
    [OutputType(typeof(DSInternals.Common.Data.BitlockerRecoveryInfo))]
    public class GetADDBBitlockerRecoveryInfoCommand : ADDBCommandBase
    {
        #region Constants
        protected Guid recoveryGuid = Guid.Empty;
        protected string exportKeysPath = null;
        #endregion Constants

        #region Parameters
        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public string RecoveryGuid
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [ValidateNotNull]
        public string ExportKeysPath
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (RecoveryGuid != null && RecoveryGuid.Length > 0)
            {
                recoveryGuid = Guid.Parse(RecoveryGuid);
            }

            if (ExportKeysPath != null && ExportKeysPath.Length > 0)
            {
                this.exportKeysPath = ExportKeysPath;
            }

            using (var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                if (recoveryGuid != Guid.Empty)
                {
                    var obj = directoryAgent.GetBitlockerRecoveryInfoSingle(recoveryGuid, this.exportKeysPath);
                    if (obj != null)
                    {
                        this.WriteObject(obj);
                    }
                }
                else
                {
                    foreach (var obj in directoryAgent.GetBitlockerRecoveryInfoAll(this.exportKeysPath))
                    {
                        this.WriteObject(obj);
                    }
                }
            }
        }
        #endregion Cmdlet Overrides
    }
}
