namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Common.Cryptography;

    [Cmdlet(VerbsData.ConvertFrom, "GPPrefPassword")]
    [OutputType(new Type[] { typeof(string) })]
    public class ConvertFromGPPrefPasswordCommand : PSCmdlet
    {
        #region Parameters

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            HelpMessage = "Provide an encrypted password from a Group Policy Preferences XML file."
        )]
        public string EncryptedPassword
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            WriteVerbose("Decrypting GP Preferences password.");
            try
            {
                string password = GPPrefPwdObfuscator.Decrypt(EncryptedPassword);
                this.WriteObject(password);
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.NotSpecified, EncryptedPassword);
                this.WriteError(error);
            }
        }

        #endregion Cmdlet Overrides
    }
}