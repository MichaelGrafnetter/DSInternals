using DSInternals.Common;
using DSInternals.Common.Cryptography;
using System;
using System.Management.Automation;
using System.Security;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsData.ConvertTo, "OrgIdHash", DefaultParameterSetName = "FromHash")]
    [OutputType(new Type[] { typeof(string) })]
    public class ConvertToOrgIdHashCommand : PSCmdlet
    {
        #region Parameters

        [Parameter(
            ParameterSetName = "FromPassword",
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "Provide a password in the form of a SecureString."
        )]
        [Alias("p")]
        [ValidatePasswordLength(0, DSInternals.Common.Cryptography.NTHash.MaxInputLength)]
        public SecureString Password
        {
            get;
            set;
        }

        [Parameter(
            ParameterSetName = "FromHash",
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "Provide a 16-byte NT Hash of user's password in hexadecimal format."
        )]
        [ValidateNotNull]
        [ValidateHexString(DSInternals.Common.Cryptography.NTHash.HashSize)]
        [Alias("h")]
        public string NTHash
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "Provide a 10-byte salt in hexadecimal format."
        )]
        [ValidateNotNull]
        [ValidateHexString(OrgIdHash.SaltSize)]
        [Alias("s")]
        public string Salt
        {
            get;
            set;
        }

        #endregion Parameters

        #region Cmdlet Overrides

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Calculating OrgId hash.");
            try
            {
                byte[] binarySalt = null;
                if (Salt != null)
                {
                    binarySalt = Salt.HexToBinary();
                }
                string orgIdHash;
                // TODO: Switch by parametersetname
                if (NTHash != null)
                {
                    // Calculate OrgId hash from NT Hash:
                    byte[] binaryNTHash = NTHash.HexToBinary();
                    orgIdHash = OrgIdHash.ComputeFormattedHash(binaryNTHash, binarySalt);
                }
                else
                {
                    // Calculate OrgId hash from password:
                    orgIdHash = OrgIdHash.ComputeFormattedHash(Password, binarySalt);
                }
                this.WriteObject(orgIdHash);
            }
            catch (Exception ex)
            {
                ErrorRecord error = new ErrorRecord(ex, "Error1", ErrorCategory.NotSpecified, this.Password);
                this.WriteError(error);
            }
        }

        #endregion Cmdlet Overrides
    }
}