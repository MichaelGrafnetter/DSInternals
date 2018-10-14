namespace DSInternals.PowerShell.Commands
{
    using System;
    using System.Management.Automation;
    using DSInternals.Replication;
    using System.Net;
    using DSInternals.Common.Data;

    public abstract class ADReplObjectCommandBase : ADReplCommandBase
    {
        protected const string ParameterSetByGuid = "ByGuid";
        protected const string ParameterSetByDN = "ByDN";
        protected const string ParameterSetAll = "All";

        #region Parameters

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [Alias("AllAccounts", "ReturnAllAccounts")]
        public SwitchParameter All
        {
            get;
            set;
        }

        [Parameter(
            Mandatory = true,
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

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        [ValidateNotNullOrEmpty]
        [Alias("NC", "DomainNC","DomainNamingContext")]
        public string NamingContext
        {
            get;
            set;
        }

        #endregion Parameters
    }
}