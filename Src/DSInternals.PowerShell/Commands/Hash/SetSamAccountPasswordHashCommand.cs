namespace DSInternals.PowerShell.Commands
{
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Security.Principal;
    using DSInternals.Common;
    using DSInternals.Common.Interop;
    using DSInternals.SAM;

    [Cmdlet(VerbsCommon.Set, "SamAccountPasswordHash")]
    [OutputType("None")]
    public class SetSamAccountPasswordHashCommand : SamCommandBase
    {
        private const string ParameterSetBySid = "BySid";
        private const string ParameterSetByLogonName = "ByLogonName";
        // TODO: Support -Force parameter

        #region Parameters

        [Parameter(
            HelpMessage = @"Specify user's login.",
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetByLogonName
        )]
        [ValidateNotNullOrEmpty]
        public string SamAccountName
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = @"Specify the user's domain.",
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetByLogonName
        )]
        [ValidateNotNullOrEmpty]
        public string Domain
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = @"Specify user SID.",
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetBySid
        )]
        [ValidateNotNull]
        public SecurityIdentifier Sid
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify a new NT password hash value in hexadecimal format.",
            Mandatory = true,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNull]
        [ValidateCount(DSInternals.Common.Cryptography.NTHash.HashSize, DSInternals.Common.Cryptography.NTHash.HashSize)]
        [AcceptHexString]
        public byte[] NTHash
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify a new LM password hash value in hexadecimal format.",
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNull]
        [ValidateCount(DSInternals.Common.Cryptography.LMHash.HashSize, DSInternals.Common.Cryptography.LMHash.HashSize)]
        [AcceptHexString]
        public byte[] LMHash
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            try
            {
                /* Retrieve domain SID of the current user. */
                SecurityIdentifier domainSid;
                // TODO: Domain name to SID translation Cache
                // TODO: Get default domain from server
                switch(this.ParameterSetName)
                {
                    case ParameterSetByLogonName:
                        this.WriteVerbose(string.Format("Setting password hash on account {0}\\{1}.", this.Domain, this.SamAccountName));

                        if (this.Domain.Contains("."))
                        {
                            // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
                            this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
                        }

                        // We need to translate domain name to SID:
                        domainSid = this.SamServer.LookupDomain(this.Domain);
                        break;
                    case ParameterSetBySid:
                        if (!this.Sid.IsAccountSid())
                        {
                            // Allow the processing to continue on this error:
                            PSArgumentException ex = new PSArgumentException("The SID provided is not a account SID.", "Sid");
                            this.WriteError(ex.ErrorRecord);
                        }

                        this.WriteVerbose(string.Format("Setting password hash on account {0}.", this.Sid));
                        // We already know the SID:
                        domainSid = this.Sid.AccountDomainSid;
                        break;
                    default:
                        // This should never happen:
                        throw new PSInvalidOperationException(InvalidParameterSetMessage);
                }
                /* Connect to the domain. */
                using (SamDomain domain = this.SamServer.OpenDomain(domainSid, SamDomainAccessMask.Lookup))
                {
                    /* Retrieve RID of the current user. */
                    int userId = (this.ParameterSetName == ParameterSetBySid) ? this.Sid.GetRid() : domain.LookupUser(this.SamAccountName, throwIfNotFound: true).Value;
                    /* Open the user account and reset password: */
                    using (SamUser user = domain.OpenUser(userId, SamUserAccessMask.ForcePasswordChange))
                    {
                        user.SetPasswordHash(this.NTHash, this.LMHash);
                    }
                }
            }
            catch (Win32Exception ex)
            {
                ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
                object identity = (this.Sid != null) ? this.Sid.ToString() : this.SamAccountName;
                ErrorRecord error = new ErrorRecord(ex, "WinAPIErrorProcess", category, identity);
                // Allow the processing to continue on this error:
                this.WriteError(error);
            }
        }

        #endregion Cmdlet Overrides
    }
}
