﻿using System.Management.Automation;
using DSInternals.Common.Data;
using DSInternals.DataStore;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "ADDBAccountPasswordHash")]
    [OutputType("None")]
    public class SetADDBAccountPasswordHashCommand : ADDBModifyPrincipalCommandBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [AcceptHexString()]
        [Alias("Hash", "PasswordHash", "NTLMHash", "MD4Hash", "h")]
        [ValidateNotNull]
        [ValidateCount(DSInternals.Common.Cryptography.NTHash.HashSize, DSInternals.Common.Cryptography.NTHash.HashSize)]
        public byte[] NTHash
        {
            get;
            set;
        }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        [Alias("KerberosKeys", "sc", "c")]
        [ValidateNotNull]
        public SupplementalCredentials SupplementalCredentials
        {
            get;
            set;
        }

        [Parameter(Mandatory = true)]
        [ValidateCount(BootKeyRetriever.BootKeyLength, BootKeyRetriever.BootKeyLength)]
        [AcceptHexString]
        [Alias("Key", "SysKey", "SystemKey")]
        public byte[] BootKey
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            //TODO: Exception handling: Object not found, malformed DN, ...
            // TODO: Extract as Resource
            string verboseMessage = "Setting password hash for account {0}.";
            bool hasChanged;
            switch (this.ParameterSetName)
            {
                case parameterSetByDN:
                    this.WriteVerbose(String.Format(verboseMessage, this.DistinguishedName));
                    var dn = new DistinguishedName(this.DistinguishedName);
                    hasChanged = this.DirectoryAgent.SetAccountPasswordHash(dn, this.NTHash, this.SupplementalCredentials, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetByName:
                    this.WriteVerbose(String.Format(verboseMessage, this.SamAccountName));
                    hasChanged = this.DirectoryAgent.SetAccountPasswordHash(this.SamAccountName, this.NTHash, this.SupplementalCredentials, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetByGuid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectGuid));
                    hasChanged = this.DirectoryAgent.SetAccountPasswordHash(this.ObjectGuid, this.NTHash, this.SupplementalCredentials, this.BootKey, this.SkipMetaUpdate);
                    break;

                case parameterSetBySid:
                    this.WriteVerbose(String.Format(verboseMessage, this.ObjectSid));
                    hasChanged = this.DirectoryAgent.SetAccountPasswordHash(this.ObjectSid, this.NTHash, this.SupplementalCredentials, this.BootKey, this.SkipMetaUpdate);
                    break;

                default:
                    // This should never happen:
                    throw new PSInvalidOperationException(InvalidParameterSetMessage);
            }
            this.WriteVerboseResult(hasChanged);
        }
    }
}
