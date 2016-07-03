namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common;
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Interop;
    using DSInternals.PowerShell.Properties;
    using DSInternals.SAM;
    using DSInternals.SAM.Interop;
    using System;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Net;
    using System.Security;
    using System.Security.Principal;

    [Cmdlet(VerbsCommon.Set, "SamAccountPasswordHash")]
    [OutputType("None")]
    public class SetSamAccountPasswordHashCommand : PSCmdlet, IDisposable
    {
        private const string DefaultServer = "localhost";
        private const string ParameterSetBySid = "BySid";
        private const string ParameterSetByLogonName = "ByLogonName";

        private string server;

        private SamServer SamServer
        {
            get;
            set;
        }

        // TODO: Support -Force parameter
        // TODO: Safe Critical everywhere?

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
            HelpMessage = @"Specify user's domain.",
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
        [ValidateNotNullOrEmpty]
        [ValidateHexString(DSInternals.Common.Cryptography.NTHash.HashSize)]
        public string NTHash
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify a new LM password hash value in hexadecimal format.",
            Mandatory = false,
            ValueFromPipelineByPropertyName = true
        )]
        [ValidateNotNullOrEmpty]
        [ValidateHexString(DSInternals.Common.Cryptography.LMHash.HashSize)]
        public string LMHash
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify the user account credentials to use to perform this task. The default credentials are the credentials of the currently logged on user.",
            Mandatory = false,
            ValueFromPipeline = false
        )]
        [ValidateNotNullOrEmpty]
        [Credential]
        public PSCredential Credential
        {
            get;
            set;
        }

        [Parameter(
            HelpMessage = "Specify the name of a SAM server.",
            Mandatory = false,
            ValueFromPipeline = false
        )]
        [PSDefaultValue(Value = DefaultServer)]
        [ValidateNotNullOrEmpty]
        public string Server
        {
            get
            {
                return this.server ?? DefaultServer;
            }
            set
            {
                this.server = value;
            }
        }

        #endregion Parameters

        public void Dispose()
        {
            this.Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            /* Connect to the specified SAM server: */
            // TODO: Extract as resource:
            WriteDebug(string.Format("Connecting to SAM server {0}.", this.Server));
            try
            {
                NetworkCredential netCred = null;
                if (this.Credential != null)
                {
                    netCred = this.Credential.GetNetworkCredential();
                    
                }
                this.SamServer = new SamServer(this.Server, netCred, SamServerAccessMask.LookupDomain);
            }
            catch (Win32Exception ex)
            {
                ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
                ErrorRecord error = new ErrorRecord(ex, "WinAPIErrorConnect", category, this.Server);
                // Terminate on this error:
                this.ThrowTerminatingError(error);
            }
        }

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
                        // TODO: Extract as resource:
                        this.WriteVerbose(string.Format("Setting password hash on account {0}\\{1}.", this.Domain, this.SamAccountName));
                        
                        if (this.Domain.Contains("."))
                        {
                            // This is not a hard check, because dots are actually allowed in NetBIOS names, although not recommended.
                            // TODO: Extract as a resource
                            this.WriteWarning("The domain name supplied appears to be a DNS name instead of NetBIOS name.");
                        }

                        // We need to translate domain name to SID:
                        domainSid = this.SamServer.LookupDomain(this.Domain);
                        break;
                    case ParameterSetBySid:
                        if (!this.Sid.IsAccountSid())
                        {
                            // Allow the processing to continue on this error:
                            // TODO: Extract as resource:
                            PSArgumentException ex = new PSArgumentException("The SID provided is not a account SID.", "Sid");
                            this.WriteError(ex.ErrorRecord);
                        }
                        // TODO: Extract as resource:
                        this.WriteVerbose(string.Format("Setting password hash on account {0}.", this.Sid));
                        // We already know the SID:
                        domainSid = this.Sid.AccountDomainSid;
                        break;
                    default:
                        // This should never happen:
                        throw new PSInvalidOperationException(Resources.InvalidParameterSetMessage);
                }
                /* Connect to the domain. */
                using (SamDomain domain = this.SamServer.OpenDomain(domainSid, SamDomainAccessMask.Lookup))
                {
                    /* Retrieve RID of the current user. */
                    int userId = (this.ParameterSetName == ParameterSetBySid) ? this.Sid.GetRid() : domain.LookupUser(this.SamAccountName);
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.SamServer != null)
            {
                this.SamServer.Dispose();
                this.SamServer = null;
            }
        }
    }
}