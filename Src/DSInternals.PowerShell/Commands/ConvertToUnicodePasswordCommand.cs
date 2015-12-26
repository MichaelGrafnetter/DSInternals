using DSInternals.Common;
using System;
using System.Management.Automation;
using System.Security;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsData.ConvertTo, "UnicodePassword")]
    [OutputType(new Type[] { typeof(String) })]
    public class ConvertToUnicodePasswordCommand : PSCmdlet
    {
        private const string passwordSuffix = "AdministratorPassword";

        #region Parameters

        [Parameter(
            Mandatory = true,
            ValueFromPipeline = false,
            Position = 0
            )]
        [ValidateNotNullOrEmpty]
        public SecureString Password
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = false
            )]
        public SwitchParameter IsUnattendPassword
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void BeginProcessing()
        {
            if (this.IsUnattendPassword.IsPresent)
            {
                // TODO: Move to DSInternals.Cryptography
                this.Password.Append(passwordSuffix);
            }
            byte[] bytes = this.Password.ToByteArray();
            try
            {
                string base64 = Convert.ToBase64String(bytes);
                this.WriteObject(base64);
            }
            finally
            {
                bytes.ZeroFill();
            }
        }

        #endregion Cmdlet Overrides
    }
}