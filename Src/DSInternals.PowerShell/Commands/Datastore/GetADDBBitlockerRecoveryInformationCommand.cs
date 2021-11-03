namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Common.Data;
    using DSInternals.DataStore;
    using DSInternals.PowerShell.Properties;

    [Cmdlet(VerbsCommon.Get, "ADDBBitLockerRecoveryInformation")]
    [OutputType(typeof(DSInternals.Common.Data.BitLockerRecoveryInformation))]
    public class GetADDBBitLockerRecoveryInformationCommand : ADDBObjectCommandBase
    {
        #region Parameters
        private const string ParameterSetAll = "All";
        private const string ParameterSetByComputerName = "ByComputerName";
        private const string ParameterSetByKeyIdentifier = "ByKeyIdentifier";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetByKeyIdentifier, ValueFromPipelineByPropertyName = true)]
        [Alias("KeyID", "RecoveryGuid", "RecoveryId")]
        public Guid KeyIdentifier
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetByComputerName, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Computer", "SamAccountName")]
        public string ComputerName
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        public SwitchParameter All
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            // TODO: Exception handling: Object not found, malformed DN, ...
            switch (this.ParameterSetName)
            {
                case ParameterSetAll:
                    foreach (var recoveryInfo in this.DirectoryAgent.GetBitlockerRecoveryInformation())
                    {
                        this.WriteObject(recoveryInfo);
                    }
                    break;

                case ParameterSetByComputerName:
                    foreach (var recoveryInfo in this.DirectoryAgent.GetBitlockerRecoveryInformation(this.ComputerName))
                    {
                        this.WriteObject(recoveryInfo);
                    }
                    break;
                default:
                    this.ReturnSingleObject();
                    break;
            }
        }
        #endregion Cmdlet Overrides
        #region Helper Methods
        private void ReturnSingleObject()
        {
            BitLockerRecoveryInformation info;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    var dn = new DistinguishedName(this.DistinguishedName);
                    info = this.DirectoryAgent.GetBitlockerRecoveryInformation(dn);
                    break;

                case ParameterSetByKeyIdentifier:
                    info = this.DirectoryAgent.GetBitlockerRecoveryInformationByKeyId(this.KeyIdentifier);
                    break;

                case ParameterSetByGuid:
                    info = this.DirectoryAgent.GetBitlockerRecoveryInformation(this.ObjectGuid);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }

            this.WriteObject(info);
        }
        #endregion Helper Methods
    }
}
