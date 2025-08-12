namespace DSInternals.PowerShell.Commands
{
    using DSInternals.DataStore;
    using System;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Set, "ADDBDomainController", ConfirmImpact = ConfirmImpact.High)]
    [OutputType("None")]
    public class SetADDBDomainControllerCommand : ADDBCommandBase
    {
        private const string EpochParameterSet = "Epoch";
        private const string UsnParameterSet = "USN";
        private const string ExpirationParameterSet = "Expiration";

        [ValidateRange(DSInternals.DataStore.DomainController.UsnMinValue, DSInternals.DataStore.DomainController.UsnMaxValue)]
        [Parameter(Mandatory = true, ParameterSetName = UsnParameterSet)]
        [Alias("USN")]
        public long HighestCommittedUsn;

        [ValidateRange(DSInternals.DataStore.DomainController.EpochMinValue, DSInternals.DataStore.DomainController.EpochMaxValue)]
        [Parameter(Mandatory = true, ParameterSetName = EpochParameterSet)]
        [Alias("DSAEpoch")]
        public int Epoch;

        [Parameter(Mandatory = true, ParameterSetName = ExpirationParameterSet)]
        [Alias("Expiration", "Expire")]
        public DateTime BackupExpiration;

        [Parameter]
        public SwitchParameter Force
        {
            get;
            set;
        }

        protected override bool ReadOnly
        {
            get
            {
                return false;
            }
        }

        // TODO: Extract to base
        protected DirectoryAgent DirectoryAgent
        {
            get;
            private set;
        }

        protected override void BeginProcessing()
        {
            if (!Force.IsPresent)
            {
                // Do not continue with operation until the user enforces it.
                var exception = new ArgumentException(WarningMessage);
                var error = new ErrorRecord(exception, "SetADDBDomainController_ForceRequired", ErrorCategory.InvalidArgument, null);
                this.ThrowTerminatingError(error);
            }

            base.BeginProcessing();

            try
            {
                this.DirectoryAgent = new DirectoryAgent(this.DirectoryContext);
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "TableOpenError", ErrorCategory.OpenError, null);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
            }

        }

        protected override void ProcessRecord()
        {
            try
            {
                switch (this.ParameterSetName)
                {
                    case EpochParameterSet:
                        this.DirectoryAgent.SetDomainControllerEpoch(this.Epoch);
                        break;

                    case UsnParameterSet:
                        this.DirectoryAgent.SetDomainControllerUsn(this.HighestCommittedUsn);
                        break;

                    case ExpirationParameterSet:
                        this.DirectoryAgent.SetDomainControllerBackupExpiration(this.BackupExpiration);
                        break;

                    default:
                        // This should never happen:
                        throw new PSInvalidOperationException(InvalidParameterSetMessage);
                }
            }
            catch (Exception ex)
            {
                var error = new ErrorRecord(ex, "SetADDBDomainController_Process", ErrorCategory.WriteError, null);
                this.WriteError(error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.DirectoryAgent != null)
            {
                this.DirectoryAgent.Dispose();
                this.DirectoryAgent = null;
            }
            base.Dispose(disposing);
        }
    }
}
