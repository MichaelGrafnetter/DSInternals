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

            // TODO: AES SHA2 ETypes are not yet supported by the crypto library
            /*
            var aes256sha2 = new KerberosKeyDataNew(KerberosKeyType.AES256_CTS_HMAC_SHA384_192, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes256sha2);

            var aes128sha2 = new KerberosKeyDataNew(KerberosKeyType.AES128_CTS_HMAC_SHA256_128, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes128sha2);
            */

            var aes256sha1 = new KerberosKeyDataNew(KerberosKeyType.AES256_CTS_HMAC_SHA1_96, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes256sha1);

            var aes128sha1 = new KerberosKeyDataNew(KerberosKeyType.AES128_CTS_HMAC_SHA1_96, this.Password, this.Salt, this.Iterations);
            this.WriteObject(aes128sha1);

            var des = new KerberosKeyDataNew(KerberosKeyType.DES_CBC_MD5, this.Password, this.Salt, this.Iterations);
            this.WriteObject(des);

            var rc4 = new KerberosKeyDataNew(KerberosKeyType.RC4_HMAC_NT, this.Password, this.Salt, this.Iterations);
            this.WriteObject(rc4);
        }
        #endregion Cmdlet Overrides
    }
}
