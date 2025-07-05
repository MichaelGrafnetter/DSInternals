namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.DataStore;

    [Cmdlet(VerbsCommon.Get, "ADDBServiceAccount")]
    [OutputType(typeof(DSInternals.Common.Data.GroupManagedServiceAccount))]
    public class GetADDBServiceAccountCommand : ADDBCommandBase
    {
        [Parameter(Mandatory = false)]
        [Alias("EffectiveDate", "PasswordLastSet", "PwdLastSet", "Date", "Time", "d", "t")]
        public DateTime? EffectiveTime { get; set; }

        // TODO: Implement gMSA filtering
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (this.DirectoryContext.DomainController.ForestMode < FunctionalLevel.Win2012)
            {
                var record = new ErrorRecord(new NotSupportedException("Support for gMSAs has been added in Windows Server 2012 FFL."), "gMSA_Insuffitient_FFL", ErrorCategory.InvalidOperation, targetObject: null);
                this.ThrowTerminatingError(record);
            }

            // Current date is the default value
            DateTime passwordEffectiveTime = this.EffectiveTime ?? DateTime.Now;

            using(var directoryAgent = new DirectoryAgent(this.DirectoryContext))
            {
                // Now fetch all gMSAs and associate them with the KDS root keys
                foreach (var gmsa in directoryAgent.GetGroupManagedServiceAccounts(passwordEffectiveTime))
                {
                    this.WriteObject(gmsa);
                }
            }
            // TODO: Exception handling
        }
    }
}
