namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Data;
    using DSInternals.PowerShell.Properties;
    using System;
    using System.Management.Automation;
    using System.Security.Principal;

    [Cmdlet(VerbsCommon.Get, "ADReplAccount")]
    [OutputType(typeof(DSAccount))]
    public class GetADReplAccountCommand : ADReplObjectCommandBase
    {
        protected const string parameterSetByName = "ByName";
        protected const string parameterSetBySid = "BySID";

        // Validate Mask domain\user
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetByName
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Login", "sam", "AccountName","User")]
        public string SamAccountName
        {
            get;
            set;
        }
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetByName
        )]
        [ValidateNotNullOrEmpty]
        [Alias("AccountDomain", "UserDomain")]
        public string Domain
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            HelpMessage = "TODO",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = parameterSetBySid
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Sid")]
        public SecurityIdentifier ObjectSid
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            // TODO: Error processing
            if (this.ParameterSetName == ParameterSetAll)
            {
                this.ReturnAllAccounts();
            }
            else
            {
                this.ReturnSingleAccount();
            }
        }

        protected void ReturnAllAccounts()
        {
            foreach (var account in this.ReplicationClient.GetAccounts(this.NamingContext))
            {
                this.WriteObject(account);
            }
        }

        protected void ReturnSingleAccount()
        {
            DSAccount account;
            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    account = this.ReplicationClient.GetAccount(this.DistinguishedName);
                    break;

                case parameterSetByName:
                    if(this.Domain.Contains("."))
                    {
                        // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
                        // TODO: Extract as a resource
                        this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
                    }
                    var accountName = new NTAccount(this.Domain, this.SamAccountName);
                    account = this.ReplicationClient.GetAccount(accountName);
                    break;

                case ParameterSetByGuid:
                    account = this.ReplicationClient.GetAccount(this.ObjectGuid);
                    break;

                case parameterSetBySid:
                    account = this.ReplicationClient.GetAccount(this.ObjectSid);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
            }
            this.WriteObject(account);
        }
    }
}
