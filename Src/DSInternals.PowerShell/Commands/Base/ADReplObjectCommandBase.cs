using System;
using System.Management.Automation;

namespace DSInternals.PowerShell.Commands
{
    public abstract class ADReplObjectCommandBase : ADReplCommandBase
    {
        protected const string ParameterSetByGuid = "ByGuid";
        protected const string ParameterSetByDN = "ByDN";

        #region Parameters

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetByDN
        )]
        [ValidateNotNullOrEmpty]
        [Alias("dn")]
        public string DistinguishedName
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParameterSetByGuid
        )]
        [ValidateNotNullOrEmpty]
        [Alias("Guid")]
        public Guid ObjectGuid
        {
            get;
            set;
        }

        #endregion Parameters
    }
}
