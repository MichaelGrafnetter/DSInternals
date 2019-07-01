namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using System.Security.Cryptography.X509Certificates;
    using DSInternals.Common.Data;

    [Cmdlet(VerbsCommon.Get, "ADKeyCredential", DefaultParameterSetName = ParamSetFromCertificate)]
    [OutputType(new Type[] { typeof(KeyCredential) })]
    public class GetADKeyCredentialCommand : PSCmdlet
    {
        #region Parameters
        private const string ParamSetFromCertificate = "FromCertificate";
        private const string ParamSetFromBinary = "FromBinary";
        private const string ParamSetFromDNBinary = "FromDNBinary";

        [Parameter(
            Mandatory = true,
            Position = 0,
            ParameterSetName = ParamSetFromDNBinary,
            ValueFromPipeline = true
        )]
        [Alias("DNWithBinary", "DistinguishedNameWithBinary")]
        public string DNWithBinaryData
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            ParameterSetName = ParamSetFromBinary
        )]
        [AcceptHexString]
        [Alias("Binary")]
        public byte[] BinaryData
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            Position = 0,
            ParameterSetName = ParamSetFromCertificate
        )]
        public X509Certificate2 Certificate
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = ParamSetFromCertificate
        )]
        [Alias("ComputerId", "ComputerGuid")]
        public Guid  DeviceId
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            ParameterSetName = ParamSetFromBinary
        )]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = ParamSetFromCertificate
        )]
        [Alias("DistinguishedName", "DN", "ObjectDN")]
        public string HolderDN
        {
            get;
            set;
        }
        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            KeyCredential keyCredential;
            switch(this.ParameterSetName)
            {
                case ParamSetFromDNBinary:
                    keyCredential = KeyCredential.Parse(this.DNWithBinaryData);
                    break;
                case ParamSetFromBinary:
                    keyCredential = new KeyCredential(this.BinaryData, this.HolderDN);
                    break;
                case ParamSetFromCertificate:
                default:
                    keyCredential = new KeyCredential(this.Certificate, this.DeviceId, this.HolderDN);
                    break;
            }
            this.WriteObject(keyCredential);
        }

        #endregion Cmdlet Overrides
    }
}
