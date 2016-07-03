namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Security;
    using DSInternals.Common.Cryptography;

    [Cmdlet(VerbsData.ConvertTo, "GPPrefPassword")]
    [OutputType(new Type[] { typeof(string) })]
    public class ConvertToGPPrefPasswordCommand : PSCmdlet
    {
        #region Parameters

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "Provide a password in the form of a SecureString."
        )]
        [Alias("p")]
        [ValidateNotNull]
        public SecureString Password
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Encrypting GP Preferences password.");
            try
            {
                string encryptedPassword = GPPrefPwdObfuscator.Encrypt(Password);
                this.WriteObject(encryptedPassword);
            }
            catch (ArgumentException ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.InvalidArgument, this.Password);
                this.WriteError(error);
            }
        }

        #endregion Cmdlet Overrides
    }
}