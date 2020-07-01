using System;
using System.Management.Automation;
using DSInternals.Common.AzureAD;

namespace DSInternals.PowerShell.Commands
{
    public abstract class AzureADCommandBase : PSCmdlet, IDisposable
    {
        protected const string ParamSetSingleUserId = "ById";
        protected const string ParamSetSingleUserUPN = "ByUPN";

        [Parameter(Mandatory = true)]
        [Alias("Token")]
        public string AccessToken
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParamSetSingleUserId)]
        [Alias("Identity", "Id", "UserId", "ObjectGuid")]
        public Guid? ObjectId
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ParameterSetName = ParamSetSingleUserUPN)]
        [Alias("UPN", "UserName")]
        public string UserPrincipalName
        {
            get;
            set;
        }


        [Parameter(Mandatory = false)]
        [Alias("Tenant")]
        public Guid? TenantId
        {
            get;
            set;
        }

        protected AzureADClient Client
        {
            get;
            set;
        }

        protected override void BeginProcessing()
        {
            Client = new AzureADClient(AccessToken, TenantId);
        }

        #region IDisposable Support
        public virtual void Dispose()
        {
            if(Client != null)
            {
                Client.Dispose();
            }
        }
        #endregion
    }
}
