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
        [PSDefaultValue(Value = KerberosKeyType.AES256_CTS_HMAC_SHA1_96)]
        [Alias("e", "ETYPE", "k")]
        public KerberosKeyType KeyType
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = false,
            Position = 3
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
            if(this.KeyType == KerberosKeyType.NULL)
            {
                this.KeyType = KerberosKeyType.AES256_CTS_HMAC_SHA1_96;
            }

            if(this.Iterations < 1)
            {
                this.Iterations = KerberosKeyDerivation.DefaultIterationCount;
            }
        }

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Calculating Kerberos key.");
            // TODO: Using some unsupported KerberosKeyType values will result in a strange exception.
            var key = new KerberosKeyDataNew(this.KeyType, this.Password, this.Salt, this.Iterations);
            this.WriteObject(key);
        }
        #endregion Cmdlet Overrides
    }
}