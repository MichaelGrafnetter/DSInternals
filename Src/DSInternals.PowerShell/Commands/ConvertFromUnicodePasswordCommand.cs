namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Text;
    using DSInternals.Common;

    [Cmdlet(VerbsData.ConvertFrom, "UnicodePassword")]
    [OutputType(typeof(string))]
    public class ConvertFromUnicodePasswordCommand : PSCmdlet
    {
        // TODO: Extract this routine as a class in DSInternals.Cryptography?
        private const string adminPwdSuffix = "AdministratorPassword";

        private const string pwdSuffix = "Password";

        #region Parameters

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = false,
            Position = 0
        )]
        [ValidateNotNullOrEmpty]
        public string UnicodePassword
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            byte[] binaryPassword = Convert.FromBase64String(this.UnicodePassword);
            string plainPassword = Encoding.Unicode.GetString(binaryPassword);
            if (plainPassword.EndsWith(adminPwdSuffix))
            {
                this.WriteObject(plainPassword.TrimEnd(adminPwdSuffix));
            }
            else if (plainPassword.EndsWith(pwdSuffix))
            {
                this.WriteObject(plainPassword.TrimEnd(pwdSuffix));
            }
            else
            {
                this.WriteObject(plainPassword);
            }
        }

        #endregion Cmdlet Overrides
    }
}