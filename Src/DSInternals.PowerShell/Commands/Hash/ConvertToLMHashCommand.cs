namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Security;
    using DSInternals.Common;
    using DSInternals.Common.Interop;
    using DSInternals.Common.Cryptography;

    [Cmdlet(VerbsData.ConvertTo, "LMHash")]
    [OutputType(new Type[] { typeof(string) })]
    public class ConvertToLMHashCommand : PSCmdlet
    {
        #region Parameters

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            HelpMessage = "Provide a password in the form of a SecureString."
        )]
        [Alias("p")]
        [ValidatePasswordLength(0, 14)]
        public SecureString Password
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            WriteVerbose("Calculating LM hash.");
            try
            {
                byte[] hashBytes = LMHash.ComputeHash(this.Password);
                string hashHex = hashBytes.ToHex();
                this.WriteObject(hashHex);
            }
            catch (ArgumentException ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.InvalidArgument, this.Password);
                this.WriteError(error);
            }
            catch (Win32Exception ex)
            {
                // TODO: Handle password length.
                ErrorCategory category = ((Win32ErrorCode)ex.NativeErrorCode).ToPSCategory();
                ErrorRecord error = new ErrorRecord(ex, "Error2", category, this.Password);
                // Allow the processing to continue on this error:
                this.WriteError(error);
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Error3", ErrorCategory.NotSpecified, this.Password);
                this.WriteError(error);
            }
        }

        #endregion Cmdlet Overrides
    }
}
