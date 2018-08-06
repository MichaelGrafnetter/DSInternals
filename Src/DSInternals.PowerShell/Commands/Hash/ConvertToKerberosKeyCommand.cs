namespace DSInternals.PowerShell.Commands
{
    using DSInternals.Common.Cryptography;
    using DSInternals.Common.Data;
    using System;
    using System.Management.Automation;
    using System.Security;

    [Cmdlet(VerbsData.ConvertTo, "KerberosKey")]
    [OutputType(new Type[] { typeof(KerberosKeyDataNew) })]
    public class ConvertToKerberosKeyCommand : PSCmdlet
    {
        #region Parameters
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "Provide a password in the form of a SecureString."
        )]
        [Alias("p")]
        public SecureString Password
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            Position = 1
        )]
        [Alias("s")]
        public string Salt
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = false,
            Position = 2
        )]
        [Alias("i")]
        [ValidateNotNull()]
        [ValidateRange(1, int.MaxValue)]
        [PSDefaultValue(Value = KerberosKeyDerivation.DefaultIterationCount)]
        public int Iterations
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides
        protected override void BeginProcessing()
        {
            // Set default values
            if(this.Iterations < 1)
            {
                this.Iterations = KerberosKeyDerivation.DefaultIterationCount;
            }
        }

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Calculating Kerberos keys.");

            var aes256 = new KerberosKeyDataNew(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes256);

            var aes128 = new KerberosKeyDataNew(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes128);

            var des = new KerberosKeyDataNew(KerberosKeyType.DES_CBC_MD5, this.Password, this.Salt, this.Iterations);
            this.WriteObject(des);
        }
        #endregion Cmdlet Overrides
    }
}