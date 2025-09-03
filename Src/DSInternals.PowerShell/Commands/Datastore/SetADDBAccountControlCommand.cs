﻿using System;
using System.Management.Automation;
using DSInternals.Common.Data;

namespace DSInternals.PowerShell.Commands
{
    /// <summary>
    /// <summary>
    /// Implements the Set-ADDBAccountControl PowerShell cmdlet for modifying user account control (UAC) values for an Active Directory account.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "ADDBAccountControl")]
    [OutputType("None")]
    public class SetADDBAccountControlCommand : ADDBModifyPrincipalCommandBase
    {
        /// <summary>
        /// Indicates whether an account is enabled.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the Enabled.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Indicates whether an account can change its password.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the CannotChangePassword.
        /// </summary>
        public bool? CannotChangePassword { get; set; }

        /// <summary>
        /// Indicates whether the password of an account can expire.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the PasswordNeverExpires.
        /// </summary>
        public bool? PasswordNeverExpires { get; set; }

        /// <summary>
        /// Indicates whether a smart card is required to logon.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the SmartcardLogonRequired.
        /// </summary>
        public bool? SmartcardLogonRequired { get; set; }

        /// <summary>
        /// Indicates whether the account is restricted to use only Data Encryption Standard encryption types for keys.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the UseDESKeyOnly.
        /// </summary>
        public bool? UseDESKeyOnly { get; set; }

        /// <summary>
        /// Indicates whether a home directory is required for the account.
        /// </summary>
        [Parameter(Mandatory = false)]
        /// <summary>
        /// Gets or sets the HomedirRequired.
        /// </summary>
        public bool? HomedirRequired { get; set; }

        protected override void BeginProcessing()
        {
            // Validate the parameters:
            if (this.Enabled == null &&
                this.CannotChangePassword == null &&
                this.PasswordNeverExpires == null &&
                this.SmartcardLogonRequired == null &&
                this.UseDESKeyOnly == null &&
                this.HomedirRequired == null)
            {
                this.ThrowTerminatingError(
                    new ErrorRecord(
                        new ArgumentException("At least one of the parameters must be specified."),
                        "SetADDBAccountControl_ParameterRequired",
                        ErrorCategory.InvalidArgument,
                        null));
            }

            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            bool hasChanged;

            string verboseMessage = "Configuring account {0}.";

            switch (this.ParameterSetName)
            {
                case ParameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);

                    hasChanged = this.DirectoryAgent.SetAccountControl(
                        dn,
                        this.Enabled,
                        this.CannotChangePassword,
                        this.PasswordNeverExpires,
                        this.SmartcardLogonRequired,
                        this.UseDESKeyOnly,
                        this.HomedirRequired,
                        this.SkipMetaUpdate);
                    break;

                case ParameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetAccountControl(
                        this.SamAccountName,
                        this.Enabled,
                        this.CannotChangePassword,
                        this.PasswordNeverExpires,
                        this.SmartcardLogonRequired,
                        this.UseDESKeyOnly,
                        this.HomedirRequired,
                        this.SkipMetaUpdate);
                    break;

                case ParameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetAccountControl(
                        this.ObjectGuid,
                        this.Enabled,
                        this.CannotChangePassword,
                        this.PasswordNeverExpires,
                        this.SmartcardLogonRequired,
                        this.UseDESKeyOnly,
                        this.HomedirRequired,
                        this.SkipMetaUpdate);
                    break;

                case ParameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetAccountControl(
                        this.ObjectSid,
                        this.Enabled,
                        this.CannotChangePassword,
                        this.PasswordNeverExpires,
                        this.SmartcardLogonRequired,
                        this.UseDESKeyOnly,
                        this.HomedirRequired,
                        this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
