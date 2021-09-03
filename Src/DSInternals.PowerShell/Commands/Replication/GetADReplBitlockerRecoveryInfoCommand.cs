namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using DSInternals.Common.Data;
    using DSInternals.Replication;
    using DSInternals.Replication.Model;

    [Cmdlet(VerbsCommon.Get, "ADReplBitlockerRecoveryInfo")]
    [OutputType(typeof(BitlockerRecoveryInfo))]
    public class GetADReplBitlockerRecoveryInfoCommand : ADReplCommandBase
    {
        #region Constants
        protected string recoveryGuid = null;
        protected string domainDN = null;
        protected string exportKeysPath = null;
        #endregion Constants

        #region Parameters
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [Alias("FQDN", "DomainName", "DNSDomainName")]
        public string Domain
        {
            get;
            set;
        }

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

            if (string.IsNullOrEmpty(this.Domain))
            {
                // Automatically infer DC's domain name.
                domainDN = this.ReplicationClient.DomainNamingContext;
            }
            else
            {
                if (!this.Domain.Contains("."))
                {
                    // This is not a hard check, because root domain does not need to have a dot in it.
                    // TODO: Extract as a resource
                    this.WriteWarning("The domain name supplied appears to be a NetBIOS name instead of DNS name.");
                }

                // Convert Domain DNS name to distinguished name
                domainDN = DistinguishedName.GetDNFromDNSName(this.Domain).ToString();
            }

            if (RecoveryGuid != null && RecoveryGuid.Length > 0)
            {
                this.recoveryGuid = RecoveryGuid;
            }

            if (ExportKeysPath != null && ExportKeysPath.Length > 0)
            {
                this.exportKeysPath = ExportKeysPath;
            }

            this.ReturnBitlockerRecoveryInfo();
        }
        #endregion Cmdlet Overrides

        #region Helper Methods
        protected void ReturnBitlockerRecoveryInfo()
        {
            List<BitlockerRecoveryInfo> bitlockerRecoveryInfoList = new List<BitlockerRecoveryInfo>();
            
            var progress = new ProgressRecord(1, "Replication", "Replicating Active Directory objects.");
            ulong bitlockerCount = 0;

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

            foreach (var bitlockerRecoveryData in this.ReplicationClient.GetBitlockerRecoveryData(domainDN, progressReporter, this.exportKeysPath))
            {
                if (this.recoveryGuid != null)
                {
                    if (this.recoveryGuid.Equals(bitlockerRecoveryData.RecoveryGuid.ToString()))
                    {
                        this.WriteObject(bitlockerRecoveryData);
                        bitlockerCount++;
                        break;
                    }

                    continue;
                }

                this.WriteObject(bitlockerRecoveryData);
                bitlockerCount++;
            }

            // Write progress completed
            progress.RecordType = ProgressRecordType.Completed;
            this.WriteProgress(progress);
        }
        #endregion Helper Methods
    }
}
