using System;
using System.Management.Automation;
using System.Security.Principal;
using DSInternals.Common.Data;
using DSInternals.PowerShell.Properties;
using DSInternals.Replication;
using DSInternals.Replication.Model;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "ADReplAccount")]
    [OutputType(typeof(DSAccount), typeof(DSUser), typeof(DSComputer))]
    public class GetADReplAccountCommand : ADReplPrincipalCommandBase
    {
        #region Constants
        protected const string ParameterSetAll = "All";
        #endregion Constants

        #region Parameters
        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetAll)]
        [ValidateNotNullOrEmpty]
        [Alias("NC", "DomainNC", "DomainNamingContext")]
        public string NamingContext
        {
            get;
            set;
        }

        [Parameter(Mandatory = false)]
        [Alias("Property", "PropertySets", "PropertySet")]
        [PSDefaultValue(Value = "All")]
        public AccountPropertySets Properties
        {
            get;
            set;
        } = AccountPropertySets.All;

        [Parameter(Mandatory = false)]
        [Alias("View", "ExportView", "Format")]
        [ValidateSet(
            "JohnNT",
            "JohnNTHistory",
            "JohnLM",
            "JohnLMHistory",
            "HashcatNT",
            "HashcatNTHistory",
            "HashcatLM",
            "HashcatLMHistory",
            "NTHash",
            "NTHashHistory",
            "LMHash",
            "LMHashHistory",
            "Ophcrack",
            "PWDump",
            "PWDumpHistory"
        )]
        public AccountExportFormat? ExportFormat
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (this.ExportFormat != null)
            {
                // Override the property sets to match the requirements of the export formats.
                this.Properties = this.ExportFormat.GetRequiredProperties();
            }
        }

        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts();
            }
            else
            {
                this.ReturnSingleAccount();
            }
        }
        #endregion Cmdlet Overrides

        #region Helper Methods

        protected void ReturnAllAccounts()
        {
            // Write the initial progress
            // TODO: Extract strings as resources
            var progress = new ProgressRecord(1, "Replication", "Replicating Active Directory objects.");
            progress.PercentComplete = 0;
            this.WriteProgress(progress);

            // Update the progress after each replication cycle
            ReplicationProgressHandler progressReporter = (ReplicationCookie cookie, int processedObjectCount, int totalObjectCount) =>
            {
                int percentComplete = (int)(((double)processedObjectCount / (double)totalObjectCount) * 100);
                // AD's object count estimate is sometimes lower than the actual count, so we cap the value to 100%.
                progress.PercentComplete = Math.Min(percentComplete, 100);
                this.WriteProgress(progress);
            };

            // Automatically infer domain name if no value is provided
            string domainNamingContext = this.NamingContext ?? this.ReplicationClient.DomainNamingContext;

            // Replicate all accounts
            foreach (var account in this.ReplicationClient.GetAccounts(domainNamingContext, progressReporter, this.Properties))
            {
                this.WriteObject(account);
            }

            // Write progress completed
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }

        protected void ReturnSingleAccount()
        {
            DSAccount account = null;

            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    account = this.ReplicationClient.GetAccount(this.DistinguishedName, this.Properties);
                    break;

                case ParameterSetByName:
                    var accountName = new NTAccount(this.Domain, this.SamAccountName);
                    account = this.ReplicationClient.GetAccount(accountName, this.Properties);
                    break;

                case ParameterSetByGuid:
                    account = this.ReplicationClient.GetAccount(this.ObjectGuid, this.Properties);
                    break;

                case ParameterSetBySid:
                    account = this.ReplicationClient.GetAccount(this.ObjectSid, this.Properties);
                    break;

                case ParameterSetByUPN:
                    var upn = new NTAccount(this.UserPrincipalName);
                    account = this.ReplicationClient.GetAccount(upn, this.Properties);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }

            this.WriteObject(account);
        }

        private new void WriteObject(object sendToPipeline)
        {
            if (this.ExportFormat != null)
            {
                // Add a virtual type to the object to change the default out-of-band View, e.g., DSInternals.Common.Data.DSAccount#PwDump.
                PSObject psObject = PSObject.AsPSObject(sendToPipeline);
                string virtualTypeName = String.Format("{0}#{1}", typeof(DSAccount).FullName, this.ExportFormat.ToString());
                psObject.TypeNames.Insert(0, virtualTypeName);
                base.WriteObject(psObject);
            }
            else
            {
                // Pass-through the original object without any changes.
                base.WriteObject(sendToPipeline);
            }
        }
        #endregion Helper Methods
    }
}
