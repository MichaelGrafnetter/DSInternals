using System.Management.Automation;
using System.Security.Principal;

namespace DSInternals.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Add, "ADReplNgcKey")]
    [OutputType("None")]
    public class AddADReplNgcKeyCommand : ADReplPrincipalCommandBase
    {
        // TODO: Change to X509Certificate2
        [Parameter(Mandatory = true)]
        [AcceptHexString]
        public byte[] PublicKey
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case ParameterSetByGuid:
                    this.ReplicationClient.WriteNgcKey(this.ObjectGuid, this.PublicKey);
                    break;

                case ParameterSetByDN:
                    this.ReplicationClient.WriteNgcKey(this.DistinguishedName, this.PublicKey);
                    break;

                case ParameterSetByName:
                    this.ReplicationClient.WriteNgcKey(new NTAccount(this.Domain, this.SamAccountName), this.PublicKey);
                    break;

                case ParameterSetBySid:
                    this.ReplicationClient.WriteNgcKey(this.ObjectSid, this.PublicKey);
                    break;

                case ParameterSetByUPN:
                    this.ReplicationClient.WriteNgcKey(new NTAccount(this.UserPrincipalName), this.PublicKey);
                    break;
            }
        }
    }
}
